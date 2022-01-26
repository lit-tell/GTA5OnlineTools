using System.Threading;
using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Features.SDK
{
    public class Hacks
    {
        public static long GlobalAddress(int address)
        {
            return Memory.Read<long>(Globals.GlobalPTR + 0x8 * ((address >> 0x12) & 0x3F)) + 8 * (address & 0x3FFFF);
        }

        public static T ReadGA<T>(int ga) where T : struct
        {
            return Memory.Read<T>(GlobalAddress(ga));
        }

        public static void WriteGA<T>(int ga, T vaule) where T : struct
        {
            Memory.Write<T>(GlobalAddress(ga), vaule);
        }

        public static string ReadGAString(int ga)
        {
            return Memory.ReadString(GlobalAddress(ga), null, 20);
        }

        public static void WriteGAString(int ga, string str)
        {
            Memory.WriteString(GlobalAddress(ga), null, str);
        }

        /*********************************************************/

        public static int GET_NETWORK_TIME()
        {
            return ReadGA<int>(1574747 + 11);
        }
        public static int PLAYER_ID()
        {
            return ReadGA<int>(2703656);
        }

        public static int GetBusinessIndex(int ID)
        {
            // ID 0-5
            return 1853128 + 1 + (PLAYER_ID() * 888) + 267 + 187 + 1 + (ID * 13);
        }

        public static uint Joaat(string input)
        {
            uint num1 = 0U;
            input = input.ToLower();
            foreach (char c in input)
            {
                uint num2 = num1 + c;
                uint num3 = num2 + (num2 << 10);
                num1 = num3 ^ num3 >> 6;
            }
            uint num4 = num1 + (num1 << 3);
            uint num5 = num4 ^ num4 >> 11;

            return num5 + (num5 << 15);
        }

        /// <summary>
        /// 写入stat值，只支持int类型
        /// </summary>
        public static void WriteStat(string hash, int value)
        {
            if (hash.IndexOf("_") == 0)
            {
                int Stat_MP = ReadGA<int>(1574907);
                hash = $"MP{Stat_MP}{hash}";
            }

            uint Stat_ResotreHash = ReadGA<uint>(1655444 + 4);
            int Stat_ResotreValue = ReadGA<int>(1020252 + 5526);

            WriteGA<uint>(1655444 + 4, Joaat(hash));
            WriteGA<int>(1020252 + 5526, value);
            WriteGA<int>(1644209 + 1139, -1);
            Thread.Sleep(1000);
            WriteGA<uint>(1655444 + 4, Stat_ResotreHash);
            WriteGA<int>(1020252 + 5526, Stat_ResotreValue);
        }

        /// <summary>
        /// 掉落物品
        /// </summary>
        public static void CreateAmbientPickup(string pickup)
        {
            uint modelHash = Joaat("prop_cash_pile_01");
            uint pickupHash = Joaat(pickup);

            float x = Memory.Read<float>(Globals.WorldPTR, Offsets.PlayerPositionX);
            float y = Memory.Read<float>(Globals.WorldPTR, Offsets.PlayerPositionY);
            float z = Memory.Read<float>(Globals.WorldPTR, Offsets.PlayerPositionZ);

            WriteGA<int>(2783329 + 1, 0);    // 9999
            WriteGA<float>(2783329 + 3, x);
            WriteGA<float>(2783329 + 4, y);
            WriteGA<float>(2783329 + 5, z + 2.0f);

            WriteGA<int>(4528329 + 1 + (ReadGA<int>(2783329) * 85) + 66 + 2, 2);
            WriteGA<int>(2783335, 1);

            long m_dwpUnkModelBase = Memory.Read<long>(Globals.UnkModelPTR, null);
            long m_dwpUnkModelStruct = Memory.Read<long>(m_dwpUnkModelBase + 0x00, null);
            uint m_dwModelHash = Memory.Read<uint>(m_dwpUnkModelStruct + 0x2640, null);
            if (m_dwModelHash != modelHash)
            {
                Memory.Write<uint>(m_dwpUnkModelStruct + 0x2640, null, modelHash);
            }

            //var ptr_prop_cash_pile_01 = Memory.Read<int>(Globals.ReplayInterfacePTR, new int[] { 0x20, 0xB0, 0x00, 0x490 }) + 0xE80;
            //Memory.Write<uint>(ptr_prop_cash_pile_01, null, modelHash);

            Thread.Sleep(150);

            var m_dwpPickUpInterface = Memory.Read<long>(Globals.ReplayInterfacePTR, new int[] { 0x20 });

            var m_dwpPedList = Memory.Read<long>(m_dwpPickUpInterface + 0x100, null);
            var dw_curPickUpNum = Memory.Read<long>(m_dwpPickUpInterface + 0x110, null);

            for (long i = 0; i < dw_curPickUpNum; i++)
            {
                long dwpPickup, dwpPickupCur;
                uint dwPickupHash, dwModelHash;

                dwpPickup = Memory.Read<long>(m_dwpPedList + i * 0x10, null);

                dwpPickupCur = Memory.Read<long>(dwpPickup + 0x20, null);
                dwModelHash = Memory.Read<uint>(dwpPickupCur + 0x18, null);
                dwPickupHash = Memory.Read<uint>(dwpPickup + 0x488, null);

                if (dwPickupHash != pickupHash && dwModelHash == modelHash)
                {
                    Memory.Write<uint>(dwpPickup + 0x488, pickupHash);
                    break;
                }
            }

            Memory.Write<uint>(m_dwpUnkModelStruct + 0x2640, null, modelHash);

            //Memory.Write<uint>(ptr_prop_cash_pile_01, null, modelHash);
        }
    }
}
