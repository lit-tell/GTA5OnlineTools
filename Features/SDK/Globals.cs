using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Features.SDK;

public partial class Globals
{
    public static long TempPTR;

    public static long WorldPTR;
    public static long BlipPTR;
    public static long GlobalPTR;

    public static long ReplayInterfacePTR;
    public static long NetworkPlayerMgrPTR;

    public static long ViewPortPTR;
    public static long AimingPedPTR;
    public static long CCameraPTR;

    public static long WeatherPTR;

    public static long UnkModelPTR;
    public static long PickupDataPTR;

    public static long PlayerChatterNamePTR;

    public static long LocalScriptsPTR;

    public static long InfiniteAmmoADDR;
    //41 2B D1 E8 ?? ?? ?? ?? 
    //90 90 90 E8 ?? ?? ?? ?? 
    public static long NoReloadADDR;
    //41 2B C9 3B C8 0F 4D C8 
    //90 90 90 3B C8 
}

public partial class Globals
{
    public static long GA(int index) { return Memory.Read<long>(GlobalPTR + 0x8 * ((index >> 0x12) & 0x3F)) + 8 * (index & 0x3FFFF); }

    public static T GG<T>(int index) where T : struct { return Memory.Read<T>(GA(index)); }

    public static void SG<T>(int index, T vaule) where T : struct { Memory.Write<T>(GA(index), vaule); }

    public static string get_global_string(int index) { return Memory.ReadString(GA(index), null, 20); }

    public static void set_global_string(int index, string str) { Memory.WriteString(GA(index), null, str); }
}

public partial class Globals
{
    //-- Outfit Editor Globals from VenomKY

    public const int oWardrobeG = 2359296;
    public const int oWPointA = 5559;
    public const int oWPointB = 675;
    public const int oWComponent = 1333;
    public const int oWComponentTex = 1607;
    public const int oWProp = 1881;
    public const int oWPropTex = 2092;

    //-- Vehicle Menus Globals

    public const int oVMCreate = 2725269;  //-- Create any vehicle.
    public const int oVMYCar = 2810701;  //-- Get my car.
    public const int oVGETIn = 2671447;  //-- Spawn into vehicle.
    public const int oVMSlots = 1585853;  //-- Get vehicle slots.

    //-- Some Player / Network times associated Globals

    public const int oPlayerGA = 2703660;
    public const int oPlayerIDHelp = 2689224;
    public const int oNETTimeHelp = 2703660;

    public static void anti_afk(bool toggle)
    {
        SG<int>(262145 + 87, toggle ? 99999999 : 120000);//120000 
        SG<int>(262145 + 88, toggle ? 99999999 : 300000);//300000 
        SG<int>(262145 + 89, toggle ? 99999999 : 600000);//600000 
        SG<int>(262145 + 90, toggle ? 99999999 : 900000);//900000 
        SG<int>(262145 + 8041, toggle ? 2000000000 : 30000);//30000  
        SG<int>(262145 + 8042, toggle ? 2000000000 : 60000);//60000  
        SG<int>(262145 + 8043, toggle ? 2000000000 : 90000);//90000  
        SG<int>(262145 + 8044, toggle ? 2000000000 : 120000);//120000 
    }

    public static void create_ambient_pickup(int amount, Vector3 pos)
    {
        SG<float>(2783345 + 3, pos.X);
        SG<float>(2783345 + 4, pos.Y);
        SG<float>(2783345 + 5, pos.Z);
        SG<int>(2783345 + 1, amount);
        SG<int>(4528329 + 1 + (GG<int>(2783345) * 85) + 66 + 2, 2);
        SG<int>(2783345 + 6, 1);
    }

    public static int get_network_time() { return GG<int>(1574755 + 11); }

    public static int player_id() { return GG<int>(oPlayerGA); }

    public static int get_business_index(int ID) { return 1853131 + 1 + (player_id() * 888) + 267 + 187 + 1 + (ID * 13);/* ID 0-5 */}

    public static int get_last_mp_char() { return GG<int>(1574915); }

