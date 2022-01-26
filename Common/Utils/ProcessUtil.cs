using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Common.Utils
{
    public class ProcessUtil
    {
        /// <summary>
        /// 打开指定链接或程序
        /// </summary>
        /// <param name="link"></param>
        public static void OpenLink(string link)
        {
            Process.Start(new ProcessStartInfo(link) { UseShellExecute = true });
        }

        /// <summary>
        /// 打开指定链接或程序
        /// </summary>
        /// <param name="link"></param>
        public static void OpenLink(string link, string path)
        {
            Process.Start(new ProcessStartInfo(link, path) { UseShellExecute = true });
        }

        /// <summary>
        /// 判断程序是否运行
        /// </summary>
        /// <param name="appName">程序名称</param>
        /// <returns>正在运行返回true，未运行返回false</returns>
        public static bool IsAppRun(string appName)
        {
            return Process.GetProcessesByName(appName).ToList().Count > 0;
        }

        /// <summary>
        /// 打开指定程序，不需要后缀.exe
        /// </summary>
        /// <param name="processName">程序名字，要带后缀名</param>
        /// <param name="isKiddion">是否在Kiddion目录下</param>
        public static void OpenProcess(string processName, bool isKiddion)
        {
            try
            {
                if (IsAppRun(processName))
                {
                    MsgBoxUtil.WarningMsgBox($"请不要重复打开，{processName} 已经在运行了");
                }
                else
                {
                    string path = string.Empty;
                    if (isKiddion)
                    {
                        path = FileUtil.Kiddion_Path;
                    }
                    else
                    {
                        path = FileUtil.Temp_Path;
                    }

                    Directory.SetCurrentDirectory(path);

                    Process process = new Process();
                    process.StartInfo.UseShellExecute = true;
                    // 设置启动动作,确保以管理员身份运行
                    process.StartInfo.Verb = "runas";
                    process.StartInfo.FileName = Path.Combine(path, processName + ".exe");
                    process.Start();
                }
            }
            catch (Exception ex)
            {
                MsgBoxUtil.ExceptionMsgBox(ex);
            }
        }

        /// <summary>
        /// 是否置顶窗口
        /// </summary>
        public static void TopMostProcess(string processName, bool isTopMost)
        {
            try
            {
                var process = Process.GetProcessesByName(processName)[0];
                var windowHandle = process.MainWindowHandle;

                if (isTopMost)
                {
                    WinAPI.SetWindowPos(windowHandle, -1, 0, 0, 0, 0, 1 | 2);

                    //MessageBox.Show($"将目标进程 {processName} 窗口置顶成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    WinAPI.SetWindowPos(windowHandle, -2, 0, 0, 0, 0, 1 | 2);

                    //MessageBox.Show($"将目标进程 {processName} 取消窗口置顶成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MsgBoxUtil.ExceptionMsgBox(ex);
            }
        }

        /// <summary>
        /// 关闭指定程序
        /// </summary>
        /// <param name="processName">程序名字</param>
        public static void CloseProcess(string processName)
        {
            Process[] allProgresse = Process.GetProcesses();
            foreach (Process closeProgress in allProgresse)
            {
                if (closeProgress.ProcessName.Equals(processName))
                {
                    closeProgress.Kill();
                }
            }
        }

        /// <summary>
        /// 关闭全部第三方exe进程
        /// </summary> 
        public static void CloseTheseProcess()
        {
            CloseProcess("Kiddion");
            CloseProcess("Kiddion_Chs");
            CloseProcess("SubVersion");
            CloseProcess("GTAHax");
            CloseProcess("BincoHax");
            CloseProcess("LSCHax");
            CloseProcess("PedDropper");
            CloseProcess("JobMoney");
            CloseProcess("DefenderControl");
        }
    }
}
