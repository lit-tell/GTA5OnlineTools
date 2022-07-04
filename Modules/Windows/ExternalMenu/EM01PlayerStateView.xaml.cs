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
        MainHotKeys.KeyDownEvent += new HotKeys.KeyHandler(MyKeyDownEvent);

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
                        Hacks.fill_all_ammo();
                    }
                    break;
                case (int)WinVK.F4:
                    if (CheckBox_MovingFoward.IsChecked == true)
                    {
                        Hacks.to_coords(local_ped, Ped.get_real_forwardpos(local_ped, Settings.ForwardDist));
                    }
                    break;
                case (int)WinVK.F5:
                    if (CheckBox_ToWaypoint.IsChecked == true)
                    {
                        Hacks.to_waypoint();
                    }
                    break;
                case (int)WinVK.F6:
                    if (CheckBox_ToObjective.IsChecked == true)
                    {
                        Hacks.to_objective();
                    }
                    break;
                case (int)WinVK.F7:
                    if (CheckBox_FillHealthArmor.IsChecked == true)
                    {
                        Ped.set_health(local_ped, 328.0f);
                        Ped.set_max_health(local_ped, 328.0f);
                        Ped.set_armour(local_ped, 50.0f);
                    }
                    break;
                case (int)WinVK.F8:
                    if (CheckBox_ClearWanted.IsChecked == true)
                    {
                        Ped.set_wanted_level(local_ped, 0);
                    }
                    break;
                case (int)WinVK.E:
                    if (CheckBox_ThroughTheWall.IsChecked == true)
                    {
                        Ped.set_no_collision(local_ped, true);
                    }
                    else if (Ped.get_no_collision(local_ped)) Ped.set_no_collision(local_ped, false);
                    break;
            }
        }));
    }

    private void MainThread()
    {
        while (true)
        {
            local_ped = Hacks.get_local_ped();
            local_ped_current_vehicle = Ped.get_current_vehicle(local_ped);
            local_ped_is_in_vehicle = Ped.is_in_vehicle(local_ped);

            local_ped_health = Ped.get_health(local_ped);
            local_ped_max_health = Ped.get_max_health(local_ped);
            local_ped_armor = Ped.get_armor(local_ped);

            local_ped_wanted_level = Ped.get_wanted_level(local_ped);
            local_ped_run_speed = Ped.get_run_speed(local_ped);
            local_ped_swim_speed = Ped.get_swim_speed(local_ped);
            local_ped_stealth_speed = Ped.get_stealth_speed(local_ped);

            ////////////////////////////////

            if (Settings.GodMode == 1) Ped.set_godmode(local_ped, true);
            else if (Settings.GodMode == 0) { Ped.set_godmode(local_ped, false); Settings.GodMode = -1; }

            if (Settings.AntiAFK == 1) Globals.anti_afk(true);
            else if (Settings.AntiAFK == 0) { Globals.anti_afk(false); Settings.AntiAFK = -1; }

            if (Settings.NoRagdoll == 1) Ped.set_no_ragdoll(local_ped, true);
            else if (Settings.NoRagdoll == 0) { Ped.set_no_ragdoll(local_ped, false); Settings.NoRagdoll = -1; }

            if (Settings.WaterProof == 1) Ped.set_waterproof(local_ped, true);
            else if (Settings.WaterProof == 0) { Ped.set_waterproof(local_ped, false); Settings.WaterProof = -1; }

            if (Settings.Invisible == 1) Ped.set_invisible(local_ped, true);
            else if (Settings.Invisible == 0) { Ped.set_invisible(local_ped, false); Settings.Invisible = -1; }

            if (Settings.UndeadOffRadar == 1) Ped.set_max_health(local_ped, 0.0f);
            else if (Settings.UndeadOffRadar == 0) { Ped.set_max_health(local_ped, 328.0f); Settings.UndeadOffRadar = -1; }

            if (Settings.EveryoneIgnore == 1) Ped.set_everyone_ignore(local_ped, true);
            else if (Settings.EveryoneIgnore == 0) { Ped.set_everyone_ignore(local_ped, false); Settings.EveryoneIgnore = -1; }

            if (Settings.CopsIgnore == 1) Ped.set_cops_ignore(local_ped, true);
            else if (Settings.CopsIgnore == 0) { Ped.set_cops_ignore(local_ped, false); Settings.CopsIgnore = -1; }

            if (Settings.NoCollision == 1) Ped.set_no_collision(local_ped, true);
            else if (Settings.NoCollision == 0) { Ped.set_no_collision(local_ped, false); Settings.NoCollision = -1; }

            if (Settings.AmmoModifier_InfiniteAmmo == 1) Ped.set_infinite_ammo(local_ped, true);
            else if (Settings.AmmoModifier_InfiniteAmmo == 0) { Ped.set_infinite_ammo(local_ped, false); Settings.AmmoModifier_InfiniteAmmo = -1; }

            if (Settings.AmmoModifier_InfiniteClip == 1) Ped.set_infinite_clip(local_ped, true);
            else if (Settings.AmmoModifier_InfiniteClip == 0) { Ped.set_infinite_clip(local_ped, false); Settings.AmmoModifier_InfiniteClip = -1; }

            if (Settings.Seatbelt == 1) Ped.set_seatbelt(local_ped, true);
            else if (Settings.Seatbelt == 0) { Ped.set_seatbelt(local_ped, false); Settings.Seatbelt = -1; }

            if (Settings.VehicleGodMode == 1) Vehicle.set_godmode(local_ped_current_vehicle, true);
            else if (Settings.VehicleGodMode == 0) { Vehicle.set_godmode(local_ped_current_vehicle, false); Settings.VehicleGodMode = -1; }

            if(Settings.RemovePassiveModeCooldown == 1) { Globals.remove_passive_mode_cooldown(true); }
            else if (Settings.RemovePassiveModeCooldown == 0) { Globals.remove_passive_mode_cooldown(false); Settings.RemovePassiveModeCooldown = -1; }

            if (Settings.RemoveSuicideCooldown == 1) { Globals.remove_suicide_cooldown(true); }
            else if (Settings.RemoveSuicideCooldown == 0) { Globals.remove_suicide_cooldown(false); Settings.RemoveSuicideCooldown = -1; }

            if (Settings.DisableOrbitalCooldown == 1) { Globals.disable_orbital_cooldown(true); }
            else if (Settings.DisableOrbitalCooldown == 0) { Globals.disable_orbital_cooldown(false); Settings.DisableOrbitalCooldown = -1; }

            if(Settings.OffRadar == 1) { Globals.off_radar(true); }
            else if (Settings.OffRadar == 0) { Globals.off_radar(false); Settings.OffRadar = -1; }

            if (Settings.GhostOrganization == 1) { Globals.ghost_organization(true); }
            else if (Settings.GhostOrganization == 0) { Globals.ghost_organization(false); Settings.GhostOrganization = -1; }

            if (Settings.BlindCops == 1) { Globals.blind_cops(true); }
            else if (Settings.BlindCops == 0) { Globals.blind_cops(false); Settings.BlindCops = -1; }

            if (Settings.BribeCops == 1) { Globals.bribe_cops(true); }
            else if (Settings.BribeCops == 0) { Globals.bribe_cops(false); Settings.BribeCops = -1; }

            if (Settings.RevealPlayers == 1) { Globals.reveal_players(true); }
            else if (Settings.RevealPlayers == 0) { Globals.reveal_players(false); Settings.RevealPlayers = -1; }

            if (Settings.AllowSellOnNonPublic == 1) { Globals.allow_sell_on_non_public(true); }
            else if (Settings.AllowSellOnNonPublic == 0) { Globals.allow_sell_on_non_public(false); Settings.AllowSellOnNonPublic = -1; }

            if (Settings.OnlineSnow == 1) { Globals.session_snow(true); }
            else if (Settings.OnlineSnow == 0) { Globals.session_snow(false); Settings.OnlineSnow = -1; }
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
            if (Settings.FrameFlagsExplosiveAmmo) Ped.set_frame_flags_explosiveammo(local_ped, true);
            if (Settings.FrameFlagsFlamingAmmo) Ped.set_frame_flags_flamingammo(local_ped, true);
            if (Settings.FrameFlagsExplosiveFists) Ped.set_frame_flags_explosivefists(local_ped, true);
            if (Settings.FrameFlagsSuperJump) Ped.set_frame_flags_superjump(local_ped, true);

            Thread.Sleep(1);
        }
    }

    private void CommonThread()
    {
        while (true)
        {
            if (Settings.AutoClearWanted)
                Ped.set_wanted_level(local_ped, 0);

            if (Settings.AutoKillNPC)
                Hacks.kill_npcs();

            if (Settings.AutoKillHostilityNPC)
                Hacks.kill_enemies();

            if (Settings.AutoKillPolice)
                Hacks.kill_cops();

            Thread.Sleep(200);
        }
    }

    private void Slider_Health_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) { Ped.set_health(local_ped, (float)Slider_Health.Value); }

    private void Slider_MaxHealth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) { Ped.set_max_health(local_ped, (float)Slider_MaxHealth.Value); }

    private void Slider_Armor_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) { Ped.set_armour(local_ped, (float)Slider_Armor.Value); }

    private void Slider_Wanted_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) { Ped.set_wanted_level(local_ped, (int)Slider_Wanted.Value); }

    private void Slider_RunSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) { Ped.set_run_speed(local_ped, (float)Slider_RunSpeed.Value); }

    private void Slider_SwimSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) { Ped.set_swim_speed(local_ped, (float)Slider_SwimSpeed.Value); }

    private void Slider_StealthSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) { Ped.set_stealth_speed(local_ped, (float)Slider_StealthSpeed.Value); }

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
        Settings.FrameFlagsExplosiveAmmo  = CheckBox_FrameFlagsExplosiveAmmo.IsChecked == true;
        Settings.FrameFlagsFlamingAmmo    = CheckBox_FrameFlagsFlamingAmmo.IsChecked == true;
        Settings.FrameFlagsExplosiveFists = CheckBox_FrameFlagsExplosiveFists.IsChecked == true;
        Settings.FrameFlagsSuperJump      = CheckBox_FrameFlagsSuperJump.IsChecked == true;
    }

    private void CheckBox_NoCollision_Click(object sender, RoutedEventArgs e) { Settings.NoCollision = CheckBox_NoCollision.IsChecked == true ? 1 : 0; }

    private void Button_ToWaypoint_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Hacks.to_waypoint();
    }

    private void Button_ToObjective_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Hacks.to_objective();
    }

    private void Button_FillHealthArmor_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Ped.set_health(local_ped, 328.0f);
        Ped.set_max_health(local_ped, 328.0f);
        Ped.set_armour(local_ped, 50.0f);
    }

    private void Button_ClearWanted_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Ped.set_wanted_level(local_ped, 0);
    }

    private void Button_Suicide_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Ped.set_health(local_ped, 0.0f);
    }

    private void Slider_MovingFoward_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Settings.ForwardDist = (float)Slider_MovingFoward.Value;
    }
}
