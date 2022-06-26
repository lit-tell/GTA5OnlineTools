using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Features.SDK;

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
    public static void CreateAmbientPickup(int amount, Vector3 pos)
    {
        WriteGA<float>(2783345 + 3, pos.X);
        WriteGA<float>(2783345 + 4, pos.Y);
        WriteGA<float>(2783345 + 5, pos.Z);
        WriteGA<int>(2783345 + 1, amount);
        WriteGA<int>(4528329 + 1 + (ReadGA<int>(2783345) * 85) + 66 + 2, 2);
        WriteGA<int>(2783345 + 6, 1);
    }

    public static long GetBlip(int[] icons, int[] colors = null)
    {
        for (int i = 1; i < 2001; i++)
        {
            long p = Memory.Read<long>(Globals.BlipPTR + i * 0x8);
            if (p == 0) continue;
            int icon = Memory.Read<int>(p + 0x40);
            int color = Memory.Read<int>(p + 0x48);
            if (Array.IndexOf(icons, icon) == -1) continue;
            if (colors != null && Array.IndexOf(colors, color) == -1) continue;
            return p;
        }
        return 0;
    }

    public static Vector3 GetBlipPos(int[] icons, int[] colors = null)
    {
        long blip = GetBlip(icons, colors);
        return ((blip == 0) ? new Vector3() : Memory.Read<Vector3>(blip + 0x10));
    }

    public static long GetLocalPed() { return Memory.Read<long>(Globals.WorldPTR, new int[] { 0x8 }); }
    public static bool Entity_is_player(long entity) { return ((Entity.get_type(entity) == 156) ? true : false); }
    public static bool Ped_is_player(long ped) { return Entity_is_player(ped); }
    public static Vector3 Entity_get_forwardpos(long entity, float dist = 7.0f)
    {
        long navigation = Entity.get_navigation(entity);
        Vector3 vec = Navigation.get_right_vector3(navigation);
        Vector3 pos = Navigation.get_position(navigation);
        pos.X -= dist * vec.Y;
        pos.Y += dist * vec.X;
        return pos;
    }
    public static Vector3 Ped_get_forwardpos(long ped, float dist = 7.0f) { return Entity_get_forwardpos(ped, dist); }
    public static void Entity_set_entity_coords(long entity, Vector3 pos) 
    {
        long navigation = Entity.get_navigation(entity);
        Navigation.set_position(navigation, pos);
        Entity.set_position(entity, pos);
    }
    public static void TeleportToCoords(long ped, Vector3 pos)
    {
        long entity = (Ped.is_in_vehicle(ped) ? Ped.get_current_vehicle(ped) : ped);
        Entity_set_entity_coords(ped, pos);
    }
    public static void TeleportToCoordsWithCheck(long ped, Vector3 pos)
    {
        if (pos.X == 0.0f && pos.Y == 0.0f && pos.Z == 0.0f) return;
        TeleportToCoords(ped, pos);
    }
    public static uint Entity_get_hash(long entity) { return BaseModelInfo.get_hash(Entity.get_basemodelinfo(entity)); }
    public static void SpawnDrop(uint hash, Vector3 pos)
    {
        CreateAmbientPickup(9999, pos);
        Thread.Sleep(150);
        List<long> pickups = Replayinterface.get_pickups();
        for (int i = 0; i < pickups.Count; i++)
        {
            long pickup = pickups[i];
            if (Entity_get_hash(pickup) == Joaat("prop_cash_pile_01")) //if (Pickup.get_pickup_hash(pickup) == 4263048111)
            {
                Pickup.set_pickup_hash(pickup, hash);
                break;
            }
        }
    }
    public static void SpawnDrop(long ped, uint hash, float dist = 0.0f, float height = 3.0f)
    {
        Vector3 pos = Ped_get_forwardpos(ped, dist);
        pos.Z += height;
        SpawnDrop(hash, pos);
    }
    public static void SpawnDrop(long ped, string name, float dist = 0.0f, float height = 3.0f) { SpawnDrop(ped, Joaat(name), dist, height); }
    public static bool Ped_get_infinite_ammo(long ped) { return WeaponInventory.get_infinite_ammo(Ped.get_weaponinventory(ped)); }
    public static bool Ped_get_infinite_clip(long ped) { return WeaponInventory.get_infinite_clip(Ped.get_weaponinventory(ped)); }
    public static void Ped_set_infinite_ammo(long ped, bool toggle) { WeaponInventory.set_infinite_ammo(Ped.get_weaponinventory(ped), toggle); }
    public static void Ped_set_infinite_clip(long ped, bool toggle) { WeaponInventory.set_infinite_clip(Ped.get_weaponinventory(ped), toggle); }
    public static bool Ped_is_enemy(long ped) { return ((Ped.get_hostility(ped) > 1) ? true : false); }
    public static bool Ped_get_frame_flags_explosiveammo(long ped) { return PlayerInfo.get_frame_flags_explosiveammo(Ped.get_playerinfo(ped)); }
    public static bool Ped_get_frame_flags_flamingammo(long ped) { return PlayerInfo.get_frame_flags_flamingammo(Ped.get_playerinfo(ped)); }
    public static bool Ped_get_frame_flags_explosivefists(long ped) { return PlayerInfo.get_frame_flags_explosivefists(Ped.get_playerinfo(ped)); }
    public static bool Ped_get_frame_flags_superjump(long ped) { return PlayerInfo.get_frame_flags_superjump(Ped.get_playerinfo(ped)); }
    public static void Ped_set_frame_flags_explosiveammo(long ped, bool toggle) { PlayerInfo.set_frame_flags_explosiveammo(Ped.get_playerinfo(ped), toggle); }
    public static void Ped_set_frame_flags_flamingammo(long ped, bool toggle) { PlayerInfo.set_frame_flags_flamingammo(Ped.get_playerinfo(ped), toggle); }
    public static void Ped_set_frame_flags_explosivefists(long ped, bool toggle) { PlayerInfo.set_frame_flags_explosivefists(Ped.get_playerinfo(ped), toggle); }
    public static void Ped_set_frame_flags_superjump(long ped, bool toggle) { PlayerInfo.set_frame_flags_superjump(Ped.get_playerinfo(ped), toggle); }
    public static int Ped_get_wanted_level(long ped) { return PlayerInfo.get_wanted_level((Ped.get_playerinfo(ped))); }
    public static void Ped_set_wanted_level(long ped, int value) { PlayerInfo.set_wanted_level(Ped.get_playerinfo(ped), value); }
    public static float Ped_get_run_speed(long ped) { return PlayerInfo.get_run_speed(Ped.get_playerinfo(ped)); }
    public static float Ped_get_swim_speed(long ped) { return PlayerInfo.get_swim_speed(Ped.get_playerinfo(ped)); }
    public static float Ped_get_stealth_speed(long ped) { return PlayerInfo.get_stealth_speed(Ped.get_playerinfo(ped)); }
    public static void Ped_set_run_speed(long ped, float value) { PlayerInfo.set_run_speed(Ped.get_playerinfo(ped), value); }
    public static void Ped_set_swim_speed(long ped, float value) { PlayerInfo.set_swim_speed(Ped.get_playerinfo(ped), value); }
    public static void Ped_set_stealth_speed(long ped, float value) { PlayerInfo.set_stealth_speed(Ped.get_playerinfo(ped), value); }
}


