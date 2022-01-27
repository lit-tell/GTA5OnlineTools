using System.Windows;
using Prism.Commands;
using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Modules.Windows;

namespace GTA5OnlineTools.ViewModels
{
    public class UC2ModulesViewModel
    {
        private ExternalMenuWindow ExternalMenuWindow = null;
        private SpawnVehicleWindow SpawnVehicleWindow = null;
        private GTAHaxWindow GTAHaxWindow = null;
        private OutfitsWindow OutfitsWindow = null;
        private HeistCutWindow HeistCutWindow = null;
        private NameChangeWindow NameChangeWindow = null;
        private GTAOverlayWindow GTAOverlayWindow = null;
        private MoneyRPWindow MoneyRPWindow = null;
        private StatAutoScriptsWindow StatAutoScriptsWindow = null;
        private CustomTPWindow CustomTPWindow = null;
        private SendTextWindow SendTextWindow = null;
        private HeistPrepsWindow HeistPrepsWindow = null;
        private BigBaseV2Window BigBaseV2Window = null;

        public DelegateCommand ExternalMenuClickCommand { get; private set; }
        public DelegateCommand SpawnVehicleClickCommand { get; private set; }
        public DelegateCommand GTAHaxClickCommand { get; private set; }
        public DelegateCommand OutfitsClickCommand { get; private set; }
        public DelegateCommand HeistCutClickCommand { get; private set; }
        public DelegateCommand NameChangeClickCommand { get; private set; }
        public DelegateCommand GTAOverlayClickCommand { get; private set; }
        public DelegateCommand MoneyRPClickCommand { get; private set; }
        public DelegateCommand StatAutoScriptsClickCommand { get; private set; }
        public DelegateCommand CustomTPClickCommand { get; private set; }
        public DelegateCommand SendTextClickCommand { get; private set; }
        public DelegateCommand HeistPrepsClickCommand { get; private set; }
        public DelegateCommand BigBaseV2ClickCommand { get; private set; }

        public UC2ModulesViewModel()
        {
            ExternalMenuClickCommand = new DelegateCommand(ExternalMenuClick);
            SpawnVehicleClickCommand = new DelegateCommand(SpawnVehicleClick);
            GTAHaxClickCommand = new DelegateCommand(GTAHaxClick);
            OutfitsClickCommand = new DelegateCommand(OutfitsClick);
            HeistCutClickCommand = new DelegateCommand(HeistCutClick);
            NameChangeClickCommand = new DelegateCommand(NameChangeClick);
            GTAOverlayClickCommand = new DelegateCommand(GTAOverlayClick);
            MoneyRPClickCommand = new DelegateCommand(MoneyRPClick);
            StatAutoScriptsClickCommand = new DelegateCommand(StatAutoScriptsClick);
            CustomTPClickCommand = new DelegateCommand(CustomTPClick);
            SendTextClickCommand = new DelegateCommand(SendTextClick);
            HeistPrepsClickCommand = new DelegateCommand(HeistPrepsClick);
            BigBaseV2ClickCommand = new DelegateCommand(BigBaseV2Click);
        }

