using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Features.SDK;

public partial class Hacks
{
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
}

public partial class Hacks
{
    public static long get_blip(int[] icons, int[] colors = null)
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

    public static Vector3 get_blip_pos(int[] icons, int[] colors = null)
    {
        long blip = get_blip(icons, colors);
        return ((blip == 0) ? new Vector3() : Memory.Read<Vector3>(blip + 0x10));
    }

    public static long get_local_ped() { return Memory.Read<long>(Globals.WorldPTR, new int[] { 0x8 }); }

    /// <summary>
    /// 传送到导航点
    /// </summary>
    public static void to_waypoint()
    {
        Vector3 pos = get_blip_pos(new int[] { 8 }, new int[] { 84 });
        if (pos.X == 0.0f && pos.Y == 0.0f && pos.Z == 0.0f) return;
        pos.Z = pos.Z == 20.0f ? -225.0f : pos.Z + 1.0f;
        to_coords(get_local_ped(), pos);
    }

    public static void to_objective()
    {
        Vector3 pos = get_blip_pos(new int[] { 1 }, new int[] { 5, 60, 66 });
        if (pos.X == 0.0f && pos.Y == 0.0f && pos.Z == 0.0f) pos = get_blip_pos(new int[] { 1, 225, 427, 478, 501, 523, 556 }, new int[] { 1, 2, 3, 54, 78 });
        if (pos.X == 0.0f && pos.Y == 0.0f && pos.Z == 0.0f) pos = get_blip_pos(new int[] { 432, 443 }, new int[] { 59 });
        to_coords_with_check(get_local_ped(), pos);
    }

    public static void to_blip(int[] icons, int[] colors = null)
    {
        Vector3 pos = get_blip_pos(icons, colors);
        to_coords_with_check(get_local_ped(), pos);
    }

    public static void to_coords(long ped, Vector3 pos)
    {
        long entity = (Ped.is_in_vehicle(ped) ? Ped.get_current_vehicle(ped) : ped);
        Entity.set_position(entity, pos);
    }

    public static void to_coords_with_check(long ped, Vector3 pos)
    {
        if (pos.X == 0.0f && pos.Y == 0.0f && pos.Z == 0.0f) return;
        to_coords(ped, pos);
    }

    public static void spawn_drop(uint hash, Vector3 pos)
    {
        Globals.create_ambient_pickup(9999, pos);
        Thread.Sleep(150);
        List<long> pickups = Replayinterface.get_pickups();
        for (int i = 0; i < pickups.Count; i++)
        {
            long pickup = pickups[i];
            if (Entity.get_model_hash(pickup) == Joaat("prop_cash_pile_01")) //if (Pickup.get_pickup_hash(pickup) == 4263048111)
            {
                Pickup.set_pickup_hash(pickup, hash);
                break;
            }
        }
    }

    public static void spawn_drop(long ped, uint hash, float dist = 0.0f, float height = 3.0f)
    {
        Vector3 pos = Ped.get_real_forwardpos(ped, dist);
        pos.Z += height;
        spawn_drop(hash, pos);
    }
    public static void spawn_drop(long ped, string name, float dist = 0.0f, float height = 3.0f) { spawn_drop(ped, Joaat(name), dist, height); }

    public static void kill_npcs() 
    {
        List<long> peds = Replayinterface.get_peds();
        for(int i = 0;i < peds.Count; i++)
        {
            long ped = peds[i];
            if (Ped.is_player(ped)) continue;
            Ped.set_health(ped, 0.0f);
        }
    }
    public static void kill_enemies() 
    {
        List<long> peds = Replayinterface.get_peds();
        for (int i = 0; i < peds.Count; i++)
        {
            long ped = peds[i];
            if (Ped.is_player(ped)) continue;
            if (Ped.is_enemy(ped)) Ped.set_health(ped, 0.0f);
        }
    }
    public static void kill_cops()
    {
        List<long> peds = Replayinterface.get_peds();
        for (int i = 0; i < peds.Count; i++)
        {
            long ped = peds[i];
            if (Ped.is_player(ped)) continue;
            uint pedtype = Ped.get_pedtype(ped);
            if(pedtype == (uint)Data.EnumData.PedTypes.COP ||
                pedtype == (uint)Data.EnumData.PedTypes.SWAT ||
                pedtype == (uint)Data.EnumData.PedTypes.ARMY) Ped.set_health(ped, 0.0f);
        }
    }
    public static void revive_vehicle(long vehicle)
    {
        Vehicle.set_state_is_destroyed(vehicle, false);
        Vehicle.set_health(vehicle, 1000.0f);
        Vehicle.set_health2(vehicle, 1000.0f);
        Vehicle.set_health3(vehicle, 1000.0f);
        Vehicle.set_engine_health(vehicle, 1000.0f);
    }
    public static void destroy_vehicle(long vehicle)
    {
        revive_vehicle(vehicle);
        //Vehicle.set_health(vehicle, 0.0f);
        //Vehicle.set_health2(vehicle, 0.0f);
        Vehicle.set_health3(vehicle, -999.9f);//-1000.0f
        //Vehicle.set_engine_health(vehicle, -3999.0f);//-4000.0f
    }
    public static void destroy_vehs_of_npcs()
    {
        List<long> peds = Replayinterface.get_peds();
        for (int i = 0; i < peds.Count; i++)
        {
            long ped = peds[i];
            if (Ped.is_player(ped)) continue;
            destroy_vehicle(Ped.get_current_vehicle(ped));
        }
    }
    public static void destroy_vehs_of_enemies()
    {
        List<long> peds = Replayinterface.get_peds();
        for (int i = 0; i < peds.Count; i++)
        {
            long ped = peds[i];
            if (ped == Hacks.get_local_ped()) continue;
            if (Ped.is_enemy(ped)) destroy_vehicle(Ped.get_current_vehicle(ped));
        }
    }
    public static void destroy_vehs_of_cops()
    {
        List<long> peds = Replayinterface.get_peds();
        for (int i = 0; i < peds.Count; i++)
        {
            long ped = peds[i];
            if (ped == Hacks.get_local_ped()) continue;
            uint pedtype = Ped.get_pedtype(ped);
            if (pedtype == (uint)Data.EnumData.PedTypes.COP ||
                pedtype == (uint)Data.EnumData.PedTypes.SWAT ||
                pedtype == (uint)Data.EnumData.PedTypes.ARMY) destroy_vehicle(Ped.get_current_vehicle(ped));
        }
    }
    /// <summary>
    /// 摧毁附近所有载具
    /// </summary>
    public static void destroy_all_vehicles()
    {
        List<long> vehicles = Replayinterface.get_vehicles();
        for (int i = 0; i < vehicles.Count; i++)
        {
            long vehicle = vehicles[i];
            destroy_vehicle(vehicle);
        }
    }
    /// <summary>
    /// 复活附近所有载具
    /// </summary>
    public static void revive_all_vehicles()
    {
        List<long> vehicles = Replayinterface.get_vehicles();
        for(int i = 0; i < vehicles.Count; i++)
        {
            long vehicle = vehicles[i];
            revive_vehicle(vehicle);
        }
    }
    public static void tp_npcs_to_me()
    {
        List<long> peds = Replayinterface.get_peds();
        for (int i = 0; i < peds.Count; i++)
        {
            long ped = peds[i];
            if (Ped.is_player(ped)) continue;
            Ped.set_position(ped, Ped.get_real_forwardpos(get_local_ped(), 5.0f));
        }
    }
    public static void tp_enemies_to_me()
    {
        List<long> peds = Replayinterface.get_peds();
        for (int i = 0; i < peds.Count; i++)
        {
            long ped = peds[i];
            if (Ped.is_player(ped)) continue;
            if (Ped.is_enemy(ped)) Ped.set_position(ped, Ped.get_real_forwardpos(get_local_ped(), 5.0f));
        }
    }
    public static void tp_not_enemies_to_me()
    {
        List<long> peds = Replayinterface.get_peds();
        for (int i = 0; i < peds.Count; i++)
        {
            long ped = peds[i];
            if (Ped.is_player(ped)) continue;
            if (!Ped.is_enemy(ped)) Ped.set_position(ped, Ped.get_real_forwardpos(get_local_ped(), 5.0f));
        }
    }
    public static void tp_cops_to_me()
    {
        List<long> peds = Replayinterface.get_peds();
        for (int i = 0; i < peds.Count; i++)
        {
            long ped = peds[i];
            if (Ped.is_player(ped)) continue;
            uint pedtype = Ped.get_pedtype(ped);
            if (pedtype == (uint)Data.EnumData.PedTypes.COP ||
                pedtype == (uint)Data.EnumData.PedTypes.SWAT ||
                pedtype == (uint)Data.EnumData.PedTypes.ARMY) Ped.set_position(ped, Ped.get_real_forwardpos(get_local_ped(), 5.0f));
        }
    }

