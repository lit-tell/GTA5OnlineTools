using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Modules.Windows;
using GTA5OnlineTools.Modules.Windows.ExternalMenu;

using Microsoft.Toolkit.Mvvm.Input;

namespace GTA5OnlineTools.Views;

/// <summary>
/// UC2ModulesView.xaml 的交互逻辑
/// </summary>
public partial class UC2ModulesView : UserControl
{
    private ExternalMenuView ExternalMenuView = null;

    private GTAHaxWindow GTAHaxWindow = null;
    private OutfitsWindow OutfitsWindow = null;
    private HeistCutWindow HeistCutWindow = null;
    private StatAutoScriptsWindow StatAutoScriptsWindow = null;
    private HeistPrepsWindow HeistPrepsWindow = null;
    private BigBaseV2Window BigBaseV2Window = null;
    private CasinoHackWindow CasinoHackWindow = null;

    public RelayCommand ExternalMenuClickCommand { get; private set; }
    public RelayCommand GTAHaxClickCommand { get; private set; }
    public RelayCommand OutfitsClickCommand { get; private set; }
    public RelayCommand HeistCutClickCommand { get; private set; }
    public RelayCommand StatAutoScriptsClickCommand { get; private set; }
    public RelayCommand HeistPrepsClickCommand { get; private set; }
    public RelayCommand BigBaseV2ClickCommand { get; private set; }
    public RelayCommand CasinoHackClickCommand { get; private set; }

    private const string HintMsg = "未发现GTA5进程，请先运行GTA5游戏";

    public UC2ModulesView()
    {
        InitializeComponent();

        this.DataContext = this;

        ExternalMenuClickCommand = new(ExternalMenuClick);
        GTAHaxClickCommand = new(GTAHaxClick);
        OutfitsClickCommand = new(OutfitsClick);
        HeistCutClickCommand = new(HeistCutClick);
        StatAutoScriptsClickCommand = new(StatAutoScriptsClick);
        HeistPrepsClickCommand = new(HeistPrepsClick);
        BigBaseV2ClickCommand = new(BigBaseV2Click);
        CasinoHackClickCommand = new(CasinoHackClick);
    }

