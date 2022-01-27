using System;
using System.IO;
using System.Windows;
using Prism.Commands;
using GTA5OnlineTools.Views;
using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Modules.Kits;

namespace GTA5OnlineTools.ViewModels
{
    public class UC3ToolsViewModel
    {
        private UpdateWindow UpdateWindow = null;
        private InjectorWindow InjectorWindow = null;

        public DelegateCommand<string> ToolsButtonClickCommand { get; private set; }
        public DelegateCommand<string> HyperlinkClickCommand { get; private set; }

        public DelegateCommand SwitchButtonAudioCommand { get; private set; }

        public UC3ToolsViewModel()
        {
            ToolsButtonClickCommand = new DelegateCommand<string>(ToolsButtonClick);
            HyperlinkClickCommand = new DelegateCommand<string>(HyperlinkClick);

            SwitchButtonAudioCommand = new DelegateCommand(SwitchButtonAudio);
        }

        private void SwitchButtonAudio()
        {
            if (AudioUtil.ClickSoundIndex < 5)
                AudioUtil.ClickSoundIndex++;
            else
                AudioUtil.ClickSoundIndex = 0;

            AudioUtil.ClickSound();
        }

        private void ToolsButtonClick(string obj)
        {
            AudioUtil.ClickSound();

            switch (obj)
            {
                case "RestartApp":
                    RestartAppClick();
                    break;
                case "InitCPDPath":
                    InitCPDPathClick();
                    break;
                case "KiddionChsON":
                    KiddionChsONClick();
                    break;
                case "KiddionChsOFF":
                    KiddionChsOFFClick();
                    break;
                case "KiddionKey87":
                    KiddionKey87Click();
                    break;
                case "KiddionKey104":
                    KiddionKey104Click();
                    break;
                case "SubVersionKey87":
                    SubVersionKey87Click();
                    break;
                case "SubVersionKey104":
                    SubVersionKey104Click();
                    break;
                case "ReleaseDirectory":
                    ReleaseDirectoryClick();
                    break;
                case "CurrentDirectory":
                    CurrentDirectoryClick();
                    break;
                case "EditKiddionConfig":
                    EditKiddionConfigClick();
                    break;
                case "EditSubVersionSettings":
                    EditSubVersionSettingsClick();
                    break;
                case "EditKiddionTP":
                    EditKiddionTPClick();
                    break;
                case "EditKiddionVC":
                    EditKiddionVCClick();
                    break;
                case "EditGTAHaxStat":
                    EditGTAHaxStatClick();
                    break;
                case "ReNameAppCN":
                    ReNameAppCNClick();
                    break;
                case "ReNameAppEN":
                    ReNameAppENClick();
                    break;
                case "RefreshDNSCache":
                    RefreshDNSCacheClick();
                    break;
                case "EditHosts":
                    EditHostsClick();
                    break;
                case "OpenUpdateWindow":
                    OpenUpdateWindowClick();
                    break;
                case "GTA5Win2TopMost":
                    GTA5Win2TopMostClick();
                    break;
                case "GTA5Win2NoTopMost":
                    GTA5Win2NoTopMostClick();
                    break;
                case "DefenderControl":
                    DefenderControlClick();
                    break;
                case "BaseInjector":
                    BaseInjectorClick();
                    break;
                case "MinimizeToTray":
                    MinimizeToTrayClick();
                    break;
            }
        }

        private void HyperlinkClick(string url)
        {
            ProcessUtil.OpenLink(url);
        }

