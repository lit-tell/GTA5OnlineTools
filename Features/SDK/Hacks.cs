using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Features.SDK;

public static class Hacks
{

    // -- Vehicle Menus Globals

    public const int oVMCreate = 2725269;       // -- Create any vehicle.
    public const int oVMYCar = 2810701;         // -- Get my car.
    public const int oVGETIn = 2671447;         // -- Spawn into vehicle.
    public const int oVMSlots = 1585853;        // -- Get vehicle slots.

    // -- Some Player / Network times associated Globals

    public const int oPlayerGA = 2703660;
    public const int oPlayerIDHelp = 2689224;
    public const int oNETTimeHelp = 2703660;

    //////////////////////////////////////////////////////////

    public static long GlobalAddress(int index)
    {
        return Memory.Read<long>(Globals.GlobalPTR + 0x8 * ((index >> 0x12) & 0x3F)) + 8 * (index & 0x3FFFF);
    }

    public static T ReadGA<T>(int index) where T : struct
    {
        return Memory.Read<T>(GlobalAddress(index));
    }

    public static void WriteGA<T>(int index, T vaule) where T : struct
    {
        Memory.Write<T>(GlobalAddress(index), vaule);
    }

    public static string ReadGAString(int index)
    {
        return Memory.ReadString(GlobalAddress(index), null, 20);
    }

    public static void WriteGAString(int index, string str)
    {
        Memory.WriteString(GlobalAddress(index), null, str);
    }

    /////////////////////////////////////////////////////

    public static int GET_NETWORK_TIME()
    {
        return ReadGA<int>(1574755 + 11);
    }

    public static int PLAYER_ID()
    {
        return ReadGA<int>(oPlayerGA);
    }

    public static int GetBusinessIndex(int ID)
    {
        // ID 0-5
        return 1853131 + 1 + (PLAYER_ID() * 888) + 267 + 187 + 1 + (ID * 13);
    }

    public static int Get_Last_MP_Char()
    {
        return ReadGA<int>(1574915);
    }

    /////////////////////////////////////////

    public static uint Joaat(string input)
    {
        uint num1 = 0U;
        input = input.ToLower();
        foreach (char c in input)
        {
            uint num2 = num1 + c;
            uint num3 = num2 + (num2 << 10);
            num1 = num3 ^ num3 >> 6;
        }
        uint num4 = num1 + (num1 << 3);
        uint num5 = num4 ^ num4 >> 11;

        return num5 + (num5 << 15);
    }

    public static void Stat_Set_Int(string hash, int value)
    {
        if (hash.IndexOf("_") == 0)
        {
            int Stat_MP = Get_Last_MP_Char();
            hash = $"MP{Stat_MP}{hash}";
        }

        uint Stat_ResotreHash = ReadGA<uint>(1655453 + 4);
        int Stat_ResotreValue = ReadGA<int>(1020252 + 5526);

        WriteGA<uint>(1655453 + 4, Hacks.Joaat(hash));
        WriteGA<int>(1020252 + 5526, value);
        WriteGA<int>(1644218 + 1139, -1);
        Thread.Sleep(1000);
        WriteGA<uint>(1655453 + 4, Stat_ResotreHash);
        WriteGA<int>(1020252 + 5526, Stat_ResotreValue);
    }

    public static void Create_Ambient_Pickup(int amount, Vector3 pos)
    {
        WriteGA<float>(2783345 + 3, pos.X);
        WriteGA<float>(2783345 + 4, pos.Y);
        WriteGA<float>(2783345 + 5, pos.Z);
        WriteGA<int>(2783345 + 1, amount);
        WriteGA<int>(4528329 + 1 + (ReadGA<int>(2783345) * 85) + 66 + 2, 2);
        WriteGA<int>(2783345 + 6, 1);
    }

    public static long Get_Local_Ped()
    {
        return Memory.Read<long>(Globals.WorldPTR, new int[] { 0x8 });
    }
}


public partial class Hacks
{
  

    public static void Spawn_Drop(uint hash, Vector3 pos)
    {
        Globals.Create_Ambient_Pickup(9999, pos);
        Thread.Sleep(150);
        List<long> pickups = Replayinterface.Get_Pickups();
        for (int i = 0; i < pickups.Count; i++)
        {
            long pickup = pickups[i];
            if (Entity.Get_Model_Hash(pickup) == Joaat("prop_cash_pile_01")) //if (Pickup.get_pickup_hash(pickup) == 4263048111)
            {
                Pickup.Set_Pickup_Hash(pickup, hash);
                break;
            }
        }
    }

