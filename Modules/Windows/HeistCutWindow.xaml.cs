using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Modules.Windows
{
    /// <summary>
    /// HeistCutWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HeistCutWindow : Window
    {
        public HeistCutWindow()
        {
            InitializeComponent();
        }

        private void Window_HeistCut_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                Memory.Initialize(CoreUtil.TargetAppName);

                Globals.TempPTR = Memory.FindPattern(Offsets.Mask.GlobalMask);
                Globals.GlobalPTR = Memory.Rip_37(Globals.TempPTR);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    ReadHeistCutData();
                });
            });
        }

        private void Window_HeistCut_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Button_Read_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            ReadHeistCutData();
        }

        private void Button_Write_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            try
            {
                if (TextBox_Cayo_Player1.Text != "" &&
                    TextBox_Cayo_Player2.Text != "" &&
                    TextBox_Cayo_Player3.Text != "" &&
                    TextBox_Cayo_Player4.Text != "" &&

                    TextBox_Cayo_Tequila.Text != "" &&
                    TextBox_Cayo_RubyNecklace.Text != "" &&
                    TextBox_Cayo_BearerBonds.Text != "" &&
                    TextBox_Cayo_PinkDiamond.Text != "" &&
                    TextBox_Cayo_MadrazoFiles.Text != "" &&
                    TextBox_Cayo_BlackPanther.Text != "" &&

                    TextBox_Cayo_LocalBagSize.Text != "" &&

                    TextBox_Cayo_FencingFee.Text != "" &&
                    TextBox_Cayo_PavelCut.Text != "" &&

                    TextBox_Casino_Player1.Text != "" &&
                    TextBox_Casino_Player2.Text != "" &&
                    TextBox_Casino_Player3.Text != "" &&
                    TextBox_Casino_Player4.Text != "" &&

                    TextBox_Casino_Lester.Text != "" &&

                    TextBox_CasinoPotential_Money.Text != "" &&
                    TextBox_CasinoPotential_Artwork.Text != "" &&
                    TextBox_CasinoPotential_Gold.Text != "" &&
                    TextBox_CasinoPotential_Diamonds.Text != "" &&

                    TextBox_CasinoAI_1.Text != "" &&
                    TextBox_CasinoAI_2.Text != "" &&
                    TextBox_CasinoAI_3.Text != "" &&
                    TextBox_CasinoAI_4.Text != "" &&
                    TextBox_CasinoAI_5.Text != "" &&

                    TextBox_CasinoAI_6.Text != "" &&
                    TextBox_CasinoAI_7.Text != "" &&
                    TextBox_CasinoAI_8.Text != "" &&
                    TextBox_CasinoAI_9.Text != "" &&
                    TextBox_CasinoAI_10.Text != "" &&

                    TextBox_CasinoAI_11.Text != "" &&
                    TextBox_CasinoAI_12.Text != "" &&
                    TextBox_CasinoAI_13.Text != "" &&
                    TextBox_CasinoAI_14.Text != "" &&
                    TextBox_CasinoAI_15.Text != "" &&

                    TextBox_Doomsday_Player1.Text != "" &&
                    TextBox_Doomsday_Player2.Text != "" &&
                    TextBox_Doomsday_Player3.Text != "" &&
                    TextBox_Doomsday_Player4.Text != "" &&

                    TextBox_Doomsday_ActI.Text != "" &&
                    TextBox_Doomsday_ActII.Text != "" &&
                    TextBox_Doomsday_ActIII.Text != "" &&

                    TextBox_Apart_Player1.Text != "" &&
                    TextBox_Apart_Player2.Text != "" &&
                    TextBox_Apart_Player3.Text != "" &&
                    TextBox_Apart_Player4.Text != "" &&

                    TextBox_Apart_Fleeca.Text != "" &&
                    TextBox_Apart_PrisonBreak.Text != "" &&
                    TextBox_Apart_HumaneLabs.Text != "" &&
                    TextBox_Apart_SeriesA.Text != "" &&
                    TextBox_Apart_PacificStandard.Text != "")
                {
                    Hacks.WriteGA<int>(1973525 + 823 + 56 + 1, Convert.ToInt32(TextBox_Cayo_Player1.Text));
                    Hacks.WriteGA<int>(1973525 + 823 + 56 + 2, Convert.ToInt32(TextBox_Cayo_Player2.Text));
                    Hacks.WriteGA<int>(1973525 + 823 + 56 + 3, Convert.ToInt32(TextBox_Cayo_Player3.Text));
                    Hacks.WriteGA<int>(1973525 + 823 + 56 + 4, Convert.ToInt32(TextBox_Cayo_Player4.Text));

                    Hacks.WriteGA<int>(262145 + 29632, Convert.ToInt32(TextBox_Cayo_Tequila.Text));
                    Hacks.WriteGA<int>(262145 + 29633, Convert.ToInt32(TextBox_Cayo_RubyNecklace.Text));
                    Hacks.WriteGA<int>(262145 + 29634, Convert.ToInt32(TextBox_Cayo_BearerBonds.Text));
                    Hacks.WriteGA<int>(262145 + 29635, Convert.ToInt32(TextBox_Cayo_PinkDiamond.Text));
                    Hacks.WriteGA<int>(262145 + 29636, Convert.ToInt32(TextBox_Cayo_MadrazoFiles.Text));
                    Hacks.WriteGA<int>(262145 + 29637, Convert.ToInt32(TextBox_Cayo_BlackPanther.Text));

                    Hacks.WriteGA<int>(262145 + 29395, Convert.ToInt32(TextBox_Cayo_LocalBagSize.Text));

                    Hacks.WriteGA<float>(262145 + 29641, Convert.ToSingle(TextBox_Cayo_FencingFee.Text));
                    Hacks.WriteGA<float>(262145 + 29642, Convert.ToSingle(TextBox_Cayo_PavelCut.Text));

                    //////////////////////////////////////////////////////////////////////////////////

                    Hacks.WriteGA<int>(1966739 + 2325 + 1, Convert.ToInt32(TextBox_Casino_Player1.Text));
                    Hacks.WriteGA<int>(1966739 + 2325 + 2, Convert.ToInt32(TextBox_Casino_Player2.Text));
                    Hacks.WriteGA<int>(1966739 + 2325 + 3, Convert.ToInt32(TextBox_Casino_Player3.Text));
                    Hacks.WriteGA<int>(1966739 + 2325 + 4, Convert.ToInt32(TextBox_Casino_Player4.Text));

                    Hacks.WriteGA<int>(262145 + 28455, Convert.ToInt32(TextBox_Casino_Lester.Text));

                    Hacks.WriteGA<int>(262145 + 28469, Convert.ToInt32(TextBox_CasinoPotential_Money.Text));
                    Hacks.WriteGA<int>(262145 + 28470, Convert.ToInt32(TextBox_CasinoPotential_Artwork.Text));
                    Hacks.WriteGA<int>(262145 + 28471, Convert.ToInt32(TextBox_CasinoPotential_Gold.Text));
                    Hacks.WriteGA<int>(262145 + 28472, Convert.ToInt32(TextBox_CasinoPotential_Diamonds.Text));

                    Hacks.WriteGA<int>(262145 + 28480 + 1, Convert.ToInt32(TextBox_CasinoAI_1.Text));
                    Hacks.WriteGA<int>(262145 + 28480 + 2, Convert.ToInt32(TextBox_CasinoAI_2.Text));
                    Hacks.WriteGA<int>(262145 + 28480 + 3, Convert.ToInt32(TextBox_CasinoAI_3.Text));
                    Hacks.WriteGA<int>(262145 + 28480 + 4, Convert.ToInt32(TextBox_CasinoAI_4.Text));
                    Hacks.WriteGA<int>(262145 + 28480 + 5, Convert.ToInt32(TextBox_CasinoAI_5.Text));

                    Hacks.WriteGA<int>(262145 + 28480 + 6, Convert.ToInt32(TextBox_CasinoAI_6.Text));
                    Hacks.WriteGA<int>(262145 + 28480 + 7, Convert.ToInt32(TextBox_CasinoAI_7.Text));
                    Hacks.WriteGA<int>(262145 + 28480 + 8, Convert.ToInt32(TextBox_CasinoAI_8.Text));
                    Hacks.WriteGA<int>(262145 + 28480 + 9, Convert.ToInt32(TextBox_CasinoAI_9.Text));
                    Hacks.WriteGA<int>(262145 + 28480 + 10, Convert.ToInt32(TextBox_CasinoAI_10.Text));

                    Hacks.WriteGA<int>(262145 + 28480 + 11, Convert.ToInt32(TextBox_CasinoAI_11.Text));
                    Hacks.WriteGA<int>(262145 + 28480 + 12, Convert.ToInt32(TextBox_CasinoAI_12.Text));
                    Hacks.WriteGA<int>(262145 + 28480 + 13, Convert.ToInt32(TextBox_CasinoAI_13.Text));
                    Hacks.WriteGA<int>(262145 + 28480 + 14, Convert.ToInt32(TextBox_CasinoAI_14.Text));
                    Hacks.WriteGA<int>(262145 + 28480 + 15, Convert.ToInt32(TextBox_CasinoAI_15.Text));

                    //////////////////////////////////////////////////////////////////////////////////

                    Hacks.WriteGA<int>(1962763 + 812 + 50 + 1, Convert.ToInt32(TextBox_Doomsday_Player1.Text));
                    Hacks.WriteGA<int>(1962763 + 812 + 50 + 2, Convert.ToInt32(TextBox_Doomsday_Player2.Text));
                    Hacks.WriteGA<int>(1962763 + 812 + 50 + 3, Convert.ToInt32(TextBox_Doomsday_Player3.Text));
                    Hacks.WriteGA<int>(1962763 + 812 + 50 + 4, Convert.ToInt32(TextBox_Doomsday_Player4.Text));

                    Hacks.WriteGA<int>(262145 + 8925, Convert.ToInt32(TextBox_Doomsday_ActI.Text));
                    Hacks.WriteGA<int>(262145 + 8926, Convert.ToInt32(TextBox_Doomsday_ActII.Text));
                    Hacks.WriteGA<int>(262145 + 8927, Convert.ToInt32(TextBox_Doomsday_ActIII.Text));

                    //////////////////////////////////////////////////////////////////////////////////

                    Hacks.WriteGA<int>(1934636 + 3008 + 1, Convert.ToInt32(TextBox_Apart_Player1.Text));
                    Hacks.WriteGA<int>(1934636 + 3008 + 2, Convert.ToInt32(TextBox_Apart_Player2.Text));
                    Hacks.WriteGA<int>(1934636 + 3008 + 3, Convert.ToInt32(TextBox_Apart_Player3.Text));
                    Hacks.WriteGA<int>(1934636 + 3008 + 4, Convert.ToInt32(TextBox_Apart_Player4.Text));

                    Hacks.WriteGA<int>(262145 + 8920, Convert.ToInt32(TextBox_Apart_Fleeca.Text));
                    Hacks.WriteGA<int>(262145 + 8921, Convert.ToInt32(TextBox_Apart_PrisonBreak.Text));
                    Hacks.WriteGA<int>(262145 + 8922, Convert.ToInt32(TextBox_Apart_HumaneLabs.Text));
                    Hacks.WriteGA<int>(262145 + 8923, Convert.ToInt32(TextBox_Apart_SeriesA.Text));
                    Hacks.WriteGA<int>(262145 + 8924, Convert.ToInt32(TextBox_Apart_PacificStandard.Text));

                    TextBox_Result.Text = $"数据写入成功";
                }
                else
                {
                    MessageBox.Show("部分数据写入时为空，请检查后重新写入", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MsgBoxUtil.ExceptionMsgBox(ex);
            }
        }

        private void ReadHeistCutData()
        {
            try
            {
                TextBox_Cayo_Player1.Text = Hacks.ReadGA<int>(1973525 + 823 + 56 + 1).ToString();
                TextBox_Cayo_Player2.Text = Hacks.ReadGA<int>(1973525 + 823 + 56 + 2).ToString();
                TextBox_Cayo_Player3.Text = Hacks.ReadGA<int>(1973525 + 823 + 56 + 3).ToString();
                TextBox_Cayo_Player4.Text = Hacks.ReadGA<int>(1973525 + 823 + 56 + 4).ToString();

                TextBox_Cayo_Tequila.Text = Hacks.ReadGA<int>(262145 + 29632).ToString();
                TextBox_Cayo_RubyNecklace.Text = Hacks.ReadGA<int>(262145 + 29633).ToString();
                TextBox_Cayo_BearerBonds.Text = Hacks.ReadGA<int>(262145 + 29634).ToString();
                TextBox_Cayo_PinkDiamond.Text = Hacks.ReadGA<int>(262145 + 29635).ToString();
                TextBox_Cayo_MadrazoFiles.Text = Hacks.ReadGA<int>(262145 + 29636).ToString();
                TextBox_Cayo_BlackPanther.Text = Hacks.ReadGA<int>(262145 + 29637).ToString();

                TextBox_Cayo_LocalBagSize.Text = Hacks.ReadGA<int>(262145 + 29395).ToString();

                TextBox_Cayo_FencingFee.Text = Hacks.ReadGA<float>(262145 + 29641).ToString();
                TextBox_Cayo_PavelCut.Text = Hacks.ReadGA<float>(262145 + 29642).ToString();

                //////////////////////////////////////////////////////////////////////////////////

                TextBox_Casino_Player1.Text = Hacks.ReadGA<int>(1966739 + 2325 + 1).ToString();
                TextBox_Casino_Player2.Text = Hacks.ReadGA<int>(1966739 + 2325 + 2).ToString();
                TextBox_Casino_Player3.Text = Hacks.ReadGA<int>(1966739 + 2325 + 3).ToString();
                TextBox_Casino_Player4.Text = Hacks.ReadGA<int>(1966739 + 2325 + 4).ToString();

                TextBox_Casino_Lester.Text = Hacks.ReadGA<int>(262145 + 28455).ToString();

                TextBox_CasinoPotential_Money.Text = Hacks.ReadGA<int>(262145 + 28469).ToString();
                TextBox_CasinoPotential_Artwork.Text = Hacks.ReadGA<int>(262145 + 28470).ToString();
                TextBox_CasinoPotential_Gold.Text = Hacks.ReadGA<int>(262145 + 28471).ToString();
                TextBox_CasinoPotential_Diamonds.Text = Hacks.ReadGA<int>(262145 + 28472).ToString();

                TextBox_CasinoAI_1.Text = Hacks.ReadGA<int>(262145 + 28480 + 1).ToString();
                TextBox_CasinoAI_2.Text = Hacks.ReadGA<int>(262145 + 28480 + 2).ToString();
                TextBox_CasinoAI_3.Text = Hacks.ReadGA<int>(262145 + 28480 + 3).ToString();
                TextBox_CasinoAI_4.Text = Hacks.ReadGA<int>(262145 + 28480 + 4).ToString();
                TextBox_CasinoAI_5.Text = Hacks.ReadGA<int>(262145 + 28480 + 5).ToString();

                TextBox_CasinoAI_6.Text = Hacks.ReadGA<int>(262145 + 28480 + 6).ToString();
                TextBox_CasinoAI_7.Text = Hacks.ReadGA<int>(262145 + 28480 + 7).ToString();
                TextBox_CasinoAI_8.Text = Hacks.ReadGA<int>(262145 + 28480 + 8).ToString();
                TextBox_CasinoAI_9.Text = Hacks.ReadGA<int>(262145 + 28480 + 9).ToString();
                TextBox_CasinoAI_10.Text = Hacks.ReadGA<int>(262145 + 28480 + 10).ToString();

                TextBox_CasinoAI_11.Text = Hacks.ReadGA<int>(262145 + 28480 + 11).ToString();
                TextBox_CasinoAI_12.Text = Hacks.ReadGA<int>(262145 + 28480 + 12).ToString();
                TextBox_CasinoAI_13.Text = Hacks.ReadGA<int>(262145 + 28480 + 13).ToString();
                TextBox_CasinoAI_14.Text = Hacks.ReadGA<int>(262145 + 28480 + 14).ToString();
                TextBox_CasinoAI_15.Text = Hacks.ReadGA<int>(262145 + 28480 + 15).ToString();

                //////////////////////////////////////////////////////////////////////////////////

                TextBox_Doomsday_Player1.Text = Hacks.ReadGA<int>(1962763 + 812 + 50 + 1).ToString();
                TextBox_Doomsday_Player2.Text = Hacks.ReadGA<int>(1962763 + 812 + 50 + 2).ToString();
                TextBox_Doomsday_Player3.Text = Hacks.ReadGA<int>(1962763 + 812 + 50 + 3).ToString();
                TextBox_Doomsday_Player4.Text = Hacks.ReadGA<int>(1962763 + 812 + 50 + 4).ToString();

                TextBox_Doomsday_ActI.Text = Hacks.ReadGA<int>(262145 + 8925).ToString();
                TextBox_Doomsday_ActII.Text = Hacks.ReadGA<int>(262145 + 8926).ToString();
                TextBox_Doomsday_ActIII.Text = Hacks.ReadGA<int>(262145 + 8927).ToString();

                //////////////////////////////////////////////////////////////////////////////////

                TextBox_Apart_Player1.Text = Hacks.ReadGA<int>(1934636 + 3008 + 1).ToString();
                TextBox_Apart_Player2.Text = Hacks.ReadGA<int>(1934636 + 3008 + 2).ToString();
                TextBox_Apart_Player3.Text = Hacks.ReadGA<int>(1934636 + 3008 + 3).ToString();
                TextBox_Apart_Player4.Text = Hacks.ReadGA<int>(1934636 + 3008 + 4).ToString();

                TextBox_Apart_Fleeca.Text = Hacks.ReadGA<int>(262145 + 8920).ToString();
                TextBox_Apart_PrisonBreak.Text = Hacks.ReadGA<int>(262145 + 8921).ToString();
                TextBox_Apart_HumaneLabs.Text = Hacks.ReadGA<int>(262145 + 8922).ToString();
                TextBox_Apart_SeriesA.Text = Hacks.ReadGA<int>(262145 + 8923).ToString();
                TextBox_Apart_PacificStandard.Text = Hacks.ReadGA<int>(262145 + 8924).ToString();

                TextBox_Result.Text = $"数据读取成功";
            }
            catch (Exception ex)
            {
                MsgBoxUtil.ExceptionMsgBox(ex);
            }
        }
    }
}
