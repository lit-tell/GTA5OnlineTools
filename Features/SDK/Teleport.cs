﻿using GTA5OnlineTools.Features.Core;
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
        Vector3 pos = Hacks.GetBlipPos(new int[] { 8 }, new int[] { 84 });
        if (pos.X == 0.0f && pos.Y == 0.0f && pos.Z == 0.0f) return;
        pos.Z = pos.Z == 20.0f ? -225.0f : pos.Z + 1.0f;
        Hacks.TeleportToCoords(Hacks.GetLocalPed(), pos);
    }

    /// <summary>
    /// 传送到目标点
    /// </summary>
    public static void ToObjective()
    {
        Vector3 pos = Hacks.GetBlipPos(new int[] { 1 }, new int[] { 5, 60, 66 });
        if (pos.X == 0.0f && pos.Y == 0.0f && pos.Z == 0.0f) pos = Hacks.GetBlipPos(new int[] { 1, 225, 427, 478, 501, 523, 556 }, new int[] { 1, 2, 3, 54, 78 });
        if (pos.X == 0.0f && pos.Y == 0.0f && pos.Z == 0.0f) pos = Hacks.GetBlipPos(new int[] { 432, 443 }, new int[] { 59 });
        Hacks.TeleportToCoordsWithCheck(Hacks.GetLocalPed(), pos);
    }

    /// <summary>
    /// 传送到Blips
    /// </summary>
    public static void ToBlip(int[] icons, int[] colors = null)
    {
        Vector3 pos = Hacks.GetBlipPos(icons, colors);
        Hacks.TeleportToCoordsWithCheck(Hacks.GetLocalPed(), pos);
    }
}
