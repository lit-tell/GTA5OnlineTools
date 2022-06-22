using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Features.SDK;

public class Types
{
    public struct Vector3
    {
        public float x;
        public float y;
        public float z;
    }
}

public class Hacks
{
    public static long GlobalAddress(int address)
    {
        return Memory.Read<long>(Globals.GlobalPTR + 0x8 * ((address >> 0x12) & 0x3F)) + 8 * (address & 0x3FFFF);
    }

    public static T ReadGA<T>(int ga) where T : struct
    {
        return Memory.Read<T>(GlobalAddress(ga));
    }

    public static void WriteGA<T>(int ga, T vaule) where T : struct
    {
        Memory.Write<T>(GlobalAddress(ga), vaule);
    }

    public static string ReadGAString(int ga)
    {
        return Memory.ReadString(GlobalAddress(ga), null, 20);
    }

    public static void WriteGAString(int ga, string str)
    {
        Memory.WriteString(GlobalAddress(ga), null, str);
    }

    /*********************************************************/

    public static int GET_NETWORK_TIME()
    {
        return ReadGA<int>(1574755 + 11);
    }
    public static int PLAYER_ID()
    {
        return ReadGA<int>(2703660);
    }

    public static int GetBusinessIndex(int ID)
    {
        // ID 0-5
        return 1853131 + 1 + (PLAYER_ID() * 888) + 267 + 187 + 1 + (ID * 13);
    }

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

    /// <summary>
    /// 写入stat值，只支持int类型
    /// </summary>
    public static void WriteStat(string hash, int value)
    {
        if (hash.IndexOf("_") == 0)
        {
            int Stat_MP = ReadGA<int>(1574915);
            hash = $"MP{Stat_MP}{hash}";
        }

        uint Stat_ResotreHash = ReadGA<uint>(1655453 + 4);
        int Stat_ResotreValue = ReadGA<int>(1020252 + 5526);

        WriteGA<uint>(1655453 + 4, Joaat(hash));
        WriteGA<int>(1020252 + 5526, value);
        WriteGA<int>(1644218 + 1139, -1);
        Thread.Sleep(1000);
        WriteGA<uint>(1655453 + 4, Stat_ResotreHash);
        WriteGA<int>(1020252 + 5526, Stat_ResotreValue);
    }

    /// <summary>
    /// 掉落物品
    /// </summary>
    public static void CreateAmbientPickup(string pickup)
    {
        //uint modelHash = Joaat("prop_cash_pile_01");
        uint pickupHash = Joaat(pickup);

        float x = Memory.Read<float>(Globals.WorldPTR, Offsets.PlayerPositionX);
        float y = Memory.Read<float>(Globals.WorldPTR, Offsets.PlayerPositionY);
        float z = Memory.Read<float>(Globals.WorldPTR, Offsets.PlayerPositionZ);

        WriteGA<float>(2783345 + 3, x);
        WriteGA<float>(2783345 + 4, y);
        WriteGA<float>(2783345 + 5, z + 3.0f);
        WriteGA<int>(2783345 + 1, 9999);    // 9999

        WriteGA<int>(4528329 + 1 + (ReadGA<int>(2783345) * 85) + 66 + 2, 2);
        WriteGA<int>(2783345 + 6, 1);

        Thread.Sleep(150);

        var m_dwpPickUpInterface = Memory.Read<long>(Globals.ReplayInterfacePTR, new int[] { 0x20 });

        var dw_curPickUpNum = Memory.Read<long>(m_dwpPickUpInterface + 0x110, null);
        var m_dwpPedList = Memory.Read<long>(m_dwpPickUpInterface + 0x100, null);

        for (long i = 0; i < dw_curPickUpNum; i++)
        {
            long dwpPickup = Memory.Read<long>(m_dwpPedList + i * 0x10, null);
            uint dwPickupHash = Memory.Read<uint>(dwpPickup + 0x488, null);

            if (dwPickupHash == 4263048111)
            {
                Memory.Write<uint>(dwpPickup + 0x488, pickupHash);
                break;
            }
        }
    }

    public static Types.Vector3 GetBlipPos(int[] icons, int[] colors = null)
    {
        for (int i = 1; i < 2001; i++)
        {
            long p = Memory.Read<long>(Globals.BlipPTR + i * 0x8);
            if (p == 0) continue;
            int icon = Memory.Read<int>(p + 0x40);
            int color = Memory.Read<int>(p + 0x48);
            if (Array.IndexOf(icons, icon) == -1) continue;
            if (colors != null && Array.IndexOf(colors, color) == -1) continue;
            Types.Vector3 pos = new Types.Vector3();
            pos.x = Memory.Read<float>(p + 0x10);
            pos.y = Memory.Read<float>(p + 0x14);
            pos.z = Memory.Read<float>(p + 0x18);
            return pos;
        }
        return new Types.Vector3();
    }
}

public class Ped
{
    public static int pCNavigation = 0x30;
    public static int pCPlayerInfo = 0x10C8;
    public static int pCWeaponInventory = 0x10D0;

