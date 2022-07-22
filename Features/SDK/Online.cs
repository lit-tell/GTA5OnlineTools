using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Features.SDK;

public static class Online
{
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
    public static void Load_Session(int sessionId)
    {
        Task.Run(() =>
        {
            Memory.SetForegroundWindow();

            Hacks.WriteGA<int>(1575012, sessionId);
            Hacks.WriteGA<int>(1574589 + 2, sessionId == -1 ? -1 : 0);
            Hacks.WriteGA<int>(1574589, 1);
        });
    }

    /// <summary>
    /// 空战局（原理：暂停GTA5进程10秒）
    /// </summary>
    public static void Empty_Session()
    {
        Task.Run(() =>
        {
            ProcessMgr.SuspendProcess(Memory.GetProcessID());
            Task.Delay(10000).Wait();
            ProcessMgr.ResumeProcess(Memory.GetProcessID());
        });
    }

    /// <summary>
    /// 挂机防踢
    /// </summary>
    /// <param name="toggle"></param>
    public static void Anti_AFK(bool toggle)
    {
        Hacks.WriteGA<int>(262145 + 87, toggle ? 99999999 : 120000);        // 120000 
        Hacks.WriteGA<int>(262145 + 88, toggle ? 99999999 : 300000);        // 300000 
        Hacks.WriteGA<int>(262145 + 89, toggle ? 99999999 : 600000);        // 600000 
        Hacks.WriteGA<int>(262145 + 90, toggle ? 99999999 : 900000);        // 900000 
        Hacks.WriteGA<int>(262145 + 8041, toggle ? 2000000000 : 30000);     // 30000  
        Hacks.WriteGA<int>(262145 + 8042, toggle ? 2000000000 : 60000);     // 60000  
        Hacks.WriteGA<int>(262145 + 8043, toggle ? 2000000000 : 90000);     // 90000  
        Hacks.WriteGA<int>(262145 + 8044, toggle ? 2000000000 : 120000);    // 120000 
    }

    public static void Allow_Sell_On_Non_Public(bool toggle)
    {
        Hacks.WriteGA<int>(2714635 + 744, toggle ? 0 : 1);
    }

    public static void Remove_Suicide_Cooldown(bool toggle)
    {
        if (toggle) Hacks.WriteGA<int>(2810701 + 6729, 0);
        Hacks.WriteGA<int>(262145 + 28072, toggle ? 3 : 300000);
        Hacks.WriteGA<int>(262145 + 28073, toggle ? 3 : 60000);
    }

    public static void Remove_Passive_Mode_Cooldown(bool toggle)
    {
        Hacks.WriteGA<int>(2810701 + 4460, toggle ? 0 : 1);
        Hacks.WriteGA<int>(1966542, toggle ? 0 : 1);
    }

    public static void Disable_Orbital_Cooldown(bool toggle)
    {
        Hacks.WriteGA<int>(262145 + 22853, toggle ? 0 : 2880000);
    }

    public static void Model_Change(uint hash)
    {
        Hacks.WriteGA<int>(Hacks.oVGETIn + 59, 1);
        Hacks.WriteGA<uint>(Hacks.oVGETIn + 46, hash);
        Thread.Sleep(10);
        Hacks.WriteGA<int>(Hacks.oVGETIn + 59, 0);
    }

    public static void Get_Into_Online_Personal_Vehicle()
    {
        Hacks.WriteGA<int>(Hacks.oVGETIn + 8, 1);
    }

    public static void Session_Snow(bool toggle)
    {
        Hacks.WriteGA<int>(262145 + 4723, toggle ? 1 : 0);
    }

    public static void Off_Radar(bool toggle)
    {
        Hacks.WriteGA<int>(Hacks.oPlayerIDHelp + 1 + Hacks.PLAYER_ID() * 451 + 207, toggle ? 1 : 0);
        if (toggle)
            Hacks.WriteGA<int>(Hacks.oNETTimeHelp + 56, Hacks.GET_NETWORK_TIME() + 3600000);
        if (toggle)
            Hacks.WriteGA<int>(Hacks.oVMYCar + 4630, 3);
    }

    public static void Ghost_Organization(bool toggle)
    {
        Hacks.WriteGA<int>(Hacks.oPlayerIDHelp + 1 + Hacks.PLAYER_ID() * 451 + 207, toggle ? 1 : 0);
        if (toggle)
            Hacks.WriteGA<int>(Hacks.oNETTimeHelp + 56, Hacks.GET_NETWORK_TIME() + 3600000);
        if (toggle)
            Hacks.WriteGA<int>(Hacks.oVMYCar + 4630, 4);
    }

