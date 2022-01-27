using System.Collections.Generic;

namespace GTA5OnlineTools.Features.Data
{
    public class WeaponData
    {
        public class WeaponClass
        {
            public string ClassName;
            public List<WeaponInfo> WeaponInfo;
        }

        public class WeaponInfo
        {
            public string Name;
            public string Weapon;
            public string Pickup;
            public string Model;
        }

        public static List<WeaponInfo> Pistol = new List<WeaponInfo>()
        {
            new WeaponInfo(){ Name="穿甲手枪", Weapon="weapon_appistol", Pickup="pickup_weapon_appistol", Model="w_pi_appistol" },
            new WeaponInfo(){ Name="战斗手枪", Weapon="weapon_combatpistol", Pickup="pickup_weapon_combatpistol", Model="w_pi_combatpistol" },
            new WeaponInfo(){ Name="信号枪", Weapon="weapon_flaregun", Pickup="pickup_weapon_flaregun", Model="w_pi_flaregun"},
            new WeaponInfo(){ Name="重型手枪", Weapon="weapon_heavypistol", Pickup="pickup_weapon_heavypistol", Model="w_pi_heavypistol" },
            new WeaponInfo(){ Name="射手手枪", Weapon="weapon_marksmanpistol", Pickup="pickup_weapon_marksmanpistol", Model="w_pi_marksmanpistol"},
            new WeaponInfo(){ Name="手枪", Weapon="weapon_pistol", Pickup="pickup_weapon_pistol", Model="w_pi_pistol" },
            new WeaponInfo(){ Name="手枪 MK2", Weapon="weapon_pistol_mk2", Pickup="pickup_weapon_pistol_mk2", Model="w_pi_pistol_mk2"},
            new WeaponInfo(){ Name=".5口径手枪", Weapon="weapon_pistol50", Pickup="pickup_weapon_pistol50", Model="w_pi_pistol50" },
            new WeaponInfo(){ Name="冲锋手枪", Weapon="weapon_raypistol", Pickup="pickup_weapon_raypistol", Model="w_pi_raygun" },
            new WeaponInfo(){ Name="重型左轮手枪", Weapon="weapon_revolver", Pickup="pickup_weapon_revolver", Model="w_pi_revolver"},
            new WeaponInfo(){ Name="重型左轮手枪 MK2 ", Weapon="weapon_revolver_mk2", Pickup="pickup_weapon_revolver_mk2", Model="w_pi_revolver_mk2"},
            new WeaponInfo(){ Name="劣质手枪", Weapon="weapon_snspistol", Pickup="pickup_weapon_snspistol", Model="w_pi_sns_pistol "},
            new WeaponInfo(){ Name="劣质手枪 MK2", Weapon="weapon_snspistol_mk2", Pickup="pickup_weapon_snspistol_mk2", Model="w_pi_sns_pistol_mk2"},
            new WeaponInfo(){ Name="电击枪", Weapon="weapon_stungun", Pickup="pickup_weapon_stungun", Model="w_pi_stungun" },
            new WeaponInfo(){ Name="老式手枪", Weapon="weapon_vintagepistol", Pickup="pickup_weapon_vintagepistol", Model="w_pi_vintage_pistol" },
        };

