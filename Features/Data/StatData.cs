using System.Collections.Generic;

namespace GTA5OnlineTools.Features.Data
{
    public class StatData
    {
        public class StatInfo
        {
            public string SName;
            public List<StatPreview> SCode;
        }

        public class StatPreview
        {
            public string SHash;
            public int SValue;
        }

        /// <summary>
        /// 玩家护甲全满
        /// </summary>
        public static List<StatPreview> _MP_CHAR_ARMOUR = new List<StatPreview>()
        {
            new StatPreview(){ SHash = "_MP_CHAR_ARMOUR_1_COUNT", SValue = 100 },
            new StatPreview(){ SHash = "_MP_CHAR_ARMOUR_2_COUNT", SValue = 100 },
            new StatPreview(){ SHash = "_MP_CHAR_ARMOUR_3_COUNT", SValue = 100 },
            new StatPreview(){ SHash = "_MP_CHAR_ARMOUR_4_COUNT", SValue = 100 },
            new StatPreview(){ SHash = "_MP_CHAR_ARMOUR_5_COUNT", SValue = 100 },
        };

        /// <summary>
        /// 玩家零食全满
        /// </summary>
        public static List<StatPreview> _NO_BOUGHT = new List<StatPreview>()
        {
            new StatPreview(){ SHash = "_NO_BOUGHT_YUM_SNACKS", SValue = 100 },
            new StatPreview(){ SHash = "_NO_BOUGHT_HEALTH_SNACKS", SValue = 100 },
            new StatPreview(){ SHash = "_NO_BOUGHT_EPIC_SNACKS", SValue = 100 },
            new StatPreview(){ SHash = "_NUMBER_OF_ORANGE_BOUGHT", SValue = 100 },
            new StatPreview(){ SHash = "_NUMBER_OF_BOURGE_BOUGHT", SValue = 100 },
            new StatPreview(){ SHash = "_CIGARETTES_BOUGHT", SValue = 100 },
            new StatPreview(){ SHash = "_NUMBER_OF_CHAMP_BOUGHT", SValue = 100 },
        };

        /// <summary>
        /// 玩家属性全满
        /// </summary>
        public static List<StatPreview> _SCRIPT_INCREASE = new List<StatPreview>()
        {
            new StatPreview(){ SHash = "_SCRIPT_INCREASE_STAM", SValue = 100 },
            new StatPreview(){ SHash = "_SCRIPT_INCREASE_SHO", SValue = 100 },
            new StatPreview(){ SHash = "_SCRIPT_INCREASE_STRN", SValue = 100 },
            new StatPreview(){ SHash = "_SCRIPT_INCREASE_STL", SValue = 100 },
            new StatPreview(){ SHash = "_SCRIPT_INCREASE_FLY", SValue = 100 },
            new StatPreview(){ SHash = "_SCRIPT_INCREASE_DRIV", SValue = 100 },
            new StatPreview(){ SHash = "_SCRIPT_INCREASE_LUNG", SValue = 100 },
        };

        /// <summary>
        /// 玩家隐藏属性全满
        /// </summary>
        public static List<StatPreview> _CHAR_ABILITY = new List<StatPreview>()
        {
            new StatPreview(){ SHash = "_CHAR_ABILITY_1_UNLCK", SValue = -1 },
            new StatPreview(){ SHash = "_CHAR_ABILITY_2_UNLCK", SValue = -1 },
            new StatPreview(){ SHash = "_CHAR_ABILITY_3_UNLCK", SValue = -1 },
            new StatPreview(){ SHash = "_CHAR_FM_ABILITY_1_UNLCK", SValue = -1 },
            new StatPreview(){ SHash = "_CHAR_FM_ABILITY_2_UNLCK", SValue = -1 },
            new StatPreview(){ SHash = "_CHAR_FM_ABILITY_3_UNLCK", SValue = -1 },
        };

        /// <summary>
        /// 设置玩家等级为1
        /// </summary>
        public static List<StatPreview> _CHAR_SET_RP1 = new List<StatPreview>()
        {
            new StatPreview(){ SHash = "_CHAR_SET_RP_GIFT_ADMIN", SValue = 0 },
        };

        /// <summary>
        /// 设置玩家等级为30
        /// </summary>
        public static List<StatPreview> _CHAR_SET_RP30 = new List<StatPreview>()
        {
            new StatPreview(){ SHash = "_CHAR_SET_RP_GIFT_ADMIN", SValue = 177100 },
        };

