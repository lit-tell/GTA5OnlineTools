using GTA5OnlineTools.Features.Core;
using GTA5OnlineTools.Features.Data;

namespace GTA5OnlineTools.Features.SDK
{
    public class Online
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
        public static void LoadSession(int sessionID)
        {
            Task.Run(() =>
            {
                Memory.SetForegroundWindow();

                if (sessionID == -1)
                {
                    Hacks.WriteGA<int>(Offsets.InitSession_Cache, -1);
                    Hacks.WriteGA<int>(Offsets.InitSession, 1);
                    Task.Delay(200).Wait();
                    Hacks.WriteGA<int>(Offsets.InitSession, 0);
                }
                else
                {
                    Hacks.WriteGA<int>(Offsets.InitSession_Type, sessionID);
                    Hacks.WriteGA<int>(Offsets.InitSession, 1);
                    Task.Delay(200).Wait();
                    Hacks.WriteGA<int>(Offsets.InitSession, 0);
                }
            });
        }

        /// <summary>
        /// 空战局，暂停GTA5进程10秒钟
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
        /// 允许非公共战局运货
        /// </summary>
        public static void AllowSellOnNonPublic(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(2714635 + 744, 0);
            }
            else
            {
                Hacks.WriteGA<int>(2714635 + 744, 1);
            }
        }

        /// <summary>
        /// 移除自杀CD
        /// </summary>
        public static void RemoveSuicideCooldown(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(2810701 + 6729, 0);
                Hacks.WriteGA<int>(262145 + 28072, 3);
                Hacks.WriteGA<int>(262145 + 28073, 3);
            }
            else
            {
                Hacks.WriteGA<int>(262145 + 28072, 300000);
                Hacks.WriteGA<int>(262145 + 28073, 60000);
            }
        }

        /// <summary>
        /// 移除被动模式CD
        /// </summary>
        public static void RemovePassiveModeCooldown(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(2810701 + 4460, 0);
                Hacks.WriteGA<int>(1966542, 0);
            }
            else
            {
                Hacks.WriteGA<int>(2810701 + 4460, 1);
                Hacks.WriteGA<int>(1966542, 1);
            }
        }

        /// <summary>
        /// 禁用轨道炮CD
        /// </summary>
        public static void DisableOrbitalCooldown(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(262145 + 22853, 0);
            }
            else
            {
                Hacks.WriteGA<int>(262145 + 22853, 2880000);
            }
        }

        /// <summary>
        /// 坐进线上个人载具
        /// </summary>
        public static void GetInOnlinePV()
        {
            Hacks.WriteGA<int>(2671447 + 8, 1);
        }

        /// <summary>
        /// 战局雪天 (仅自己可见)
        /// </summary>
        public static void SessionSnow(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(262145 + 4723, 1);
            }
            else
            {
                Hacks.WriteGA<int>(262145 + 4723, 0);
            }
        }

        /// <summary>
        /// 雷达影踪/人间蒸发
        /// </summary>
        public static void OffRadar(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(Offsets.oPlayerIDHelp + 1 + Hacks.PLAYER_ID() * 451 + 207, 1);
                Hacks.WriteGA<int>(Offsets.oNETTimeHelp + 56, Hacks.GET_NETWORK_TIME() + 3600000);
                Hacks.WriteGA<int>(Offsets.oVMYCar + 4630, 3);
            }
            else
            {
                Hacks.WriteGA<int>(Offsets.oPlayerIDHelp + 1 + Hacks.PLAYER_ID() * 451 + 207, 0);
                Hacks.WriteGA<int>(Offsets.oVMYCar + 4630, 0);
            }
        }

        /// <summary>
        /// 幽灵组织
        /// </summary>
        public static void GhostOrganization(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(Offsets.oPlayerIDHelp + 1 + Hacks.PLAYER_ID() * 451 + 207, 1);
                Hacks.WriteGA<int>(Offsets.oNETTimeHelp + 56, Hacks.GET_NETWORK_TIME() + 3600000);
                Hacks.WriteGA<int>(Offsets.oVMYCar + 4630, 4);
            }
            else
            {
                Hacks.WriteGA<int>(Offsets.oPlayerIDHelp + 1 + Hacks.PLAYER_ID() * 451 + 207, 0);
                Hacks.WriteGA<int>(Offsets.oVMYCar + 4630, 0);
            }
        }

        /// <summary>
        /// 警察无视犯罪
        /// </summary>
        public static void BribeOrBlindCops(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(Offsets.oVMYCar + 4625, 1);
                Hacks.WriteGA<int>(Offsets.oVMYCar + 4627, Hacks.GET_NETWORK_TIME() + 3600000);
                Hacks.WriteGA<int>(Offsets.oVMYCar + 4624, 5);
            }
            else
            {
                Hacks.WriteGA<int>(Offsets.oVMYCar + 4625, 0);
                Hacks.WriteGA<int>(Offsets.oVMYCar + 4627, 0);
                Hacks.WriteGA<int>(Offsets.oVMYCar + 4624, 0);
            }
        }

        /// <summary>
        /// 贿赂当局
        /// </summary>
        public static void BribeAuthorities(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(Offsets.oVMYCar + 4625, 1);
                Hacks.WriteGA<int>(Offsets.oVMYCar + 4627, Hacks.GET_NETWORK_TIME() + 3600000);
                Hacks.WriteGA<int>(Offsets.oVMYCar + 4624, 21);
            }
            else
            {
                Hacks.WriteGA<int>(Offsets.oVMYCar + 4625, 0);
                Hacks.WriteGA<int>(Offsets.oVMYCar + 4627, 0);
                Hacks.WriteGA<int>(Offsets.oVMYCar + 4624, 0);
            }
        }

        /// <summary>
        /// 显示玩家
        /// </summary>
        public static void RevealPlayers(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(Offsets.oPlayerIDHelp + 1 + Hacks.PLAYER_ID() * 451 + 210, 1);
                Hacks.WriteGA<int>(Offsets.oNETTimeHelp + 57, Hacks.GET_NETWORK_TIME() + 3600000);
            }
            else
            {
                Hacks.WriteGA<int>(Offsets.oPlayerIDHelp + 1 + Hacks.PLAYER_ID() * 451 + 210, 0);
                Hacks.WriteGA<int>(Offsets.oNETTimeHelp + 57, 0);
            }
        }

        /// <summary>
        /// 设置角色等级经验倍数
        /// </summary>
        public static void RPMultiplier(float multiplier)
        {
            Hacks.WriteGA<float>(262145 + 1, multiplier);
        }

        /// <summary>
        /// 设置车友会等级经验倍数
        /// </summary>
        public static void REPMultiplier(float multiplier)
        {
            // car meet 汽车见面会       1819417801
            Hacks.WriteGA<float>(262145 + 31299, multiplier);
            // Test Track
            Hacks.WriteGA<float>(262145 + 31300, multiplier);
            // Head_2_Head 头对头
            Hacks.WriteGA<float>(262145 + 31297, multiplier);
            // Scramble 攀登
            Hacks.WriteGA<float>(262145 + 31296, multiplier);
            // Pursuit Race 追逐赛
            Hacks.WriteGA<float>(262145 + 31295, multiplier);
            // Street Race 街头比赛
            Hacks.WriteGA<float>(262145 + 31294, multiplier);
        }

        /// <summary>
        /// 使用牛鲨睾酮
        /// </summary>
        public static void InstantBullShark(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(2703660 + 3576, 1);
            }
            else
            {
                Hacks.WriteGA<int>(2703660 + 3576, 5);
            }
        }

        /// <summary>
        /// 呼叫支援直升机
        /// </summary>
        public static void BackupHeli(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(2810701 + 4454, 1);
            }
            else
            {
                Hacks.WriteGA<int>(2810701 + 4454, 0);
            }
        }

        /// <summary>
        /// 呼叫空袭
        /// </summary>
        public static void Airstrike(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(2810701 + 4455, 1);
            }
            else
            {
                Hacks.WriteGA<int>(2810701 + 4455, 0);
            }
        }

        /// <summary>
        /// CEO特殊货物
        /// </summary>
        public static void CEOSpecialCargo(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(1946798, 1);
            }
            else
            {
                Hacks.WriteGA<int>(1946798, 0);
            }
        }

        /// <summary>
        /// CEO特殊货物
        /// </summary>
        public static void CEOCargoType(int cargoID)
        {
            Hacks.WriteGA<int>(1946644, cargoID);
        }

        /// <summary>
        /// CEO移除购买板条箱冷却
        /// </summary>
        public static void CEOBuyingCratesCooldown(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(262145 + 15361, 0);
            }
            else
            {
                Hacks.WriteGA<int>(262145 + 15361, 300000);
            }
        }

        /// <summary>
        /// CEO移除出售板条箱冷却
        /// </summary>
        public static void CEOSellingCratesCooldown(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(262145 + 15362, 0);
            }
            else
            {
                Hacks.WriteGA<int>(262145 + 15362, 1800000);
            }
        }

        /// <summary>
        /// CEO每箱出售单价设置为2W
        /// </summary>
        public static void CEOPricePerCrateAtXCrates(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(262145 + 15596, 20000);      // 1
                Hacks.WriteGA<int>(262145 + 15597, 20000);      // 2
                Hacks.WriteGA<int>(262145 + 15598, 20000);      // 3                                                       
                Hacks.WriteGA<int>(262145 + 15599, 20000);      // 4-5
                Hacks.WriteGA<int>(262145 + 15600, 20000);      // 6-7
                Hacks.WriteGA<int>(262145 + 15601, 20000);      // 8-9
                Hacks.WriteGA<int>(262145 + 15602, 20000);      // 10-14
                Hacks.WriteGA<int>(262145 + 15603, 20000);      // 15-19
                Hacks.WriteGA<int>(262145 + 15604, 20000);      // 20-24
                Hacks.WriteGA<int>(262145 + 15605, 20000);      // 25-29
                Hacks.WriteGA<int>(262145 + 15606, 20000);      // 30-34
                Hacks.WriteGA<int>(262145 + 15607, 20000);      // 35-39
                Hacks.WriteGA<int>(262145 + 15608, 20000);      // 40-44
                Hacks.WriteGA<int>(262145 + 15609, 20000);      // 45-49
                Hacks.WriteGA<int>(262145 + 15610, 20000);      // 50-59
                Hacks.WriteGA<int>(262145 + 15611, 20000);      // 60-69
                Hacks.WriteGA<int>(262145 + 15612, 20000);      // 70-79
                Hacks.WriteGA<int>(262145 + 15613, 20000);      // 80-89
                Hacks.WriteGA<int>(262145 + 15614, 20000);      // 90-990
                Hacks.WriteGA<int>(262145 + 15615, 20000);      // 100-11
                Hacks.WriteGA<int>(262145 + 15616, 20000);      // 111
            }
            else
            {
                Hacks.WriteGA<int>(262145 + 15596, 10000);      // 1
                Hacks.WriteGA<int>(262145 + 15597, 11000);      // 2
                Hacks.WriteGA<int>(262145 + 15598, 12000);      // 3                                                       
                Hacks.WriteGA<int>(262145 + 15599, 13000);      // 4-5
                Hacks.WriteGA<int>(262145 + 15600, 13500);      // 6-7
                Hacks.WriteGA<int>(262145 + 15601, 14000);      // 8-9
                Hacks.WriteGA<int>(262145 + 15602, 14500);      // 10-14
                Hacks.WriteGA<int>(262145 + 15603, 15000);      // 15-19
                Hacks.WriteGA<int>(262145 + 15604, 15500);      // 20-24
                Hacks.WriteGA<int>(262145 + 15605, 16000);      // 25-29
                Hacks.WriteGA<int>(262145 + 15606, 16500);      // 30-34
                Hacks.WriteGA<int>(262145 + 15607, 17000);      // 35-39
                Hacks.WriteGA<int>(262145 + 15608, 17500);      // 40-44
                Hacks.WriteGA<int>(262145 + 15609, 17750);      // 45-49
                Hacks.WriteGA<int>(262145 + 15610, 18000);      // 50-59
                Hacks.WriteGA<int>(262145 + 15611, 18250);      // 60-69
                Hacks.WriteGA<int>(262145 + 15612, 18500);      // 70-79
                Hacks.WriteGA<int>(262145 + 15613, 18750);      // 80-89
                Hacks.WriteGA<int>(262145 + 15614, 19000);      // 90-990
                Hacks.WriteGA<int>(262145 + 15615, 19500);      // 100-11
                Hacks.WriteGA<int>(262145 + 15616, 20000);      // 111
            }
        }

        /// <summary>
        /// 移除地堡进货延迟
        /// </summary>
        public static void RemoveBunkerSupplyDelay(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(262145 + 21349, 0);
            }
            else
            {
                Hacks.WriteGA<int>(262145 + 21349, 600);
            }
        }

        /// <summary>
        /// 设置地堡生产和研究时间为指定时间，单位秒
        /// </summary>
        public static void SetBunkerProduceResearchTime(bool isEnable)
        {
            if (isEnable)
            {
                // Base Time to Produce
                Hacks.WriteGA<int>(262145 + 21324, Settings.ProduceTime);  // Product                  215868155 
                Hacks.WriteGA<int>(262145 + 21340, Settings.ProduceTime);  // Research                 -676414773

                // Time to Produce Reductions
                Hacks.WriteGA<int>(262145 + 21325, Settings.ProduceTime);  // Production Equipment     631477612
                Hacks.WriteGA<int>(262145 + 21326, Settings.ProduceTime);  // Production Staff         818645907
                Hacks.WriteGA<int>(262145 + 21341, Settings.ProduceTime);  // Research Equipment       -1148432846
                Hacks.WriteGA<int>(262145 + 21342, Settings.ProduceTime);  // Research Staff           510883248
            }
            else
            {
                // Base Time to Produce
                Hacks.WriteGA<int>(262145 + 21324, 600000);  // Product
                Hacks.WriteGA<int>(262145 + 21340, 300000);  // Research

                // Time to Produce Reductions
                Hacks.WriteGA<int>(262145 + 21325, 90000);  // Production Equipment
                Hacks.WriteGA<int>(262145 + 21326, 90000);  // Production Staff
                Hacks.WriteGA<int>(262145 + 21341, 45000);  // Research Equipment
                Hacks.WriteGA<int>(262145 + 21342, 45000);  // Research Staff
            }
        }

        /// <summary>
        /// 设置地堡进货单价为200元
        /// </summary>
        public static void SetBunkerResupplyCosts(bool isEnable)
        {
            if (isEnable)
            {
                // Resupply Costs
                Hacks.WriteGA<int>(262145 + 21347, 200);  // Discounted             970448219
                Hacks.WriteGA<int>(262145 + 21348, 200);  // Base                   262971166
            }
            else
            {
                // Resupply Costs
                Hacks.WriteGA<int>(262145 + 21347, 15000);  // Discounted
                Hacks.WriteGA<int>(262145 + 21348, 15000);  // Base
            }
        }

        /// <summary>
        /// 设置地堡远近出货倍数
        /// </summary>
        public static void SetBunkerSaleMultipliers(bool isEnable)
        {
            if (isEnable)
            {
                // Sale Multipliers
                Hacks.WriteGA<float>(262145 + 21303, 2.0f);     // Near         1865029244
                Hacks.WriteGA<float>(262145 + 21304, 3.0f);     // Far          1021567941
            }
            else
            {
                // Sale Multipliers
                Hacks.WriteGA<float>(262145 + 21303, 1.0f);     // Near
                Hacks.WriteGA<float>(262145 + 21304, 1.5f);     // Far
            }
        }

        /// <summary>
        /// 设置摩托帮远近出货倍数
        /// </summary>
        public static void SetMCSaleMultipliers(bool isEnable)
        {
            if (isEnable)
            {
                // Sale Multipliers
                Hacks.WriteGA<float>(262145 + 18861, 2.0f);     // Near         -823848572
                Hacks.WriteGA<float>(262145 + 18862, 3.0f);     // Far          1763638426
            }
            else
            {
                // Sale Multipliers
                Hacks.WriteGA<float>(262145 + 18861, 1.0f);     // Near
                Hacks.WriteGA<float>(262145 + 18862, 1.5f);     // Far
            }
        }

        /// <summary>
        /// 设置地堡原材料消耗量
        /// </summary>
        public static void SetBunkerSuppliesPerUnitProduced(bool isEnable)
        {
            if (isEnable)
            {
                // Supplies Per Unit Produced
                Hacks.WriteGA<int>(262145 + 21327, 1);     // Product Base              -1652502760
                Hacks.WriteGA<int>(262145 + 21328, 1);     // Product Upgraded          1647327744
                Hacks.WriteGA<int>(262145 + 21343, 1);     // Research Base             1485279815
                Hacks.WriteGA<int>(262145 + 21344, 1);     // Research Upgraded         2041812011
            }
            else
            {
                // Supplies Per Unit Produced
                Hacks.WriteGA<int>(262145 + 21327, 10);     // Product Base
                Hacks.WriteGA<int>(262145 + 21328, 5);     // Product Upgraded
                Hacks.WriteGA<int>(262145 + 21343, 2);     // Research Base
                Hacks.WriteGA<int>(262145 + 21344, 1);     // Research Upgraded
            }
        }

        /// <summary>
        /// 设置摩托帮原材料消耗量
        /// </summary>
        public static void SetMCSuppliesPerUnitProduced(bool isEnable)
        {
            if (isEnable)
            {
                // Supplies Per Unit Produced
                Hacks.WriteGA<int>(262145 + 17213, 1);     // Documents Base            -1839004359
                Hacks.WriteGA<int>(262145 + 17214, 1);     // Cash Base
                Hacks.WriteGA<int>(262145 + 17215, 1);     // Cocaine Base
                Hacks.WriteGA<int>(262145 + 17216, 1);     // Meth Base
                Hacks.WriteGA<int>(262145 + 17217, 1);     // Weed Base
                Hacks.WriteGA<int>(262145 + 17218, 1);     // Documents Upgraded
                Hacks.WriteGA<int>(262145 + 17219, 1);     // Cash Upgraded
                Hacks.WriteGA<int>(262145 + 17220, 1);     // Cocaine Upgraded
                Hacks.WriteGA<int>(262145 + 17221, 1);     // Meth Upgraded
                Hacks.WriteGA<int>(262145 + 17222, 1);     // Weed Upgraded
            }
            else
            {
                // Supplies Per Unit Produced
                Hacks.WriteGA<int>(262145 + 17213, 4);      // Documents Base
                Hacks.WriteGA<int>(262145 + 17214, 10);     // Cash Base
                Hacks.WriteGA<int>(262145 + 17215, 50);     // Cocaine Base
                Hacks.WriteGA<int>(262145 + 17216, 24);     // Meth Base
                Hacks.WriteGA<int>(262145 + 17217, 4);      // Weed Base
                Hacks.WriteGA<int>(262145 + 17218, 2);      // Documents Upgraded
                Hacks.WriteGA<int>(262145 + 17219, 5);      // Cash Upgraded
                Hacks.WriteGA<int>(262145 + 17220, 25);     // Cocaine Upgraded
                Hacks.WriteGA<int>(262145 + 17221, 12);     // Meth Upgraded
                Hacks.WriteGA<int>(262145 + 17222, 2);      // Weed Upgraded
            }
        }

        /// <summary>
        /// 解锁地堡所有研究 (临时)
        /// </summary>
        public static void UnlockBunkerResearch(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(262145 + 21477, 1);       // 886070202
            }
            else
            {
                Hacks.WriteGA<int>(262145 + 21477, 0);
            }
        }

        /// <summary>
        /// 设置夜总会生产时间为指定时间，单位秒
        /// </summary>
        public static void SetNightclubProduceTime(bool isEnable)
        {
            if (isEnable)
            {
                // Time to Produce
                Hacks.WriteGA<int>(262145 + 24135, Settings.ProduceTime);  // Sporting Goods               -147565853
                Hacks.WriteGA<int>(262145 + 24136, Settings.ProduceTime);  // South American Imports
                Hacks.WriteGA<int>(262145 + 24137, Settings.ProduceTime);  // Pharmaceutical Research
                Hacks.WriteGA<int>(262145 + 24138, Settings.ProduceTime);  // Organic Produce
                Hacks.WriteGA<int>(262145 + 24139, Settings.ProduceTime);  // Printing and Copying
                Hacks.WriteGA<int>(262145 + 24140, Settings.ProduceTime);  // Cash Creation
                Hacks.WriteGA<int>(262145 + 24141, Settings.ProduceTime);  // Cargo and Shipments
            }
            else
            {
                // Time to Produce
                Hacks.WriteGA<int>(262145 + 24135, 4800000);    // Sporting Goods
                Hacks.WriteGA<int>(262145 + 24136, 14400000);   // South American Imports
                Hacks.WriteGA<int>(262145 + 24137, 7200000);    // Pharmaceutical Research
                Hacks.WriteGA<int>(262145 + 24138, 2400000);    // Organic Produce
                Hacks.WriteGA<int>(262145 + 24139, 1800000);    // Printing and Copying
                Hacks.WriteGA<int>(262145 + 24140, 3600000);    // Cash Creation
                Hacks.WriteGA<int>(262145 + 24141, 8400000);    // Cargo and Shipments
            }
        }

        /// <summary>
        /// 设置摩托帮生产时间为指定时间，单位秒
        /// </summary>
        public static void SetMCProduceTime(bool isEnable)
        {
            if (isEnable)
            {
                // Base Time to Produce
                Hacks.WriteGA<int>(262145 + 17198, Settings.ProduceTime);  // Weed         -635596193
                Hacks.WriteGA<int>(262145 + 17199, Settings.ProduceTime);  // Meth
                Hacks.WriteGA<int>(262145 + 17200, Settings.ProduceTime);  // Cocaine
                Hacks.WriteGA<int>(262145 + 17201, Settings.ProduceTime);  // Documents
                Hacks.WriteGA<int>(262145 + 17202, Settings.ProduceTime);  // Cash

                // Time to Produce Reductions
                Hacks.WriteGA<int>(262145 + 17203, 1);  // Documents Equipment
                Hacks.WriteGA<int>(262145 + 17204, 1);  // Cash Equipment
                Hacks.WriteGA<int>(262145 + 17205, 1);  // Cocaine Equipment
                Hacks.WriteGA<int>(262145 + 17206, 1);  // Meth Equipment
                Hacks.WriteGA<int>(262145 + 17207, 1);  // Weed Equipment
                Hacks.WriteGA<int>(262145 + 17208, 1);  // Documents Staff
                Hacks.WriteGA<int>(262145 + 17209, 1);  // Cash Staff
                Hacks.WriteGA<int>(262145 + 17210, 1);  // Cocaine Staff
                Hacks.WriteGA<int>(262145 + 17211, 1);  // Meth Staff
                Hacks.WriteGA<int>(262145 + 17212, 1);  // Weed Staff
            }
            else
            {
                // Base Time to Produce
                Hacks.WriteGA<int>(262145 + 17198, 360000);     // Weed
                Hacks.WriteGA<int>(262145 + 17199, 1800000);    // Meth
                Hacks.WriteGA<int>(262145 + 17200, 3000000);    // Cocaine
                Hacks.WriteGA<int>(262145 + 17201, 300000);     // Documents
                Hacks.WriteGA<int>(262145 + 17202, 720000);     // Cash

                // Time to Produce Reductions
                Hacks.WriteGA<int>(262145 + 17203, 60000);      // Documents Equipment
                Hacks.WriteGA<int>(262145 + 17204, 120000);     // Cash Equipment
                Hacks.WriteGA<int>(262145 + 17205, 600000);     // Cocaine Equipment
                Hacks.WriteGA<int>(262145 + 17206, 360000);     // Meth Equipment
                Hacks.WriteGA<int>(262145 + 17207, 60000);      // Weed Equipment
                Hacks.WriteGA<int>(262145 + 17208, 60000);      // Documents Staff
                Hacks.WriteGA<int>(262145 + 17209, 120000);     // Cash Staff
                Hacks.WriteGA<int>(262145 + 17210, 600000);     // Cocaine Staff
                Hacks.WriteGA<int>(262145 + 17211, 360000);     // Meth Staff
                Hacks.WriteGA<int>(262145 + 17212, 60000);      // Weed Staff
            }
        }

        /// <summary>
        /// 移除摩托帮进货延迟
        /// </summary>
        public static void RemoveMCSupplyDelay(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(262145 + 18749, 0);          // 728170457
            }
            else
            {
                Hacks.WriteGA<int>(262145 + 18749, 600);
            }
        }

        /// <summary>
        /// 设置摩托帮进货单价为200元
        /// </summary>
        public static void SetMCResupplyCosts(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(262145 + 18748, 200);  // Discounted Resupply Cost       BIKER_PURCHASE_SUPPLIES_COST_PER_SEGMENT
            }
            else
            {
                Hacks.WriteGA<int>(262145 + 18748, 15000);  // Discounted Resupply Cost
            }
        }

        /// <summary>
        /// 梅利威瑟服务
        /// </summary>
        public static void MerryweatherServices(int serverId)
        {
            Hacks.WriteGA<int>(2810701 + serverId, 1);
        }

        /// <summary>
        /// 移除进出口大亨出货CD
        /// </summary>
        public static void RemoveExportVehicleDelay(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(262145 + 19477, 0);          // 1001423248
                Hacks.WriteGA<int>(262145 + 19478, 0);
                Hacks.WriteGA<int>(262145 + 19479, 0);
                Hacks.WriteGA<int>(262145 + 19480, 0);
            }
            else
            {
                Hacks.WriteGA<int>(262145 + 19477, 1200000);
                Hacks.WriteGA<int>(262145 + 19478, 1680000);
                Hacks.WriteGA<int>(262145 + 19479, 2340000);
                Hacks.WriteGA<int>(262145 + 19480, 2880000);
            }
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
        public static void RemoveSmugglerRunInDelay(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(262145 + 22522, 0);          // 1278611667
                Hacks.WriteGA<int>(262145 + 22523, 0);
                Hacks.WriteGA<int>(262145 + 22524, 0);
                Hacks.WriteGA<int>(262145 + 22525, 0);
            }
            else
            {
                Hacks.WriteGA<int>(262145 + 22522, 120000);
                Hacks.WriteGA<int>(262145 + 22523, 180000);
                Hacks.WriteGA<int>(262145 + 22524, 240000);
                Hacks.WriteGA<int>(262145 + 22525, 60000);
            }
        }


        /// <summary>
        /// 移除机库出货CD
        /// </summary>
        public static void RemoveSmugglerRunOutDelay(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(262145 + 22561, 0);          // -1525481945
            }
            else
            {
                Hacks.WriteGA<int>(262145 + 22561, 180000);
            }
        }

        /// <summary>
        /// 移除夜总会出货CD
        /// </summary>
        public static void RemoveNightclubOutDelay(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(262145 + 24216, 0);      // 1763921019
                Hacks.WriteGA<int>(262145 + 24251, 0);      // -1004589438
                Hacks.WriteGA<int>(262145 + 24252, 0);
            }
            else
            {
                Hacks.WriteGA<int>(262145 + 24216, 300000);
                Hacks.WriteGA<int>(262145 + 24251, 300000);
                Hacks.WriteGA<int>(262145 + 24252, 300000);
            }
        }

        /// <summary>
        /// 夜总会托尼洗钱费用
        /// </summary>
        public static void NightclubNoTonyLaunderingMoney(bool isEnable)
        {
            // 托尼分红 GA(262145+24162) 默认10000
            if (isEnable)
            {
                Hacks.WriteGA<float>(262145 + 24258, 0.000001f);        // -1002770353
            }
            else
            {
                Hacks.WriteGA<float>(262145 + 24258, 0.1f);
            }
        }
    }
}
