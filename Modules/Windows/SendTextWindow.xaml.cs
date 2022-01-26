using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Modules.Windows
{
    /// <summary>
    /// SendTextWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SendTextWindow : Window
    {
        //private static readonly HttpClient client = new HttpClient();

        //private static string youdaoAPI = "http://fanyi.youdao.com/translate?&doctype=json&type=AUTO&i=";

        public SendTextWindow()
        {
            InitializeComponent();

            //Dispatcher.BeginInvoke(new Action(delegate
            //{

            //}));
        }

        private void Window_SendText_Loaded(object sender, RoutedEventArgs e)
        {
            Task t = new Task(() =>
            {
                Memory.Initialize(CoreUtil.TargetAppName);
            });
            t.Start();

            TextBox_InputMessage.Text = "请把游戏中输入法调成英文,否则会漏掉文字.Hello1234,漏掉文字了吗?";
        }

        private void Window_SendText_Closing(object sender, CancelEventArgs e)
        {

        }

        private void Button_Translate_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    if (TextBox_InputMessage.Text != "")
            //    {
            //        var str = (e.OriginalSource as Button).Content.ToString();

            //        switch (str)
            //        {
            //            case "中英互译":
            //                Translation();
            //                break;
            //            case "繁转简":
            //                TextBox_InputMessage.Text = CommonUtils.ToSimplifiedChinese(TextBox_InputMessage.Text);
            //                break;
            //            case "简转繁":
            //                TextBox_InputMessage.Text = CommonUtils.ToTraditionalChinese(TextBox_InputMessage.Text);
            //                break;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MsgBoxUtil.ExceptionMsgBox(ex);
            //}
        }

        private void Button_SendTextToGTA5_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    if (TextBox_InputMessage.Text != "")
            //    {
            //        TextBox_InputMessage.Text = CommonUtils.ToDBC(TextBox_InputMessage.Text);

            //        Memory.SetForegroundWindow();

            //        SendMessageToGTA5(TextBox_InputMessage.Text);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MsgBoxUtil.ExceptionMsgBox(ex);
            //}
        }

        //private void KeyPress(WinVK winVK)
        //{
        //    Thread.Sleep(Convert.ToInt32(Slider_SendKey_Sleep2.Value));
        //    WinAPI.Keybd_Event(winVK, WinAPI.MapVirtualKey(winVK, 0), 0, 0);
        //    Thread.Sleep(Convert.ToInt32(Slider_SendKey_Sleep2.Value));
        //    WinAPI.Keybd_Event(winVK, WinAPI.MapVirtualKey(winVK, 0), 2, 0);
        //    Thread.Sleep(Convert.ToInt32(Slider_SendKey_Sleep2.Value));
        //}

        //private void SendMessageToGTA5(string str)
        //{
        //    Thread.Sleep(Convert.ToInt32(Slider_SendKey_Sleep1.Value));

        //    KeyPress(WinVK.RETURN);

        //    if (RadioButton_PressKeyT.IsChecked == true)
        //    {
        //        KeyPress(WinVK.T);
        //    }
        //    else
        //    {
        //        KeyPress(WinVK.Y);
        //    }

        //    Thread.Sleep(Convert.ToInt32(Slider_SendKey_Sleep1.Value));
        //    Forms.SendKeys.Flush();
        //    Thread.Sleep(Convert.ToInt32(Slider_SendKey_Sleep2.Value));
        //    Forms.SendKeys.SendWait(str);
        //    Thread.Sleep(Convert.ToInt32(Slider_SendKey_Sleep2.Value));
        //    Forms.SendKeys.Flush();
        //    Thread.Sleep(Convert.ToInt32(Slider_SendKey_Sleep1.Value));

        //    KeyPress(WinVK.RETURN);
        //    KeyPress(WinVK.RETURN);
        //}

        //private async Task<string> YoudaoTranslation(string str)
        //{
        //    try
        //    {
        //        return await client.GetStringAsync(youdaoAPI + str);
        //    }
        //    catch (HttpRequestException ex)
        //    {
        //        CommonUtils.ExceptionDialogMessage(ex);
        //    }

        //    return string.Empty;
        //}

        //private async void Translation()
        //{
        //    try
        //    {
        //        StringBuilder stringBuilder = new StringBuilder();

        //        string str = await YoudaoTranslation(TextBox_InputMessage.Text);
        //        ReceiveObj rb = JsonConvert.DeserializeObject<ReceiveObj>(str);

        //        foreach (var item in rb.translateResult)
        //        {
        //            foreach (var t in item)
        //            {
        //                stringBuilder.Append(t.tgt);
        //            }
        //        }

        //        TextBox_InputMessage.Text = stringBuilder.ToString();
        //    }
        //    catch (HttpRequestException ex)
        //    {
        //        CommonUtils.ExceptionDialogMessage(ex);
        //    }
        //}

        private void TextBox_InputMessage_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                Button_SendTextToGTA5_Click(null, null);
            }
        }
    }

    public class TranslateResultItemItem
    {
        public string src { get; set; }
        public string tgt { get; set; }
    }

    public class ReceiveObj
    {
        public string type { get; set; }
        public int errorCode { get; set; }
        public int elapsedTime { get; set; }
        public List<List<TranslateResultItemItem>> translateResult { get; set; }
    }
}
