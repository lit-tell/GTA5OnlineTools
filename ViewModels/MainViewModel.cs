using System;
using System.IO;
using System.Timers;
using System.Windows;
using System.Threading;
using System.Collections.Generic;
using Prism.Events;
using Prism.Regions;
using Prism.Commands;
using GTA5OnlineTools.Views;
using GTA5OnlineTools.Event;
using GTA5OnlineTools.Models;
using GTA5OnlineTools.Common.Http;
using GTA5OnlineTools.Common.Data;
using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Modules.Kits;

namespace GTA5OnlineTools.ViewModels
{
    public class MainViewModel
    {
        public MainModel MainModel { get; set; }
        public List<MenuBar> MenuBars { get; set; }

        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }

        private IRegionManager _RegionManager;
        private IEventAggregator _EventAggregator;

        // 声明一个变量，用于存储软件开始运行的时间
        private DateTime Origin_DateTime;

        public MainViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            MainModel = new MainModel();
            MenuBars = new List<MenuBar>();
            CreateMenuBar();
            _RegionManager = regionManager;
            _EventAggregator = eventAggregator;
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);

            _RegionManager.RegisterViewWithRegion("MainViewRegion", "UC0IndexView");
            _RegionManager.RegisterViewWithRegion("MainViewRegion", "UC4UpdateView");

            //////////////////////////////////////////////////////////////////////////////

            MainModel.WindowTitle = CoreUtil.MainAppWindowName + CoreUtil.ClientVersionInfo;

            // 获取当前时间，存储到对于变量中
            Origin_DateTime = DateTime.Now;

            MainModel.GTA5IsRun = "GTA5 : OFF";
            MainModel.AppRunTime = "运行时间 : Loading...";

            var timer = new System.Timers.Timer();
            timer.Elapsed += new ElapsedEventHandler(Timer_Tick);
            timer.Interval = 1000; ;
            timer.AutoReset = true;
            timer.Start();

            var thread = new Thread(InitThread);
            thread.IsBackground = true;
            thread.Start();
        }

        private void CreateMenuBar()
        {
            MenuBars.Add(new MenuBar() { Icon = "\xe734", Title = "软件公告", ColorHex = "#F45221", NameSpace = "UC0IndexView" });
            MenuBars.Add(new MenuBar() { Icon = "\xe630", Title = "第三方辅助", ColorHex = "#00B2F2", NameSpace = "UC1HacksView" });
            MenuBars.Add(new MenuBar() { Icon = "\xe609", Title = "小助手辅助", ColorHex = "#88C600", NameSpace = "UC2ModulesView" });
            MenuBars.Add(new MenuBar() { Icon = "\xe644", Title = "工具设置", ColorHex = "#673AB7", NameSpace = "UC3ToolsView" });
            MenuBars.Add(new MenuBar() { Icon = "\xe652", Title = "更新日志", ColorHex = "#FFC501", NameSpace = "UC4UpdateView" });
            MenuBars.Add(new MenuBar() { Icon = "\xe684", Title = "关于作者", ColorHex = "#66CCCC", NameSpace = "UC5AboutView" });
        }

        private void Navigate(MenuBar obj)
        {
            if (obj == null || string.IsNullOrEmpty(obj.NameSpace))
                return;

            _RegionManager.Regions["MainViewRegion"].RequestNavigate(obj.NameSpace);
        }

        private void Timer_Tick(object sender, ElapsedEventArgs e)
        {
            // 获取软件运行时间
            MainModel.AppRunTime = "运行时间 : " + CoreUtil.ExecDateDiff(Origin_DateTime, DateTime.Now);

            // 判断 GTA5 是否运行
            MainModel.GTA5IsRun = ProcessUtil.IsAppRun("GTA5") ? "GTA5 : ON" : "GTA5 : OFF";
        }

        private async void InitThread()
        {
            try
            {
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

                if (!File.Exists(FileUtil.Kiddion_Path + "config.json"))
                    FileUtil.ExtractResFile(FileUtil.Resource_Path + "config.json", FileUtil.Kiddion_Path + "config.json");
                if (!File.Exists(FileUtil.Kiddion_Path + "teleports.json"))
                    FileUtil.ExtractResFile(FileUtil.Resource_Path + "teleports.json", FileUtil.Kiddion_Path + "teleports.json");
                if (!File.Exists(FileUtil.Kiddion_Path + "vehicles.json"))
                    FileUtil.ExtractResFile(FileUtil.Resource_Path + "vehicles.json", FileUtil.Kiddion_Path + "vehicles.json");
                if (!File.Exists(FileUtil.KiddionScripts_Path + "Readme.api"))
                    FileUtil.ExtractResFile(FileUtil.Resource_Path + "scripts.Readme.api", FileUtil.KiddionScripts_Path + "Readme.api");
                if (!File.Exists(FileUtil.KiddionScripts_Path + "scripts.sirius.lua.example"))
                    FileUtil.ExtractResFile(FileUtil.Resource_Path + "scripts.sirius.lua.example", FileUtil.KiddionScripts_Path + "sirius.lua.example");
                if (!File.Exists(FileUtil.Kiddion_Path + "settings.ini"))
                    FileUtil.ExtractResFile(FileUtil.Resource_Path + "settings.ini", FileUtil.Kiddion_Path + "settings.ini");

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
            }
            catch (Exception ex)
            {
                MsgBoxUtil.ExceptionMsgBox(ex);
            }

            try
            {
                // 刷新DNS缓存
                CoreUtil.CMD_Code("ipconfig /flushdns");

                string webConfig = await HttpHelper.HttpClientGET(CoreUtil.ConfigAddress);

                GlobalData.ServerData = JsonUtil.JsonDese<ServerData>(webConfig);

                CoreUtil.ServerVersionInfo = Version.Parse(GlobalData.ServerData.Version);
                CoreUtil.NoticeAddress = GlobalData.ServerData.Address.Notice;
                CoreUtil.ChangeAddress = GlobalData.ServerData.Address.Change;

                await HttpHelper.HttpClientGET(CoreUtil.NoticeAddress).ContinueWith((t) =>
                {
                    this._EventAggregator.GetEvent<NoticeMsgEvent>().Publish(t.Result);
                });

                await HttpHelper.HttpClientGET(CoreUtil.ChangeAddress).ContinueWith((t) =>
                {
                    this._EventAggregator.GetEvent<ChangeMsgEvent>().Publish(t.Result);
                });
            }
            catch (Exception ex)
            {
                MsgBoxUtil.ExceptionMsgBox(ex);
            }

            if (CoreUtil.ServerVersionInfo > CoreUtil.ClientVersionInfo)
            {
                AudioUtil.SP_GTA5_Email.Play();
                OpenUpateWindow();
            }
        }

        private static void OpenUpateWindow()
        {
            if (MessageBox.Show($"检测到新版本已发布，是否立即前往更新？\n\n{GlobalData.ServerData.Latest.Date}\n{GlobalData.ServerData.Latest.Change}\n\n强烈建议大家使用最新版本呢！",
                "发现新版本", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var UpdateWindow = new UpdateWindow();
                    UpdateWindow.Owner = MainView.MainWindow;
                    UpdateWindow.ShowDialog();
                });
            }
        }
    }
}