    public static void Spawn_Drop(long ped, uint hash, float dist = 0.0f, float height = 3.0f)
    {
        Vector3 pos = Ped.Get_Real_Forward_Position(ped, dist);
        pos.Z += height;
        Spawn_Drop(hash, pos);
    }
    public static void Spawn_Drop(long ped, string name, float dist = 0.0f, float height = 3.0f)
    {
        Spawn_Drop(ped, Joaat(name), dist, height);
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
            Globals.Deliver_Bull_Shark(true);
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

    public static void Infinite_Ammo(bool toggle)
    {
        Memory.WriteBytes(Globals.InfiniteAmmoADDR, toggle ? new byte[] { 0x90, 0x90, 0x90 } : new byte[] { 0x41, 0x2B, 0xD1 });
    }

    public static void No_Reload(bool toggle)
    {
        Memory.WriteBytes(Globals.NoReloadADDR, toggle ? new byte[] { 0x90, 0x90, 0x90 } : new byte[] { 0x41, 0x2B, 0xC9 });
    }

    public static void Fill_Current_Ammo()
    {
        // Ped实体
        long pWeapon_AmmoInfo = Memory.Read<long>(Globals.WorldPTR, Offsets.Weapon.AmmoInfo);

        int getMaxAmmo = Memory.Read<int>(pWeapon_AmmoInfo + 0x28);

        long my_offset_0 = pWeapon_AmmoInfo;
        long my_offset_1;
        byte ammo_type;

        do
        {
            my_offset_0 = Memory.Read<long>(my_offset_0 + 0x08);
            my_offset_1 = Memory.Read<long>(my_offset_0 + 0x00);

            if (my_offset_0 == 0 || my_offset_1 == 0)
            {
                return;
            }

            ammo_type = Memory.Read<byte>(my_offset_1 + 0x0C);

        } while (ammo_type == 0x00);

        Memory.Write<int>(my_offset_1 + 0x18, getMaxAmmo);
    }

    public static void Fill_All_Ammo()
    {
        long p = Ped.Get_Ped_Inventory(Get_Local_Ped());
        p = Memory.Read<long>(p + 0x48);
        //for(int i = 0; i < 32; i++)
        //{
        //    long temp = Memory.Read<long>(p + i * 0x08);
        //    if (!Memory.IsValid(temp)) continue;
        //    if (!Memory.IsValid(Memory.Read<long>(temp + 0x08))) continue;
        //    Func<int, int, int> Max = (int a, int b) => { return  a > b ? a : b; };
        //    int max_ammo = Max(Memory.Read<int>(temp + 0x08, new int[] { 0x28 }), Memory.Read<int>(temp + 0x08, new int[] { 0x34 }));
        //    Memory.Write<int>(temp + 0x20, max_ammo);
        //}
        int count = 0;
        while (Memory.Read<int>(p + count * 0x08) != 0 && Memory.Read<int>(p + count * 0x08, new int[] { 0x08 }) != 0)
        {
            Func<int, int, int> Max = (int a, int b) => { return a > b ? a : b; };
            int max_ammo = Max(Memory.Read<int>(p + count * 0x08, new int[] { 0x08, 0x28 }), Memory.Read<int>(p + count * 0x08, new int[] { 0x08, 0x34 }));
            Memory.Write<int>(p + count * 0x08, new int[] { 0x20 }, max_ammo);
            count++;
        }
    }
}


public class BaseModelInfo
{
    public static uint Get_Hash(long baseModelInfo) { return Memory.Read<uint>(baseModelInfo + 0x18); }
    public static byte Get_Type(long baseModelInfo) { return Memory.Read<byte>(baseModelInfo + 0x9D); }
}

public class PedModelInfo : BaseModelInfo
{

}

public class VehicleModelInfo : BaseModelInfo
{
    public static ushort Get_Extras(long vehicleModelInfo) { return Memory.Read<ushort>(vehicleModelInfo + 0x58B); }
    public static bool Get_Extras_Rocket_Boost(long vehicleModelInfo) { return (Get_Extras(vehicleModelInfo) & (1 << 6)) == (1 << 6); }
    public static bool Get_Extras_Vehicle_Jump(long vehicleModelInfo) { return (Get_Extras(vehicleModelInfo) & (1 << 5)) == (1 << 5); }
    public static bool Get_Extras_Parachute(long vehicleModelInfo) { return (Get_Extras(vehicleModelInfo) & (1 << 8)) == (1 << 8); }
    public static bool Get_Extras_Ramp_Buggy(long vehicleModelInfo) { return (Get_Extras(vehicleModelInfo) & (1 << 9)) == (1 << 9); }


    public static void Set_Extras(long vehicleModelInfo, ushort value) { Memory.Write<ushort>(vehicleModelInfo + 0x58B, value); }
    public static void Set_Extras_Rocket_Boost(long vehicleModelInfo, bool toggle)
    {
        ushort temp = Get_Extras(vehicleModelInfo);
        if (toggle) temp = (ushort)(temp | (1 << 6));
        else temp = (ushort)(temp & ~(1 << 6));
        Set_Extras(vehicleModelInfo, temp);
    }
    public static void Set_Extras_Vehicle_Jump(long vehicleModelInfo, bool toggle)
    {
        ushort temp = Get_Extras(vehicleModelInfo);
        if (toggle) temp = (ushort)(temp | (1 << 5));
        else temp = (ushort)(temp & ~(1 << 5));
        Set_Extras(vehicleModelInfo, temp);
    }
    public static void Set_Extras_Parachute(long vehicleModelInfo, bool toggle)
    {
        ushort temp = Get_Extras(vehicleModelInfo);
        if (toggle) temp = (ushort)(temp | (1 << 8));
        else temp = (ushort)(temp & ~(1 << 8));
        Set_Extras(vehicleModelInfo, temp);
    }
    public static void Set_Extras_Ramp_Buggy(long vehicleModelInfo, bool toggle)
    {
        ushort temp = Get_Extras(vehicleModelInfo);
        if (toggle) temp = (ushort)(temp | (1 << 9));
        else temp = (ushort)(temp & ~(1 << 9));
        Set_Extras(vehicleModelInfo, temp);
    }
}

public class WeaponInfo : BaseModelInfo
{
    public static short Get_Damage_Type(long weaponInfo) { return Memory.Read<short>(weaponInfo + 0x20); }
    public static int Get_Explosion_Type(long weaponInfo) { return Memory.Read<int>(weaponInfo + 0x24); }
    public static int Get_Fire_Type(long weaponInfo) { return Memory.Read<int>(weaponInfo + 0x54); }
    public static float Get_Spread(long weaponInfo) { return Memory.Read<float>(weaponInfo + 0x7C); }
    public static float Get_Reload_Time_Multiplier(long weaponInfo) { return Memory.Read<float>(weaponInfo + 0x134); }
    public static float Get_Lock_On_Range(long weaponInfo) { return Memory.Read<float>(weaponInfo + 0x288); }
    public static float Get_Range(long weaponInfo) { return Memory.Read<float>(weaponInfo + 0x28C); }
    public static float Get_Recoil(long weaponInfo) { return Memory.Read<float>(weaponInfo + 0x2F4); }


    public static void Set_Damage_Type(long weaponInfo, short value) { Memory.Write<short>(weaponInfo + 0x20, value); }
    public static void Set_Explosion_Type(long weaponInfo, int value) { Memory.Write<int>(weaponInfo + 0x24, value); }
    public static void Set_Fire_Type(long weaponInfo, int value) { Memory.Write<int>(weaponInfo + 0x54, value); }
    public static void Set_Spread(long weaponInfo, float value) { Memory.Write<float>(weaponInfo + 0x7C, value); }
    public static void Set_Reload_Time_Multiplier(long weaponInfo, float value) { Memory.Write<float>(weaponInfo + 0x134, value); }
    public static void Set_Lock_On_Range(long weaponInfo, float value) { Memory.Write<float>(weaponInfo + 0x288, value); }
    public static void Set_Range(long weaponInfo, float value) { Memory.Write<float>(weaponInfo + 0x28C, value); }
    public static void Set_Recoil(long weaponInfo, float value) { Memory.Write<float>(weaponInfo + 0x2F4, value); }
}

public class ArchetypeDamp//rage::phArchetypeDamp
{
    public static float Get_Water_Collision_Strength(long archetypeDamp) { return Memory.Read<float>(archetypeDamp + 0x54); }
    public static bool Get_No_Water_Collision_Strength(long archetypeDamp) { return Get_Water_Collision_Strength(archetypeDamp) <= 0.0f; }


