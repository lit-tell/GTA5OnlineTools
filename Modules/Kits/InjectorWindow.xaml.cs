using System;
using System.Text;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace GTA5OnlineTools.Modules.Kits
{
    /// <summary>
    /// InjectorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class InjectorWindow : Window
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

        public InjectorWindow()
        {
            InitializeComponent();
        }

        private void Window_Injector_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Process process in Process.GetProcesses())
            {
                ListBox_Process.Items.Add(new ProcessList()
                {
                    PID = process.Id,
                    PName = process.ProcessName,
                    MWindowTitle = process.MainWindowTitle
                });
            }

            InjectInfo = new InjectInfo();
        }

        private void Button_Inject_Click(object sender, RoutedEventArgs e)
        {
            if (InjectInfo.PID == 0)
            {
                TextBlock_Status.Text = "请选择目标进程";
                return;
            }
            else if (string.IsNullOrEmpty(InjectInfo.DLLPath))
            {
                TextBlock_Status.Text = "请选择dll路径";
                return;
            }

            try
            {
                foreach (ProcessModule module in Process.GetProcessById(InjectInfo.PID).Modules)
                {
                    if (module.FileName == InjectInfo.DLLPath)
                    {
                        TextBlock_Status.Text = "该DLL已经被注入过了";
                        return;
                    }
                }

                DLL_Inject(InjectInfo.PID, InjectInfo.DLLPath);
                TextBlock_Status.Text = $"DLL注入到进程 {InjectInfo.PName} 成功";
                SetForegroundWindow(InjectInfo.MWindowHandle);
            }
            catch (Exception ex)
            {
                TextBlock_Status.Text = ex.Message;
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

        private void CheckBox_OnlyShowWindowProcess_Click(object sender, RoutedEventArgs e)
        {
            ListBox_Process.Items.Clear();

            if (CheckBox_OnlyShowWindowProcess.IsChecked == true)
            {
                foreach (Process process in Process.GetProcesses())
                {
                    if (!string.IsNullOrEmpty(process.MainWindowTitle))
                    {
                        ListBox_Process.Items.Add(new ProcessList()
                        {
                            PID = process.Id,
                            PName = process.ProcessName,
                            MWindowTitle = process.MainWindowTitle,
                            MWindowHandle = process.MainWindowHandle,
                        });
                    }
                }
            }
            else
            {
                foreach (Process process in Process.GetProcesses())
                {
                    ListBox_Process.Items.Add(new ProcessList()
                    {
                        PID = process.Id,
                        PName = process.ProcessName,
                        MWindowTitle = process.MainWindowTitle,
                        MWindowHandle = process.MainWindowHandle,
                    });
                }
            }
        }

        private void TextBox_DLLPath_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var openFileDialog = new OpenFileDialog { Filter = "dll files |*.dll", RestoreDirectory = true };
            if (openFileDialog.ShowDialog() == true)
            {
                TextBox_DLLPath.Text = openFileDialog.FileName;
                InjectInfo.DLLPath = openFileDialog.FileName;
            }
            else
            {
                TextBox_DLLPath.Text = "请选择DLL文件位置";
                InjectInfo.DLLPath = string.Empty;
            }
        }

        private void ListBox_Process_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var temp = ListBox_Process.SelectedItem as ProcessList;

            if (temp != null)
            {
                InjectInfo.PID = temp.PID;
                InjectInfo.PName = temp.PName;
                InjectInfo.MWindowHandle = temp.MWindowHandle;
            }
        }
    }

    public class ProcessList
    {
        public int PID { get; set; }
        public string PName { get; set; }
        public string MWindowTitle { get; set; }
        public IntPtr MWindowHandle { get; set; }
    }

    public class InjectInfo
    {
        public int PID { get; set; }
        public string DLLPath { get; set; }
        public string PName { get; set; }
        public IntPtr MWindowHandle { get; set; }
    }
}
