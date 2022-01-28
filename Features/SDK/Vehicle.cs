using System.Threading.Tasks;
using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Features.SDK
{
    public class Vehicle
    {
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
