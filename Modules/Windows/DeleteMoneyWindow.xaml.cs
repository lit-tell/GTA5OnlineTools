using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Modules.Windows;

/// <summary>
/// DeleteMoneyWindow.xaml 的交互逻辑
/// </summary>
public partial class DeleteMoneyWindow : Window
{
    private int cost = 0;

    public DeleteMoneyWindow()
    {
        InitializeComponent();

        Memory.Initialize(CoreUtil.TargetAppName);

        Globals.TempPTR = Memory.FindPattern(Offsets.Mask.GlobalMask);
        Globals.GlobalPTR = Memory.Rip_37(Globals.TempPTR);

        Dispatcher.Invoke(new Action(delegate
        {
            TextBox_Cost.Text = Globals.Get_Ballistic_Armor_Request_Cost().ToString();
        }));

        var thread_init = new Thread(InitThread);
        thread_init.IsBackground = true;
        thread_init.Start();
    }

    private void InitThread()
    {
        while (true)
        {
            if (!ProcessUtil.IsAppRun("GTA5"))
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.Close();
                });
                return;
            }

            Thread.Sleep(2000);
        }
    }

    private void TextBox_Cost_TextChanged(object sender, TextChangedEventArgs e)
    {
        bool result = int.TryParse(TextBox_Cost.Text, out cost);
        //try
        //{
        //    cost = Convert.ToInt32(TextBox_Cost.Text);
        //}
        //catch (Exception ex)
        //{
        //    MsgBoxUtil.ExceptionMsgBox(ex);
        //}
    }

    private void Button_SetCost_Click(object sender, RoutedEventArgs e) { AudioUtil.ClickSound(); Globals.Set_Ballistic_Armor_Request_Cost(cost); }
}
