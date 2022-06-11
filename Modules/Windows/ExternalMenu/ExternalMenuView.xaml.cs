using GTA5OnlineTools.Common.Data;
using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Core;
using GTA5OnlineTools.Features.Data;

using Microsoft.Toolkit.Mvvm.Input;

namespace GTA5OnlineTools.Modules.Windows.ExternalMenu;

/// <summary>
/// ExternalMenuView.xaml 的交互逻辑
/// </summary>
public partial class ExternalMenuView : Window
{
    public List<MenuBar> MenuBars { get; set; }
    public RelayCommand<MenuBar> NavigateCommand { get; private set; }

    public delegate void ClosingDispose();
    public static event ClosingDispose ClosingDisposeEvent;

    // 程序自身的窗口句柄
    private IntPtr EMHandle;
    private POINT EMPOINT;

    public delegate void IsShowWindow();
    public static IsShowWindow IsShowWindowDelegate;

    public ExternalMenuView()
    {
        InitializeComponent();

        Button_TitleMin.Click += (s, e) => { this.WindowState = WindowState.Minimized; };
        Button_TitleClose.Click += (s, e) => { this.Close(); };
    }

    private void Window_ExternalMenuView_Loaded(object sender, RoutedEventArgs e)
    {
        MenuBars = new List<MenuBar>();

        CreateMenuBar();

        NavigateCommand = new(Navigate);

        // 获取自身窗口句柄
        EMHandle = new WindowInteropHelper(this).Handle;
        WinAPI.GetCursorPos(out EMPOINT);

        IsShowWindowDelegate = ShowWindow;

        Settings.ShowWindow = true;

        Topmost = false;

        Task.Run(() =>
        {
            Memory.Initialize(CoreUtil.TargetAppName);

            Globals.TempPTR = Memory.FindPattern(Offsets.Mask.WorldMask);
            Globals.WorldPTR = Memory.Rip_37(Globals.TempPTR);

            Globals.TempPTR = Memory.FindPattern(Offsets.Mask.BlipMask);
            Globals.BlipPTR = Memory.Rip_37(Globals.TempPTR);

            Globals.TempPTR = Memory.FindPattern(Offsets.Mask.GlobalMask);
            Globals.GlobalPTR = Memory.Rip_37(Globals.TempPTR);

            Globals.TempPTR = Memory.FindPattern(Offsets.Mask.PlayerchatterNameMask);
            Globals.PlayerChatterNamePTR = Memory.Rip_37(Globals.TempPTR);

            Globals.TempPTR = Memory.FindPattern(Offsets.Mask.NetworkPlayerMgrMask);
            Globals.NetworkPlayerMgrPTR = Memory.Rip_37(Globals.TempPTR);

            Globals.TempPTR = Memory.FindPattern(Offsets.Mask.ReplayInterfaceMask);
            Globals.ReplayInterfacePTR = Memory.Rip_37(Globals.TempPTR);

            Globals.TempPTR = Memory.FindPattern(Offsets.Mask.WeatherMask);
            Globals.WeatherPTR = Memory.Rip_6A(Globals.TempPTR);

            Globals.TempPTR = Memory.FindPattern(Offsets.Mask.UnkModelMask);
            Globals.UnkModelPTR = Memory.Rip_37(Globals.TempPTR);

            Globals.TempPTR = Memory.FindPattern(Offsets.Mask.PickupDataMask);
            Globals.PickupDataPTR = Memory.Rip_37(Globals.TempPTR);

            Globals.TempPTR = Memory.FindPattern(Offsets.Mask.ViewPortMask);
            Globals.ViewPortPTR = Memory.Rip_37(Globals.TempPTR);

            Globals.TempPTR = Memory.FindPattern(Offsets.Mask.AimingPedMask);
            Globals.AimingPedPTR = Memory.Rip_37(Globals.TempPTR);

            Globals.TempPTR = Memory.FindPattern(Offsets.Mask.CCameraMask);
            Globals.CCameraPTR = Memory.Rip_37(Globals.TempPTR);
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
        MenuBars.Add(new MenuBar() { Icon = "\xe882", Title = "使用说明", ColorHex = "#333333", NameSpace = "EM00ReadMeView" });
        MenuBars.Add(new MenuBar() { Icon = "\xe64b", Title = "玩家属性", ColorHex = "#333333", NameSpace = "EM01PlayerStateView" });
        MenuBars.Add(new MenuBar() { Icon = "\xed18", Title = "世界功能", ColorHex = "#333333", NameSpace = "EM02WorldFunctionView" });
        MenuBars.Add(new MenuBar() { Icon = "\xe882", Title = "线上选项", ColorHex = "#333333", NameSpace = "EM03OnlineOptionView" });
        MenuBars.Add(new MenuBar() { Icon = "\xe64b", Title = "玩家列表", ColorHex = "#333333", NameSpace = "EM04PlayerListView" });
        MenuBars.Add(new MenuBar() { Icon = "\xed18", Title = "线上载具", ColorHex = "#333333", NameSpace = "EM05SpawnVehicleView" });
        MenuBars.Add(new MenuBar() { Icon = "\xe882", Title = "线上武器", ColorHex = "#333333", NameSpace = "EM06SpawnWeaponView" });
        MenuBars.Add(new MenuBar() { Icon = "\xe64b", Title = "自定传送", ColorHex = "#333333", NameSpace = "EM07CustomTPView" });
        MenuBars.Add(new MenuBar() { Icon = "\xed18", Title = "外部ESP", ColorHex = "#333333", NameSpace = "EM08ExternalOverlayView" });
        MenuBars.Add(new MenuBar() { Icon = "\xe882", Title = "战局聊天", ColorHex = "#333333", NameSpace = "EM09SessionChatView" });
        MenuBars.Add(new MenuBar() { Icon = "\xe64b", Title = "任务帮手", ColorHex = "#333333", NameSpace = "EM10JobHelperView" });
    }

    private void Navigate(MenuBar obj)
    {
        if (obj == null || string.IsNullOrEmpty(obj.NameSpace))
            return;
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

    private void ShowWindow()
    {
        Settings.ShowWindow = !Settings.ShowWindow;
        if (Settings.ShowWindow)
        {
            //Show();
            WindowState = WindowState.Normal;
            Focus();

            if (CheckBox_IsTopMost.IsChecked == false)
            {
                Topmost = true;
                Topmost = false;
            }

            WinAPI.SetCursorPos(EMPOINT.X, EMPOINT.Y);

            WinAPI.SetForegroundWindow(EMHandle);
        }
        else
        {
            //Hide();
            WindowState = WindowState.Minimized;

            WinAPI.GetCursorPos(out EMPOINT);

            Memory.SetForegroundWindow();
        }
    }

    private void CheckBox_IsTopMost_Click(object sender, RoutedEventArgs e)
    {
        if (CheckBox_IsTopMost.IsChecked == true)
            Topmost = true;
        else
            Topmost = false;
    }
}
