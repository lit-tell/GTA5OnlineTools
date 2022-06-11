using GTA5OnlineTools.Views;
using GTA5OnlineTools.Models;
using GTA5OnlineTools.Modules.Kits;
using GTA5OnlineTools.Common.Data;
using GTA5OnlineTools.Common.Http;
using GTA5OnlineTools.Common.Utils;

using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;

using Hardcodet.Wpf.TaskbarNotification;

namespace GTA5OnlineTools;

/// <summary>
/// MainWindow.xaml 的交互逻辑
/// </summary>
public partial class MainWindow : Window
{
    // 任务栏图标
    private static TaskbarIcon TaskbarIcon_Main = null;
    // 主窗口数据模型
    public MainModel MainModel { get; set; } = new();

    // 页面导航命令
    public RelayCommand<string> NavigateCommand { get; private set; }

    // 用户控件，用于视图切换
    private UC0IndexView UC0IndexView { get; set; } = new();
    private UC1HacksView UC1HacksView { get; set; } = new();
    private UC2ModulesView UC2ModulesView { get; set; } = new();
    private UC3ToolsView UC3ToolsView { get; set; } = new();
    private UC4UpdateView UC4UpdateView { get; set; } = new();
    private UC5AboutView UC5AboutView { get; set; } = new();

    /// <summary>
    /// 用于向外暴露主窗口实例
    /// </summary>
    public static Window MainWindowIns = null;

    // 声明一个变量，用于存储软件开始运行的时间
    private DateTime Origin_DateTime;

    public MainWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 窗口加载事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Window_Main_Loaded(object sender, RoutedEventArgs e)
    {
        // 设置当前上下文数据
        this.DataContext = this;
        // 向外暴露主窗口实例
        MainWindowIns = this;

        // 初始化主数据模型
        NavigateCommand = new(Navigate);
        // 首页导航
        Navigate("UC0IndexView");

        // 获取当前时间，存储到对于变量中
        Origin_DateTime = DateTime.Now;

        //////////////////////////////////////////////////////////////////////////////

        // 设置主窗口标题
        MainModel.WindowTitle = CoreUtil.MainAppWindowName + CoreUtil.ClientVersionInfo;

        MainModel.GTA5IsRun = "GTA5 : OFF";
        MainModel.AppRunTime = "运行时间 : Loading...";

        // 定时器线程
        var timer = new System.Timers.Timer();
        timer.Elapsed += new ElapsedEventHandler(Timer_Tick);
        timer.Interval = 1000;
        timer.AutoReset = true;
        timer.Start();
        // 初始化后台线程
        var thread0 = new Thread(InitThread);
        thread0.IsBackground = true;
        thread0.Start();

        //////////////////////////////////////////////////////////////////////////////

        #region 状态栏图标
        TaskbarIcon_Main = new TaskbarIcon();
        TaskbarIcon_Main.IconSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Favicon.ico", UriKind.RelativeOrAbsolute));
        TaskbarIcon_Main.MenuActivation = PopupActivationMode.RightClick;
        TaskbarIcon_Main.ToolTipText = "GTA5线上小助手";
        TaskbarIcon_Main.Visibility = Visibility.Visible;

        ContextMenu contextMenu = new ContextMenu();
        MenuItem MenuItem_Show = new MenuItem();
        MenuItem_Show.Header = "显示";
        MenuItem_Show.Click += TaskbarIcon_MenuItem_Show_Click;
        MenuItem MenuItem_Exit = new MenuItem();
        MenuItem_Exit.Header = "退出";
        MenuItem_Exit.Click += TaskbarIcon_MenuItem_Exit_Click;
        contextMenu.Items.Add(MenuItem_Show);
        contextMenu.Items.Add(MenuItem_Exit);
        TaskbarIcon_Main.ContextMenu = contextMenu;

        TaskbarIcon_Main.TrayMouseDoubleClick += TaskbarIcon_Main_TrayMouseDoubleClick;
        #endregion
    }

