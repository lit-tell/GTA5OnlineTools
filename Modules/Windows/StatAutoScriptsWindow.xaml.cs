using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.Core;
using GTA5OnlineTools.Features.SDK;
using static GTA5OnlineTools.Features.Data.StatData;

namespace GTA5OnlineTools.Modules.Windows
{
    /// <summary>
    /// StatAutoScriptsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class StatAutoScriptsWindow : Window
    {
        public StatAutoScriptsWindow()
        {
            InitializeComponent();
        }

        private void Window_StatAutoScripts_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                Memory.Initialize(CoreUtil.TargetAppName);

                Offsets.Mask.TempPTR = Memory.FindPattern(Offsets.Mask.GlobalPTR);
                Globals.GlobalPTR = Memory.Rip_37(Offsets.Mask.TempPTR);
            });
        }

        private void Window_StatAutoScripts_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Button_LoadSession_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            Online.LoadSession(11);
        }

        private void AppendTextBox(string str)
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                TextBox_Result.AppendText($"[{DateTime.Now:T}] {str}\r\n");
                TextBox_Result.ScrollToEnd();
            }));
        }

        private void AutoScript(string comName)
        {
            TextBox_Result.Clear();

            Task.Run(() =>
            {
                try
                {
                    int index = StatDataClass.FindIndex(t => t.SName == comName);

                    AppendTextBox($"正在执行 {StatDataClass[index].SName} 脚本代码");

                    for (int i = 0; i < StatDataClass[index].SCode.Count; i++)
                    {
                        AppendTextBox($"正在执行 第 {i + 1}/{StatDataClass[index].SCode.Count} 条代码");

                        Hacks.WriteStat(StatDataClass[index].SCode[i].SHash, StatDataClass[index].SCode[i].SValue);
                        Task.Delay(500).Wait();
                    }

                    AppendTextBox($"{StatDataClass[index].SName} 脚本代码执行完毕，请切换战局生效");
                }
                catch (Exception ex)
                {
                    AppendTextBox($"发生了未知的错误 {ex.Message}");
                }
            });
        }

        private void Button_ExecuteAutoScript_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            AutoScript((e.OriginalSource as Button).Content.ToString());
        }
    }
}
