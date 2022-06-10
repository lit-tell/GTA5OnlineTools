using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Features.SDK;

internal class Locals
{
    public static long LocalAddress(string name)
    {
        for(int i = 0; i < 53; i++)
        {
            long p = Memory.Read<long>(Globals.LocalScriptsPTR);
            p = Memory.Read<long>(p + i * 0x8);
            string str = Memory.ReadString(p + 0xD0, null, name.Length);
            if (str == name) return p + 0xB0;
        }
        return 0;
    }
}