    public static void stat_set_int(string hash, int value)
    {
        if (hash.IndexOf("_") == 0)
        {
            int Stat_MP = get_last_mp_char();
            hash = $"MP{Stat_MP}{hash}";
        }

        uint Stat_ResotreHash = GG<uint>(1655453 + 4);
        int Stat_ResotreValue = GG<int>(1020252 + 5526);

        SG<uint>(1655453 + 4, Hacks.Joaat(hash));
        SG<int>(1020252 + 5526, value);
        SG<int>(1644218 + 1139, -1);
        Thread.Sleep(1000);
        SG<uint>(1655453 + 4, Stat_ResotreHash);
        SG<int>(1020252 + 5526, Stat_ResotreValue);
    }

    public static void allow_sell_on_non_public(bool toggle) { SG<int>(2714635 + 744, toggle ? 0 : 1); }

    /// <summary>
    /// 线上战局切换
    /// 
    /// -1, 离开线上模式
    ///  0, 公共战局
    ///  1, 创建公共战局
    ///  2, 私人帮会战局
    ///  3, 帮会战局
    ///  9, 加入好友
    ///  6, 私人好友战局
    ///  10 单人战局
    ///  11 仅限邀请战局
    ///  12 加入帮会伙伴
    /// </summary>
    /// <param name="sessionID">战局ID</param>
    public static void load_session(int sessionID)
    {
        Task.Run(() =>
        {
            Memory.SetForegroundWindow();

            SG<int>(1575012, sessionID);
            SG<int>(1574589 + 2, sessionID == -1 ? -1 : 0);
            SG<int>(1574589, 1);
        });
    }

    public static void remove_suicide_cooldown(bool toggle)
    {
        if (toggle) SG<int>(2810701 + 6729, 0);
        SG<int>(262145 + 28072, toggle ? 3 : 300000);
        SG<int>(262145 + 28073, toggle ? 3 : 60000);
    }

    public static void remove_passive_mode_cooldown(bool toggle)
    {
        SG<int>(2810701 + 4460, toggle ? 0 : 1);
        SG<int>(1966542, toggle ? 0 : 1);
    }

    public static void disable_orbital_cooldown(bool toggle) { SG<int>(262145 + 22853, toggle ? 0 : 2880000); }

    public static void model_change(uint hash)
    {
        SG<int>(oVGETIn + 59, 1);
        SG<uint>(oVGETIn + 46, hash);
        Thread.Sleep(10);
        SG<int>(oVGETIn + 59, 0);
    }

    public static void get_into_online_personal_vehicle() { SG<int>(oVGETIn + 8, 1); }

    public static void session_snow(bool toggle) { SG<int>(262145 + 4723, toggle ? 1 : 0); }

    public static void off_radar(bool toggle)
    {
        SG<int>(oPlayerIDHelp + 1 + player_id() * 451 + 207, toggle ? 1 : 0);
        if (toggle) SG<int>(oNETTimeHelp + 56, get_network_time() + 3600000);
        if (toggle) SG<int>(oVMYCar + 4630, 3);
    }

    public static void ghost_organization(bool toggle)
    {
        SG<int>(oPlayerIDHelp + 1 + player_id() * 451 + 207, toggle ? 1 : 0);
        if (toggle) SG<int>(oNETTimeHelp + 56, get_network_time() + 3600000);
        if (toggle) SG<int>(oVMYCar + 4630, 4);
    }

    public static void blind_cops(bool toggle)
    {
        SG<int>(oVMYCar + 4625, toggle ? 1 : 0);
        if (toggle) SG<int>(oVMYCar + 4627, get_network_time() + 3600000);
        SG<int>(oVMYCar + 4624, toggle ? 5 : 0);
    }

    public static void bribe_cops(bool toggle)
    {
        SG<int>(oVMYCar + 4625, toggle ? 1 : 0);
        if (toggle) SG<int>(oVMYCar + 4627, get_network_time() + 3600000);
        SG<int>(oVMYCar + 4624, toggle ? 21 : 0);
    }

    public static void reveal_players(bool toggle)
    {
        SG<int>(oPlayerIDHelp + 1 + player_id() * 451 + 210, toggle ? 1 : 0);
        if (toggle) SG<int>(oNETTimeHelp + 57, get_network_time() + 3600000);
    }

