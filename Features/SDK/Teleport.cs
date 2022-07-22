using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Features.SDK;

public static class Teleport
{
    public static long Get_Blip(int[] icons, int[] colors = null)
    {
        for (int i = 1; i < 2001; i++)
        {
            long p = Memory.Read<long>(Globals.BlipPTR + i * 0x8);
            if (p == 0) continue;
            int icon = Memory.Read<int>(p + 0x40);
            int color = Memory.Read<int>(p + 0x48);
            if (Array.IndexOf(icons, icon) == -1) continue;
            if (colors != null && Array.IndexOf(colors, color) == -1) continue;
            return p;
        }
        return 0;
    }

    public static Vector3 Get_Blip_Pos(int[] icons, int[] colors = null)
    {
        long blip = Get_Blip(icons, colors);
        return ((blip == 0) ? new Vector3() : Memory.Read<Vector3>(blip + 0x10));
    }


    /// <summary>
    /// 传送到导航点
    /// </summary>
    public static void To_Waypoint()
    {
        Vector3 pos = Get_Blip_Pos(new int[] { 8 }, new int[] { 84 });
        if (pos.X == 0.0f && pos.Y == 0.0f && pos.Z == 0.0f) return;
        pos.Z = pos.Z == 20.0f ? -255.0f : pos.Z + 1.0f;
        To_Coords(Hacks.Get_Local_Ped(), pos);
    }

    public static void To_Objective()
    {
        Vector3 pos = Get_Blip_Pos(new int[] { 1 }, new int[] { 5, 60, 66 });
        if (pos.X == 0.0f && pos.Y == 0.0f && pos.Z == 0.0f) pos = Get_Blip_Pos(new int[] { 1, 225, 427, 478, 501, 523, 556 }, new int[] { 1, 2, 3, 54, 78 });
        if (pos.X == 0.0f && pos.Y == 0.0f && pos.Z == 0.0f) pos = Get_Blip_Pos(new int[] { 432, 443 }, new int[] { 59 });
        To_Coords_With_Check(Hacks.Get_Local_Ped(), pos);
    }

    public static void To_Blip(int[] icons, int[] colors = null)
    {
        Vector3 pos = Get_Blip_Pos(icons, colors);
        To_Coords_With_Check(Hacks.Get_Local_Ped(), pos);
    }

    public static void To_Coords(long ped, Vector3 pos)
    {
        long entity = (Ped.Is_In_Vehicle(ped) ? Ped.Get_Current_Vehicle(ped) : ped);
        Entity.Set_Position(entity, pos);
    }

    public static void To_Coords_With_Check(long ped, Vector3 pos)
    {
        if (pos.X == 0.0f && pos.Y == 0.0f && pos.Z == 0.0f) return;
        To_Coords(ped, pos);
    }
}
