using System.Collections.Generic;

namespace GTA5OnlineTools.Features.Data
{
    public class WeaponData
    {
        public class WeaponType
        {
            public string WType;
            public List<WeaponPreview> WPreview;
        }

        public class WeaponPreview
        {
            public string Name;
            public string Weapon;
            public string Pickup;
            public string Model;
        }

        public static List<WeaponPreview> Pistol = new List<WeaponPreview>()
        {
            new WeaponPreview(){ Name = "穿甲手枪", Weapon = "weapon_appistol", Pickup = "pickup_weapon_appistol", Model = "w_pi_appistol" },
            new WeaponPreview(){ Name = "战斗手枪", Weapon = "weapon_combatpistol", Pickup = "pickup_weapon_combatpistol", Model = "w_pi_combatpistol" },
            new WeaponPreview(){ Name = "信号枪", Weapon = "weapon_flaregun", Pickup = "pickup_weapon_flaregun", Model = "w_pi_flaregun"},
            new WeaponPreview(){ Name = "重型手枪", Weapon = "weapon_heavypistol", Pickup = "pickup_weapon_heavypistol", Model = "w_pi_heavypistol" },
            new WeaponPreview(){ Name = "射手手枪", Weapon = "weapon_marksmanpistol", Pickup = "pickup_weapon_marksmanpistol", Model = "w_pi_marksmanpistol"},
            new WeaponPreview(){ Name = "手枪", Weapon = "weapon_pistol", Pickup = "pickup_weapon_pistol", Model = "w_pi_pistol" },
            new WeaponPreview(){ Name = "手枪 MK2", Weapon = "weapon_pistol_mk2", Pickup = "pickup_weapon_pistol_mk2", Model = "w_pi_pistol_mk2"},
            new WeaponPreview(){ Name = ".5口径手枪", Weapon = "weapon_pistol50", Pickup = "pickup_weapon_pistol50", Model = "w_pi_pistol50" },
            new WeaponPreview(){ Name = "冲锋手枪", Weapon = "weapon_raypistol", Pickup = "pickup_weapon_raypistol", Model = "w_pi_raygun" },
            new WeaponPreview(){ Name = "重型左轮手枪", Weapon = "weapon_revolver", Pickup = "pickup_weapon_revolver", Model = "w_pi_revolver"},
            new WeaponPreview(){ Name = "重型左轮手枪 MK2 ", Weapon = "weapon_revolver_mk2", Pickup = "pickup_weapon_revolver_mk2", Model = "w_pi_revolver_mk2"},
            new WeaponPreview(){ Name = "劣质手枪", Weapon = "weapon_snspistol", Pickup = "pickup_weapon_snspistol", Model = "w_pi_sns_pistol "},
            new WeaponPreview(){ Name = "劣质手枪 MK2", Weapon = "weapon_snspistol_mk2", Pickup = "pickup_weapon_snspistol_mk2", Model = "w_pi_sns_pistol_mk2"},
            new WeaponPreview(){ Name = "电击枪", Weapon = "weapon_stungun", Pickup = "pickup_weapon_stungun", Model = "w_pi_stungun" },
            new WeaponPreview(){ Name = "老式手枪", Weapon = "weapon_vintagepistol", Pickup = "pickup_weapon_vintagepistol", Model = "w_pi_vintage_pistol" },
        };