    public static void set_local_weather(int weatherID)
    {
        /*
         -1:Default
         0:Extra Sunny
         1:Clear
         2:Clouds
         3:Smog
         4:Foggy
         5:Overcast
         6:Rain
         7:Thunder
         8:Light Rain
         9:Smoggy Light Rain
         10:Very Light Snow
         11:Windy Snow
         12:Light Snow
         14:Halloween
         */
        if(weatherID == -1)
        {
            Memory.Write(Globals.WeatherPTR + 0x24, -1);
            Memory.Write(Globals.WeatherPTR + 0x104, 13);
        }
        if(weatherID == 13)
        {
            Memory.Write(Globals.WeatherPTR + 0x24, 13);
            Memory.Write(Globals.WeatherPTR + 0x104, 13);
        }
        Memory.Write(Globals.WeatherPTR + 0x104, weatherID);
    }

    public static void empty_session()
    {
        Task.Run(() =>
        {
            ProcessMgr.SuspendProcess(Memory.GetProcessID());
            Task.Delay(10000).Wait();
            ProcessMgr.ResumeProcess(Memory.GetProcessID());
        });
    }

    public static uint get_fix_veh_value() { return Memory.Read<uint>(Globals.PickupDataPTR, new int[] { 0x228 }); }
    public static uint get_bull_shark_testosterone_value() { return Memory.Read<uint>(Globals.PickupDataPTR, new int[] { 0x160 }); }
    public static void repair_online_vehicle(long vehicle)
    {
        Task.Run(() =>
        {
            Globals.deliver_bull_shark(true);
            Task.Delay(300).Wait();
            uint fix_veh_value = get_fix_veh_value();
            uint bull_shark_testosterone_value = get_bull_shark_testosterone_value();
            List<long> pickups = Replayinterface.get_pickups();
            for (int i = 0; i < pickups.Count; i++)
            {
                if (Pickup.get_pickup_value(pickups[i]) == bull_shark_testosterone_value)
                {
                    Pickup.set_pickup_value(pickups[i], fix_veh_value);
                    Task.Delay(10).Wait();
                    Vehicle.set_health(vehicle, 999.0f);
                    Task.Delay(10).Wait();
                    Pickup.set_position(pickups[i], Vehicle.get_real_position(vehicle));
                    Task.Delay(10).Wait();
                    break;
                }
            }
            Task.Delay(1000).Wait();
            if(Globals.is_in_bull_shark())
            {
                Vehicle.set_dirt_level(vehicle, 0.0f);
                Globals.instant_bull_shark(false);
            }
        });
    }

    public static string find_vehicle_display_name(long hash, bool isDisplay)
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

    public static void infinite_ammo(bool toggle) { Memory.WriteBytes(Globals.InfiniteAmmoADDR, toggle ? new byte[] { 0x90, 0x90, 0x90 } : new byte[] { 0x41, 0x2B, 0xD1 }); }

    public static void no_reload(bool toggle) { Memory.WriteBytes(Globals.NoReloadADDR, toggle ? new byte[] { 0x90, 0x90, 0x90 } : new byte[] { 0x41, 0x2B, 0xC9 }); }

    public static void fill_current_ammo()
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
    public static void fill_all_ammo()
    {
        long p = Ped.get_ped_inventory(get_local_ped());
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
        while (Memory.Read<int>(p + count * 0x08) != 0 && Memory.Read<int>(p + count * 0x08, new int[] {0x08}) != 0)
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
    public static uint get_hash(long basemodelinfo) { return Memory.Read<uint>(basemodelinfo + 0x18); }
    public static byte get_type(long basemodelinfo) { return Memory.Read<byte>(basemodelinfo + 0x9D); }
}

public class PedModelInfo : BaseModelInfo
{

}

public class VehicleModelInfo : BaseModelInfo
{
    public static ushort get_extras(long vehiclemodelinfo) { return Memory.Read<ushort>(vehiclemodelinfo + 0x58B); }
    public static bool get_extras_rocket_boost(long vehiclemodelinfo) { return (get_extras(vehiclemodelinfo) & (1 << 6)) == (1 << 6); }
    public static bool get_extras_vehicle_jump(long vehiclemodelinfo) { return (get_extras(vehiclemodelinfo) & (1 << 5)) == (1 << 5); }
    public static bool get_extras_parachute(long vehiclemodelinfo) { return (get_extras(vehiclemodelinfo) & (1 << 8)) == (1 << 8); }
    public static bool get_extras_ramp_buggy(long vehiclemodelinfo) { return (get_extras(vehiclemodelinfo) & (1 << 9)) == (1 << 9); }