        /// <summary>
        /// 设置玩家等级为60
        /// </summary>
        public static List<StatPreview> _CHAR_SET_RP60 = new List<StatPreview>()
        {
            new StatPreview(){ SHash = "_CHAR_SET_RP_GIFT_ADMIN", SValue = 625400 },
        };

        /// <summary>
        /// 设置玩家等级为90
        /// </summary>
        public static List<StatPreview> _CHAR_SET_RP90 = new List<StatPreview>()
        {
            new StatPreview(){ SHash = "_CHAR_SET_RP_GIFT_ADMIN", SValue = 1308100 },
        };

        /// <summary>
        /// 设置玩家等级为120
        /// </summary>
        public static List<StatPreview> _CHAR_SET_RP120 = new List<StatPreview>()
        {
            new StatPreview(){ SHash = "_CHAR_SET_RP_GIFT_ADMIN", SValue = 2165850 },
        };

        /// <summary>
        /// 赌场抢劫面板重置
        /// </summary>
        public static List<StatPreview> _H3OPT = new List<StatPreview>()
        {
            new StatPreview(){ SHash = "_H3OPT_BITSET1", SValue = 0 },
            new StatPreview(){ SHash = "_H3OPT_BITSET0", SValue = 0 },
            new StatPreview(){ SHash = "_H3OPT_POI", SValue = 0 },
            new StatPreview(){ SHash = "_H3OPT_ACCESSPOINTS", SValue = 0 },
            new StatPreview(){ SHash = "_CAS_HEIST_FLOW", SValue = 0 },
        };

        /// <summary>
        /// 佩里克岛抢劫面板重置
        /// </summary>
        public static List<StatPreview> _H4 = new List<StatPreview>()
        {
            new StatPreview(){ SHash = "_H4_MISSIONS", SValue = 0 },
            new StatPreview(){ SHash = "_H4_PROGRESS", SValue = 0 },
            new StatPreview(){ SHash = "_H4_PLAYTHROUGH_STATUS", SValue = 0 },
            new StatPreview(){ SHash = "_H4CNF_APPROACH", SValue = 0 },
            new StatPreview(){ SHash = "_H4CNF_BS_ENTR", SValue = 0 },
            new StatPreview(){ SHash = "_H4CNF_BS_GEN", SValue = 0 },
        };

        /// <summary>
        /// 玩家性别修改（去重新捏脸）
        /// </summary>
        public static List<StatPreview> _GENDER_CHANGE = new List<StatPreview>()
        {
            new StatPreview(){ SHash = "_ALLOW_GENDER_CHANGE", SValue = 52 },
        };

        /// <summary>
        /// 补满夜总会人气
        /// </summary>
        public static List<StatPreview> _CLUB_POPULARITY = new List<StatPreview>()
        {
            new StatPreview(){ SHash = "_CLUB_POPULARITY", SValue = 1000 },
        };

        /// <summary>
        /// 重置地堡总营收
        /// </summary>
        public static List<StatPreview> _LIFETIME_BKR_SELL = new List<StatPreview>()
        {
            new StatPreview(){ SHash = "_LIFETIME_BKR_SELL_EARNINGS5", SValue = 0 },
        };

        /// <summary>
        /// 设置车友会等级为1
        /// </summary>
        public static List<StatPreview> _CAR_CLUB_REP = new List<StatPreview>()
        {
            new StatPreview(){ SHash = "_CAR_CLUB_REP", SValue = 5 },
        };

        /// <summary>
        /// 跳过过场动画 (地堡、摩托帮、办公室等)
        /// </summary>
        public static List<StatPreview> _FM_CUT_DONE = new List<StatPreview>()
        {
            new StatPreview(){ SHash = "_FM_CUT_DONE", SValue = -1 },
            new StatPreview(){ SHash = "_FM_CUT_DONE_2", SValue = -1 },
        };

        /// <summary>
        /// 地堡、摩托帮自动进货
        /// </summary>
        public static List<StatPreview> _PAYRESUPPLYTIMER0 = new List<StatPreview>()
        {
            new StatPreview(){ SHash = "_PAYRESUPPLYTIMER0", SValue = 1 },
            new StatPreview(){ SHash = "_PAYRESUPPLYTIMER1", SValue = 1 },
            new StatPreview(){ SHash = "_PAYRESUPPLYTIMER2", SValue = 1 },
            new StatPreview(){ SHash = "_PAYRESUPPLYTIMER3", SValue = 1 },
            new StatPreview(){ SHash = "_PAYRESUPPLYTIMER4", SValue = 1 },
            new StatPreview(){ SHash = "_PAYRESUPPLYTIMER5", SValue = 1 },
        };

