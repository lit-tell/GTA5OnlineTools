using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Features.SDK;

public static class Locals
{
    public static long LocalAddress(string name)
    {
        for (int i = 0; i < 54; i++)
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

    public static string take_casino_script_name = "fm_mission_controller";
    public static int take_casino_script_index = 19652 + 2685;
    public static string casino_mission_life_script_name = "fm_mission_controller";
    public static int casino_mission_life_script_index = 26077 + 1322 + 1;
    public static string vault_door_script_name = "fm_mission_controller";
    public static int vault_door_script_index = 10068 + 7;
    public static string vault_door_total_script_name = "fm_mission_controller";
    public static int vault_door_total_script_index = 10068 + 37;
    public static string casino_fingerprint_script_name = "fm_mission_controller";
    public static int casino_fingerprint_script_index = (0x68CA0 - 0x8) / 8;
    public static string casino_keypad_script_name = "fm_mission_controller";
    public static int casino_keypad_script_index = (0x6ADD0 - 0x8) / 8;

    public static int get_take_casino()
    { return GL<int>(take_casino_script_name, take_casino_script_index); }

    public static void set_take_casino(int value)
    {
        SL<int>(take_casino_script_name, take_casino_script_index, value);
    }

    public static int get_casino_mission_life()
    {
        return GL<int>(casino_mission_life_script_name, casino_mission_life_script_index);
    }

    public static void set_casino_mission_life(int value)
    {
        SL<int>(casino_mission_life_script_name, casino_mission_life_script_index, value);
    }

    public static void vault_door_open()
    {
        int total = GL<int>(vault_door_total_script_name, vault_door_total_script_index);
        SL<int>(vault_door_script_name, vault_door_script_index, total);
    }

    public static void casino_fingerprint_hack()
    {
        SL<int>(casino_fingerprint_script_name, casino_fingerprint_script_index, 11);
    }

    public static void casino_keypad_hack()
    {
        SL<int>(casino_keypad_script_name, casino_keypad_script_index, 11);
    }

    public static string take_cayo_script_name = "fm_mission_controller_2020";
    public static int take_cayo_script_index = 40004 + 1392 + 53;
    public static string cayo_mission_life_script_name = "fm_mission_controller_2020";
    public static int cayo_mission_life_script_index = 43059 + 865 + 1;
    public static string plasma_cutter_progress_script_name = "fm_mission_controller_2020";
    public static int plasma_cutter_progress_script_index = 28269 + 3;
    public static string plasma_cutter_heat_script_name = "fm_mission_controller_2020";
    public static int plasma_cutter_heat_script_index = 28269 + 4;
    public static string cayo_fingerprint_script_name = "fm_mission_controller_2020";
    public static int cayo_fingerprint_script_index = (0x2EBD0 - 0x8) / 8;
    public static string cayo_sewer_cuts_script_name = "fm_mission_controller_2020";
    public static int cayo_sewer_cuts_script_index = 0x34CE0 / 8;

    public static int get_take_cayo()
    {
        return GL<int>(take_cayo_script_name, take_cayo_script_index);
    }

    public static void set_take_cayo(int value)
    {
        SL<int>(take_cayo_script_name, take_cayo_script_index, value);
    }

    public static int get_cayo_mission_life()
    {
        return GL<int>(cayo_mission_life_script_name, cayo_mission_life_script_index);
    }

    public static void set_cayo_mission_life(int value)
    {
        SL<int>(cayo_mission_life_script_name, cayo_mission_life_script_index, value);
    }

    public static void plasma_cutter_finish()
    {
        SL<float>(plasma_cutter_progress_script_name, plasma_cutter_progress_script_index, 100.0f);
    }

    public static float get_plasma_cutter_heat()
    {
        return GL<float>(plasma_cutter_heat_script_name, plasma_cutter_heat_script_index);
    }

    public static void set_plasma_cutter_heat(float value)
    {
        SL<float>(plasma_cutter_heat_script_name, plasma_cutter_heat_script_index, value);
    }

    public static void reset_plasma_cutter_heat()
    {
        SL<float>(plasma_cutter_heat_script_name, plasma_cutter_heat_script_index, 0.0f);
    }

    public static void cayo_fingerprint_hack()
    {
        SL<int>(cayo_fingerprint_script_name, cayo_fingerprint_script_index, 143);
    }

    public static void cayo_sewer_cuts_finish()
    {
        SL<int>(cayo_sewer_cuts_script_name, cayo_sewer_cuts_script_index, 6);
    }

}
