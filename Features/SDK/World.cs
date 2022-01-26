using System.Numerics;
using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Features.SDK
{
    public class World
    {
        /// <summary>
        /// 设置天气
        /// </summary>
        /// <param name="weatherID"></param>
        public static void SetLocalWeather(int weatherID)
        {
            /*
             -1:Default
             0:Extra Sunny
             1:Clear
             2:Clouds
             3:Smog
             4:Foggy
             5:Overcast
             6:Rain
             7:Thunder
             8:Light Rain
             9:Smoggy Light Rain
             10:Very Light Snow
             11:Windy Snow
             12:Light Snow
             14:Halloween
             */

            //Memory.Write(Common.WeatherPTR, weatherID);
            //Memory.Write(Common.WeatherPTR + 0x04, weatherID);

            Memory.Write(Globals.WeatherPTR + 0x104, weatherID);
        }

        /// <summary>
        /// 杀死NPC
        /// </summary>
        public static void KillNPC(bool isOnlyHostility)
        {
            // Ped实体
            long pReplayInterfacePTR = Memory.Read<long>(Globals.ReplayInterfacePTR);
            long my_offset_0x18 = Memory.Read<long>(pReplayInterfacePTR + 0x18);

            for (int i = 0; i < 128; i++)
            {
                long ped_offset_0 = Memory.Read<long>(my_offset_0x18 + 0x100);
                ped_offset_0 = Memory.Read<long>(ped_offset_0 + i * 0x10);
                if (ped_offset_0 == 0)
                {
                    continue;
                }

                long ped_offset_1 = Memory.Read<long>(ped_offset_0 + 0x10C8);
                long pedRID = Memory.Read<long>(ped_offset_1 + 0x90);
                if (pedRID != 0)
                {
                    continue;
                }

                if (isOnlyHostility)
                {
                    byte oHostility = Memory.Read<byte>(ped_offset_0 + 0x18C);

                    if (oHostility > 0x01)
                    {
                        Memory.Write<float>(ped_offset_0 + 0x280, 0.0f);
                    }
                }
                else
                {
                    Memory.Write<float>(ped_offset_0 + 0x280, 0.0f);
                }
            }
        }

        /// <summary>
        /// 摧毁NPC载具
        /// </summary>
        public static void DestroyNPCVehicles(bool isOnlyHostility)
        {
            // Ped实体
            long pReplayInterfacePTR = Memory.Read<long>(Globals.ReplayInterfacePTR);
            long my_offset_0x18 = Memory.Read<long>(pReplayInterfacePTR + 0x18);

            for (int i = 0; i < 128; i++)
            {
                long ped_offset_0 = Memory.Read<long>(my_offset_0x18 + 0x100);
                ped_offset_0 = Memory.Read<long>(ped_offset_0 + i * 0x10);
                if (ped_offset_0 == 0)
                {
                    continue;
                }

                long ped_offset_1 = Memory.Read<long>(ped_offset_0 + 0x10C8);
                long pedRID = Memory.Read<long>(ped_offset_1 + 0x90);
                if (pedRID != 0)
                {
                    continue;
                }

                long ped_offset_2 = Memory.Read<long>(ped_offset_0 + 0xD30);

                if (isOnlyHostility)
                {
                    byte oHostility = Memory.Read<byte>(ped_offset_0 + 0x18C);

                    if (oHostility > 0x01)
                    {
                        Memory.Write<float>(ped_offset_2 + 0x280, -1.0f);
                        Memory.Write<float>(ped_offset_2 + 0x840, -1.0f);
                        Memory.Write<float>(ped_offset_2 + 0x844, -1.0f);
                        Memory.Write<float>(ped_offset_2 + 0x908, -1.0f);
                    }
                }
                else
                {
                    Memory.Write<float>(ped_offset_2 + 0x280, -1.0f);
                    Memory.Write<float>(ped_offset_2 + 0x840, -1.0f);
                    Memory.Write<float>(ped_offset_2 + 0x844, -1.0f);
                    Memory.Write<float>(ped_offset_2 + 0x908, -1.0f);
                }
            }
        }

        /// <summary>
        /// 摧毁全部载具
        /// </summary>
        public static void DestroyAllVehicles()
        {
            // Ped实体
            long pReplayInterfacePTR = Memory.Read<long>(Globals.ReplayInterfacePTR);
            long my_offset_0x10 = Memory.Read<long>(pReplayInterfacePTR + 0x10);

            for (int i = 0; i < 128; i++)
            {
                long ped_offset_0 = Memory.Read<long>(my_offset_0x10 + 0x180);
                ped_offset_0 = Memory.Read<long>(ped_offset_0 + i * 0x10);
                if (ped_offset_0 == 0)
                {
                    continue;
                }

                Memory.Write<float>(ped_offset_0 + 0x280, -1.0f);
                Memory.Write<float>(ped_offset_0 + 0x840, -1.0f);
                Memory.Write<float>(ped_offset_0 + 0x844, -1.0f);
                Memory.Write<float>(ped_offset_0 + 0x908, -1.0f);
            }
        }

        /// <summary>
        /// 传送NPC到我这里
        /// </summary>
        public static void TeleportNPCToMe(bool isOnlyFriendly)
        {
            Vector3 v3MyPos = Memory.Read<Vector3>(Globals.WorldPTR, Offsets.PlayerPositionX);

            // Ped实体
            long pReplayInterfacePTR = Memory.Read<long>(Globals.ReplayInterfacePTR);
            long my_offset_0x18 = Memory.Read<long>(pReplayInterfacePTR + 0x18);

            for (int i = 0; i < 128; i++)
            {
                long ped_offset_0 = Memory.Read<long>(my_offset_0x18 + 0x100);
                ped_offset_0 = Memory.Read<long>(ped_offset_0 + i * 0x10);
                if (ped_offset_0 == 0)
                {
                    continue;
                }

                long ped_offset_1 = Memory.Read<long>(ped_offset_0 + 0x10C8);
                long pedRID = Memory.Read<long>(ped_offset_1 + 0x90);
                if (pedRID != 0)
                {
                    continue;
                }

                long ped_offset_2 = Memory.Read<long>(ped_offset_0 + 0x30);

                if (isOnlyFriendly)
                {
                    byte oHostility = Memory.Read<byte>(ped_offset_0 + 0x18C);

                    if (oHostility > 0x01)
                    {
                        Memory.Write<Vector3>(ped_offset_2 + 0x50, v3MyPos);
                        Memory.Write<Vector3>(ped_offset_0 + 0x90, v3MyPos);
                    }
                }
                else
                {
                    Memory.Write<Vector3>(ped_offset_2 + 0x50, v3MyPos);
                    Memory.Write<Vector3>(ped_offset_0 + 0x90, v3MyPos);
                }
            }
        }
    }
}
