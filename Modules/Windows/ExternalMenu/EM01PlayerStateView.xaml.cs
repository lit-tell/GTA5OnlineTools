using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Core;
using GTA5OnlineTools.Features.Data;

namespace GTA5OnlineTools.Modules.Windows.ExternalMenu;

/// <summary>
/// EM01PlayerStateView.xaml 的交互逻辑
/// </summary>
public partial class EM01PlayerStateView : UserControl
{
    // 快捷键
    private HotKeys MainHotKeys;

    private long local_ped;
    private long local_ped_current_vehicle;
    private bool local_ped_is_in_vehicle;
    private float local_ped_health;
    private float local_ped_max_health;
    private float local_ped_armor;
    private float local_ped_wanted_level;
    private float local_ped_run_speed;
    private float local_ped_swim_speed;
    private float local_ped_stealth_speed;

    public EM01PlayerStateView()
    {
        InitializeComponent();

        var thread0 = new Thread(MainThread);
        thread0.IsBackground = true;
        thread0.Start();

        var thread1 = new Thread(SpecialThread);
        thread1.IsBackground = true;
        thread1.Start();

        var thread2 = new Thread(CommonThread);
        thread2.IsBackground = true;
        thread2.Start();

        MainHotKeys = new HotKeys();
        MainHotKeys.AddKey(WinVK.F3);
        MainHotKeys.AddKey(WinVK.F4);
        MainHotKeys.AddKey(WinVK.F5);
        MainHotKeys.AddKey(WinVK.F6);
        MainHotKeys.AddKey(WinVK.F7);
        MainHotKeys.AddKey(WinVK.F8);
        MainHotKeys.AddKey(WinVK.DELETE);
        MainHotKeys.AddKey(WinVK.E);
        MainHotKeys.AddKey(WinVK.ADD);
        MainHotKeys.KeyDownEvent += new HotKeys.KeyHandler(MyKeyDownEvent);
        MainHotKeys.KeyUpEvent += new HotKeys.KeyHandler(MyKeyUpEvent);

        ExternalMenuView.ClosingDisposeEvent += ExternalMenuView_ClosingDisposeEvent;
    }

    private void ExternalMenuView_ClosingDisposeEvent()
    {
        MainHotKeys.Dispose();
    }

    private void MyKeyDownEvent(int keyId, string keyName)
    {
        Dispatcher.BeginInvoke(new Action(delegate
        {
            switch (keyId)
            {
                case (int)WinVK.DELETE:
                    ExternalMenuView.IsShowWindowDelegate();
                    break;
                case (int)WinVK.F3:
                    if (CheckBox_FillCurrentAmmo.IsChecked == true)
                    {
                        Hacks.Fill_All_Ammo();
                    }
                    break;
                case (int)WinVK.F4:
                    if (CheckBox_MovingFoward.IsChecked == true)
                    {
                        Hacks.To_Coords(local_ped, Ped.Get_Real_Forward_Position(local_ped, Settings.ForwardDist));
                    }
                    break;
                case (int)WinVK.F5:
                    if (CheckBox_ToWaypoint.IsChecked == true)
                    {
                        Hacks.To_Waypoint();
                    }
                    break;
                case (int)WinVK.F6:
                    if (CheckBox_ToObjective.IsChecked == true)
                    {
                        Hacks.To_Objective();
                    }
                    break;
                case (int)WinVK.F7:
                    if (CheckBox_FillHealthArmor.IsChecked == true)
                    {
                        Ped.Set_Health(local_ped, 328.0f);
                        Ped.Set_Max_Health(local_ped, 328.0f);
                        Ped.Set_Armour(local_ped, 50.0f);
                    }
                    break;
                case (int)WinVK.F8:
                    if (CheckBox_ClearWanted.IsChecked == true)
                    {
                        Ped.Set_Wanted_Level(local_ped, 0);
                    }
                    break;
                case (int)WinVK.E:
                    if (CheckBox_ThroughTheWall.IsChecked == true)
                    {
                        Settings.NoCollision = 1;
                        Ped.Set_No_Collision(local_ped, true);
                    }
                    break;
                case (int)WinVK.ADD:
                    if (CheckBox_SpawnVehicle.IsChecked == true)
                    {
                        Globals.Create_Vehicle(Hacks.Get_Local_Ped(), Settings.SpawnVehicleHash, Settings.SpawnVehicleMod, 7.0f, -225.0f);
                        Globals.Create_Vehicle(Hacks.Get_Local_Ped(), Settings.SpawnVehicleHash, Settings.SpawnVehicleMod, 7.0f, 0.0f);
                    }
                    break;
            }
        }));
    }

