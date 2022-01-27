﻿using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.ComponentModel;
using System.Windows.Navigation;
using System.Windows.Shell;
using Downloader;
using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Common.Data;

namespace GTA5OnlineTools.Modules.Kits
{
    /// <summary>
    /// UpdateWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UpdateWindow : Window
    {
        private DownloadService downloader;

        public UpdateWindow()
        {
            InitializeComponent();
        }

        private void Window_Update_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CoreUtil.ServerVersionInfo != CoreUtil.ClientVersionInfo)
                    AudioUtil.SP_GTA5_Job.Play();

                downloader = new DownloadService();

                if (GlobalData.ServerData != null)
                {
                    foreach (var item in GlobalData.ServerData.Download)
                    {
                        ListBox_DownloadAddress.Items.Add(item.Name);
                    }
                    ListBox_DownloadAddress.SelectedIndex = 0;
                }

                File.Delete(FileUtil.GetCurrFullPath("未下载完的更新.exe"));
            }
            catch (Exception ex)
            {
                MsgBoxUtil.ExceptionMsgBox(ex);
            }
        }

        private void Window_Update_Closing(object sender, CancelEventArgs e)
        {
            downloader.CancelAsync();
            downloader.Clear();
            downloader.Dispose();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            ProcessUtil.OpenLink(e.Uri.OriginalString);
            e.Handled = true;
        }

        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            Button_Update.IsEnabled = false;
            Button_CancelUpdate.IsEnabled = true;

            TextBlock_Info.Text = "下载开始";
            TextBlock_Percentage.Text = "0KB / 0MB";

            int index = ListBox_DownloadAddress.SelectedIndex;
            if (index != -1)
            {
                CoreUtil.UpdateAddress = GlobalData.ServerData.Download[index].Url;
            }
            else
            {
                CoreUtil.UpdateAddress = "https://github.com/CrazyZhang666/GTA5OnlineTools/releases/download/update/GTA5onlineTools.exe";
            }

            // 下载临时文件完整路径
            string OldPath = FileUtil.GetCurrFullPath(CoreUtil.HalfwayAppName);

            downloader.DownloadProgressChanged += DownloadProgressChanged;
            downloader.DownloadFileCompleted += DownloadFileCompleted;

            downloader.DownloadFileTaskAsync(CoreUtil.UpdateAddress, OldPath);
        }

        private void Button_CancelUpdate_Click(object sender, RoutedEventArgs e)
        {
            downloader.CancelAsync();
            downloader.Clear();

            Button_Update.IsEnabled = true;
            Button_CancelUpdate.IsEnabled = false;

            ProgressBar_Update.Minimum = 0;
            ProgressBar_Update.Maximum = 1024;
            ProgressBar_Update.Value = 0;

            TaskbarItemInfo.ProgressValue = 0;

            TextBlock_Info.Text = "下载取消";
            TextBlock_Percentage.Text = "0KB / 0MB";
        }

        private void DownloadProgressChanged(object sender, Downloader.DownloadProgressChangedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                ProgressBar_Update.Minimum = 0;
                ProgressBar_Update.Maximum = e.TotalBytesToReceive;
                ProgressBar_Update.Value = e.ReceivedBytesSize;

                TextBlock_Info.Text = $"下载开始 文件大小 {e.TotalBytesToReceive / 1024.0f / 1024:0.0}MB";

                TextBlock_Percentage.Text = $"{LongToString(e.ReceivedBytesSize)}/{LongToString(e.TotalBytesToReceive)}";

                TaskbarItemInfo.ProgressValue = ProgressBar_Update.Value / ProgressBar_Update.Maximum;
            }));
        }

        private void DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                if (e.Error != null)
                {
                    ProgressBar_Update.Minimum = 0;
                    ProgressBar_Update.Maximum = 1024;
                    ProgressBar_Update.Value = 0;

                    TaskbarItemInfo.ProgressValue = 0;

                    TextBlock_Info.Text = $"下载失败 {e.Error.Message}";
                    TextBlock_Percentage.Text = "0KB / 0MB";
                }
                else
                {
                    try
                    {
                        AudioUtil.SP_DownloadOK.Play();

                        // 下载临时文件完整路径
                        string OldPath = FileUtil.GetCurrFullPath(CoreUtil.HalfwayAppName);
                        // 下载完成后文件真正路径
                        string NewPath = FileUtil.GetCurrFullPath(CoreUtil.FinalAppName());
                        // 下载完成后新文件重命名
                        FileUtil.FileReName(OldPath, NewPath);

                        Thread.Sleep(50);

                        // 下载完成后旧文件重命名
                        string oldFileName = $"[旧版本小助手请手动删除] {Guid.NewGuid()}.exe";
                        // 旧版本小助手重命名
                        FileUtil.FileReName(FileUtil.Current_Path, FileUtil.GetCurrFullPath(oldFileName));

                        TextBlock_Info.Text = "更新下载完成，程序将在3秒内重新启动";

                        Thread.Sleep(1000);
                        App.AppMainMutex.Dispose();
                        ProcessUtil.OpenLink(NewPath);
                        Application.Current.Shutdown();
                    }
                    catch (Exception ex)
                    {
                        MsgBoxUtil.ExceptionMsgBox(ex);
                    }
                }
            }));
        }

        private string LongToString(long num)
        {
            float kb = num / 1024.0f;

            if (kb > 1024)
            {
                return $"{kb / 1024:0.0}MB";
            }
            else
            {
                return $"{kb:0.0}KB";
            }
        }
    }
}