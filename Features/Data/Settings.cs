using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Features.Data;

public class Settings
{
    public static bool ShowWindow = true;

    public static float ForwardDist = 1.5f;

    public static int ProduceTime = 5;

    public static int GodMode = -1;
    public static int AntiAFK = -1;
    public static int NoRagdoll = -1;
    public static int WaterProof = -1;
    public static int Invisible = -1;
    public static int UndeadOffRadar = -1;
    public static int EveryoneIgnore = -1;
    public static int CopsIgnore = -1;
    public static int NoCollision = -1;
    public static int AmmoModifier_InfiniteAmmo = -1;
    public static int AmmoModifier_InfiniteClip = -1;
    public static int Seatbelt = -1;

    public static int VehicleGodMode = -1;

    public static bool FrameFlagsExplosiveAmmo = false;
    public static bool FrameFlagsFlamingAmmo = false;
    public static bool FrameFlagsExplosiveFists = false;
    public static bool FrameFlagsSuperJump = false;

    public static bool AutoClearWanted = false;
    public static bool AutoKillNPC = false;
    public static bool AutoKillHostilityNPC = false;
    public static bool AutoKillPolice = false;

    public struct Overlay
    {
        public static bool VSync = false;
        public static int FPS = 300;

        public static bool ESP_2DBox = true;
        public static bool ESP_3DBox = false;
        public static bool ESP_2DLine = true;
        public static bool ESP_Bone = false;
        public static bool ESP_2DHealthBar = true;
        public static bool ESP_HealthText = false;
        public static bool ESP_NameText = false;
        public static bool ESP_Player = true;
        public static bool ESP_NPC = true;
        public static bool ESP_Crosshair = true;

        public static bool AimBot_Enabled = false;
        public static int AimBot_BoneIndex = 0;
        public static float AimBot_Fov = 8848.0f;
        public static WinVK AimBot_Key = WinVK.CONTROL;

        public static bool IsNoTOPMostHide = false;
    }
}
