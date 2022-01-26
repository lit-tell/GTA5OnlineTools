using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using GTA5OnlineTools.Common.Utils;

namespace GTA5OnlineTools.Modules.Windows
{
    /// <summary>
    /// BigBaseV2Window.xaml 的交互逻辑
    /// </summary>
    public partial class BigBaseV2Window : Window
    {
        // OpenProcess signture https://www.pinvoke.net/default.aspx/kernel32.openprocess
        [Flags]
        public enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VirtualMemoryOperation = 0x00000008,
            VirtualMemoryRead = 0x00000010,
            VirtualMemoryWrite = 0x00000020,
            DuplicateHandle = 0x00000040,
            CreateProcess = 0x000000080,
            SetQuota = 0x00000100,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            QueryLimitedInformation = 0x00001000,
            Synchronize = 0x00100000
        }

        // VirtualAllocEx signture https://www.pinvoke.net/default.aspx/kernel32.virtualallocex
        [Flags]
        public enum AllocationType
        {
            Commit = 0x1000,
            Reserve = 0x2000,
            Decommit = 0x4000,
            Release = 0x8000,
            Reset = 0x80000,
            Physical = 0x400000,
            TopDown = 0x100000,
            WriteWatch = 0x200000,
            LargePages = 0x20000000
        }

        [Flags]
        public enum MemoryProtection
        {
            Execute = 0x10,
            ExecuteRead = 0x20,
            ExecuteReadWrite = 0x40,
            ExecuteWriteCopy = 0x80,
            NoAccess = 0x01,
            ReadOnly = 0x02,
            ReadWrite = 0x04,
            WriteCopy = 0x08,
            GuardModifierflag = 0x100,
            NoCacheModifierflag = 0x200,
            WriteCombineModifierflag = 0x400
        }