    public static void Blind_Cops(bool toggle)
    {
        Hacks.WriteGA<int>(Hacks.oVMYCar + 4625, toggle ? 1 : 0);
        if (toggle)
            Hacks.WriteGA<int>(Hacks.oVMYCar + 4627, Hacks.GET_NETWORK_TIME() + 3600000);
        Hacks.WriteGA<int>(Hacks.oVMYCar + 4624, toggle ? 5 : 0);
    }

    public static void Bribe_Cops(bool toggle)
    {
        Hacks.WriteGA<int>(Hacks.oVMYCar + 4625, toggle ? 1 : 0);
        if (toggle)
            Hacks.WriteGA<int>(Hacks.oVMYCar + 4627, Hacks.GET_NETWORK_TIME() + 3600000);
        Hacks.WriteGA<int>(Hacks.oVMYCar + 4624, toggle ? 21 : 0);
    }

    public static void Reveal_Players(bool toggle)
    {
        Hacks.WriteGA<int>(Hacks.oPlayerIDHelp + 1 + Hacks.PLAYER_ID() * 451 + 210, toggle ? 1 : 0);
        if (toggle) 
            Hacks.WriteGA<int>(Hacks.oNETTimeHelp + 57, Hacks.GET_NETWORK_TIME() + 3600000);
    }

    public static void RP_Multiplier(float value)
    {
        Hacks.WriteGA<float>(262145 + 1, value);
    }

    public static void REP_Multiplier(float value)
    {
        Hacks.WriteGA<float>(262145 + 31299, value);        // car meet 汽车见面会       1819417801
        Hacks.WriteGA<float>(262145 + 31300, value);        // Test Track
        Hacks.WriteGA<float>(262145 + 31297, value);        // Head_2_Head 头对头
        Hacks.WriteGA<float>(262145 + 31296, value);        // Scramble 攀登
        Hacks.WriteGA<float>(262145 + 31295, value);        // Pursuit Race 追逐赛
        Hacks.WriteGA<float>(262145 + 31294, value);        // Street Race 街头比赛
    }

    public static bool Is_In_Bull_Shark()
    {
        return Hacks.ReadGA<int>(Hacks.oNETTimeHelp + 3576) != 0;
    }

    public static void Instant_Bull_Shark(bool toggle)
    {
        if (toggle)
        {
            Hacks.WriteGA<int>(Hacks.oNETTimeHelp + 3576, 1);
        }
        else
        {
            if (Hacks.ReadGA<int>(Hacks.oNETTimeHelp + 3576) != 0)
                Hacks.WriteGA<int>(Hacks.oNETTimeHelp + 3576, 5);
        }
    }

    public static void Call_Heli_Backup(bool toggle)
    {
        Hacks.WriteGA<int>(Hacks.oVMYCar + 4454, toggle ? 1 : 0);
    }

    public static void Call_Airstrike(bool toggle)
    {
        Hacks.WriteGA<int>(Hacks.oVMYCar + 4455, toggle ? 1 : 0);
    }

    public static void Deliver_Ammo(bool toggle)
    {
        Hacks.WriteGA<int>(Hacks.oVMYCar + 874, toggle ? 1 : 0);
    }

    public static void Deliver_Bull_Shark(bool toggle)
    {
        Hacks.WriteGA<int>(Hacks.oVMYCar + 882, toggle ? 1 : 0);
    }

    public static void Deliver_Ballistic_Armor(bool toggle)
    {
        Hacks.WriteGA<int>(Hacks.oVMYCar + 884, toggle ? 1 : 0);
    }

    public static int Get_Ballistic_Armor_Request_Cost()
    {
        return Hacks.ReadGA<int>(262145 + 20083);
    }

    public static void Set_Ballistic_Armor_Request_Cost(int value)
    {
        Hacks.WriteGA<int>(262145 + 20083, value);
    }

    public static void Trigger_Boat_Pickup(bool toggle)
    {
        Hacks.WriteGA<int>(Hacks.oVMYCar + 875, toggle ? 1 : 0);
    }

