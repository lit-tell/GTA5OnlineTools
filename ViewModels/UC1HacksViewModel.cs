using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading;
using System.Threading.Tasks;
using Prism.Commands;
using GTA5OnlineTools.Models;
using GTA5OnlineTools.Common.Utils;
using System.Diagnostics;
using GTA5OnlineTools.Features.Core;
using GTA5OnlineTools.Modules.Pages;

namespace GTA5OnlineTools.ViewModels
{
    public class UC1HacksViewModel
    {
        public UC1HacksModel UC1HacksModel { get; set; }

        public DelegateCommand KiddionClickCommand { get; private set; }
        public DelegateCommand SubVersionClickCommand { get; private set; }
        public DelegateCommand GTAHaxClickCommand { get; private set; }
        public DelegateCommand BincoHaxClickCommand { get; private set; }
        public DelegateCommand LSCHaxClickCommand { get; private set; }

        public DelegateCommand<string> ReadMeClickCommand { get; private set; }
        public DelegateCommand FrameHideClickCommand { get; private set; }

        private static KiddionPage KiddionPage = new KiddionPage();
        private static SubVersionPage SubVersionPage = new SubVersionPage();
        private static GTAHaxPage GTAHaxPage = new GTAHaxPage();
        private static BincoHaxPage BincoHaxPage = new BincoHaxPage();
        private static LSCHaxPage LSCHaxPage = new LSCHaxPage();

        public UC1HacksViewModel()
        {
            UC1HacksModel = new UC1HacksModel();

            KiddionClickCommand = new DelegateCommand(KiddionClick);
            SubVersionClickCommand = new DelegateCommand(SubVersionClick);
            GTAHaxClickCommand = new DelegateCommand(GTAHaxClick);
            BincoHaxClickCommand = new DelegateCommand(BincoHaxClick);
            LSCHaxClickCommand = new DelegateCommand(LSCHaxClick);

            ReadMeClickCommand = new DelegateCommand<string>(ReadMeClick);
            FrameHideClickCommand = new DelegateCommand(FrameHideClick);

            var thread = new Thread(MainThread);
            thread.IsBackground = true;
            thread.Start();
        }

        private void MainThread()
        {
            while (true)
            {
                // 判断 Kiddion 是否运行
                UC1HacksModel.KiddionIsRun = ProcessUtil.IsAppRun("Kiddion") ? true : false;

                // 判断 SubVersion 是否运行
                UC1HacksModel.SubVersionIsRun = ProcessUtil.IsAppRun("SubVersion") ? true : false;

                // 判断 GTAHax 是否运行
                UC1HacksModel.GTAHaxIsRun = ProcessUtil.IsAppRun("GTAHax") ? true : false;

                // 判断 BincoHax 是否运行
                UC1HacksModel.BincoHaxIsRun = ProcessUtil.IsAppRun("BincoHax") ? true : false;

                // 判断 LSCHax 是否运行
                UC1HacksModel.LSCHaxIsRun = ProcessUtil.IsAppRun("LSCHax") ? true : false;

                // 判断 PedDropper 是否运行
                UC1HacksModel.PedDropperIsRun = ProcessUtil.IsAppRun("PedDropper") ? true : false;

                // 判断 JobMoney 是否运行
                UC1HacksModel.JobMoneyIsRun = ProcessUtil.IsAppRun("JobMoney") ? true : false;

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

                            var pKiddion = Process.GetProcessesByName("Kiddion").ToList()[0];

                            bool isShow = false;
                            do
                            {
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
}