    public static void Set_Water_Collision_Strength(long archetypeDamp, float value) { Memory.Write<float>(archetypeDamp + 0x54, value); }
    public static void Set_No_Water_Collision_Strength(long archetypeDamp, bool toggle) { Set_Water_Collision_Strength(archetypeDamp, toggle ? 0.0f : 1.0f); }
}

public class Navigation
{
    public static long Get_Archetypedamp(long navigation) { return Memory.Read<long>(navigation + 0x10); }
    public static float Get_Water_Collision_Strength(long navigation) { return ArchetypeDamp.Get_Water_Collision_Strength(Get_Archetypedamp(navigation)); }
    public static bool Get_No_Water_Collision_Strength(long navigation) { return ArchetypeDamp.Get_No_Water_Collision_Strength(Get_Archetypedamp(navigation)); }
    public static Vector3 Get_Real_Right_Vector3(long navigation) { return Memory.Read<Vector3>(navigation + 0x20); }
    public static Vector3 Get_Real_Forward_Vector3(long navigation) { return Memory.Read<Vector3>(navigation + 0x30); }
    public static Vector3 Get_Real_Up_Vector3(long navigation) { return Memory.Read<Vector3>(navigation + 0x40); }
    public static Vector3 Get_Real_Position(long navigation) { return Memory.Read<Vector3>(navigation + 0x50); }


    public static void Set_Water_Collision_Strength(long navigation, float value) { ArchetypeDamp.Set_Water_Collision_Strength(Get_Archetypedamp(navigation), value); }
    public static void Set_No_Water_Collision_Strength(long navigation, bool toggle) { ArchetypeDamp.Set_No_Water_Collision_Strength(Get_Archetypedamp(navigation), toggle); }
    public static void Set_Real_Right_Vector3(long navigation, Vector3 pos) { Memory.Write<Vector3>(navigation + 0x20, pos); }
    public static void Set_Real_Forward_Vector3(long navigation, Vector3 pos) { Memory.Write<Vector3>(navigation + 0x30, pos); }
    public static void Set_Real_Up_Vector3(long navigation, Vector3 pos) { Memory.Write<Vector3>(navigation + 0x40, pos); }
    public static void Set_Real_Position(long navigation, Vector3 pos) { Memory.Write<Vector3>(navigation + 0x50, pos); }
}


public class Entity
{
    public static long Get_Model_Info(long entity) { return Memory.Read<long>(entity + 0x20); }
    public static uint Get_Model_Hash(long entity) { return BaseModelInfo.Get_Hash(Get_Model_Info(entity)); }
    public static uint Get_Model_Type(long entity) { return BaseModelInfo.Get_Type(Get_Model_Info(entity)); }
    public static byte Get_Type2(long entity) { return Memory.Read<byte>(entity + 0x29); }
    public static byte Get_Type(long entity) { return Memory.Read<byte>(entity + 0x2B); }
    public static bool Is_Player(long entity) { return ((Get_Type(entity) == 156) ? true : false); }
    public static bool Get_Invisible(long entity) { return (((Memory.Read<byte>(entity + 0x2C) & (1 << 0)) == (1 << 0)) ? false : true); }
    public static long Get_Navigation(long entity) { return Memory.Read<long>(entity + 0x30); }
    public static Vector3 Get_Real_Right_Vector3(long entity) { return Navigation.Get_Real_Right_Vector3(Get_Navigation(entity)); }
    public static Vector3 Get_Real_Forward_Vector3(long entity) { return Navigation.Get_Real_Forward_Vector3(Get_Navigation(entity)); }
    public static Vector3 Get_Real_Up_Vector3(long entity) { return Navigation.Get_Real_Up_Vector3(Get_Navigation(entity)); }
    public static Vector3 Get_Real_Position(long entity) { return Navigation.Get_Real_Position(Get_Navigation(entity)); }
    public static Vector3 Get_Real_Forward_Position(long entity, float dist = 7.0f)
    {
        Vector3 vec = Get_Real_Right_Vector3(entity);
        Vector3 pos = Get_Real_Position(entity);
        pos.X -= dist * vec.Y;
        pos.Y += dist * vec.X;
        return pos;
    }
    public static float Get_Water_Collision_Strength(long entity) { return Navigation.Get_Water_Collision_Strength(Get_Navigation(entity)); }
    public static bool Get_No_Water_Collision_Strength(long entity) { return Navigation.Get_No_Water_Collision_Strength(Get_Navigation(entity)); }
    public static Vector3 Get_Visual_Right_Vector3(long entity) { return Memory.Read<Vector3>(entity + 0x60); }
    public static Vector3 Get_Visual_Forward_Vector3(long entity) { return Memory.Read<Vector3>(entity + 0x70); }
    public static Vector3 Get_Visual_Up_Vector3(long entity) { return Memory.Read<Vector3>(entity + 0x80); }
    public static Vector3 Get_Visual_Position(long entity) { return Memory.Read<Vector3>(entity + 0x90); }
    public static Vector3 Get_Visual_Forward_Position(long entity, float dist = 7.0f)
    {
        Vector3 vec = Get_Visual_Right_Vector3(entity);
        Vector3 pos = Get_Visual_Position(entity);
        pos.X -= dist * vec.Y;
        pos.Y += dist * vec.X;
        return pos;
    }
    public static uint Get_Damage_Bits(long entity) { return Memory.Read<uint>(entity + 0x188); }
    public static bool Get_Proofs_Bullet(long entity) { return (Get_Damage_Bits(entity) & (1 << 4)) == (1 << 4); }
    public static bool Get_Proofs_Fire(long entity) { return (Get_Damage_Bits(entity) & (1 << 5)) == (1 << 5); }
    public static bool Get_Proofs_Collision(long entity) { return (Get_Damage_Bits(entity) & (1 << 6)) == (1 << 6); }
    public static bool Get_Proofs_Melee(long entity) { return (Get_Damage_Bits(entity) & (1 << 7)) == (1 << 7); }
    public static bool Get_Proofs_God(long entity) { return (Get_Damage_Bits(entity) & (1 << 8)) == (1 << 8); }//invincible
    public static bool Get_Proofs_Explosion(long entity) { return (Get_Damage_Bits(entity) & (1 << 11)) == (1 << 11); }
    public static bool Get_Proofs_Steam(long entity) { return (Get_Damage_Bits(entity) & (1 << 15)) == (1 << 15); }
    public static bool Get_Proofs_Drown(long entity) { return (Get_Damage_Bits(entity) & (1 << 16)) == (1 << 16); }
    public static bool Get_Proofs_Water(long entity) { return (Get_Damage_Bits(entity) & (1 << 24)) == (1 << 24); }