    public static void set_extras(long vehiclemodelinfo, ushort value) { Memory.Write<ushort>(vehiclemodelinfo + 0x58B, value); }
    public static void set_extras_rocket_boost(long vehiclemodelinfo, bool toggle)
    {
        ushort temp = get_extras(vehiclemodelinfo);
        if (toggle) temp = (ushort)(temp | (1 << 6));
        else temp = (ushort)(temp & ~(1 << 6));
        set_extras(vehiclemodelinfo, temp);
    }
    public static void set_extras_vehicle_jump(long vehiclemodelinfo, bool toggle)
    {
        ushort temp = get_extras(vehiclemodelinfo);
        if (toggle) temp = (ushort)(temp | (1 << 5));
        else temp = (ushort)(temp & ~(1 << 5));
        set_extras(vehiclemodelinfo, temp);
    }
    public static void set_extras_parachute(long vehiclemodelinfo, bool toggle)
    {
        ushort temp = get_extras(vehiclemodelinfo);
        if (toggle) temp = (ushort)(temp | (1 << 8));
        else temp = (ushort)(temp & ~(1 << 8));
        set_extras(vehiclemodelinfo, temp);
    }
    public static void set_extras_ramp_buggy(long vehiclemodelinfo, bool toggle)
    {
        ushort temp = get_extras(vehiclemodelinfo);
        if (toggle) temp = (ushort)(temp | (1 << 9));
        else temp = (ushort)(temp & ~(1 << 9));
        set_extras(vehiclemodelinfo, temp);
    }
}

public class WeaponInfo : BaseModelInfo
{
    public static short get_damage_type(long weaponinfo) { return Memory.Read<short>(weaponinfo + 0x20); }
    public static int get_explosion_type(long weaponinfo) { return Memory.Read<int>(weaponinfo + 0x24); }
    public static int get_fire_type(long weaponinfo) { return Memory.Read<int>(weaponinfo + 0x54); }
    public static float get_spread(long weaponinfo) { return Memory.Read<float>(weaponinfo + 0x7C); }
    public static float get_reload_time_multiplier(long weaponinfo) { return Memory.Read<float>(weaponinfo + 0x134); }
    public static float get_lock_on_range(long weaponinfo) { return Memory.Read<float>(weaponinfo + 0x288); }
    public static float get_range(long weaponinfo) { return Memory.Read<float>(weaponinfo + 0x28C); }
    public static float get_recoil(long weaponinfo) { return Memory.Read<float>(weaponinfo + 0x2F4); }


    public static void set_damage_type(long weaponinfo, short value) { Memory.Write<short>(weaponinfo + 0x20, value); }
    public static void set_explosion_type(long weaponinfo, int value) { Memory.Write<int>(weaponinfo + 0x24, value); }
    public static void set_fire_type(long weaponinfo, int value) { Memory.Write<int>(weaponinfo + 0x54, value); }
    public static void set_spread(long weaponinfo, float value) { Memory.Write<float>(weaponinfo + 0x7C, value); }
    public static void set_reload_time_multiplier(long weaponinfo, float value) { Memory.Write<float>(weaponinfo + 0x134, value); }
    public static void set_lock_on_range(long weaponinfo, float value) { Memory.Write<float>(weaponinfo + 0x288, value); }
    public static void set_range(long weaponinfo, float value) { Memory.Write<float>(weaponinfo + 0x28C, value); }
    public static void set_recoil(long weaponinfo, float value) { Memory.Write<float>(weaponinfo + 0x2F4, value); }
}

public class ArchetypeDamp//rage::phArchetypeDamp
{
    public static float get_water_collision_strength(long archetypedamp) { return Memory.Read<float>(archetypedamp + 0x54); }
    public static bool get_no_water_collision_strength(long archetypedamp) { return get_water_collision_strength(archetypedamp) <= 0.0f; }


    public static void set_water_collision_strength(long archetypedamp, float value) { Memory.Write<float>(archetypedamp + 0x54, value); }
    public static void set_no_water_collision_strength(long archetypedamp, bool toggle) { set_water_collision_strength(archetypedamp, toggle ? 0.0f : 1.0f); }
}

public class Navigation
{
    public static long get_archetypedamp(long navigation) { return Memory.Read<long>(navigation + 0x10); }
    public static float get_water_collision_strength(long navigation) { return ArchetypeDamp.get_water_collision_strength(get_archetypedamp(navigation)); }
    public static bool get_no_water_collision_strength(long navigation) { return ArchetypeDamp.get_no_water_collision_strength(get_archetypedamp(navigation)); }
    public static Vector3 get_real_right_vector3(long navigation) { return Memory.Read<Vector3>(navigation + 0x20); }
    public static Vector3 get_real_forward_vector3(long navigation) { return Memory.Read<Vector3>(navigation + 0x30); }
    public static Vector3 get_real_up_vector3(long navigation) { return Memory.Read<Vector3>(navigation + 0x40); }
    public static Vector3 get_real_position(long navigation) { return Memory.Read<Vector3>(navigation + 0x50); }