    private void MyKeyUpEvent(int keyId, string keyName)
    {
        Dispatcher.BeginInvoke(new Action(delegate
        {
            switch (keyId)
            {
                case (int)WinVK.E:
                    if (CheckBox_ThroughTheWall.IsChecked == true)
                    {
                        Settings.NoCollision = CheckBox_NoCollision.IsChecked == true ? 1 : 0;
                        if(Settings.NoCollision == 0) Ped.Set_No_Collision(local_ped, false);
                    }
                    break;
            }
        }));
    }

    private void MainThread()
    {
        while (true)
        {
            local_ped = Hacks.Get_Local_Ped();
            local_ped_current_vehicle = Ped.Get_Current_Vehicle(local_ped);
            local_ped_is_in_vehicle = Ped.Is_In_Vehicle(local_ped);

            local_ped_health = Ped.Get_Health(local_ped);
            local_ped_max_health = Ped.Get_Max_Health(local_ped);
            local_ped_armor = Ped.Get_Armor(local_ped);

            local_ped_wanted_level = Ped.Get_Wanted_Level(local_ped);
            local_ped_run_speed = Ped.Get_Run_Speed(local_ped);
            local_ped_swim_speed = Ped.Get_Swim_Speed(local_ped);
            local_ped_stealth_speed = Ped.Get_Stealth_Speed(local_ped);

            ////////////////////////////////

            if (Settings.GodMode == 1) Ped.Set_Proofs_God(local_ped, true);
            else if (Settings.GodMode == 0) { Ped.Set_Proofs_God(local_ped, false); Settings.GodMode = -1; }

            if (Settings.AntiAFK == 1) Globals.Anti_AFK(true);
            else if (Settings.AntiAFK == 0) { Globals.Anti_AFK(false); Settings.AntiAFK = -1; }

            if (Settings.NoRagdoll == 1) Ped.Set_No_Ragdoll(local_ped, true);
            else if (Settings.NoRagdoll == 0) { Ped.Set_No_Ragdoll(local_ped, false); Settings.NoRagdoll = -1; }

            if (Settings.WaterProof == 1) Ped.Set_Proofs_Water(local_ped, true);
            else if (Settings.WaterProof == 0) { Ped.Set_Proofs_Water(local_ped, false); Settings.WaterProof = -1; }

            if (Settings.Invisible == 1) Ped.Set_Invisible(local_ped, true);
            else if (Settings.Invisible == 0) { Ped.Set_Invisible(local_ped, false); Settings.Invisible = -1; }

            if (Settings.UndeadOffRadar == 1) Ped.Set_Max_Health(local_ped, 0.0f);
            else if (Settings.UndeadOffRadar == 0) { Ped.Set_Max_Health(local_ped, 328.0f); Settings.UndeadOffRadar = -1; }

            if (Settings.EveryoneIgnore == 1) Ped.Set_Everyone_Ignore(local_ped, true);
            else if (Settings.EveryoneIgnore == 0) { Ped.Set_Everyone_Ignore(local_ped, false); Settings.EveryoneIgnore = -1; }

            if (Settings.CopsIgnore == 1) Ped.Set_Cops_Ignore(local_ped, true);
            else if (Settings.CopsIgnore == 0) { Ped.Set_Cops_Ignore(local_ped, false); Settings.CopsIgnore = -1; }

            if (Settings.NoCollision == 1) Ped.Set_No_Collision(local_ped, true);
            else if (Settings.NoCollision == 0) { Ped.Set_No_Collision(local_ped, false); Settings.NoCollision = -1; }

            if (Settings.AmmoModifier_InfiniteAmmo == 1) Ped.Set_Infinite_Ammo(local_ped, true);
            else if (Settings.AmmoModifier_InfiniteAmmo == 0) { Ped.Set_Infinite_Ammo(local_ped, false); Settings.AmmoModifier_InfiniteAmmo = -1; }

            if (Settings.AmmoModifier_InfiniteClip == 1) Ped.Set_Infinite_Clip(local_ped, true);
            else if (Settings.AmmoModifier_InfiniteClip == 0) { Ped.Set_Infinite_Clip(local_ped, false); Settings.AmmoModifier_InfiniteClip = -1; }

            if (Settings.Seatbelt == 1) Ped.Set_Seatbelt(local_ped, true);
            else if (Settings.Seatbelt == 0) { Ped.Set_Seatbelt(local_ped, false); Settings.Seatbelt = -1; }

            if (Settings.ProofsBullet == 1) Ped.Set_Proofs_Bullet(local_ped, true);
            else if (Settings.ProofsBullet == 0) { Ped.Set_Proofs_Bullet(local_ped, false); Settings.ProofsBullet = -1; }

            if (Settings.ProofsFire == 1) Ped.Set_Proofs_Fire(local_ped, true);
            else if (Settings.ProofsFire == 0) { Ped.Set_Proofs_Fire(local_ped, false); Settings.ProofsFire = -1; }

            if (Settings.ProofsCollision == 1) Ped.Set_Proofs_Collision(local_ped, true);
            else if (Settings.ProofsCollision == 0) { Ped.Set_Proofs_Collision(local_ped, false); Settings.ProofsCollision = -1; }

            if (Settings.ProofsMelee == 1) Ped.Set_Proofs_Melee(local_ped, true);
            else if (Settings.ProofsMelee == 0) { Ped.Set_Proofs_Melee(local_ped, false); Settings.ProofsMelee = -1; }

            if (Settings.ProofsExplosion == 1) Ped.Set_Proofs_Explosion(local_ped, true);
            else if (Settings.ProofsExplosion == 0) { Ped.Set_Proofs_Explosion(local_ped, false); Settings.ProofsExplosion = -1; }

            if (Settings.ProofsSteam == 1) Ped.Set_Proofs_Steam(local_ped, true);
            else if (Settings.ProofsSteam == 0) { Ped.Set_Proofs_Steam(local_ped, false); Settings.ProofsSteam = -1; }

            if (Settings.ProofsDrown == 1) Ped.Set_Proofs_Drown(local_ped, true);
            else if (Settings.ProofsDrown == 0) { Ped.Set_Proofs_Drown(local_ped, false); Settings.ProofsDrown = -1; }

            if (Settings.VehicleGodMode == 1) Vehicle.Set_Proofs_God(local_ped_current_vehicle, true);
            else if (Settings.VehicleGodMode == 0) { Vehicle.Set_Proofs_God(local_ped_current_vehicle, false); Settings.VehicleGodMode = -1; }

            if(Settings.RemovePassiveModeCooldown == 1) { Globals.Remove_Passive_Mode_Cooldown(true); }
            else if (Settings.RemovePassiveModeCooldown == 0) { Globals.Remove_Passive_Mode_Cooldown(false); Settings.RemovePassiveModeCooldown = -1; }

            if (Settings.RemoveSuicideCooldown == 1) { Globals.Remove_Suicide_Cooldown(true); }
            else if (Settings.RemoveSuicideCooldown == 0) { Globals.Remove_Suicide_Cooldown(false); Settings.RemoveSuicideCooldown = -1; }

            if (Settings.DisableOrbitalCooldown == 1) { Globals.Disable_Orbital_Cooldown(true); }
            else if (Settings.DisableOrbitalCooldown == 0) { Globals.Disable_Orbital_Cooldown(false); Settings.DisableOrbitalCooldown = -1; }

            if(Settings.OffRadar == 1) { Globals.Off_Radar(true); }
            else if (Settings.OffRadar == 0) { Globals.Off_Radar(false); Settings.OffRadar = -1; }

            if (Settings.GhostOrganization == 1) { Globals.Ghost_Organization(true); }
            else if (Settings.GhostOrganization == 0) { Globals.Ghost_Organization(false); Settings.GhostOrganization = -1; }

            if (Settings.BlindCops == 1) { Globals.Blind_Cops(true); }
            else if (Settings.BlindCops == 0) { Globals.Blind_Cops(false); Settings.BlindCops = -1; }

            if (Settings.BribeCops == 1) { Globals.Bribe_Cops(true); }
            else if (Settings.BribeCops == 0) { Globals.Bribe_Cops(false); Settings.BribeCops = -1; }

            if (Settings.RevealPlayers == 1) { Globals.Reveal_Players(true); }
            else if (Settings.RevealPlayers == 0) { Globals.Reveal_Players(false); Settings.RevealPlayers = -1; }

            if (Settings.AllowSellOnNonPublic == 1) { Globals.Allow_Sell_On_Non_Public(true); }
            else if (Settings.AllowSellOnNonPublic == 0) { Globals.Allow_Sell_On_Non_Public(false); Settings.AllowSellOnNonPublic = -1; }

            if (Settings.OnlineSnow == 1) { Globals.Session_Snow(true); }
            else if (Settings.OnlineSnow == 0) { Globals.Session_Snow(false); Settings.OnlineSnow = -1; }
            ////////////////////////////////

            Dispatcher.BeginInvoke(new Action(delegate
            {
                if (Slider_Health.Value != local_ped_health)
                    Slider_Health.Value = local_ped_health;

                if (Slider_MaxHealth.Value != local_ped_max_health)
                    Slider_MaxHealth.Value = local_ped_max_health;

                if (Slider_Armor.Value != local_ped_armor)
                    Slider_Armor.Value = local_ped_armor;

                if (Slider_Wanted.Value != local_ped_wanted_level)
                    Slider_Wanted.Value = local_ped_wanted_level;

                if (Slider_RunSpeed.Value != local_ped_run_speed)
                    Slider_RunSpeed.Value = local_ped_run_speed;

                if (Slider_SwimSpeed.Value != local_ped_swim_speed)
                    Slider_SwimSpeed.Value = local_ped_swim_speed;

                if (Slider_StealthSpeed.Value != local_ped_stealth_speed)
                    Slider_StealthSpeed.Value = local_ped_stealth_speed;
            }));

            Thread.Sleep(1000);
        }
    }

