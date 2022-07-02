using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Data;

namespace GTA5OnlineTools.Modules.Windows.ExternalMenu;

/// <summary>
/// EM06SpawnWeaponView.xaml 的交互逻辑
/// </summary>
public partial class EM06SpawnWeaponView : UserControl
{
    public EM06SpawnWeaponView()
    {
        InitializeComponent();

        // 武器列表
        for (int i = 0; i < WeaponData.WeaponDataClass.Count; i++)
        {
            ListBox_WeaponList.Items.Add(WeaponData.WeaponDataClass[i].ClassName);
        }
        ListBox_WeaponList.SelectedIndex = 0;

        // 子弹类型
        for (int i = 0; i < MiscData.ImpactExplosions.Count; i++)
        {
            ComboBox_ImpactExplosion.Items.Add(MiscData.ImpactExplosions[i].Name);
        }
        ComboBox_ImpactExplosion.SelectedIndex = 0;

        ExternalMenuView.ClosingDisposeEvent += ExternalMenuView_ClosingDisposeEvent;
    }

    private void ExternalMenuView_ClosingDisposeEvent()
    {

    }

    private void ListBox_WeaponList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        int index = ListBox_WeaponList.SelectedIndex;

        if (index != -1)
        {
            ListBox_WeaponInfo.Items.Clear();

            for (int i = 0; i < WeaponData.WeaponDataClass[index].WeaponInfo.Count; i++)
            {
                ListBox_WeaponInfo.Items.Add(WeaponData.WeaponDataClass[index].WeaponInfo[i].DisplayName);
            }

            ListBox_WeaponInfo.SelectedIndex = 0;
        }
    }

    private void ListBox_WeaponInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        int index1 = ListBox_WeaponList.SelectedIndex;
        int index2 = ListBox_WeaponInfo.SelectedIndex;

        if (index1 != -1 && index2 != -1)
        {
            TempData.WPickup = "pickup_" + WeaponData.WeaponDataClass[index1].WeaponInfo[index2].Name;
        }
    }

    private void Button_SpawnWeapon_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Hacks.spawn_drop(Hacks.get_local_ped(), TempData.WPickup);
    }

    ////////////////////////////////////////////////////////////////////////////////////////

    private void CheckBox_AmmoModifier_InfiniteAmmo_Click(object sender, RoutedEventArgs e)
    {
        Settings.Player.AmmoModifier_InfiniteAmmo = (sbyte)(CheckBox_AmmoModifier_InfiniteAmmo.IsChecked == true ? 1 : 0);
    }

    private void CheckBox_AmmoModifier_InfiniteClip_Click(object sender, RoutedEventArgs e)
    {
        Settings.Player.AmmoModifier_InfiniteClip = (sbyte)(CheckBox_AmmoModifier_InfiniteClip.IsChecked == true ? 1 : 0);
    }

    private void CheckBox_InfiniteAmmo_Click(object sender, RoutedEventArgs e)
    {
        Hacks.infinite_ammo(CheckBox_InfiniteAmmo.IsChecked == true);
    }

    private void CheckBox_NoReload_Click(object sender, RoutedEventArgs e)
    {
        Hacks.no_reload(CheckBox_NoReload.IsChecked == true);
    }

    private void CheckBox_ReloadMult_Click(object sender, RoutedEventArgs e)
    {
        Ped.set_reload_time_multiplier(Hacks.get_local_ped(), CheckBox_ReloadMult.IsChecked == true ? 4.0f : 1.0f);
    }

    private void Button_NoRecoil_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Ped.set_recoil(Hacks.get_local_ped(), 0.0f);
    }

    private void CheckBox_NoSpread_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Ped.set_spread(Hacks.get_local_ped(), 0.0f);
    }

    private void CheckBox_Range_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Ped.set_lock_on_range(Hacks.get_local_ped(), 1000.0f);
        Ped.set_range(Hacks.get_local_ped(), 2000.0f);
    }

    private void CheckBox_ImpactType_Click(object sender, RoutedEventArgs e)
    {
        if (CheckBox_ImpactType.IsChecked == true)
        {
            Ped.set_damage_type(Hacks.get_local_ped(), 5);
        }
        else
        {
            Ped.set_damage_type(Hacks.get_local_ped(), 3);
        }
    }

    private void ComboBox_ImpactExplosion_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        int index = ComboBox_ImpactExplosion.SelectedIndex;

        if (index != -1)
        {
            Ped.set_explosion_type(Hacks.get_local_ped(), MiscData.ImpactExplosions[index].ID);
        }
    }

    private void Button_FillCurrentAmmo_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Hacks.fill_current_ammo();
    }

    private void Button_FillAllAmmo_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Hacks.fill_all_ammo();
    }
}