    public static void Set_Invisible(long entity, bool toggle)
    {
        byte temp = Memory.Read<byte>(entity + 0x2C);
        if (toggle) temp = (byte)(temp & ~(1 << 0));
        else temp = (byte)(temp | (1 << 0));
        Memory.Write(entity + 0x2C, temp);
    }
    public static void Set_Real_Right_Vector3(long entity, Vector3 pos) { Navigation.Set_Real_Right_Vector3(Get_Navigation(entity), pos); }
    public static void Set_Real_Forward_Vector3(long entity, Vector3 pos) { Navigation.Set_Real_Forward_Vector3(Get_Navigation(entity), pos); }
    public static void Set_Real_Up_Vector3(long entity, Vector3 pos) { Navigation.Set_Real_Up_Vector3(Get_Navigation(entity), pos); }
    public static void Set_Real_Position(long entity, Vector3 pos) { Navigation.Set_Real_Position(Get_Navigation(entity), pos); }
    public static void Set_Water_Collision_Strength(long entity, float value) { Navigation.Set_Water_Collision_Strength(Get_Navigation(entity), value); }
    public static void Set_No_Water_Collision_Strength(long entity, bool toggle) { Navigation.Set_No_Water_Collision_Strength(Get_Navigation(entity), toggle); }
    public static void Set_Visual_Right_Vector3(long entity, Vector3 pos) { Memory.Write<Vector3>(entity + 0x60, pos); }
    public static void Set_Visual_Forward_Vector3(long entity, Vector3 pos) { Memory.Write<Vector3>(entity + 0x70, pos); }
    public static void Set_Visual_Up_Vector3(long entity, Vector3 pos) { Memory.Write<Vector3>(entity + 0x80, pos); }
    public static void Set_Visual_Position(long entity, Vector3 pos) { Memory.Write<Vector3>(entity + 0x90, pos); }
    public static void Set_Position(long entity, Vector3 pos)
    {
        Set_Real_Position(entity, pos);
        Set_Visual_Position(entity, pos);
    }
    public static void Set_Damage_Bits(long entity, uint value) { Memory.Write<uint>(entity + 0x188, value); }
    public static void Set_Proofs_Bullet(long entity, bool toggle)
    {
        uint temp = Get_Damage_Bits(entity);
        temp = toggle ? (uint)(temp | (1 << 4)) : (uint)(temp & ~(1 << 4));
        Set_Damage_Bits(entity, temp);
    }
    public static void Set_Proofs_Fire(long entity, bool toggle)
    {
        uint temp = Get_Damage_Bits(entity);
        temp = toggle ? (uint)(temp | (1 << 5)) : (uint)(temp & ~(1 << 5));
        Set_Damage_Bits(entity, temp);
    }
    public static void Set_Proofs_Collision(long entity, bool toggle)
    {
        uint temp = Get_Damage_Bits(entity);
        temp = toggle ? (uint)(temp | (1 << 6)) : (uint)(temp & ~(1 << 6));
        Set_Damage_Bits(entity, temp);
    }
    public static void Set_Proofs_Melee(long entity, bool toggle)
    {
        uint temp = Get_Damage_Bits(entity);
        temp = toggle ? (uint)(temp | (1 << 7)) : (uint)(temp & ~(1 << 7));
        Set_Damage_Bits(entity, temp);
    }
    public static void Set_Proofs_God(long entity, bool toggle)
    {
        uint temp = Get_Damage_Bits(entity);
        temp = toggle ? (uint)(temp | (1 << 8)) : (uint)(temp & ~(1 << 8));
        Set_Damage_Bits(entity, temp);
    }
    public static void Set_Proofs_Explosion(long entity, bool toggle)
    {
        uint temp = Get_Damage_Bits(entity);
        temp = toggle ? (uint)(temp | (1 << 11)) : (uint)(temp & ~(1 << 11));
        Set_Damage_Bits(entity, temp);
    }
    public static void Set_Proofs_Steam(long entity, bool toggle)
    {
        uint temp = Get_Damage_Bits(entity);
        temp = toggle ? (uint)(temp | (1 << 15)) : (uint)(temp & ~(1 << 15));
        Set_Damage_Bits(entity, temp);
    }
    public static void Set_Proofs_Drown(long entity, bool toggle)
    {
        uint temp = Get_Damage_Bits(entity);
        temp = toggle ? (uint)(temp | (1 << 16)) : (uint)(temp & ~(1 << 16));
        Set_Damage_Bits(entity, temp);
    }
    public static void Set_Proofs_Water(long entity, bool toggle)
    {
        uint temp = Get_Damage_Bits(entity);
        temp = toggle ? (uint)(temp | (1 << 24)) : (uint)(temp & ~(1 << 24));
        Set_Damage_Bits(entity, temp);
    }
}


public class Ped : Entity
{
    public static byte Get_Hostility(long ped) { return Memory.Read<byte>(ped + 0x18C); }
    public static bool Is_Enemy(long ped) { return ((Get_Hostility(ped) > 1) ? true : false); }
    public static float Get_Health(long ped) { return Memory.Read<float>(ped + 0x280); }
    public static float Get_Max_Health(long ped) { return Memory.Read<float>(ped + 0x2A0); }
    public static long Get_Current_Vehicle(long ped) { return Memory.Read<long>(ped + 0xD30); }
    public static bool Is_In_Vehicle(long ped) { return ((Memory.Read<byte>(ped + 0xE52) == 1) ? true : false); }
    public static uint Get_Pedtype(long ped) { return Memory.Read<uint>(ped + 0x10B8) << 11 >> 25; }
    public static bool Get_No_Ragdoll(long ped) { return (((Memory.Read<byte>(ped + 0x10B8) & (1 << 5)) == (1 << 5)) ? false : true); }
    public static long Get_Player_Info(long ped) { return Memory.Read<long>(ped + 0x10C8); }
    public static bool Get_Frame_Flags_Explosiveammo(long ped) { return PlayerInfo.Get_Frame_Flags_Explosiveammo(Get_Player_Info(ped)); }
    public static bool Get_Frame_Flags_Flamingammo(long ped) { return PlayerInfo.Get_Frame_Flags_Flamingammo(Get_Player_Info(ped)); }
    public static bool Get_Frame_Flags_Explosivefists(long ped) { return PlayerInfo.Get_Frame_Flags_Explosivefists(Get_Player_Info(ped)); }
    public static bool Get_Frame_Flags_Superjump(long ped) { return PlayerInfo.Get_Frame_Flags_Superjump(Get_Player_Info(ped)); }
    public static void Set_Frame_Flags_Explosiveammo(long ped, bool toggle) { PlayerInfo.Set_Frame_Flags_Explosiveammo(Get_Player_Info(ped), toggle); }
    public static void Set_Frame_Flags_Flamingammo(long ped, bool toggle) { PlayerInfo.Set_Frame_Flags_Flamingammo(Get_Player_Info(ped), toggle); }
    public static void Set_Frame_Flags_Explosivefists(long ped, bool toggle) { PlayerInfo.Set_Frame_Flags_Explosivefists(Get_Player_Info(ped), toggle); }
    public static void Set_Frame_Flags_Superjump(long ped, bool toggle) { PlayerInfo.Set_Frame_Flags_Superjump(Get_Player_Info(ped), toggle); }
    public static int Get_Wanted_Level(long ped) { return PlayerInfo.Get_Wanted_Level((Get_Player_Info(ped))); }
    public static void Set_Wanted_Level(long ped, int value) { PlayerInfo.Set_Wanted_Level(Get_Player_Info(ped), value); }
    public static float Get_Run_Speed(long ped) { return PlayerInfo.Get_Run_Speed(Get_Player_Info(ped)); }
    public static float Get_Swim_Speed(long ped) { return PlayerInfo.Get_Swim_Speed(Get_Player_Info(ped)); }
    public static float Get_Stealth_Speed(long ped) { return PlayerInfo.Get_Stealth_Speed(Get_Player_Info(ped)); }
    public static void Set_Run_Speed(long ped, float value) { PlayerInfo.Set_Run_Speed(Get_Player_Info(ped), value); }
    public static void Set_Swim_Speed(long ped, float value) { PlayerInfo.Set_Swim_Speed(Get_Player_Info(ped), value); }
    public static void Set_Stealth_Speed(long ped, float value) { PlayerInfo.Set_Stealth_Speed(Get_Player_Info(ped), value); }
    public static bool Get_Everyone_Ignore(long ped) { return PlayerInfo.Get_Everyone_Ignore(Get_Player_Info(ped)); }
    public static bool Get_Cops_Ignore(long ped) { return PlayerInfo.Get_Cops_Ignore(Get_Player_Info(ped)); }
    public static void Set_Everyone_Ignore(long ped, bool toggle) { PlayerInfo.Set_Everyone_Ignore(Get_Player_Info(ped), toggle); }
    public static void Set_Cops_Ignore(long ped, bool toggle) { PlayerInfo.Set_Cops_Ignore(Get_Player_Info(ped), toggle); }
    public static long Get_Ped_Inventory(long ped) { return Memory.Read<long>(ped + 0x10D0); }
    public static bool Get_Infinite_Ammo(long ped) { return PedInventory.Get_Infinite_Ammo(Get_Ped_Inventory(ped)); }
    public static bool Get_Infinite_Clip(long ped) { return PedInventory.Get_Infinite_Clip(Get_Ped_Inventory(ped)); }
    public static void Set_Infinite_Ammo(long ped, bool toggle) { PedInventory.Set_Infinite_Ammo(Get_Ped_Inventory(ped), toggle); }
    public static void Set_Infinite_Clip(long ped, bool toggle) { PedInventory.Set_Infinite_Clip(Get_Ped_Inventory(ped), toggle); }
    public static long get_pedweaponmanager(long ped) { return Memory.Read<long>(ped + 0x10D8); }
    public static short Get_Damage_Type(long ped) { return PedWeaponManager.Get_Damage_Type(get_pedweaponmanager(ped)); }
    public static int Get_Explosion_Type(long ped) { return PedWeaponManager.Get_Explosion_Type(get_pedweaponmanager(ped)); }
    public static int Get_Fire_Type(long ped) { return PedWeaponManager.Get_Fire_Type(get_pedweaponmanager(ped)); }
    public static float Get_Spread(long ped) { return PedWeaponManager.Get_Spread(get_pedweaponmanager(ped)); }
    public static float Get_Reload_Time_Multiplier(long ped) { return PedWeaponManager.Get_Reload_Time_Multiplier(get_pedweaponmanager(ped)); }
    public static float Get_Lock_On_Range(long ped) { return PedWeaponManager.Get_Lock_On_Range(get_pedweaponmanager(ped)); }
    public static float Get_Range(long ped) { return PedWeaponManager.Get_Range(get_pedweaponmanager(ped)); }
    public static float Get_Recoil(long ped) { return PedWeaponManager.Get_Recoil(get_pedweaponmanager(ped)); }
    public static bool Get_Seatbelt(long ped) { return (((Memory.Read<byte>(ped + 0x145C) & (1 << 0)) == (1 << 0)) ? true : false); }
    public static float Get_Armor(long ped) { return Memory.Read<float>(ped + 0x1530); }
    private static float Get_Collision(long ped) { return Memory.Read<float>(ped + 0x30, new int[] { 0x10, 0x20, 0x70, 0x00, 0x2C }); }
    public static bool Get_No_Collision(long ped) { return ((Get_Collision(ped) <= 0.0f) ? true : false); }


