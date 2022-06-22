using GTA5OnlineTools.Features.Core;
using GTA5OnlineTools.Features.Data;

namespace GTA5OnlineTools.Features.SDK;

public class Teleport
{
    /// <summary>
    /// 坐标向前微调
    /// </summary>
    public static void MovingFoward()
    {
        float sin = Memory.Read<float>(Globals.WorldPTR, Offsets.PlayerSin);
        float cos = Memory.Read<float>(Globals.WorldPTR, Offsets.PlayerCos);

        if (Memory.Read<byte>(Globals.WorldPTR, Offsets.InVehicle) == 0)
        {
            float x = Memory.Read<float>(Globals.WorldPTR, Offsets.VehiclePositionX);
            float y = Memory.Read<float>(Globals.WorldPTR, Offsets.VehiclePositionY);

            x += Settings.Forward * cos;
            y += Settings.Forward * sin;

            Memory.Write(Globals.WorldPTR, Offsets.VehiclePositionX, x);
            Memory.Write(Globals.WorldPTR, Offsets.VehiclePositionY, y);

            Memory.Write(Globals.WorldPTR, Offsets.VehicleVisualX, x);
            Memory.Write(Globals.WorldPTR, Offsets.VehicleVisualY, y);
        }
        else
        {
            float x = Memory.Read<float>(Globals.WorldPTR, Offsets.PlayerPositionX);
            float y = Memory.Read<float>(Globals.WorldPTR, Offsets.PlayerPositionY);

            x += Settings.Forward * cos;
            y += Settings.Forward * sin;

            Memory.Write(Globals.WorldPTR, Offsets.PlayerPositionX, x);
            Memory.Write(Globals.WorldPTR, Offsets.PlayerPositionY, y);

            Memory.Write(Globals.WorldPTR, Offsets.PlayerVisualX, x);
            Memory.Write(Globals.WorldPTR, Offsets.PlayerVisualY, y);
        }
    }

    /// <summary>
    /// 传送到导航点
    /// </summary>
    public static void ToWaypoint()
    {
        Types.Vector3 pos = Hacks.GetBlipPos(new int[] { 8 }, new int[] { 84 });
        if (pos.x == 0.0 && pos.y == 0.0 && pos.z == 0.0) return;
        if (pos.z == 20.0) pos.z = (float)-255.0;
        Hacks.TeleportToCoords(Hacks.GetLocalPed(), pos);
    }

    /// <summary>
    /// 传送到目标点
    /// </summary>
    public static void ToObjective()
    {
        Types.Vector3 pos = Hacks.GetBlipPos(new int[] { 1 }, new int[] { 5, 60, 66 });
        if (pos.x == 0.0 && pos.y == 0.0 && pos.z == 0.0) pos = Hacks.GetBlipPos(new int[] { 1, 225, 427, 478, 501, 523, 556 }, new int[] { 1, 2, 3, 54, 78 });
        if (pos.x == 0.0 && pos.y == 0.0 && pos.z == 0.0) pos = Hacks.GetBlipPos(new int[] { 432, 443 }, new int[] { 59 });
        if (pos.x == 0.0 && pos.y == 0.0 && pos.z == 0.0) return;
        Hacks.TeleportToCoords(Hacks.GetLocalPed(), pos);
    }

    /// <summary>
    /// 传送到Blips
    /// </summary>
    public static void ToBlip(int[] icons, int[] colors = null)
    {
        Types.Vector3 pos = Hacks.GetBlipPos(icons, colors);
        if (pos.x == 0.0 && pos.y == 0.0 && pos.z == 0.0) return;
        Hacks.TeleportToCoords(Hacks.GetLocalPed(), pos);
    }
}