    private void ExternalMenuClick()
    {
        AudioUtil.ClickSound();

        if (ProcessUtil.IsAppRun(CoreUtil.TargetAppName))
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (ExternalMenuView == null)
                {
                    ExternalMenuView = new ExternalMenuView();
                    ExternalMenuView.Show();
                }
                else
                {
                    if (ExternalMenuView.IsVisible)
                    {
                        if (!ExternalMenuView.Topmost)
                        {
                            ExternalMenuView.Topmost = true;
                            ExternalMenuView.Topmost = false;
                        }

                        ExternalMenuView.WindowState = WindowState.Normal;
                    }
                    else
                    {
                        ExternalMenuView = null;
                        ExternalMenuView = new ExternalMenuView();
                        ExternalMenuView.Show();
                    }
                }
            });
        }
        else
        {
            MsgBoxUtil.ErrorMsgBox(HintMsg);
        }
    }
    private void HeistPrepsClick()
    {
        AudioUtil.ClickSound();

        if (ProcessUtil.IsAppRun(CoreUtil.TargetAppName))
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (HeistPrepsWindow == null)
                {
                    HeistPrepsWindow = new HeistPrepsWindow();
                    HeistPrepsWindow.Show();
                }
                else
                {
                    if (HeistPrepsWindow.IsVisible)
                    {
                        HeistPrepsWindow.Topmost = true;
                        HeistPrepsWindow.Topmost = false;
                        HeistPrepsWindow.WindowState = WindowState.Normal;
                    }
                    else
                    {
                        HeistPrepsWindow = null;
                        HeistPrepsWindow = new HeistPrepsWindow();
                        HeistPrepsWindow.Show();
                    }
                }
            });
        }
        else
        {
            MsgBoxUtil.ErrorMsgBox(HintMsg);
        }
    }
    private void StatAutoScriptsClick()
    {
        AudioUtil.ClickSound();

        if (ProcessUtil.IsAppRun(CoreUtil.TargetAppName))
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (StatAutoScriptsWindow == null)
                {
                    StatAutoScriptsWindow = new StatAutoScriptsWindow();
                    StatAutoScriptsWindow.Show();
                }
                else
                {
                    if (StatAutoScriptsWindow.IsVisible)
                    {
                        StatAutoScriptsWindow.Topmost = true;
                        StatAutoScriptsWindow.Topmost = false;
                        StatAutoScriptsWindow.WindowState = WindowState.Normal;
                    }
                    else
                    {
                        StatAutoScriptsWindow = null;
                        StatAutoScriptsWindow = new StatAutoScriptsWindow();
                        StatAutoScriptsWindow.Show();
                    }
                }
            });
        }
        else
        {
            MsgBoxUtil.ErrorMsgBox(HintMsg);
        }
    }
    private void HeistCutClick()
    {
        AudioUtil.ClickSound();

        if (ProcessUtil.IsAppRun(CoreUtil.TargetAppName))
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (HeistCutWindow == null)
                {
                    HeistCutWindow = new HeistCutWindow();
                    HeistCutWindow.Show();
                }
                else
                {
                    if (HeistCutWindow.IsVisible)
                    {
                        HeistCutWindow.Topmost = true;
                        HeistCutWindow.Topmost = false;
                        HeistCutWindow.WindowState = WindowState.Normal;
                    }
                    else
                    {
                        HeistCutWindow = null;
                        HeistCutWindow = new HeistCutWindow();
                        HeistCutWindow.Show();
                    }
                }
            });
        }
        else
        {
            MsgBoxUtil.ErrorMsgBox(HintMsg);
        }
    }
    private void OutfitsClick()
    {
        AudioUtil.ClickSound();

        if (ProcessUtil.IsAppRun(CoreUtil.TargetAppName))
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (OutfitsWindow == null)
                {
                    OutfitsWindow = new OutfitsWindow();
                    OutfitsWindow.Show();
                }
                else
                {
                    if (OutfitsWindow.IsVisible)
                    {
                        OutfitsWindow.Topmost = true;
                        OutfitsWindow.Topmost = false;
                        OutfitsWindow.WindowState = WindowState.Normal;
                    }
                    else
                    {
                        OutfitsWindow = null;
                        OutfitsWindow = new OutfitsWindow();
                        OutfitsWindow.Show();
                    }
                }
            });
        }
        else
        {
            MsgBoxUtil.ErrorMsgBox(HintMsg);
        }
    }
    private void GTAHaxClick()
    {
        AudioUtil.ClickSound();

        Application.Current.Dispatcher.Invoke(() =>
        {
            if (GTAHaxWindow == null)
            {
                GTAHaxWindow = new GTAHaxWindow();
                GTAHaxWindow.Show();
            }
            else
            {
                if (GTAHaxWindow.IsVisible)
                {
                    GTAHaxWindow.Topmost = true;
                    GTAHaxWindow.Topmost = false;
                    GTAHaxWindow.WindowState = WindowState.Normal;
                }
                else
                {
                    GTAHaxWindow = null;
                    GTAHaxWindow = new GTAHaxWindow();
                    GTAHaxWindow.Show();
                }
            }
        });
    }
    private void BigBaseV2Click()
    {
        AudioUtil.ClickSound();

        if (ProcessUtil.IsAppRun(CoreUtil.TargetAppName))
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (BigBaseV2Window == null)
                {
                    BigBaseV2Window = new BigBaseV2Window();
                    BigBaseV2Window.Show();
                }
                else
                {
                    if (BigBaseV2Window.IsVisible)
                    {
                        BigBaseV2Window.Topmost = true;
                        BigBaseV2Window.Topmost = false;
                        BigBaseV2Window.WindowState = WindowState.Normal;
                    }
                    else
                    {
                        BigBaseV2Window = null;
                        BigBaseV2Window = new BigBaseV2Window();
                        BigBaseV2Window.Show();
                    }
                }
            });
        }
        else
        {
            MsgBoxUtil.ErrorMsgBox(HintMsg);
        }
    }
    private void CasinoHackClick()
    {
        AudioUtil.ClickSound();

        if (ProcessUtil.IsAppRun(CoreUtil.TargetAppName))
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (CasinoHackWindow == null)
                {
                    CasinoHackWindow = new CasinoHackWindow();
                    CasinoHackWindow.Show();
                }
                else
                {
                    if (CasinoHackWindow.IsVisible)
                    {
                        CasinoHackWindow.Topmost = true;
                        CasinoHackWindow.Topmost = false;
                        CasinoHackWindow.WindowState = WindowState.Normal;
                    }
                    else
                    {
                        CasinoHackWindow = null;
                        CasinoHackWindow = new CasinoHackWindow();
                        CasinoHackWindow.Show();
                    }
                }
            });
        }
        else
        {
            MsgBoxUtil.ErrorMsgBox(HintMsg);
        }
    }
}
