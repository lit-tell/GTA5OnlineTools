using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Features.SDK;

public static class Vehicle
{
    public static void Create_Vehicle(uint hash, int[] mod, Vector3 pos)
    {
        Hacks.WriteGA<uint>(Hacks.oVMCreate + 27 + 66, hash);       // 载具哈希值

        Hacks.WriteGA<int>(Hacks.oVMCreate + 27 + 94, 2);           // personal car ownerflag  个人载具拥有者标志
        Hacks.WriteGA<int>(Hacks.oVMCreate + 27 + 95, 14);          // ownerflag  拥有者标志

        Hacks.WriteGA<int>(Hacks.oVMCreate + 27 + 5, -1);           // primary -1 auto 159  主色调
        Hacks.WriteGA<int>(Hacks.oVMCreate + 27 + 6, -1);           // secondary -1 auto 159  副色调

        Hacks.WriteGA<float>(Hacks.oVMCreate + 7 + 0, pos.X);       // 载具坐标x
        Hacks.WriteGA<float>(Hacks.oVMCreate + 7 + 1, pos.Y);       // 载具坐标y
        Hacks.WriteGA<float>(Hacks.oVMCreate + 7 + 2, pos.Z);       // 载具坐标z

        Hacks.WriteGAString(Hacks.oVMCreate + 27 + 1, Guid.NewGuid().ToString()[..8]);    // License plate  车牌

        for (int i = 0; i < 43; i++)
        {
            if (i < 17)
            {
                Hacks.WriteGA<int>(Hacks.oVMCreate + 27 + 10 + i, mod[i]);
            }
            else if (i >= 17 && i != 42)
            {
                Hacks.WriteGA<int>(Hacks.oVMCreate + 27 + 10 + 6 + i, mod[i]);
            }
            else if (mod[42] > 0 && i == 42)
            {
                Hacks.WriteGA<int>(Hacks.oVMCreate + 27 + 10 + 6 + 42, new Random().Next(1, mod[42] + 1));
            }
        }

        Hacks.WriteGA<int>(Hacks.oVMCreate + 27 + 7, -1);       // pearlescent
        Hacks.WriteGA<int>(Hacks.oVMCreate + 27 + 8, -1);       // wheel color
        Hacks.WriteGA<int>(Hacks.oVMCreate + 27 + 33, -1);      // wheel selection
        Hacks.WriteGA<int>(Hacks.oVMCreate + 27 + 69, -1);      // Wheel type

        Hacks.WriteGA<int>(Hacks.oVMCreate + 27 + 28, 1);
        Hacks.WriteGA<int>(Hacks.oVMCreate + 27 + 30, 1);
        Hacks.WriteGA<int>(Hacks.oVMCreate + 27 + 32, 1);
        Hacks.WriteGA<int>(Hacks.oVMCreate + 27 + 65, 1);

        Hacks.WriteGA<uint>(Hacks.oVMCreate + 27 + 77, 0xF0400200);         // vehstate  载具状态 没有这个载具起落架是收起状态

        Hacks.WriteGA<int>(Hacks.oVMCreate + 5, 1);                         // can spawn flag must be odd
        Hacks.WriteGA<int>(Hacks.oVMCreate + 2, 1);                         // spawn toggle gets reset to 0 on car spawn
    }

    public static void Create_Vehicle(long ped, uint hash, int[] mod, float dist = 7.0f, float height = 0.0f)
    {
        Vector3 pos = Ped.Get_Real_Forward_Position(ped, dist);
        pos.Z = height == -225.0f ? height : pos.Z + height;
        Create_Vehicle(hash, mod, pos);
    }

    public static uint Get_Fix_Veh_Value()
    {
        return Memory.Read<uint>(Globals.PickupDataPTR, new int[] { 0x228 });
    }

    public static uint Get_Bull_Shark_Testosterone_Value()
    {
        return Memory.Read<uint>(Globals.PickupDataPTR, new int[] { 0x160 });
    }

    public static void Repair_Online_Vehicle(long vehicle)
    {
        Task.Run(() =>
        {
            Hacks.Deliver_Bull_Shark(true);
            Task.Delay(300).Wait();
            uint fix_veh_value = Get_Fix_Veh_Value();
            uint bull_shark_testosterone_value = Get_Bull_Shark_Testosterone_Value();
            List<long> pickups = Replayinterface.Get_Pickups();
            for (int i = 0; i < pickups.Count; i++)
            {
                if (Pickup.Get_Pickup_Value(pickups[i]) == bull_shark_testosterone_value)
                {
                    Pickup.Set_Pickup_Value(pickups[i], fix_veh_value);
                    Task.Delay(10).Wait();
                    Vehicle.Set_Health(vehicle, 999.0f);
                    Task.Delay(10).Wait();
                    Pickup.Set_Position(pickups[i], Vehicle.Get_Real_Position(vehicle));
                    Task.Delay(10).Wait();
                    break;
                }
            }
            Task.Delay(1000).Wait();
            if (Globals.Is_In_Bull_Shark())
            {
                Vehicle.Set_Dirt_Level(vehicle, 0.0f);
                Globals.Instant_Bull_Shark(false);
            }
        });
    }

    public static string Find_Vehicle_Display_Name(long hash, bool isDisplay)
    {
        foreach (var item in Data.VehicleData.VehicleClassData)
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
