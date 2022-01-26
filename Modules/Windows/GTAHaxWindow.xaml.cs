using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Threading.Tasks;
using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Modules.Windows
{
    /// <summary>
    /// GTAHaxWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GTAHaxWindow : Window
    {
        private string GTAHax_MP = "$MPx";

        public GTAHaxWindow()
        {
            InitializeComponent();
        }

        private void Window_GTAHax_Loaded(object sender, RoutedEventArgs e)
        {
            TextBox_GTAHaxCodePreview.Text = "INT32\n";

            ListBox_GTAHaxCode_FuncList.Items.SortDescriptions.Add(new SortDescription("Content", ListSortDirection.Ascending));
        }

        private void Window_GTAHax_Closing(object sender, CancelEventArgs e)
        {
            
        }

        private void Button_Read_Stat_Click(object sender, RoutedEventArgs e)
        {
            FileUtil.ReadTextToTextBox(TextBox_GTAHaxCodePreview, FileUtil.GTAHaxStat_Path);

            //MessageBox.Show("读取stat.txt文件成功", " 提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_Write_Stat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                File.WriteAllText(FileUtil.GTAHaxStat_Path, string.Empty);

                using (StreamWriter sw = new StreamWriter(FileUtil.GTAHaxStat_Path, true))
                {
                    sw.Write(TextBox_GTAHaxCodePreview.Text);

                    if (MessageBox.Show("写入成功！\n\n点击<是>确认结果，点击<否>打开stat.txt查看", " 提示", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.No)
                    {
                        ProcessUtil.OpenLink(FileUtil.GTAHaxStat_Path);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("写入stat.txt文件失败！\n\n" + "错误提示：\n" + ex.Message,
                    "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Import_Stat_Click(object sender, RoutedEventArgs e)
        {
            if (!ProcessUtil.IsAppRun("GTAHax"))
                ProcessUtil.OpenProcess("GTAHax", false);

            Task.Run(() =>
            {
                bool isRun = false;
                do
                {
                    if (ProcessUtil.IsAppRun("GTAHax"))
                    {
                        isRun = true;

                        var pGTAHax = Process.GetProcessesByName("GTAHax").ToList()[0];

                        bool isShow = false;
                        do
                        {
                            IntPtr Menu_handle = pGTAHax.MainWindowHandle;
                            IntPtr child_handle = WinAPI.FindWindowEx(Menu_handle, IntPtr.Zero, "Static", null);
                            child_handle = WinAPI.FindWindowEx(Menu_handle, child_handle, "Static", null);
                            child_handle = WinAPI.FindWindowEx(Menu_handle, child_handle, "Static", null);
                            child_handle = WinAPI.FindWindowEx(Menu_handle, child_handle, "Static", null);

                            child_handle = WinAPI.FindWindowEx(Menu_handle, child_handle, "Edit", null);
                            child_handle = WinAPI.FindWindowEx(Menu_handle, child_handle, "Edit", null);

                            child_handle = WinAPI.FindWindowEx(Menu_handle, child_handle, "Button", null);
                            child_handle = WinAPI.FindWindowEx(Menu_handle, child_handle, "Button", null);

                            child_handle = WinAPI.FindWindowEx(Menu_handle, child_handle, "Button", null);

                            if (child_handle != IntPtr.Zero)
                            {
                                isShow = true;

                                WinAPI.SendMessage(child_handle, WinAPI.WM_LBUTTONDOWN, IntPtr.Zero, null);
                                WinAPI.SendMessage(child_handle, WinAPI.WM_LBUTTONUP, IntPtr.Zero, null);

                                MessageBox.Show("导入到GTAHax成功！代码正在执行，请返回GTAHax和GTA5游戏查看\n\n" +
                                    "如果未执行，请重新点击\"导入GTAHax\"",
                                    "提示", MessageBoxButton.OK, MessageBoxImage.Information);
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
            });
        }

        private void Button_ToMPx_TextBox_Click(object sender, RoutedEventArgs e)
        {
            TextBox_GTAHaxCodePreview.Text = TextBox_GTAHaxCodePreview.Text.Replace("$MP0", "$MPx");
            TextBox_GTAHaxCodePreview.Text = TextBox_GTAHaxCodePreview.Text.Replace("$MP1", "$MPx");

            //MessageBox.Show("替换角色为自动成功", " 提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        void TextBox_AppendText_MP(string str, string value)
        {
            TextBox_GTAHaxCodePreview.AppendText("\r\n" + GTAHax_MP + str);
            TextBox_GTAHaxCodePreview.AppendText("\r\n" + value);
        }

        void TextBox_AppendText_NoMP(string str, string value)
        {
            TextBox_GTAHaxCodePreview.AppendText("\r\n" + str);
            TextBox_GTAHaxCodePreview.AppendText("\r\n" + value);
        }

        string SelectedItemContent(ListBox listBox)
        {
            return (listBox.SelectedItem as ListBoxItem).Content.ToString();
        }

        /**********************************************************************************/

        private void ListBox_GTAHaxCode_FuncList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                if (RadioButton_MPx.IsChecked == true)
                {
                    GTAHax_MP = "$MPx";
                }
                else if (RadioButton_MP0.IsChecked == true)
                {
                    GTAHax_MP = "$MP0";
                }
                else if (RadioButton_MP1.IsChecked == true)
                {
                    GTAHax_MP = "$MP1";
                }

                TextBox_GTAHaxCodePreview.Clear();
                TextBox_GTAHaxCodePreview.AppendText("INT32");

                if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "角色-属性全满")
                {
                    TextBox_AppendText_MP("_SCRIPT_INCREASE_STAM", "100");
                    TextBox_AppendText_MP("_SCRIPT_INCREASE_SHO", "100");
                    TextBox_AppendText_MP("_SCRIPT_INCREASE_STRN", "100");
                    TextBox_AppendText_MP("_SCRIPT_INCREASE_STL", "100");
                    TextBox_AppendText_MP("_SCRIPT_INCREASE_FLY", "100");
                    TextBox_AppendText_MP("_SCRIPT_INCREASE_DRIV", "100");
                    TextBox_AppendText_MP("_SCRIPT_INCREASE_LUNG", "100");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "角色-隐藏属性全满")
                {
                    TextBox_AppendText_MP("_CHAR_ABILITY_1_UNLCK", "-1");
                    TextBox_AppendText_MP("_CHAR_ABILITY_2_UNLCK", "-1");
                    TextBox_AppendText_MP("_CHAR_ABILITY_3_UNLCK", "-1");
                    TextBox_AppendText_MP("_CHAR_FM_ABILITY_1_UNLCK", "-1");
                    TextBox_AppendText_MP("_CHAR_FM_ABILITY_2_UNLCK", "-1");
                    TextBox_AppendText_MP("_CHAR_FM_ABILITY_3_UNLCK", "-1");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "角色-性别更改（去重新捏脸）")
                {
                    TextBox_AppendText_MP("_ALLOW_GENDER_CHANGE", "52");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "角色-等级修改为1级")
                {
                    TextBox_AppendText_MP("_CHAR_SET_RP_GIFT_ADMIN", "0");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "角色-等级修改为30级")
                {
                    TextBox_AppendText_MP("_CHAR_SET_RP_GIFT_ADMIN", "177100");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "角色-等级修改为90级")
                {
                    TextBox_AppendText_MP("_CHAR_SET_RP_GIFT_ADMIN", "1308100");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "角色-等级修改为120级")
                {
                    TextBox_AppendText_MP("_CHAR_SET_RP_GIFT_ADMIN", "2165850");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "角色-零食全满")
                {
                    TextBox_AppendText_MP("_NO_BOUGHT_YUM_SNACKS", "99");
                    TextBox_AppendText_MP("_NO_BOUGHT_HEALTH_SNACKS", "99");
                    TextBox_AppendText_MP("_NO_BOUGHT_EPIC_SNACKS", "99");
                    TextBox_AppendText_MP("_NUMBER_OF_ORANGE_BOUGHT", "99");
                    TextBox_AppendText_MP("_NUMBER_OF_BOURGE_BOUGHT", "99");
                    TextBox_AppendText_MP("_CIGARETTES_BOUGHT", "99");
                    TextBox_AppendText_MP("_NUMBER_OF_CHAMP_BOUGHT", "99");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "角色-护甲全满")
                {
                    TextBox_AppendText_MP("_MP_CHAR_ARMOUR_1_COUNT", "99");
                    TextBox_AppendText_MP("_MP_CHAR_ARMOUR_2_COUNT", "99");
                    TextBox_AppendText_MP("_MP_CHAR_ARMOUR_3_COUNT", "99");
                    TextBox_AppendText_MP("_MP_CHAR_ARMOUR_4_COUNT", "99");
                    TextBox_AppendText_MP("_MP_CHAR_ARMOUR_5_COUNT", "99");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "赌场-解除老虎机限制")
                {
                    TextBox_AppendText_NoMP("$MPPLY_CASINO_CHIPS_WON_GD", "0");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "CEO办公室-解锁小金人（切换战局，进出货一次）")
                {
                    TextBox_AppendText_MP("_LIFETIME_BUY_UNDERTAKEN", "1000");
                    TextBox_AppendText_MP("_LIFETIME_BUY_COMPLETE", "1000");
                    TextBox_AppendText_MP("_LIFETIME_SELL_UNDERTAKEN", "1000");
                    TextBox_AppendText_MP("_LIFETIME_SELL_COMPLETE", "1000");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "CEO办公室-地板全是钱（前提你银行里必须有2000万以上存款）")
                {
                    TextBox_AppendText_MP("_LIFETIME_CONTRA_EARNINGS", "20000000");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "杂项-重置载具销售计时")
                {
                    TextBox_AppendText_NoMP("$MPPLY_VEHICLE_SELL_TIME", "0");
                    TextBox_AppendText_NoMP("$MPPLY_NUM_CARS_SOLD_TODAY", "0");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "夜总会-补满人气")
                {
                    TextBox_AppendText_MP("_CLUB_POPULARITY", "1000");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "自动进货-摩托帮-假证（补完换战局）")
                {
                    TextBox_AppendText_MP("_PAYRESUPPLYTIMER0", "1");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "自动进货-摩托帮-大麻（补完换战局）")
                {
                    TextBox_AppendText_MP("_PAYRESUPPLYTIMER1", "1");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "自动进货-摩托帮-伪钞（补完换战局）")
                {
                    TextBox_AppendText_MP("_PAYRESUPPLYTIMER2", "1");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "自动进货-摩托帮-冰毒（补完换战局）")
                {
                    TextBox_AppendText_MP("_PAYRESUPPLYTIMER3", "1");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "自动进货-摩托帮-可卡因（补完换战局）")
                {
                    TextBox_AppendText_MP("_PAYRESUPPLYTIMER4", "1");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "自动进货-地堡（补完换战局）")
                {
                    TextBox_AppendText_MP("_PAYRESUPPLYTIMER5", "1");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "公寓抢劫-解锁全部公寓抢劫（切换角色生效）")
                {
                    TextBox_AppendText_NoMP("", "----------------");
                    TextBox_AppendText_NoMP("注意", "此代码无法导入，需要人工一个一个执行");
                    TextBox_AppendText_NoMP("----------------", "");
                    TextBox_AppendText_NoMP("$MPPLY_AWD_COMPLET_HEIST_1STPER", "true");
                    TextBox_AppendText_NoMP("$MPPLY_AWD_COMPLET_HEIST_MEM", "true");
                    TextBox_AppendText_NoMP("$MPPLY_AWD_FLEECA_FIN", "true");
                    TextBox_AppendText_NoMP("$MPPLY_AWD_PRISON_FIN", "true");
                    TextBox_AppendText_NoMP("$MPPLY_AWD_HUMANE_FIN", "true");
                    TextBox_AppendText_NoMP("$MPPLY_AWD_SERIESA_FIN", "true");
                    TextBox_AppendText_NoMP("$MPPLY_AWD_PACIFIC_FIN", "true");
                    TextBox_AppendText_NoMP("$MPPLY_AWD_HST_ORDER", "true");
                    TextBox_AppendText_NoMP("$MPPLY_AWD_HST_SAME_TEAM", "true");
                    TextBox_AppendText_NoMP("$MPPLY_AWD_HST_ULT_CHAL", "true");
                    TextBox_AppendText_NoMP("$MPPLY_HEIST_ACH_TRACKER", "-1");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "公寓抢劫-跳过准备任务（看动画过程中输入）")
                {
                    TextBox_AppendText_MP("_HEIST_PLANNING_STAGE", "-1");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "末日抢劫-解锁/重玩全部流程（切换战局，打给莱斯特取消末日浩劫任务3次）")
                {
                    TextBox_AppendText_MP("_GANGOPS_HEIST_STATUS", "-1");
                    TextBox_AppendText_MP("_GANGOPS_FLOW_NOTIFICATIONS", "-1");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "末日抢劫-跳过前置及准备任务（M键-设施管理-关闭/开启策划大屏）")
                {
                    TextBox_AppendText_MP("_GANGOPS_FLOW_MISSION_PROG", "-1");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "末日抢劫-1重置末日一冷却时间")
                {
                    TextBox_AppendText_MP("_HEISTCOOLDOWNTIMER0", "-1");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "末日抢劫-2重置末日二冷却时间")
                {
                    TextBox_AppendText_MP("_HEISTCOOLDOWNTIMER1", "-1");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "末日抢劫-3重置末日三冷却时间")
                {
                    TextBox_AppendText_MP("_HEISTCOOLDOWNTIMER2", "-1");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "末日抢劫-载具批发价格解锁")
                {
                    TextBox_AppendText_MP("_GANGOPS_FLOW_BITSET_MISS0", "48326");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "末日抢劫-跳过前置-1数据泄露（M键-设施管理-关闭/开启策划大屏）")
                {
                    TextBox_AppendText_MP("_GANGOPS_FLOW_MISSION_PROG", "503");
                    TextBox_AppendText_MP("_GANGOPS_HEIST_STATUS", "229383");
                    TextBox_AppendText_MP("_GANGOPS_FLOW_NOTIFICATIONS", "1557");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "末日抢劫-跳过前置-2波格丹危机（M键-设施管理-关闭/开启策划大屏）")
                {
                    TextBox_AppendText_MP("_GANGOPS_FLOW_MISSION_PROG", "240");
                    TextBox_AppendText_MP("_GANGOPS_HEIST_STATUS", "229378");
                    TextBox_AppendText_MP("_GANGOPS_FLOW_NOTIFICATIONS", "1557");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "末日抢劫-跳过前置-3末日将至（M键-设施管理-关闭/开启策划大屏）")
                {
                    TextBox_AppendText_MP("_GANGOPS_FLOW_MISSION_PROG", "16368");
                    TextBox_AppendText_MP("_GANGOPS_HEIST_STATUS", "229380");
                    TextBox_AppendText_MP("_GANGOPS_FLOW_NOTIFICATIONS", "1557");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "解锁-限定载具节日涂装")
                {
                    TextBox_AppendText_NoMP("$MPPLY_XMASLIVERIES0", "-1");
                    TextBox_AppendText_NoMP("$MPPLY_XMASLIVERIES1", "-1");
                    TextBox_AppendText_NoMP("$MPPLY_XMASLIVERIES2", "-1");
                    TextBox_AppendText_NoMP("$MPPLY_XMASLIVERIES3", "-1");
                    TextBox_AppendText_NoMP("$MPPLY_XMASLIVERIES4", "-1");
                    TextBox_AppendText_NoMP("$MPPLY_XMASLIVERIES5", "-1");
                    TextBox_AppendText_NoMP("$MPPLY_XMASLIVERIES6", "-1");
                    TextBox_AppendText_NoMP("$MPPLY_XMASLIVERIES7", "-1");
                    TextBox_AppendText_NoMP("$MPPLY_XMASLIVERIES8", "-1");
                    TextBox_AppendText_NoMP("$MPPLY_XMASLIVERIES9", "-1");
                    TextBox_AppendText_NoMP("$MPPLY_XMASLIVERIES10", "-1");
                    TextBox_AppendText_NoMP("$MPPLY_XMASLIVERIES11", "-1");
                    TextBox_AppendText_NoMP("$MPPLY_XMASLIVERIES12", "-1");
                    TextBox_AppendText_NoMP("$MPPLY_XMASLIVERIES13", "-1");
                    TextBox_AppendText_NoMP("$MPPLY_XMASLIVERIES14", "-1");
                    TextBox_AppendText_NoMP("$MPPLY_XMASLIVERIES15", "-1");
                    TextBox_AppendText_NoMP("$MPPLY_XMASLIVERIES16", "-1");
                    TextBox_AppendText_NoMP("$MPPLY_XMASLIVERIES17", "-1");
                    TextBox_AppendText_NoMP("$MPPLY_XMASLIVERIES18", "-1");
                    TextBox_AppendText_NoMP("$MPPLY_XMASLIVERIES19", "-1");
                    TextBox_AppendText_NoMP("$MPPLY_XMASLIVERIES20", "-1");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "解锁-全部游艇任务")
                {
                    TextBox_AppendText_MP("_YACHT_MISSION_PROG", "0");
                    TextBox_AppendText_MP("_YACHT_MISSION_FLOW", "21845");
                    TextBox_AppendText_MP("_CASINO_DECORATION_GIFT_1", "-1");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "解锁-外星人纹身")
                {
                    TextBox_AppendText_MP("_TATTOO_FM_CURRENT_32", "32768");
                    TextBox_AppendText_MP("_TATTOO_FM_CURRENT_32", "67108864");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "解锁-载具金属质感喷漆与铬合金轮毂")
                {
                    TextBox_AppendText_MP("_CHAR_FM_CARMOD_1_UNLCK", "-1");
                    TextBox_AppendText_MP("_CHAR_FM_CARMOD_2_UNLCK", "-1");
                    TextBox_AppendText_MP("_CHAR_FM_CARMOD_3_UNLCK", "-1");
                    TextBox_AppendText_MP("_CHAR_FM_CARMOD_4_UNLCK", "-1");
                    TextBox_AppendText_MP("_CHAR_FM_CARMOD_5_UNLCK", "-1");
                    TextBox_AppendText_MP("_CHAR_FM_CARMOD_6_UNLCK", "-1");
                    TextBox_AppendText_MP("_CHAR_FM_CARMOD_7_UNLCK", "-1");
                    TextBox_AppendText_MP("_NUMBER_TURBO_STARTS_IN_RACE", "50");
                    TextBox_AppendText_MP("_USJS_COMPLETED", "50");
                    TextBox_AppendText_MP("_AWD_FM_RACES_FASTEST_LAP", "50");
                    TextBox_AppendText_MP("_NUMBER_SLIPSTREAMS_IN_RACE", "100");
                    TextBox_AppendText_MP("_AWD_WIN_CAPTURES", "50");
                    TextBox_AppendText_MP("_AWD_DROPOFF_CAP_PACKAGES", "1");
                    TextBox_AppendText_MP("_AWD_KILL_CARRIER_CAPTURE", "1");
                    TextBox_AppendText_MP("_AWD_FINISH_HEISTS", "50");
                    TextBox_AppendText_MP("_AWD_FINISH_HEIST_SETUP_JOB", "50");
                    TextBox_AppendText_MP("_AWD_NIGHTVISION_KILLS", "1");
                    TextBox_AppendText_MP("_AWD_WIN_LAST_TEAM_STANDINGS", "50");
                    TextBox_AppendText_MP("_AWD_ONLY_PLAYER_ALIVE_LTS", "50");
                    TextBox_AppendText_MP("_AWD_FMRALLYWONDRIVE", "1");
                    TextBox_AppendText_MP("_AWD_FMRALLYWONNAV", "1");
                    TextBox_AppendText_MP("_AWD_FMWINSEARACE", "1");
                    TextBox_AppendText_MP("_AWD_FMWINAIRRACE", "1");
                    TextBox_AppendText_MP("_AWD_RACES_WON", "50");
                    TextBox_AppendText_MP("_RACES_WON", "50");
                    TextBox_AppendText_NoMP("$MPPLY_TOTAL_RACES_WON", "50");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "解锁-全部联系人")
                {
                    TextBox_AppendText_MP("_FM_ACT_PHN", "-1");
                    TextBox_AppendText_MP("_FM_ACT_PH2", "-1");
                    TextBox_AppendText_MP("_FM_ACT_PH3", "-1");
                    TextBox_AppendText_MP("_FM_ACT_PH4", "-1");
                    TextBox_AppendText_MP("_FM_ACT_PH5", "-1");
                    TextBox_AppendText_MP("_FM_VEH_TX1", "-1");
                    TextBox_AppendText_MP("_FM_ACT_PH6", "-1");
                    TextBox_AppendText_MP("_FM_ACT_PH7", "-1");
                    TextBox_AppendText_MP("_FM_ACT_PH8", "-1");
                    TextBox_AppendText_MP("_FM_ACT_PH9", "-1");
                    TextBox_AppendText_MP("_FM_CUT_DONE", "-1");
                    TextBox_AppendText_MP("_FM_CUT_DONE_2", "-1");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "赌场-重置购买筹码冷却")
                {
                    TextBox_AppendText_NoMP("MPPLY_CASINO_CHIPS_PUR_GD", "0");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "杂项-天基炮无冷却")
                {
                    TextBox_AppendText_MP("_ORBITAL_CANNON_COOLDOWN", "0");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "杂项-跳过过场动画（地堡、摩托帮、办公室等）")
                {
                    TextBox_AppendText_MP("_FM_CUT_DONE", "-1");
                    TextBox_AppendText_MP("_FM_CUT_DONE_2", "-1");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "竞技场-25级解锁出租车")
                {
                    TextBox_AppendText_MP("_ARENAWARS_AP_TIER", "24");
                    TextBox_AppendText_MP("_ARENAWARS_AP", "280");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "竞技场-50级解锁推土机")
                {
                    TextBox_AppendText_MP("_ARENAWARS_AP_TIER", "49");
                    TextBox_AppendText_MP("_ARENAWARS_AP", "530");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "竞技场-75级解锁小丑花车")
                {
                    TextBox_AppendText_MP("_ARENAWARS_AP_TIER", "74");
                    TextBox_AppendText_MP("_ARENAWARS_AP", "780");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "竞技场-100级解锁垃圾大王")
                {
                    TextBox_AppendText_MP("_ARENAWARS_AP_TIER", "99");
                    TextBox_AppendText_MP("_ARENAWARS_AP", "1030");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "竞技场-200级解锁地霸王拖车")
                {
                    TextBox_AppendText_MP("_ARENAWARS_AP_TIER", "199");
                    TextBox_AppendText_MP("_ARENAWARS_AP", "2030");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "竞技场-300级解锁混凝土搅拌车")
                {
                    TextBox_AppendText_MP("_ARENAWARS_AP_TIER", "299");
                    TextBox_AppendText_MP("_ARENAWARS_AP", "3030");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "竞技场-500级解锁星际码头")
                {
                    TextBox_AppendText_MP("_ARENAWARS_AP_TIER", "499");
                    TextBox_AppendText_MP("_ARENAWARS_AP", "5030");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "竞技场-1000级解锁老式机")
                {
                    TextBox_AppendText_MP("_ARENAWARS_AP_TIER", "999");
                    TextBox_AppendText_MP("_ARENAWARS_AP", "10030");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "竞技场-解锁冲冲猴旅行家购买权限")
                {
                    TextBox_AppendText_MP("_ARENAWARS_SKILL_LEVEL", "19");
                    TextBox_AppendText_MP("_ARENAWARS_SP", "209");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "游戏厅-解锁隐藏骇客（50个干扰器）")
                {
                    TextBox_AppendText_MP("_CAS_HISIT_Flow", "-1");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "游戏厅-解锁所有的探查点")
                {
                    TextBox_AppendText_MP("_H3OPT_ACCESSPOINTS", "-1");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "游戏厅-解锁所有的兴趣点")
                {
                    TextBox_AppendText_MP("_H3OPT_POI", "-1");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "赌场抢劫-重置抢劫冷却时间")
                {
                    TextBox_AppendText_MP("_H3_COMPLETEDPOSIX", "-1");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "赌场抢劫-枪手车手枪手工具人0分红（拿完东西出金库门后写入）")
                {
                    TextBox_AppendText_MP("_H3OPT_CREWWEAP", "6");
                    TextBox_AppendText_MP("_H3OPT_CREWDRIVER", "6");
                    TextBox_AppendText_MP("_H3OPT_CREWHACKER", "6");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "赌场抢劫-重置第一第二块面板")
                {
                    TextBox_AppendText_MP("_H3OPT_BITSET1", "0");
                    TextBox_AppendText_MP("_H3OPT_BITSET0", "0");
                    TextBox_AppendText_MP("_H3OPT_POI", "0");
                    TextBox_AppendText_MP("_H3OPT_ACCESSPOINTS", "0");
                    TextBox_AppendText_MP("_CAS_HEIST_FLOW", "0");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "佩里科岛抢劫-重置海岛面板")
                {
                    TextBox_AppendText_MP("_H4_MISSIONS", "0");
                    TextBox_AppendText_MP("_H4_PROGRESS", "0");
                    TextBox_AppendText_MP("_H4_PLAYTHROUGH_STATUS", "0");
                    TextBox_AppendText_MP("_H4CNF_APPROACH", "0");
                    TextBox_AppendText_MP("_H4CNF_BS_ENTR", "0");
                    TextBox_AppendText_MP("_H4CNF_BS_GEN", "0");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "佩里科岛抢劫-重置次要目标")
                {
                    TextBox_AppendText_MP("_H4LOOT_CASH_I", "0");
                    TextBox_AppendText_MP("_H4LOOT_CASH_C", "0");
                    TextBox_AppendText_MP("_H4LOOT_CASH_I_SCOPED", "0");
                    TextBox_AppendText_MP("_H4LOOT_CASH_C_SCOPED", "0");
                    TextBox_AppendText_MP("_H4LOOT_CASH_V", "0");
                    TextBox_AppendText_MP("_H4LOOT_WEED_I", "0");
                    TextBox_AppendText_MP("_H4LOOT_WEED_C", "0");
                    TextBox_AppendText_MP("_H4LOOT_WEED_I_SCOPED", "0");
                    TextBox_AppendText_MP("_H4LOOT_WEED_C_SCOPED", "0");
                    TextBox_AppendText_MP("_H4LOOT_WEED_V", "0");
                    TextBox_AppendText_MP("_H4LOOT_COKE_I", "0");
                    TextBox_AppendText_MP("_H4LOOT_COKE_C", "0");
                    TextBox_AppendText_MP("_H4LOOT_COKE_I_SCOPED", "0");
                    TextBox_AppendText_MP("_H4LOOT_COKE_C_SCOPED", "0");
                    TextBox_AppendText_MP("_H4LOOT_COKE_V", "0");
                    TextBox_AppendText_MP("_H4LOOT_GOLD_I", "0");
                    TextBox_AppendText_MP("_H4LOOT_GOLD_C", "0");
                    TextBox_AppendText_MP("_H4LOOT_GOLD_I_SCOPED", "0");
                    TextBox_AppendText_MP("_H4LOOT_GOLD_C_SCOPED", "0");
                    TextBox_AppendText_MP("_H4LOOT_GOLD_V", "0");
                    TextBox_AppendText_MP("_H4LOOT_PAINT", "0");
                    TextBox_AppendText_MP("_H4LOOT_PAINT_SCOPED", "0");
                    TextBox_AppendText_MP("_H4LOOT_PAINT_V", "0");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "佩里科岛抢劫-玩家x1/100%分红/困难模式/无精英/不拿保险柜/人均245W（粉钻x1+画作x2）")
                {
                    TextBox_AppendText_MP("_H4CNF_BS_GEN", "131071");
                    TextBox_AppendText_MP("_H4CNF_BS_ENTR", "63");
                    TextBox_AppendText_MP("_H4CNF_BS_ABIL", "63");
                    TextBox_AppendText_MP("_H4CNF_APPROACH", "-1");
                    TextBox_AppendText_MP("_H4_PROGRESS", "131055");
                    TextBox_AppendText_MP("_H4CNF_TARGET", "3");
                    TextBox_AppendText_MP("_H4LOOT_PAINT", "-1");
                    TextBox_AppendText_MP("_H4LOOT_PAINT_SCOPED", "-1");
                    TextBox_AppendText_MP("_H4LOOT_PAINT_V", "677045");
                    TextBox_AppendText_MP("_H4_MISSIONS", "65535");
                    TextBox_AppendText_MP("_H4CNF_WEAPONS", "2");
                    TextBox_AppendText_MP("_H4CNF_WEP_DISRP", "3");
                    TextBox_AppendText_MP("_H4CNF_ARM_DISRP", "3");
                    TextBox_AppendText_MP("_H4CNF_HEL_DISRP", "3");
                    TextBox_AppendText_MP("_H4CNF_GRAPPEL", "-1");
                    TextBox_AppendText_MP("_H4CNF_UNIFORM", "-1");
                    TextBox_AppendText_MP("_H4CNF_BOLTCUT", "-1");
                    TextBox_AppendText_MP("_H4CNF_TROJAN", "4");
                    TextBox_AppendText_MP("_H4_PLAYTHROUGH_STATUS", "10");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "佩里科岛抢劫-玩家x2/50%50%分红/困难模式/无精英/不拿保险柜/人均245W（粉钻x1+画作x4）")
                {
                    TextBox_AppendText_MP("_H4CNF_BS_GEN", "131071");
                    TextBox_AppendText_MP("_H4CNF_BS_ENTR", "63");
                    TextBox_AppendText_MP("_H4CNF_BS_ABIL", "63");
                    TextBox_AppendText_MP("_H4CNF_APPROACH", "-1");
                    TextBox_AppendText_MP("_H4_PROGRESS", "131055");
                    TextBox_AppendText_MP("_H4CNF_TARGET", "3");
                    TextBox_AppendText_MP("_H4LOOT_PAINT", "-1");
                    TextBox_AppendText_MP("_H4LOOT_PAINT_SCOPED", "-1");
                    TextBox_AppendText_MP("_H4LOOT_PAINT_V", "1034545");
                    TextBox_AppendText_MP("_H4_MISSIONS", "65535");
                    TextBox_AppendText_MP("_H4CNF_WEAPONS", "2");
                    TextBox_AppendText_MP("_H4CNF_WEP_DISRP", "3");
                    TextBox_AppendText_MP("_H4CNF_ARM_DISRP", "3");
                    TextBox_AppendText_MP("_H4CNF_HEL_DISRP", "3");
                    TextBox_AppendText_MP("_H4CNF_GRAPPEL", "-1");
                    TextBox_AppendText_MP("_H4CNF_UNIFORM", "-1");
                    TextBox_AppendText_MP("_H4CNF_BOLTCUT", "-1");
                    TextBox_AppendText_MP("_H4CNF_TROJAN", "4");
                    TextBox_AppendText_MP("_H4_PLAYTHROUGH_STATUS", "10");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "佩里科岛抢劫-玩家x3/35%35%30%分红/困难模式/无精英/不拿保险柜/人均245W（粉钻x1+画作x6）")
                {
                    TextBox_AppendText_MP("_H4CNF_BS_GEN", "131071");
                    TextBox_AppendText_MP("_H4CNF_BS_ENTR", "63");
                    TextBox_AppendText_MP("_H4CNF_BS_ABIL", "63");
                    TextBox_AppendText_MP("_H4CNF_APPROACH", "-1");
                    TextBox_AppendText_MP("_H4_PROGRESS", "131055");
                    TextBox_AppendText_MP("_H4CNF_TARGET", "3");
                    TextBox_AppendText_MP("_H4LOOT_PAINT", "-1");
                    TextBox_AppendText_MP("_H4LOOT_PAINT_SCOPED", "-1");
                    TextBox_AppendText_MP("_H4LOOT_PAINT_V", "1087424");
                    TextBox_AppendText_MP("_H4_MISSIONS", "65535");
                    TextBox_AppendText_MP("_H4CNF_WEAPONS", "2");
                    TextBox_AppendText_MP("_H4CNF_WEP_DISRP", "3");
                    TextBox_AppendText_MP("_H4CNF_ARM_DISRP", "3");
                    TextBox_AppendText_MP("_H4CNF_HEL_DISRP", "3");
                    TextBox_AppendText_MP("_H4CNF_GRAPPEL", "-1");
                    TextBox_AppendText_MP("_H4CNF_UNIFORM", "-1");
                    TextBox_AppendText_MP("_H4CNF_BOLTCUT", "-1");
                    TextBox_AppendText_MP("_H4CNF_TROJAN", "4");
                    TextBox_AppendText_MP("_H4_PLAYTHROUGH_STATUS", "10");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "佩里科岛抢劫-玩家x4/人均25%分红/困难模式/无精英/不拿保险柜/人均245W（粉钻x1+画作x7）")
                {
                    TextBox_AppendText_MP("_H4CNF_BS_GEN", "131071");
                    TextBox_AppendText_MP("_H4CNF_BS_ENTR", "63");
                    TextBox_AppendText_MP("_H4CNF_BS_ABIL", "63");
                    TextBox_AppendText_MP("_H4CNF_APPROACH", "-1");
                    TextBox_AppendText_MP("_H4_PROGRESS", "131055");
                    TextBox_AppendText_MP("_H4CNF_TARGET", "3");
                    TextBox_AppendText_MP("_H4LOOT_PAINT", "-1");
                    TextBox_AppendText_MP("_H4LOOT_PAINT_SCOPED", "-1");
                    TextBox_AppendText_MP("_H4LOOT_PAINT_V", "1213295");
                    TextBox_AppendText_MP("_H4_MISSIONS", "65535");
                    TextBox_AppendText_MP("_H4CNF_WEAPONS", "2");
                    TextBox_AppendText_MP("_H4CNF_WEP_DISRP", "3");
                    TextBox_AppendText_MP("_H4CNF_ARM_DISRP", "3");
                    TextBox_AppendText_MP("_H4CNF_HEL_DISRP", "3");
                    TextBox_AppendText_MP("_H4CNF_GRAPPEL", "-1");
                    TextBox_AppendText_MP("_H4CNF_UNIFORM", "-1");
                    TextBox_AppendText_MP("_H4CNF_BOLTCUT", "-1");
                    TextBox_AppendText_MP("_H4CNF_TROJAN", "4");
                    TextBox_AppendText_MP("_H4_PLAYTHROUGH_STATUS", "10");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "地堡-重置地堡总营收")
                {
                    TextBox_AppendText_MP("_LIFETIME_BKR_SELL_EARNINGS5", "0");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "杂项-取消抢劫并重新开始")
                {
                    TextBox_AppendText_MP("_CAS_HEIST_NOTS", "-1");
                    TextBox_AppendText_MP("_CAS_HEIST_FLOW", "-1");
                }
                else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "赌场抢劫-解决侦察拍照发不了莱斯特")
                {
                    TextBox_AppendText_MP("_H3OPT_ACCESSPOINTS", "0");
                    TextBox_AppendText_MP("_H3OPT_POI", "0");
                }
                //else if (SelectedItemContent(ListBox_GTAHaxCode_FuncList) == "")
                //{
                //    TextBox_AppendText_MP("", "");                    
                //    TextBox_AppendText_NoMP("", "");
                //}

                TextBox_GTAHaxCodePreview.AppendText("\n");
            }));
        }

        /**********************************************************************************/

        private void Button_Create_HaxCode_Click(object sender, RoutedEventArgs e)
        {
            if (RadioButton_MPx.IsChecked == true)
            {
                GTAHax_MP = "$MPx";
            }
            else if (RadioButton_MP0.IsChecked == true)
            {
                GTAHax_MP = "$MP0";
            }
            else if (RadioButton_MP1.IsChecked == true)
            {
                GTAHax_MP = "$MP1";
            }

            TextBox_GTAHaxCodePreview.Clear();
            TextBox_GTAHaxCodePreview.AppendText("INT32");

            if (TabControl_Main.SelectedIndex == 1)
            {
                if (CheckBox_H3_COMPLETEDPOSIX.IsChecked == true)
                {
                    TextBox_AppendText_MP("_H3_COMPLETEDPOSIX", "-1");
                }

                if (CheckBox_Reset_P1P2.IsChecked == true)
                {
                    TextBox_AppendText_MP("_H3OPT_BITSET1", "0");
                    TextBox_AppendText_MP("_H3OPT_BITSET0", "0");
                    TextBox_AppendText_MP("_H3OPT_POI", "0");
                    TextBox_AppendText_MP("_H3OPT_ACCESSPOINTS", "0");
                    TextBox_AppendText_MP("_CAS_HEIST_FLOW", "0");
                }

                if (RadioButton_H3_P1.IsChecked == true)
                {
                    if (CheckBox_H3OPT_BITSET1.IsChecked == true)
                    {
                        TextBox_AppendText_MP("_H3OPT_BITSET1", "0");
                    }

                    if (SelectedItemContent(ListBox_H3OPT_APPROACH) == "不选择抢劫方式")
                    {

                    }
                    else if (SelectedItemContent(ListBox_H3OPT_APPROACH) == "隐迹潜踪")
                    {
                        TextBox_AppendText_MP("_H3OPT_APPROACH", "1");
                    }
                    else if (SelectedItemContent(ListBox_H3OPT_APPROACH) == "兵不厌诈")
                    {
                        TextBox_AppendText_MP("_H3OPT_APPROACH", "2");
                    }
                    else if (SelectedItemContent(ListBox_H3OPT_APPROACH) == "气势汹汹")
                    {
                        TextBox_AppendText_MP("_H3OPT_APPROACH", "3");
                    }

                    if (SelectedItemContent(ListBoxItem_H3OPT_TARGET) == "不选择抢劫物品")
                    {

                    }
                    else if (SelectedItemContent(ListBoxItem_H3OPT_TARGET) == "现金")
                    {
                        TextBox_AppendText_MP("_H3OPT_TARGET", "0");
                    }
                    else if (SelectedItemContent(ListBoxItem_H3OPT_TARGET) == "黄金")
                    {
                        TextBox_AppendText_MP("_H3OPT_TARGET", "1");
                    }
                    else if (SelectedItemContent(ListBoxItem_H3OPT_TARGET) == "艺术品")
                    {
                        TextBox_AppendText_MP("_H3OPT_TARGET", "2");
                    }
                    else if (SelectedItemContent(ListBoxItem_H3OPT_TARGET) == "钻石")
                    {
                        TextBox_AppendText_MP("_H3OPT_TARGET", "3");
                    }

                    if (CheckBox_H3OPT_ACCESSPOINTS.IsChecked == true)
                    {
                        TextBox_AppendText_MP("_H3OPT_ACCESSPOINTS", "-1");
                    }

                    if (CheckBox_H3OPT_POI.IsChecked == true)
                    {
                        TextBox_AppendText_MP("_H3OPT_POI", "-1");
                    }

                    if (CheckBox_H3OPT_BITSET1_1.IsChecked == true)
                    {
                        TextBox_AppendText_MP("_H3OPT_BITSET1", "-1");
                    }
                }
                else if (RadioButton_H3_P2.IsChecked == true)
                {
                    if (CheckBox_H3OPT_BITSET0.IsChecked == true)
                    {
                        TextBox_AppendText_MP("_H3OPT_BITSET0", "0");
                    }

                    //////////////////////////////////////

                    if (SelectedItemContent(ListBoxItem_H3OPT_VEH) == "不选择逃亡载具")
                    {

                    }
                    else if (SelectedItemContent(ListBoxItem_H3OPT_VEH) == "载具类型一")
                    {
                        TextBox_AppendText_MP("_H3OPT_VEHS", "0");
                    }
                    else if (SelectedItemContent(ListBoxItem_H3OPT_VEH) == "载具类型二")
                    {
                        TextBox_AppendText_MP("_H3OPT_VEHS", "1");
                    }
                    else if (SelectedItemContent(ListBoxItem_H3OPT_VEH) == "载具类型三")
                    {
                        TextBox_AppendText_MP("_H3OPT_VEHS", "2");
                    }
                    else if (SelectedItemContent(ListBoxItem_H3OPT_VEH) == "载具类型四")
                    {
                        TextBox_AppendText_MP("_H3OPT_VEHS", "3");
                    }

                    //////////////////////////////////////

                    if (SelectedItemContent(ListBoxItem_H3OPT_WEAPS) == "不选择武器类型")
                    {

                    }
                    else if (SelectedItemContent(ListBoxItem_H3OPT_WEAPS) == "武器类型一")
                    {
                        TextBox_AppendText_MP("_H3OPT_WEAPS", "0");
                    }
                    else if (SelectedItemContent(ListBoxItem_H3OPT_WEAPS) == "武器类型二")
                    {
                        TextBox_AppendText_MP("_H3OPT_WEAPS", "1");
                    }
                    else if (SelectedItemContent(ListBoxItem_H3OPT_WEAPS) == "武器类型三")
                    {
                        TextBox_AppendText_MP("_H3OPT_WEAPS", "2");
                    }
                    else if (SelectedItemContent(ListBoxItem_H3OPT_WEAPS) == "武器类型四")
                    {
                        TextBox_AppendText_MP("_H3OPT_WEAPS", "3");
                    }
                    else if (SelectedItemContent(ListBoxItem_H3OPT_WEAPS) == "武器类型五")
                    {
                        TextBox_AppendText_MP("_H3OPT_WEAPS", "4");
                    }

                    //////////////////////////////////////

                    if (CheckBox_H3OPT_DISRUPTSHIP.IsChecked == true)
                    {
                        TextBox_AppendText_MP("_H3OPT_DISRUPTSHIP", "3");
                    }

                    if (CheckBox_H3OPT_KEYLEVELS.IsChecked == true)
                    {
                        TextBox_AppendText_MP("_H3OPT_KEYLEVELS", "2");
                    }

                    //////////////////////////////////////

                    if (SelectedItemContent(ListBoxItem_H3OPT_CREWWEAP) == "不选择枪手")
                    {

                    }
                    else if (SelectedItemContent(ListBoxItem_H3OPT_CREWWEAP) == "卡尔·阿不拉季（5％分红）")
                    {
                        TextBox_AppendText_MP("_H3OPT_CREWWEAP", "1");
                    }
                    else if (SelectedItemContent(ListBoxItem_H3OPT_CREWWEAP) == "古斯塔沃·莫塔（9％分红）")
                    {
                        TextBox_AppendText_MP("_H3OPT_CREWWEAP", "2");
                    }
                    else if (SelectedItemContent(ListBoxItem_H3OPT_CREWWEAP) == "查理·里德（7％分红）")
                    {
                        TextBox_AppendText_MP("_H3OPT_CREWWEAP", "3");
                    }
                    else if (SelectedItemContent(ListBoxItem_H3OPT_CREWWEAP) == "切斯特·麦考伊（10％分红）")
                    {
                        TextBox_AppendText_MP("_H3OPT_CREWWEAP", "4");
                    }
                    else if (SelectedItemContent(ListBoxItem_H3OPT_CREWWEAP) == "帕里克·麦克瑞利（8％分红）")
                    {
                        TextBox_AppendText_MP("_H3OPT_CREWWEAP", "5");
                    }
                    else if (SelectedItemContent(ListBoxItem_H3OPT_CREWWEAP) == "枪手零分红")
                    {
                        TextBox_AppendText_MP("_H3OPT_CREWWEAP", "6");
                    }

                    //////////////////////////////////////

                    if (SelectedItemContent(ListBoxItem_H3OPT_CREWDRIVER) == "不选择车手")
                    {

                    }
                    else if (SelectedItemContent(ListBoxItem_H3OPT_CREWDRIVER) == "卡里姆·登茨（5％分红）")
                    {
                        TextBox_AppendText_MP("_H3OPT_CREWDRIVER", "1");
                    }
                    else if (SelectedItemContent(ListBoxItem_H3OPT_CREWDRIVER) == "塔丽娜·马丁内斯（7％分红）")
                    {
                        TextBox_AppendText_MP("_H3OPT_CREWDRIVER", "2");
                    }
                    else if (SelectedItemContent(ListBoxItem_H3OPT_CREWDRIVER) == "淘艾迪（9％分红）")
                    {
                        TextBox_AppendText_MP("_H3OPT_CREWDRIVER", "3");
                    }
                    else if (SelectedItemContent(ListBoxItem_H3OPT_CREWDRIVER) == "扎克·尼尔森（6％分红）")
                    {
                        TextBox_AppendText_MP("_H3OPT_CREWDRIVER", "4");
                    }
                    else if (SelectedItemContent(ListBoxItem_H3OPT_CREWDRIVER) == "切斯特·麦考伊（10％分红）")
                    {
                        TextBox_AppendText_MP("_H3OPT_CREWDRIVER", "5");
                    }
                    else if (SelectedItemContent(ListBoxItem_H3OPT_CREWDRIVER) == "车手零分红")
                    {
                        TextBox_AppendText_MP("_H3OPT_CREWDRIVER", "6");
                    }

                    //////////////////////////////////////

                    if (SelectedItemContent(ListBoxItem_H3OPT_CREWHACKER) == "不选择黑客")
                    {

                    }
                    else if (SelectedItemContent(ListBoxItem_H3OPT_CREWHACKER) == "里奇·卢肯斯（3％分红）")
                    {
                        TextBox_AppendText_MP("_H3OPT_CREWHACKER", "1");
                    }
                    else if (SelectedItemContent(ListBoxItem_H3OPT_CREWHACKER) == "克里斯汀·费尔兹（7％分红）")
                    {
                        TextBox_AppendText_MP("_H3OPT_CREWHACKER", "2");
                    }
                    else if (SelectedItemContent(ListBoxItem_H3OPT_CREWHACKER) == "尤汗·布莱尔（5％分红）")
                    {
                        TextBox_AppendText_MP("_H3OPT_CREWHACKER", "3");
                    }
                    else if (SelectedItemContent(ListBoxItem_H3OPT_CREWHACKER) == "阿维·施瓦茨曼（10％分红）")
                    {
                        TextBox_AppendText_MP("_H3OPT_CREWHACKER", "4");
                    }
                    else if (SelectedItemContent(ListBoxItem_H3OPT_CREWHACKER) == "佩奇·哈里斯（9％分红）")
                    {
                        TextBox_AppendText_MP("_H3OPT_CREWHACKER", "5");
                    }
                    else if (SelectedItemContent(ListBoxItem_H3OPT_CREWHACKER) == "黑客零分红")
                    {
                        TextBox_AppendText_MP("_H3OPT_CREWHACKER", "6");
                    }

                    //////////////////////////////////////

                    if (CheckBox_H3OPT_BITSET0_0.IsChecked == true)
                    {
                        TextBox_AppendText_MP("_H3OPT_BITSET0", "-1");
                    }
                }

                TextBox_GTAHaxCodePreview.AppendText("\n");
                //MessageBox.Show("生成Hax代码成功", " 提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (TabControl_Main.SelectedIndex == 2)
            {
                if (RadioButton_H4CNF_P1.IsChecked == true)
                {
                    if (CheckBox_H4CNF_BS_GEN.IsChecked == true)
                    {
                        TextBox_AppendText_MP("_H4CNF_BS_GEN", "131071");
                    }
                    if (CheckBox_H4CNF_BS_ENTR.IsChecked == true)
                    {
                        TextBox_AppendText_MP("_H4CNF_BS_ENTR", "63");
                    }
                    if (CheckBox_H4CNF_BS_ABIL.IsChecked == true)
                    {
                        TextBox_AppendText_MP("_H4CNF_BS_ABIL", "63");
                    }
                    if (CheckBox_H4CNF_APPROACH.IsChecked == true)
                    {
                        TextBox_AppendText_MP("_H4CNF_APPROACH", "-1");
                    }

                    //////////////////////////

                    if (SelectedItemContent(ListBoxItem_H4_PROGRESS) == "未选择")
                    {

                    }
                    else if (SelectedItemContent(ListBoxItem_H4_PROGRESS) == "普通模式")
                    {
                        TextBox_AppendText_MP("_H4_PROGRESS", "126823");
                    }
                    else if (SelectedItemContent(ListBoxItem_H4_PROGRESS) == "困难模式")
                    {
                        TextBox_AppendText_MP("_H4_PROGRESS", "131055");
                    }

                    //////////////////////////

                    if (SelectedItemContent(ListBoxItem_H4CNF_TARGET) == "未选择")
                    {

                    }
                    else if (SelectedItemContent(ListBoxItem_H4CNF_TARGET) == "西西米托龙舌兰")
                    {
                        TextBox_AppendText_MP("_H4CNF_TARGET", "0");
                    }
                    else if (SelectedItemContent(ListBoxItem_H4CNF_TARGET) == "红宝石项链")
                    {
                        TextBox_AppendText_MP("_H4CNF_TARGET", "1");
                    }
                    else if (SelectedItemContent(ListBoxItem_H4CNF_TARGET) == "不记名债券")
                    {
                        TextBox_AppendText_MP("_H4CNF_TARGET", "2");
                    }
                    else if (SelectedItemContent(ListBoxItem_H4CNF_TARGET) == "粉钻")
                    {
                        TextBox_AppendText_MP("_H4CNF_TARGET", "3");
                    }
                    else if (SelectedItemContent(ListBoxItem_H4CNF_TARGET) == "玛德拉索文件")
                    {
                        TextBox_AppendText_MP("_H4CNF_TARGET", "4");
                    }
                    else if (SelectedItemContent(ListBoxItem_H4CNF_TARGET) == "猎豹雕像")
                    {
                        TextBox_AppendText_MP("_H4CNF_TARGET", "5");
                    }

                    //////////////////////////

                    if (SelectedItemContent(ListBoxItem_H4LOOT) == "未选择")
                    {

                    }

                    /**************************** 现金 ****************************/
                    if (SelectedItemContent(ListBoxItem_H4LOOT) == "已发现/侦察 现金（室内/室外）")
                    {
                        TextBox_AppendText_MP("_H4LOOT_CASH_I", "-1");
                        TextBox_AppendText_MP("_H4LOOT_CASH_C", "-1");
                        TextBox_AppendText_MP("_H4LOOT_CASH_I_SCOPED", "-1");
                        TextBox_AppendText_MP("_H4LOOT_CASH_C_SCOPED", "-1");
                        TextBox_AppendText_MP("_H4LOOT_CASH_V", "90000");
                    }
                    else
                    {
                        if (CheckBox_H4LOOT_Random.IsChecked == false && Expander_H4LOOT.IsExpanded == true)
                        {
                            TextBox_AppendText_MP("_H4LOOT_CASH_I", "0");
                            TextBox_AppendText_MP("_H4LOOT_CASH_C", "0");
                            TextBox_AppendText_MP("_H4LOOT_CASH_I_SCOPED", "0");
                            TextBox_AppendText_MP("_H4LOOT_CASH_C_SCOPED", "0");
                            //TextBox_AppendText_MP("_H4LOOT_CASH_V", "0");
                        }
                    }

                    /**************************** 大麻 ****************************/
                    if (SelectedItemContent(ListBoxItem_H4LOOT) == "已发现/侦察 大麻（室内/室外）")
                    {
                        TextBox_AppendText_MP("_H4LOOT_WEED_I", "-1");
                        TextBox_AppendText_MP("_H4LOOT_WEED_C", "-1");
                        TextBox_AppendText_MP("_H4LOOT_WEED_I_SCOPED", "-1");
                        TextBox_AppendText_MP("_H4LOOT_WEED_C_SCOPED", "-1");
                        TextBox_AppendText_MP("_H4LOOT_WEED_V", "145000");
                    }
                    else
                    {
                        if (CheckBox_H4LOOT_Random.IsChecked == false && Expander_H4LOOT.IsExpanded == true)
                        {
                            TextBox_AppendText_MP("_H4LOOT_WEED_I", "0");
                            TextBox_AppendText_MP("_H4LOOT_WEED_C", "0");
                            TextBox_AppendText_MP("_H4LOOT_WEED_I_SCOPED", "0");
                            TextBox_AppendText_MP("_H4LOOT_WEED_C_SCOPED", "0");
                            //TextBox_AppendText_MP("_H4LOOT_WEED_V", "0");
                        }
                    }

                    /**************************** 可可 ****************************/
                    if (SelectedItemContent(ListBoxItem_H4LOOT) == "已发现/侦察 可可（室内/室外）")
                    {
                        TextBox_AppendText_MP("_H4LOOT_COKE_I", "-1");
                        TextBox_AppendText_MP("_H4LOOT_COKE_C", "-1");
                        TextBox_AppendText_MP("_H4LOOT_COKE_I_SCOPED", "-1");
                        TextBox_AppendText_MP("_H4LOOT_COKE_C_SCOPED", "-1");
                        TextBox_AppendText_MP("_H4LOOT_COKE_V", "220000");
                    }
                    else
                    {
                        if (CheckBox_H4LOOT_Random.IsChecked == false && Expander_H4LOOT.IsExpanded == true)
                        {
                            TextBox_AppendText_MP("_H4LOOT_COKE_I", "0");
                            TextBox_AppendText_MP("_H4LOOT_COKE_C", "0");
                            TextBox_AppendText_MP("_H4LOOT_COKE_I_SCOPED", "0");
                            TextBox_AppendText_MP("_H4LOOT_COKE_C_SCOPED", "0");
                            //TextBox_AppendText_MP("_H4LOOT_COKE_V", "0");
                        }
                    }

                    /**************************** 黄金 ****************************/
                    if (SelectedItemContent(ListBoxItem_H4LOOT) == "已发现/侦察 黄金（室内/室外）")
                    {
                        TextBox_AppendText_MP("_H4LOOT_GOLD_I", "-1");
                        TextBox_AppendText_MP("_H4LOOT_GOLD_C", "-1");
                        TextBox_AppendText_MP("_H4LOOT_GOLD_I_SCOPED", "-1");
                        TextBox_AppendText_MP("_H4LOOT_GOLD_C_SCOPED", "-1");
                        TextBox_AppendText_MP("_H4LOOT_GOLD_V", "320000");
                    }
                    else
                    {
                        if (CheckBox_H4LOOT_Random.IsChecked == false && Expander_H4LOOT.IsExpanded == true)
                        {
                            TextBox_AppendText_MP("_H4LOOT_GOLD_I", "0");
                            TextBox_AppendText_MP("_H4LOOT_GOLD_C", "0");
                            TextBox_AppendText_MP("_H4LOOT_GOLD_I_SCOPED", "0");
                            TextBox_AppendText_MP("_H4LOOT_GOLD_C_SCOPED", "0");
                            //TextBox_AppendText_MP("_H4LOOT_GOLD_V", "0");
                        }
                    }

                    //////////////////////////

                    /**************************** 画作 ****************************/
                    if (SelectedItemContent(ListBoxItem_H4LOOT_PAINT) == "已发现/侦察 画作（室内/室外）")
                    {
                        TextBox_AppendText_MP("_H4LOOT_PAINT", "-1");
                        TextBox_AppendText_MP("_H4LOOT_PAINT_SCOPED", "-1");
                        TextBox_AppendText_MP("_H4LOOT_PAINT_V", "180000");
                    }
                    else
                    {
                        if (CheckBox_H4LOOT_Random.IsChecked == false && Expander_H4LOOT.IsExpanded == true)
                        {
                            TextBox_AppendText_MP("_H4LOOT_PAINT", "0");
                            TextBox_AppendText_MP("_H4LOOT_PAINT_SCOPED", "0");
                            //TextBox_AppendText_MP("_H4LOOT_PAINT_V", "0");
                        }
                    }

                    //////////////////////////

                    if (SelectedItemContent(ListBoxItem_H4_MISSIONS) == "未选择")
                    {

                    }
                    else if (SelectedItemContent(ListBoxItem_H4_MISSIONS) == "潜水艇：虎鲸")
                    {
                        TextBox_AppendText_MP("_H4_MISSIONS", "65283");
                    }
                    else if (SelectedItemContent(ListBoxItem_H4_MISSIONS) == "飞机：阿尔科诺斯特")
                    {
                        TextBox_AppendText_MP("_H4_MISSIONS", "65413");
                    }
                    else if (SelectedItemContent(ListBoxItem_H4_MISSIONS) == "飞机：梅杜莎")
                    {
                        TextBox_AppendText_MP("_H4_MISSIONS", "65289");
                    }
                    else if (SelectedItemContent(ListBoxItem_H4_MISSIONS) == "直升机：隐形歼灭者")
                    {
                        TextBox_AppendText_MP("_H4_MISSIONS", "65425");
                    }
                    else if (SelectedItemContent(ListBoxItem_H4_MISSIONS) == "船只：巡逻艇")
                    {
                        TextBox_AppendText_MP("_H4_MISSIONS", "65313");
                    }
                    else if (SelectedItemContent(ListBoxItem_H4_MISSIONS) == "船只：长鳍")
                    {
                        TextBox_AppendText_MP("_H4_MISSIONS", "65345");
                    }
                    else if (SelectedItemContent(ListBoxItem_H4_MISSIONS) == "全部载具可用")
                    {
                        TextBox_AppendText_MP("_H4_MISSIONS", "65535");
                    }

                    //////////////////////////

                    if (SelectedItemContent(ListBoxItem_H4CNF_WEAPONS) == "未选择")
                    {

                    }
                    else if (SelectedItemContent(ListBoxItem_H4CNF_WEAPONS) == "侵略者（连发散弹，连发手枪，手雷，砍刀）")
                    {
                        TextBox_AppendText_MP("_H4CNF_WEAPONS", "1");
                    }
                    else if (SelectedItemContent(ListBoxItem_H4CNF_WEAPONS) == "阴谋者（军用步枪，单发手枪，粘弹，指虎）")
                    {
                        TextBox_AppendText_MP("_H4CNF_WEAPONS", "2");
                    }
                    else if (SelectedItemContent(ListBoxItem_H4CNF_WEAPONS) == "神枪手（轻狙，连发手枪，燃烧瓶，小刀）")
                    {
                        TextBox_AppendText_MP("_H4CNF_WEAPONS", "3");
                    }
                    else if (SelectedItemContent(ListBoxItem_H4CNF_WEAPONS) == "破坏者（MK2冲锋枪，单发手枪，土质炸弹，小刀）")
                    {
                        TextBox_AppendText_MP("_H4CNF_WEAPONS", "4");
                    }
                    else if (SelectedItemContent(ListBoxItem_H4CNF_WEAPONS) == "神射手（MK2突击步枪，单发手枪，土质炸弹，砍刀）")
                    {
                        TextBox_AppendText_MP("_H4CNF_WEAPONS", "5");
                    }

                    //////////////////////////

                    if (CheckBox_H4CNF_WEP_DISRP.IsChecked == true)
                    {
                        TextBox_AppendText_MP("_H4CNF_WEP_DISRP", "3");
                    }
                    if (CheckBox_H4CNF_ARM_DISRP.IsChecked == true)
                    {
                        TextBox_AppendText_MP("_H4CNF_ARM_DISRP", "3");
                    }
                    if (CheckBox_H4CNF_HEL_DISRP.IsChecked == true)
                    {
                        TextBox_AppendText_MP("_H4CNF_HEL_DISRP", "3");
                    }

                    //////////////////////////

                    if (CheckBox_H4CNF_GRAPPEL.IsChecked == true)
                    {
                        TextBox_AppendText_MP("_H4CNF_GRAPPEL", "-1");
                    }
                    if (CheckBox_H4CNF_UNIFORM.IsChecked == true)
                    {
                        TextBox_AppendText_MP("_H4CNF_UNIFORM", "-1");
                    }
                    if (CheckBox_H4CNF_BOLTCUT.IsChecked == true)
                    {
                        TextBox_AppendText_MP("_H4CNF_BOLTCUT", "-1");
                    }

                    //////////////////////////

                    if (SelectedItemContent(ListBoxItem_H4CNF_TROJAN) == "未选择")
                    {

                    }
                    else if (SelectedItemContent(ListBoxItem_H4CNF_TROJAN) == "机场")
                    {
                        TextBox_AppendText_MP("_H4CNF_TROJAN", "1");
                    }
                    else if (SelectedItemContent(ListBoxItem_H4CNF_TROJAN) == "北船坞")
                    {
                        TextBox_AppendText_MP("_H4CNF_TROJAN", "2");
                    }
                    else if (SelectedItemContent(ListBoxItem_H4CNF_TROJAN) == "主码头-东")
                    {
                        TextBox_AppendText_MP("_H4CNF_TROJAN", "3");
                    }
                    else if (SelectedItemContent(ListBoxItem_H4CNF_TROJAN) == "主码头-西")
                    {
                        TextBox_AppendText_MP("_H4CNF_TROJAN", "4");
                    }
                    else if (SelectedItemContent(ListBoxItem_H4CNF_TROJAN) == "混合粉")
                    {
                        TextBox_AppendText_MP("_H4CNF_TROJAN", "5");
                    }

                    //////////////////////////

                    if (CheckBox_H4_PLAYTHROUGH_STATUS.IsChecked == true)
                    {
                        TextBox_AppendText_MP("_H4_PLAYTHROUGH_STATUS", "10");
                    }
                }
                else if (RadioButton_H4CNF_P2.IsChecked == true)
                {
                    if (CheckBox_ResetEverything.IsChecked == true)
                    {
                        TextBox_AppendText_MP("_H4_MISSIONS", "0");
                        TextBox_AppendText_MP("_H4_PROGRESS", "0");
                        TextBox_AppendText_MP("_H4_PLAYTHROUGH_STATUS", "0");
                        TextBox_AppendText_MP("_H4CNF_APPROACH", "0");
                        TextBox_AppendText_MP("_H4CNF_BS_ENTR", "0");
                        TextBox_AppendText_MP("_H4CNF_BS_GEN", "0");
                    }
                    if (CheckBox_H4_COOLDOWN.IsChecked == true)
                    {
                        TextBox_AppendText_MP("_H4_COOLDOWN", "0");
                    }
                    if (CheckBox_H4_COOLDOWN_HARD.IsChecked == true)
                    {
                        TextBox_AppendText_MP("_H4_COOLDOWN_HARD", "0");
                    }
                }

                TextBox_GTAHaxCodePreview.AppendText("\n");
                //MessageBox.Show("生成Hax代码成功", " 提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void TabControl_Main_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                if (TabControl_Main.SelectedIndex == 0)
                {
                    Button_Create_HaxCode.IsEnabled = false;
                }
                else
                {
                    Button_Create_HaxCode.IsEnabled = true;
                }
            }));
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            ProcessUtil.OpenLink(e.Uri.OriginalString);
            e.Handled = true;
        }

        private bool IsInputNumber(KeyEventArgs e)
        {
            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
               e.Key == Key.Delete || e.Key == Key.Back || e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Tab)
            {
                // 按下了Alt、ctrl、shift等修饰键
                if (e.KeyboardDevice.Modifiers != ModifierKeys.None)
                {
                    e.Handled = true;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                // 按下了字符等其它功能键
                e.Handled = true;
            }
            return false;
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            IsInputNumber(e);
        }

        private void CheckBox_H4CNF_P1_Click(object sender, RoutedEventArgs e)
        {
            if (RadioButton_H4CNF_P1.IsChecked == true)
            {
                RadioButton_H4CNF_P2.IsChecked = false;
            }
            else
            {
                RadioButton_H4CNF_P2.IsChecked = true;
            }
        }

        private void CheckBox_H4CNF_P2_Click(object sender, RoutedEventArgs e)
        {
            if (RadioButton_H4CNF_P2.IsChecked == true)
            {
                RadioButton_H4CNF_P1.IsChecked = false;
            }
            else
            {
                RadioButton_H4CNF_P1.IsChecked = true;
            }
        }

        private void RadioButton_H3_P1_Click(object sender, RoutedEventArgs e)
        {
            if (RadioButton_H3_P1.IsChecked == true)
            {
                RadioButton_H3_P2.IsChecked = false;
            }
            else
            {
                RadioButton_H3_P2.IsChecked = true;
            }
        }

        private void RadioButton_H3_P2_Click(object sender, RoutedEventArgs e)
        {
            if (RadioButton_H3_P2.IsChecked == true)
            {
                RadioButton_H3_P1.IsChecked = false;
            }
            else
            {
                RadioButton_H3_P1.IsChecked = true;
            }
        }
    }
}
