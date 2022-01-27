﻿using System;

namespace GTA5OnlineTools.Features.Data
{
    public class EnumData
    {
        public enum PedType : int
        {
            PEDTYPE_PLAYER_0,               // michael
            PEDTYPE_PLAYER_1,               // franklin
            PEDTYPE_NETWORK_PLAYER,         // mp character
            PEDTYPE_PLAYER_2,               // trevor
            PEDTYPE_CIVMALE,
            PEDTYPE_CIVFEMALE,
            PEDTYPE_COP,
            PEDTYPE_GANG_ALBANIAN,
            PEDTYPE_GANG_BIKER_1,
            PEDTYPE_GANG_BIKER_2,
            PEDTYPE_GANG_ITALIAN,
            PEDTYPE_GANG_RUSSIAN,
            PEDTYPE_GANG_RUSSIAN_2,
            PEDTYPE_GANG_IRISH,
            PEDTYPE_GANG_JAMAICAN,
            PEDTYPE_GANG_AFRICAN_AMERICAN,
            PEDTYPE_GANG_KOREAN,
            PEDTYPE_GANG_CHINESE_JAPANESE,
            PEDTYPE_GANG_PUERTO_RICAN,
            PEDTYPE_DEALER,
            PEDTYPE_MEDIC,
            PEDTYPE_FIREMAN,
            PEDTYPE_CRIMINAL,
            PEDTYPE_BUM,
            PEDTYPE_PROSTITUTE,
            PEDTYPE_SPECIAL,
            PEDTYPE_MISSION,
            PEDTYPE_SWAT,
            PEDTYPE_ANIMAL,
            PEDTYPE_ARMY
        };

        public enum FrameFlags
        {
            ExplosiveAmmo = 1 << 11,
            FireAmmo = 1 << 12,
            ExplosiveMelee = 1 << 13,
            SuperJump = 1 << 14,
        }
    }
}