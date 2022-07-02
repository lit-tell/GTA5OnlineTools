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
                        Hacks.fill_current_ammo();
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

            if (Settings.Player.GodMode == 1) Ped.set_godmode(local_ped, true);
            else if (Settings.Player.GodMode == 0) { Ped.set_godmode(local_ped, false); Settings.Player.GodMode = -1; }

            if (Settings.Player.AntiAFK == 1) Globals.anti_afk(true);
            else if (Settings.Player.AntiAFK == 0) { Globals.anti_afk(false); Settings.Player.AntiAFK = -1; }

            if (Settings.Player.NoRagdoll == 1) Ped.set_no_ragdoll(local_ped, true);
            else if (Settings.Player.NoRagdoll == 0) { Ped.set_no_ragdoll(local_ped, false); Settings.Player.NoRagdoll = -1; }

            if (Settings.Player.WaterProof == 1) Ped.set_waterproof(local_ped, true);
            else if (Settings.Player.WaterProof == 0) { Ped.set_waterproof(local_ped, false); Settings.Player.WaterProof = -1; }

            if (Settings.Player.Invisible == 1) Ped.set_invisible(local_ped, true);
            else if (Settings.Player.Invisible == 0) { Ped.set_invisible(local_ped, false); Settings.Player.Invisible = -1; }

            if (Settings.Player.UndeadOffRadar == 1) Ped.set_max_health(local_ped, 0.0f);
            else if (Settings.Player.UndeadOffRadar == 0) { Ped.set_max_health(local_ped, 328.0f); Settings.Player.UndeadOffRadar = -1; }

            if (Settings.Player.EveryoneIgnore == 1) Ped.set_everyone_ignore(local_ped, true);
            else if (Settings.Player.EveryoneIgnore == 0) { Ped.set_everyone_ignore(local_ped, false); Settings.Player.EveryoneIgnore = -1; }

            if (Settings.Player.CopsIgnore == 1) Ped.set_cops_ignore(local_ped, true);
            else if (Settings.Player.CopsIgnore == 0) { Ped.set_cops_ignore(local_ped, false); Settings.Player.CopsIgnore = -1; }

            if (Settings.Player.NoCollision == 1) Ped.set_no_collision(local_ped, true);
            else if (Settings.Player.NoCollision == 0) { Ped.set_no_collision(local_ped, false); Settings.Player.NoCollision = -1; }

            if (Settings.Player.AmmoModifier_InfiniteAmmo == 1) Ped.set_infinite_ammo(local_ped, true);
            else if (Settings.Player.AmmoModifier_InfiniteAmmo == 0) { Ped.set_infinite_ammo(local_ped, false); Settings.Player.AmmoModifier_InfiniteAmmo = -1; }

            if (Settings.Player.AmmoModifier_InfiniteClip == 1) Ped.set_infinite_clip(local_ped, true);
            else if (Settings.Player.AmmoModifier_InfiniteClip == 0) { Ped.set_infinite_clip(local_ped, false); Settings.Player.AmmoModifier_InfiniteClip = -1; }

            if (Settings.Player.Seatbelt == 1) Ped.set_seatbelt(local_ped, true);
            else if (Settings.Player.Seatbelt == 0) { Ped.set_seatbelt(local_ped, false); Settings.Player.Seatbelt = -1; }

            if (Settings.Vehicle.VehicleGodMode == 1) Vehicle.set_godmode(local_ped_current_vehicle, true);
            else if (Settings.Vehicle.VehicleGodMode == 0) { Vehicle.set_godmode(local_ped_current_vehicle, false); Settings.Vehicle.VehicleGodMode = -1; }

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
            if (Settings.Special.FrameFlagsExplosiveAmmo) Ped.set_frame_flags_explosiveammo(local_ped, true);
            if (Settings.Special.FrameFlagsFlamingAmmo) Ped.set_frame_flags_flamingammo(local_ped, true);
            if (Settings.Special.FrameFlagsExplosiveFists) Ped.set_frame_flags_explosivefists(local_ped, true);
            if (Settings.Special.FrameFlagsSuperJump) Ped.set_frame_flags_superjump(local_ped, true);

            Thread.Sleep(1);
        }
    }

    private void CommonThread()
    {
        while (true)
        {
            if (Settings.Common.AutoClearWanted)
                Ped.set_wanted_level(local_ped, 0);

            if (Settings.Common.AutoKillNPC)
                Hacks.kill_npcs();

            if (Settings.Common.AutoKillHostilityNPC)
                Hacks.kill_enemies();

            if (Settings.Common.AutoKillPolice)
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

    private void CheckBox_PlayerGodMode_Click(object sender, RoutedEventArgs e) { Settings.Player.GodMode = CheckBox_PlayerGodMode.IsChecked == true ? 1 : 0; }

    private void CheckBox_AntiAFK_Click(object sender, RoutedEventArgs e) { Settings.Player.AntiAFK = CheckBox_AntiAFK.IsChecked == true ? 1 : 0; }

    private void CheckBox_WaterProof_Click(object sender, RoutedEventArgs e) { Settings.Player.WaterProof = CheckBox_WaterProof.IsChecked == true ? 1 : 0; }

    private void CheckBox_Invisibility_Click(object sender, RoutedEventArgs e) { Settings.Player.Invisible = CheckBox_Invisibility.IsChecked == true ? 1 : 0; }

    private void CheckBox_UndeadOffRadar_Click(object sender, RoutedEventArgs e) { Settings.Player.UndeadOffRadar = CheckBox_UndeadOffRadar.IsChecked == true ? 1 : 0; }

    private void CheckBox_NoRagdoll_Click(object sender, RoutedEventArgs e) { Settings.Player.NoRagdoll = CheckBox_NoRagdoll.IsChecked == true ? 1 : 0; }

    private void CheckBox_NpcsIgnore_Click(object sender, RoutedEventArgs e) { Settings.Player.EveryoneIgnore = CheckBox_NPCIgnore.IsChecked == true ? 1 : 0; }

    private void CheckBox_CopsIgnore_Click(object sender, RoutedEventArgs e) { Settings.Player.CopsIgnore = CheckBox_PoliceIgnore.IsChecked == true ? 1 : 0; }

    private void CheckBox_AutoClearWanted_Click(object sender, RoutedEventArgs e) { Settings.Common.AutoClearWanted = CheckBox_AutoClearWanted.IsChecked == true; }

    private void CheckBox_AutoKillNPC_Click(object sender, RoutedEventArgs e) { Settings.Common.AutoKillNPC = CheckBox_AutoKillNPC.IsChecked == true; }

    private void CheckBox_AutoKillHostilityNPC_Click(object sender, RoutedEventArgs e) { Settings.Common.AutoKillHostilityNPC = CheckBox_AutoKillHostilityNPC.IsChecked == true; }

    private void CheckBox_AutoKillPolice_Click(object sender, RoutedEventArgs e) { Settings.Common.AutoKillPolice = CheckBox_AutoKillPolice.IsChecked == true; }

    private void CheckBox_FrameFlags_Click(object sender, RoutedEventArgs e)
    {
        Settings.Special.FrameFlagsExplosiveAmmo  = CheckBox_FrameFlagsExplosiveAmmo.IsChecked == true;
        Settings.Special.FrameFlagsFlamingAmmo    = CheckBox_FrameFlagsFlamingAmmo.IsChecked == true;
        Settings.Special.FrameFlagsExplosiveFists = CheckBox_FrameFlagsExplosiveFists.IsChecked == true;
        Settings.Special.FrameFlagsSuperJump      = CheckBox_FrameFlagsSuperJump.IsChecked == true;
    }

    private void CheckBox_NoCollision_Click(object sender, RoutedEventArgs e) { Settings.Player.NoCollision = CheckBox_NoCollision.IsChecked == true ? 1 : 0; }

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
