using System;
using System.Threading.Tasks;
using GTA5OnlineTools.Features.Core;
using static GTA5OnlineTools.Features.SDK.Hacks;

namespace GTA5OnlineTools.Features.SDK
{
    public class Vehicle
    {
        /// <summary>
        /// 刷出线上载具
        /// </summary>
        public static void SpawnVehicle(string hash, int dist, float z255, int[] mod)
        {
            uint Hash = Joaat(hash);

            //WriteGA<bool>(4269479, true);

            float x = Memory.Read<float>(Globals.WorldPTR, Offsets.PlayerPositionX);
            float y = Memory.Read<float>(Globals.WorldPTR, Offsets.PlayerPositionY);
            float z = Memory.Read<float>(Globals.WorldPTR, Offsets.PlayerPositionZ);
            float sin = Memory.Read<float>(Globals.WorldPTR, Offsets.PlayerSin);
            float cos = Memory.Read<float>(Globals.WorldPTR, Offsets.PlayerCos);

            x += cos * dist;
            y += sin * dist;

            WriteGA<long>(2725260 + 27 + 66, Convert.ToInt64(Hash));
            WriteGA<int>(2725260 + 27 + 94, 2);    // personal car ownerflag
            WriteGA<int>(2725260 + 27 + 95, 14);   // ownerflag
            WriteGA<int>(2725260 + 27 + 5, -1);    // primary -1 auto 159
            WriteGA<int>(2725260 + 27 + 6, -1);    // secondary -1 auto 159
            WriteGA<float>(2725260 + 7 + 0, x);
            WriteGA<float>(2725260 + 7 + 1, y);

            if (z255 == -255.0f)
            {
                WriteGA<float>(2725260 + 7 + 2, -255.0f);   // z -255.0f
            }
            else
            {
                WriteGA<float>(2725260 + 7 + 2, z + z255);   // z -255.0f
            }

            for (int i = 0; i < 42; i++)
            {
                if (i < 17)
                {
                    WriteGA<int>(2725260 + 27 + 10 + i, mod[i]);
                }
                else if (mod[42] > 0 && i == 42)
                {
                    WriteGA<int>(2725260 + 27 + 10 + 6 + i, new Random().Next() % mod[i] + 1);
                }
                else if (i > 22)
                {
                    WriteGA<int>(2725260 + 27 + 10 + 6 + i, mod[i]);
                }
                else
                {
                    continue;
                }
            }

            WriteGA<int>(2725260 + 27 + 28, 1);           // weaponised ownerflag
            WriteGA<int>(2725260 + 27 + 30, 1);
            WriteGA<int>(2725260 + 27 + 32, 1);
            WriteGA<int>(2725260 + 27 + 65, 1);
            WriteGA<long>(2725260 + 27 + 77, 0xF04000A1); // vehstate

            WriteGA<int>(2725260 + 5, 1);      // can spawn flag must be odd
            WriteGA<int>(2725260 + 2, 1);      // spawn toggle gets reset to 0 on car spawn
        }

        /// <summary>
        /// 载具无敌模式
        /// </summary>
        public static void GodMode(bool isEnable)
        {
            if (isEnable)
            {
                Memory.Write<byte>(Globals.WorldPTR, Offsets.Vehicle.GodMode, 0x01);
            }
            else
            {
                Memory.Write<byte>(Globals.WorldPTR, Offsets.Vehicle.GodMode, 0x00);
            }
        }

        /// <summary>
        /// 载具安全带
        /// </summary>
        public static void Seatbelt(bool isEnable)
        {
            if (isEnable)
            {
                Memory.Write<byte>(Globals.WorldPTR, Offsets.Player.Seatbelt, 0xC9);
            }
            else
            {
                Memory.Write<byte>(Globals.WorldPTR, Offsets.Player.Seatbelt, 0xC8);
            }
        }

        /// <summary>
        /// 载具隐形
        /// </summary>
        public static void Invisibility(bool isEnable)
        {
            if (isEnable)
            {
                Memory.Write<byte>(Globals.WorldPTR, Offsets.Vehicle.Invisibility, 0x01);
            }
            else
            {
                Memory.Write<byte>(Globals.WorldPTR, Offsets.Vehicle.Invisibility, 0x27);
            }
        }

