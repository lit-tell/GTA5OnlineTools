using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Features.SDK;

public static class World
{
    /// <summary>
    /// 设置本地天气
    /// </summary>
    /// <param name="weatherID">天气ID</param>
    public static void Set_Local_Weather(int weatherID)
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

        if (weatherID == -1)
        {
            Memory.Write(Globals.WeatherPTR + 0x24, -1);
            Memory.Write(Globals.WeatherPTR + 0x104, 13);
        }
        if (weatherID == 13)
        {
            Memory.Write(Globals.WeatherPTR + 0x24, 13);
            Memory.Write(Globals.WeatherPTR + 0x104, 13);
        }

        Memory.Write(Globals.WeatherPTR + 0x104, weatherID);
    }

    public static void Kill_Npcs()
    {
        List<long> peds = Replayinterface.Get_Peds();

        for (int i = 0; i < peds.Count; i++)
        {
            long ped = peds[i];

            if (Ped.Is_Player(ped)) 
                continue;

            Ped.Set_Health(ped, 0.0f);
        }
    }

    public static void Kill_Enemies()
    {
        List<long> peds = Replayinterface.Get_Peds();

        for (int i = 0; i < peds.Count; i++)
        {
            long ped = peds[i];

            if (Ped.Is_Player(ped)) 
                continue;

            if (Ped.Is_Enemy(ped)) 
                Ped.Set_Health(ped, 0.0f);
        }
    }

    public static void Kill_Cops()
    {
        List<long> peds = Replayinterface.Get_Peds();

        for (int i = 0; i < peds.Count; i++)
        {
            long ped = peds[i];

            if (Ped.Is_Player(ped)) 
                continue;

            uint pedtype = Ped.Get_Pedtype(ped);
            if (pedtype == (uint)Data.EnumData.PedTypes.COP ||
                pedtype == (uint)Data.EnumData.PedTypes.SWAT ||
                pedtype == (uint)Data.EnumData.PedTypes.ARMY) 
                Ped.Set_Health(ped, 0.0f);
        }
    }

    public static void Revive_Vehicle(long vehicle)
    {
        Vehicle.Set_State_Is_Destroyed(vehicle, false);
        Vehicle.Set_Health(vehicle, 1000.0f);
        Vehicle.Set_Health2(vehicle, 1000.0f);
        Vehicle.Set_Health3(vehicle, 1000.0f);
        Vehicle.Set_Engine_Health(vehicle, 1000.0f);
    }

    public static void Destroy_Vehicle(long vehicle)
    {
        Revive_Vehicle(vehicle);
        //Vehicle.set_health(vehicle, 0.0f);
        //Vehicle.set_health2(vehicle, 0.0f);
        Vehicle.Set_Health3(vehicle, -999.9f);//-1000.0f
        //Vehicle.set_engine_health(vehicle, -3999.0f);//-4000.0f
    }

    public static void Destroy_Vehs_Of_Npcs()
    {
        List<long> peds = Replayinterface.Get_Peds();

        for (int i = 0; i < peds.Count; i++)
        {
            long ped = peds[i];

            if (Ped.Is_Player(ped)) 
                continue;

            Destroy_Vehicle(Ped.Get_Current_Vehicle(ped));
        }
    }

    public static void Destroy_Vehs_Of_Enemies()
    {
        List<long> peds = Replayinterface.Get_Peds();

        for (int i = 0; i < peds.Count; i++)
        {
            long ped = peds[i];

            if (ped == Hacks.Get_Local_Ped()) 
                continue;

            if (Ped.Is_Enemy(ped)) 
                Destroy_Vehicle(Ped.Get_Current_Vehicle(ped));
        }
    }

    public static void Destroy_Vehs_Of_Cops()
    {
        List<long> peds = Replayinterface.Get_Peds();

        for (int i = 0; i < peds.Count; i++)
        {
            long ped = peds[i];

            if (ped == Hacks.Get_Local_Ped()) 
                continue;

            uint pedtype = Ped.Get_Pedtype(ped);
            if (pedtype == (uint)Data.EnumData.PedTypes.COP ||
                pedtype == (uint)Data.EnumData.PedTypes.SWAT ||
                pedtype == (uint)Data.EnumData.PedTypes.ARMY) 
                Destroy_Vehicle(Ped.Get_Current_Vehicle(ped));
        }
    }

    /// <summary>
    /// 摧毁附近所有载具
    /// </summary>
    public static void Destroy_All_Vehicles()
    {
        List<long> vehicles = Replayinterface.Get_Vehicles();

        for (int i = 0; i < vehicles.Count; i++)
        {
            long vehicle = vehicles[i];
            Destroy_Vehicle(vehicle);
        }
    }

    /// <summary>
    /// 复活附近所有载具
    /// </summary>
    public static void Revive_All_Vehicles()
    {
        List<long> vehicles = Replayinterface.Get_Vehicles();

        for (int i = 0; i < vehicles.Count; i++)
        {
            long vehicle = vehicles[i];
            Revive_Vehicle(vehicle);
        }
    }

    public static void Tp_Npcs_To_Me()
    {
        List<long> peds = Replayinterface.Get_Peds();

        for (int i = 0; i < peds.Count; i++)
        {
            long ped = peds[i];

            if (Ped.Is_Player(ped))
                continue;

            Ped.Set_Position(ped, Ped.Get_Real_Forward_Position(Get_Local_Ped(), 5.0f));
        }
    }

    public static void Tp_Enemies_To_Me()
    {
        List<long> peds = Replayinterface.Get_Peds();

        for (int i = 0; i < peds.Count; i++)
        {
            long ped = peds[i];

            if (Ped.Is_Player(ped)) 
                continue;

            if (Ped.Is_Enemy(ped)) 
                Ped.Set_Position(ped, Ped.Get_Real_Forward_Position(Get_Local_Ped(), 5.0f));
        }
    }

    public static void Tp_Not_Enemies_To_Me()
    {
        List<long> peds = Replayinterface.Get_Peds();

        for (int i = 0; i < peds.Count; i++)
        {
            long ped = peds[i];

            if (Ped.Is_Player(ped)) 
                continue;

            if (!Ped.Is_Enemy(ped)) 
                Ped.Set_Position(ped, Ped.Get_Real_Forward_Position(Get_Local_Ped(), 5.0f));
        }
    }

    public static void Tp_Cops_To_Me()
    {
        List<long> peds = Replayinterface.Get_Peds();
        for (int i = 0; i < peds.Count; i++)
        {
            long ped = peds[i];

            if (Ped.Is_Player(ped)) 
                continue;

            uint pedtype = Ped.Get_Pedtype(ped);
            if (pedtype == (uint)Data.EnumData.PedTypes.COP ||
                pedtype == (uint)Data.EnumData.PedTypes.SWAT ||
                pedtype == (uint)Data.EnumData.PedTypes.ARMY) 
                Ped.Set_Position(ped, Ped.Get_Real_Forward_Position(Get_Local_Ped(), 5.0f));
        }
    }
}