        /// <summary>
        /// 重置载具销售计时
        /// </summary>
        public static List<StatPreview> MPPLY_VEHICLE = new List<StatPreview>()
        {
            new StatPreview(){ SHash = "MPPLY_VEHICLE_SELL_TIME", SValue = 0 },
            new StatPreview(){ SHash = "MPPLY_NUM_CARS_SOLD_TODAY", SValue = 0 },
        };

        /// <summary>
        /// CEO办公室满地钱+小金人
        /// </summary>
        public static List<StatPreview> _LIFETIME_BUY_UNDERTAKEN = new List<StatPreview>()
        {
            new StatPreview(){ SHash = "_LIFETIME_BUY_UNDERTAKEN", SValue = 1000 },
            new StatPreview(){ SHash = "_LIFETIME_BUY_COMPLETE", SValue = 1000 },
            new StatPreview(){ SHash = "_LIFETIME_SELL_UNDERTAKEN", SValue = 10 },
            new StatPreview(){ SHash = "_LIFETIME_SELL_COMPLETE", SValue = 10 },
            new StatPreview(){ SHash = "_LIFETIME_CONTRA_EARNINGS", SValue = 18000000 },
        };

        /// <summary>
        /// 解锁电话联系人功能
        /// </summary>
        public static List<StatPreview> _FM_ACT_PHN = new List<StatPreview>()
        {
            new StatPreview(){ SHash = "_FM_ACT_PHN", SValue = -1 },
            new StatPreview(){ SHash = "_FM_ACT_PH2", SValue = -1 },
            new StatPreview(){ SHash = "_FM_ACT_PH3", SValue = -1 },
            new StatPreview(){ SHash = "_FM_ACT_PH4", SValue = -1 },
            new StatPreview(){ SHash = "_FM_ACT_PH5", SValue = -1 },
            new StatPreview(){ SHash = "_FM_VEH_TX1", SValue = -1 },
            new StatPreview(){ SHash = "_FM_ACT_PH6", SValue = -1 },
            new StatPreview(){ SHash = "_FM_ACT_PH7", SValue = -1 },
            new StatPreview(){ SHash = "_FM_ACT_PH8", SValue = -1 },
            new StatPreview(){ SHash = "_FM_ACT_PH9", SValue = -1 },
            new StatPreview(){ SHash = "_FM_CUT_DONE", SValue = -1 },
            new StatPreview(){ SHash = "_FM_CUT_DONE_2", SValue = -1 },
        };

        /// <summary>
        /// 公寓抢劫直接分红
        /// </summary>
        public static List<StatPreview> _HEIST_PLANNING_STAGE = new List<StatPreview>()
        {
            new StatPreview(){ SHash = "_HEIST_PLANNING_STAGE", SValue = -1 },
        };

        /// <summary>
        /// 载具节日涂装
        /// </summary>
        public static List<StatPreview> MPPLY_XMASLIVERIES0 = new List<StatPreview>()
        {
            new StatPreview(){ SHash = "MPPLY_XMASLIVERIES0", SValue = -1 },
            new StatPreview(){ SHash = "MPPLY_XMASLIVERIES1", SValue = -1 },
            new StatPreview(){ SHash = "MPPLY_XMASLIVERIES2", SValue = -1 },
            new StatPreview(){ SHash = "MPPLY_XMASLIVERIES3", SValue = -1 },
            new StatPreview(){ SHash = "MPPLY_XMASLIVERIES4", SValue = -1 },
            new StatPreview(){ SHash = "MPPLY_XMASLIVERIES5", SValue = -1 },
            new StatPreview(){ SHash = "MPPLY_XMASLIVERIES6", SValue = -1 },
            new StatPreview(){ SHash = "MPPLY_XMASLIVERIES7", SValue = -1 },
            new StatPreview(){ SHash = "MPPLY_XMASLIVERIES8", SValue = -1 },
            new StatPreview(){ SHash = "MPPLY_XMASLIVERIES9", SValue = -1 },
            new StatPreview(){ SHash = "MPPLY_XMASLIVERIES10", SValue = -1 },
            new StatPreview(){ SHash = "MPPLY_XMASLIVERIES11", SValue = -1 },
            new StatPreview(){ SHash = "MPPLY_XMASLIVERIES12", SValue = -1 },
            new StatPreview(){ SHash = "MPPLY_XMASLIVERIES13", SValue = -1 },
            new StatPreview(){ SHash = "MPPLY_XMASLIVERIES14", SValue = -1 },
            new StatPreview(){ SHash = "MPPLY_XMASLIVERIES15", SValue = -1 },
            new StatPreview(){ SHash = "MPPLY_XMASLIVERIES16", SValue = -1 },
            new StatPreview(){ SHash = "MPPLY_XMASLIVERIES17", SValue = -1 },
            new StatPreview(){ SHash = "MPPLY_XMASLIVERIES18", SValue = -1 },
            new StatPreview(){ SHash = "MPPLY_XMASLIVERIES19", SValue = -1 },
            new StatPreview(){ SHash = "MPPLY_XMASLIVERIES20", SValue = -1 },
        };

