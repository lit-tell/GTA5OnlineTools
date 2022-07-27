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
    ///  6, 私人好友战局
    ///  9, 加入好友
    ///  10 单人战局
    ///  11 仅限邀请战局
    ///  12 加入帮会伙伴
    /// </summary>
    /// <param name="sessionID">战局ID</param>
    public static void LoadSession(int sessionID)
    {
        Task.Run(() =>
        {
            Memory.SetForegroundWindow();

            if (sessionID == -1)
            {
                // 离开线上模式需要特殊处理
                Hacks.WriteGA<int>(Offsets.InitSession_Cache, -1);
                Hacks.WriteGA<int>(Offsets.InitSession_State, 1);
                Task.Delay(200).Wait();
                Hacks.WriteGA<int>(Offsets.InitSession_State, 0);
            }
            else
            {
                // 正常切换战局，修改战局类型，然后切换战局状态
                Hacks.WriteGA<int>(Offsets.InitSession_Type, sessionID);
                Hacks.WriteGA<int>(Offsets.InitSession_State, 1);
                Task.Delay(200).Wait();
                Hacks.WriteGA<int>(Offsets.InitSession_State, 0);
            }
        });
    }

    /// <summary>
    /// 空战局（原理：暂停GTA5进程10秒钟）
    /// </summary>
    public static void EmptySession()
    {
        Task.Run(() =>
        {
            ProcessMgr.SuspendProcess(Memory.GetProcessID());
            Task.Delay(10000).Wait();
            ProcessMgr.ResumeProcess(Memory.GetProcessID());
        });
    }

    /// <summary>
    /// 模型变更
    /// </summary>
    /// <param name="hash"></param>
    public static void ModelChanger(long hash)
    {
        Hacks.WriteGA<int>(Offsets.oVGETIn + 59, 1);                // triggerModelChange   Global_2671449.f_59
        Hacks.WriteGA<long>(Offsets.oVGETIn + 46, hash);            // modelChangeHash      Global_2671449.f_46
        Thread.Sleep(10);
        Hacks.WriteGA<int>(Offsets.oVGETIn + 59, 0);
    }

    /// <summary>
    /// 挂机防踢
    /// </summary>
    /// <param name="isEnable"></param>
    public static void AntiAFK(bool isEnable)
    {
        Hacks.WriteGA<int>(262145 + 87, isEnable ? 99999999 : 120000);        // 120000 
        Hacks.WriteGA<int>(262145 + 88, isEnable ? 99999999 : 300000);        // 300000 
        Hacks.WriteGA<int>(262145 + 89, isEnable ? 99999999 : 600000);        // 600000 
        Hacks.WriteGA<int>(262145 + 90, isEnable ? 99999999 : 900000);        // 900000 
        Hacks.WriteGA<int>(262145 + 8041, isEnable ? 2000000000 : 30000);     // 30000  
        Hacks.WriteGA<int>(262145 + 8042, isEnable ? 2000000000 : 60000);     // 60000  
        Hacks.WriteGA<int>(262145 + 8043, isEnable ? 2000000000 : 90000);     // 90000  
        Hacks.WriteGA<int>(262145 + 8044, isEnable ? 2000000000 : 120000);    // 120000 
    }

    /// <summary>
    /// 允许非公共战局运货
    /// </summary>
    /// <param name="isEnable"></param>
    public static void AllowSellOnNonPublic(bool isEnable)
    {
        Hacks.WriteGA<int>(2714762 + 744, isEnable ? 0 : 1);
    }

    /// <summary>
    /// 移除自杀CD
    /// </summary>
    /// <param name="isEnable"></param>
    public static void RemoveSuicideCooldown(bool isEnable)
    {
        if (isEnable)
            Hacks.WriteGA<int>(2810701 + 6729, 0);

        Hacks.WriteGA<int>(262145 + 28072, isEnable ? 3 : 300000);
        Hacks.WriteGA<int>(262145 + 28073, isEnable ? 3 : 60000);
    }

    /// <summary>
    /// 移除被动模式CD
    /// </summary>
    /// <param name="isEnable"></param>
    public static void RemovePassiveModeCooldown(bool isEnable)
    {
        Hacks.WriteGA<int>(2810701 + 4460, isEnable ? 0 : 1);
        Hacks.WriteGA<int>(1966542, isEnable ? 0 : 1);
    }

    /// <summary>
    /// 移除轨道炮CD
    /// </summary>
    /// <param name="isEnable"></param>
    public static void DisableOrbitalCooldown(bool isEnable)
    {
        Hacks.WriteGA<int>(262145 + 22853, isEnable ? 0 : 2880000);
    }

    /// <summary>
    /// 进入线上个人载具
    /// </summary>
    public static void GetInOnlinePV()
    {
        Hacks.WriteGA<int>(Offsets.oVGETIn + 8, 1);
    }

    /// <summary>
    /// 战局雪天
    /// </summary>
    /// <param name="isEnable"></param>
    public static void SessionSnow(bool isEnable)
    {
        Hacks.WriteGA<int>(262145 + 4751, isEnable ? 1 : 0);            // turn snow on / off Global_262145.f_4751
    }

    /// <summary>
    /// 雷达影踪/人间蒸发
    /// </summary>
    /// <param name="isEnable"></param>
    public static void OffRadar(bool isEnable)
    {
        Hacks.WriteGA<int>(Offsets.oPlayerIDHelp + 1 + Hacks.PlayerID() * 451 + 207, isEnable ? 1 : 0);
        if (isEnable)
            Hacks.WriteGA<int>(Offsets.oNETTimeHelp + 56, Hacks.GetNetworkTime() + 3600000);
        Hacks.WriteGA<int>(Offsets.oVMYCar + 4630, isEnable ? 3 : 0);
    }

    /// <summary>
    /// 幽灵组织
    /// </summary>
    /// <param name="isEnable"></param>
    public static void GhostOrganization(bool isEnable)
    {
        Hacks.WriteGA<int>(Offsets.oPlayerIDHelp + 1 + Hacks.PlayerID() * 451 + 207, isEnable ? 1 : 0);
        if (isEnable)
            Hacks.WriteGA<int>(Offsets.oNETTimeHelp + 56, Hacks.GetNetworkTime() + 3600000);
        Hacks.WriteGA<int>(Offsets.oVMYCar + 4630, isEnable ? 4 : 0);
    }

    /// <summary>
    /// 警察无视犯罪
    /// </summary>
    /// <param name="isEnable"></param>
    public static void BribeOrBlindCops(bool isEnable)
    {
        Hacks.WriteGA<int>(Offsets.oVMYCar + 4625, isEnable ? 1 : 0);
        Hacks.WriteGA<int>(Offsets.oVMYCar + 4627, isEnable ? Hacks.GetNetworkTime() + 3600000 : 0);
        Hacks.WriteGA<int>(Offsets.oVMYCar + 4624, isEnable ? 5 : 0);
    }

    /// <summary>
    /// 贿赂当局
    /// </summary>
    /// <param name="isEnable"></param>
    public static void BribeAuthorities(bool isEnable)
    {
        Hacks.WriteGA<int>(Offsets.oVMYCar + 4625, isEnable ? 1 : 0);
        Hacks.WriteGA<int>(Offsets.oVMYCar + 4627, isEnable ? Hacks.GetNetworkTime() + 3600000 : 0);
        Hacks.WriteGA<int>(Offsets.oVMYCar + 4624, isEnable ? 21 : 0);
    }

    /// <summary>
    /// 显示玩家
    /// </summary>
    /// <param name="isEnable"></param>
    public static void RevealPlayers(bool isEnable)
    {
        Hacks.WriteGA<int>(Offsets.oPlayerIDHelp + 1 + Hacks.PlayerID() * 451 + 210, isEnable ? 1 : 0);
        Hacks.WriteGA<int>(Offsets.oNETTimeHelp + 57, isEnable ? Hacks.GetNetworkTime() + 3600000 : 0);
    }

    /// <summary>
    /// 设置角色等级经验倍数
    /// </summary>
    /// <param name="multiplier"></param>
    public static void RPMultiplier(float multiplier)
    {
        Hacks.WriteGA<float>(262145 + 1, multiplier);           // xpMultiplier Global_262145.f_1
    }

    /// <summary>
    /// 设置车友会等级经验倍数
    /// </summary>
    /// <param name="multiplier"></param>
    public static void REPMultiplier(float multiplier)
    {
        Hacks.WriteGA<float>(262145 + 31299, multiplier);        // car meet         汽车见面会       1819417801
        Hacks.WriteGA<float>(262145 + 31300, multiplier);        // Test Track
        Hacks.WriteGA<float>(262145 + 31297, multiplier);        // Head_2_Head      头对头
        Hacks.WriteGA<float>(262145 + 31296, multiplier);        // Scramble         攀登
        Hacks.WriteGA<float>(262145 + 31295, multiplier);        // Pursuit Race     追逐赛
        Hacks.WriteGA<float>(262145 + 31294, multiplier);        // Street Race      街头比赛
    }

    /// <summary>
    /// 使用牛鲨睾酮
    /// </summary>
    /// <param name="isEnable"></param>
    public static void InstantBullShark(bool isEnable)
    {
        Hacks.WriteGA<int>(Offsets.oNETTimeHelp + 3576, isEnable ? 1 : 5);
    }

    /// <summary>
    /// 呼叫支援直升机
    /// </summary>
    /// <param name="isEnable"></param>
    public static void CallBackupHeli(bool isEnable)
    {
        Hacks.WriteGA<int>(Offsets.oVMYCar + 4454, isEnable ? 1 : 0);
    }

    /// <summary>
    /// 呼叫空袭
    /// </summary>
    /// <param name="isEnable"></param>
    public static void CallAirstrike(bool isEnable)
    {
        Hacks.WriteGA<int>(Offsets.oVMYCar + 4455, isEnable ? 1 : 0);
    }

    /// <summary>
    /// 启用CEO特殊货物
    /// </summary>
    /// <param name="isEnable"></param>
    public static void CEOSpecialCargo(bool isEnable)
    {
        Hacks.WriteGA<int>(1946798, isEnable ? 1 : 0);
    }

    /// <summary>
    /// 设置CEO特殊货物类型
    /// </summary>
    /// <param name="cargoID"></param>
    public static void CEOCargoType(int cargoID)
    {
        Hacks.WriteGA<int>(1946644, cargoID);
    }

    /// <summary>
    /// 移除购买CEO板条箱冷却
    /// </summary>
    /// <param name="isEnable"></param>
    public static void CEOBuyingCratesCooldown(bool isEnable)
    {
        Hacks.WriteGA<int>(262145 + 15608, isEnable ? 0 : 300000);          // Special cargo buy cooldown Global_262145.f_15608
    }

    /// <summary>
    /// 移除出售CEO板条箱冷却
    /// </summary>
    /// <param name="isEnable"></param>
    public static void CEOSellingCratesCooldown(bool isEnable)
    {
        Hacks.WriteGA<int>(262145 + 15609, isEnable ? 0 : 1800000);         // Special cargo sell cooldown Global_262145.f_15609
    }

    /// <summary>
    /// 设置CEO板条箱每箱出售单价为2W
    /// </summary>
    /// <param name="isEnable"></param>
    public static void CEOPricePerCrateAtCrates(bool isEnable)
    {
        // tuneable_processing.c    -1445480509   Global_262145.f_15843
        Hacks.WriteGA<int>(262145 + 15843, isEnable ? 20000 : 10000);           // 1        specialCargo1CratesPrice
        Hacks.WriteGA<int>(262145 + 15843 + 1, isEnable ? 20000 : 11000);       // 2        specialCargo2CratesPrice
        Hacks.WriteGA<int>(262145 + 15843 + 2, isEnable ? 20000 : 12000);       // 3        specialCargo3CratesPrice                                                
        Hacks.WriteGA<int>(262145 + 15843 + 3, isEnable ? 20000 : 13000);       // 4-5      specialCargo4to5CratesPrice
        Hacks.WriteGA<int>(262145 + 15843 + 4, isEnable ? 20000 : 13500);       // 6-7      specialCargo6to7CratesPrice
        Hacks.WriteGA<int>(262145 + 15843 + 5, isEnable ? 20000 : 14000);       // 8-9      ...
        Hacks.WriteGA<int>(262145 + 15843 + 6, isEnable ? 20000 : 14500);       // 10-14
        Hacks.WriteGA<int>(262145 + 15843 + 7, isEnable ? 20000 : 15000);       // 15-19
        Hacks.WriteGA<int>(262145 + 15843 + 8, isEnable ? 20000 : 15500);       // 20-24
        Hacks.WriteGA<int>(262145 + 15843 + 9, isEnable ? 20000 : 16000);       // 25-29
        Hacks.WriteGA<int>(262145 + 15843 + 10, isEnable ? 20000 : 16500);      // 30-34
        Hacks.WriteGA<int>(262145 + 15843 + 11, isEnable ? 20000 : 17000);      // 35-39
        Hacks.WriteGA<int>(262145 + 15843 + 12, isEnable ? 20000 : 17500);      // 40-44
        Hacks.WriteGA<int>(262145 + 15843 + 13, isEnable ? 20000 : 17750);      // 45-49
        Hacks.WriteGA<int>(262145 + 15843 + 14, isEnable ? 20000 : 18000);      // 50-59
        Hacks.WriteGA<int>(262145 + 15843 + 15, isEnable ? 20000 : 18250);      // 60-69
        Hacks.WriteGA<int>(262145 + 15843 + 16, isEnable ? 20000 : 18500);      // 70-79
        Hacks.WriteGA<int>(262145 + 15843 + 17, isEnable ? 20000 : 18750);      // 80-89
        Hacks.WriteGA<int>(262145 + 15843 + 18, isEnable ? 20000 : 19000);      // 90-990
        Hacks.WriteGA<int>(262145 + 15843 + 19, isEnable ? 20000 : 19500);      // 100-11
        Hacks.WriteGA<int>(262145 + 15843 + 20, isEnable ? 20000 : 20000);      // 111
    }

    /// <summary>
    /// 移除地堡进货延迟
    /// </summary>
    /// <param name="isEnable"></param>
    public static void RemoveBunkerSupplyDelay(bool isEnable)
    {
        Hacks.WriteGA<int>(262145 + 21349, isEnable ? 0 : 600);
    }

    /// <summary>
    /// 设置地堡生产和研究时间为指定时间，单位秒
    /// </summary>
    /// <param name="isEnable"></param>
    /// <param name="produce_time"></param>
    public static void SetBunkerProduceResearchTime(bool isEnable, int produce_time)
    {
        // Base Time to Produce
        Hacks.WriteGA<int>(262145 + 21324, isEnable ? produce_time : 600000);     // Product                  215868155 
        Hacks.WriteGA<int>(262145 + 21340, isEnable ? produce_time : 300000);     // Research                 -676414773

        // Time to Produce Reductions
        Hacks.WriteGA<int>(262145 + 21325, isEnable ? produce_time : 90000);      // Production Equipment     631477612
        Hacks.WriteGA<int>(262145 + 21326, isEnable ? produce_time : 90000);      // Production Staff         818645907
        Hacks.WriteGA<int>(262145 + 21341, isEnable ? produce_time : 45000);      // Research Equipment       -1148432846
        Hacks.WriteGA<int>(262145 + 21342, isEnable ? produce_time : 45000);      // Research Staff           510883248
    }

    /// <summary>
    /// 设置地堡进货单价为200元
    /// </summary>
    /// <param name="isEnable"></param>
    public static void SetBunkerResupplyCosts(bool isEnable)
    {
        Hacks.WriteGA<int>(262145 + 21347, isEnable ? 200 : 15000);
        Hacks.WriteGA<int>(262145 + 21348, isEnable ? 200 : 15000);
    }

    /// <summary>
    /// 设置地堡远近出货倍数
    /// </summary>
    /// <param name="isEnable"></param>
    public static void SetBunkerSaleMultipliers(bool isEnable)
    {
        // Sale Multipliers
        Hacks.WriteGA<float>(262145 + 21303, isEnable ? 2.0f : 1.0f);     // Near         1865029244
        Hacks.WriteGA<float>(262145 + 21304, isEnable ? 3.0f : 1.5f);     // Far          1021567941
    }

    /// <summary>
    /// 设置摩托帮远近出货倍数
    /// </summary>
    /// <param name="isEnable"></param>
    public static void SetMCSaleMultipliers(bool isEnable)
    {
        // Sale Multipliers
        Hacks.WriteGA<float>(262145 + 18861, isEnable ? 2.0f : 1.0f);     // Near         -823848572
        Hacks.WriteGA<float>(262145 + 18862, isEnable ? 3.0f : 1.5f);     // Far          1763638426
    }

    /// <summary>
    /// 设置地堡原材料消耗量
    /// </summary>
    /// <param name="isEnable"></param>
    public static void SetBunkerSuppliesPerUnitProduced(bool isEnable)
    {
        // Supplies Per Unit Produced
        Hacks.WriteGA<int>(262145 + 21327, isEnable ? 1 : 10);        // Product Base              -1652502760
        Hacks.WriteGA<int>(262145 + 21328, isEnable ? 1 : 5);         // Product Upgraded          1647327744
        Hacks.WriteGA<int>(262145 + 21343, isEnable ? 1 : 2);         // Research Base             1485279815
        Hacks.WriteGA<int>(262145 + 21344, isEnable ? 1 : 1);         // Research Upgraded         2041812011
    }

    /// <summary>
    /// 设置摩托帮原材料消耗量
    /// </summary>
    /// <param name="isEnable"></param>
    public static void SetMCSuppliesPerUnitProduced(bool isEnable)
    {
        // Supplies Per Unit Produced
        Hacks.WriteGA<int>(262145 + 17213, isEnable ? 1 : 4);         // Documents Base            -1839004359
        Hacks.WriteGA<int>(262145 + 17214, isEnable ? 1 : 10);        // Cash Base
        Hacks.WriteGA<int>(262145 + 17215, isEnable ? 1 : 50);        // Cocaine Base
        Hacks.WriteGA<int>(262145 + 17216, isEnable ? 1 : 24);        // Meth Base
        Hacks.WriteGA<int>(262145 + 17217, isEnable ? 1 : 4);         // Weed Base
        Hacks.WriteGA<int>(262145 + 17218, isEnable ? 1 : 2);         // Documents Upgraded
        Hacks.WriteGA<int>(262145 + 17219, isEnable ? 1 : 5);         // Cash Upgraded
        Hacks.WriteGA<int>(262145 + 17220, isEnable ? 1 : 25);        // Cocaine Upgraded
        Hacks.WriteGA<int>(262145 + 17221, isEnable ? 1 : 12);        // Meth Upgraded
        Hacks.WriteGA<int>(262145 + 17222, isEnable ? 1 : 2);         // Weed Upgraded
    }

    /// <summary>
    /// 解锁地堡所有研究 (临时)
    /// </summary>
    /// <param name="isEnable"></param>
    public static void UnlockBunkerResearch(bool isEnable)
    {
        Hacks.WriteGA<int>(262145 + 21477, isEnable ? 1 : 0);         // 886070202
    }

    /// <summary>
    /// 设置夜总会生产时间为指定时间，单位秒
    /// </summary>
    /// <param name="isEnable"></param>
    /// <param name="produce_time"></param>
    public static void SetNightclubProduceTime(bool isEnable, int produce_time)
    {
        // Time to Produce
        Hacks.WriteGA<int>(262145 + 24135, isEnable ? produce_time : 4800000);    // Sporting Goods               -147565853
        Hacks.WriteGA<int>(262145 + 24136, isEnable ? produce_time : 14400000);   // South American Imports
        Hacks.WriteGA<int>(262145 + 24137, isEnable ? produce_time : 7200000);    // Pharmaceutical Research
        Hacks.WriteGA<int>(262145 + 24138, isEnable ? produce_time : 2400000);    // Organic Produce
        Hacks.WriteGA<int>(262145 + 24139, isEnable ? produce_time : 1800000);    // Printing and Copying
        Hacks.WriteGA<int>(262145 + 24140, isEnable ? produce_time : 3600000);    // Cash Creation
        Hacks.WriteGA<int>(262145 + 24141, isEnable ? produce_time : 8400000);    // Cargo and Shipments
    }

    /// <summary>
    /// 设置摩托帮生产时间为指定时间，单位秒
    /// </summary>
    /// <param name="isEnable"></param>
    /// <param name="produce_time"></param>
    public static void SetMCProduceTime(bool isEnable, int produce_time)
    {
        // Base Time to Produce
        Hacks.WriteGA<int>(262145 + 17198, isEnable ? produce_time : 360000);     // Weed         -635596193
        Hacks.WriteGA<int>(262145 + 17199, isEnable ? produce_time : 1800000);    // Meth
        Hacks.WriteGA<int>(262145 + 17200, isEnable ? produce_time : 3000000);    // Cocaine
        Hacks.WriteGA<int>(262145 + 17201, isEnable ? produce_time : 300000);     // Documents
        Hacks.WriteGA<int>(262145 + 17202, isEnable ? produce_time : 720000);     // Cash

        // Time to Produce Reductions
        Hacks.WriteGA<int>(262145 + 17203, isEnable ? 1 : 60000);     // Documents Equipment
        Hacks.WriteGA<int>(262145 + 17204, isEnable ? 1 : 120000);    // Cash Equipment
        Hacks.WriteGA<int>(262145 + 17205, isEnable ? 1 : 600000);    // Cocaine Equipment
        Hacks.WriteGA<int>(262145 + 17206, isEnable ? 1 : 360000);    // Meth Equipment
        Hacks.WriteGA<int>(262145 + 17207, isEnable ? 1 : 60000);     // Weed Equipment
        Hacks.WriteGA<int>(262145 + 17208, isEnable ? 1 : 60000);     // Documents Staff
        Hacks.WriteGA<int>(262145 + 17209, isEnable ? 1 : 120000);    // Cash Staff
        Hacks.WriteGA<int>(262145 + 17210, isEnable ? 1 : 600000);    // Cocaine Staff
        Hacks.WriteGA<int>(262145 + 17211, isEnable ? 1 : 360000);    // Meth Staff
        Hacks.WriteGA<int>(262145 + 17212, isEnable ? 1 : 60000);     // Weed Staff
    }

    /// <summary>
    /// 移除摩托帮进货延迟
    /// </summary>
    /// <param name="isEnable"></param>
    public static void RemoveMCSupplyDelay(bool isEnable)
    {
        Hacks.WriteGA<int>(262145 + 18749, isEnable ? 0 : 600);           // 728170457
    }

    /// <summary>
    /// 设置摩托帮进货单价为200元
    /// </summary>
    /// <param name="isEnable"></param>
    public static void SetMCResupplyCosts(bool isEnable)
    {
        Hacks.WriteGA<int>(262145 + 18748, isEnable ? 200 : 15000);       // Discounted Resupply Cost, BIKER_PURCHASE_SUPPLIES_COST_PER_SEGMENT
    }

    /// <summary>
    /// 设置梅利威瑟服务类型
    /// </summary>
    /// <param name="serverId"></param>
    public static void MerryWeatherServices(int serverId)
    {
        Hacks.WriteGA<int>(2810701 + serverId, 1);
    }

    /// <summary>
    /// 移除进出口大亨出货CD
    /// </summary>
    /// <param name="isEnable"></param>
    public static void RemoveExportVehicleDelay(bool isEnable)
    {
        Hacks.WriteGA<int>(262145 + 19477, isEnable ? 0 : 1200000);          // 1001423248
        Hacks.WriteGA<int>(262145 + 19478, isEnable ? 0 : 1680000);
        Hacks.WriteGA<int>(262145 + 19479, isEnable ? 0 : 2340000);
        Hacks.WriteGA<int>(262145 + 19480, isEnable ? 0 : 2880000);
    }

    /// <summary>
    /// 断开战局连接
    /// </summary>
    public static void Disconnect()
    {
        Hacks.WriteGA<int>(31788, 1);
        Thread.Sleep(20);
        Hacks.WriteGA<int>(31788, 0);
    }

    /// <summary>
    /// 移除机库进货CD
    /// </summary>
    /// <param name="isEnable"></param>
    public static void RemoveSmugglerRunInDelay(bool isEnable)
    {
        Hacks.WriteGA<int>(262145 + 22522, isEnable ? 0 : 120000);          // 1278611667
        Hacks.WriteGA<int>(262145 + 22523, isEnable ? 0 : 180000);
        Hacks.WriteGA<int>(262145 + 22524, isEnable ? 0 : 240000);
        Hacks.WriteGA<int>(262145 + 22525, isEnable ? 0 : 60000);
    }

    /// <summary>
    /// 移除机库出货CD
    /// </summary>
    /// <param name="isEnable"></param>
    public static void RemoveSmugglerRunOutDelay(bool isEnable)
    {
        Hacks.WriteGA<int>(262145 + 22561, isEnable ? 0 : 180000);          // -1525481945
    }

    /// <summary>
    /// 移除夜总会出货CD
    /// </summary>
    /// <param name="isEnable"></param>
    public static void RemoveNightclubOutDelay(bool isEnable)
    {
        Hacks.WriteGA<int>(262145 + 24216, isEnable ? 0 : 300000);          // 1763921019
        Hacks.WriteGA<int>(262145 + 24251, isEnable ? 0 : 300000);          // -1004589438
        Hacks.WriteGA<int>(262145 + 24252, isEnable ? 0 : 300000);
    }

    /// <summary>
    /// 夜总会托尼洗钱费用
    /// </summary>
    /// <param name="isEnable"></param>
    public static void NightclubNoTonyLaundering_Money(bool isEnable)
    {
        // 托尼分红 GA(262145+24162) 默认10000
        Hacks.WriteGA<float>(262145 + 24258, isEnable ? 0.000001f : 0.1f);        // -1002770353
    }

    public static void DeliverPersonalVehicle(int index)
    {
        Hacks.WriteGA<int>(Offsets.oVMYCar + 965, index);
        Hacks.WriteGA<int>(Offsets.oVMYCar + 962, 1);
    }
}