        private void BaseInjectorClick()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (InjectorWindow == null)
                {
                    InjectorWindow = new InjectorWindow();
                    InjectorWindow.Show();
                }
                else
                {
                    if (InjectorWindow.IsVisible)
                    {
                        InjectorWindow.Topmost = true;
                        InjectorWindow.Topmost = false;
                        InjectorWindow.WindowState = WindowState.Normal;
                    }
                    else
                    {
                        InjectorWindow = null;
                        InjectorWindow = new InjectorWindow();
                        InjectorWindow.Show();
                    }
                }
            });
        }

        private void DefenderControlClick()
        {
            ProcessUtil.OpenProcess("DefenderControl", false);
        }

        private void GTA5Win2NoTopMostClick()
        {
            ProcessUtil.TopMostProcess(CoreUtil.TargetAppName, false);
        }

        private void GTA5Win2TopMostClick()
        {
            ProcessUtil.TopMostProcess(CoreUtil.TargetAppName, true);
        }

        private void OpenUpdateWindowClick()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (UpdateWindow == null)
                {
                    UpdateWindow = new UpdateWindow();
                    UpdateWindow.Show();
                }
                else
                {
                    if (UpdateWindow.IsVisible)
                    {
                        UpdateWindow.Topmost = true;
                        UpdateWindow.Topmost = false;
                        UpdateWindow.WindowState = WindowState.Normal;
                    }
                    else
                    {
                        UpdateWindow = null;
                        UpdateWindow = new UpdateWindow();
                        UpdateWindow.Show();
                    }
                }
            });
        }

        private void EditHostsClick()
        {
            ProcessUtil.OpenLink("notepad.exe", @"C:\windows\system32\drivers\etc\hosts");
        }

        private void RefreshDNSCacheClick()
        {
            CoreUtil.CMD_Code("ipconfig /flushdns");
            MainView.ShowNoticeInfo("成功刷新DNS解析程序缓存");
        }

        private void ReNameAppENClick()
        {
            try
            {
                // 重命名小助手文件
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(FileUtil.Current_Path);
                if (fileNameWithoutExtension != "GTA5OnlineTools")
                {
                    FileUtil.FileReName(FileUtil.Current_Path, "GTA5OnlineTools.exe");

                    ProcessUtil.CloseTheseProcess();
                    App.AppMainMutex.Dispose();
                    ProcessUtil.OpenLink(FileUtil.GetCurrFullPath("GTA5OnlineTools.exe"));
                    Application.Current.Shutdown();

                }
                else
                {
                    MsgBoxUtil.InformationMsgBox("程序文件名已经符合英文命名标准，无需继续重命名");
                }
            }
            catch (Exception ex)
            {
                MsgBoxUtil.ExceptionMsgBox(ex);
            }
        }

        private void ReNameAppCNClick()
        {
            try
            {
                // 重命名小助手文件
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(FileUtil.Current_Path);
                if (fileNameWithoutExtension != (CoreUtil.MainAppWindowName + CoreUtil.ClientVersionInfo))
                {
                    FileUtil.FileReName(FileUtil.Current_Path, FileUtil.GetCurrFullPath(CoreUtil.MainAppWindowName + CoreUtil.ClientVersionInfo + ".exe"));

                    ProcessUtil.CloseTheseProcess();
                    App.AppMainMutex.Dispose();
                    ProcessUtil.OpenLink(FileUtil.GetCurrFullPath(CoreUtil.MainAppWindowName + CoreUtil.ClientVersionInfo + ".exe"));
                    Application.Current.Shutdown();
                }
                else
                {
                    MsgBoxUtil.InformationMsgBox("程序文件名已经符合中文命名标准，无需继续重命名");
                }
            }
            catch (Exception ex)
            {
                MsgBoxUtil.ExceptionMsgBox(ex);
            }
        }

        private void EditGTAHaxStatClick()
        {
            ProcessUtil.OpenLink("notepad.exe", FileUtil.GTAHaxStat_Path);
        }

        private void EditKiddionVCClick()
        {
            ProcessUtil.OpenLink("notepad.exe", FileUtil.Kiddion_Path + @"vehicles.json");
        }

        private void EditKiddionTPClick()
        {
            ProcessUtil.OpenLink("notepad.exe", FileUtil.Kiddion_Path + @"teleports.json");
        }

        private void EditSubVersionSettingsClick()
        {
            ProcessUtil.OpenLink("notepad.exe", FileUtil.Kiddion_Path + @"settings.ini");
        }

        private void EditKiddionConfigClick()
        {
            ProcessUtil.OpenLink("notepad.exe", FileUtil.Kiddion_Path + @"config.json");
        }

        private void CurrentDirectoryClick()
        {
            ProcessUtil.OpenLink(FileUtil.CurrentDirectory_Path);
        }

        private void ReleaseDirectoryClick()
        {
            ProcessUtil.OpenLink(FileUtil.Default_Path);
        }

        private void SubVersionKey87Click()
        {
            ProcessUtil.CloseProcess("SubVersion");
            FileUtil.ExtractResFile(FileUtil.Resource_Path + "Key87.settings.ini", FileUtil.Kiddion_Path + @"settings.ini");
        }

        private void SubVersionKey104Click()
        {
            ProcessUtil.CloseProcess("SubVersion");
            FileUtil.ExtractResFile(FileUtil.Resource_Path + "settings.ini", FileUtil.Kiddion_Path + @"settings.ini");
        }

        private void KiddionKey87Click()
        {
            ProcessUtil.CloseProcess("Kiddion");
            ProcessUtil.CloseProcess("Kiddion_Chs");
            FileUtil.ExtractResFile(FileUtil.Resource_Path + "Key87.config.json", FileUtil.Kiddion_Path + @"config.json");
        }

        private void KiddionKey104Click()
        {
            ProcessUtil.CloseProcess("Kiddion");
            ProcessUtil.CloseProcess("Kiddion_Chs");
            FileUtil.ExtractResFile(FileUtil.Resource_Path + "config.json", FileUtil.Kiddion_Path + @"config.json");
        }

        private void KiddionChsOFFClick()
        {
            ProcessUtil.CloseProcess("Kiddion_Chs");
        }

        private void KiddionChsONClick()
        {
            if (ProcessUtil.IsAppRun("Kiddion"))
            {
                ProcessUtil.CloseProcess("Kiddion_Chs");
                ProcessUtil.OpenProcess("Kiddion_Chs", true);
            }
            else
            {
                MsgBoxUtil.InformationMsgBox("请先启动 Kiddion 程序");
            }
        }

        private void RestartAppClick()
        {
            ProcessUtil.CloseTheseProcess();
            App.AppMainMutex.Dispose();
            ProcessUtil.OpenLink(FileUtil.Current_Path);
            Application.Current.Shutdown();
        }

        private void InitCPDPathClick()
        {
            try
            {
                if (MessageBox.Show("你确定要初始化配置文件吗？", "警告", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    ProcessUtil.CloseTheseProcess();

                    FileUtil.DelectDir(FileUtil.Default_Path);

                    App.AppMainMutex.Dispose();
                    ProcessUtil.OpenLink(FileUtil.Current_Path);
                    Application.Current.Shutdown();
                }
            }
            catch (Exception ex)
            {
                MsgBoxUtil.ExceptionMsgBox(ex);
            }
        }

        private void MinimizeToTrayClick()
        {
            MainView.MainWindow.WindowState = WindowState.Minimized;
            MainView.MainWindow.ShowInTaskbar = false;
            MainView.ShowNoticeInfo("程序已最小化到托盘");
        }
    }
}
