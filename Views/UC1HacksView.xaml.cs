using GTA5OnlineTools.Models;
using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Modules.Pages;
using GTA5OnlineTools.Features.Core;

using Microsoft.Toolkit.Mvvm.Input;

namespace GTA5OnlineTools.Views;

/// <summary>
/// UC1HacksView.xaml 的交互逻辑
/// </summary>
public partial class UC1HacksView : UserControl
{
    public UC1HacksModel UC1HacksModel { get; set; } = new();

    public RelayCommand KiddionClickCommand { get; private set; }
    public RelayCommand SubVersionClickCommand { get; private set; }
    public RelayCommand GTAHaxClickCommand { get; private set; }
    public RelayCommand BincoHaxClickCommand { get; private set; }
    public RelayCommand LSCHaxClickCommand { get; private set; }

    public RelayCommand<string> ReadMeClickCommand { get; private set; }
    public RelayCommand FrameHideClickCommand { get; private set; }

    private KiddionPage KiddionPage = new();
    private SubVersionPage SubVersionPage = new();
    private GTAHaxPage GTAHaxPage = new();
    private BincoHaxPage BincoHaxPage = new();
    private LSCHaxPage LSCHaxPage = new();

    public UC1HacksView()
    {
        InitializeComponent();
        this.DataContext = this;

        KiddionClickCommand = new(KiddionClick);
        SubVersionClickCommand = new(SubVersionClick);
        GTAHaxClickCommand = new(GTAHaxClick);
        BincoHaxClickCommand = new(BincoHaxClick);
        LSCHaxClickCommand = new(LSCHaxClick);

        ReadMeClickCommand = new(ReadMeClick);
        FrameHideClickCommand = new(FrameHideClick);

        var thread = new Thread(MainThread);
        thread.IsBackground = true;
        thread.Start();
    }

    /// <summary>
    /// 主线程
    /// </summary>
    private void MainThread()
    {
        while (true)
        {
            // 判断 Kiddion 是否运行
            UC1HacksModel.KiddionIsRun = ProcessUtil.IsAppRun("Kiddion");

            // 判断 SubVersion 是否运行
            UC1HacksModel.SubVersionIsRun = ProcessUtil.IsAppRun("SubVersion");

            // 判断 GTAHax 是否运行
            UC1HacksModel.GTAHaxIsRun = ProcessUtil.IsAppRun("GTAHax");

            // 判断 BincoHax 是否运行
            UC1HacksModel.BincoHaxIsRun = ProcessUtil.IsAppRun("BincoHax");

            // 判断 LSCHax 是否运行
            UC1HacksModel.LSCHaxIsRun = ProcessUtil.IsAppRun("LSCHax");

            // 判断 PedDropper 是否运行
            UC1HacksModel.PedDropperIsRun = ProcessUtil.IsAppRun("PedDropper");

            // 判断 JobMoney 是否运行
            UC1HacksModel.JobMoneyIsRun = ProcessUtil.IsAppRun("JobMoney");

            Thread.Sleep(1000);
        }
    }

    private void KiddionClick()
    {
        AudioUtil.ClickSound();

        Task.Run(() =>
        {
            if (UC1HacksModel.KiddionIsRun)
            {
                ProcessUtil.CloseProcess("Kiddion_Chs");

                if (!ProcessUtil.IsAppRun("Kiddion"))
                    ProcessUtil.OpenProcess("Kiddion", true);

                bool isRun = false;
                do
                {
                    if (ProcessUtil.IsAppRun("Kiddion"))
                    {
                        isRun = true;

                        bool isShow = false;
                        do
                        {
                            var pKiddion = Process.GetProcessesByName("Kiddion").ToList()[0];

                            IntPtr Menu_handle = pKiddion.MainWindowHandle;
                            IntPtr child_handle = WinAPI.FindWindowEx(Menu_handle, IntPtr.Zero, "Static", null);
                            child_handle = WinAPI.FindWindowEx(Menu_handle, child_handle, "Static", null);

                            int length = WinAPI.GetWindowTextLength(child_handle);
                            StringBuilder windowName = new StringBuilder(length + 1);
                            WinAPI.GetWindowText(child_handle, windowName, windowName.Capacity);

                            if (windowName.ToString() == "Kiddion's Modest Menu v0.9.3")
                            {
                                isShow = true;
                                ProcessUtil.OpenProcess("Kiddion_Chs", true);
                            }
                            else
                            {
                                isShow = false;
                            }

                            Task.Delay(100).Wait();
                        } while (!isShow);
                    }
                    else
                    {
                        isRun = false;
                    }

                    Task.Delay(100).Wait();
                } while (!isRun);
            }
            else
            {
                ProcessUtil.CloseProcess("Kiddion");
                ProcessUtil.CloseProcess("Kiddion_Chs");
            }
        });
    }

    private void SubVersionClick()
    {
        AudioUtil.ClickSound();

        if (UC1HacksModel.SubVersionIsRun)
        {
            if (!ProcessUtil.IsAppRun("SubVersion"))
                ProcessUtil.OpenProcess("SubVersion", true);
        }
        else
        {
            ProcessUtil.CloseProcess("SubVersion");
        }
    }

    private void GTAHaxClick()
    {
        AudioUtil.ClickSound();

        if (UC1HacksModel.GTAHaxIsRun)
        {
            if (!ProcessUtil.IsAppRun("GTAHax"))
                ProcessUtil.OpenProcess("GTAHax", false);
        }
        else
        {
            ProcessUtil.CloseProcess("GTAHax");
        }
    }

    private void BincoHaxClick()
    {
        AudioUtil.ClickSound();

        if (UC1HacksModel.BincoHaxIsRun)
        {
            if (!ProcessUtil.IsAppRun("BincoHax"))
                ProcessUtil.OpenProcess("BincoHax", false);
        }
        else
        {
            ProcessUtil.CloseProcess("BincoHax");
        }
    }

    private void LSCHaxClick()
    {
        AudioUtil.ClickSound();

        if (UC1HacksModel.LSCHaxIsRun)
        {
            if (!ProcessUtil.IsAppRun("LSCHax"))
                ProcessUtil.OpenProcess("LSCHax", false);
        }
        else
        {
            ProcessUtil.CloseProcess("LSCHax");
        }
    }

    private void ReadMeClick(string pageName)
    {
        switch (pageName)
        {
            case "KiddionPage":
                UC1HacksModel.FrameVisibilityState = Visibility.Visible;
                UC1HacksModel.FrameContent = KiddionPage;
                break;
            case "SubVersionPage":
                UC1HacksModel.FrameVisibilityState = Visibility.Visible;
                UC1HacksModel.FrameContent = SubVersionPage;
                break;
            case "GTAHaxPage":
                UC1HacksModel.FrameVisibilityState = Visibility.Visible;
                UC1HacksModel.FrameContent = GTAHaxPage;
                break;
            case "BincoHaxPage":
                UC1HacksModel.FrameVisibilityState = Visibility.Visible;
                UC1HacksModel.FrameContent = BincoHaxPage;
                break;
            case "LSCHaxPage":
                UC1HacksModel.FrameVisibilityState = Visibility.Visible;
                UC1HacksModel.FrameContent = LSCHaxPage;
                break;
        }
    }

    private void FrameHideClick()
    {
        UC1HacksModel.FrameVisibilityState = Visibility.Collapsed;
        UC1HacksModel.FrameContent = null;
    }
}