    public static void set_water_collision_strength(long navigation, float value) { ArchetypeDamp.set_water_collision_strength(get_archetypedamp(navigation), value); }
    public static void set_no_water_collision_strength(long navigation, bool toggle) { ArchetypeDamp.set_no_water_collision_strength(get_archetypedamp(navigation), toggle); }
    public static void set_real_right_vector3(long navigation, Vector3 pos) { Memory.Write<Vector3>(navigation + 0x20, pos); }
    public static void set_real_forward_vector3(long navigation, Vector3 pos) { Memory.Write<Vector3>(navigation + 0x30, pos); }
    public static void set_real_up_vector3(long navigation, Vector3 pos) { Memory.Write<Vector3>(navigation + 0x40, pos); }
    public static void set_real_position(long navigation, Vector3 pos) { Memory.Write<Vector3>(navigation + 0x50, pos); }
}


public class Entity
{
    public static long get_modelinfo(long entity) { return Memory.Read<long>(entity + 0x20); }
    public static uint get_model_hash(long entity) { return BaseModelInfo.get_hash(get_modelinfo(entity)); }
    public static uint get_model_type(long entity) { return BaseModelInfo.get_type(get_modelinfo(entity)); }
    public static byte get_type2(long entity) { return Memory.Read<byte>(entity + 0x29); }
    public static byte get_type(long entity) { return Memory.Read<byte>(entity + 0x2B); }
    public static bool is_player(long entity) { return ((get_type(entity) == 156) ? true : false); }
    public static bool get_invisible(long entity) { return (((Memory.Read<byte>(entity + 0x2C) & (1 << 0)) == (1 << 0)) ? false : true); }
    public static long get_navigation(long entity) { return Memory.Read<long>(entity + 0x30); }
    public static Vector3 get_real_right_vector3(long entity) { return Navigation.get_real_right_vector3(get_navigation(entity)); }
    public static Vector3 get_real_forward_vector3(long entity) { return Navigation.get_real_forward_vector3(get_navigation(entity)); }
    public static Vector3 get_real_up_vector3(long entity) { return Navigation.get_real_up_vector3(get_navigation(entity)); }
    public static Vector3 get_real_position(long entity) { return Navigation.get_real_position(get_navigation(entity)); }
    public static Vector3 get_real_forwardpos(long entity, float dist = 7.0f)
    {
        Vector3 vec = get_real_right_vector3(entity);
        Vector3 pos = get_real_position(entity);
        pos.X -= dist * vec.Y;
        pos.Y += dist * vec.X;
        return pos;
    }
    public static float get_water_collision_strength(long entity) { return Navigation.get_water_collision_strength(get_navigation(entity)); }
    public static bool get_no_water_collision_strength(long entity) { return Navigation.get_no_water_collision_strength(get_navigation(entity)); }
    public static Vector3 get_visual_right_vector3(long entity) { return Memory.Read<Vector3>(entity + 0x60); }
    public static Vector3 get_visual_forward_vector3(long entity) { return Memory.Read<Vector3>(entity + 0x70); }
    public static Vector3 get_visual_up_vector3(long entity) { return Memory.Read<Vector3>(entity + 0x80); }
    public static Vector3 get_visual_position(long entity) { return Memory.Read<Vector3>(entity + 0x90); }
    public static Vector3 get_visual_forwardpos(long entity, float dist = 7.0f)
    {
        Vector3 vec = get_visual_right_vector3(entity);
        Vector3 pos = get_visual_position(entity);
        pos.X -= dist * vec.Y;
        pos.Y += dist * vec.X;
        return pos;
    }
    public static uint get_damage_bits(long entity) { return Memory.Read<uint>(entity + 0x188); }
    public static bool get_proofs_bullet(long entity) { return (get_damage_bits(entity) & (1 << 4)) == (1 << 4); }
    public static bool get_proofs_fire(long entity) { return (get_damage_bits(entity) & (1 << 5)) == (1 << 5); }
    public static bool get_proofs_collision(long entity) { return (get_damage_bits(entity) & (1 << 6)) == (1 << 6); }
    public static bool get_proofs_melee(long entity) { return (get_damage_bits(entity) & (1 << 7)) == (1 << 7); }
    public static bool get_proofs_god(long entity) { return (get_damage_bits(entity) & (1 << 8)) == (1 << 8); }//invincible
    public static bool get_proofs_explosion(long entity) { return (get_damage_bits(entity) & (1 << 11)) == (1 << 11); }
    public static bool get_proofs_steam(long entity) { return (get_damage_bits(entity) & (1 << 15)) == (1 << 15); }
    public static bool get_proofs_drown(long entity) { return (get_damage_bits(entity) & (1 << 16)) == (1 << 16); }
    public static bool get_proofs_water(long entity) { return (get_damage_bits(entity) & (1 << 24)) == (1 << 24); }