        /////////////////////////////////////////////////////////////////////

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, int processId);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, AllocationType dwFreeType);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] lpBuffer, int dwSize, out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(IntPtr hwnd);

        /////////////////////////////////////////////////////////////////////

        private InjectInfo InjectInfo { get; set; }

        /////////////////////////////////////////////////////////////////////

        public BigBaseV2Window()
        {
            InitializeComponent();
        }

        private void Window_BigBaseV2_Loaded(object sender, RoutedEventArgs e)
        {
            Task t = new Task(() =>
            {
                InjectInfo = new InjectInfo();

                InjectInfo.DLLPath = FileUtil.Temp_Path + "Bread.dll";

                Process process = Process.GetProcessesByName("GTA5")[0];
                InjectInfo.PID = process.Id;
                InjectInfo.PName = process.ProcessName;
                InjectInfo.MWindowHandle = process.MainWindowHandle;
            });
            t.Start();

            AppendTextBox("等待用户操作...");
        }

        private void Window_BigBaseV2_Closing(object sender, CancelEventArgs e)
        {

        }

        private void AppendTextBox(string str)
        {
            TextBox_Log.AppendText($"[{DateTime.Now:T}] {str}\r\n");
            TextBox_Log.ScrollToEnd();
        }

        private void RadioButton_InjectDLL_Bread_Click(object sender, RoutedEventArgs e)
        {
            if (RadioButton_InjectDLL_Bread.IsChecked == true)
            {
                InjectInfo.DLLPath = FileUtil.Temp_Path + "Bread.dll";
            }
            else if (RadioButton_InjectDLL_Bread_Chs.IsChecked == true)
            {
                InjectInfo.DLLPath = FileUtil.Temp_Path + "Bread_Chs.dll";
            }
            else if (RadioButton_InjectDLL_PackedStatEditor.IsChecked == true)
            {
                InjectInfo.DLLPath = FileUtil.Temp_Path + "PackedStatEditor.dll";
            }
        }

        private void Button_BigBaseV2_Inject_Click(object sender, RoutedEventArgs e)
        {
            if (InjectInfo.PID == 0)
            {
                AppendTextBox("未找到GTA5进程");
                return;
            }
            else if (string.IsNullOrEmpty(InjectInfo.DLLPath))
            {
                AppendTextBox("DLL路径为空");
                return;
            }

            foreach (ProcessModule module in Process.GetProcessById(InjectInfo.PID).Modules)
            {
                if (module.FileName == InjectInfo.DLLPath)
                {
                    AppendTextBox("该DLL已经被注入过了");
                    return;
                }
            }

            try
            {
                SetForegroundWindow(InjectInfo.MWindowHandle);

                DLL_Inject(InjectInfo.PID, InjectInfo.DLLPath);
                AppendTextBox($"DLL注入到进程 {InjectInfo.PName} 成功");
            }
            catch (Exception ex)
            {
                AppendTextBox($"发生错误：{ex.Message}");
            }
        }

        private void DLL_Inject(int ProcId, string DllPath)
        {
            IntPtr Size = (IntPtr)DllPath.Length;

            IntPtr ProcHandle = OpenProcess(ProcessAccessFlags.All, false, ProcId);

            IntPtr DllSpace = VirtualAllocEx(ProcHandle, IntPtr.Zero, Size, AllocationType.Reserve | AllocationType.Commit, MemoryProtection.ExecuteReadWrite);

            byte[] bytes = Encoding.ASCII.GetBytes(DllPath);
            bool DllWrite = WriteProcessMemory(ProcHandle, DllSpace, bytes, (int)bytes.Length, out var bytesread);

            IntPtr Kernel32Handle = GetModuleHandle("Kernel32.dll");
            IntPtr LoadLibraryAAddress = GetProcAddress(Kernel32Handle, "LoadLibraryA");

            IntPtr RemoteThreadHandle = CreateRemoteThread(ProcHandle, IntPtr.Zero, 0, LoadLibraryAAddress, DllSpace, 0, IntPtr.Zero);

            //bool FreeDllSpace = VirtualFreeEx(ProcHandle, DllSpace, 0, AllocationType.Release);

            CloseHandle(RemoteThreadHandle);

            CloseHandle(ProcHandle);
        }

        private void Button_Located_Stats_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ProcessUtil.OpenLink(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/BigBaseV2/");
            }
            catch (Exception ex)
            {
                MsgBoxUtil.ExceptionMsgBox(ex);
            }
        }

        private bool IsInputNumber(KeyEventArgs e)
        {
            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
               e.Key == Key.Delete || e.Key == Key.Back || e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Tab)
            {
                // 按下了Alt、ctrl、shift等修饰键
                if (e.KeyboardDevice.Modifiers != ModifierKeys.None)
                {
                    e.Handled = true;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                // 按下了字符等其它功能键
                e.Handled = true;
            }
            return false;
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            IsInputNumber(e);
        }

        private void Button_Calculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RadioButton_ToMS.IsChecked == true)
                {
                    if (TextBox_d.Text != "" && TextBox_h.Text != "" && TextBox_m.Text != "" && TextBox_s.Text != "")
                    {
                        TextBox_ms.Text = (
                        (Convert.ToInt64(TextBox_d.Text) * 86400 +
                        Convert.ToInt64(TextBox_h.Text) * 3600 +
                        Convert.ToInt64(TextBox_m.Text) * 60 +
                        Convert.ToInt64(TextBox_s.Text)) * 1000
                        ).ToString();
                    }
                }
                else if (RadioButton_ToTIME.IsChecked == true)
                {
                    if (TextBox_ms.Text != "")
                    {
                        if (Convert.ToInt64(TextBox_ms.Text) >= 1000)
                        {
                            long t = Convert.ToInt64(TextBox_ms.Text) / 1000;

                            long day = 0, hour = 0, minute = 0, second = 0;
                            if (t >= 86400)         // 天
                            {
                                day = Convert.ToInt64(t / 86400);
                                hour = Convert.ToInt64((t % 86400) / 3600);
                                minute = Convert.ToInt64((t % 86400 % 3600) / 60);
                                second = Convert.ToInt64(t % 86400 % 3600 % 60);
                            }
                            else if (t >= 3600)     // 时
                            {
                                hour = Convert.ToInt64(t / 3600);
                                minute = Convert.ToInt64((t % 3600) / 60);
                                second = Convert.ToInt64(t % 3600 % 60);
                            }
                            else if (t >= 60)       // 分
                            {
                                minute = Convert.ToInt64(t / 60);
                                second = Convert.ToInt64(t % 60);
                            }
                            else
                            {
                                second = Convert.ToInt64(t);
                            }

                            TextBox_d.Text = day.ToString();
                            TextBox_h.Text = hour.ToString();
                            TextBox_m.Text = minute.ToString();
                            TextBox_s.Text = second.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBoxUtil.ExceptionMsgBox(ex);
            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            ProcessUtil.OpenLink(e.Uri.OriginalString);
            e.Handled = true;
        }
    }

    public class InjectInfo
    {
        public int PID { get; set; }
        public string DLLPath { get; set; }
        public string PName { get; set; }
        public IntPtr MWindowHandle { get; set; }
    }
}
