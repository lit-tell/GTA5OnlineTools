using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Features.SDK;

public static class Player
{
    /// <summary>
    /// 无敌模式
    /// </summary>
    public static void GodMode(bool isEnable)
    {
        if (isEnable)
            Memory.Write<byte>(Globals.WorldPTR, Offsets.Player.GodMode, 0x01);
        else
            Memory.Write<byte>(Globals.WorldPTR, Offsets.Player.GodMode, 0x00);
    }

    /// <summary>
    /// 玩家通缉等级，0x00,0x01,0x02,0x03,0x04,0x05
    /// </summary>
    public static void WantedLevel(byte level)
    {
        Memory.Write<byte>(Globals.WorldPTR, Offsets.Player.Wanted, level);
    }

    /// <summary>
    /// 挂机防踢
    /// </summary>
    public static void AntiAFK(bool isEnable)
    {
        if (isEnable)
        {
            Hacks.WriteGA<int>(262145 + 87, 2000000000);
            Hacks.WriteGA<int>(262145 + 88, 2000000000);
            Hacks.WriteGA<int>(262145 + 89, 2000000000);
            Hacks.WriteGA<int>(262145 + 90, 2000000000);

            Hacks.WriteGA<int>(262145 + 8041, 2000000000);
            Hacks.WriteGA<int>(262145 + 8042, 2000000000);
            Hacks.WriteGA<int>(262145 + 8043, 2000000000);
            Hacks.WriteGA<int>(262145 + 8044, 2000000000);
        }
        else
        {
            Hacks.WriteGA<int>(262145 + 87, 120000);
            Hacks.WriteGA<int>(262145 + 88, 300000);
            Hacks.WriteGA<int>(262145 + 89, 600000);
            Hacks.WriteGA<int>(262145 + 90, 900000);

            Hacks.WriteGA<int>(262145 + 8041, 30000);
            Hacks.WriteGA<int>(262145 + 8042, 60000);
            Hacks.WriteGA<int>(262145 + 8043, 90000);
            Hacks.WriteGA<int>(262145 + 8044, 120000);
        }
    }

    /// <summary>
    /// 无布娃娃
    /// </summary>
    public static void NoRagdoll(bool isEnable)
    {
        if (isEnable)
            Memory.Write<byte>(Globals.WorldPTR, Offsets.Player.NoRagdoll, 0x01);
        else
            Memory.Write<byte>(Globals.WorldPTR, Offsets.Player.NoRagdoll, 0x20);
    }

    /// <summary>
    /// 无碰撞体积
    /// </summary>
    public static void NoCollision(bool isEnable)
    {
        if (isEnable)
            Memory.Write(Globals.WorldPTR, Offsets.Player.NoCollision, -1.0f);
        else
            Memory.Write(Globals.WorldPTR, Offsets.Player.NoCollision, 0.25f);
    }

    /// <summary>
    /// 角色水下行走
    /// </summary>
    public static void WaterProof(bool isEnable)
    {
        long waterProof = Memory.Read<long>(Globals.WorldPTR, Offsets.PlayerWaterProof);
        waterProof %= 0x1000000;

        if (isEnable)
            Memory.Write<long>(Globals.WorldPTR, Offsets.PlayerWaterProof, waterProof + 0x1000000);
        else
            Memory.Write<long>(Globals.WorldPTR, Offsets.PlayerWaterProof, waterProof);
    }

    /// <summary>
    /// 角色隐形
    /// </summary>
    public static void Invisibility(bool isEnable)
    {
        if (isEnable)
            Memory.Write<byte>(Globals.WorldPTR, Offsets.Player.Invisibility, 0x01);
        else
            Memory.Write<byte>(Globals.WorldPTR, Offsets.Player.Invisibility, 0x27);
    }

    /// <summary>
    /// 补满血量和护甲
    /// </summary>
    public static void FillHealthArmor()
    {
        Memory.Write(Globals.WorldPTR, Offsets.Player.Health, 328.0f);
        Memory.Write(Globals.WorldPTR, Offsets.Player.MaxHealth, 328.0f);
        Memory.Write(Globals.WorldPTR, Offsets.Player.Armor, 50.0f);
    }

    /// <summary>
    /// 玩家自杀
    /// </summary>
    public static void Suicide()
    {
        Memory.Write(Globals.WorldPTR, Offsets.Player.Health, 1.0f);
    }

    /// <summary>
    /// 雷达影踪（最大生命值为0）
    /// </summary>
    public static void UndeadOffRadar(bool isEnable)
    {
        if (isEnable)
            Memory.Write<float>(Globals.WorldPTR, Offsets.Player.MaxHealth, 0.0f);
        else
            Memory.Write<float>(Globals.WorldPTR, Offsets.Player.MaxHealth, 328.0f);
    }

    /// <summary>
    /// 永不通缉
    /// </summary>
    public static void WantedCanChange(bool isEnable)
    {
        if (isEnable)
            Memory.Write<float>(Globals.WorldPTR, Offsets.WantedCanChange, 1.0f);
        else
            Memory.Write<float>(Globals.WorldPTR, Offsets.WantedCanChange, 0.0f);
    }
}