        /// <summary>
        /// 删除角色后清空剩余数据
        /// </summary>
        public static List<StatPreview> MPPLY_TOTAL_EVC = new List<StatPreview>()
        {
            new StatPreview(){ SHash = "MPPLY_TOTAL_EVC", SValue = 0 },
            new StatPreview(){ SHash = "MPPLY_TOTAL_SVC", SValue = 0 },
            new StatPreview(){ SHash = "MP_PLAYING_TIME", SValue = 1 },
            new StatPreview(){ SHash = "MP_FIRST_PERSON_CAM_TIME", SValue = 1 },
        };

        /// <summary>
        /// 解决赌场侦察拍照问题
        /// </summary>
        public static List<StatPreview> _H3OPT_ACCESSPOINTS = new List<StatPreview>()
        {
            new StatPreview(){ SHash = "_H3OPT_ACCESSPOINTS", SValue = 0 },
            new StatPreview(){ SHash = "_H3OPT_POI", SValue = 0 },
        };

        //////////////////////////////////////////////////////////////

        public static List<StatInfo> StatDataClass = new List<StatInfo>()
        {
            new StatInfo(){ SName = "玩家护甲全满", SCode = _MP_CHAR_ARMOUR },
            new StatInfo(){ SName = "角色零食全满", SCode = _NO_BOUGHT },
            new StatInfo(){ SName = "玩家属性全满", SCode = _SCRIPT_INCREASE },
            new StatInfo(){ SName = "设置玩家等级为1", SCode = _CHAR_SET_RP1 },
            new StatInfo(){ SName = "设置玩家等级为30", SCode = _CHAR_SET_RP30 },
            new StatInfo(){ SName = "设置玩家等级为60", SCode = _CHAR_SET_RP60 },
            new StatInfo(){ SName = "设置玩家等级为90", SCode = _CHAR_SET_RP90 },
            new StatInfo(){ SName = "设置玩家等级为120", SCode = _CHAR_SET_RP120 },
            new StatInfo(){ SName = "赌场抢劫面板重置", SCode = _H3OPT },
            new StatInfo(){ SName = "佩里克岛抢劫面板重置", SCode = _H4 },
            new StatInfo(){ SName = "玩家隐藏属性全满", SCode = _CHAR_ABILITY },
            new StatInfo(){ SName = "玩家性别修改", SCode = _GENDER_CHANGE },
            new StatInfo(){ SName = "补满夜总会人气", SCode = _CLUB_POPULARITY },
            new StatInfo(){ SName = "重置地堡总营收", SCode = _LIFETIME_BKR_SELL },
            new StatInfo(){ SName = "设置车友会等级为1", SCode = _CAR_CLUB_REP },
            new StatInfo(){ SName = "跳过过场动画", SCode = _FM_CUT_DONE },
            new StatInfo(){ SName = "地堡和摩托帮自动进货", SCode = _PAYRESUPPLYTIMER0 },
            new StatInfo(){ SName = "重置载具销售计时", SCode = MPPLY_VEHICLE },
            new StatInfo(){ SName = "CEO办公室满地钱和小金人", SCode = _LIFETIME_BUY_UNDERTAKEN },
            new StatInfo(){ SName = "解锁电话联系人功能", SCode = _FM_ACT_PHN },
            new StatInfo(){ SName = "公寓抢劫直接分红", SCode = _HEIST_PLANNING_STAGE },
            new StatInfo(){ SName = "载具节日涂装", SCode = MPPLY_XMASLIVERIES0 },

            new StatInfo(){ SName = "删除角色后清空剩余数据", SCode = MPPLY_TOTAL_EVC },
            new StatInfo(){ SName = "解决赌场侦察拍照问题", SCode = _H3OPT_ACCESSPOINTS },

        };
    }
}