    /// <summary>
    /// 窗口关闭事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Window_Main_Closing(object sender, CancelEventArgs e)
    {
        // 关闭第三方进程
        ProcessUtil.CloseTheseProcess();
        // 释放状态栏图标
        TaskbarIcon_Main.IconSource = null;
        TaskbarIcon_Main.ContextMenu = null;
        TaskbarIcon_Main.Dispose();
        // 结束程序
        Application.Current.Shutdown();
    }

    #region 状态栏图标相关事件
    /// <summary>
    /// 托盘菜单显示点击事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TaskbarIcon_MenuItem_Show_Click(object sender, RoutedEventArgs e)
    {
        Topmost = true;
        Topmost = false;
        ShowInTaskbar = true;
        WindowState = WindowState.Normal;
    }

    /// <summary>
    /// 托盘菜单退出点击事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TaskbarIcon_MenuItem_Exit_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    /// <summary>
    /// 鼠标双击事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TaskbarIcon_Main_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
    {
        Topmost = true;
        Topmost = false;
        ShowInTaskbar = true;
        WindowState = WindowState.Normal;
    }

    /// <summary>
    /// 显示通知信息
    /// </summary>
    /// <param name="msg"></param>
    public static void ShowNoticeInfo(string msg)
    {
        TaskbarIcon_Main?.ShowBalloonTip("提示", msg, BalloonIcon.Info);
    }
    #endregion

    //////////////////////////////////////////////////////////////////////

    /// <summary>
    /// 页面导航服务
    /// </summary>
    /// <param name="viewName"></param>
    private void Navigate(string viewName)
    {
        if (viewName == null || string.IsNullOrEmpty(viewName))
            return;

        switch (viewName)
        {
            case "UC0IndexView":
                if (ContentControl_Main.Content != UC0IndexView)
                    ContentControl_Main.Content = UC0IndexView;
                break;
            case "UC1HacksView":
                if (ContentControl_Main.Content != UC1HacksView)
                    ContentControl_Main.Content = UC1HacksView;
                break;
            case "UC2ModulesView":
                if (ContentControl_Main.Content != UC2ModulesView)
                    ContentControl_Main.Content = UC2ModulesView;
                break;
            case "UC3ToolsView":
                if (ContentControl_Main.Content != UC3ToolsView)
                    ContentControl_Main.Content = UC3ToolsView;
                break;
            case "UC4UpdateView":
                if (ContentControl_Main.Content != UC4UpdateView)
                    ContentControl_Main.Content = UC4UpdateView;
                break;
            case "UC5AboutView":
                if (ContentControl_Main.Content != UC5AboutView)
                    ContentControl_Main.Content = UC5AboutView;
                break;
        }
    }
    /// <summary>
    /// 计时器独立线程
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Timer_Tick(object sender, ElapsedEventArgs e)
    {
        // 获取软件运行时间
        MainModel.AppRunTime = "运行时间 : " + CoreUtil.ExecDateDiff(Origin_DateTime, DateTime.Now);

        // 判断 GTA5 是否运行
        MainModel.GTA5IsRun = ProcessUtil.IsAppRun("GTA5") ? "GTA5 : ON" : "GTA5 : OFF";
    }