    public static void set_invisible(long entity, bool toggle)
    {
        byte temp = Memory.Read<byte>(entity + 0x2C);
        if (toggle) temp = (byte)(temp & ~(1 << 0));
        else temp = (byte)(temp | (1 << 0));
        Memory.Write(entity + 0x2C, temp);
    }
    public static void set_real_right_vector3(long entity, Vector3 pos) { Navigation.set_real_right_vector3(get_navigation(entity), pos); }
    public static void set_real_forward_vector3(long entity, Vector3 pos) { Navigation.set_real_forward_vector3(get_navigation(entity), pos); }
    public static void set_real_up_vector3(long entity, Vector3 pos) { Navigation.set_real_up_vector3(get_navigation(entity), pos); }
    public static void set_real_position(long entity, Vector3 pos) { Navigation.set_real_position(get_navigation(entity), pos); }
    public static void set_water_collision_strength(long entity, float value) { Navigation.set_water_collision_strength(get_navigation(entity), value); }
    public static void set_no_water_collision_strength(long entity, bool toggle) { Navigation.set_no_water_collision_strength(get_navigation(entity), toggle); }
    public static void set_visual_right_vector3(long entity, Vector3 pos) { Memory.Write<Vector3>(entity + 0x60, pos); }
    public static void set_visual_forward_vector3(long entity, Vector3 pos) { Memory.Write<Vector3>(entity + 0x70, pos); }
    public static void set_visual_up_vector3(long entity, Vector3 pos) { Memory.Write<Vector3>(entity + 0x80, pos); }
    public static void set_visual_position(long entity, Vector3 pos) { Memory.Write<Vector3>(entity + 0x90, pos); }
    public static void set_position(long entity, Vector3 pos)
    {
        set_real_position(entity, pos);
        set_visual_position(entity, pos);
    }
    public static void set_damage_bits(long entity, uint value) { Memory.Write<uint>(entity + 0x188, value); }
    public static void set_proofs_bullet(long entity, bool toggle)
    {
        uint temp = get_damage_bits(entity);
        temp = toggle ? (uint)(temp | (1 << 4)) : (uint)(temp & ~(1 << 4));
        set_damage_bits(entity, temp);
    }
    public static void set_proofs_fire(long entity, bool toggle)
    {
        uint temp = get_damage_bits(entity);
        temp = toggle ? (uint)(temp | (1 << 5)) : (uint)(temp & ~(1 << 5));
        set_damage_bits(entity, temp);
    }
    public static void set_proofs_collision(long entity, bool toggle)
    {
        uint temp = get_damage_bits(entity);
        temp = toggle ? (uint)(temp | (1 << 6)) : (uint)(temp & ~(1 << 6));
        set_damage_bits(entity, temp);
    }
    public static void set_proofs_melee(long entity, bool toggle)
    {
        uint temp = get_damage_bits(entity);
        temp = toggle ? (uint)(temp | (1 << 7)) : (uint)(temp & ~(1 << 7));
        set_damage_bits(entity, temp);
    }
    public static void set_proofs_god(long entity, bool toggle)
    {
        uint temp = get_damage_bits(entity);
        temp = toggle ? (uint)(temp | (1 << 8)) : (uint)(temp & ~(1 << 8));
        set_damage_bits(entity, temp);
    }
    public static void set_proofs_explosion(long entity, bool toggle)
    {
        uint temp = get_damage_bits(entity);
        temp = toggle ? (uint)(temp | (1 << 11)) : (uint)(temp & ~(1 << 11));
        set_damage_bits(entity, temp);
    }
    public static void set_proofs_steam(long entity, bool toggle)
    {
        uint temp = get_damage_bits(entity);
        temp = toggle ? (uint)(temp | (1 << 15)) : (uint)(temp & ~(1 << 15));
        set_damage_bits(entity, temp);
    }
    public static void set_proofs_drown(long entity, bool toggle)
    {
        uint temp = get_damage_bits(entity);
        temp = toggle ? (uint)(temp | (1 << 16)) : (uint)(temp & ~(1 << 16));
        set_damage_bits(entity, temp);
    }
    public static void set_proofs_water(long entity, bool toggle)
    {
        uint temp = get_damage_bits(entity);
        temp = toggle ? (uint)(temp | (1 << 24)) : (uint)(temp & ~(1 << 24));
        set_damage_bits(entity, temp);
    }
}


public class Ped : Entity
{
    public static byte get_hostility(long ped) { return Memory.Read<byte>(ped + 0x18C); }
    public static bool is_enemy(long ped) { return ((get_hostility(ped) > 1) ? true : false); }
    public static float get_health(long ped) { return Memory.Read<float>(ped + 0x280); }
    public static float get_max_health(long ped) { return Memory.Read<float>(ped + 0x2A0); }
    public static long get_current_vehicle(long ped) { return Memory.Read<long>(ped + 0xD30); }
    public static bool is_in_vehicle(long ped) { return ((Memory.Read<byte>(ped + 0xE52) == 1) ? true : false); }
    public static uint get_pedtype(long ped) { return Memory.Read<uint>(ped + 0x10B8) << 11 >> 25; }
    public static bool get_no_ragdoll(long ped) { return (((Memory.Read<byte>(ped + 0x10B8) & (1 << 5)) == (1 << 5)) ? false : true); }
    public static long get_playerinfo(long ped) { return Memory.Read<long>(ped + 0x10C8); }
    public static bool get_frame_flags_explosiveammo(long ped) { return PlayerInfo.get_frame_flags_explosiveammo(get_playerinfo(ped)); }
    public static bool get_frame_flags_flamingammo(long ped) { return PlayerInfo.get_frame_flags_flamingammo(get_playerinfo(ped)); }
    public static bool get_frame_flags_explosivefists(long ped) { return PlayerInfo.get_frame_flags_explosivefists(get_playerinfo(ped)); }
    public static bool get_frame_flags_superjump(long ped) { return PlayerInfo.get_frame_flags_superjump(get_playerinfo(ped)); }
    public static void set_frame_flags_explosiveammo(long ped, bool toggle) { PlayerInfo.set_frame_flags_explosiveammo(get_playerinfo(ped), toggle); }
    public static void set_frame_flags_flamingammo(long ped, bool toggle) { PlayerInfo.set_frame_flags_flamingammo(get_playerinfo(ped), toggle); }
    public static void set_frame_flags_explosivefists(long ped, bool toggle) { PlayerInfo.set_frame_flags_explosivefists(get_playerinfo(ped), toggle); }
    public static void set_frame_flags_superjump(long ped, bool toggle) { PlayerInfo.set_frame_flags_superjump(get_playerinfo(ped), toggle); }
    public static int  get_wanted_level(long ped) { return PlayerInfo.get_wanted_level((get_playerinfo(ped))); }
    public static void set_wanted_level(long ped, int value) { PlayerInfo.set_wanted_level(get_playerinfo(ped), value); }
    public static float get_run_speed(long ped) { return PlayerInfo.get_run_speed(get_playerinfo(ped)); }
    public static float get_swim_speed(long ped) { return PlayerInfo.get_swim_speed(get_playerinfo(ped)); }
    public static float get_stealth_speed(long ped) { return PlayerInfo.get_stealth_speed(get_playerinfo(ped)); }
    public static void set_run_speed(long ped, float value) { PlayerInfo.set_run_speed(get_playerinfo(ped), value); }
    public static void set_swim_speed(long ped, float value) { PlayerInfo.set_swim_speed(get_playerinfo(ped), value); }
    public static void set_stealth_speed(long ped, float value) { PlayerInfo.set_stealth_speed(get_playerinfo(ped), value); }
    public static bool get_everyone_ignore(long ped) { return PlayerInfo.get_everyone_ignore(get_playerinfo(ped)); }
    public static bool get_cops_ignore(long ped) { return PlayerInfo.get_cops_ignore(get_playerinfo(ped)); }
    public static void set_everyone_ignore(long ped, bool toggle) { PlayerInfo.set_everyone_ignore(get_playerinfo(ped), toggle); }
    public static void set_cops_ignore(long ped, bool toggle) { PlayerInfo.set_cops_ignore(get_playerinfo(ped), toggle); }
    public static long get_ped_inventory(long ped) { return Memory.Read<long>(ped + 0x10D0); }
    public static bool get_infinite_ammo(long ped) { return PedInventory.get_infinite_ammo(get_ped_inventory(ped)); }
    public static bool get_infinite_clip(long ped) { return PedInventory.get_infinite_clip(get_ped_inventory(ped)); }
    public static void set_infinite_ammo(long ped, bool toggle) { PedInventory.set_infinite_ammo(get_ped_inventory(ped), toggle); }
    public static void set_infinite_clip(long ped, bool toggle) { PedInventory.set_infinite_clip(get_ped_inventory(ped), toggle); }
    public static long get_pedweaponmanager(long ped) { return Memory.Read<long>(ped + 0x10D8); }
    public static short get_damage_type(long ped) { return PedWeaponManager.get_damage_type(get_pedweaponmanager(ped)); }
    public static int get_explosion_type(long ped) { return PedWeaponManager.get_explosion_type(get_pedweaponmanager(ped)); }
    public static int get_fire_type(long ped) { return PedWeaponManager.get_fire_type(get_pedweaponmanager(ped)); }
    public static float get_spread(long ped) { return PedWeaponManager.get_spread(get_pedweaponmanager(ped)); }
    public static float get_reload_time_multiplier(long ped) { return PedWeaponManager.get_reload_time_multiplier(get_pedweaponmanager(ped)); }
    public static float get_lock_on_range(long ped) { return PedWeaponManager.get_lock_on_range(get_pedweaponmanager(ped)); }
    public static float get_range(long ped) { return PedWeaponManager.get_range(get_pedweaponmanager(ped)); }
    public static float get_recoil(long ped) { return PedWeaponManager.get_recoil(get_pedweaponmanager(ped)); }
    public static bool get_seatbelt(long ped) { return (((Memory.Read<byte>(ped + 0x145C) & (1 << 0)) == (1 << 0)) ? true : false); }
    public static float get_armor(long ped) { return Memory.Read<float>(ped + 0x1530); }
    private static float get_collision(long ped) { return Memory.Read<float>(ped + 0x30, new int[] { 0x10, 0x20, 0x70, 0x00, 0x2C }); }
    public static bool get_no_collision(long ped) { return ((get_collision(ped) <= 0.0f) ? true : false); }