    public static void Set_Damage_Type(long ped, short value) { PedWeaponManager.Set_Damage_Type(get_pedweaponmanager(ped), value); }
    public static void Set_Explosion_Type(long ped, int value) { PedWeaponManager.Set_Explosion_Type(get_pedweaponmanager(ped), value); }
    public static void Set_Fire_Type(long ped, int value) { PedWeaponManager.Set_Fire_Type(get_pedweaponmanager(ped), value); }
    public static void Set_Spread(long ped, float value) { PedWeaponManager.Set_Spread(get_pedweaponmanager(ped), value); }
    public static void Set_Reload_Time_Multiplier(long ped, float value) { PedWeaponManager.Set_Reload_Time_Multiplier(get_pedweaponmanager(ped), value); }
    public static void Set_Lock_On_Range(long ped, float value) { PedWeaponManager.Set_Lock_On_Range(get_pedweaponmanager(ped), value); }
    public static void Set_Range(long ped, float value) { PedWeaponManager.Set_Range(get_pedweaponmanager(ped), value); }
    public static void Set_Recoil(long ped, float value) { PedWeaponManager.Set_Recoil(get_pedweaponmanager(ped), value); }
    public static void Set_Health(long ped, float value) { Memory.Write<float>(ped + 0x280, value); }
    public static void Set_Max_Health(long ped, float value) { Memory.Write<float>(ped + 0x2A0, value); }
    public static void Set_No_Ragdoll(long ped, bool toggle)
    {
        byte temp = Memory.Read<byte>(ped + 0x10B8);
        if (toggle) temp = (byte)(temp | (1 << 5));
        else temp = (byte)(temp & ~(1 << 5));
        Memory.Write<byte>(ped + 0x10B8, temp);
    }
    public static void Set_Seatbelt(long ped, bool toggle)
    {
        byte temp = Memory.Read<byte>(ped + 0x145C);
        if (toggle) temp = (byte)(temp | (1 << 0));
        else temp = (byte)(temp & ~(1 << 0));
        Memory.Write<byte>(ped + 0x145C, temp);
    }
    public static void Set_Armour(long ped, float value) { Memory.Write<float>(ped + 0x1530, value); }
    private static void Set_Collision(long ped, float value) { Memory.Write<float>(ped + 0x30, new int[] { 0x10, 0x20, 0x70, 0x00, 0x2C }, value); }
    public static void Set_No_Collision(long ped, bool toggle) { Set_Collision(ped, (toggle ? -1.0f : 0.25f)); }
}


public class Vehicle : Entity
{
    public static ushort Get_Extras(long vehicle) { return VehicleModelInfo.Get_Extras(Get_Model_Info(vehicle)); }
    public static bool Get_Extras_Rocket_Boost(long vehicle) { return VehicleModelInfo.Get_Extras_Rocket_Boost(Get_Model_Info(vehicle)); }
    public static bool Get_Extras_Vehicle_Jump(long vehicle) { return VehicleModelInfo.Get_Extras_Vehicle_Jump(Get_Model_Info(vehicle)); }
    public static bool Get_Extras_Parachute(long vehicle) { return VehicleModelInfo.Get_Extras_Parachute(Get_Model_Info(vehicle)); }
    public static bool Get_Extras_Ramp_Buggy(long vehicle) { return VehicleModelInfo.Get_Extras_Ramp_Buggy(Get_Model_Info(vehicle)); }
    public static byte Get_State(long vehicle) { return Memory.Read<byte>(vehicle + 0xD8); }
    public static bool Get_State_Is_Personal(long vehicle) { return (((Get_State(vehicle) & (1 << 5)) == (1 << 5)) ? true : false); }
    public static bool Get_State_Is_Using(long vehicle)
    {
        byte temp = Get_State(vehicle);
        return ((((temp & (1 << 1)) == (1 << 1)) && !((temp & (1 << 0)) == (1 << 0))) ? true : false);
    }
    public static bool Get_State_Is_Destroyed(long vehicle)
    {
        byte temp = Get_State(vehicle);
        return ((((temp & (1 << 1)) == (1 << 1)) && ((temp & (1 << 0)) == (1 << 0))) ? true : false);
    }
    public static float Get_Health(long vehicle) { return Memory.Read<float>(vehicle + 0x280); }
    public static float Get_Max_Health(long vehicle) { return Memory.Read<float>(vehicle + 0x2A0); }
    public static float Get_Health2(long vehicle) { return Memory.Read<float>(vehicle + 0x840); }//m_body_health
    public static float Get_Health3(long vehicle) { return Memory.Read<float>(vehicle + 0x844); }//m_petrol_tank_health
    public static float Get_Engine_Health(long vehicle) { return Memory.Read<float>(vehicle + 0x908); }//m_engine_health
    public static float Get_Dirt_Level(long vehicle) { return Memory.Read<float>(vehicle + 0x9F8); }
    public static float Get_Gravity(long vehicle) { return Memory.Read<float>(vehicle + 0xC5C); }
    public static byte Get_Cur_Num_Of_Passenger(long vehicle) { return Memory.Read<byte>(vehicle + 0xC62); }