    public static void rp_multiplier(float value) { SG<float>(262145 + 1, value); }

    public static void rep_multiplier(float value)
    {
        // car meet 汽车见面会       1819417801
        SG<float>(262145 + 31299, value);
        // Test Track
        SG<float>(262145 + 31300, value);
        // Head_2_Head 头对头
        SG<float>(262145 + 31297, value);
        // Scramble 攀登
        SG<float>(262145 + 31296, value);
        // Pursuit Race 追逐赛
        SG<float>(262145 + 31295, value);
        // Street Race 街头比赛
        SG<float>(262145 + 31294, value);
    }

    public static bool is_in_bull_shark() { return GG<int>(oNETTimeHelp + 3576) != 0; }

    public static void instant_bull_shark(bool toggle)
    {
        if (!toggle)
            if (GG<int>(oNETTimeHelp + 3576) != 0) SG<int>(oNETTimeHelp + 3576, 5);
        if (toggle) SG<int>(oNETTimeHelp + 3576, 1);
    }

    public static void backup_heli(bool toggle) { SG<int>(oVMYCar + 4454, toggle ? 1 : 0); }

    public static void airstrike(bool toggle) { SG<int>(oVMYCar + 4455, toggle ? 1 : 0); }

    public static void deliver_bull_shark(bool toggle) { SG<int>(oVMYCar + 882, toggle ? 1 : 0); }

    public static void ceo_special_cargo(bool toggle) { SG<int>(1946798, toggle ? 1 : 0); }

    public static void ceo_cargo_type(int cargoID) { SG<int>(1946644, cargoID); }

    public static void ceo_buying_crates_cooldown(bool toggle) { SG<int>(262145 + 15361, toggle ? 0 : 300000); }

    public static void ceo_selling_crates_cooldown(bool toggle) { SG<int>(262145 + 15362, toggle ? 0 : 1800000); }

    public static void ceo_price_per_crate_at_crates(bool toggle)
    {
        SG<int>(262145 + 15596, toggle ? 20000 : 10000);      // 1
        SG<int>(262145 + 15597, toggle ? 20000 : 11000);      // 2
        SG<int>(262145 + 15598, toggle ? 20000 : 12000);      // 3                                                       
        SG<int>(262145 + 15599, toggle ? 20000 : 13000);      // 4-5
        SG<int>(262145 + 15600, toggle ? 20000 : 13500);      // 6-7
        SG<int>(262145 + 15601, toggle ? 20000 : 14000);      // 8-9
        SG<int>(262145 + 15602, toggle ? 20000 : 14500);      // 10-14
        SG<int>(262145 + 15603, toggle ? 20000 : 15000);      // 15-19
        SG<int>(262145 + 15604, toggle ? 20000 : 15500);      // 20-24
        SG<int>(262145 + 15605, toggle ? 20000 : 16000);      // 25-29
        SG<int>(262145 + 15606, toggle ? 20000 : 16500);      // 30-34
        SG<int>(262145 + 15607, toggle ? 20000 : 17000);      // 35-39
        SG<int>(262145 + 15608, toggle ? 20000 : 17500);      // 40-44
        SG<int>(262145 + 15609, toggle ? 20000 : 17750);      // 45-49
        SG<int>(262145 + 15610, toggle ? 20000 : 18000);      // 50-59
        SG<int>(262145 + 15611, toggle ? 20000 : 18250);      // 60-69
        SG<int>(262145 + 15612, toggle ? 20000 : 18500);      // 70-79
        SG<int>(262145 + 15613, toggle ? 20000 : 18750);      // 80-89
        SG<int>(262145 + 15614, toggle ? 20000 : 19000);      // 90-990
        SG<int>(262145 + 15615, toggle ? 20000 : 19500);      // 100-11
        SG<int>(262145 + 15616, toggle ? 20000 : 20000);      // 111
    }

    public static void remove_bunker_supply_delay(bool toggle) { SG<int>(262145 + 21349, toggle ? 0 : 600); }

