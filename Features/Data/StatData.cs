namespace GTA5OnlineTools.Features.Data
{
    public class StatData
    {
        public struct StatClass
        {
            public string ClassName;
            public List<StatInfo> StatInfo;
        }

        public struct StatInfo
        {
            public string Hash;
            public int Value;
        }

        /// <summary>
        /// 玩家护甲全满
        /// </summary>
        public static List<StatInfo> _MP_CHAR_ARMOUR = new List<StatInfo>()
        {
            new StatInfo(){ Hash="_MP_CHAR_ARMOUR_1_COUNT", Value=100 },
            new StatInfo(){ Hash="_MP_CHAR_ARMOUR_2_COUNT", Value=100 },
            new StatInfo(){ Hash="_MP_CHAR_ARMOUR_3_COUNT", Value=100 },
            new StatInfo(){ Hash="_MP_CHAR_ARMOUR_4_COUNT", Value=100 },
            new StatInfo(){ Hash="_MP_CHAR_ARMOUR_5_COUNT", Value=100 },
        };

        /// <summary>
        /// 玩家零食全满
        /// </summary>
        public static List<StatInfo> _NO_BOUGHT = new List<StatInfo>()
        {
            new StatInfo(){ Hash="_NO_BOUGHT_YUM_SNACKS", Value=100 },
            new StatInfo(){ Hash="_NO_BOUGHT_HEALTH_SNACKS", Value=100 },
            new StatInfo(){ Hash="_NO_BOUGHT_EPIC_SNACKS", Value=100 },
            new StatInfo(){ Hash="_NUMBER_OF_ORANGE_BOUGHT", Value=100 },
            new StatInfo(){ Hash="_NUMBER_OF_BOURGE_BOUGHT", Value=100 },
            new StatInfo(){ Hash="_CIGARETTES_BOUGHT", Value=100 },
            new StatInfo(){ Hash="_NUMBER_OF_CHAMP_BOUGHT", Value=100 },
        };

        /// <summary>
        /// 玩家属性全满
        /// </summary>
        public static List<StatInfo> _SCRIPT_INCREASE = new List<StatInfo>()
        {
            new StatInfo(){ Hash="_SCRIPT_INCREASE_STAM", Value=100 },
            new StatInfo(){ Hash="_SCRIPT_INCREASE_SHO", Value=100 },
            new StatInfo(){ Hash="_SCRIPT_INCREASE_STRN", Value=100 },
            new StatInfo(){ Hash="_SCRIPT_INCREASE_STL", Value=100 },
            new StatInfo(){ Hash="_SCRIPT_INCREASE_FLY", Value=100 },
            new StatInfo(){ Hash="_SCRIPT_INCREASE_DRIV", Value=100 },
            new StatInfo(){ Hash="_SCRIPT_INCREASE_LUNG", Value=100 },
        };

        /// <summary>
        /// 玩家隐藏属性全满
        /// </summary>
        public static List<StatInfo> _CHAR_ABILITY = new List<StatInfo>()
        {
            new StatInfo(){ Hash="_CHAR_ABILITY_1_UNLCK", Value=-1 },
            new StatInfo(){ Hash="_CHAR_ABILITY_2_UNLCK", Value=-1 },
            new StatInfo(){ Hash="_CHAR_ABILITY_3_UNLCK", Value=-1 },
            new StatInfo(){ Hash="_CHAR_FM_ABILITY_1_UNLCK", Value=-1 },
            new StatInfo(){ Hash="_CHAR_FM_ABILITY_2_UNLCK", Value=-1 },
            new StatInfo(){ Hash="_CHAR_FM_ABILITY_3_UNLCK", Value=-1 },
        };

        /// <summary>
        /// 设置玩家等级为1
        /// </summary>
        public static List<StatInfo> _CHAR_SET_RP1 = new List<StatInfo>()
        {
            new StatInfo(){ Hash="_CHAR_SET_RP_GIFT_ADMIN", Value=0 },
        };

        /// <summary>
        /// 设置玩家等级为30
        /// </summary>
        public static List<StatInfo> _CHAR_SET_RP30 = new List<StatInfo>()
        {
            new StatInfo(){ Hash="_CHAR_SET_RP_GIFT_ADMIN", Value=177100 },
        };

        /// <summary>
        /// 设置玩家等级为60
        /// </summary>
        public static List<StatInfo> _CHAR_SET_RP60 = new List<StatInfo>()
        {
            new StatInfo(){ Hash="_CHAR_SET_RP_GIFT_ADMIN", Value=625400 },
        };

        /// <summary>
        /// 设置玩家等级为90
        /// </summary>
        public static List<StatInfo> _CHAR_SET_RP90 = new List<StatInfo>()
        {
            new StatInfo(){ Hash="_CHAR_SET_RP_GIFT_ADMIN", Value=1308100 },
        };

        /// <summary>
        /// 设置玩家等级为120
        /// </summary>
        public static List<StatInfo> _CHAR_SET_RP120 = new List<StatInfo>()
        {
            new StatInfo(){ Hash="_CHAR_SET_RP_GIFT_ADMIN", Value=2165850 },
        };

        /// <summary>
        /// 赌场抢劫面板重置
        /// </summary>
        public static List<StatInfo> _H3OPT = new List<StatInfo>()
        {
            new StatInfo(){ Hash="_H3OPT_BITSET1", Value=0 },
            new StatInfo(){ Hash="_H3OPT_BITSET0", Value=0 },
            new StatInfo(){ Hash="_H3OPT_POI", Value=0 },
            new StatInfo(){ Hash="_H3OPT_ACCESSPOINTS", Value=0 },
            new StatInfo(){ Hash="_CAS_HEIST_FLOW", Value=0 },
        };

        /// <summary>
        /// 佩里克岛抢劫面板重置
        /// </summary>
        public static List<StatInfo> _H4 = new List<StatInfo>()
        {
            new StatInfo(){ Hash="_H4_MISSIONS", Value=0 },
            new StatInfo(){ Hash="_H4_PROGRESS", Value=0 },
            new StatInfo(){ Hash="_H4_PLAYTHROUGH_STATUS", Value=0 },
            new StatInfo(){ Hash="_H4CNF_APPROACH", Value=0 },
            new StatInfo(){ Hash="_H4CNF_BS_ENTR", Value=0 },
            new StatInfo(){ Hash="_H4CNF_BS_GEN", Value=0 },
        };

        /// <summary>
        /// 玩家性别修改（去重新捏脸）
        /// </summary>
        public static List<StatInfo> _GENDER_CHANGE = new List<StatInfo>()
        {
            new StatInfo(){ Hash="_ALLOW_GENDER_CHANGE", Value=52 },
        };

        /// <summary>
        /// 补满夜总会人气
        /// </summary>
        public static List<StatInfo> _CLUB_POPULARITY = new List<StatInfo>()
        {
            new StatInfo(){ Hash="_CLUB_POPULARITY", Value=1000 },
        };

        /// <summary>
        /// 重置地堡总营收
        /// </summary>
        public static List<StatInfo> _LIFETIME_BKR_SELL = new List<StatInfo>()
        {
            new StatInfo(){ Hash="_LIFETIME_BKR_SELL_EARNINGS5", Value=0 },
        };

        /// <summary>
        /// 设置车友会等级为1
        /// </summary>
        public static List<StatInfo> _CAR_CLUB_REP = new List<StatInfo>()
        {
            new StatInfo(){ Hash="_CAR_CLUB_REP", Value=5 },
        };

        /// <summary>
        /// 跳过过场动画 (地堡、摩托帮、办公室等)
        /// </summary>
        public static List<StatInfo> _FM_CUT_DONE = new List<StatInfo>()
        {
            new StatInfo(){ Hash="_FM_CUT_DONE", Value=-1 },
            new StatInfo(){ Hash="_FM_CUT_DONE_2", Value=-1 },
        };

        /// <summary>
        /// 地堡、摩托帮自动进货
        /// </summary>
        public static List<StatInfo> _PAYRESUPPLYTIMER0 = new List<StatInfo>()
        {
            new StatInfo(){ Hash="_PAYRESUPPLYTIMER0", Value=1 },
            new StatInfo(){ Hash="_PAYRESUPPLYTIMER1", Value=1 },
            new StatInfo(){ Hash="_PAYRESUPPLYTIMER2", Value=1 },
            new StatInfo(){ Hash="_PAYRESUPPLYTIMER3", Value=1 },
            new StatInfo(){ Hash="_PAYRESUPPLYTIMER4", Value=1 },
            new StatInfo(){ Hash="_PAYRESUPPLYTIMER5", Value=1 },
        };

        /// <summary>
        /// 重置载具销售计时
        /// </summary>
        public static List<StatInfo> MPPLY_VEHICLE = new List<StatInfo>()
        {
            new StatInfo(){ Hash="MPPLY_VEHICLE_SELL_TIME", Value=0 },
            new StatInfo(){ Hash="MPPLY_NUM_CARS_SOLD_TODAY", Value=0 },
        };

        /// <summary>
        /// CEO办公室满地钱+小金人
        /// </summary>
        public static List<StatInfo> _LIFETIME_BUY_UNDERTAKEN = new List<StatInfo>()
        {
            new StatInfo(){ Hash="_LIFETIME_BUY_UNDERTAKEN", Value=1000 },
            new StatInfo(){ Hash="_LIFETIME_BUY_COMPLETE", Value=1000 },
            new StatInfo(){ Hash="_LIFETIME_SELL_UNDERTAKEN", Value=10 },
            new StatInfo(){ Hash="_LIFETIME_SELL_COMPLETE", Value=10 },
            new StatInfo(){ Hash="_LIFETIME_CONTRA_EARNINGS", Value=18000000 },
        };

        /// <summary>
        /// 解锁电话联系人功能
        /// </summary>
        public static List<StatInfo> _FM_ACT_PHN = new List<StatInfo>()
        {
            new StatInfo(){ Hash="_FM_ACT_PHN", Value=-1 },
            new StatInfo(){ Hash="_FM_ACT_PH2", Value=-1 },
            new StatInfo(){ Hash="_FM_ACT_PH3", Value=-1 },
            new StatInfo(){ Hash="_FM_ACT_PH4", Value=-1 },
            new StatInfo(){ Hash="_FM_ACT_PH5", Value=-1 },
            new StatInfo(){ Hash="_FM_VEH_TX1", Value=-1 },
            new StatInfo(){ Hash="_FM_ACT_PH6", Value=-1 },
            new StatInfo(){ Hash="_FM_ACT_PH7", Value=-1 },
            new StatInfo(){ Hash="_FM_ACT_PH8", Value=-1 },
            new StatInfo(){ Hash="_FM_ACT_PH9", Value=-1 },
            new StatInfo(){ Hash="_FM_CUT_DONE", Value=-1 },
            new StatInfo(){ Hash="_FM_CUT_DONE_2", Value=-1 },
        };

        /// <summary>
        /// 公寓抢劫直接分红
        /// </summary>
        public static List<StatInfo> _HEIST_PLANNING_STAGE = new List<StatInfo>()
        {
            new StatInfo(){ Hash="_HEIST_PLANNING_STAGE", Value=-1 },
        };

        /// <summary>
        /// 载具节日涂装
        /// </summary>
        public static List<StatInfo> MPPLY_XMASLIVERIES0 = new List<StatInfo>()
        {
            new StatInfo(){ Hash="MPPLY_XMASLIVERIES0", Value=-1 },
            new StatInfo(){ Hash="MPPLY_XMASLIVERIES1", Value=-1 },
            new StatInfo(){ Hash="MPPLY_XMASLIVERIES2", Value=-1 },
            new StatInfo(){ Hash="MPPLY_XMASLIVERIES3", Value=-1 },
            new StatInfo(){ Hash="MPPLY_XMASLIVERIES4", Value=-1 },
            new StatInfo(){ Hash="MPPLY_XMASLIVERIES5", Value=-1 },
            new StatInfo(){ Hash="MPPLY_XMASLIVERIES6", Value=-1 },
            new StatInfo(){ Hash="MPPLY_XMASLIVERIES7", Value=-1 },
            new StatInfo(){ Hash="MPPLY_XMASLIVERIES8", Value=-1 },
            new StatInfo(){ Hash="MPPLY_XMASLIVERIES9", Value=-1 },
            new StatInfo(){ Hash="MPPLY_XMASLIVERIES10", Value=-1 },
            new StatInfo(){ Hash="MPPLY_XMASLIVERIES11", Value=-1 },
            new StatInfo(){ Hash="MPPLY_XMASLIVERIES12", Value=-1 },
            new StatInfo(){ Hash="MPPLY_XMASLIVERIES13", Value=-1 },
            new StatInfo(){ Hash="MPPLY_XMASLIVERIES14", Value=-1 },
            new StatInfo(){ Hash="MPPLY_XMASLIVERIES15", Value=-1 },
            new StatInfo(){ Hash="MPPLY_XMASLIVERIES16", Value=-1 },
            new StatInfo(){ Hash="MPPLY_XMASLIVERIES17", Value=-1 },
            new StatInfo(){ Hash="MPPLY_XMASLIVERIES18", Value=-1 },
            new StatInfo(){ Hash="MPPLY_XMASLIVERIES19", Value=-1 },
            new StatInfo(){ Hash="MPPLY_XMASLIVERIES20", Value=-1 },
        };

        /// <summary>
        /// 解决赌场侦察拍照问题
        /// </summary>
        public static List<StatInfo> _H3OPT_ACCESSPOINTS = new List<StatInfo>()
        {
            new StatInfo(){ Hash="_H3OPT_ACCESSPOINTS", Value=0 },
            new StatInfo(){ Hash="_H3OPT_POI", Value=0 },
        };

        //////////////////////////////////////////////////////////////

        public static List<StatClass> StatDataClass = new List<StatClass>()
        {
            new StatClass(){ ClassName="玩家-护甲全满", StatInfo=_MP_CHAR_ARMOUR },
            new StatClass(){ ClassName="玩家-零食全满", StatInfo=_NO_BOUGHT },
            new StatClass(){ ClassName="玩家-属性全满", StatInfo=_SCRIPT_INCREASE },
            new StatClass(){ ClassName="玩家-隐藏属性全满", StatInfo=_CHAR_ABILITY },
            new StatClass(){ ClassName="玩家-性别修改", StatInfo=_GENDER_CHANGE },
            new StatClass(){ ClassName="玩家-修改等级为1", StatInfo=_CHAR_SET_RP1 },
            new StatClass(){ ClassName="玩家-修改等级为30", StatInfo=_CHAR_SET_RP30 },
            new StatClass(){ ClassName="玩家-修改等级为60", StatInfo=_CHAR_SET_RP60 },
            new StatClass(){ ClassName="玩家-修改等级为90", StatInfo=_CHAR_SET_RP90 },
            new StatClass(){ ClassName="玩家-修改等级为120", StatInfo=_CHAR_SET_RP120 },

            new StatClass(){ ClassName="资产-补满夜总会人气", StatInfo=_CLUB_POPULARITY },
            new StatClass(){ ClassName="资产-地堡和摩托帮自动进货", StatInfo=_PAYRESUPPLYTIMER0 },
            new StatClass(){ ClassName="资产-重置地堡总营收", StatInfo=_LIFETIME_BKR_SELL },

            new StatClass(){ ClassName="重置-赌场抢劫面板重置", StatInfo=_H3OPT },
            new StatClass(){ ClassName="重置-佩里克岛抢劫面板重置", StatInfo=_H4 },
            new StatClass(){ ClassName="重置-重置载具销售计时", StatInfo=MPPLY_VEHICLE },

            new StatClass(){ ClassName="其他-跳过过场动画", StatInfo=_FM_CUT_DONE },
            new StatClass(){ ClassName="其他-CEO办公室满地钱和小金人", StatInfo=_LIFETIME_BUY_UNDERTAKEN },
            new StatClass(){ ClassName="其他-解锁电话联系人功能", StatInfo=_FM_ACT_PHN },
            new StatClass(){ ClassName="其他-载具节日涂装", StatInfo=MPPLY_XMASLIVERIES0 },
            new StatClass(){ ClassName="其他-修改车友会等级为1", StatInfo=_CAR_CLUB_REP },
            new StatClass(){ ClassName="其他-公寓抢劫直接分红", StatInfo=_HEIST_PLANNING_STAGE },

            new StatClass(){ ClassName="问题-解决赌场侦察拍照问题", StatInfo=_H3OPT_ACCESSPOINTS },

        };
    }
}