    public static void Trigger_Heli_Pickup(bool toggle, bool is_vip)
    {
        Hacks.WriteGA<int>(Hacks.oVMYCar + 876, toggle ? 1 : 0);
        Hacks.WriteGA<int>(Hacks.oVMYCar + 883, is_vip ? 1 : -1);
    }

    public static void CEO_Special_Cargo(bool toggle)
    {
        Hacks.WriteGA<int>(1946798, toggle ? 1 : 0);
    }

    public static void CEO_Cargo_Type(int cargoID)
    {
        Hacks.WriteGA<int>(1946644, cargoID);
    }

    public static void CEO_Buying_Crates_Cooldown(bool toggle)
    {
        Hacks.WriteGA<int>(262145 + 15361, toggle ? 0 : 300000);
    }

    public static void CEO_Selling_Crates_Cooldown(bool toggle)
    {
        Hacks.WriteGA<int>(262145 + 15362, toggle ? 0 : 1800000);
    }

    public static void CEO_Price_Per_Crate_At_Crates(bool toggle)
    {
        Hacks.WriteGA<int>(262145 + 15596, toggle ? 20000 : 10000);      // 1
        Hacks.WriteGA<int>(262145 + 15597, toggle ? 20000 : 11000);      // 2
        Hacks.WriteGA<int>(262145 + 15598, toggle ? 20000 : 12000);      // 3                                                       
        Hacks.WriteGA<int>(262145 + 15599, toggle ? 20000 : 13000);      // 4-5
        Hacks.WriteGA<int>(262145 + 15600, toggle ? 20000 : 13500);      // 6-7
        Hacks.WriteGA<int>(262145 + 15601, toggle ? 20000 : 14000);      // 8-9
        Hacks.WriteGA<int>(262145 + 15602, toggle ? 20000 : 14500);      // 10-14
        Hacks.WriteGA<int>(262145 + 15603, toggle ? 20000 : 15000);      // 15-19
        Hacks.WriteGA<int>(262145 + 15604, toggle ? 20000 : 15500);      // 20-24
        Hacks.WriteGA<int>(262145 + 15605, toggle ? 20000 : 16000);      // 25-29
        Hacks.WriteGA<int>(262145 + 15606, toggle ? 20000 : 16500);      // 30-34
        Hacks.WriteGA<int>(262145 + 15607, toggle ? 20000 : 17000);      // 35-39
        Hacks.WriteGA<int>(262145 + 15608, toggle ? 20000 : 17500);      // 40-44
        Hacks.WriteGA<int>(262145 + 15609, toggle ? 20000 : 17750);      // 45-49
        Hacks.WriteGA<int>(262145 + 15610, toggle ? 20000 : 18000);      // 50-59
        Hacks.WriteGA<int>(262145 + 15611, toggle ? 20000 : 18250);      // 60-69
        Hacks.WriteGA<int>(262145 + 15612, toggle ? 20000 : 18500);      // 70-79
        Hacks.WriteGA<int>(262145 + 15613, toggle ? 20000 : 18750);      // 80-89
        Hacks.WriteGA<int>(262145 + 15614, toggle ? 20000 : 19000);      // 90-990
        Hacks.WriteGA<int>(262145 + 15615, toggle ? 20000 : 19500);      // 100-11
        Hacks.WriteGA<int>(262145 + 15616, toggle ? 20000 : 20000);      // 111
    }

    public static void Remove_Bunker_Supply_Delay(bool toggle)
    {
        Hacks.WriteGA<int>(262145 + 21349, toggle ? 0 : 600);
    }

    public static void Set_Bunker_Produce_Research_Time(int produce_time, bool toggle)
    {
        // Base Time to Produce
        Hacks.WriteGA<int>(262145 + 21324, toggle ? produce_time : 600000);     // Product                  215868155 
        Hacks.WriteGA<int>(262145 + 21340, toggle ? produce_time : 300000);     // Research                 -676414773

        // Time to Produce Reductions
        Hacks.WriteGA<int>(262145 + 21325, toggle ? produce_time : 90000);      // Production Equipment     631477612
        Hacks.WriteGA<int>(262145 + 21326, toggle ? produce_time : 90000);      // Production Staff         818645907
        Hacks.WriteGA<int>(262145 + 21341, toggle ? produce_time : 45000);      // Research Equipment       -1148432846
        Hacks.WriteGA<int>(262145 + 21342, toggle ? produce_time : 45000);      // Research Staff           510883248
    }

