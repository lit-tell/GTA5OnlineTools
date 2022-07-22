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

    private void CheckBox_RemovePassiveModeCooldown_Click(object sender, RoutedEventArgs e) { Settings.RemovePassiveModeCooldown = CheckBox_RemovePassiveModeCooldown.IsChecked == true ? 1 : 0; }

    private void CheckBox_RemoveSuicideCooldown_Click(object sender, RoutedEventArgs e) { Settings.RemoveSuicideCooldown = CheckBox_RemoveSuicideCooldown.IsChecked == true ? 1 : 0; }

    private void CheckBox_DisableOrbitalCooldown_Click(object sender, RoutedEventArgs e) { Settings.DisableOrbitalCooldown = CheckBox_DisableOrbitalCooldown.IsChecked == true ? 1 : 0; }

    private void CheckBox_OffRadar_Click(object sender, RoutedEventArgs e) { Settings.OffRadar = CheckBox_OffRadar.IsChecked == true ? 1 : 0; }

    private void CheckBox_GhostOrganization_Click(object sender, RoutedEventArgs e) { Settings.GhostOrganization = CheckBox_GhostOrganization.IsChecked == true ? 1 : 0; }

    private void CheckBox_BribeOrBlindCops_Click(object sender, RoutedEventArgs e) { Settings.BlindCops = CheckBox_BribeOrBlindCops.IsChecked == true ? 1 : 0; }

    private void CheckBox_BribeAuthorities_Click(object sender, RoutedEventArgs e) { Settings.BribeCops = CheckBox_BribeAuthorities.IsChecked == true ? 1 : 0; }

    private void CheckBox_RevealPlayers_Click(object sender, RoutedEventArgs e) { Settings.RevealPlayers = CheckBox_RevealPlayers.IsChecked == true ? 1 : 0; }

    private void CheckBox_AllowSellOnNonPublic_Click(object sender, RoutedEventArgs e) { Settings.AllowSellOnNonPublic = CheckBox_AllowSellOnNonPublic.IsChecked == true ? 1 : 0; }

    private void CheckBox_OnlineSnow_Click(object sender, RoutedEventArgs e) { Settings.OnlineSnow = CheckBox_OnlineSnow.IsChecked == true ? 1 : 0; }

    private void Button_Blips_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        var str = (e.OriginalSource as Button).Content.ToString();

        int index = MiscData.Blips.FindIndex(t => t.Name == str);
        if (index != -1)
        {
            Hacks.To_Blip(new int[] { MiscData.Blips[index].ID });
        }
    }

    private void Button_DeliverAmmo_Click(object sender, RoutedEventArgs e) { AudioUtil.ClickSound(); Globals.Deliver_Ammo(true); }
    
    private void Button_DeliverBallisticArmor_Click(object sender, RoutedEventArgs e) { AudioUtil.ClickSound(); Globals.Deliver_Ballistic_Armor(true); }
    
    private void Button_DeliverBullShark_Click(object sender, RoutedEventArgs e) { AudioUtil.ClickSound(); Globals.Deliver_Bull_Shark(true); }
    
    private void Button_TriggerBoatPickup_Click(object sender, RoutedEventArgs e) { AudioUtil.ClickSound(); Globals.Trigger_Boat_Pickup(true); }
    
    private void Button_TriggerHeliPickup_Click(object sender, RoutedEventArgs e) { AudioUtil.ClickSound(); Globals.Trigger_Heli_Pickup(true, false); }

    private void Button_TriggerHeliVipPickup_Click(object sender, RoutedEventArgs e) { AudioUtil.ClickSound(); Globals.Trigger_Heli_Pickup(true, true); }

    private void Button_InstantBullShark_True_Click(object sender, RoutedEventArgs e) { AudioUtil.ClickSound(); Globals.Instant_Bull_Shark(true); }

    private void Button_InstantBullShark_False_Click(object sender, RoutedEventArgs e) { AudioUtil.ClickSound(); Globals.Instant_Bull_Shark(false); }

    private void Button_CallAirstrike_Click(object sender, RoutedEventArgs e) { AudioUtil.ClickSound(); Globals.Call_Airstrike(true); }

    private void Button_CallHeliBackup_Click(object sender, RoutedEventArgs e) { AudioUtil.ClickSound(); Globals.Call_Heli_Backup(true); }

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

                string plate = Globals.Get_Global_String(1585853 + 1 + (i * 142) + 1);

                pVInfos.Add(new PVInfo()
                {
                    Index = i,
                    Name = Hacks.Find_Vehicle_Display_Name(hash, true),
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
                Globals.Deliver_Personal_Vehicle(pVInfos[index].Index);
            });
        }
    }

    
}
