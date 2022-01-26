using System.Threading;
using GTA5OnlineTools.Features.Core;

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
            Memory.SetForegroundWindow();

            if (sessionID == -1)
            {
                Hacks.WriteGA<int>(1574587 + 2, -1);
                Hacks.WriteGA<int>(1574587, 1);
                Thread.Sleep(200);
                Hacks.WriteGA<int>(1574587, 0);
            }
            else
            {
                Hacks.WriteGA<int>(1575004, sessionID);
                Hacks.WriteGA<int>(1574587, 1);
                Thread.Sleep(200);
                Hacks.WriteGA<int>(1574587, 0);
            }
        }

        /// <summary>
        /// 允许非公共战局运货
        /// </summary>
        public static void AllowSellOnNonPublic(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(2714627 + 744, 0);
            }
            else
            {
                Hacks.WriteGA<int>(2714627 + 744, 1);
            }
        }

        /// <summary>
        /// 移除自杀CD
        /// </summary>
        public static void RemoveSuicideCooldown(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(2810287 + 6734, 0);
                Hacks.WriteGA<int>(262145 + 28056, 3);
                Hacks.WriteGA<int>(262145 + 28057, 3);
            }
            else
            {
                Hacks.WriteGA<int>(262145 + 28056, 300000);
                Hacks.WriteGA<int>(262145 + 28057, 60000);
            }
        }

        /// <summary>
        /// 移除被动模式CD
        /// </summary>
        public static void RemovePassiveModeCooldown(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(2810287 + 4460, 0);
                Hacks.WriteGA<int>(1966521, 0);
            }
            else
            {
                Hacks.WriteGA<int>(2810287 + 4460, 1);
                Hacks.WriteGA<int>(1966521, 1);
            }
        }

        /// <summary>
        /// 禁用轨道炮CD
        /// </summary>
        public static void DisableOrbitalCooldown(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(262145 + 22852, 0);
            }
            else
            {
                Hacks.WriteGA<int>(262145 + 22852, 2880000);
            }
        }

        /// <summary>
        /// 坐进线上个人载具
        /// </summary>
        public static void GetInOnlinePV()
        {
            Hacks.WriteGA<int>(2671444 + 8, 1);
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
                Hacks.WriteGA<int>(Offsets.oPlayerIDHelp + 1 + Hacks.PLAYER_ID() * 453 + 209, 1);
                Hacks.WriteGA<int>(Offsets.oNETTimeHelp + 70, Hacks.GET_NETWORK_TIME() + 3600000);
                Hacks.WriteGA<int>(Offsets.oVMYCar + 4629, 3);
            }
            else
            {
                Hacks.WriteGA<int>(Offsets.oPlayerIDHelp + 1 + Hacks.PLAYER_ID() * 453 + 209, 0);
                Hacks.WriteGA<int>(Offsets.oVMYCar + 4629, 0);
            }
        }

        /// <summary>
        /// 幽灵组织
        /// </summary>
        public static void GhostOrganization(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(Offsets.oPlayerIDHelp + 1 + Hacks.PLAYER_ID() * 453 + 209, 1);
                Hacks.WriteGA<int>(Offsets.oNETTimeHelp + 70, Hacks.GET_NETWORK_TIME() + 3600000);
                Hacks.WriteGA<int>(Offsets.oVMYCar + 4629, 4);
            }
            else
            {
                Hacks.WriteGA<int>(Offsets.oPlayerIDHelp + 1 + Hacks.PLAYER_ID() * 453 + 209, 0);
                Hacks.WriteGA<int>(Offsets.oVMYCar + 4629, 0);
            }
        }

        /// <summary>
        /// 警察无视犯罪
        /// </summary>
        public static void BribeOrBlindCops(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(Offsets.oVMYCar + 4624, 1);
                Hacks.WriteGA<int>(Offsets.oVMYCar + 4626, Hacks.GET_NETWORK_TIME() + 3600000);
                Hacks.WriteGA<int>(Offsets.oVMYCar + 4623, 5);
            }
            else
            {
                Hacks.WriteGA<int>(Offsets.oVMYCar + 4624, 0);
                Hacks.WriteGA<int>(Offsets.oVMYCar + 4626, 0);
                Hacks.WriteGA<int>(Offsets.oVMYCar + 4623, 0);
            }
        }

        /// <summary>
        /// 贿赂当局
        /// </summary>
        public static void BribeAuthorities(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(Offsets.oVMYCar + 4624, 1);
                Hacks.WriteGA<int>(Offsets.oVMYCar + 4626, Hacks.GET_NETWORK_TIME() + 3600000);
                Hacks.WriteGA<int>(Offsets.oVMYCar + 4623, 21);
            }
            else
            {
                Hacks.WriteGA<int>(Offsets.oVMYCar + 4624, 0);
                Hacks.WriteGA<int>(Offsets.oVMYCar + 4626, 0);
                Hacks.WriteGA<int>(Offsets.oVMYCar + 4623, 0);
            }
        }

        /// <summary>
        /// 显示玩家
        /// </summary>
        public static void RevealPlayers(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(Offsets.oPlayerIDHelp + 1 + Hacks.PLAYER_ID() * 499 + 212, 1);
                Hacks.WriteGA<int>(Offsets.oNETTimeHelp + 71, Hacks.GET_NETWORK_TIME() + 3600000);
            }
            else
            {
                Hacks.WriteGA<int>(Offsets.oPlayerIDHelp + 1 + Hacks.PLAYER_ID() * 499 + 212, 0);
                Hacks.WriteGA<int>(Offsets.oNETTimeHelp + 71, 0);
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
            Hacks.WriteGA<float>(262145 + 31283, multiplier);
            // Test Track
            Hacks.WriteGA<float>(262145 + 31284, multiplier);
            // Head_2_Head 头对头
            Hacks.WriteGA<float>(262145 + 31281, multiplier);
            // Scramble 攀登
            Hacks.WriteGA<float>(262145 + 31280, multiplier);
            // Pursuit Race 追逐赛
            Hacks.WriteGA<float>(262145 + 31279, multiplier);
            // Street Race 街头比赛
            Hacks.WriteGA<float>(262145 + 31278, multiplier);
        }

        /// <summary>
        /// 使用牛鲨睾酮
        /// </summary>
        public static void InstantBullShark(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(2703656 + 3590, 1);
            }
            else
            {
                Hacks.WriteGA<int>(2703656 + 3590, 5);
            }
        }

        /// <summary>
        /// 呼叫支援直升机
        /// </summary>
        public static void BackupHeli(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(2810287 + 4454, 1);
            }
            else
            {
                Hacks.WriteGA<int>(2810287 + 4454, 0);
            }
        }

        /// <summary>
        /// 呼叫空袭
        /// </summary>
        public static void Airstrike(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(2810287 + 4455, 1);
            }
            else
            {
                Hacks.WriteGA<int>(2810287 + 4455, 0);
            }
        }

        /// <summary>
        /// CEO特殊货物
        /// </summary>
        public static void CEOSpecialCargo(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(1946791, 1);
            }
            else
            {
                Hacks.WriteGA<int>(1946791, 0);
            }
        }

        /// <summary>
        /// CEO特殊货物
        /// </summary>
        public static void CEOCargoType(int cargoID)
        {
            Hacks.WriteGA<int>(1946637, cargoID);
        }

        /// <summary>
        /// CEO移除购买板条箱冷却
        /// </summary>
        public static void CEOBuyingCratesCooldown(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(262145 + 15360, 0);
            }
            else
            {
                Hacks.WriteGA<int>(262145 + 15360, 300000);
            }
        }

        /// <summary>
        /// CEO移除出售板条箱冷却
        /// </summary>
        public static void CEOSellingCratesCooldown(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(262145 + 15361, 0);
            }
            else
            {
                Hacks.WriteGA<int>(262145 + 15361, 1800000);
            }
        }

        /// <summary>
        /// CEO每箱出售单价设置为2W
        /// </summary>
        public static void CEOPricePerCrateAtXCrates(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(262145 + 15595, 20000);      // 1
                Hacks.WriteGA<int>(262145 + 15596, 20000);      // 2
                Hacks.WriteGA<int>(262145 + 15597, 20000);      // 3                                                       
                Hacks.WriteGA<int>(262145 + 15598, 20000);      // 4-5
                Hacks.WriteGA<int>(262145 + 15599, 20000);      // 6-7
                Hacks.WriteGA<int>(262145 + 15600, 20000);      // 8-9
                Hacks.WriteGA<int>(262145 + 15601, 20000);      // 10-14
                Hacks.WriteGA<int>(262145 + 15602, 20000);      // 15-19
                Hacks.WriteGA<int>(262145 + 15603, 20000);      // 20-24
                Hacks.WriteGA<int>(262145 + 15604, 20000);      // 25-29
                Hacks.WriteGA<int>(262145 + 15605, 20000);      // 30-34
                Hacks.WriteGA<int>(262145 + 15606, 20000);      // 35-39
                Hacks.WriteGA<int>(262145 + 15607, 20000);      // 40-44
                Hacks.WriteGA<int>(262145 + 15608, 20000);      // 45-49
                Hacks.WriteGA<int>(262145 + 15609, 20000);      // 50-59
                Hacks.WriteGA<int>(262145 + 15610, 20000);      // 60-69
                Hacks.WriteGA<int>(262145 + 15611, 20000);      // 70-79
                Hacks.WriteGA<int>(262145 + 15612, 20000);      // 80-89
                Hacks.WriteGA<int>(262145 + 15613, 20000);      // 90-990
                Hacks.WriteGA<int>(262145 + 15614, 20000);      // 100-11
                Hacks.WriteGA<int>(262145 + 15615, 20000);      // 111
            }
            else
            {
                Hacks.WriteGA<int>(262145 + 15595, 10000);      // 1
                Hacks.WriteGA<int>(262145 + 15596, 11000);      // 2
                Hacks.WriteGA<int>(262145 + 15597, 12000);      // 3                                                       
                Hacks.WriteGA<int>(262145 + 15598, 13000);      // 4-5
                Hacks.WriteGA<int>(262145 + 15599, 13500);      // 6-7
                Hacks.WriteGA<int>(262145 + 15600, 14000);      // 8-9
                Hacks.WriteGA<int>(262145 + 15601, 14500);      // 10-14
                Hacks.WriteGA<int>(262145 + 15602, 15000);      // 15-19
                Hacks.WriteGA<int>(262145 + 15603, 15500);      // 20-24
                Hacks.WriteGA<int>(262145 + 15604, 16000);      // 25-29
                Hacks.WriteGA<int>(262145 + 15605, 16500);      // 30-34
                Hacks.WriteGA<int>(262145 + 15606, 17000);      // 35-39
                Hacks.WriteGA<int>(262145 + 15607, 17500);      // 40-44
                Hacks.WriteGA<int>(262145 + 15608, 17750);      // 45-49
                Hacks.WriteGA<int>(262145 + 15609, 18000);      // 50-59
                Hacks.WriteGA<int>(262145 + 15610, 18250);      // 60-69
                Hacks.WriteGA<int>(262145 + 15611, 18500);      // 70-79
                Hacks.WriteGA<int>(262145 + 15612, 18750);      // 80-89
                Hacks.WriteGA<int>(262145 + 15613, 19000);      // 90-990
                Hacks.WriteGA<int>(262145 + 15614, 19500);      // 100-11
                Hacks.WriteGA<int>(262145 + 15615, 20000);      // 111
            }
        }

        /// <summary>
        /// 移除地堡进货延迟
        /// </summary>
        public static void RemoveBunkerSupplyDelay(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(262145 + 21348, 0);
            }
            else
            {
                Hacks.WriteGA<int>(262145 + 21348, 600);
            }
        }

        /// <summary>
        /// 设置地堡生产和研究时间为1秒
        /// </summary>
        public static void SetBunkerProduceResearchTime(bool isEnable)
        {
            if (isEnable)
            {
                // Base Time to Produce
                Hacks.WriteGA<int>(262145 + 21323, 1);  // Product                  215868155 
                Hacks.WriteGA<int>(262145 + 21339, 1);  // Research                 -676414773

                // Time to Produce Reductions
                Hacks.WriteGA<int>(262145 + 21324, 1);  // Production Equipment     631477612
                Hacks.WriteGA<int>(262145 + 21325, 1);  // Production Staff         818645907
                Hacks.WriteGA<int>(262145 + 21340, 1);  // Research Equipment       -1148432846
                Hacks.WriteGA<int>(262145 + 21341, 1);  // Research Staff           510883248
            }
            else
            {
                // Base Time to Produce
                Hacks.WriteGA<int>(262145 + 21323, 600000);  // Product
                Hacks.WriteGA<int>(262145 + 21339, 300000);  // Research

                // Time to Produce Reductions
                Hacks.WriteGA<int>(262145 + 21324, 90000);  // Production Equipment
                Hacks.WriteGA<int>(262145 + 21325, 90000);  // Production Staff
                Hacks.WriteGA<int>(262145 + 21340, 45000);  // Research Equipment
                Hacks.WriteGA<int>(262145 + 21341, 45000);  // Research Staff
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
                Hacks.WriteGA<int>(262145 + 21346, 200);  // Discounted             970448219
                Hacks.WriteGA<int>(262145 + 21347, 200);  // Base                   262971166
            }
            else
            {
                // Resupply Costs
                Hacks.WriteGA<int>(262145 + 21346, 15000);  // Discounted
                Hacks.WriteGA<int>(262145 + 21347, 15000);  // Base
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
                Hacks.WriteGA<float>(262145 + 21302, 2.0f);     // Near         1865029244
                Hacks.WriteGA<float>(262145 + 21303, 3.0f);     // Far          1021567941
            }
            else
            {
                // Sale Multipliers
                Hacks.WriteGA<float>(262145 + 21302, 1.0f);     // Near
                Hacks.WriteGA<float>(262145 + 21303, 1.5f);     // Far
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
                Hacks.WriteGA<float>(262145 + 18860, 2.0f);     // Near         -823848572
                Hacks.WriteGA<float>(262145 + 18861, 3.0f);     // Far          1763638426
            }
            else
            {
                // Sale Multipliers
                Hacks.WriteGA<float>(262145 + 18860, 1.0f);     // Near
                Hacks.WriteGA<float>(262145 + 18861, 1.5f);     // Far
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
                Hacks.WriteGA<int>(262145 + 21326, 1);     // Product Base              -1652502760
                Hacks.WriteGA<int>(262145 + 21327, 1);     // Product Upgraded          1647327744
                Hacks.WriteGA<int>(262145 + 21342, 1);     // Research Base             1485279815
                Hacks.WriteGA<int>(262145 + 21343, 1);     // Research Upgraded         2041812011
            }
            else
            {
                // Supplies Per Unit Produced
                Hacks.WriteGA<int>(262145 + 21326, 10);     // Product Base
                Hacks.WriteGA<int>(262145 + 21327, 5);     // Product Upgraded
                Hacks.WriteGA<int>(262145 + 21342, 2);     // Research Base
                Hacks.WriteGA<int>(262145 + 21343, 1);     // Research Upgraded
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
                Hacks.WriteGA<int>(262145 + 17212, 1);     // Documents Base            -1839004359
                Hacks.WriteGA<int>(262145 + 17213, 1);     // Cash Base
                Hacks.WriteGA<int>(262145 + 17214, 1);     // Cocaine Base
                Hacks.WriteGA<int>(262145 + 17215, 1);     // Meth Base
                Hacks.WriteGA<int>(262145 + 17216, 1);     // Weed Base
                Hacks.WriteGA<int>(262145 + 17217, 1);     // Documents Upgraded
                Hacks.WriteGA<int>(262145 + 17218, 1);     // Cash Upgraded
                Hacks.WriteGA<int>(262145 + 17219, 1);     // Cocaine Upgraded
                Hacks.WriteGA<int>(262145 + 17220, 1);     // Meth Upgraded
                Hacks.WriteGA<int>(262145 + 17221, 1);     // Weed Upgraded
            }
            else
            {
                // Supplies Per Unit Produced
                Hacks.WriteGA<int>(262145 + 17212, 4);      // Documents Base
                Hacks.WriteGA<int>(262145 + 17213, 10);     // Cash Base
                Hacks.WriteGA<int>(262145 + 17214, 50);     // Cocaine Base
                Hacks.WriteGA<int>(262145 + 17215, 24);     // Meth Base
                Hacks.WriteGA<int>(262145 + 17216, 4);      // Weed Base
                Hacks.WriteGA<int>(262145 + 17217, 2);      // Documents Upgraded
                Hacks.WriteGA<int>(262145 + 17218, 5);      // Cash Upgraded
                Hacks.WriteGA<int>(262145 + 17219, 25);     // Cocaine Upgraded
                Hacks.WriteGA<int>(262145 + 17220, 12);     // Meth Upgraded
                Hacks.WriteGA<int>(262145 + 17221, 2);      // Weed Upgraded
            }
        }

        /// <summary>
        /// 解锁地堡所有研究 (临时)
        /// </summary>
        public static void UnlockBunkerResearch(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(262145 + 21476, 1);       // 886070202
            }
            else
            {
                Hacks.WriteGA<int>(262145 + 21476, 0);
            }
        }

        /// <summary>
        /// 设置夜总会生产时间为1秒
        /// </summary>
        public static void SetNightclubProduceTime(bool isEnable)
        {
            if (isEnable)
            {
                // Time to Produce
                Hacks.WriteGA<int>(262145 + 24134, 1);  // Sporting Goods               -147565853
                Hacks.WriteGA<int>(262145 + 24135, 1);  // South American Imports
                Hacks.WriteGA<int>(262145 + 24136, 1);  // Pharmaceutical Research
                Hacks.WriteGA<int>(262145 + 24137, 1);  // Organic Produce
                Hacks.WriteGA<int>(262145 + 24138, 1);  // Printing and Copying
                Hacks.WriteGA<int>(262145 + 24139, 1);  // Cash Creation
                Hacks.WriteGA<int>(262145 + 24140, 1);  // Cargo and Shipments
            }
            else
            {
                // Time to Produce
                Hacks.WriteGA<int>(262145 + 24134, 4800000);    // Sporting Goods
                Hacks.WriteGA<int>(262145 + 24135, 14400000);   // South American Imports
                Hacks.WriteGA<int>(262145 + 24136, 7200000);    // Pharmaceutical Research
                Hacks.WriteGA<int>(262145 + 24137, 2400000);    // Organic Produce
                Hacks.WriteGA<int>(262145 + 24138, 1800000);    // Printing and Copying
                Hacks.WriteGA<int>(262145 + 24139, 3600000);    // Cash Creation
                Hacks.WriteGA<int>(262145 + 24140, 8400000);    // Cargo and Shipments
            }
        }

        /// <summary>
        /// 设置摩托帮生产时间为1秒
        /// </summary>
        public static void SetMCProduceTime(bool isEnable)
        {
            if (isEnable)
            {
                // Base Time to Produce
                Hacks.WriteGA<int>(262145 + 17197, 1);  // Weed         -635596193
                Hacks.WriteGA<int>(262145 + 17198, 1);  // Meth
                Hacks.WriteGA<int>(262145 + 17199, 1);  // Cocaine
                Hacks.WriteGA<int>(262145 + 17200, 1);  // Documents
                Hacks.WriteGA<int>(262145 + 17201, 1);  // Cash

                // Time to Produce Reductions
                Hacks.WriteGA<int>(262145 + 17202, 1);  // Documents Equipment
                Hacks.WriteGA<int>(262145 + 17203, 1);  // Cash Equipment
                Hacks.WriteGA<int>(262145 + 17204, 1);  // Cocaine Equipment
                Hacks.WriteGA<int>(262145 + 17205, 1);  // Meth Equipment
                Hacks.WriteGA<int>(262145 + 17206, 1);  // Weed Equipment
                Hacks.WriteGA<int>(262145 + 17207, 1);  // Documents Staff
                Hacks.WriteGA<int>(262145 + 17208, 1);  // Cash Staff
                Hacks.WriteGA<int>(262145 + 17209, 1);  // Cocaine Staff
                Hacks.WriteGA<int>(262145 + 17210, 1);  // Meth Staff
                Hacks.WriteGA<int>(262145 + 17211, 1);  // Weed Staff
            }
            else
            {
                // Base Time to Produce
                Hacks.WriteGA<int>(262145 + 17197, 360000);     // Weed
                Hacks.WriteGA<int>(262145 + 17198, 1800000);    // Meth
                Hacks.WriteGA<int>(262145 + 17199, 3000000);    // Cocaine
                Hacks.WriteGA<int>(262145 + 17200, 300000);     // Documents
                Hacks.WriteGA<int>(262145 + 17201, 720000);     // Cash

                // Time to Produce Reductions
                Hacks.WriteGA<int>(262145 + 17202, 60000);      // Documents Equipment
                Hacks.WriteGA<int>(262145 + 17203, 120000);     // Cash Equipment
                Hacks.WriteGA<int>(262145 + 17204, 600000);     // Cocaine Equipment
                Hacks.WriteGA<int>(262145 + 17205, 360000);     // Meth Equipment
                Hacks.WriteGA<int>(262145 + 17206, 60000);      // Weed Equipment
                Hacks.WriteGA<int>(262145 + 17207, 60000);      // Documents Staff
                Hacks.WriteGA<int>(262145 + 17208, 120000);     // Cash Staff
                Hacks.WriteGA<int>(262145 + 17209, 600000);     // Cocaine Staff
                Hacks.WriteGA<int>(262145 + 17210, 360000);     // Meth Staff
                Hacks.WriteGA<int>(262145 + 17211, 60000);      // Weed Staff
            }
        }

        /// <summary>
        /// 移除摩托帮进货延迟
        /// </summary>
        public static void RemoveMCSupplyDelay(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(262145 + 18748, 0);          // 728170457
            }
            else
            {
                Hacks.WriteGA<int>(262145 + 18748, 600);
            }
        }

        /// <summary>
        /// 设置摩托帮进货单价为200元
        /// </summary>
        public static void SetMCResupplyCosts(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(262145 + 18747, 200);  // Discounted Resupply Cost       BIKER_PURCHASE_SUPPLIES_COST_PER_SEGMENT
            }
            else
            {
                Hacks.WriteGA<int>(262145 + 18747, 15000);  // Discounted Resupply Cost
            }
        }

        /// <summary>
        /// 梅利威瑟服务
        /// </summary>
        public static void MerryweatherServices(int serverId)
        {
            Hacks.WriteGA<int>(2810287 + serverId, 1);
        }

        /// <summary>
        /// 移除进出口大亨出货CD
        /// </summary>
        public static void RemoveExportVehicleDelay(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(262145 + 19476, 0);          // 1001423248
                Hacks.WriteGA<int>(262145 + 19477, 0);
                Hacks.WriteGA<int>(262145 + 19478, 0);
                Hacks.WriteGA<int>(262145 + 19479, 0);
            }
            else
            {
                Hacks.WriteGA<int>(262145 + 19476, 1200000);
                Hacks.WriteGA<int>(262145 + 19477, 1680000);
                Hacks.WriteGA<int>(262145 + 19478, 2340000);
                Hacks.WriteGA<int>(262145 + 19479, 2880000);
            }
        }

        /// <summary>
        /// 断开战局连接
        /// </summary>
        public static void Disconnect()
        {
            Hacks.WriteGA<int>(31782, 1);
            Thread.Sleep(20);
            Hacks.WriteGA<int>(31782, 0);
        }

        /// <summary>
        /// 移除机库进货CD
        /// </summary>
        public static void RemoveSmugglerRunInDelay(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(262145 + 22521, 0);          // 1278611667
                Hacks.WriteGA<int>(262145 + 22522, 0);
                Hacks.WriteGA<int>(262145 + 22523, 0);
                Hacks.WriteGA<int>(262145 + 22524, 0);
            }
            else
            {
                Hacks.WriteGA<int>(262145 + 22521, 120000);
                Hacks.WriteGA<int>(262145 + 22522, 180000);
                Hacks.WriteGA<int>(262145 + 22523, 240000);
                Hacks.WriteGA<int>(262145 + 22524, 60000);
            }
        }


        /// <summary>
        /// 移除机库出货CD
        /// </summary>
        public static void RemoveSmugglerRunOutDelay(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(262145 + 22560, 0);          // -1525481945
            }
            else
            {
                Hacks.WriteGA<int>(262145 + 22560, 180000);
            }
        }

        /// <summary>
        /// 移除夜总会出货CD
        /// </summary>
        public static void RemoveNightclubOutDelay(bool isEnable)
        {
            if (isEnable)
            {
                Hacks.WriteGA<int>(262145 + 24208, 0);      // 1763921019
                Hacks.WriteGA<int>(262145 + 24243, 0);      // -1004589438
                Hacks.WriteGA<int>(262145 + 24244, 0);
            }
            else
            {
                Hacks.WriteGA<int>(262145 + 24208, 300000);
                Hacks.WriteGA<int>(262145 + 24243, 300000);
                Hacks.WriteGA<int>(262145 + 24244, 300000);
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
                Hacks.WriteGA<float>(262145 + 24250, 0.000001f);        // -1002770353
            }
            else
            {
                Hacks.WriteGA<float>(262145 + 24250, 0.1f);
            }
        }
    }
}