    private void SpecialThread()
    {
        while (true)
        {
            if (Settings.FrameFlagsExplosiveAmmo) Ped.Set_Frame_Flags_Explosiveammo(local_ped, true);
            if (Settings.FrameFlagsFlamingAmmo) Ped.Set_Frame_Flags_Flamingammo(local_ped, true);
            if (Settings.FrameFlagsExplosiveFists) Ped.Set_Frame_Flags_Explosivefists(local_ped, true);
            if (Settings.FrameFlagsSuperJump) Ped.Set_Frame_Flags_Superjump(local_ped, true);

            Thread.Sleep(1);
        }
    }

    private void CommonThread()
    {
        while (true)
        {
            if (Settings.AutoClearWanted)
                Ped.Set_Wanted_Level(local_ped, 0);

            if (Settings.AutoKillNPC)
                Hacks.Kill_Npcs();

            if (Settings.AutoKillHostilityNPC)
                Hacks.Kill_Enemies();

            if (Settings.AutoKillPolice)
                Hacks.Kill_Cops();

            Thread.Sleep(200);
        }
    }

    private void Slider_Health_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) { Ped.Set_Health(local_ped, (float)Slider_Health.Value); }

    private void Slider_MaxHealth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) { Ped.Set_Max_Health(local_ped, (float)Slider_MaxHealth.Value); }

    private void Slider_Armor_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) { Ped.Set_Armour(local_ped, (float)Slider_Armor.Value); }

    private void Slider_Wanted_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) { Ped.Set_Wanted_Level(local_ped, (int)Slider_Wanted.Value); }

    private void Slider_RunSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) { Ped.Set_Run_Speed(local_ped, (float)Slider_RunSpeed.Value); }

    private void Slider_SwimSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) { Ped.Set_Swim_Speed(local_ped, (float)Slider_SwimSpeed.Value); }

    private void Slider_StealthSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) { Ped.Set_Stealth_Speed(local_ped, (float)Slider_StealthSpeed.Value); }

    private void CheckBox_PlayerGodMode_Click(object sender, RoutedEventArgs e) { Settings.GodMode = CheckBox_PlayerGodMode.IsChecked == true ? 1 : 0; }

    private void CheckBox_AntiAFK_Click(object sender, RoutedEventArgs e) { Settings.AntiAFK = CheckBox_AntiAFK.IsChecked == true ? 1 : 0; }

    private void CheckBox_WaterProof_Click(object sender, RoutedEventArgs e) { Settings.WaterProof = CheckBox_WaterProof.IsChecked == true ? 1 : 0; }

    private void CheckBox_Invisibility_Click(object sender, RoutedEventArgs e) { Settings.Invisible = CheckBox_Invisibility.IsChecked == true ? 1 : 0; }

    private void CheckBox_UndeadOffRadar_Click(object sender, RoutedEventArgs e) { Settings.UndeadOffRadar = CheckBox_UndeadOffRadar.IsChecked == true ? 1 : 0; }

    private void CheckBox_NoRagdoll_Click(object sender, RoutedEventArgs e) { Settings.NoRagdoll = CheckBox_NoRagdoll.IsChecked == true ? 1 : 0; }

    private void CheckBox_NpcsIgnore_Click(object sender, RoutedEventArgs e) { Settings.EveryoneIgnore = CheckBox_NPCIgnore.IsChecked == true ? 1 : 0; }

    private void CheckBox_CopsIgnore_Click(object sender, RoutedEventArgs e) { Settings.CopsIgnore = CheckBox_PoliceIgnore.IsChecked == true ? 1 : 0; }

    private void CheckBox_AutoClearWanted_Click(object sender, RoutedEventArgs e) { Settings.AutoClearWanted = CheckBox_AutoClearWanted.IsChecked == true; }

    private void CheckBox_AutoKillNPC_Click(object sender, RoutedEventArgs e) { Settings.AutoKillNPC = CheckBox_AutoKillNPC.IsChecked == true; }

    private void CheckBox_AutoKillHostilityNPC_Click(object sender, RoutedEventArgs e) { Settings.AutoKillHostilityNPC = CheckBox_AutoKillHostilityNPC.IsChecked == true; }

    private void CheckBox_AutoKillPolice_Click(object sender, RoutedEventArgs e) { Settings.AutoKillPolice = CheckBox_AutoKillPolice.IsChecked == true; }

    private void CheckBox_FrameFlags_Click(object sender, RoutedEventArgs e)
    {
        //Settings.FrameFlagsExplosiveAmmo  = CheckBox_FrameFlagsExplosiveAmmo.IsChecked == true;
        //Settings.FrameFlagsFlamingAmmo    = CheckBox_FrameFlagsFlamingAmmo.IsChecked == true;
        //Settings.FrameFlagsExplosiveFists = CheckBox_FrameFlagsExplosiveFists.IsChecked == true;
        Settings.FrameFlagsSuperJump      = CheckBox_FrameFlagsSuperJump.IsChecked == true;
    }

    private void CheckBox_NoCollision_Click(object sender, RoutedEventArgs e) { Settings.NoCollision = CheckBox_NoCollision.IsChecked == true ? 1 : 0; }

    private void Button_ToWaypoint_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Hacks.To_Waypoint();
    }

    private void Button_ToObjective_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Hacks.To_Objective();
    }

    private void Button_FillHealthArmor_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Ped.Set_Health(local_ped, 328.0f);
        Ped.Set_Max_Health(local_ped, 328.0f);
        Ped.Set_Armour(local_ped, 50.0f);
    }

    private void Button_ClearWanted_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Ped.Set_Wanted_Level(local_ped, 0);
    }

    private void Button_Suicide_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Ped.Set_Health(local_ped, 0.0f);
    }

    private void Slider_MovingFoward_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Settings.ForwardDist = (float)Slider_MovingFoward.Value;
    }

    private void CheckBox_ProofsBullet_Click(object sender, RoutedEventArgs e) { Settings.ProofsBullet = CheckBox_ProofsBullet.IsChecked == true ? 1 : 0; }

    private void CheckBox_ProofsFire_Click(object sender, RoutedEventArgs e) { Settings.ProofsFire = CheckBox_ProofsFire.IsChecked == true ? 1 : 0; }

    private void CheckBox_ProofsCollision_Click(object sender, RoutedEventArgs e) { Settings.ProofsCollision = CheckBox_ProofsCollision.IsChecked == true ? 1 : 0; }

    private void CheckBox_ProofsMelee_Click(object sender, RoutedEventArgs e) { Settings.ProofsMelee = CheckBox_ProofsMelee.IsChecked == true ? 1 : 0; }

    private void CheckBox_ProofsExplosion_Click(object sender, RoutedEventArgs e) { Settings.ProofsExplosion = CheckBox_ProofsExplosion.IsChecked == true ? 1 : 0; }

    private void CheckBox_ProofsSteam_Click(object sender, RoutedEventArgs e) { Settings.ProofsSteam = CheckBox_ProofsSteam.IsChecked == true ? 1 : 0; }

    private void CheckBox_ProofsDrown_Click(object sender, RoutedEventArgs e) { Settings.ProofsDrown = CheckBox_ProofsDrown.IsChecked == true ? 1 : 0; }

}