        public static List<WeaponInfo> Rifle = new List<WeaponInfo>()
        {
            new WeaponInfo(){ Name="高级步枪", Weapon="weapon_advancedrifle", Pickup="pickup_weapon_advancedrifle", Model="w_ar_advancedrifle" },
            new WeaponInfo(){ Name="突击步枪AK47", Weapon="weapon_assaultrifle", Pickup="pickup_weapon_assaultrifle", Model="w_ar_assaultrifle" },
            new WeaponInfo(){ Name="突击步枪AK47 MK2", Weapon="weapon_assaultrifle_mk2", Pickup="pickup_weapon_assaultrifle_mk2", Model="w_ar_assaultrifle_mk2" },
            new WeaponInfo(){ Name="无托式步枪QBZ95", Weapon="weapon_bullpuprifle", Pickup="pickup_weapon_bullpuprifle", Model="w_ar_bullpuprifle" },
            new WeaponInfo(){ Name="无托式步枪QBZ95 MK2", Weapon="weapon_bullpuprifle_mk2", Pickup="pickup_weapon_bullpuprifle_mk2", Model="w_ar_bullpuprifle_mk2" },
            new WeaponInfo(){ Name="卡宾步枪M4", Weapon="weapon_carbinerifle", Pickup="pickup_weapon_carbinerifle", Model="w_ar_carbinerifle" },
            new WeaponInfo(){ Name="卡宾步枪M4 MK2", Weapon="weapon_carbinerifle_mk2", Pickup="pickup_weapon_carbinerifle_mk2", Model="w_ar_carbinerifle_mk2" },
            new WeaponInfo(){ Name="紧凑型步枪", Weapon="weapon_compactrifle", Pickup="pickup_weapon_compactrifle", Model="w_ar_compactrifle" },
            new WeaponInfo(){ Name="老式火枪", Weapon="weapon_musket", Pickup="pickup_weapon_musket", Model="w_ar_musket" },
            new WeaponInfo(){ Name="特制卡宾步枪", Weapon="weapon_specialcarbine", Pickup="pickup_weapon_specialcarbine", Model="w_ar_specialcarbine" },
            new WeaponInfo(){ Name="特制卡宾步枪 MK2", Weapon="weapon_specialcarbine_mk2", Pickup="pickup_weapon_specialcarbine_mk2", Model="w_ar_specialcarbine_mk2" },
        };

        public static List<WeaponInfo> SMG = new List<WeaponInfo>()
        {
            new WeaponInfo(){ Name="突击冲锋枪", Weapon="weapon_assaultsmg", Pickup="pickup_weapon_assaultsmg", Model="w_sb_assaultsmg" },
            new WeaponInfo(){ Name="作战自卫冲锋枪", Weapon="weapon_combatpdw", Pickup="pickup_weapon_combatpdw", Model="w_sb_pdw" },
            new WeaponInfo(){ Name="UZI", Weapon="weapon_microsmg", Pickup="pickup_weapon_microsmg", Model="w_sb_microsmg" },
            new WeaponInfo(){ Name="汤姆逊冲锋枪", Weapon="weapon_gusenberg", Pickup="pickup_weapon_gusenberg", Model="w_sb_gusenberg" },
            new WeaponInfo(){ Name="冲锋枪", Weapon="weapon_smg", Pickup="pickup_weapon_smg", Model="w_sb_smg" },
            new WeaponInfo(){ Name="冲锋枪 MK2", Weapon="weapon_smg_mk2", Pickup="pickup_weapon_smg_mk2", Model="w_sb_smg_mk2" },
            new WeaponInfo(){ Name="微型冲锋枪", Weapon="weapon_minismg", Pickup="pickup_weapon_minismg", Model="w_sb_minismg" },
        };

        public static List<WeaponInfo> MG = new List<WeaponInfo>()
        {
            new WeaponInfo(){ Name="战斗机枪", Weapon="weapon_combatmg", Pickup="pickup_weapon_combatmg", Model="w_mg_combatmg" },
            new WeaponInfo(){ Name="战斗机枪 MK2", Weapon="weapon_combatmg_mk2", Pickup="pickup_weapon_combatmg_mk2", Model="w_mg_combatmgmk2" },
            new WeaponInfo(){ Name="机枪", Weapon="weapon_mg", Pickup="pickup_weapon_mg", Model="w_mg_mg" },
        };

