using System.Windows;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.Generic;
using Prism.Regions;
using Prism.Commands;
using GTA5OnlineTools.Common.Data;
using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Core;
using System;

namespace GTA5OnlineTools.Modules.Windows.ExternalMenu
{
    /// <summary>
    /// ExternalMenuView.xaml 的交互逻辑
    /// </summary>
    public partial class ExternalMenuView : Window
    {
        public List<MenuBar> MenuBars { get; set; }
        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }

        public delegate void ClosingDispose();
        public static event ClosingDispose ClosingDisposeEvent;

        private IRegionManager _RegionManager;
        private IRegionManager _ScopedRegion;

        public ExternalMenuView(IRegionManager regionManager)
        {
            InitializeComponent();

            _RegionManager = regionManager;

            _ScopedRegion = _RegionManager.CreateRegionManager();
            RegionManager.SetRegionManager(this, _ScopedRegion);
        }

        private void Window_ExternalMenuView_Loaded(object sender, RoutedEventArgs e)
        {
            MenuBars = new List<MenuBar>();
            CreateMenuBar();
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);

            _ScopedRegion.RequestNavigate("ExternalMenuViewRegion", "EM0PlayerStateView");

            Task.Run(() =>
            {
                Memory.Initialize(CoreUtil.TargetAppName);

                Offsets.Mask.TempPTR = Memory.FindPattern(Offsets.Mask.WorldPTR);
                Globals.WorldPTR = Memory.Rip_37(Offsets.Mask.TempPTR);

                Offsets.Mask.TempPTR = Memory.FindPattern(Offsets.Mask.BlipPTR);
                Globals.BlipPTR = Memory.Rip_37(Offsets.Mask.TempPTR);

                Offsets.Mask.TempPTR = Memory.FindPattern(Offsets.Mask.GlobalPTR);
                Globals.GlobalPTR = Memory.Rip_37(Offsets.Mask.TempPTR);

                Offsets.Mask.TempPTR = Memory.FindPattern(Offsets.Mask.PlayerNameChatPTR);
                Globals.PlayerNameChatterPTR = Memory.Rip_37(Offsets.Mask.TempPTR);

                Offsets.Mask.TempPTR = Memory.FindPattern(Offsets.Mask.PlayerListPTR);
                Globals.PlayerListPTR = Memory.Rip_37(Offsets.Mask.TempPTR);

                Offsets.Mask.TempPTR = Memory.FindPattern(Offsets.Mask.ReplayInterfacePTR);
                Globals.ReplayInterfacePTR = Memory.Rip_37(Offsets.Mask.TempPTR);

                Offsets.Mask.TempPTR = Memory.FindPattern(Offsets.Mask.WeatherPTR);
                Globals.WeatherPTR = Memory.Rip_6A(Offsets.Mask.TempPTR);

                Offsets.Mask.TempPTR = Memory.FindPattern(Offsets.Mask.UnkModel);
                Globals.UnkModelPTR = Memory.Rip_37(Offsets.Mask.TempPTR);

                Offsets.Mask.TempPTR = Memory.FindPattern(Offsets.Mask.PickupData);
                Globals.PickupDataPTR = Memory.Rip_37(Offsets.Mask.TempPTR);

                Globals.FrameFlagsZeroWriteCallOffset = Memory.FindPattern(Offsets.Mask.FrameFlagsZeroWriteCall) + 0x07;
            });

            this.DataContext = this;

            var thread = new Thread(InitThread);
            thread.IsBackground = true;
            thread.Start();
        }

        private void Window_ExternalMenuView_Closing(object sender, CancelEventArgs e)
        {
            ClosingDisposeEvent();
        }

        private void CreateMenuBar()
        {
            MenuBars.Add(new MenuBar() { Icon = "\xe738", Title = "玩家属性", ColorHex = "#333333", NameSpace = "EM0PlayerStateView" });
            MenuBars.Add(new MenuBar() { Icon = "\xe738", Title = "世界功能", ColorHex = "#333333", NameSpace = "EM3WorldFunctionView" });
            MenuBars.Add(new MenuBar() { Icon = "\xe738", Title = "线上选项", ColorHex = "#333333", NameSpace = "EM4OnlineOptionView" });
            MenuBars.Add(new MenuBar() { Icon = "\xe738", Title = "玩家列表", ColorHex = "#333333", NameSpace = "EM5PlayerListView" });
            MenuBars.Add(new MenuBar() { Icon = "\xe738", Title = "刷出线上载具", ColorHex = "#333333", NameSpace = "EM1SpawnVehicleView" });
            MenuBars.Add(new MenuBar() { Icon = "\xe738", Title = "刷出线上武器", ColorHex = "#333333", NameSpace = "EM2SpawnWeaponView" });
            MenuBars.Add(new MenuBar() { Icon = "\xe738", Title = "自定义传送", ColorHex = "#333333", NameSpace = "EM6CustomTPView" });
            MenuBars.Add(new MenuBar() { Icon = "\xe738", Title = "外部ESP", ColorHex = "#333333", NameSpace = "EM7ExternalOverlayView" });
            MenuBars.Add(new MenuBar() { Icon = "\xe738", Title = "战局聊天", ColorHex = "#333333", NameSpace = "EM8SessionChatView" });
            MenuBars.Add(new MenuBar() { Icon = "\xe738", Title = "任务小帮手", ColorHex = "#333333", NameSpace = "EM9JobHelperView" });

        }

        private void Navigate(MenuBar obj)
        {
            if (obj == null || string.IsNullOrEmpty(obj.NameSpace))
                return;

            _ScopedRegion.RequestNavigate("ExternalMenuViewRegion", obj.NameSpace);
        }

        private void InitThread()
        {
            while (true)
            {
                if (!ProcessUtil.IsAppRun("GTA5"))
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.Close();
                    });
                    return;
                }

                Thread.Sleep(2000);
            }
        }
    }
}