    public static void set_bunker_produce_research_time(int produce_time, bool toggle)
    {
        // Base Time to Produce
        SG<int>(262145 + 21324, toggle ? produce_time : 600000);  // Product                  215868155 
        SG<int>(262145 + 21340, toggle ? produce_time : 300000);  // Research                 -676414773

        // Time to Produce Reductions
        SG<int>(262145 + 21325, toggle ? produce_time : 90000);  // Production Equipment     631477612
        SG<int>(262145 + 21326, toggle ? produce_time : 90000);  // Production Staff         818645907
        SG<int>(262145 + 21341, toggle ? produce_time : 45000);  // Research Equipment       -1148432846
        SG<int>(262145 + 21342, toggle ? produce_time : 45000);  // Research Staff           510883248
    }

    public static void set_bunker_resupply_costs(bool toggle)
    {
        SG<int>(262145 + 21347, toggle ? 200 : 15000);
        SG<int>(262145 + 21348, toggle ? 200 : 15000);
    }

    public static void set_bunker_sale_multipliers(bool toggle)
    {
        // Sale Multipliers
        SG<float>(262145 + 21303, toggle ? 2.0f : 1.0f);     // Near         1865029244
        SG<float>(262145 + 21304, toggle ? 3.0f : 1.5f);     // Far          1021567941
    }

    public static void set_mc_sale_multipliers(bool toggle)
    {
        // Sale Multipliers
        SG<float>(262145 + 18861, toggle ? 2.0f : 1.0f);     // Near         -823848572
        SG<float>(262145 + 18862, toggle ? 3.0f : 1.5f);     // Far          1763638426
    }

    public static void set_bunker_supplies_per_unit_produced(bool toggle)
    {
        // Supplies Per Unit Produced
        SG<int>(262145 + 21327, toggle ? 1 : 10);     // Product Base              -1652502760
        SG<int>(262145 + 21328, toggle ? 1 : 5);     // Product Upgraded          1647327744
        SG<int>(262145 + 21343, toggle ? 1 : 2);     // Research Base             1485279815
        SG<int>(262145 + 21344, toggle ? 1 : 1);     // Research Upgraded         2041812011
    }

    public static void set_mc_supplies_per_unit_produced(bool toggle)
    {
        // Supplies Per Unit Produced
        SG<int>(262145 + 17213, toggle ? 1 : 4);     // Documents Base            -1839004359
        SG<int>(262145 + 17214, toggle ? 1 : 10);     // Cash Base
        SG<int>(262145 + 17215, toggle ? 1 : 50);     // Cocaine Base
        SG<int>(262145 + 17216, toggle ? 1 : 24);     // Meth Base
        SG<int>(262145 + 17217, toggle ? 1 : 4);     // Weed Base
        SG<int>(262145 + 17218, toggle ? 1 : 2);     // Documents Upgraded
        SG<int>(262145 + 17219, toggle ? 1 : 5);     // Cash Upgraded
        SG<int>(262145 + 17220, toggle ? 1 : 25);     // Cocaine Upgraded
        SG<int>(262145 + 17221, toggle ? 1 : 12);     // Meth Upgraded
        SG<int>(262145 + 17222, toggle ? 1 : 2);     // Weed Upgraded
    }

    public static void unlock_bunker_research(bool toggle) { SG<int>(262145 + 21477, toggle ? 1 : 0);       /* 886070202 */}

    public static void set_nightclub_produce_time(int produce_time, bool toggle)
    {
        // Time to Produce
        SG<int>(262145 + 24135, toggle ? produce_time : 4800000);  // Sporting Goods               -147565853
        SG<int>(262145 + 24136, toggle ? produce_time : 14400000);  // South American Imports
        SG<int>(262145 + 24137, toggle ? produce_time : 7200000);  // Pharmaceutical Research
        SG<int>(262145 + 24138, toggle ? produce_time : 2400000);  // Organic Produce
        SG<int>(262145 + 24139, toggle ? produce_time : 1800000);  // Printing and Copying
        SG<int>(262145 + 24140, toggle ? produce_time : 3600000);  // Cash Creation
        SG<int>(262145 + 24141, toggle ? produce_time : 8400000);  // Cargo and Shipments
    }