    public static void Set_Extras(long vehicle, ushort value) { VehicleModelInfo.Set_Extras(Get_Model_Info(vehicle), value); }
    public static void Set_Extras_Rocket_Boost(long vehicle, bool toggle) { VehicleModelInfo.Set_Extras_Rocket_Boost(Get_Model_Info(vehicle), toggle); }
    public static void Set_Extras_Vehicle_Jump(long vehicle, bool toggle) { VehicleModelInfo.Set_Extras_Vehicle_Jump(Get_Model_Info(vehicle), toggle); }
    public static void Set_Extras_Parachute(long vehicle, bool toggle) { VehicleModelInfo.Set_Extras_Parachute(Get_Model_Info(vehicle), toggle); }
    public static void Set_Extras_Ramp_Buggy(long vehicle, bool toggle) { VehicleModelInfo.Set_Extras_Ramp_Buggy(Get_Model_Info(vehicle), toggle); }
    public static void Set_State(long vehicle, byte value) { Memory.Write<byte>(vehicle + 0xD8, value); }
    public static void Set_State_Is_Personal(long vehicle, bool toggle)
    {
        byte temp = Get_State(vehicle);
        if (toggle) temp = (byte)(temp | (1 << 5));
        else temp = (byte)(temp & ~(1 << 5));
        Set_State(vehicle, temp);
    }
    public static void Set_State_Is_Using(long vehicle, bool toggle)
    {
        byte temp = Get_State(vehicle);
        if (toggle) temp = (byte)(temp | (1 << 1) & ~(1 << 0));
        else temp = (byte)(temp & ~(1 << 1) & ~(1 << 0));
        Set_State(vehicle, temp);
    }
    public static void Set_State_Is_Destroyed(long vehicle, bool toggle)
    {
        byte temp = Get_State(vehicle);
        if (toggle) temp = (byte)(temp | (1 << 1) | (1 << 0));
        else temp = (byte)(temp & ~(1 << 0));
        Set_State(vehicle, temp);
    }
    public static void Set_Health(long vehicle, float value) { Memory.Write<float>(vehicle + 0x280, value); }
    public static void Set_Max_Health(long vehicle, float value) { Memory.Write<float>(vehicle + 0x2A0, value); }
    public static void Set_Health2(long vehicle, float value) { Memory.Write<float>(vehicle + 0x840, value); }
    public static void Set_Health3(long vehicle, float value) { Memory.Write<float>(vehicle + 0x844, value); }
    public static void Set_Engine_Health(long vehicle, float value) { Memory.Write<float>(vehicle + 0x908, value); }
    public static void Set_Dirt_Level(long vehicle, float value) { Memory.Write<float>(vehicle + 0x9F8, value); }
    public static void Set_Gravity(long vehicle, float value) { Memory.Write<float>(vehicle + 0xC5C, value); }
}


public class Replayinterface
{
    public static List<long> Get_Vehicles()
    {
        List<long> vehicles = new List<long>();
        long p = Memory.Read<long>(Globals.ReplayInterfacePTR, new int[] { 0x10 });
        int max_num = Memory.Read<int>(p + 0x188);
        int count = Memory.Read<int>(p + 0x190);
        long list = Memory.Read<long>(p + 0x180);
        for (int i = 0; i < 300; i++)
        {
            long vehicle = Memory.Read<long>(list + i * 0x10);
            if (Memory.IsValid(vehicle)) vehicles.Add(vehicle);
        }
        return vehicles;
    }
    public static List<long> Get_Peds()
    {
        List<long> peds = new List<long>();
        long p = Memory.Read<long>(Globals.ReplayInterfacePTR, new int[] { 0x18 });
        int max_num = Memory.Read<int>(p + 0x108);
        int count = Memory.Read<int>(p + 0x110);
        long list = Memory.Read<long>(p + 0x100);
        for (int i = 0; i < 256; i++)
        {
            long ped = Memory.Read<long>(list + i * 0x10);
            if (Memory.IsValid(ped)) peds.Add(ped);
        }
        return peds;
    }
    public static List<long> Get_Pickups()
    {
        List<long> pickups = new List<long>();
        long p = Memory.Read<long>(Globals.ReplayInterfacePTR, new int[] { 0x20 });
        int max_num = Memory.Read<int>(p + 0x108);
        int count = Memory.Read<int>(p + 0x110);
        long list = Memory.Read<long>(p + 0x100);
        for (int i = 0; i < 256; i++)
        {
            long pickup = Memory.Read<long>(list + i * 0x10);
            if (Memory.IsValid(pickup)) pickups.Add(pickup);
        }
        return pickups;
    }
    public static List<long> Get_Objects()//https://github.com/Yimura/YimMenu/blob/2e57cd273ffa1cef5637efc60746a686516d58d4/BigBaseV2/src/gta/replay.hpp#L57
    {
        List<long> objects = new List<long>();
        long p = Memory.Read<long>(Globals.ReplayInterfacePTR, new int[] { 0x28 });
        int max_num = Memory.Read<int>(p + 0x160);
        int count = Memory.Read<int>(p + 0x168);
        long list = Memory.Read<long>(p + 0x158);
        for (int i = 0; i < 2300; i++)
        {
            long object_ = Memory.Read<long>(list + i * 0x10);
            if (Memory.IsValid(object_)) objects.Add(object_);
        }
        return objects;
    }
}

public class OnlinePlayer
{
    public static int Get_Number_Of_Players()
    {
        int number = 0;
        for (int i = 0; i < 32; i++)
        {
            if (Get_Player_Ped(i) == 0) continue;
            number++;
        }
        return number;
    }
    public static long Get_Player_Info(int i)
    {
        long CNetworkPlayerMgr = Memory.Read<long>(Globals.NetworkPlayerMgrPTR);
        if (!Memory.IsValid(CNetworkPlayerMgr)) return 0;
        long CNetGamePlayer = Memory.Read<long>(CNetworkPlayerMgr + 0x180 + i * 0x8);
        if (!Memory.IsValid(CNetGamePlayer)) return 0;
        long CPlayerInfo = Memory.Read<long>(CNetGamePlayer + 0xA0);
        if (!Memory.IsValid(CPlayerInfo)) return 0;
        return CPlayerInfo;
    }
    public static long Get_Player_Ped(int i)//0-31
    {
        long CNetworkPlayerMgr = Memory.Read<long>(Globals.NetworkPlayerMgrPTR);
        if (!Memory.IsValid(CNetworkPlayerMgr)) return 0;
        long CNetGamePlayer = Memory.Read<long>(CNetworkPlayerMgr + 0x180 + i * 0x8);
        if (!Memory.IsValid(CNetGamePlayer)) return 0;
        long CPlayerInfo = Memory.Read<long>(CNetGamePlayer + 0xA0);
        if (!Memory.IsValid(CPlayerInfo)) return 0;
        long CPed = PlayerInfo.Get_Ped(CPlayerInfo);
        if (!Memory.IsValid(CPed)) return 0;
        return CPed;
    }
}

public class Pickup : Entity
{
    public static uint Get_Pickup_Hash(long pickup) { return Memory.Read<uint>(pickup + 0x488); }
    public static uint Get_Pickup_Value(long pickup) { return Memory.Read<uint>(pickup + 0x490); }
    public static uint Get_Model_Hash() { return Memory.Read<uint>(Globals.ReplayInterfacePTR, new int[] { 0x20, 0xB0, 0x0, 0x490, 0xE80 }); }