        public static List<WeaponPreview> Rifle = new List<WeaponPreview>()
        {
            new WeaponPreview(){ Name = "高级步枪", Weapon = "weapon_advancedrifle", Pickup = "pickup_weapon_advancedrifle", Model = "w_ar_advancedrifle" },
            new WeaponPreview(){ Name = "突击步枪AK47", Weapon = "weapon_assaultrifle", Pickup = "pickup_weapon_assaultrifle", Model = "w_ar_assaultrifle" },
            new WeaponPreview(){ Name = "突击步枪AK47 MK2", Weapon = "weapon_assaultrifle_mk2", Pickup = "pickup_weapon_assaultrifle_mk2", Model = "w_ar_assaultrifle_mk2" },
            new WeaponPreview(){ Name = "无托式步枪QBZ95", Weapon = "weapon_bullpuprifle", Pickup = "pickup_weapon_bullpuprifle", Model = "w_ar_bullpuprifle" },
            new WeaponPreview(){ Name = "无托式步枪QBZ95 MK2", Weapon = "weapon_bullpuprifle_mk2", Pickup = "pickup_weapon_bullpuprifle_mk2", Model = "w_ar_bullpuprifle_mk2" },
            new WeaponPreview(){ Name = "卡宾步枪M4", Weapon = "weapon_carbinerifle", Pickup = "pickup_weapon_carbinerifle", Model = "w_ar_carbinerifle" },
            new WeaponPreview(){ Name = "卡宾步枪M4 MK2", Weapon = "weapon_carbinerifle_mk2", Pickup = "pickup_weapon_carbinerifle_mk2", Model = "w_ar_carbinerifle_mk2" },
            new WeaponPreview(){ Name = "紧凑型步枪", Weapon = "weapon_compactrifle", Pickup = "pickup_weapon_compactrifle", Model = "w_ar_compactrifle" },
            new WeaponPreview(){ Name = "老式火枪", Weapon = "weapon_musket", Pickup = "pickup_weapon_musket", Model = "w_ar_musket" },
            new WeaponPreview(){ Name = "特制卡宾步枪", Weapon = "weapon_specialcarbine", Pickup = "pickup_weapon_specialcarbine", Model = "w_ar_specialcarbine" },
            new WeaponPreview(){ Name = "特制卡宾步枪 MK2", Weapon = "weapon_specialcarbine_mk2", Pickup = "pickup_weapon_specialcarbine_mk2", Model = "w_ar_specialcarbine_mk2" },
        };

        public static List<WeaponPreview> SMG = new List<WeaponPreview>()
        {
            new WeaponPreview(){ Name = "突击冲锋枪", Weapon = "weapon_assaultsmg", Pickup = "pickup_weapon_assaultsmg", Model = "w_sb_assaultsmg" },
            new WeaponPreview(){ Name = "作战自卫冲锋枪", Weapon = "weapon_combatpdw", Pickup = "pickup_weapon_combatpdw", Model = "w_sb_pdw" },
            new WeaponPreview(){ Name = "UZI", Weapon = "weapon_microsmg", Pickup = "pickup_weapon_microsmg", Model = "w_sb_microsmg" },
            new WeaponPreview(){ Name = "汤姆逊冲锋枪", Weapon = "weapon_gusenberg", Pickup = "pickup_weapon_gusenberg", Model = "w_sb_gusenberg" },
            new WeaponPreview(){ Name = "冲锋枪", Weapon = "weapon_smg", Pickup = "pickup_weapon_smg", Model = "w_sb_smg" },
            new WeaponPreview(){ Name = "冲锋枪 MK2", Weapon = "weapon_smg_mk2", Pickup = "pickup_weapon_smg_mk2", Model = "w_sb_smg_mk2" },
            new WeaponPreview(){ Name = "微型冲锋枪", Weapon = "weapon_minismg", Pickup = "pickup_weapon_minismg", Model = "w_sb_minismg" },
        };

        public static List<WeaponPreview> MG = new List<WeaponPreview>()
        {
            new WeaponPreview(){ Name = "战斗机枪", Weapon = "weapon_combatmg", Pickup = "pickup_weapon_combatmg", Model = "w_mg_combatmg" },
            new WeaponPreview(){ Name = "战斗机枪 MK2", Weapon = "weapon_combatmg_mk2", Pickup = "pickup_weapon_combatmg_mk2", Model = "w_mg_combatmgmk2" },
            new WeaponPreview(){ Name = "机枪", Weapon = "weapon_mg", Pickup = "pickup_weapon_mg", Model = "w_mg_mg" },
        };

