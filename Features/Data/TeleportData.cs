using System.Numerics;
using System.Collections.Generic;

namespace GTA5OnlineTools.Features.Data
{
    public class TeleportData
    {
        public class TeleportInfo
        {
            public string TClass { get; set; }
            public List<TeleportPreview> TInfo { get; set; }
        }

        public class TeleportPreview
        {
            public string TName { get; set; }
            public Vector3 TCode { get; set; }
        }

        public static List<TeleportPreview> CommonTeleport = new List<TeleportPreview>()
        {
            new TeleportPreview(){ TName = "洛圣都改车王", TCode = new Vector3 { X=-365.425f, Y=-131.809f, Z=-225.0f } },
            new TeleportPreview(){ TName = "洛圣都机场", TCode = new Vector3 { X=-1336.0f, Y=-3044.0f, Z=-225.0f } },
            new TeleportPreview(){ TName = "赛诺拉沙漠机场", TCode = new Vector3 { X=1747.0f, Y=3273.0f, Z=-225.0f } },
            new TeleportPreview(){ TName = "乞力耶德山", TCode = new Vector3 { X=489.979f, Y=5587.527f, Z=794.3f } },
        };

        public static List<TeleportPreview> Indoor = new List<TeleportPreview>()
        {
            new TeleportPreview(){ TName = "FIB大楼楼顶", TCode = new Vector3 { X=136.0f, Y=-750.0f, Z=262.0f } },
            new TeleportPreview(){ TName = "服装厂", TCode = new Vector3 { X=712.716f, Y=-962.906f, Z=30.6f } },
            new TeleportPreview(){ TName = "富兰克林家", TCode = new Vector3 { X=7.119f, Y=536.615f, Z=176.2f } },
            new TeleportPreview(){ TName = "麦克家", TCode = new Vector3 { X=-813.603f, Y=179.474f, Z=72.5f } },
            new TeleportPreview(){ TName = "崔佛家", TCode = new Vector3 { X=1972.610f, Y=3817.040f, Z=33.65f } },
            new TeleportPreview(){ TName = "丹尼斯阿姨家", TCode = new Vector3 { X=-14.380f, Y=-1438.510f, Z=31.3f } },
            new TeleportPreview(){ TName = "弗洛伊德家", TCode = new Vector3 { X=-1151.770f, Y=-1518.138f, Z=10.85f } },
            new TeleportPreview(){ TName = "莱斯特家", TCode = new Vector3 { X=1273.898f, Y=-1719.304f, Z=54.8f } },
            new TeleportPreview(){ TName = "脱衣舞俱乐部", TCode = new Vector3 { X=97.271f, Y=-1290.994f, Z=29.45f } },
            new TeleportPreview(){ TName = "银行金库（太平洋标准）", TCode = new Vector3 { X=255.85f, Y=217.0f, Z=101.9f } },
            new TeleportPreview(){ TName = "喜剧俱乐部", TCode = new Vector3 { X=378.100f, Y=-999.964f, Z=-98.6f } },
            new TeleportPreview(){ TName = "人道实验室", TCode = new Vector3 { X=3614.394f, Y=3744.803f, Z=28.9f } },
            new TeleportPreview(){ TName = "人道实验室地道", TCode = new Vector3 { X=3525.201f, Y=3709.625f, Z=21.2f } },
            new TeleportPreview(){ TName = "IAA办公室", TCode = new Vector3 { X=113.568f, Y=-619.001f, Z=206.25f } },
            new TeleportPreview(){ TName = "刑讯室", TCode = new Vector3 { X=142.746f, Y=-2201.189f, Z=4.9f } },
            new TeleportPreview(){ TName = "军事基地高塔", TCode = new Vector3 { X=-2358.132f, Y=3249.754f, Z=101.65f } },
            new TeleportPreview(){ TName = "矿井", TCode = new Vector3 { X=-595.342f, Y=2086.008f, Z=131.6f } },
        };

        public static List<TeleportPreview> CustomTeleport = new List<TeleportPreview>()
        {
            //new TeleportPreview(){ TName = "坐标原点", TCode = new Vector3 { X=0.000f, Y=0.000f, Z=72.0f } },
        };

        public static List<TeleportInfo> TeleportDataClass = new List<TeleportInfo>()
        {
            new TeleportInfo(){ TClass = "常用地点", TInfo = CommonTeleport },
            new TeleportInfo(){ TClass = "室内", TInfo = Indoor },
            new TeleportInfo(){ TClass = "自定义地点", TInfo = CustomTeleport },
        };
    }
}
