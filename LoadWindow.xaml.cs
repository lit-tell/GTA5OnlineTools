using GTA5OnlineTools.Common.Utils;

namespace GTA5OnlineTools;

/// <summary>
/// LoadWindow.xaml 的交互逻辑
/// </summary>
public partial class LoadWindow : Window
{
    public LoadWindow()
    {
        InitializeComponent();
    }

    private void Window_Load_Loaded(object sender, RoutedEventArgs e)
    {
        Task.Run(() =>
        {
            try
            {
                // 创建、清空缓存文件夹
                Directory.CreateDirectory(FileUtil.Cache_Path);
                FileUtil.DelectDir(FileUtil.Cache_Path);

                // 创建指定文件夹，用于释放必要文件和更新软件（如果已存在则不会创建）
                Directory.CreateDirectory(FileUtil.Config_Path);
                Directory.CreateDirectory(FileUtil.Kiddion_Path);
                Directory.CreateDirectory(FileUtil.KiddionScripts_Path);

                // 释放必要文件
                FileUtil.ExtractResFile(FileUtil.Resource_Path + "Kiddion.exe", FileUtil.Kiddion_Path + "Kiddion.exe");
                FileUtil.ExtractResFile(FileUtil.Resource_Path + "Kiddion_Chs.exe", FileUtil.Kiddion_Path + "Kiddion_Chs.exe");

                // 释放前先判断，防止覆盖配置文件
                if (!File.Exists(FileUtil.Kiddion_Path + "config.json"))
                    FileUtil.ExtractResFile(FileUtil.Resource_Path + "config.json", FileUtil.Kiddion_Path + "config.json");
                if (!File.Exists(FileUtil.Kiddion_Path + "teleports.json"))
                    FileUtil.ExtractResFile(FileUtil.Resource_Path + "teleports.json", FileUtil.Kiddion_Path + "teleports.json");
                if (!File.Exists(FileUtil.Kiddion_Path + "vehicles.json"))
                    FileUtil.ExtractResFile(FileUtil.Resource_Path + "vehicles.json", FileUtil.Kiddion_Path + "vehicles.json");

                // Kiddion Lua脚本
                FileUtil.ExtractResFile(FileUtil.Resource_Path + "scripts.Readme.api", FileUtil.KiddionScripts_Path + "Readme.api");

                ///////////////////////////////////////////////////////////////////////////////////////////////////////

                FileUtil.ExtractResFile(FileUtil.Resource_Path + "GTAHax.exe", FileUtil.Cache_Path + "GTAHax.exe");
                FileUtil.ExtractResFile(FileUtil.Resource_Path + "stat.txt", FileUtil.Cache_Path + "stat.txt");
                FileUtil.ExtractResFile(FileUtil.Resource_Path + "BincoHax.exe", FileUtil.Cache_Path + "BincoHax.exe");
                FileUtil.ExtractResFile(FileUtil.Resource_Path + "LSCHax.exe", FileUtil.Cache_Path + "LSCHax.exe");

                FileUtil.ExtractResFile(FileUtil.Resource_Path + "Bread.dll", FileUtil.Cache_Path + "Bread.dll");
                FileUtil.ExtractResFile(FileUtil.Resource_Path + "Bread_Chs.dll", FileUtil.Cache_Path + "Bread_Chs.dll");
                FileUtil.ExtractResFile(FileUtil.Resource_Path + "PackedStatEditor.dll", FileUtil.Cache_Path + "PackedStatEditor.dll");

                FileUtil.ExtractResFile(FileUtil.Resource_Path + "DefenderControl.exe", FileUtil.Cache_Path + "DefenderControl.exe");
                FileUtil.ExtractResFile(FileUtil.Resource_Path + "DefenderControl.ini", FileUtil.Cache_Path + "DefenderControl.ini");

                FileUtil.ExtractResFile(FileUtil.Resource_Path + "MyInjectMenu.dll", FileUtil.Cache_Path + "MyInjectMenu.dll");
            }
            catch (Exception) { }

            // 防止加载窗口一闪而过
            Task.Delay(500).Wait();

            this.Dispatcher.Invoke(() =>
            {
                var mainWindow = new MainWindow();
                // 转移主程序控制权
                Application.Current.MainWindow = mainWindow;
                // 关闭初始化窗口
                this.Close();
                mainWindow.Show();
            });
        });
    }
}
