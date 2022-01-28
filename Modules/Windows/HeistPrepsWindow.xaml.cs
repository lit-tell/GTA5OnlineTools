using System;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.Generic;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Core;
using GTA5OnlineTools.Common.Utils;
using static GTA5OnlineTools.Features.Data.TeleportData;

namespace GTA5OnlineTools.Modules.Windows
{
    /// <summary>
    /// HeistPrepsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HeistPrepsWindow : Window
    {
        public HeistPrepsWindow()
        {
            InitializeComponent();
        }

        private void Window_HeistPreps_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                Memory.Initialize(CoreUtil.TargetAppName);

                Offsets.Mask.TempPTR = Memory.FindPattern(Offsets.Mask.WorldPTR);
                Globals.WorldPTR = Memory.Rip_37(Offsets.Mask.TempPTR);

                Offsets.Mask.TempPTR = Memory.FindPattern(Offsets.Mask.BlipPTR);
                Globals.BlipPTR = Memory.Rip_37(Offsets.Mask.TempPTR);

                Offsets.Mask.TempPTR = Memory.FindPattern(Offsets.Mask.GlobalPTR);
                Globals.GlobalPTR = Memory.Rip_37(Offsets.Mask.TempPTR);

                Offsets.Mask.TempPTR = Memory.FindPattern(Offsets.Mask.ReplayInterfacePTR);
                Globals.ReplayInterfacePTR = Memory.Rip_37(Offsets.Mask.TempPTR);

                Application.Current.Dispatcher.Invoke(() =>
                {

                });
            });
        }

        private void AppendTextBox(string str)
        {
            TextBox_Result.AppendText($"[{DateTime.Now:T}] {str}\r\n");
            TextBox_Result.ScrollToEnd();
        }

        private void Window_HeistPreps_Closing(object sender, CancelEventArgs e)
        {

        }

        ////////////////////////////////////////////////////

        private void Button_H3OPT_BITSET1_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET1", -1);
            Hacks.WriteStat("_H3OPT_BITSET1", 0);
            AppendTextBox($"重置第一款面板成功");
        }

        ////////////////////////////////////////////////////

        private void Button_H3OPT_APPROACH_1_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET1", 0);

            Hacks.WriteStat("_H3OPT_APPROACH", 1);

            Hacks.WriteStat("_H3OPT_BITSET1", -1);
            AppendTextBox($"设置抢劫方式为 隐迹潜踪 成功");
        }

        private void Button_H3OPT_APPROACH_2_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET1", 0);

            Hacks.WriteStat("_H3OPT_APPROACH", 2);

            Hacks.WriteStat("_H3OPT_BITSET1", -1);
            AppendTextBox($"设置抢劫方式为 兵不厌诈 成功");
        }

        private void Button_H3OPT_APPROACH_3_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET1", 0);

            Hacks.WriteStat("_H3OPT_APPROACH", 3);

            Hacks.WriteStat("_H3OPT_BITSET1", -1);
            AppendTextBox($"设置抢劫方式为 气势汹汹 成功");
        }

        ////////////////////////////////////////////////////

        private void Button_H3OPT_TARGET_0_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET1", 0);

            Hacks.WriteStat("_H3OPT_TARGET", 0);

            Hacks.WriteStat("_H3OPT_BITSET1", -1);
            AppendTextBox($"设置抢劫物品为 现金 成功");
        }

        private void Button_H3OPT_TARGET_1_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET1", 0);

            Hacks.WriteStat("_H3OPT_TARGET", 1);

            Hacks.WriteStat("_H3OPT_BITSET1", -1);
            AppendTextBox($"设置抢劫物品为 黄金 成功");
        }

        private void Button_H3OPT_TARGET_2_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET1", 0);

            Hacks.WriteStat("_H3OPT_TARGET", 2);

            Hacks.WriteStat("_H3OPT_BITSET1", -1);
            AppendTextBox($"设置抢劫物品为 艺术品 成功");
        }

        private void Button_H3OPT_TARGET_3_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET1", 0);

            Hacks.WriteStat("_H3OPT_TARGET", 3);

            Hacks.WriteStat("_H3OPT_BITSET1", -1);
            AppendTextBox($"设置抢劫物品为 钻石 成功");
        }

        ////////////////////////////////////////////////////

        private void Button_H3OPT_ACCESSPOINTS_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET1", 0);

            Hacks.WriteStat("_H3OPT_ACCESSPOINTS", -1);

            Hacks.WriteStat("_H3OPT_BITSET1", -1);
            AppendTextBox($"解锁所有侦察点成功");
        }

        private void Button_H3OPT_ACCESSPOINTS_0_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET1", 0);

            Hacks.WriteStat("_H3OPT_ACCESSPOINTS", 0);

            Hacks.WriteStat("_H3OPT_BITSET1", -1);
            AppendTextBox($"取消解锁所有侦察点成功");
        }

        private void Button_H3OPT_H3OPT_POI_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET1", 0);

            Hacks.WriteStat("_H3OPT_POI", -1);

            Hacks.WriteStat("_H3OPT_BITSET1", -1);
            AppendTextBox($"解锁所有兴趣点成功");
        }

        private void Button_H3OPT_H3OPT_POI_0_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET1", 0);

            Hacks.WriteStat("_H3OPT_POI", 0);

            Hacks.WriteStat("_H3OPT_BITSET1", -1);
            AppendTextBox($"取消解锁所有兴趣点成功");
        }

        ////////////////////////////////////////////////////

        private void Button_H3OPT_BITSET0_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET0", -1);
            Hacks.WriteStat("_H3OPT_BITSET0", 0);
            AppendTextBox($"重置第二款面板成功");
        }

        ////////////////////////////////////////////////////

        private void Button_H3OPT_DISRUPTSHIP_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET0", 0);

            Hacks.WriteStat("_H3OPT_DISRUPTSHIP", 3);

            Hacks.WriteStat("_H3OPT_BITSET0", -1);
            AppendTextBox($"撤走重型武装警卫成功");
        }

        private void Button_H3OPT_KEYLEVELS_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET0", 0);

            Hacks.WriteStat("_H3OPT_KEYLEVELS", 2);

            Hacks.WriteStat("_H3OPT_BITSET0", -1);
            AppendTextBox($"获得二级门禁卡成功");
        }

        ////////////////////////////////////////////////////

        private void Button_H3OPT_CREWWEAP_1_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET0", 0);

            Hacks.WriteStat("_H3OPT_CREWWEAP", 1);

            Hacks.WriteStat("_H3OPT_BITSET0", -1);
            AppendTextBox($"设置枪手等级为 卡尔·阿不拉季 5% 成功");
        }

        private void Button_H3OPT_CREWWEAP_2_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET0", 0);

            Hacks.WriteStat("_H3OPT_CREWWEAP", 2);

            Hacks.WriteStat("_H3OPT_BITSET0", -1);
            AppendTextBox($"设置枪手等级为 古斯塔沃·莫塔 9％ 成功");
        }

        private void Button_H3OPT_CREWWEAP_3_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET0", 0);

            Hacks.WriteStat("_H3OPT_CREWWEAP", 3);

            Hacks.WriteStat("_H3OPT_BITSET0", -1);
            AppendTextBox($"设置枪手等级为 查理·里德 7％ 成功");
        }

        private void Button_H3OPT_CREWWEAP_4_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET0", 0);

            Hacks.WriteStat("_H3OPT_CREWWEAP", 4);

            Hacks.WriteStat("_H3OPT_BITSET0", -1);
            AppendTextBox($"设置枪手等级为 切斯特·麦考伊 10％ 成功");
        }

        private void Button_H3OPT_CREWWEAP_5_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET0", 0);

            Hacks.WriteStat("_H3OPT_CREWWEAP", 5);

            Hacks.WriteStat("_H3OPT_BITSET0", -1);
            AppendTextBox($"设置枪手等级为 帕里克·麦克瑞利 8％ 成功");
        }

        private void Button_H3OPT_CREWWEAP_6_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET0", 0);

            Hacks.WriteStat("_H3OPT_CREWWEAP", 6);

            Hacks.WriteStat("_H3OPT_BITSET0", -1);
            AppendTextBox($"设置枪手等级为 枪手零分红 成功");
        }

        ////////////////////////////////////////////////////

        private void Button_H3OPT_CREWDRIVER_1_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET0", 0);

            Hacks.WriteStat("_H3OPT_CREWDRIVER", 1);

            Hacks.WriteStat("_H3OPT_BITSET0", -1);
            AppendTextBox($"设置车手等级为 卡里姆·登茨 5％ 成功");
        }

        private void Button_H3OPT_CREWDRIVER_2_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET0", 0);

            Hacks.WriteStat("_H3OPT_CREWDRIVER", 2);

            Hacks.WriteStat("_H3OPT_BITSET0", -1);
            AppendTextBox($"设置车手等级为 塔丽娜·马丁内斯 7％ 成功");
        }

        private void Button_H3OPT_CREWDRIVER_3_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET0", 0);

            Hacks.WriteStat("_H3OPT_CREWDRIVER", 3);

            Hacks.WriteStat("_H3OPT_BITSET0", -1);
            AppendTextBox($"设置车手等级为 淘艾迪 9％ 成功");
        }

        private void Button_H3OPT_CREWDRIVER_4_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET0", 0);

            Hacks.WriteStat("_H3OPT_CREWDRIVER", 4);

            Hacks.WriteStat("_H3OPT_BITSET0", -1);
            AppendTextBox($"设置车手等级为 扎克·尼尔森 6％ 成功");
        }

        private void Button_H3OPT_CREWDRIVER_5_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET0", 0);

            Hacks.WriteStat("_H3OPT_CREWDRIVER", 5);

            Hacks.WriteStat("_H3OPT_BITSET0", -1);
            AppendTextBox($"设置车手等级为 切斯特·麦考伊 10％ 成功");
        }

        private void Button_H3OPT_CREWDRIVER_6_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET0", 0);

            Hacks.WriteStat("_H3OPT_CREWDRIVER", 6);

            Hacks.WriteStat("_H3OPT_BITSET0", -1);
            AppendTextBox($"设置车手等级为 车手零分红 成功");
        }

        ////////////////////////////////////////////////////

        private void Button_H3OPT_CREWHACKER_1_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET0", 0);

            Hacks.WriteStat("_H3OPT_CREWHACKER", 1);

            Hacks.WriteStat("_H3OPT_BITSET0", -1);
            AppendTextBox($"设置黑客等级为 里奇·卢肯斯 3％ 成功");
        }

        private void Button_H3OPT_CREWHACKER_2_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET0", 0);

            Hacks.WriteStat("_H3OPT_CREWHACKER", 2);

            Hacks.WriteStat("_H3OPT_BITSET0", -1);
            AppendTextBox($"设置黑客等级为 克里斯汀·费尔兹 7％ 成功");
        }

        private void Button_H3OPT_CREWHACKER_3_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET0", 0);

            Hacks.WriteStat("_H3OPT_CREWHACKER", 3);

            Hacks.WriteStat("_H3OPT_BITSET0", -1);
            AppendTextBox($"设置黑客等级为 尤汗·布莱尔 5％ 成功");
        }

        private void Button_H3OPT_CREWHACKER_4_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET0", 0);

            Hacks.WriteStat("_H3OPT_CREWHACKER", 4);

            Hacks.WriteStat("_H3OPT_BITSET0", -1);
            AppendTextBox($"设置黑客等级为 阿维·施瓦茨曼 10％ 成功");
        }

        private void Button_H3OPT_CREWHACKER_5_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET0", 0);

            Hacks.WriteStat("_H3OPT_CREWHACKER", 5);

            Hacks.WriteStat("_H3OPT_BITSET0", -1);
            AppendTextBox($"设置黑客等级为 佩奇·哈里斯 9％ 成功");
        }

        private void Button_H3OPT_CREWHACKER_6_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET0", 0);

            Hacks.WriteStat("_H3OPT_CREWHACKER", 6);

            Hacks.WriteStat("_H3OPT_BITSET0", -1);
            AppendTextBox($"设置黑客等级为 黑客零分红 成功");
        }

        ////////////////////////////////////////////////////

        private void Button_H3OPT_WEAPS_0_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET0", 0);

            Hacks.WriteStat("_H3OPT_WEAPS", 0);

            Hacks.WriteStat("_H3OPT_BITSET0", -1);
            AppendTextBox($"设置武器类型为 类型一 成功");
        }

        private void Button_H3OPT_WEAPS_1_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET0", 0);

            Hacks.WriteStat("_H3OPT_WEAPS", 1);

            Hacks.WriteStat("_H3OPT_BITSET0", -1);
            AppendTextBox($"设置武器类型为 类型二 成功");
        }

        private void Button_H3OPT_WEAPS_2_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET0", 0);

            Hacks.WriteStat("_H3OPT_WEAPS", 2);

            Hacks.WriteStat("_H3OPT_BITSET0", -1);
            AppendTextBox($"设置武器类型为 类型三 成功");
        }

        private void Button_H3OPT_WEAPS_3_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET0", 0);

            Hacks.WriteStat("_H3OPT_WEAPS", 3);

            Hacks.WriteStat("_H3OPT_BITSET0", -1);
            AppendTextBox($"设置武器类型为 类型四 成功");
        }

        private void Button_H3OPT_WEAPS_4_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET0", 0);

            Hacks.WriteStat("_H3OPT_WEAPS", 4);

            Hacks.WriteStat("_H3OPT_BITSET0", -1);
            AppendTextBox($"设置武器类型为 类型五 成功");
        }

        ////////////////////////////////////////////////////

        private void Button_H3OPT_VEH_0_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET0", 0);

            Hacks.WriteStat("_H3OPT_VEHS", 0);

            Hacks.WriteStat("_H3OPT_BITSET0", -1);
            AppendTextBox($"设置逃亡载具为 类型一 成功");
        }

        private void Button_H3OPT_VEH_1_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET0", 0);

            Hacks.WriteStat("_H3OPT_VEHS", 1);

            Hacks.WriteStat("_H3OPT_BITSET0", -1);
            AppendTextBox($"设置逃亡载具为 类型二 成功");
        }

        private void Button_H3OPT_VEH_2_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET0", 0);

            Hacks.WriteStat("_H3OPT_VEHS", 2);

            Hacks.WriteStat("_H3OPT_BITSET0", -1);
            AppendTextBox($"设置逃亡载具为 类型三 成功");
        }

        private void Button_H3OPT_VEH_3_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_H3OPT_BITSET0", 0);

            Hacks.WriteStat("_H3OPT_VEHS", 3);

            Hacks.WriteStat("_H3OPT_BITSET0", -1);
            AppendTextBox($"设置逃亡载具为 类型四 成功");
        }

        ////////////////////////////////////////////////////

        private void Button_FastTeleport_Click(object sender, RoutedEventArgs e)
        {
            var str = (e.OriginalSource as Button).Content.ToString();

            int index = HeistPrepsConfig.FastTeleport.FindIndex(t => t.TName == str);
            if (index != -1)
            {
                Teleport.SetTeleportV3Pos(HeistPrepsConfig.FastTeleport[index].TCode);
            }

            AppendTextBox($"传送到 {str} 成功");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Teleport.ToBlips(740);

            AppendTextBox($"传送到 游戏厅图标处 成功");
        }

        ////////////////////////////////////////////////////

        private void Button_GANGOPS_FLOW_MISSION_PROG_503_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_GANGOPS_FLOW_MISSION_PROG", 503);

            Hacks.WriteStat("_GANGOPS_HEIST_STATUS", 229383);

            Hacks.WriteStat("_GANGOPS_FLOW_NOTIFICATIONS", 1557);

            AppendTextBox($"进入末日一分红关 成功");
        }

        private void Button_GANGOPS_FLOW_MISSION_PROG_240_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_GANGOPS_FLOW_MISSION_PROG", 240);

            Hacks.WriteStat("_GANGOPS_HEIST_STATUS", 229378);

            Hacks.WriteStat("_GANGOPS_FLOW_NOTIFICATIONS", 1557);

            AppendTextBox($"进入末日二分红关 成功");
        }

        private void Button_GANGOPS_FLOW_MISSION_PROG_16368_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_GANGOPS_FLOW_MISSION_PROG", 16368);

            Hacks.WriteStat("_GANGOPS_HEIST_STATUS", 229380);

            Hacks.WriteStat("_GANGOPS_FLOW_NOTIFICATIONS", 1557);

            AppendTextBox($"进入末日三分红关 成功");
        }

        ////////////////////////////////////////////////////

        private void Button_HEISTCOOLDOWNTIMER0_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_HEISTCOOLDOWNTIMER0", -1);

            AppendTextBox($"重置末日一冷却 成功");
        }

        private void Button_HEISTCOOLDOWNTIMER1_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_HEISTCOOLDOWNTIMER1", -1);

            AppendTextBox($"重置末日二冷却 成功");
        }

        private void Button_HEISTCOOLDOWNTIMER2_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_HEISTCOOLDOWNTIMER2", -1);

            AppendTextBox($"重置末日三冷却 成功");
        }

        private void Button_GANGOPS_HEIST_STATUS_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_GANGOPS_HEIST_STATUS", -1);

            AppendTextBox($"重置末日一二三任务 成功");
        }

        private void Button_GANGOPS_FLOW_NOTIFICATIONS_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_GANGOPS_HEIST_STATUS", 9999);

            //Hacks.WriteStat("_GANGOPS_HEIST_STATUS", -1);
            //CoreUtils.Delay(500);

            //Hacks.WriteStat("_GANGOPS_FLOW_NOTIFICATIONS", -1);
            //CoreUtils.Delay(500);

            AppendTextBox($"解锁重玩末日豪劫 成功");
        }

        private void Button_GANGOPS_FLOW_MISSION_PROG_1_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_GANGOPS_FLOW_MISSION_PROG", -1);

            AppendTextBox($"跳过末日前置及准备任务 成功");
        }

        private void Button_HEIST_PLANNING_STAGE_Click(object sender, RoutedEventArgs e)
        {
            Hacks.WriteStat("_HEIST_PLANNING_STAGE", -1);

            AppendTextBox($"直接进入分红关 成功");
        }
    }

    public class HeistPrepsConfig
    {
        public static List<TeleportPreview> FastTeleport = new List<TeleportPreview>()
        {
            new TeleportPreview(){ TName = "赌场门口", TCode = new Vector3 { X=911.072f, Y=53.321f, Z=80.893f } },
            new TeleportPreview(){ TName = "监控和安保人员", TCode = new Vector3 { X=1089.614f, Y=215.696f, Z=-49.200f } },
            new TeleportPreview(){ TName = "门禁系统", TCode = new Vector3 { X=1117.732f, Y=214.123f, Z=-49.440f } },
            new TeleportPreview(){ TName = "赌场后门", TCode = new Vector3 { X=993.162f, Y=86.234f, Z=80.990f } },
        };
    }
}
