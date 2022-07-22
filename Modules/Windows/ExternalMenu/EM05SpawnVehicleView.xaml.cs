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
        Settings.SpawnVehicleHash = 0;

        int index1 = ListBox_VehicleClass.SelectedIndex;
        int index2 = ListBox_VehicleInfo.SelectedIndex;

        if (index1 != -1 && index2 != -1)
        {
            Settings.SpawnVehicleHash = VehicleData.VehicleClassData[index1].VehicleInfo[index2].Hash;
            Settings.SpawnVehicleMod = VehicleData.VehicleClassData[index1].VehicleInfo[index2].Mod;
        }
    }

    private void Button_SpawnOnlineVehicle_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        string str = (e.OriginalSource as Button).Content.ToString();

        if (str == "刷出线上载具（空地）")
        {
            Globals.Create_Vehicle(Hacks.Get_Local_Ped(), Settings.SpawnVehicleHash, Settings.SpawnVehicleMod, 7.0f, -225.0f);
        }
        else
        {
            Globals.Create_Vehicle(Hacks.Get_Local_Ped(), Settings.SpawnVehicleHash, Settings.SpawnVehicleMod, 7.0f, 0.0f);
        }
    }

    /////////////////////////////////////////////////////////////////////////////////

    private void CheckBox_VehicleGodMode_Click(object sender, RoutedEventArgs e) { Settings.VehicleGodMode = CheckBox_VehicleGodMode.IsChecked == true ? 1 : 0; }

    private void CheckBox_PlayerSeatbelt_Click(object sender, RoutedEventArgs e) { Settings.Seatbelt = CheckBox_PlayerSeatbelt.IsChecked == true ? 1 : 0; }

    private void CheckBox_VehicleParachute_Click(object sender, RoutedEventArgs e)
    {
        Vehicle.Set_Extras_Parachute(Ped.Get_Current_Vehicle(Hacks.Get_Local_Ped()), CheckBox_VehicleParachute.IsChecked == true);
    }

    private void CheckBox_VehicleInvisibility_Click(object sender, RoutedEventArgs e)
    {
        Vehicle.Set_Invisible(Ped.Get_Current_Vehicle(Hacks.Get_Local_Ped()), CheckBox_VehicleInvisibility.IsChecked == true);
    }

    private void Button_FillVehicleHealth_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Hacks.Revive_Vehicle(Ped.Get_Current_Vehicle(Hacks.Get_Local_Ped()));
    }

    private void RadioButton_VehicleExtras_None_Click(object sender, RoutedEventArgs e)
    {
        if (RadioButton_VehicleExtras_None.IsChecked == true)
        {
            Vehicle.Set_Extras_Vehicle_Jump(Ped.Get_Current_Vehicle(Hacks.Get_Local_Ped()), false);
            Vehicle.Set_Extras_Rocket_Boost(Ped.Get_Current_Vehicle(Hacks.Get_Local_Ped()), false);
        }
        else if (RadioButton_VehicleExtras_Jump.IsChecked == true)
        {
            Vehicle.Set_Extras_Vehicle_Jump(Ped.Get_Current_Vehicle(Hacks.Get_Local_Ped()), true);
        }
        else if (RadioButton_VehicleExtras_Boost.IsChecked == true)
        {
            Vehicle.Set_Extras_Rocket_Boost(Ped.Get_Current_Vehicle(Hacks.Get_Local_Ped()), true);
        }
        else if (RadioButton_VehicleExtras_Both.IsChecked == true)
        {
            Vehicle.Set_Extras_Vehicle_Jump(Ped.Get_Current_Vehicle(Hacks.Get_Local_Ped()), true);
            Vehicle.Set_Extras_Rocket_Boost(Ped.Get_Current_Vehicle(Hacks.Get_Local_Ped()), true);
        }
    }

    private void Button_RepairVehicle_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Hacks.Repair_Online_Vehicle(Ped.Get_Current_Vehicle(Hacks.Get_Local_Ped()));
    }

    private void Button_TurnOffBST_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Globals.Instant_Bull_Shark(false);
    }

    private void Button_GetInOnlinePV_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Globals.Get_Into_Online_Personal_Vehicle();
    }
}