        /// <summary>
        /// 载具附加功能，默认0，跳跃40，加速66，二者都96
        /// </summary>
        public static void Extras(byte flag)
        {
            Memory.Write<byte>(Globals.WorldPTR, Offsets.Vehicle.Extras, flag);
        }

        /// <summary>
        /// 载具降落伞，关0，开1
        /// </summary>
        public static void Parachute(bool isEnable)
        {
            if (isEnable)
            {
                Memory.Write<byte>(Globals.WorldPTR, Offsets.Vehicle.Parachute, 1);
            }
            else
            {
                Memory.Write<byte>(Globals.WorldPTR, Offsets.Vehicle.Parachute, 0);
            }
        }

        /// <summary>
        /// 修复载具外观
        /// </summary>
        public static void Fix1stfoundBST()
        {
            Task.Run(() =>
            {
                Memory.Write<int>(Globals.GlobalPTR + 0x08 * 0x0A, new int[] { 0x172908 }, 1);
                Memory.Write<float>(Globals.WorldPTR, new int[] { 0x08, 0xD30, 0x280 }, 999.0f);

                Task.Delay(300).Wait();

                int FixVehValue = Memory.Read<int>(Globals.PickupDataPTR, new int[] { 0x228 });
                int BSTValue = Memory.Read<int>(Globals.PickupDataPTR, new int[] { 0x160 });

                long m_dwpPickUpInterface = Memory.Read<long>(Globals.ReplayInterfacePTR, new int[] { 0x20 });
                long dw_curPickUpNum = Memory.Read<long>(m_dwpPickUpInterface + 0x110, null);
                long m_dwpPedList = Memory.Read<long>(m_dwpPickUpInterface + 0x100, null);

                for (long i = 0; i < dw_curPickUpNum; i++)
                {
                    long dwpPickup = Memory.Read<long>(m_dwpPedList + i * 0x10, null);
                    int dwPickupValue = Memory.Read<int>(dwpPickup + 0x490, null);

                    if (dwPickupValue == BSTValue)
                    {
                        Memory.Write<int>(dwpPickup + 0x490, FixVehValue);

                        Task.Delay(10).Wait();

                        float dwpPickupX = Memory.Read<float>(dwpPickup + 0x90, null);
                        float dwpPickupY = Memory.Read<float>(dwpPickup + 0x94, null);
                        float dwpPickupZ = Memory.Read<float>(dwpPickup + 0x98, null);

                        float Vehx = Memory.Read<float>(Globals.WorldPTR, Offsets.VehicleVisualX);
                        float Vehy = Memory.Read<float>(Globals.WorldPTR, Offsets.VehicleVisualY);
                        float Vehz = Memory.Read<float>(Globals.WorldPTR, Offsets.VehicleVisualZ);

                        Task.Delay(10).Wait();

                        Memory.Write<float>(dwpPickup + 0x90, Vehx);
                        Memory.Write<float>(dwpPickup + 0x94, Vehy);
                        Memory.Write<float>(dwpPickup + 0x98, Vehz);

                        Memory.Write<float>(Globals.WorldPTR, new int[] { 0x08, 0xD30, 0x9F8 }, 0);
                    }
                }

                Task.Delay(500).Wait();

                int BST = Memory.Read<int>(Globals.GlobalPTR + 0x08 * 0x0A, new int[] { 0xA7970 });
                if (BST != 0)
                {
                    Memory.Write<int>(Globals.GlobalPTR + 0x08 * 0x0A, new int[] { 0xA7970 }, -1);
                }

                Task.Delay(50).Wait();

                Online.InstantBullShark(false);
            });
        }

        /// <summary>
        /// 补满载具血量
        /// </summary>
        public static void FillHealth()
        {
            Memory.Write<float>(Globals.WorldPTR, Offsets.Vehicle.Health, 1000.0f);
        }
    }
}