    public static void Set_Bunker_Resupply_Costs(bool toggle)
    {
        Hacks.WriteGA<int>(262145 + 21347, toggle ? 200 : 15000);
        Hacks.WriteGA<int>(262145 + 21348, toggle ? 200 : 15000);
    }

    public static void Set_Bunker_Sale_Multipliers(bool toggle)
    {
        // Sale Multipliers
        Hacks.WriteGA<float>(262145 + 21303, toggle ? 2.0f : 1.0f);     // Near         1865029244
        Hacks.WriteGA<float>(262145 + 21304, toggle ? 3.0f : 1.5f);     // Far          1021567941
    }

    public static void Set_MC_Sale_Multipliers(bool toggle)
    {
        // Sale Multipliers
        Hacks.WriteGA<float>(262145 + 18861, toggle ? 2.0f : 1.0f);     // Near         -823848572
        Hacks.WriteGA<float>(262145 + 18862, toggle ? 3.0f : 1.5f);     // Far          1763638426
    }

    public static void Set_Bunker_Supplies_Per_Unit_Produced(bool toggle)
    {
        // Supplies Per Unit Produced
        Hacks.WriteGA<int>(262145 + 21327, toggle ? 1 : 10);        // Product Base              -1652502760
        Hacks.WriteGA<int>(262145 + 21328, toggle ? 1 : 5);         // Product Upgraded          1647327744
        Hacks.WriteGA<int>(262145 + 21343, toggle ? 1 : 2);         // Research Base             1485279815
        Hacks.WriteGA<int>(262145 + 21344, toggle ? 1 : 1);         // Research Upgraded         2041812011
    }

    public static void Set_MC_Supplies_Per_Unit_Produced(bool toggle)
    {
        // Supplies Per Unit Produced
        Hacks.WriteGA<int>(262145 + 17213, toggle ? 1 : 4);         // Documents Base            -1839004359
        Hacks.WriteGA<int>(262145 + 17214, toggle ? 1 : 10);        // Cash Base
        Hacks.WriteGA<int>(262145 + 17215, toggle ? 1 : 50);        // Cocaine Base
        Hacks.WriteGA<int>(262145 + 17216, toggle ? 1 : 24);        // Meth Base
        Hacks.WriteGA<int>(262145 + 17217, toggle ? 1 : 4);         // Weed Base
        Hacks.WriteGA<int>(262145 + 17218, toggle ? 1 : 2);         // Documents Upgraded
        Hacks.WriteGA<int>(262145 + 17219, toggle ? 1 : 5);         // Cash Upgraded
        Hacks.WriteGA<int>(262145 + 17220, toggle ? 1 : 25);        // Cocaine Upgraded
        Hacks.WriteGA<int>(262145 + 17221, toggle ? 1 : 12);        // Meth Upgraded
        Hacks.WriteGA<int>(262145 + 17222, toggle ? 1 : 2);         // Weed Upgraded
    }

    public static void Unlock_Bunker_Research(bool toggle)
    {
        Hacks.WriteGA<int>(262145 + 21477, toggle ? 1 : 0);         // 886070202
    }

    public static void Set_Nightclub_Produce_Time(int produce_time, bool toggle)
    {
        // Time to Produce
        Hacks.WriteGA<int>(262145 + 24135, toggle ? produce_time : 4800000);    // Sporting Goods               -147565853
        Hacks.WriteGA<int>(262145 + 24136, toggle ? produce_time : 14400000);   // South American Imports
        Hacks.WriteGA<int>(262145 + 24137, toggle ? produce_time : 7200000);    // Pharmaceutical Research
        Hacks.WriteGA<int>(262145 + 24138, toggle ? produce_time : 2400000);    // Organic Produce
        Hacks.WriteGA<int>(262145 + 24139, toggle ? produce_time : 1800000);    // Printing and Copying
        Hacks.WriteGA<int>(262145 + 24140, toggle ? produce_time : 3600000);    // Cash Creation
        Hacks.WriteGA<int>(262145 + 24141, toggle ? produce_time : 8400000);    // Cargo and Shipments
    }