        private void ExternalMenuClick()
        {
            AudioUtil.ClickSound();

            if (ProcessUtil.IsAppRun(CoreUtil.TargetAppName))
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (ExternalMenuWindow == null)
                    {
                        ExternalMenuWindow = new ExternalMenuWindow();
                        ExternalMenuWindow.Show();
                    }
                    else
                    {
                        if (ExternalMenuWindow.IsVisible)
                        {
                            ExternalMenuWindow.Topmost = true;
                            ExternalMenuWindow.Topmost = false;
                            ExternalMenuWindow.WindowState = WindowState.Normal;
                        }
                        else
                        {
                            ExternalMenuWindow = null;
                            ExternalMenuWindow = new ExternalMenuWindow();
                            ExternalMenuWindow.Show();
                        }
                    }
                });
            }
            else
            {
                MessageBox.Show("未发现GTA5进程", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SpawnVehicleClick()
        {
            AudioUtil.ClickSound();

            if (ProcessUtil.IsAppRun(CoreUtil.TargetAppName))
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (SpawnVehicleWindow == null)
                    {
                        SpawnVehicleWindow = new SpawnVehicleWindow();
                        SpawnVehicleWindow.Show();
                    }
                    else
                    {
                        if (SpawnVehicleWindow.IsVisible)
                        {
                            SpawnVehicleWindow.Topmost = true;
                            SpawnVehicleWindow.Topmost = false;
                            SpawnVehicleWindow.WindowState = WindowState.Normal;
                        }
                        else
                        {
                            SpawnVehicleWindow = null;
                            SpawnVehicleWindow = new SpawnVehicleWindow();
                            SpawnVehicleWindow.Show();
                        }
                    }
                });
            }
            else
            {
                MessageBox.Show("未发现GTA5进程", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show("未发现GTA5进程", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SendTextClick()
        {
            AudioUtil.ClickSound();

            if (ProcessUtil.IsAppRun(CoreUtil.TargetAppName))
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (SendTextWindow == null)
                    {
                        SendTextWindow = new SendTextWindow();
                        SendTextWindow.Show();
                    }
                    else
                    {
                        if (SendTextWindow.IsVisible)
                        {
                            SendTextWindow.Topmost = true;
                            SendTextWindow.Topmost = false;
                            SendTextWindow.WindowState = WindowState.Normal;
                        }
                        else
                        {
                            SendTextWindow = null;
                            SendTextWindow = new SendTextWindow();
                            SendTextWindow.Show();
                        }
                    }
                });
            }
            else
            {
                MessageBox.Show("未发现GTA5进程", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CustomTPClick()
        {
            AudioUtil.ClickSound();

            if (ProcessUtil.IsAppRun(CoreUtil.TargetAppName))
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (CustomTPWindow == null)
                    {
                        CustomTPWindow = new CustomTPWindow();
                        CustomTPWindow.Show();
                    }
                    else
                    {
                        if (CustomTPWindow.IsVisible)
                        {
                            CustomTPWindow.Topmost = true;
                            CustomTPWindow.Topmost = false;
                            CustomTPWindow.WindowState = WindowState.Normal;
                        }
                        else
                        {
                            CustomTPWindow = null;
                            CustomTPWindow = new CustomTPWindow();
                            CustomTPWindow.Show();
                        }
                    }
                });
            }
            else
            {
                MessageBox.Show("未发现GTA5进程", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show("未发现GTA5进程", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MoneyRPClick()
        {
            AudioUtil.ClickSound();

            if (ProcessUtil.IsAppRun(CoreUtil.TargetAppName))
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (MoneyRPWindow == null)
                    {
                        MoneyRPWindow = new MoneyRPWindow();
                        MoneyRPWindow.Show();
                    }
                    else
                    {
                        if (MoneyRPWindow.IsVisible)
                        {
                            MoneyRPWindow.Topmost = true;
                            MoneyRPWindow.Topmost = false;
                            MoneyRPWindow.WindowState = WindowState.Normal;
                        }
                        else
                        {
                            MoneyRPWindow = null;
                            MoneyRPWindow = new MoneyRPWindow();
                            MoneyRPWindow.Show();
                        }
                    }
                });
            }
            else
            {
                MessageBox.Show("未发现GTA5进程", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NameChangeClick()
        {
            AudioUtil.ClickSound();

            if (ProcessUtil.IsAppRun(CoreUtil.TargetAppName))
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (NameChangeWindow == null)
                    {
                        NameChangeWindow = new NameChangeWindow();
                        NameChangeWindow.Show();
                    }
                    else
                    {
                        if (NameChangeWindow.IsVisible)
                        {
                            NameChangeWindow.Topmost = true;
                            NameChangeWindow.Topmost = false;
                            NameChangeWindow.WindowState = WindowState.Normal;
                        }
                        else
                        {
                            NameChangeWindow = null;
                            NameChangeWindow = new NameChangeWindow();
                            NameChangeWindow.Show();
                        }
                    }
                });
            }
            else
            {
                MessageBox.Show("未发现GTA5进程", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show("未发现GTA5进程", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show("未发现GTA5进程", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void GTAOverlayClick()
        {
            AudioUtil.ClickSound();

            if (ProcessUtil.IsAppRun(CoreUtil.TargetAppName))
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (GTAOverlayWindow == null)
                    {
                        GTAOverlayWindow = new GTAOverlayWindow();
                        GTAOverlayWindow.Show();
                    }
                    else
                    {
                        if (GTAOverlayWindow.IsVisible)
                        {
                            GTAOverlayWindow.Topmost = true;
                            GTAOverlayWindow.Topmost = false;
                            GTAOverlayWindow.WindowState = WindowState.Normal;
                        }
                        else
                        {
                            GTAOverlayWindow = null;
                            GTAOverlayWindow = new GTAOverlayWindow();
                            GTAOverlayWindow.Show();
                        }
                    }
                });
            }
            else
            {
                MessageBox.Show("未发现GTA5进程", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BigBaseV2Click()
        {
            AudioUtil.ClickSound();

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
    }
}
