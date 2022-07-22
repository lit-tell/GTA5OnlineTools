namespace GTA5OnlineTools.Features.SDK;

public static class Vehicle
{
    public static void Create_Vehicle(uint hash, int[] mod, Vector3 pos)
    {
        Hacks.WriteGA<uint>(Hacks.oVMCreate + 27 + 66, hash);       // 载具哈希值

        Hacks.WriteGA<int>(Hacks.oVMCreate + 27 + 94, 2);           // personal car ownerflag  个人载具拥有者标志
        Hacks.WriteGA<int>(Hacks.oVMCreate + 27 + 95, 14);          // ownerflag  拥有者标志

        Hacks.WriteGA<int>(Hacks.oVMCreate + 27 + 5, -1);           // primary -1 auto 159  主色调
        Hacks.WriteGA<int>(Hacks.oVMCreate + 27 + 6, -1);           // secondary -1 auto 159  副色调

        Hacks.WriteGA<float>(Hacks.oVMCreate + 7 + 0, pos.X);       // 载具坐标x
        Hacks.WriteGA<float>(Hacks.oVMCreate + 7 + 1, pos.Y);       // 载具坐标y
        Hacks.WriteGA<float>(Hacks.oVMCreate + 7 + 2, pos.Z);       // 载具坐标z

        Hacks.WriteGAString(Hacks.oVMCreate + 27 + 1, Guid.NewGuid().ToString()[..8]);    // License plate  车牌

        for (int i = 0; i < 43; i++)
        {
            if (i < 17)
            {
                Hacks.WriteGA<int>(Hacks.oVMCreate + 27 + 10 + i, mod[i]);
            }
            else if (i >= 17 && i != 42)
            {
                Hacks.WriteGA<int>(Hacks.oVMCreate + 27 + 10 + 6 + i, mod[i]);
            }
            else if (mod[42] > 0 && i == 42)
            {
                Hacks.WriteGA<int>(Hacks.oVMCreate + 27 + 10 + 6 + 42, new Random().Next(1, mod[42] + 1));
            }
        }

        Hacks.WriteGA<int>(Hacks.oVMCreate + 27 + 7, -1);       // pearlescent
        Hacks.WriteGA<int>(Hacks.oVMCreate + 27 + 8, -1);       // wheel color
        Hacks.WriteGA<int>(Hacks.oVMCreate + 27 + 33, -1);      // wheel selection
        Hacks.WriteGA<int>(Hacks.oVMCreate + 27 + 69, -1);      // Wheel type

        Hacks.WriteGA<int>(Hacks.oVMCreate + 27 + 28, 1);
        Hacks.WriteGA<int>(Hacks.oVMCreate + 27 + 30, 1);
        Hacks.WriteGA<int>(Hacks.oVMCreate + 27 + 32, 1);
        Hacks.WriteGA<int>(Hacks.oVMCreate + 27 + 65, 1);

        Hacks.WriteGA<uint>(Hacks.oVMCreate + 27 + 77, 0xF0400200);         // vehstate  载具状态 没有这个载具起落架是收起状态

        Hacks.WriteGA<int>(Hacks.oVMCreate + 5, 1);                         // can spawn flag must be odd
        Hacks.WriteGA<int>(Hacks.oVMCreate + 2, 1);                         // spawn toggle gets reset to 0 on car spawn
    }

    public static void Create_Vehicle(long ped, uint hash, int[] mod, float dist = 7.0f, float height = 0.0f)
    {
        Vector3 pos = Ped.Get_Real_Forward_Position(ped, dist);
        pos.Z = height == -225.0f ? height : pos.Z + height;
        Create_Vehicle(hash, mod, pos);
    }
}