public class BaseModelInfo
{
    public static uint get_hash(long basemodelinfo) { return Memory.Read<uint>(basemodelinfo + 0x18); }
    public static byte get_type(long basemodelinfo) { return Memory.Read<byte>(basemodelinfo + 0x9D); }
}


public class Navigation
{
    public static Vector3 get_right_vector3(long navigation) { return Memory.Read<Vector3>(navigation + 0x20); }
    public static Vector3 get_forward_vector3(long navigation) { return Memory.Read<Vector3>(navigation + 0x30); }
    public static Vector3 get_up_vector3(long navigation) { return Memory.Read<Vector3>(navigation + 0x40); }
    public static Vector3 get_position(long navigation) { return Memory.Read<Vector3>(navigation + 0x50); }


    public static void set_right_vector3(long navigation, Vector3 pos) { Memory.Write<Vector3>(navigation + 0x20, pos); }
    public static void set_forward_vector3(long navigation, Vector3 pos) { Memory.Write<Vector3>(navigation + 0x30, pos); }
    public static void set_up_vector3(long navigation, Vector3 pos) { Memory.Write<Vector3>(navigation + 0x40, pos); }
    public static void set_position(long navigation, Vector3 pos) { Memory.Write<Vector3>(navigation + 0x50, pos); }
}


