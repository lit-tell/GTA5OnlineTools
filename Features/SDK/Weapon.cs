using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Features.SDK;

public static class Weapon
{
    public static void Infinite_Ammo(bool toggle)
    {
        Memory.WriteBytes(Globals.InfiniteAmmoADDR, toggle ? new byte[] { 0x90, 0x90, 0x90 } : new byte[] { 0x41, 0x2B, 0xD1 });
    }

    public static void No_Reload(bool toggle)
    {
        Memory.WriteBytes(Globals.NoReloadADDR, toggle ? new byte[] { 0x90, 0x90, 0x90 } : new byte[] { 0x41, 0x2B, 0xC9 });
    }

    public static void Fill_Current_Ammo()
    {
        // Ped实体
        long pWeapon_AmmoInfo = Memory.Read<long>(Globals.WorldPTR, Offsets.Weapon.AmmoInfo);

        int getMaxAmmo = Memory.Read<int>(pWeapon_AmmoInfo + 0x28);

        long my_offset_0 = pWeapon_AmmoInfo;
        long my_offset_1;
        byte ammo_type;

        do
        {
            my_offset_0 = Memory.Read<long>(my_offset_0 + 0x08);
            my_offset_1 = Memory.Read<long>(my_offset_0 + 0x00);

            if (my_offset_0 == 0 || my_offset_1 == 0)
            {
                return;
            }

            ammo_type = Memory.Read<byte>(my_offset_1 + 0x0C);

        } while (ammo_type == 0x00);

        Memory.Write<int>(my_offset_1 + 0x18, getMaxAmmo);
    }

    public static void Fill_All_Ammo()
    {
        long p = Ped.Get_Ped_Inventory(Hacks.Get_Local_Ped());
        p = Memory.Read<long>(p + 0x48);
        //for(int i = 0; i < 32; i++)
        //{
        //    long temp = Memory.Read<long>(p + i * 0x08);
        //    if (!Memory.IsValid(temp)) continue;
        //    if (!Memory.IsValid(Memory.Read<long>(temp + 0x08))) continue;
        //    Func<int, int, int> Max = (int a, int b) => { return  a > b ? a : b; };
        //    int max_ammo = Max(Memory.Read<int>(temp + 0x08, new int[] { 0x28 }), Memory.Read<int>(temp + 0x08, new int[] { 0x34 }));
        //    Memory.Write<int>(temp + 0x20, max_ammo);
        //}
        int count = 0;
        while (Memory.Read<int>(p + count * 0x08) != 0 && Memory.Read<int>(p + count * 0x08, new int[] { 0x08 }) != 0)
        {
            Func<int, int, int> Max = (int a, int b) => { return a > b ? a : b; };
            int max_ammo = Max(Memory.Read<int>(p + count * 0x08, new int[] { 0x08, 0x28 }), Memory.Read<int>(p + count * 0x08, new int[] { 0x08, 0x34 }));
            Memory.Write<int>(p + count * 0x08, new int[] { 0x20 }, max_ammo);
            count++;
        }
    }
}
