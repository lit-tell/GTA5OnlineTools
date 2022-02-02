using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Modules.Kits
{
    /// <summary>
    /// InjectorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class InjectorWindow : Window
    {
        private InjectInfo InjectInfo { get; set; }

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
            AudioUtil.ClickSound();

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

                BaseInjector.DLLInjector(InjectInfo.PID, InjectInfo.DLLPath);
                TextBlock_Status.Text = $"DLL注入到进程 {InjectInfo.PName} 成功";
                BaseInjector.SetForegroundWindow(InjectInfo.MWindowHandle);
            }
            catch (Exception ex)
            {
                TextBlock_Status.Text = ex.Message;
            }
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
}