public class Entity
{
    public static long get_basemodelinfo(long entity) { return Memory.Read<long>(entity + 0x20); }
    public static byte get_type(long entity) { return Memory.Read<byte>(entity + 0x28); }
    public static byte get_type2(long entity) { return Memory.Read<byte>(entity + 0x29); }
    public static long get_navigation(long entity) { return Memory.Read<long>(entity + 0x30); }
    public static Vector3 get_right_vector3(long entity) { return Memory.Read<Vector3>(entity + 0x60); }
    public static Vector3 get_forward_vector3(long entity) { return Memory.Read<Vector3>(entity + 0x70); }
    public static Vector3 get_up_vector3(long entity) { return Memory.Read<Vector3>(entity + 0x80); }
    public static Vector3 get_position(long entity) { return Memory.Read<Vector3>(entity + 0x90); }
    public static bool get_godmode(long entity) { return ((Memory.Read<byte>(entity + 0x189) == 0) ? false : true); }//invincible
    public static bool get_waterproof(long entity) { return ((Memory.Read<byte>(entity + 0x18B) == 0) ? false : true); }


    public static void set_right_vector3(long entity, Vector3 pos) { Memory.Write<Vector3>(entity + 0x60, pos); }
    public static void set_forward_vector3(long entity, Vector3 pos) { Memory.Write<Vector3>(entity + 0x70, pos); }
    public static void set_up_vector3(long entity, Vector3 pos) { Memory.Write<Vector3>(entity + 0x80, pos); }
    public static void set_position(long entity, Vector3 pos) { Memory.Write<Vector3>(entity + 0x90, pos); }
    public static void set_godmode(long entity, bool toggle)
    {
        byte temp = Memory.Read<byte>(entity + 0x189);
        if (toggle) temp = (byte)(temp | (1 << 0));
        else temp = (byte)(temp & ~(1 << 0));
        Memory.Write<byte>(entity + 0x189, temp);
    }
    public static void set_waterproof(long entity, bool toggle)
    {
        byte temp = Memory.Read<byte>(entity + 0x18B);
        if (toggle) temp = (byte)(temp | (1 << 0));
        else temp = (byte)(temp & ~(1 << 0));
        Memory.Write<byte>(entity + 0x18B, temp);
    }
}


public class WeaponInventory
{
    public static bool get_infinite_ammo(long weaponinventory) { return (((Memory.Read<byte>(weaponinventory + 0x78, new int[] { 0x78 }) & (1 << 0)) == (1 << 0)) ? true : false); }
    public static bool get_infinite_clip(long weaponinventory) { return (((Memory.Read<byte>(weaponinventory + 0x78, new int[] { 0x78 }) & (1 << 1)) == (1 << 1)) ? true : false); }


    public static void set_infinite_ammo(long weaponinventory, bool toggle)
    {
        byte temp = Memory.Read<byte>(weaponinventory + 0x78);
        if (toggle) temp = (byte)(temp | (1 << 0));
        else temp = (byte)(temp & ~(1 << 0));
        Memory.Write<byte>(weaponinventory + 0x78, temp);
    }
    public static void set_infinite_clip(long weaponinventory, bool toggle)
    {
        byte temp = Memory.Read<byte>(weaponinventory + 0x78);
        if (toggle) temp = (byte)(temp | (1 << 1));
        else temp = (byte)(temp & ~(1 << 1));
        Memory.Write<byte>(weaponinventory + 0x78, temp);
    }
}


public class Ped : Entity
{
    public static byte get_hostility(long ped) { return Memory.Read<byte>(ped + 0x18C); }
    public static float get_health(long ped) { return Memory.Read<float>(ped + 0x280); }
    public static float get_max_health(long ped) { return Memory.Read<float>(ped + 0x2A0); }
    public static long get_current_vehicle(long ped) { return Memory.Read<long>(ped + 0xD30); }
    public static bool is_in_vehicle(long ped) { return ((Memory.Read<byte>(ped + 0xE52) == 1) ? true : false); }
    public static uint get_pedtype(long ped) { return Memory.Read<uint>(ped + 0x10B8) << 11 >> 25; }
    public static bool get_no_ragdoll(long ped) { return (((Memory.Read<byte>(ped + 0x10B8) & (1 << 5)) == (1 << 5)) ? false : true); }
    public static long get_playerinfo(long ped) { return Memory.Read<long>(ped + 0x10C8); }
    public static long get_weaponinventory(long ped) { return Memory.Read<long>(ped + 0x10D0); }
    public static bool get_seatbelt(long ped) { return (((Memory.Read<byte>(ped + 0x145C) & (1 << 0)) == (1 << 0)) ? true : false); }
    public static float get_armor(long ped) { return Memory.Read<float>(ped + 0x1530); }