    public static void set_damage_type(long ped, short value) { PedWeaponManager.set_damage_type(get_pedweaponmanager(ped), value); }
    public static void set_explosion_type(long ped, int value) { PedWeaponManager.set_explosion_type(get_pedweaponmanager(ped), value); }
    public static void set_fire_type(long ped, int value) { PedWeaponManager.set_fire_type(get_pedweaponmanager(ped), value); }
    public static void set_spread(long ped, float value) { PedWeaponManager.set_spread(get_pedweaponmanager(ped), value); }
    public static void set_reload_time_multiplier(long ped, float value) { PedWeaponManager.set_reload_time_multiplier(get_pedweaponmanager(ped), value); }
    public static void set_lock_on_range(long ped, float value) { PedWeaponManager.set_lock_on_range(get_pedweaponmanager(ped), value); }
    public static void set_range(long ped, float value) { PedWeaponManager.set_range(get_pedweaponmanager(ped), value); }
    public static void set_recoil(long ped, float value) { PedWeaponManager.set_recoil(get_pedweaponmanager(ped), value); }
    public static void set_health(long ped, float value) { Memory.Write<float>(ped + 0x280, value); }
    public static void set_max_health(long ped, float value) { Memory.Write<float>(ped + 0x2A0, value); }
    public static void set_no_ragdoll(long ped, bool toggle)
    {
        byte temp = Memory.Read<byte>(ped + 0x10B8);
        if (toggle) temp = (byte)(temp | (1 << 5));
        else temp = (byte)(temp & ~(1 << 5));
        Memory.Write<byte>(ped + 0x10B8, temp);
    }
    public static void set_seatbelt(long ped, bool toggle)
    {
        byte temp = Memory.Read<byte>(ped + 0x145C);
        if (toggle) temp = (byte)(temp | (1 << 0));
        else temp = (byte)(temp & ~(1 << 0));
        Memory.Write<byte>(ped + 0x145C, temp);
    }
    public static void set_armour(long ped, float value) { Memory.Write<float>(ped + 0x1530, value); }
    private static void set_collision(long ped, float value) { Memory.Write<float>(ped + 0x30, new int[] { 0x10, 0x20, 0x70, 0x00, 0x2C }, value); }
    public static void set_no_collision(long ped, bool toggle) { set_collision(ped, (toggle ? -1.0f : 0.25f)); }
}


public class Vehicle : Entity
{
    public static ushort get_extras(long vehicle) { return VehicleModelInfo.get_extras(get_modelinfo(vehicle)); }
    public static bool get_extras_rocket_boost(long vehicle) { return VehicleModelInfo.get_extras_rocket_boost(get_modelinfo(vehicle)); }
    public static bool get_extras_vehicle_jump(long vehicle) { return VehicleModelInfo.get_extras_vehicle_jump(get_modelinfo(vehicle)); }
    public static bool get_extras_parachute(long vehicle) { return VehicleModelInfo.get_extras_parachute(get_modelinfo(vehicle)); }
    public static bool get_extras_ramp_buggy(long vehicle) { return VehicleModelInfo.get_extras_ramp_buggy(get_modelinfo(vehicle)); }
    public static byte get_state(long vehicle) { return Memory.Read<byte>(vehicle + 0xD8); }
    public static bool get_state_is_personal(long vehicle) { return (((get_state(vehicle) & (1 << 5)) == (1 << 5)) ? true : false); }
    public static bool get_state_is_using(long vehicle)
    {
        byte temp = get_state(vehicle);
        return ((((temp & (1 << 1)) == (1 << 1)) && !((temp & (1 << 0)) == (1 << 0))) ? true : false);
    }
    public static bool get_state_is_destroyed(long vehicle)
    {
        byte temp = get_state(vehicle);
        return ((((temp & (1 << 1)) == (1 << 1)) && ((temp & (1 << 0)) == (1 << 0))) ? true : false);
    }
    public static float get_health(long vehicle) { return Memory.Read<float>(vehicle + 0x280); }
    public static float get_max_health(long vehicle) { return Memory.Read<float>(vehicle + 0x2A0); }
    public static float get_health2(long vehicle) { return Memory.Read<float>(vehicle + 0x840); }//m_body_health
    public static float get_health3(long vehicle) { return Memory.Read<float>(vehicle + 0x844); }//m_petrol_tank_health
    public static float get_engine_health(long vehicle) { return Memory.Read<float>(vehicle + 0x908); }//m_engine_health
    public static float get_dirt_level(long vehicle) { return Memory.Read<float>(vehicle + 0x9F8); }
    public static float get_gravity(long vehicle) { return Memory.Read<float>(vehicle + 0xC5C); }
    public static byte get_cur_num_of_passenger(long vehicle) { return Memory.Read<byte>(vehicle + 0xC62); }


    public static void set_extras(long vehicle, ushort value) { VehicleModelInfo.set_extras(get_modelinfo(vehicle), value); }
    public static void set_extras_rocket_boost(long vehicle, bool toggle) { VehicleModelInfo.set_extras_rocket_boost(get_modelinfo(vehicle), toggle); }
    public static void set_extras_vehicle_jump(long vehicle, bool toggle) { VehicleModelInfo.set_extras_vehicle_jump(get_modelinfo(vehicle), toggle); }
    public static void set_extras_parachute(long vehicle, bool toggle) { VehicleModelInfo.set_extras_parachute(get_modelinfo(vehicle), toggle); }
    public static void set_extras_ramp_buggy(long vehicle, bool toggle) { VehicleModelInfo.set_extras_ramp_buggy(get_modelinfo(vehicle), toggle); }
    public static void set_state(long vehicle, byte value) { Memory.Write<byte>(vehicle + 0xD8, value); }
    public static void set_state_is_personal(long vehicle, bool toggle)
    {
        byte temp = get_state(vehicle);
        if (toggle) temp = (byte)(temp | (1 << 5));
        else temp = (byte)(temp & ~(1 << 5));
        set_state(vehicle, temp);
    }
    public static void set_state_is_using(long vehicle, bool toggle)
    {
        byte temp = get_state(vehicle);
        if (toggle) temp = (byte)(temp | (1 << 1) & ~(1 << 0));
        else temp = (byte)(temp & ~(1 << 1) & ~(1 << 0));
        set_state(vehicle, temp);
    }
    public static void set_state_is_destroyed(long vehicle, bool toggle)
    {
        byte temp = get_state(vehicle);
        if (toggle) temp = (byte)(temp | (1 << 1) | (1 << 0));
        else temp = (byte)(temp & ~(1 << 0));
        set_state(vehicle, temp);
    }
    public static void set_health(long vehicle, float value) { Memory.Write<float>(vehicle + 0x280, value); }
    public static void set_max_health(long vehicle, float value) { Memory.Write<float>(vehicle + 0x2A0, value); }
    public static void set_health2(long vehicle, float value) { Memory.Write<float>(vehicle + 0x840, value); }
    public static void set_health3(long vehicle, float value) { Memory.Write<float>(vehicle + 0x844, value); }
    public static void set_engine_health(long vehicle, float value) { Memory.Write<float>(vehicle + 0x908, value); }
    public static void set_dirt_level(long vehicle, float value) { Memory.Write<float>(vehicle + 0x9F8, value); }
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
    public static uint get_pickup_value(long pickup) { return Memory.Read<uint>(pickup + 0x490); }
    public static uint get_model_hash() { return Memory.Read<uint>(Globals.ReplayInterfacePTR, new int[] { 0x20, 0xB0, 0x0, 0x490, 0xE80 }); }


