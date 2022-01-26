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
        public IRegionManager RegionManager { get; private set; }
        public IEventAggregator EventAggregator { get; private set; }

        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }

        // 声明一个变量，用于存储软件开始运行的时间
        private DateTime Origin_DateTime;

        public MainViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            MainModel = new MainModel();
            MenuBars = new List<MenuBar>();
            CreateMenuBar();
            RegionManager = regionManager;
            EventAggregator = eventAggregator;
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);

            regionManager.RegisterViewWithRegion("MainViewRegion", "UC0IndexView");
            regionManager.RegisterViewWithRegion("MainViewRegion", "UC4UpdateView");

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
            MenuBars.Add(new MenuBar() { Icon = "\xe734", Title = "软件公告", NameSpace = "UC0IndexView" });
            MenuBars.Add(new MenuBar() { Icon = "\xe630", Title = "第三方辅助", NameSpace = "UC1HacksView" });
            MenuBars.Add(new MenuBar() { Icon = "\xe609", Title = "小助手辅助", NameSpace = "UC2ModulesView" });
            MenuBars.Add(new MenuBar() { Icon = "\xe606", Title = "工具设置", NameSpace = "UC3ToolsView" });
            MenuBars.Add(new MenuBar() { Icon = "\xe652", Title = "更新日志", NameSpace = "UC4UpdateView" });
            MenuBars.Add(new MenuBar() { Icon = "\xe684", Title = "关于作者", NameSpace = "UC5AboutView" });
        }

        private void Navigate(MenuBar obj)
        {
            if (obj == null || string.IsNullOrEmpty(obj.NameSpace))
                return;

            RegionManager.Regions["MainViewRegion"].RequestNavigate(obj.NameSpace);
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
                Directory.CreateDirectory(FileUtil.Temp_Path);
                FileUtil.DelectDir(FileUtil.Temp_Path);

                // 创建指定文件夹，用于释放必要文件和更新软件（如果已存在则不会创建）
                Directory.CreateDirectory(FileUtil.Config_Path);
                Directory.CreateDirectory(FileUtil.WebCache_Path);
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

                FileUtil.ExtractResFile(FileUtil.Resource_Path + "GTAHax.exe", FileUtil.Temp_Path + "GTAHax.exe");
                FileUtil.ExtractResFile(FileUtil.Resource_Path + "stat.txt", FileUtil.Temp_Path + "stat.txt");
                FileUtil.ExtractResFile(FileUtil.Resource_Path + "BincoHax.exe", FileUtil.Temp_Path + "BincoHax.exe");
                FileUtil.ExtractResFile(FileUtil.Resource_Path + "LSCHax.exe", FileUtil.Temp_Path + "LSCHax.exe");

                FileUtil.ExtractResFile(FileUtil.Resource_Path + "Bread.dll", FileUtil.Temp_Path + "Bread.dll");
                FileUtil.ExtractResFile(FileUtil.Resource_Path + "Bread_Chs.dll", FileUtil.Temp_Path + "Bread_Chs.dll");
                FileUtil.ExtractResFile(FileUtil.Resource_Path + "PackedStatEditor.dll", FileUtil.Temp_Path + "PackedStatEditor.dll");

                FileUtil.ExtractResFile(FileUtil.Resource_Path + "DefenderControl.exe", FileUtil.Temp_Path + "DefenderControl.exe");
                FileUtil.ExtractResFile(FileUtil.Resource_Path + "DefenderControl.ini", FileUtil.Temp_Path + "DefenderControl.ini");
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
                    this.EventAggregator.GetEvent<NoticeMsgEvent>().Publish(t.Result);
                });

                await HttpHelper.HttpClientGET(CoreUtil.ChangeAddress).ContinueWith((t) =>
                {
                    this.EventAggregator.GetEvent<ChangeMsgEvent>().Publish(t.Result);
                });
            }
            catch (Exception ex)
            {
                MsgBoxUtil.ExceptionMsgBox(ex);
            }

            if (CoreUtil.ServerVersionInfo > CoreUtil.ClientVersionInfo)
            {
                OpenUpateWindow();
            }
        }

        private static void OpenUpateWindow()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (GlobalData.UpdateWindow == null)
                {
                    GlobalData.UpdateWindow = new UpdateWindow();
                    GlobalData.UpdateWindow.Owner = MainView.MainWindow;
                    GlobalData.UpdateWindow.ShowDialog();
                }
                else
                {
                    GlobalData.UpdateWindow.Topmost = true;
                    GlobalData.UpdateWindow.Topmost = false;
                    GlobalData.UpdateWindow.WindowState = WindowState.Normal;
                }
            });
        }
    }
}