    public static void set_health(long ped, float value) { Memory.Write<float>(ped + 0x280, value); }
    public static void set_max_health(long ped, float value) { Memory.Write<float>(ped + 0x2A0, value); }
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
    public static void set_armour(long ped, float value) { Memory.Write<float>(ped + 0x1530, value); }
}


public class Vehicle : Entity
{
    public static float get_health(long vehicle) { return Memory.Read<float>(vehicle + 0x280); }
    public static float get_max_health(long vehicle) { return Memory.Read<float>(vehicle + 0x2A0); }
    public static float get_health2(long vehicle) { return Memory.Read<float>(vehicle + 0x840); }
    public static float get_health3(long vehicle) { return Memory.Read<float>(vehicle + 0x844); }
    public static float get_engine_health(long vehicle) { return Memory.Read<float>(vehicle + 0x908); }
    public static float get_gravity(long vehicle) { return Memory.Read<float>(vehicle + 0xC5C); }
    public static byte get_cur_num_of_passenger(long vehicle) { return Memory.Read<byte>(vehicle + 0xC62); }


    public static void set_health(long vehicle, float value) { Memory.Write<float>(vehicle + 0x280, value); }
    public static void set_max_health(long vehicle, float value) { Memory.Write<float>(vehicle + 0x2A0, value); }
    public static void set_health2(long vehicle, float value) { Memory.Write<float>(vehicle + 0x840, value); }
    public static void set_health3(long vehicle, float value) { Memory.Write<float>(vehicle + 0x844, value); }
    public static void set_engine_health(long vehicle, float value) { Memory.Write<float>(vehicle + 0x908, value); }
    public static void set_gravity(long vehicle, float value) { Memory.Write<float>(vehicle + 0xC5C, value); }
}


public class Replayinterface
{
    public static List<long> get_vehicles()
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
    public static List<long> get_peds()
    {
        List<long> peds = new List<long>();
        long p = Memory.Read<long>(Globals.ReplayInterfacePTR, new int[] {0x18});
        int max_num = Memory.Read<int>(p + 0x108);
        int count = Memory.Read<int>(p + 0x110);
        long list = Memory.Read<long>(p + 0x100);
        for(int i = 0; i < 256; i++)
        {
            long ped = Memory.Read<long>(list + i * 0x10);
            if(Memory.IsValid(ped)) peds.Add(ped);
        }
        return peds;
    }
    public static List<long> get_pickups()
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
    public static List<long> get_objects()//https://github.com/Yimura/YimMenu/blob/2e57cd273ffa1cef5637efc60746a686516d58d4/BigBaseV2/src/gta/replay.hpp#L57
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
    public static int get_number_of_players()
    {
        int number = 0;
        for(int i = 0; i < 32; i++)
        {
            if (get_player_ped(i) == 0) continue;
            number++;
        }
        return number;
    }
    public static long get_player_info(int i)
    {
        long CNetworkPlayerMgr = Memory.Read<long>(Globals.NetworkPlayerMgrPTR);
        if (!Memory.IsValid(CNetworkPlayerMgr)) return 0;
        long CNetGamePlayer = Memory.Read<long>(CNetworkPlayerMgr + 0x180 + i * 0x8);
        if (!Memory.IsValid(CNetGamePlayer)) return 0;
        long CPlayerInfo = Memory.Read<long>(CNetGamePlayer + 0xA0);
        if (!Memory.IsValid(CPlayerInfo)) return 0;
        return CPlayerInfo;
    }
    public static long get_player_ped(int i)//0-31
    {
        long CNetworkPlayerMgr = Memory.Read<long>(Globals.NetworkPlayerMgrPTR);
        if (!Memory.IsValid(CNetworkPlayerMgr)) return 0;
        long CNetGamePlayer = Memory.Read<long>(CNetworkPlayerMgr + 0x180 + i * 0x8);
        if (!Memory.IsValid(CNetGamePlayer)) return 0;
        long CPlayerInfo = Memory.Read<long>(CNetGamePlayer + 0xA0);
        if (!Memory.IsValid(CPlayerInfo)) return 0;
        long CPed = PlayerInfo.get_ped(CPlayerInfo);
        if (!Memory.IsValid(CPed)) return 0;
        return CPed;
    }
}

