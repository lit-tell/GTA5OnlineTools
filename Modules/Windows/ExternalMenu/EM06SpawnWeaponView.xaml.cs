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

        Hacks.Spawn_Drop(Hacks.Get_Local_Ped(), TempData.WPickup);
    }

    ////////////////////////////////////////////////////////////////////////////////////////

    private void CheckBox_AmmoModifier_InfiniteAmmo_Click(object sender, RoutedEventArgs e) { Settings.AmmoModifier_InfiniteAmmo = CheckBox_AmmoModifier_InfiniteAmmo.IsChecked == true ? 1 : 0; }

    private void CheckBox_AmmoModifier_InfiniteClip_Click(object sender, RoutedEventArgs e) { Settings.AmmoModifier_InfiniteClip = CheckBox_AmmoModifier_InfiniteClip.IsChecked == true ? 1 : 0; }

    private void CheckBox_ReloadMult_Click(object sender, RoutedEventArgs e)
    {
        Ped.Set_Reload_Time_Multiplier(Hacks.Get_Local_Ped(), CheckBox_ReloadMult.IsChecked == true ? 4.0f : 1.0f);
    }

    private void Button_NoRecoil_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Ped.Set_Recoil(Hacks.Get_Local_Ped(), 0.0f);
    }

    private void CheckBox_NoSpread_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Ped.Set_Spread(Hacks.Get_Local_Ped(), 0.0f);
    }

    private void CheckBox_Range_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Ped.Set_Lock_On_Range(Hacks.Get_Local_Ped(), 1000.0f);
        Ped.Set_Range(Hacks.Get_Local_Ped(), 2000.0f);
    }

    private void CheckBox_ImpactType_Click(object sender, RoutedEventArgs e)
    {
        if (CheckBox_ImpactType.IsChecked == true)
        {
            Ped.Set_Damage_Type(Hacks.Get_Local_Ped(), 5);
        }
        else
        {
            Ped.Set_Damage_Type(Hacks.Get_Local_Ped(), 3);
        }
    }

    private void ComboBox_ImpactExplosion_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        int index = ComboBox_ImpactExplosion.SelectedIndex;

        if (index != -1)
        {
            Ped.Set_Explosion_Type(Hacks.Get_Local_Ped(), MiscData.ImpactExplosions[index].ID);
        }
    }

    private void Button_FillCurrentAmmo_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Hacks.Fill_Current_Ammo();
    }

    private void Button_FillAllAmmo_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Hacks.Fill_All_Ammo();
    }
}