    public static void set_pickup_hash(long pickup, uint hash) { Memory.Write<uint>(pickup + 0x488, hash); }
    public static void set_pickup_value(long pickup, uint value) { Memory.Write<uint>(pickup + 0x490, value); }
    public static void set_model_hash(uint hash) { Memory.Write<uint>(Globals.ReplayInterfacePTR, new int[] { 0x20, 0xB0, 0x0, 0x490, 0xE80 }, hash); }
}


public class PlayerInfo
{
    public static uint get_scid(long playerinfo) { return Memory.Read<uint>(playerinfo + 0x90); }
    public static string get_name(long playerinfo) { return Memory.ReadString(playerinfo + 0xA4, null, 20); }
    public static float get_swim_speed(long playerinfo) { return Memory.Read<float>(playerinfo + 0x170); }
    public static float get_stealth_speed(long playerinfo) { return Memory.Read<float>(playerinfo + 0x18C); }
    public static long get_ped(long playerinfo) { return Memory.Read<long>(playerinfo + 0x1E8); }
    public static byte get_npc_ignore(long playerinfo) { return Memory.Read<byte>(playerinfo + 0x872); }
    public static bool get_everyone_ignore(long playerinfo) { return (((get_npc_ignore(playerinfo) & (1 << 2)) == (1 << 2)) ? true : false); }
    public static bool get_cops_ignore(long playerinfo) { return (((get_npc_ignore(playerinfo) & ((1 << 0) + (1 << 1) + (1 << 6) + (1 << 7))) == ((1 << 0) + (1 << 1) + (1 << 6) + (1 << 7))) ? true : false); }
    public static int get_wanted_level(long playerinfo) { return Memory.Read<int>(playerinfo + 0x888); }
    public static float get_run_speed(long playerinfo) { return Memory.Read<float>(playerinfo + 0xCF0); }
    public static byte get_frame_flags(long playerinfo) { return Memory.Read<byte>(playerinfo + 0x219); }
    public static bool get_frame_flags_explosiveammo(long playerinfo) { return (((get_frame_flags(playerinfo) & (1 << 3)) == (1 << 3)) ? true : false); }
    public static bool get_frame_flags_flamingammo(long playerinfo) { return (((get_frame_flags(playerinfo) & (1 << 4)) == (1 << 4)) ? true : false); }//fire ammo
    public static bool get_frame_flags_explosivefists(long playerinfo) { return (((get_frame_flags(playerinfo) & (1 << 5)) == (1 << 5)) ? true : false); }//explosive punch
    public static bool get_frame_flags_superjump(long playerinfo) { return (((get_frame_flags(playerinfo) & (1 << 6)) == (1 << 6)) ? true : false); }


    public static void set_swim_speed(long playerinfo, float value) { Memory.Write<float>(playerinfo + 0x170, value); }
    public static void set_stealth_speed(long playerinfo, float value) { Memory.Write<float>(playerinfo + 0x18C, value); }
    public static void set_npc_ignore(long playerinfo, byte value) { Memory.Write<byte>(playerinfo + 0x872, value); }
    public static void set_everyone_ignore(long playerinfo, bool toggle) 
    {
        byte temp = get_npc_ignore(playerinfo);
        if (toggle) temp = (byte)(temp | (1 << 2));
        else temp = (byte)(temp & ~(1 << 2));
        set_npc_ignore(playerinfo, temp);
    }
    public static void set_cops_ignore(long playerinfo, bool toggle) 
    {
        byte temp = get_npc_ignore(playerinfo);
        if (toggle) temp = (byte)(temp | ((1 << 0) + (1 << 1) + (1 << 6) + (1 << 7)));
        else temp = (byte)(temp & ~((1 << 0) + (1 << 1) + (1 << 6) + (1 << 7)));
        set_npc_ignore(playerinfo, temp);
    }
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


public class PedInventory
{
    public static bool get_infinite_ammo(long ped_inventory) { return (((Memory.Read<byte>(ped_inventory + 0x78, new int[] { 0x78 }) & (1 << 0)) == (1 << 0)) ? true : false); }
    public static bool get_infinite_clip(long ped_inventory) { return (((Memory.Read<byte>(ped_inventory + 0x78, new int[] { 0x78 }) & (1 << 1)) == (1 << 1)) ? true : false); }


    public static void set_infinite_ammo(long ped_inventory, bool toggle)
    {
        byte temp = Memory.Read<byte>(ped_inventory + 0x78);
        if (toggle) temp = (byte)(temp | (1 << 0));
        else temp = (byte)(temp & ~(1 << 0));
        Memory.Write<byte>(ped_inventory + 0x78, temp);
    }
    public static void set_infinite_clip(long ped_inventory, bool toggle)
    {
        byte temp = Memory.Read<byte>(ped_inventory + 0x78);
        if (toggle) temp = (byte)(temp | (1 << 1));
        else temp = (byte)(temp & ~(1 << 1));
        Memory.Write<byte>(ped_inventory + 0x78, temp);
    }
}

public class PedWeaponManager
{
    public static long get_weapon_info(long pedweaponmanager) { return Memory.Read<long>(pedweaponmanager + 0x20); }
    public static short get_damage_type(long pedweaponmanager) { return WeaponInfo.get_damage_type(get_weapon_info(pedweaponmanager)); }
    public static int get_explosion_type(long pedweaponmanager) { return WeaponInfo.get_explosion_type(get_weapon_info(pedweaponmanager)); }
    public static int get_fire_type(long pedweaponmanager) { return WeaponInfo.get_fire_type(get_weapon_info(pedweaponmanager)); }
    public static float get_spread(long pedweaponmanager) { return WeaponInfo.get_spread(get_weapon_info(pedweaponmanager)); }
    public static float get_reload_time_multiplier(long pedweaponmanager) { return WeaponInfo.get_reload_time_multiplier(get_weapon_info(pedweaponmanager)); }
    public static float get_lock_on_range(long pedweaponmanager) { return WeaponInfo.get_lock_on_range(get_weapon_info(pedweaponmanager)); }
    public static float get_range(long pedweaponmanager) { return WeaponInfo.get_range(get_weapon_info(pedweaponmanager)); }
    public static float get_recoil(long pedweaponmanager) { return WeaponInfo.get_recoil(get_weapon_info(pedweaponmanager)); }