        public static List<WeaponInfo> Shotgun = new List<WeaponInfo>()
        {
            new WeaponInfo(){ Name="突击霰弹枪", Weapon="weapon_assaultshotgun", Pickup="pickup_weapon_assaultshotgun", Model="w_sg_assaultshotgun" },
            new WeaponInfo(){ Name="冲锋霰弹枪", Weapon="weapon_autoshotgun", Pickup="pickup_weapon_autoshotgun", Model="w_sg_autoshotgun" },
            new WeaponInfo(){ Name="无托式霰弹枪", Weapon="weapon_bullpupshotgun", Pickup="pickup_weapon_bullpupshotgun", Model="w_sg_bullpupshotgun" },
            new WeaponInfo(){ Name="双管霰弹枪", Weapon="weapon_dbshotgun", Pickup="pickup_weapon_dbshotgun", Model="w_sg_dbshotgun" },
            new WeaponInfo(){ Name="重型霰弹枪", Weapon="weapon_heavyshotgun", Pickup="pickup_weapon_heavyshotgun", Model="w_sg_heavyshotgun" },
            new WeaponInfo(){ Name="泵动式霰弹枪", Weapon="weapon_pumpshotgun", Pickup="pickup_weapon_pumpshotgun", Model="w_sg_pumpshotgun" },
            new WeaponInfo(){ Name="泵动式霰弹枪 MK2", Weapon="weapon_pumpshotgun_mk2", Pickup="pickup_weapon_pumpshotgun_mk2", Model="w_sg_pumpshotgun_mk2" },
            new WeaponInfo(){ Name="短管霰弹枪", Weapon="weapon_sawnoffshotgun", Pickup="pickup_weapon_sawnoffshotgun", Model="w_sg_sawnoff" },
        };

        public static List<WeaponInfo> Sniper = new List<WeaponInfo>()
        {
            new WeaponInfo(){ Name="重型狙击步枪", Weapon="weapon_heavysniper", Pickup="pickup_weapon_heavysniper", Model="w_sr_heavysniper" },
            new WeaponInfo(){ Name="重型狙击步枪 MK2", Weapon="weapon_heavysniper_mk2", Pickup="pickup_weapon_heavysniper_mk2", Model="w_sr_heavysniper_mk2" },
            new WeaponInfo(){ Name="射手步枪", Weapon="weapon_marksmanrifle", Pickup="pickup_weapon_marksmanrifle", Model="w_sr_marksmanrifle" },
            new WeaponInfo(){ Name="射手步枪 MK2", Weapon="weapon_marksmanrifle_mk2", Pickup="pickup_weapon_marksmanrifle_mk2", Model="w_sr_marksmanrifle_mk2" },
            new WeaponInfo(){ Name="狙击步枪", Weapon="weapon_sniperrifle", Pickup="pickup_weapon_sniperrifle", Model="w_sr_sniperrifle" },
        };

        public static List<WeaponInfo> Heavy = new List<WeaponInfo>()
        {
            new WeaponInfo(){ Name="电磁步枪", Weapon="weapon_railgun", Pickup="pickup_weapon_railgun", Model="w_ar_railgun" },
            new WeaponInfo(){ Name="加特林", Weapon="weapon_minigun", Pickup="pickup_weapon_minigun", Model="w_mg_minigun" },
            new WeaponInfo(){ Name="紧凑型榴弹发射器", Weapon="weapon_compactlauncher", Pickup="pickup_weapon_compactlauncher", Model="w_lr_compactlauncher" },
            new WeaponInfo(){ Name="烟花发射器", Weapon="weapon_firework", Pickup="pickup_weapon_firework", Model="w_lr_firework" },
            new WeaponInfo(){ Name="榴弹发射器", Weapon="weapon_grenadelauncher", Pickup="pickup_weapon_grenadelauncher", Model="w_lr_grenadelauncher" },
            new WeaponInfo(){ Name="制导火箭发射器", Weapon="weapon_hominglauncher", Pickup="pickup_weapon_hominglauncher", Model="w_lr_homing" },
            new WeaponInfo(){ Name="RPG", Weapon="weapon_rpg", Pickup="pickup_weapon_rpg", Model="w_lr_rpg" },
        };

        public static List<WeaponClass> WeaponDataClass = new List<WeaponClass>()
        {
            new WeaponClass(){ ClassName="手枪", WeaponInfo=Pistol },
            new WeaponClass(){ ClassName="步枪", WeaponInfo=Rifle },
            new WeaponClass(){ ClassName="冲锋枪", WeaponInfo=SMG },
            new WeaponClass(){ ClassName="轻机枪", WeaponInfo=MG },
            new WeaponClass(){ ClassName="霰弹枪", WeaponInfo=Shotgun },
            new WeaponClass(){ ClassName="狙击枪", WeaponInfo=Sniper },
            new WeaponClass(){ ClassName="重武器", WeaponInfo=Heavy }
        };
    }
}