        public static List<WeaponPreview> Shotgun = new List<WeaponPreview>()
        {
            new WeaponPreview(){ Name = "突击霰弹枪", Weapon = "weapon_assaultshotgun", Pickup = "pickup_weapon_assaultshotgun", Model = "w_sg_assaultshotgun" },
            new WeaponPreview(){ Name = "冲锋霰弹枪", Weapon = "weapon_autoshotgun", Pickup = "pickup_weapon_autoshotgun", Model = "w_sg_autoshotgun" },
            new WeaponPreview(){ Name = "无托式霰弹枪", Weapon = "weapon_bullpupshotgun", Pickup = "pickup_weapon_bullpupshotgun", Model = "w_sg_bullpupshotgun" },
            new WeaponPreview(){ Name = "双管霰弹枪", Weapon = "weapon_dbshotgun", Pickup = "pickup_weapon_dbshotgun", Model = "w_sg_dbshotgun" },
            new WeaponPreview(){ Name = "重型霰弹枪", Weapon = "weapon_heavyshotgun", Pickup = "pickup_weapon_heavyshotgun", Model = "w_sg_heavyshotgun" },
            new WeaponPreview(){ Name = "泵动式霰弹枪", Weapon = "weapon_pumpshotgun", Pickup = "pickup_weapon_pumpshotgun", Model = "w_sg_pumpshotgun" },
            new WeaponPreview(){ Name = "泵动式霰弹枪 MK2", Weapon = "weapon_pumpshotgun_mk2", Pickup = "pickup_weapon_pumpshotgun_mk2", Model = "w_sg_pumpshotgun_mk2" },
            new WeaponPreview(){ Name = "短管霰弹枪", Weapon = "weapon_sawnoffshotgun", Pickup = "pickup_weapon_sawnoffshotgun", Model = "w_sg_sawnoff" },
        };

        public static List<WeaponPreview> Sniper = new List<WeaponPreview>()
        {
            new WeaponPreview(){ Name = "重型狙击步枪", Weapon = "weapon_heavysniper", Pickup = "pickup_weapon_heavysniper", Model = "w_sr_heavysniper" },
            new WeaponPreview(){ Name = "重型狙击步枪 MK2", Weapon = "weapon_heavysniper_mk2", Pickup = "pickup_weapon_heavysniper_mk2", Model = "w_sr_heavysniper_mk2" },
            new WeaponPreview(){ Name = "射手步枪", Weapon = "weapon_marksmanrifle", Pickup = "pickup_weapon_marksmanrifle", Model = "w_sr_marksmanrifle" },
            new WeaponPreview(){ Name = "射手步枪 MK2", Weapon = "weapon_marksmanrifle_mk2", Pickup = "pickup_weapon_marksmanrifle_mk2", Model = "w_sr_marksmanrifle_mk2" },
            new WeaponPreview(){ Name = "狙击步枪", Weapon = "weapon_sniperrifle", Pickup = "pickup_weapon_sniperrifle", Model = "w_sr_sniperrifle" },
        };

        public static List<WeaponPreview> Heavy = new List<WeaponPreview>()
        {
            new WeaponPreview(){ Name = "电磁步枪", Weapon = "weapon_railgun", Pickup = "pickup_weapon_railgun", Model = "w_ar_railgun" },
            new WeaponPreview(){ Name = "加特林", Weapon = "weapon_minigun", Pickup = "pickup_weapon_minigun", Model = "w_mg_minigun" },
            new WeaponPreview(){ Name = "紧凑型榴弹发射器", Weapon = "weapon_compactlauncher", Pickup = "pickup_weapon_compactlauncher", Model = "w_lr_compactlauncher" },
            new WeaponPreview(){ Name = "烟花发射器", Weapon = "weapon_firework", Pickup = "pickup_weapon_firework", Model = "w_lr_firework" },
            new WeaponPreview(){ Name = "榴弹发射器", Weapon = "weapon_grenadelauncher", Pickup = "pickup_weapon_grenadelauncher", Model = "w_lr_grenadelauncher" },
            new WeaponPreview(){ Name = "制导火箭发射器", Weapon = "weapon_hominglauncher", Pickup = "pickup_weapon_hominglauncher", Model = "w_lr_homing" },
            new WeaponPreview(){ Name = "RPG", Weapon = "weapon_rpg", Pickup = "pickup_weapon_rpg", Model = "w_lr_rpg" },
        };

        public static List<WeaponType> WeaponDataClass = new List<WeaponType>()
        {
            new WeaponType(){ WType = "手枪", WPreview = Pistol },
            new WeaponType(){ WType = "步枪", WPreview = Rifle },
            new WeaponType(){ WType = "冲锋枪", WPreview = SMG },
            new WeaponType(){ WType = "轻机枪", WPreview = MG },
            new WeaponType(){ WType = "霰弹枪", WPreview = Shotgun },
            new WeaponType(){ WType = "狙击枪", WPreview = Sniper },
            new WeaponType(){ WType = "重武器", WPreview = Heavy },
        };
    }
}