    public static void set_mc_produce_time(int produce_time, bool toggle)
    {
        // Base Time to Produce
        SG<int>(262145 + 17198, toggle ? produce_time : 360000);  // Weed         -635596193
        SG<int>(262145 + 17199, toggle ? produce_time : 1800000);  // Meth
        SG<int>(262145 + 17200, toggle ? produce_time : 3000000);  // Cocaine
        SG<int>(262145 + 17201, toggle ? produce_time : 300000);  // Documents
        SG<int>(262145 + 17202, toggle ? produce_time : 720000);  // Cash

        // Time to Produce Reductions
        SG<int>(262145 + 17203, toggle ? 1 : 60000);  // Documents Equipment
        SG<int>(262145 + 17204, toggle ? 1 : 120000);  // Cash Equipment
        SG<int>(262145 + 17205, toggle ? 1 : 600000);  // Cocaine Equipment
        SG<int>(262145 + 17206, toggle ? 1 : 360000);  // Meth Equipment
        SG<int>(262145 + 17207, toggle ? 1 : 60000);  // Weed Equipment
        SG<int>(262145 + 17208, toggle ? 1 : 60000);  // Documents Staff
        SG<int>(262145 + 17209, toggle ? 1 : 120000);  // Cash Staff
        SG<int>(262145 + 17210, toggle ? 1 : 600000);  // Cocaine Staff
        SG<int>(262145 + 17211, toggle ? 1 : 360000);  // Meth Staff
        SG<int>(262145 + 17212, toggle ? 1 : 60000);  // Weed Staff
    }

    public static void remove_mc_supply_delay(bool toggle) { SG<int>(262145 + 18749, toggle ? 0 : 600);          /* 728170457 */}

    public static void set_mc_resupply_costs(bool toggle) { SG<int>(262145 + 18748, toggle? 200 : 15000);  /* Discounted Resupply Cost       BIKER_PURCHASE_SUPPLIES_COST_PER_SEGMENT */}

    public static void merry_weather_services(int serverId) { SG<int>(2810701 + serverId, 1); }

    public static void remove_export_vehicle_delay(bool toggle)
    {
        SG<int>(262145 + 19477, toggle ? 0 : 1200000);          // 1001423248
        SG<int>(262145 + 19478, toggle ? 0 : 1680000);
        SG<int>(262145 + 19479, toggle ? 0 : 2340000);
        SG<int>(262145 + 19480, toggle ? 0 : 2880000);
    }

    public static void disconnect()
    {
        SG<int>(31788, 1);
        Thread.Sleep(20);
        SG<int>(31788, 0);
    }

    public static void removeSmuggler_run_in_delay(bool toggle)
    {
        SG<int>(262145 + 22522, toggle ? 0 : 120000);          // 1278611667
        SG<int>(262145 + 22523, toggle ? 0 : 180000);
        SG<int>(262145 + 22524, toggle ? 0 : 240000);
        SG<int>(262145 + 22525, toggle ? 0 : 60000);
    }

    public static void remove_smuggler_run_out_delay(bool toggle) { SG<int>(262145 + 22561, toggle ? 0 : 180000);          /* -1525481945 */}

    public static void remove_nightclub_out_delay(bool toggle)
    {
        SG<int>(262145 + 24216, toggle ? 0 : 300000);      // 1763921019
        SG<int>(262145 + 24251, toggle ? 0 : 300000);      // -1004589438
        SG<int>(262145 + 24252, toggle ? 0 : 300000);
    }

    public static void nightclub_no_tony_laundering_money(bool toggle) { SG<float>(262145 + 24258, toggle ? 0.000001f : 0.1f);        /* -1002770353 */}

    public static void deliver_personal_vehicle(int index)
    {
        SG<int>(oVMYCar + 965, index);
        SG<int>(oVMYCar + 962, 1);
    }