    public static void Set_Pickup_Hash(long pickup, uint hash) { Memory.Write<uint>(pickup + 0x488, hash); }
    public static void Set_Pickup_Value(long pickup, uint value) { Memory.Write<uint>(pickup + 0x490, value); }
    public static void Set_Model_Hash(uint hash) { Memory.Write<uint>(Globals.ReplayInterfacePTR, new int[] { 0x20, 0xB0, 0x0, 0x490, 0xE80 }, hash); }
}


public class PlayerInfo
{
    public static uint Get_SC_Id(long playerInfo) { return Memory.Read<uint>(playerInfo + 0x90); }
    public static string Get_Name(long playerInfo) { return Memory.ReadString(playerInfo + 0xA4, null, 20); }
    public static float Get_Swim_Speed(long playerInfo) { return Memory.Read<float>(playerInfo + 0x170); }
    public static float Get_Stealth_Speed(long playerInfo) { return Memory.Read<float>(playerInfo + 0x18C); }
    public static long Get_Ped(long playerInfo) { return Memory.Read<long>(playerInfo + 0x1E8); }
    public static byte Get_Npc_Ignore(long playerInfo) { return Memory.Read<byte>(playerInfo + 0x872); }
    public static bool Get_Everyone_Ignore(long playerInfo) { return (((Get_Npc_Ignore(playerInfo) & (1 << 2)) == (1 << 2)) ? true : false); }
    public static bool Get_Cops_Ignore(long playerInfo) { return (((Get_Npc_Ignore(playerInfo) & ((1 << 0) + (1 << 1) + (1 << 6) + (1 << 7))) == ((1 << 0) + (1 << 1) + (1 << 6) + (1 << 7))) ? true : false); }
    public static int Get_Wanted_Level(long playerInfo) { return Memory.Read<int>(playerInfo + 0x888); }
    public static float Get_Run_Speed(long playerInfo) { return Memory.Read<float>(playerInfo + 0xCF0); }
    public static byte Get_Frame_Flags(long playerInfo) { return Memory.Read<byte>(playerInfo + 0x219); }
    public static bool Get_Frame_Flags_Explosiveammo(long playerInfo) { return (((Get_Frame_Flags(playerInfo) & (1 << 3)) == (1 << 3)) ? true : false); }
    public static bool Get_Frame_Flags_Flamingammo(long playerInfo) { return (((Get_Frame_Flags(playerInfo) & (1 << 4)) == (1 << 4)) ? true : false); }//fire ammo
    public static bool Get_Frame_Flags_Explosivefists(long playerInfo) { return (((Get_Frame_Flags(playerInfo) & (1 << 5)) == (1 << 5)) ? true : false); }//explosive punch
    public static bool Get_Frame_Flags_Superjump(long playerInfo) { return (((Get_Frame_Flags(playerInfo) & (1 << 6)) == (1 << 6)) ? true : false); }


