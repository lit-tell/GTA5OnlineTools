using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Data;

namespace GTA5OnlineTools.Modules.Windows.ExternalMenu
{
    /// <summary>
    /// EM4OnlineOptionView.xaml 的交互逻辑
    /// </summary>
    public partial class EM4OnlineOptionView : UserControl
    {
        public EM4OnlineOptionView()
        {
            InitializeComponent();

            ExternalMenuView.ClosingDisposeEvent += ExternalMenuView_ClosingDisposeEvent;
        }

        private void ExternalMenuView_ClosingDisposeEvent()
        {
            
        }

        private void CheckBox_RemovePassiveModeCooldown_Click(object sender, RoutedEventArgs e)
        {
            Online.RemovePassiveModeCooldown(CheckBox_RemovePassiveModeCooldown.IsChecked == true);
        }

        private void CheckBox_RemoveSuicideCooldown_Click(object sender, RoutedEventArgs e)
        {
            Online.RemoveSuicideCooldown(CheckBox_RemoveSuicideCooldown.IsChecked == true);
        }

        private void CheckBox_DisableOrbitalCooldown_Click(object sender, RoutedEventArgs e)
        {
            Online.DisableOrbitalCooldown(CheckBox_DisableOrbitalCooldown.IsChecked == true);
        }

        private void CheckBox_OffRadar_Click(object sender, RoutedEventArgs e)
        {
            Online.OffRadar(CheckBox_OffRadar.IsChecked == true);
        }

        private void CheckBox_GhostOrganization_Click(object sender, RoutedEventArgs e)
        {
            Online.GhostOrganization(CheckBox_GhostOrganization.IsChecked == true);
        }

        private void CheckBox_BribeOrBlindCops_Click(object sender, RoutedEventArgs e)
        {
            Online.BribeOrBlindCops(CheckBox_BribeOrBlindCops.IsChecked == true);
        }

        private void CheckBox_BribeAuthorities_Click(object sender, RoutedEventArgs e)
        {
            Online.BribeAuthorities(CheckBox_BribeAuthorities.IsChecked == true);
        }

        private void CheckBox_RevealPlayers_Click(object sender, RoutedEventArgs e)
        {
            Online.RevealPlayers(CheckBox_RevealPlayers.IsChecked == true);
        }

        private void CheckBox_AllowSellOnNonPublic_Click(object sender, RoutedEventArgs e)
        {
            Online.AllowSellOnNonPublic(CheckBox_AllowSellOnNonPublic.IsChecked == true);
        }

        private void CheckBox_OnlineSnow_Click(object sender, RoutedEventArgs e)
        {
            Online.SessionSnow(CheckBox_OnlineSnow.IsChecked == true);
        }

        private void Button_Blips_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            var str = (e.OriginalSource as Button).Content.ToString();

            int index = MiscData.Blips.FindIndex(t => t.Name == str);
            if (index != -1)
            {
                Teleport.ToBlips(MiscData.Blips[index].ID);
            }
        }

        private void Button_MerryweatherServices_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            var str = (e.OriginalSource as Button).Content.ToString();

            int index = MiscData.MerryweatherServices.FindIndex(t => t.Name == str);
            if (index != -1)
            {
                Online.MerryweatherServices(MiscData.MerryweatherServices[index].ID);
            }
        }

        private void CheckBox_InstantBullShark_Click(object sender, RoutedEventArgs e)
        {
            Online.InstantBullShark(CheckBox_InstantBullShark.IsChecked == true);
        }

        private void CheckBox_BackupHeli_Click(object sender, RoutedEventArgs e)
        {
            Online.BackupHeli(CheckBox_BackupHeli.IsChecked == true);
        }

        private void CheckBox_Airstrike_Click(object sender, RoutedEventArgs e)
        {
            Online.Airstrike(CheckBox_Airstrike.IsChecked == true);
        }
    }
}
