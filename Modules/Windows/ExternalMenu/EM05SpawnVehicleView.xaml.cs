using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Core;
using GTA5OnlineTools.Features.Data;
using static GTA5OnlineTools.Features.SDK.Hacks;

namespace GTA5OnlineTools.Modules.Windows.ExternalMenu
{
    /// <summary>
    /// EM05SpawnVehicleView.xaml 的交互逻辑
    /// </summary>
    public partial class EM05SpawnVehicleView : UserControl
    {
        private static long SpawnVehicleHash = 0;

        private struct PVInfo
        {
            public string Name;
            public long hash;
            public string plate;
        }

        private List<PVInfo> pVInfos = new List<PVInfo>();

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
            }
        }

        private void Button_SpawnOnlineVehicle_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            string str = (e.OriginalSource as Button).Content.ToString();

            if (str == "刷出线上载具（空地）")
            {
                SpawnVehicle(-255.0f);
            }
            else
            {
                SpawnVehicle(0.0f);
            }
        }

        private void SpawnVehicle(float z255)
        {
            Task.Run(() =>
            {
                if (SpawnVehicleHash != 0)
                {
                    int dist = 5;

                    const int oVMCreate = 2725260;
                    const int pegasus = 0;

                    float x = Memory.Read<float>(Globals.WorldPTR, Offsets.PlayerPositionX);
                    float y = Memory.Read<float>(Globals.WorldPTR, Offsets.PlayerPositionY);
                    float z = Memory.Read<float>(Globals.WorldPTR, Offsets.PlayerPositionZ);
                    float sin = Memory.Read<float>(Globals.WorldPTR, Offsets.PlayerSin);
                    float cos = Memory.Read<float>(Globals.WorldPTR, Offsets.PlayerCos);

                    x += cos * dist;
                    y += sin * dist;

                    if (z255 == -255.0f)
                        z = z255;
                    else
                        z += z255;

                    WriteGA<float>(oVMCreate + 7 + 0, x);                   // 载具坐标x
                    WriteGA<float>(oVMCreate + 7 + 1, y);                   // 载具坐标y
                    WriteGA<float>(oVMCreate + 7 + 2, z);                   // 载具坐标z

                    WriteGA<long>(oVMCreate + 27 + 66, SpawnVehicleHash);   // 载具哈希
                    WriteGA<int>(oVMCreate + 3, pegasus);                   // 帕格萨斯

                    WriteGA<int>(oVMCreate + 5, 1);                         // can spawn flag must be odd
                    WriteGA<int>(oVMCreate + 2, 1);                         // spawn toggle gets reset to 0 on car spawn
                    WriteGAString(oVMCreate + 27 + 1, Guid.NewGuid().ToString()[..8]);    // License plate  车牌

                    WriteGA<int>(oVMCreate + 27 + 5, -1);       // primary -1 auto 159  主色调
                    WriteGA<int>(oVMCreate + 27 + 6, -1);       // secondary -1 auto 159  副色调
                    WriteGA<int>(oVMCreate + 27 + 7, -1);       // pearlescent
                    WriteGA<int>(oVMCreate + 27 + 8, -1);       // wheel color

                    WriteGA<int>(oVMCreate + 27 + 15, 2);       // primary weapon  主武器
                    WriteGA<int>(oVMCreate + 27 + 19, -1);
                    WriteGA<int>(oVMCreate + 27 + 20, 2);       // secondary weapon  副武器

                    WriteGA<int>(oVMCreate + 27 + 21, 3);       // engine (0-3)  引擎
                    WriteGA<int>(oVMCreate + 27 + 22, 6);       // brakes (0-6)  刹车
                    WriteGA<int>(oVMCreate + 27 + 23, 9);       // transmission (0-9)  变速器
                    WriteGA<int>(oVMCreate + 27 + 24, new Random().Next(0, 78));        // horn (0-77)  喇叭
                    WriteGA<int>(oVMCreate + 27 + 25, 14);      // suspension (0-13)  悬吊系统
                    WriteGA<int>(oVMCreate + 27 + 26, 19);      // armor (0-18)  装甲
                    WriteGA<int>(oVMCreate + 27 + 27, 1);       // turbo (0-1)  涡轮增压
                    WriteGA<int>(oVMCreate + 27 + 28, 1);       // weaponised ownerflag

                    WriteGA<int>(oVMCreate + 27 + 30, 1);
                    WriteGA<int>(oVMCreate + 27 + 32, new Random().Next(0, 15));        // colored light (0-14)
                    WriteGA<int>(oVMCreate + 27 + 33, -1);                              // Wheel Selection

                    WriteGA<long>(oVMCreate + 27 + 60, 4030726305);  // landinggear/vehstate 起落架/载具状态

                    WriteGA<int>(oVMCreate + 27 + 62, new Random().Next(0, 256));       // Tire smoke color R
                    WriteGA<int>(oVMCreate + 27 + 63, new Random().Next(0, 256));       // Green Neon Amount 1-255 100%-0%
                    WriteGA<int>(oVMCreate + 27 + 64, new Random().Next(0, 256));       // Blue Neon Amount 1-255 100%-0%
                    WriteGA<int>(oVMCreate + 27 + 65, new Random().Next(0, 7));         // Window tint 0-6
                    WriteGA<int>(oVMCreate + 27 + 67, 1);       // Livery
                    WriteGA<int>(oVMCreate + 27 + 69, -1);      // Wheel type

                    WriteGA<int>(oVMCreate + 27 + 74, new Random().Next(0, 256));       // Red Neon Amount 1-255 100%-0%
                    WriteGA<int>(oVMCreate + 27 + 75, new Random().Next(0, 256));       // G
                    WriteGA<int>(oVMCreate + 27 + 76, new Random().Next(0, 256));       // B

                    //WriteGA<long>(oVMCreate + 27 + 77, 4030726305);                     // vehstate  载具状态
                    Memory.Write<byte>(ReadGA<long>(oVMCreate + 27 + 77) + 1, 0x02);    // 2:bulletproof 0:false  防弹的

                    WriteGA<int>(oVMCreate + 27 + 95, 14);      // ownerflag  拥有者标志
                    WriteGA<int>(oVMCreate + 27 + 94, 2);       // personal car ownerflag  个人载具拥有者标志
                }
            });
        }

        private int FixVehicleWeapon(long hash)
        {
            string name = FindVehicleDisplayName(hash, false);
            switch (name)
            {
                case "oppressor2":
                    return 2;
                case "apc":
                case "deluxo":
                    return -1;
                case "bombushka":
                    return 1;
                case "tampa3":
                case "insurgent3":
                case "halftrack":
                    return 3;
                case "barrage":
                    return 30;
                default:
                    return -1;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////

        private void CheckBox_VehicleGodMode_Click(object sender, RoutedEventArgs e)
        {
            Vehicle.GodMode(Settings.Vehicle.VehicleGodMode = true);
            Settings.Vehicle.VehicleGodMode = CheckBox_VehicleGodMode.IsChecked == true;
        }

        private void CheckBox_VehicleSeatbelt_Click(object sender, RoutedEventArgs e)
        {
            Vehicle.Seatbelt(CheckBox_VehicleSeatbelt.IsChecked == true);
            Settings.Vehicle.VehicleSeatbelt = CheckBox_VehicleSeatbelt.IsChecked == true;
        }

        private void CheckBox_VehicleParachute_Click(object sender, RoutedEventArgs e)
        {
            Vehicle.Parachute(CheckBox_VehicleParachute.IsChecked == true);
        }

        private void CheckBox_VehicleInvisibility_Click(object sender, RoutedEventArgs e)
        {
            Vehicle.Invisibility(CheckBox_VehicleInvisibility.IsChecked == true);
        }

        private void Button_FillVehicleHealth_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            Vehicle.FillHealth();
        }

        private void RadioButton_VehicleExtras_None_Click(object sender, RoutedEventArgs e)
        {
            if (RadioButton_VehicleExtras_None.IsChecked == true)
            {
                Vehicle.Extras(0);
            }
            else if (RadioButton_VehicleExtras_Jump.IsChecked == true)
            {
                Vehicle.Extras(40);
            }
            else if (RadioButton_VehicleExtras_Boost.IsChecked == true)
            {
                Vehicle.Extras(66);
            }
            else if (RadioButton_VehicleExtras_Both.IsChecked == true)
            {
                Vehicle.Extras(96);
            }
        }

        private void Button_RepairVehicle_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            Vehicle.Fix1stfoundBST();
        }

        private void Button_TurnOffBST_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            Online.InstantBullShark(false);
        }

        private void Button_GetInOnlinePV_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            Online.GetInOnlinePV();
        }

        private void Button_RefushPersonalVehicleList_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                int max_slots = ReadGA<int>(1585844);
                for (int i = 0; i < max_slots; i++)
                {
                    long hash = ReadGA<long>(1585844 + 1 + (i * 142) + 66);
                    if (hash == 0)
                        continue;

                    string plate = ReadGAString(1585844 + 1 + (i * 142) + 1);

                    pVInfos.Add(new PVInfo()
                    {
                        Name = "",
                        hash = hash,
                        plate = plate
                    });
                }

                foreach (var item in pVInfos)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ListBox_PersonalVehicle.Items.Add($"[{item.plate}] {FindVehicleDisplayName(item.hash, true)}");
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
                    WriteGA<int>(2810287 + 965, index);
                    WriteGA<int>(2810287 + 962, 1);
                    Task.Delay(500).Wait();
                    WriteGA<int>(2671444 + 8, 1);
                });
            }
        }

        private string FindVehicleDisplayName(long hash, bool isDisplay)
        {
            foreach (var item in VehicleData.VehicleClassData)
            {
                foreach (var item0 in item.VehicleInfo)
                {
                    if (item0.Hash == hash)
                    {
                        if (isDisplay)
                            return item0.DisplayName;
                        else
                            return item0.Name;
                    }
                }
            }

            return "";
        }
    }
}
