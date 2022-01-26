using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Navigation;
using System.Windows.Shell;
using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Common.Data;
using System.Net.Http;
using System.Threading;

namespace GTA5OnlineTools.Modules.Kits
{
    /// <summary>
    /// UpdateWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UpdateWindow : Window
    {
        private static WebClient client = new WebClient();

        public UpdateWindow()
        {
            InitializeComponent();
        }

        private void Window_Update_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (GlobalData.ServerData != null)
                {
                    foreach (var item in GlobalData.ServerData.Download)
                    {
                        ListBox_DownloadAddress.Items.Add(item.Name);
                    }
                    ListBox_DownloadAddress.SelectedIndex = 0;
                }

                File.Delete(FileUtil.GetCurrFullPath("未下载完的更新.exe"));
                File.Delete(FileUtil.GetCurrFullPath(CoreUtil.FinalAppName()));
            }
            catch (Exception ex)
            {
                MsgBoxUtil.ExceptionMsgBox(ex);
            }
        }

        private void Window_Update_Closing(object sender, CancelEventArgs e)
        {
            client.CancelAsync();
            client.Dispose();

            GlobalData.UpdateWindow = null;
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

            client.DownloadProgressChanged += Client_DownloadProgressChanged;
            client.DownloadFileCompleted += Client_DownloadFileCompleted;

            client.DownloadFileAsync(new Uri(CoreUtil.UpdateAddress), OldPath);
        }

        private void Button_CancelUpdate_Click(object sender, RoutedEventArgs e)
        {
            client.CancelAsync();

            Button_Update.IsEnabled = true;
            Button_CancelUpdate.IsEnabled = false;

            ProgressBar_Update.Minimum = 0;
            ProgressBar_Update.Maximum = 1024;
            ProgressBar_Update.Value = 0;

            TaskbarItemInfo.ProgressValue = 0;

            TextBlock_Info.Text = "下载取消";
            TextBlock_Percentage.Text = "0KB / 0MB";
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            ProgressBar_Update.Minimum = 0;
            ProgressBar_Update.Maximum = e.TotalBytesToReceive;
            ProgressBar_Update.Value = e.BytesReceived;

            TextBlock_Info.Text = $"下载开始 文件大小 {e.TotalBytesToReceive / 1024.0f / 1024:0.0}MB";

            TextBlock_Percentage.Text = $"{longToString(e.BytesReceived)}/{longToString(e.TotalBytesToReceive)}";

            TaskbarItemInfo.ProgressValue = ProgressBar_Update.Value / ProgressBar_Update.Maximum;
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                TextBlock_Info.Text = $"下载失败 {e.Error.Message}";
            }
            else
            {
                try
                {
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
                    Process.Start(NewPath);
                    Application.Current.Shutdown();
                }
                catch (Exception ex)
                {
                    MsgBoxUtil.ExceptionMsgBox(ex);
                }
            }
        }

        private string longToString(long num)
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
