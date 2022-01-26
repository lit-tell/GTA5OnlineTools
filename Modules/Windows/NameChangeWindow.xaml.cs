using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Controls;
using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.Core;
using GTA5OnlineTools.Features.SDK;

namespace GTA5OnlineTools.Modules.Windows
{
    /// <summary>
    /// NameChangeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NameChangeWindow : Window
    {
        public NameChangeWindow()
        {
            InitializeComponent();
        }

        private void Window_NameChange_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                Memory.Initialize(CoreUtil.TargetAppName);

                Offsets.Mask.TempPTR = Memory.FindPattern(Offsets.Mask.WorldPTR);
                Globals.WorldPTR = Memory.Rip_37(Offsets.Mask.TempPTR);

                Offsets.Mask.TempPTR = Memory.FindPattern(Offsets.Mask.PlayerNameChatPTR);
                Globals.PlayerNameChatterPTR = Memory.Rip_37(Offsets.Mask.TempPTR);

                Dispatcher.BeginInvoke(new Action(delegate
                {
                    ReadPlayerName();
                }));
            });
        }

        private void Window_NameChange_Closing(object sender, CancelEventArgs e)
        {
            
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            ProcessUtil.OpenLink(e.Uri.OriginalString);
            e.Handled = true;
        }

        private void Button_ReadPlayerName_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            ReadPlayerName();
        }

        private void Button_WritePlayerName_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            if (TextBox_OnlineList.Text != "" &&
                TextBox_ChatName.Text != "" &&
                TextBox_ExternalDisplay.Text != "")
            {
                Memory.WriteString(Globals.WorldPTR, Offsets.OnlineListPlayerName, TextBox_OnlineList.Text + "\0");
                Memory.WriteString(Globals.PlayerNameChatterPTR + 0x84, null, TextBox_ChatName.Text + "\0");

                if (RadioButton_PlayerName_Epic.IsChecked == true)
                {
                    Memory.WriteString(Memory.baseAddress + Offsets.PlayerNameDisPlay_Epic, null, TextBox_ExternalDisplay.Text + "\0");
                }
                else
                {
                    Memory.WriteString(Memory.baseAddress + Offsets.PlayerNameDisPlay_Steam, null, TextBox_ExternalDisplay.Text + "\0");
                }

                MessageBox.Show("写入成功，请切换战局生效",
                    "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("内容不能为空",
                    "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ReadPlayerName()
        {
            TextBox_OnlineList.Text = Memory.ReadString(Globals.WorldPTR, Offsets.OnlineListPlayerName, 20);
            TextBox_ChatName.Text = Memory.ReadString(Globals.PlayerNameChatterPTR + 0x84, null, 20);

            if (RadioButton_PlayerName_Epic.IsChecked == true)
            {
                TextBox_ExternalDisplay.Text = Memory.ReadString(Memory.baseAddress + Offsets.PlayerNameDisPlay_Epic, null, 20);
            }
            else
            {
                TextBox_ExternalDisplay.Text = Memory.ReadString(Memory.baseAddress + Offsets.PlayerNameDisPlay_Steam, null, 20);
            }
        }

        private void TextBox_OnlineList_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox_ChatName.Text = TextBox_OnlineList.Text;
            TextBox_ExternalDisplay.Text = TextBox_OnlineList.Text;
        }
    }
}