    public static void create_vehicle(uint hash, int[] mod, Vector3 pos)
    {
        SG<uint>(oVMCreate + 27 + 66, hash);   // 载具哈希值

        SG<int>(oVMCreate + 27 + 94, 2);       // personal car ownerflag  个人载具拥有者标志
        SG<int>(oVMCreate + 27 + 95, 14);      // ownerflag  拥有者标志

        SG<int>(oVMCreate + 27 + 5, -1);       // primary -1 auto 159  主色调
        SG<int>(oVMCreate + 27 + 6, -1);       // secondary -1 auto 159  副色调

        SG<float>(oVMCreate + 7 + 0, pos.X);       // 载具坐标x
        SG<float>(oVMCreate + 7 + 1, pos.Y);       // 载具坐标y
        SG<float>(oVMCreate + 7 + 2, pos.Z);       // 载具坐标z

        set_global_string(oVMCreate + 27 + 1, Guid.NewGuid().ToString()[..8]);    // License plate  车牌

        for (int i = 0; i < 43; i++)
        {
            if (i < 17)
            {
                SG<int>(oVMCreate + 27 + 10 + i, mod[i]);
            }
            else if (i >= 17 && i != 42)
            {
                SG<int>(oVMCreate + 27 + 10 + 6 + i, mod[i]);
            }
            else if (mod[42] > 0 && i == 42)
            {
                SG<int>(oVMCreate + 27 + 10 + 6 + 42, new Random().Next(1, mod[42] + 1));
            }
        }

        SG<int>(oVMCreate + 27 + 7, -1);       // pearlescent
        SG<int>(oVMCreate + 27 + 8, -1);       // wheel color
        SG<int>(oVMCreate + 27 + 33, -1);      // wheel selection
        SG<int>(oVMCreate + 27 + 69, -1);      // Wheel type

        SG<int>(oVMCreate + 27 + 28, 1);
        SG<int>(oVMCreate + 27 + 30, 1);
        SG<int>(oVMCreate + 27 + 32, 1);
        SG<int>(oVMCreate + 27 + 65, 1);

        SG<uint>(oVMCreate + 27 + 77, 0xF0400200);         // vehstate  载具状态 没有这个载具起落架是收起状态

        SG<int>(oVMCreate + 5, 1);                         // can spawn flag must be odd
        SG<int>(oVMCreate + 2, 1);                         // spawn toggle gets reset to 0 on car spawn
    }

    public static void create_vehicle(long ped, uint hash, int[] mod, float dist = 7.0f, float height = 0.0f)
    {
        Vector3 pos = Ped.get_real_forwardpos(ped, dist);
        pos.Z = height == -225.0f ? height : pos.Z + height;
        create_vehicle(hash, mod, pos);
    }

    public static string get_outfit_name_by_index(int index) { return get_global_string(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (index * 13) + 1126 - (index * 5)); }

    public static void set_outfit_name_by_index(int index, string str) { set_global_string(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (index * 13) + 1126 - (index * 5), str); }

