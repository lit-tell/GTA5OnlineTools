using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Features.SDK;

internal class Locals
{
    public static long LocalAddress(string name)
    {
        for(int i = 0; i < 54; i++)
        {
            long p = Memory.Read<long>(Globals.LocalScriptsPTR);
            p = Memory.Read<long>(p + i * 0x8);
            string str = Memory.ReadString(p + 0xD0, null, name.Length + 1);
            if (str == name) return p + 0xB0;
        }
        return 0;
    }
    public static long LA(string name, int index)
    {
        for (int i = 0; i < 54; i++)
        {
            long p = Memory.Read<long>(Globals.LocalScriptsPTR);
            p = Memory.Read<long>(p + i * 0x8);
            long address = Memory.Read<long>(p + 0xB0);
            string str = Memory.ReadString(p + 0xD0, null, name.Length + 1);
            if (str == name && p != 0) return address + index * 8;
        }
        return 0;
    }
    public static T GL<T>(string name, int index) where T : struct
    {
        return Memory.Read<T>(LA(name, index));
    }
    public static void SL<T>(string name, int index, T value) where T : struct
    {
        Memory.Write<T>(LA(name, index), value);
    }
}