    /// <summary>
    /// 初始化线程
    /// </summary>
    private async void InitThread()
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
            FileUtil.ExtractResFile(FileUtil.Resource_Path + "SubVersion.exe", FileUtil.Kiddion_Path + "SubVersion.exe");
            // 释放前先判断，防止覆盖配置文件
            if (!File.Exists(FileUtil.Kiddion_Path + "config.json"))
                FileUtil.ExtractResFile(FileUtil.Resource_Path + "config.json", FileUtil.Kiddion_Path + "config.json");
            if (!File.Exists(FileUtil.Kiddion_Path + "teleports.json"))
                FileUtil.ExtractResFile(FileUtil.Resource_Path + "teleports.json", FileUtil.Kiddion_Path + "teleports.json");
            if (!File.Exists(FileUtil.Kiddion_Path + "vehicles.json"))
                FileUtil.ExtractResFile(FileUtil.Resource_Path + "vehicles.json", FileUtil.Kiddion_Path + "vehicles.json");
            if (!File.Exists(FileUtil.KiddionScripts_Path + "Readme.api"))
                FileUtil.ExtractResFile(FileUtil.Resource_Path + "scripts.Readme.api", FileUtil.KiddionScripts_Path + "Readme.api");
            if (!File.Exists(FileUtil.Kiddion_Path + "settings.ini"))
                FileUtil.ExtractResFile(FileUtil.Resource_Path + "settings.ini", FileUtil.Kiddion_Path + "settings.ini");

            FileUtil.ExtractResFile(FileUtil.Resource_Path + "scripts.pre_skip.lua", FileUtil.KiddionScripts_Path + "pre_skip.lua");
            //if (!File.Exists(FileUtil.KiddionScripts_Path + "scripts.sirius.lua.example"))
            //FileUtil.ExtractResFile(FileUtil.Resource_Path + "scripts.sirius.lua.example", FileUtil.KiddionScripts_Path + "sirius.lua.example");

            /*****************************************************************************************************/

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
        catch (Exception ex)
        {
            MsgBoxUtil.ExceptionMsgBox(ex);
        }

        try
        {
            // 刷新DNS缓存
            CoreUtil.CMD_Code("ipconfig /flushdns");
            // 检查更新
            string webConfig = await HttpHelper.HttpClientGET(CoreUtil.ConfigAddress);
            // 解析web返回的数据
            GlobalData.ServerData = JsonUtil.JsonDese<ServerData>(webConfig);
            // 获取对应数据
            CoreUtil.ServerVersionInfo = Version.Parse(GlobalData.ServerData.Version);
            CoreUtil.NoticeAddress = GlobalData.ServerData.Address.Notice;
            CoreUtil.ChangeAddress = GlobalData.ServerData.Address.Change;
            // 获取最新公告
            await HttpHelper.HttpClientGET(CoreUtil.NoticeAddress).ContinueWith((t) =>
            {
                if (t != null)
                    WeakReferenceMessenger.Default.Send(t.Result, "Notice");
                else
                    WeakReferenceMessenger.Default.Send("获取最新公告内容失败！", "Notice");
            });
            // 获取更新日志
            await HttpHelper.HttpClientGET(CoreUtil.ChangeAddress).ContinueWith((t) =>
            {
                if (t != null)
                    WeakReferenceMessenger.Default.Send(t.Result, "Change");
                else
                    WeakReferenceMessenger.Default.Send("获取更新日志信息失败！", "Change");
            });

            // 如果线上版本号大于本地版本号，则提示更新
            if (CoreUtil.ServerVersionInfo > CoreUtil.ClientVersionInfo)
            {
                AudioUtil.SP_GTA5_Email.Play();
                // 打开更新对话框
                OpenUpateWindow();
            }
        }
        catch (Exception ex)
        {
            MsgBoxUtil.ExceptionMsgBox(ex);
        }
    }

    /// <summary>
    /// 打开更新窗口
    /// </summary>
    private static void OpenUpateWindow()
    {
        if (MessageBox.Show($"检测到新版本已发布，是否立即前往更新？\n\n{GlobalData.ServerData.Latest.Date}\n{GlobalData.ServerData.Latest.Change}\n\n强烈建议大家使用最新版本呢！",
            "发现新版本", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var UpdateWindow = new UpdateWindow();
                // 设置父窗口
                UpdateWindow.Owner = MainWindowIns;
                // 以对话框形式显示更新窗口
                UpdateWindow.ShowDialog();
            });
        }
    }
}