    public static int  get_top(int index)         { return GG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent    + (index * 13) + 14); }
    public static void set_top(int index, int value)     { SG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent    + (index * 13) + 14, value); }
    public static int  get_top_tex(int index)     { return GG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (index * 13) + 14); }
    public static void set_top_tex(int index, int value) { SG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (index * 13) + 14, value); }

    public static int  get_undershirt(int index)         { return GG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent    + (index * 13) + 11); }
    public static void set_undershirt(int index, int value)     { SG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent    + (index * 13) + 11, value); }
    public static int  get_undershirt_tex(int index)     { return GG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (index * 13) + 11); }
    public static void set_undershirt_tex(int index, int value) { SG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (index * 13) + 11, value); }

    public static int  get_legs(int index)         { return GG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent    + (index * 13) + 7); }
    public static void set_legs(int index, int value)     { SG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent    + (index * 13) + 7, value); }
    public static int  get_legs_tex(int index)     { return GG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (index * 13) + 7); }
    public static void set_legs_tex(int index, int value) { SG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (index * 13) + 7, value); }

    public static int  get_feet(int index)         { return GG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent    + (index * 13) + 9); }
    public static void set_feet(int index, int value)     { SG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent    + (index * 13) + 9, value); }
    public static int  get_feet_tex(int index)     { return GG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (index * 13) + 9); }
    public static void set_feet_tex(int index, int value) { SG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (index * 13) + 9, value); }

    public static int  get_accessories(int index)         { return GG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent    + (index * 13) + 10); }
    public static void set_accessories(int index, int value)     { SG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent    + (index * 13) + 10, value); }
    public static int  get_accessories_tex(int index)     { return GG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (index * 13) + 10); }
    public static void set_accessories_tex(int index, int value) { SG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (index * 13) + 10, value); }

    public static int  get_bags(int index)         { return GG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent    + (index * 13) + 8); }
    public static void set_bags(int index, int value)     { SG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent    + (index * 13) + 8, value); }
    public static int  get_bags_tex(int index)     { return GG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (index * 13) + 8); }
    public static void set_bags_tex(int index, int value) { SG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (index * 13) + 8, value); }

    public static int  get_gloves(int index)         { return GG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent    + (index * 13) + 6); }
    public static void set_gloves(int index, int value)     { SG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent    + (index * 13) + 6, value); }
    public static int  get_gloves_tex(int index)     { return GG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (index * 13) + 6); }
    public static void set_gloves_tex(int index, int value) { SG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (index * 13) + 6, value); }

    public static int  get_decals(int index)         { return GG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent    + (index * 13) + 13); }
    public static void set_decals(int index, int value)     { SG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent    + (index * 13) + 13, value); }
    public static int  get_decals_tex(int index)     { return GG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (index * 13) + 13); }
    public static void set_decals_tex(int index, int value) { SG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (index * 13) + 13, value); }

    public static int  get_mask(int index)         { return GG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent    + (index * 13) + 4); }
    public static void set_mask(int index, int value)     { SG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent    + (index * 13) + 4, value); }
    public static int  get_mask_tex(int index)     { return GG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (index * 13) + 4); }
    public static void set_mask_tex(int index, int value) { SG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (index * 13) + 4, value); }

    public static int  get_armor(int index)         { return GG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent    + (index * 13) + 12); }
    public static void set_armor(int index, int value)     { SG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent    + (index * 13) + 12, value); }
    public static int  get_armor_tex(int index)     { return GG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (index * 13) + 12); }
    public static void set_armor_tex(int index, int value) { SG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (index * 13) + 12, value); }

    public static int  get_hats(int index)         { return GG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWProp    + (index * 10) + 3); }
    public static void set_hats(int index, int value)     { SG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWProp    + (index * 10) + 3, value); }
    public static int  get_hats_tex(int index)     { return GG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWPropTex + (index * 10) + 3); }
    public static void set_hats_tex(int index, int value) { SG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWPropTex + (index * 10) + 3, value); }

    public static int  get_glasses(int index)         { return GG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWProp    + (index * 10) + 4); }
    public static void set_glasses(int index, int value)     { SG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWProp    + (index * 10) + 4, value); }
    public static int  get_glasses_tex(int index)     { return GG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWPropTex + (index * 10) + 4); }
    public static void set_glasses_tex(int index, int value) { SG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWPropTex + (index * 10) + 4, value); }

    public static int  get_ears(int index)         { return GG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWProp    + (index * 10) + 5); }
    public static void set_ears(int index, int value)     { SG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWProp    + (index * 10) + 5, value); }
    public static int  get_ears_tex(int index)     { return GG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWPropTex + (index * 10) + 5); }
    public static void set_ears_tex(int index, int value) { SG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWPropTex + (index * 10) + 5, value); }

    public static int  get_watches(int index)         { return GG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWProp    + (index * 10) + 9); }
    public static void set_watches(int index, int value)     { SG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWProp    + (index * 10) + 9, value); }
    public static int  get_watches_tex(int index)     { return GG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWPropTex + (index * 10) + 9); }
    public static void set_watches_tex(int index, int value) { SG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWPropTex + (index * 10) + 9, value); }

    public static int  get_wrist(int index)         { return GG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWProp    + (index * 10) + 10); }
    public static void set_wrist(int index, int value)     { SG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWProp    + (index * 10) + 10, value); }
    public static int  get_wrist_tex(int index)     { return GG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWPropTex + (index * 10) + 10); }
    public static void set_wrist_tex(int index, int value) { SG<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWPropTex + (index * 10) + 10, value); }
}