    public static void Set_MC_Produce_Time(int produce_time, bool toggle)
    {
        // Base Time to Produce
        Hacks.WriteGA<int>(262145 + 17198, toggle ? produce_time : 360000);     // Weed         -635596193
        Hacks.WriteGA<int>(262145 + 17199, toggle ? produce_time : 1800000);    // Meth
        Hacks.WriteGA<int>(262145 + 17200, toggle ? produce_time : 3000000);    // Cocaine
        Hacks.WriteGA<int>(262145 + 17201, toggle ? produce_time : 300000);     // Documents
        Hacks.WriteGA<int>(262145 + 17202, toggle ? produce_time : 720000);     // Cash

        // Time to Produce Reductions
        Hacks.WriteGA<int>(262145 + 17203, toggle ? 1 : 60000);     // Documents Equipment
        Hacks.WriteGA<int>(262145 + 17204, toggle ? 1 : 120000);    // Cash Equipment
        Hacks.WriteGA<int>(262145 + 17205, toggle ? 1 : 600000);    // Cocaine Equipment
        Hacks.WriteGA<int>(262145 + 17206, toggle ? 1 : 360000);    // Meth Equipment
        Hacks.WriteGA<int>(262145 + 17207, toggle ? 1 : 60000);     // Weed Equipment
        Hacks.WriteGA<int>(262145 + 17208, toggle ? 1 : 60000);     // Documents Staff
        Hacks.WriteGA<int>(262145 + 17209, toggle ? 1 : 120000);    // Cash Staff
        Hacks.WriteGA<int>(262145 + 17210, toggle ? 1 : 600000);    // Cocaine Staff
        Hacks.WriteGA<int>(262145 + 17211, toggle ? 1 : 360000);    // Meth Staff
        Hacks.WriteGA<int>(262145 + 17212, toggle ? 1 : 60000);     // Weed Staff
    }

    public static void Remove_MC_Supply_Delay(bool toggle)
    {
        Hacks.WriteGA<int>(262145 + 18749, toggle ? 0 : 600);           // 728170457
    }

    public static void Set_MC_Resupply_Costs(bool toggle)
    {
        Hacks.WriteGA<int>(262145 + 18748, toggle ? 200 : 15000);       // Discounted Resupply Cost, BIKER_PURCHASE_SUPPLIES_COST_PER_SEGMENT
    }

    public static void Merry_Weather_Services(int serverId)
    {
        Hacks.WriteGA<int>(2810701 + serverId, 1);
    }

    public static void Remove_Export_Vehicle_Delay(bool toggle)
    {
        Hacks.WriteGA<int>(262145 + 19477, toggle ? 0 : 1200000);          // 1001423248
        Hacks.WriteGA<int>(262145 + 19478, toggle ? 0 : 1680000);
        Hacks.WriteGA<int>(262145 + 19479, toggle ? 0 : 2340000);
        Hacks.WriteGA<int>(262145 + 19480, toggle ? 0 : 2880000);
    }

    public static void Disconnect()
    {
        Hacks.WriteGA<int>(31788, 1);
        Thread.Sleep(20);
        Hacks.WriteGA<int>(31788, 0);
    }

    public static void RemoveSmuggler_Run_In_Delay(bool toggle)
    {
        Hacks.WriteGA<int>(262145 + 22522, toggle ? 0 : 120000);          // 1278611667
        Hacks.WriteGA<int>(262145 + 22523, toggle ? 0 : 180000);
        Hacks.WriteGA<int>(262145 + 22524, toggle ? 0 : 240000);
        Hacks.WriteGA<int>(262145 + 22525, toggle ? 0 : 60000);
    }

    public static void Remove_Smuggler_Run_Out_Delay(bool toggle)
    {
        Hacks.WriteGA<int>(262145 + 22561, toggle ? 0 : 180000);          // -1525481945
    }

    public static void Remove_Nightclub_Out_Delay(bool toggle)
    {
        Hacks.WriteGA<int>(262145 + 24216, toggle ? 0 : 300000);      // 1763921019
        Hacks.WriteGA<int>(262145 + 24251, toggle ? 0 : 300000);      // -1004589438
        Hacks.WriteGA<int>(262145 + 24252, toggle ? 0 : 300000);
    }

    public static void Nightclub_No_Tony_Laundering_Money(bool toggle)
    {
        Hacks.WriteGA<float>(262145 + 24258, toggle ? 0.000001f : 0.1f);        // -1002770353
    }

    public static void Deliver_Personal_Vehicle(int index)
    {
        Hacks.WriteGA<int>(Hacks.oVMYCar + 965, index);
        Hacks.WriteGA<int>(Hacks.oVMYCar + 962, 1);
    }
}
