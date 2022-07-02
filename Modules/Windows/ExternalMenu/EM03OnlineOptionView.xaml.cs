using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Data;
using static GTA5OnlineTools.Features.SDK.Hacks;

namespace GTA5OnlineTools.Modules.Windows.ExternalMenu;

/// <summary>
/// EM03OnlineOptionView.xaml 的交互逻辑
/// </summary>
public partial class EM03OnlineOptionView : UserControl
{
    private struct PVInfo
    {
        public int Index;
        public string Name;
        public long hash;
        public string plate;
    }

    private List<PVInfo> pVInfos = new List<PVInfo>();

    public EM03OnlineOptionView()
    {
        InitializeComponent();

        ExternalMenuView.ClosingDisposeEvent += ExternalMenuView_ClosingDisposeEvent;
    }

    private void ExternalMenuView_ClosingDisposeEvent()
    {
        
    }

    private void CheckBox_RemovePassiveModeCooldown_Click(object sender, RoutedEventArgs e)
    {
        Globals.remove_passive_mode_cooldown(CheckBox_RemovePassiveModeCooldown.IsChecked == true);
    }

    private void CheckBox_RemoveSuicideCooldown_Click(object sender, RoutedEventArgs e)
    {
        Globals.remove_suicide_cooldown(CheckBox_RemoveSuicideCooldown.IsChecked == true);
    }

    private void CheckBox_DisableOrbitalCooldown_Click(object sender, RoutedEventArgs e)
    {
        Globals.disable_orbital_cooldown(CheckBox_DisableOrbitalCooldown.IsChecked == true);
    }

    private void CheckBox_OffRadar_Click(object sender, RoutedEventArgs e)
    {
        Globals.off_radar(CheckBox_OffRadar.IsChecked == true);
    }

    private void CheckBox_GhostOrganization_Click(object sender, RoutedEventArgs e)
    {
        Globals.ghost_organization(CheckBox_GhostOrganization.IsChecked == true);
    }

    private void CheckBox_BribeOrBlindCops_Click(object sender, RoutedEventArgs e)
    {
        Globals.blind_cops(CheckBox_BribeOrBlindCops.IsChecked == true);
    }

    private void CheckBox_BribeAuthorities_Click(object sender, RoutedEventArgs e)
    {
        Globals.bribe_cops(CheckBox_BribeAuthorities.IsChecked == true);
    }

    private void CheckBox_RevealPlayers_Click(object sender, RoutedEventArgs e)
    {
        Globals.reveal_players(CheckBox_RevealPlayers.IsChecked == true);
    }

    private void CheckBox_AllowSellOnNonPublic_Click(object sender, RoutedEventArgs e)
    {
        Globals.allow_sell_on_non_public(CheckBox_AllowSellOnNonPublic.IsChecked == true);
    }

    private void CheckBox_OnlineSnow_Click(object sender, RoutedEventArgs e)
    {
        Globals.session_snow(CheckBox_OnlineSnow.IsChecked == true);
    }

    private void Button_Blips_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        var str = (e.OriginalSource as Button).Content.ToString();

        int index = MiscData.Blips.FindIndex(t => t.Name == str);
        if (index != -1)
        {
            Hacks.to_blip(new int[] { MiscData.Blips[index].ID });
        }
    }

    private void Button_MerryweatherServices_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        var str = (e.OriginalSource as Button).Content.ToString();

        int index = MiscData.MerryweatherServices.FindIndex(t => t.Name == str);
        if (index != -1)
        {
            Globals.merry_weather_services(MiscData.MerryweatherServices[index].ID);
        }
    }

    private void CheckBox_InstantBullShark_Click(object sender, RoutedEventArgs e)
    {
        Globals.instant_bull_shark(CheckBox_InstantBullShark.IsChecked == true);
    }

    private void CheckBox_BackupHeli_Click(object sender, RoutedEventArgs e)
    {
        Globals.call_heli_backup(CheckBox_BackupHeli.IsChecked == true);
    }

    private void CheckBox_Airstrike_Click(object sender, RoutedEventArgs e)
    {
        Globals.call_airstrike(CheckBox_Airstrike.IsChecked == true);
    }

    private void Button_RefushPersonalVehicleList_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        ListBox_PersonalVehicle.Items.Clear();
        pVInfos.Clear();

        Task.Run(() =>
        {
            int max_slots = Globals.GG<int>(1585853);
            for (int i = 0; i < max_slots; i++)
            {
                long hash = Globals.GG<long>(1585853 + 1 + (i * 142) + 66);
                if (hash == 0)
                    continue;

                string plate = Globals.get_global_string(1585853 + 1 + (i * 142) + 1);

                pVInfos.Add(new PVInfo()
                {
                    Index = i,
                    Name = Hacks.find_vehicle_display_name(hash, true),
                    hash = hash,
                    plate = plate
                });
            }

            foreach (var item in pVInfos)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ListBox_PersonalVehicle.Items.Add($"{item.Name} [{item.plate}]");
                });
            }
        });
    }

    private void Button_SpawnPersonalVehicle_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        int index = ListBox_PersonalVehicle.SelectedIndex;

        if (index != -1)
        {
            Task.Run(() =>
            {
                Globals.deliver_personal_vehicle(pVInfos[index].Index);
            });
        }
    }

    
}