public class Pickup : Entity
{
    public static uint get_pickup_hash(long pickup) { return Memory.Read<uint>(pickup + 0x488); }
    public static uint get_model_hash() { return Memory.Read<uint>(Globals.ReplayInterfacePTR, new int[] { 0x20, 0xB0, 0x0, 0x490, 0xE80 }); }


    public static void set_pickup_hash(long pickup, uint hash) { Memory.Write<uint>(pickup + 0x488, hash); }
    public static void set_model_hash(uint hash) { Memory.Write<uint>(Globals.ReplayInterfacePTR, new int[] { 0x20, 0xB0, 0x0, 0x490, 0xE80 }, hash); }
}


public class PlayerInfo
{
    public static uint get_scid(long playerinfo) { return Memory.Read<uint>(playerinfo + 0x90); }
    public static string get_name(long playerinfo) { return Memory.ReadString(playerinfo + 0xA4, null, 20); }
    public static float get_swim_speed(long playerinfo) { return Memory.Read<float>(playerinfo + 0x170); }
    public static float get_stealth_speed(long playerinfo) { return Memory.Read<float>(playerinfo + 0x18C); }
    public static long get_ped(long playerinfo) { return Memory.Read<long>(playerinfo + 0x1E8); }
    public static int get_wanted_level(long playerinfo) { return Memory.Read<int>(playerinfo + 0x888); }
    public static float get_run_speed(long playerinfo) { return Memory.Read<float>(playerinfo + 0xCF0); }
    public static byte get_frame_flags(long playerinfo) { return Memory.Read<byte>(playerinfo + 0x219); }
    public static bool get_frame_flags_explosiveammo(long playerinfo) { return (((get_frame_flags(playerinfo) & (1 << 3)) == (1 << 3)) ? true : false); }
    public static bool get_frame_flags_flamingammo(long playerinfo) { return (((get_frame_flags(playerinfo) & (1 << 4)) == (1 << 4)) ? true : false); }//fire ammo
    public static bool get_frame_flags_explosivefists(long playerinfo) { return (((get_frame_flags(playerinfo) & (1 << 5)) == (1 << 5)) ? true : false); }//explosive punch
    public static bool get_frame_flags_superjump(long playerinfo) { return (((get_frame_flags(playerinfo) & (1 << 6)) == (1 << 6)) ? true : false); }


    public static void set_swim_speed(long playerinfo, float value) { Memory.Write<float>(playerinfo + 0x170, value); }
    public static void set_stealth_speed(long playerinfo, float value) { Memory.Write<float>(playerinfo + 0x18C, value); }
    public static void set_wanted_level(long playerinfo, int value) { Memory.Write<int>(playerinfo + 0x888, value); }
    public static void set_run_speed(long playerinfo, float value) { Memory.Write<float>(playerinfo + 0xCF0, value); }
    public static void set_frame_flags(long playerinfo, byte value) { Memory.Write<byte>(playerinfo + 0x219, value); }
    public static void set_frame_flags_explosiveammo(long playerinfo, bool toggle) 
    {
        byte temp = get_frame_flags(playerinfo);
        if (toggle) temp = (byte)(temp | (1 << 3));
        else temp = (byte)(temp & ~(1 << 3));
        set_frame_flags(playerinfo, temp);
    }
    public static void set_frame_flags_flamingammo(long playerinfo, bool toggle)
    {
        byte temp = get_frame_flags(playerinfo);
        if (toggle) temp = (byte)(temp | (1 << 4));
        else temp = (byte)(temp & ~(1 << 4));
        set_frame_flags(playerinfo, temp);
    }
    public static void set_frame_flags_explosivefists(long playerinfo, bool toggle)
    {
        byte temp = get_frame_flags(playerinfo);
        if (toggle) temp = (byte)(temp | (1 << 5));
        else temp = (byte)(temp & ~(1 << 5));
        set_frame_flags(playerinfo, temp);
    }
    public static void set_frame_flags_superjump(long playerinfo, bool toggle)
    {
        byte temp = get_frame_flags(playerinfo);
        if (toggle) temp = (byte)(temp | (1 << 6));
        else temp = (byte)(temp & ~(1 << 6));
        set_frame_flags(playerinfo, temp);
    }
}