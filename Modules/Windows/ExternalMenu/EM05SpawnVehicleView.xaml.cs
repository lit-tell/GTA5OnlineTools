using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Core;
using GTA5OnlineTools.Features.Data;
using static GTA5OnlineTools.Features.SDK.Hacks;

namespace GTA5OnlineTools.Modules.Windows.ExternalMenu;

/// <summary>
/// EM05SpawnVehicleView.xaml 的交互逻辑
/// </summary>
public partial class EM05SpawnVehicleView : UserControl
{
    private uint SpawnVehicleHash = 0;
    private int[] SpawnVehicleMod;

    public EM05SpawnVehicleView()
    {
        InitializeComponent();

        // 载具列表
        for (int i = 0; i < VehicleData.VehicleClassData.Count; i++)
        {
            ListBox_VehicleClass.Items.Add(VehicleData.VehicleClassData[i].ClassName);
        }
        ListBox_VehicleClass.SelectedIndex = 0;

        ExternalMenuView.ClosingDisposeEvent += ExternalMenuView_ClosingDisposeEvent;
    }

    private void ExternalMenuView_ClosingDisposeEvent()
    {

    }

    private void ListBox_VehicleClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        int index = ListBox_VehicleClass.SelectedIndex;

        if (index != -1)
        {
            ListBox_VehicleInfo.Items.Clear();

            for (int i = 0; i < VehicleData.VehicleClassData[index].VehicleInfo.Count; i++)
            {
                ListBox_VehicleInfo.Items.Add(VehicleData.VehicleClassData[index].VehicleInfo[i].DisplayName);
            }

            ListBox_VehicleInfo.SelectedIndex = 0;
        }
    }

    private void ListBox_VehicleInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        SpawnVehicleHash = 0;

        int index1 = ListBox_VehicleClass.SelectedIndex;
        int index2 = ListBox_VehicleInfo.SelectedIndex;

        if (index1 != -1 && index2 != -1)
        {
            SpawnVehicleHash = VehicleData.VehicleClassData[index1].VehicleInfo[index2].Hash;
            SpawnVehicleMod = VehicleData.VehicleClassData[index1].VehicleInfo[index2].Mod;
        }
    }

    private void Button_SpawnOnlineVehicle_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        string str = (e.OriginalSource as Button).Content.ToString();

        if (str == "刷出线上载具（空地）")
        {
            Globals.create_vehicle(Hacks.get_local_ped(), SpawnVehicleHash, SpawnVehicleMod, 7.0f, -225.0f);
        }
        else
        {
            Globals.create_vehicle(Hacks.get_local_ped(), SpawnVehicleHash, SpawnVehicleMod, 7.0f, 0.0f);
        }
    }

    /////////////////////////////////////////////////////////////////////////////////

    private void CheckBox_VehicleGodMode_Click(object sender, RoutedEventArgs e)
    {
        Vehicle.set_godmode(Ped.get_current_vehicle(Hacks.get_local_ped()), Settings.Vehicle.VehicleGodMode = true);
        Settings.Vehicle.VehicleGodMode = CheckBox_VehicleGodMode.IsChecked == true;
    }

    private void CheckBox_VehicleSeatbelt_Click(object sender, RoutedEventArgs e)
    {
        Ped.set_seatbelt(Hacks.get_local_ped(), CheckBox_VehicleSeatbelt.IsChecked == true);
        Settings.Vehicle.VehicleSeatbelt = CheckBox_VehicleSeatbelt.IsChecked == true;
    }

    private void CheckBox_VehicleParachute_Click(object sender, RoutedEventArgs e)
    {
        Vehicle.set_extras_parachute(Ped.get_current_vehicle(Hacks.get_local_ped()), CheckBox_VehicleParachute.IsChecked == true);
    }

    private void CheckBox_VehicleInvisibility_Click(object sender, RoutedEventArgs e)
    {
        Vehicle.set_invisible(Ped.get_current_vehicle(Hacks.get_local_ped()), CheckBox_VehicleInvisibility.IsChecked == true);
    }

    private void Button_FillVehicleHealth_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Hacks.revive_vehicle(Ped.get_current_vehicle(Hacks.get_local_ped()));
    }

    private void RadioButton_VehicleExtras_None_Click(object sender, RoutedEventArgs e)
    {
        if (RadioButton_VehicleExtras_None.IsChecked == true)
        {
            Vehicle.set_extras_vehicle_jump(Ped.get_current_vehicle(Hacks.get_local_ped()), false);
            Vehicle.set_extras_rocket_boost(Ped.get_current_vehicle(Hacks.get_local_ped()), false);
        }
        else if (RadioButton_VehicleExtras_Jump.IsChecked == true)
        {
            Vehicle.set_extras_vehicle_jump(Ped.get_current_vehicle(Hacks.get_local_ped()), true);
        }
        else if (RadioButton_VehicleExtras_Boost.IsChecked == true)
        {
            Vehicle.set_extras_rocket_boost(Ped.get_current_vehicle(Hacks.get_local_ped()), true);
        }
        else if (RadioButton_VehicleExtras_Both.IsChecked == true)
        {
            Vehicle.set_extras_vehicle_jump(Ped.get_current_vehicle(Hacks.get_local_ped()), true);
            Vehicle.set_extras_rocket_boost(Ped.get_current_vehicle(Hacks.get_local_ped()), true);
        }
    }

    private void Button_RepairVehicle_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Hacks.repair_online_vehicle(Ped.get_current_vehicle(Hacks.get_local_ped()));
    }

    private void Button_TurnOffBST_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Globals.instant_bull_shark(false);
    }

    private void Button_GetInOnlinePV_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Globals.get_into_online_personal_vehicle();
    }
}