    public static void set_damage_type(long pedweaponmanager, short value) { WeaponInfo.set_damage_type(get_weapon_info(pedweaponmanager), value); }
    public static void set_explosion_type(long pedweaponmanager, int value) { WeaponInfo.set_explosion_type(get_weapon_info(pedweaponmanager), value); }
    public static void set_fire_type(long pedweaponmanager, int value) { WeaponInfo.set_fire_type(get_weapon_info(pedweaponmanager), value); }
    public static void set_spread(long pedweaponmanager, float value) { WeaponInfo.set_spread(get_weapon_info(pedweaponmanager), value); }
    public static void set_reload_time_multiplier(long pedweaponmanager, float value) { WeaponInfo.set_reload_time_multiplier(get_weapon_info(pedweaponmanager), value); }
    public static void set_lock_on_range(long pedweaponmanager, float value) { WeaponInfo.set_lock_on_range(get_weapon_info(pedweaponmanager), value); }
    public static void set_range(long pedweaponmanager, float value) { WeaponInfo.set_range(get_weapon_info(pedweaponmanager), value); }
    public static void set_recoil(long pedweaponmanager, float value) { WeaponInfo.set_recoil(get_weapon_info(pedweaponmanager), value); }
}

public class Outfits
{
    /// <summary>
    /// 范围0~19
    /// </summary>
    public static int OutfitIndex = 0;

    public static string GetOutfitNameByIndex() { return Globals.get_outfit_name_by_index(OutfitIndex); }

    public static void SetOutfitNameByIndex(string str) { Globals.set_outfit_name_by_index(OutfitIndex, str); }

    /*********************** TOP ***********************/

    public static int TOP
    {
        get => Globals.get_top(OutfitIndex);
        set => Globals.set_top(OutfitIndex, value);
    }

    public static int TOP_TEX
    {
        get => Globals.get_top_tex(OutfitIndex);
        set => Globals.set_top_tex(OutfitIndex, value);
    }

    /*********************** UNDERSHIRT ***********************/

    public static int UNDERSHIRT
    {
        get => Globals.get_undershirt(OutfitIndex);
        set => Globals.set_undershirt(OutfitIndex, value);
    }

    public static int UNDERSHIRT_TEX
    {
        get => Globals.get_undershirt_tex(OutfitIndex);
        set => Globals.set_undershirt_tex(OutfitIndex, value);
    }

    /*********************** LEGS ***********************/

    public static int LEGS
    {
        get => Globals.get_legs(OutfitIndex);
        set => Globals.set_legs(OutfitIndex, value);
    }

    public static int LEGS_TEX
    {
        get => Globals.get_legs_tex(OutfitIndex);
        set => Globals.set_legs_tex(OutfitIndex, value);
    }

    /*********************** FEET ***********************/

    public static int FEET
    {
        get => Globals.get_feet(OutfitIndex);
        set => Globals.set_feet(OutfitIndex, value);
    }

    public static int FEET_TEX
    {
        get => Globals.get_feet_tex(OutfitIndex);
        set => Globals.set_feet_tex(OutfitIndex, value);
    }

    /*********************** ACCESSORIES ***********************/

    public static int ACCESSORIES
    {
        get => Globals.get_accessories(OutfitIndex);
        set => Globals.set_accessories(OutfitIndex, value);
    }

    public static int ACCESSORIES_TEX
    {
        get => Globals.get_accessories_tex(OutfitIndex);
        set => Globals.set_accessories_tex(OutfitIndex, value);
    }

    /*********************** BAGS ***********************/

    public static int BAGS
    {
        get => Globals.get_bags(OutfitIndex);
        set => Globals.set_bags(OutfitIndex, value);
    }

    public static int BAGS_TEX
    {
        get => Globals.get_bags_tex(OutfitIndex);
        set => Globals.set_bags_tex(OutfitIndex, value);
    }

    /*********************** GLOVES ***********************/

    public static int GLOVES
    {
        get => Globals.get_gloves(OutfitIndex);
        set => Globals.set_gloves(OutfitIndex, value);
    }

    public static int GLOVES_TEX
    {
        get => Globals.get_gloves_tex(OutfitIndex);
        set => Globals.set_gloves_tex(OutfitIndex, value);
    }

    /*********************** DECALS ***********************/

    public static int DECALS
    {
        get => Globals.get_decals(OutfitIndex);
        set => Globals.set_decals(OutfitIndex, value);
    }

    public static int DECALS_TEX
    {
        get => Globals.get_decals_tex(OutfitIndex);
        set => Globals.set_decals_tex(OutfitIndex, value);
    }

    /*********************** MASK ***********************/

    public static int MASK
    {
        get => Globals.get_mask(OutfitIndex);
        set => Globals.set_mask(OutfitIndex, value);
    }

    public static int MASK_TEX
    {
        get => Globals.get_mask_tex(OutfitIndex);
        set => Globals.set_mask_tex(OutfitIndex, value);
    }

    /*********************** ARMOR ***********************/

    public static int ARMOR
    {
        get => Globals.get_armor(OutfitIndex);
        set => Globals.set_armor(OutfitIndex, value);
    }

    public static int ARMOR_TEX
    {
        get => Globals.get_armor_tex(OutfitIndex);
        set => Globals.set_armor_tex(OutfitIndex, value);
    }

    /********************************************************************************************/
    /********************************************************************************************/
    /********************************************************************************************/

    /*********************** HATS ***********************/

    public static int HATS
    {
        get => Globals.get_hats(OutfitIndex);
        set => Globals.set_hats(OutfitIndex, value);
    }

    public static int HATS_TEX
    {
        get => Globals.get_hats_tex(OutfitIndex);
        set => Globals.set_hats_tex(OutfitIndex, value);
    }

    /*********************** GLASSES ***********************/

    public static int GLASSES
    {
        get => Globals.get_glasses(OutfitIndex);
        set => Globals.set_glasses(OutfitIndex, value);
    }

    public static int GLASSES_TEX
    {
        get => Globals.get_glasses_tex(OutfitIndex);
        set => Globals.set_glasses_tex(OutfitIndex, value);
    }

    /*********************** EARS ***********************/

    public static int EARS
    {
        get => Globals.get_ears(OutfitIndex);
        set => Globals.set_ears(OutfitIndex, value);
    }

    public static int EARS_TEX
    {
        get => Globals.get_ears_tex(OutfitIndex);
        set => Globals.set_ears_tex(OutfitIndex, value);
    }

    /*********************** WATCHES ***********************/

    public static int WATCHES
    {
        get => Globals.get_watches(OutfitIndex);
        set => Globals.set_watches(OutfitIndex, value);
    }

    public static int WATCHES_TEX
    {
        get => Globals.get_watches_tex(OutfitIndex);
        set => Globals.set_watches_tex(OutfitIndex, value);
    }

    /*********************** WRIST ***********************/

    public static int WRIST
    {
        get => Globals.get_wrist(OutfitIndex);
        set => Globals.set_wrist(OutfitIndex, value);
    }

    public static int WRIST_TEX
    {
        get => Globals.get_wrist_tex(OutfitIndex);
        set => Globals.set_wrist_tex(OutfitIndex, value);
    }
}