    public static void Set_Swim_Speed(long playerinfo, float value) { Memory.Write<float>(playerinfo + 0x170, value); }
    public static void Set_Stealth_Speed(long playerinfo, float value) { Memory.Write<float>(playerinfo + 0x18C, value); }
    public static void Set_Npc_Ignore(long playerinfo, byte value) { Memory.Write<byte>(playerinfo + 0x872, value); }
    public static void Set_Everyone_Ignore(long playerinfo, bool toggle)
    {
        byte temp = Get_Npc_Ignore(playerinfo);
        if (toggle) temp = (byte)(temp | (1 << 2));
        else temp = (byte)(temp & ~(1 << 2));
        Set_Npc_Ignore(playerinfo, temp);
    }
    public static void Set_Cops_Ignore(long playerinfo, bool toggle)
    {
        byte temp = Get_Npc_Ignore(playerinfo);
        if (toggle) temp = (byte)(temp | ((1 << 0) + (1 << 1) + (1 << 6) + (1 << 7)));
        else temp = (byte)(temp & ~((1 << 0) + (1 << 1) + (1 << 6) + (1 << 7)));
        Set_Npc_Ignore(playerinfo, temp);
    }
    public static void Set_Wanted_Level(long playerinfo, int value) { Memory.Write<int>(playerinfo + 0x888, value); }
    public static void Set_Run_Speed(long playerinfo, float value) { Memory.Write<float>(playerinfo + 0xCF0, value); }
    public static void Set_Frame_Flags(long playerinfo, byte value) { Memory.Write<byte>(playerinfo + 0x219, value); }
    public static void Set_Frame_Flags_Explosiveammo(long playerinfo, bool toggle)
    {
        byte temp = Get_Frame_Flags(playerinfo);
        if (toggle) temp = (byte)(temp | (1 << 3));
        else temp = (byte)(temp & ~(1 << 3));
        Set_Frame_Flags(playerinfo, temp);
    }
    public static void Set_Frame_Flags_Flamingammo(long playerinfo, bool toggle)
    {
        byte temp = Get_Frame_Flags(playerinfo);
        if (toggle) temp = (byte)(temp | (1 << 4));
        else temp = (byte)(temp & ~(1 << 4));
        Set_Frame_Flags(playerinfo, temp);
    }
    public static void Set_Frame_Flags_Explosivefists(long playerinfo, bool toggle)
    {
        byte temp = Get_Frame_Flags(playerinfo);
        if (toggle) temp = (byte)(temp | (1 << 5));
        else temp = (byte)(temp & ~(1 << 5));
        Set_Frame_Flags(playerinfo, temp);
    }
    public static void Set_Frame_Flags_Superjump(long playerinfo, bool toggle)
    {
        byte temp = Get_Frame_Flags(playerinfo);
        if (toggle) temp = (byte)(temp | (1 << 6));
        else temp = (byte)(temp & ~(1 << 6));
        Set_Frame_Flags(playerinfo, temp);
    }
}


public class PedInventory
{
    public static bool Get_Infinite_Ammo(long pedInventory) { return (((Memory.Read<byte>(pedInventory + 0x78, new int[] { 0x78 }) & (1 << 0)) == (1 << 0)) ? true : false); }
    public static bool Get_Infinite_Clip(long pedInventory) { return (((Memory.Read<byte>(pedInventory + 0x78, new int[] { 0x78 }) & (1 << 1)) == (1 << 1)) ? true : false); }


    public static void Set_Infinite_Ammo(long pedInventory, bool toggle)
    {
        byte temp = Memory.Read<byte>(pedInventory + 0x78);
        if (toggle) temp = (byte)(temp | (1 << 0));
        else temp = (byte)(temp & ~(1 << 0));
        Memory.Write<byte>(pedInventory + 0x78, temp);
    }
    public static void Set_Infinite_Clip(long pedInventory, bool toggle)
    {
        byte temp = Memory.Read<byte>(pedInventory + 0x78);
        if (toggle) temp = (byte)(temp | (1 << 1));
        else temp = (byte)(temp & ~(1 << 1));
        Memory.Write<byte>(pedInventory + 0x78, temp);
    }
}

public class PedWeaponManager
{
    public static long Get_Weapon_Info(long pedWeaponManager) { return Memory.Read<long>(pedWeaponManager + 0x20); }
    public static short Get_Damage_Type(long pedWeaponManager) { return WeaponInfo.Get_Damage_Type(Get_Weapon_Info(pedWeaponManager)); }
    public static int Get_Explosion_Type(long pedWeaponManager) { return WeaponInfo.Get_Explosion_Type(Get_Weapon_Info(pedWeaponManager)); }
    public static int Get_Fire_Type(long pedWeaponManager) { return WeaponInfo.Get_Fire_Type(Get_Weapon_Info(pedWeaponManager)); }
    public static float Get_Spread(long pedWeaponManager) { return WeaponInfo.Get_Spread(Get_Weapon_Info(pedWeaponManager)); }
    public static float Get_Reload_Time_Multiplier(long pedWeaponManager) { return WeaponInfo.Get_Reload_Time_Multiplier(Get_Weapon_Info(pedWeaponManager)); }
    public static float Get_Lock_On_Range(long pedWeaponManager) { return WeaponInfo.Get_Lock_On_Range(Get_Weapon_Info(pedWeaponManager)); }
    public static float Get_Range(long pedWeaponManager) { return WeaponInfo.Get_Range(Get_Weapon_Info(pedWeaponManager)); }
    public static float Get_Recoil(long pedWeaponManager) { return WeaponInfo.Get_Recoil(Get_Weapon_Info(pedWeaponManager)); }


    public static void Set_Damage_Type(long pedWeaponManager, short value) { WeaponInfo.Set_Damage_Type(Get_Weapon_Info(pedWeaponManager), value); }
    public static void Set_Explosion_Type(long pedWeaponManager, int value) { WeaponInfo.Set_Explosion_Type(Get_Weapon_Info(pedWeaponManager), value); }
    public static void Set_Fire_Type(long pedWeaponManager, int value) { WeaponInfo.Set_Fire_Type(Get_Weapon_Info(pedWeaponManager), value); }
    public static void Set_Spread(long pedWeaponManager, float value) { WeaponInfo.Set_Spread(Get_Weapon_Info(pedWeaponManager), value); }
    public static void Set_Reload_Time_Multiplier(long pedWeaponManager, float value) { WeaponInfo.Set_Reload_Time_Multiplier(Get_Weapon_Info(pedWeaponManager), value); }
    public static void Set_Lock_On_Range(long pedWeaponManager, float value) { WeaponInfo.Set_Lock_On_Range(Get_Weapon_Info(pedWeaponManager), value); }
    public static void Set_Range(long pedWeaponManager, float value) { WeaponInfo.Set_Range(Get_Weapon_Info(pedWeaponManager), value); }
    public static void Set_Recoil(long pedWeaponManager, float value) { WeaponInfo.Set_Recoil(Get_Weapon_Info(pedWeaponManager), value); }
}