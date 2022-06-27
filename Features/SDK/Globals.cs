using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Features.SDK;

public class Globals
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

    public static long GA(int index) { return Memory.Read<long>(GlobalPTR + 0x8 * ((index >> 0x12) & 0x3F)) + 8 * (index & 0x3FFFF); }

    public static T GG<T>(int index) where T : struct { return Memory.Read<T>(GA(index));}

    public static void SG<T>(int index, T vaule) where T : struct { Memory.Write<T>(GA(index), vaule); }

    public static string ReadGAString(int index) { return Memory.ReadString(GA(index), null, 20); }

    public static void WriteGAString(int index, string str) { Memory.WriteString(GA(index), null, str); }

    public static int anti_afk_index_1 = 262145 + 87;
    public static int anti_afk_index_2 = 262145 + 88;
    public static int anti_afk_index_3 = 262145 + 89;
    public static int anti_afk_index_4 = 262145 + 90;
    public static int anti_afk_index_5 = 262145 + 8041;
    public static int anti_afk_index_6 = 262145 + 8042;
    public static int anti_afk_index_7 = 262145 + 8043;
    public static int anti_afk_index_8 = 262145 + 8044;
    public static void anti_afk(bool toggle)
    {
        if(toggle)
        {
            SG<int>(anti_afk_index_1, 2000000000);//99999999
            SG<int>(anti_afk_index_2, 2000000000);
            SG<int>(anti_afk_index_3, 2000000000);
            SG<int>(anti_afk_index_4, 2000000000);
            SG<int>(anti_afk_index_5, 2000000000);
            SG<int>(anti_afk_index_6, 2000000000);
            SG<int>(anti_afk_index_7, 2000000000);
            SG<int>(anti_afk_index_8, 2000000000);
        }
        else
        {
            SG<int>(anti_afk_index_1, 120000);
            SG<int>(anti_afk_index_2, 300000);
            SG<int>(anti_afk_index_3, 600000);
            SG<int>(anti_afk_index_4, 900000);
            SG<int>(anti_afk_index_5, 60000);
            SG<int>(anti_afk_index_6, 90000);
            SG<int>(anti_afk_index_7, 120000);
            SG<int>(anti_afk_index_8, 30000);
        }
    }
}