    public static float get_armor(long ped) { return Memory.Read<float>(ped + 0x1530); }
    public static long get_current_vehicle(long ped) { return Memory.Read<long>(ped + 0xD30); }
    public static bool get_godmode(long ped) { return ((Memory.Read<byte>(ped + 0x189) == 0) ? false : true); }
    public static float get_health(long ped) { return Memory.Read<float>(ped + 0x280); }
    public static float get_max_health(long ped) { return Memory.Read<float>(ped + 0x2A0); }
    public static bool get_infinite_ammo(long ped) { return (((Memory.Read<byte>(ped + pCWeaponInventory, new int[] {0x78}) & (1 << 0)) == (1 << 0)) ? true : false); }
    public static bool get_infinite_clip(long ped) { return (((Memory.Read<byte>(ped + pCWeaponInventory, new int[] { 0x78 }) & (1 << 1)) == (1 << 1)) ? true : false); }
    public static bool get_no_ragdoll(long ped) { return (((Memory.Read<byte>(ped + 0x10B8) & (1 << 5)) == (1 << 5)) ? false : true); }
    public static bool get_seatbelt(long ped) { return (((Memory.Read<byte>(ped + 0x145C) & (1 << 0)) == (1 << 0)) ? true : false); }
    public static Types.Vector3 get_position(long ped) { return Memory.Read<Types.Vector3>(ped + pCNavigation, new int[] { 0x50 }); }
    public static float get_run_speed(long ped) { return Memory.Read<float>(ped + pCPlayerInfo, new int[] {0xCF0}); }
    public static float get_swim_speed(long ped) { return Memory.Read<float>(ped + pCPlayerInfo, new int[] { 0x170 }); }
    public static float get_stealth_speed(long ped) { return Memory.Read<float>(ped + pCPlayerInfo, new int[] { 0x18C }); }
    public static int get_wanted_level(long ped) { return Memory.Read<int>(ped + pCPlayerInfo, new int[] {0x888}); }
    public static bool is_in_vehicle(long ped) { return ((Memory.Read<byte>(ped + 0xE52) == 1) ? true : false); }


    public static void set_armour(long ped, float value) { Memory.Write<float>(ped + 0x1530, value); }
    public static void set_godmode(long ped, bool toggle) 
    {
        byte temp = Memory.Read<byte>(ped + 0x189);
        if (toggle) temp = (byte)(temp | (1 << 0));
        else temp = (byte)(temp & ~(1 << 0));
        Memory.Write<byte>(ped + 0x189, temp);
    }
    public static void set_health(long ped, float value) { Memory.Write<float>(ped + 0x280, value); }
    public static void set_max_health(long ped, float value) { Memory.Write<float>(ped + 0x2A0, value); }
    public static void set_infinite_ammo(long ped, bool toggle)
    {
        byte temp = Memory.Read<byte>(ped + pCWeaponInventory, new int[] { 0x78 });
        if (toggle) temp = (byte)(temp | (1 << 0));
        else temp = (byte)(temp & ~(1 << 0));
        Memory.Write<byte>(ped + pCWeaponInventory, new int[] { 0x78 }, temp);
    }
    public static void set_infinite_clip(long ped, bool toggle)
    {
        byte temp = Memory.Read<byte>(ped + pCWeaponInventory, new int[] { 0x78 });
        if (toggle) temp = (byte)(temp | (1 << 1));
        else temp = (byte)(temp & ~(1 << 1));
        Memory.Write<byte>(ped + pCWeaponInventory, new int[] { 0x78 }, temp);
    }
    public static void set_no_ragdoll(long ped, bool toggle)
    {
        byte temp = Memory.Read<byte>(ped + 0x10B8);
        if (toggle) temp = (byte)(temp | (1 << 5));
        else temp = (byte)(temp & ~(1 << 5));
        Memory.Write<byte>(ped + 0x10B8, temp);
    }
    public static void get_seatbelt(long ped, bool toggle)
    {
        byte temp = Memory.Read<byte>(ped + 0x145C);
        if (toggle) temp = (byte)(temp | (1 << 0));
        else temp = (byte)(temp & ~(1 << 0));
        Memory.Write<byte>(ped + 0x145C, temp);
    }
    public static void set_position(long ped, Types.Vector3 pos)
    {
        Memory.Write<Types.Vector3>(ped + 0x90, pos);
        Memory.Write<Types.Vector3>(ped + pCNavigation, new int[] { 0x50 }, pos);
    }
    public static void set_run_speed(long ped, float value) { Memory.Write<float>(ped + pCPlayerInfo, new int[] { 0xCF0 }, value); }
    public static void set_swim_speed(long ped, float value) { Memory.Write<float>(ped + pCPlayerInfo, new int[] { 0x170 }, value); }
    public static void set_stealth_speed(long ped, float value) { Memory.Write<float>(ped + pCPlayerInfo, new int[] { 0x18C }, value); }
    public static void set_wanted_level(long ped, int value) { Memory.Write<int>(ped + pCPlayerInfo, new int[] { 0x888 }, value); }
}