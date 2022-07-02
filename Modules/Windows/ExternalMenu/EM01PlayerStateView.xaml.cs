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
    // 特殊功能
    private int FrameFlagsExplosiveAmmo = 0;
    private int FrameFlagsFlamingAmmo = 0;
    private int FrameFlagsExplosiveFists = 0;
    private int FrameFlagsSuperJump = 0;

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
                        Hacks.to_coords(Hacks.get_local_ped(), Ped.get_real_forwardpos(Hacks.get_local_ped(), Settings.Forward));
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
                        Ped.set_health(Hacks.get_local_ped(), 328.0f);
                        Ped.set_max_health(Hacks.get_local_ped(), 328.0f);
                        Ped.set_armour(Hacks.get_local_ped(), 50.0f);
                    }
                    break;
                case (int)WinVK.F8:
                    if (CheckBox_ClearWanted.IsChecked == true)
                    {
                        Ped.set_wanted_level(Hacks.get_local_ped(), 0);
                    }
                    break;
            }
        }));
    }

    private void MainThread()
    {
        while (true)
        {
            float oHealth = Ped.get_health(Hacks.get_local_ped());
            float oMaxHealth = Ped.get_max_health(Hacks.get_local_ped());
            float oArmor = Ped.get_armor(Hacks.get_local_ped());

            int oWanted = Ped.get_wanted_level(Hacks.get_local_ped());
            float oRunSpeed = Ped.get_run_speed(Hacks.get_local_ped());
            float oSwimSpeed = Ped.get_swim_speed(Hacks.get_local_ped());
            float oStealthSpeed = Ped.get_stealth_speed(Hacks.get_local_ped());

            bool oInVehicle = Ped.is_in_vehicle(Hacks.get_local_ped());
            byte oCurPassenger = Vehicle.get_cur_num_of_passenger(Ped.get_current_vehicle(Hacks.get_local_ped()));

            ////////////////////////////////

            if (Settings.Player.GodMode)
                Ped.set_godmode(Hacks.get_local_ped(), true);

            if (Settings.Player.AntiAFK)
                Globals.anti_afk(true);

            if (Settings.Player.NoRagdoll)
                Ped.set_no_ragdoll(Hacks.get_local_ped(), true);

            if (Settings.Player.WaterProof)
                Ped.set_waterproof(Hacks.get_local_ped(), true);

            if (Settings.Player.Invisible)
                Ped.set_invisible(Hacks.get_local_ped(), true);

            if (Settings.Player.UndeadOffRadar)
                Ped.set_max_health(Hacks.get_local_ped(), 0.0f);

            if (Settings.Player.EveryoneIgnore)
                Ped.set_everyone_ignore(Hacks.get_local_ped(), true);

            if (Settings.Player.CopsIgnore)
                Ped.set_cops_ignore(Hacks.get_local_ped(), true);

            if (Settings.Player.NoCollision)
                Ped.set_no_collision(Hacks.get_local_ped(), true);

            if (Settings.Player.AmmoModifier_InfiniteAmmo == 1) Ped.set_infinite_ammo(Hacks.get_local_ped(), true);
            else if (Settings.Player.AmmoModifier_InfiniteAmmo == 0) { Ped.set_infinite_ammo(Hacks.get_local_ped(), false); Settings.Player.AmmoModifier_InfiniteAmmo = -1; }

            if (Settings.Player.AmmoModifier_InfiniteClip == 1) Ped.set_infinite_clip(Hacks.get_local_ped(), true);
            else if (Settings.Player.AmmoModifier_InfiniteClip == 0) { Ped.set_infinite_clip(Hacks.get_local_ped(), false); Settings.Player.AmmoModifier_InfiniteClip = -1; }

            if (Settings.Vehicle.VehicleGodMode)
                Memory.Write<byte>(Globals.WorldPTR, Offsets.Vehicle.GodMode, 0x01);

            if (Settings.Vehicle.VehicleSeatbelt)
                Memory.Write<byte>(Globals.WorldPTR, Offsets.Player.Seatbelt, 0xC9);

            ////////////////////////////////

            Dispatcher.BeginInvoke(new Action(delegate
            {
                if (Slider_Health.Value != oHealth)
                    Slider_Health.Value = oHealth;

                if (Slider_MaxHealth.Value != oMaxHealth)
                    Slider_MaxHealth.Value = oMaxHealth;

                if (Slider_Armor.Value != oArmor)
                    Slider_Armor.Value = oArmor;

                if (Slider_Wanted.Value != oWanted)
                    Slider_Wanted.Value = oWanted;

                if (Slider_RunSpeed.Value != oRunSpeed)
                    Slider_RunSpeed.Value = oRunSpeed;

                if (Slider_SwimSpeed.Value != oSwimSpeed)
                    Slider_SwimSpeed.Value = oSwimSpeed;

                if (Slider_StealthSpeed.Value != oStealthSpeed)
                    Slider_StealthSpeed.Value = oStealthSpeed;
            }));

            Thread.Sleep(1000);
        }
    }

    private void SpecialThread()
    {
        while (true)
        {
            if (FrameFlagsExplosiveAmmo == 1) Ped.set_frame_flags_explosiveammo(Hacks.get_local_ped(), true);
            if (FrameFlagsFlamingAmmo == 1) Ped.set_frame_flags_flamingammo(Hacks.get_local_ped(), true);
            if (FrameFlagsExplosiveFists == 1) Ped.set_frame_flags_explosivefists(Hacks.get_local_ped(), true);
            if (FrameFlagsSuperJump == 1) Ped.set_frame_flags_superjump(Hacks.get_local_ped(), true);

            Thread.Sleep(1);
        }
    }

    private void CommonThread()
    {
        while (true)
        {
            if (Settings.Common.AutoClearWanted)
                Ped.set_wanted_level(Hacks.get_local_ped(), 0);

            if (Settings.Common.AutoKillNPC)
                Hacks.kill_npcs();

            if (Settings.Common.AutoKillHostilityNPC)
                Hacks.kill_enemies();

            if (Settings.Common.AutoKillPolice)
                Hacks.kill_cops();

            Thread.Sleep(200);
        }
    }

    private void Slider_Health_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) { Ped.set_health(Hacks.get_local_ped(), (float)Slider_Health.Value); }

    private void Slider_MaxHealth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) { Ped.set_max_health(Hacks.get_local_ped(), (float)Slider_MaxHealth.Value); }

    private void Slider_Armor_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) { Ped.set_armour(Hacks.get_local_ped(), (float)Slider_Armor.Value); }

    private void Slider_Wanted_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) { Ped.set_wanted_level(Hacks.get_local_ped(), (int)Slider_Wanted.Value); }

    private void Slider_RunSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) { Ped.set_run_speed(Hacks.get_local_ped(), (float)Slider_RunSpeed.Value); }

    private void Slider_SwimSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) { Ped.set_swim_speed(Hacks.get_local_ped(), (float)Slider_SwimSpeed.Value); }

    private void Slider_StealthSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) { Ped.set_stealth_speed(Hacks.get_local_ped(), (float)Slider_StealthSpeed.Value); }

    private void CheckBox_PlayerGodMode_Click(object sender, RoutedEventArgs e)
    {
        Ped.set_godmode(Hacks.get_local_ped(), (CheckBox_PlayerGodMode.IsChecked == true));
        Settings.Player.GodMode = CheckBox_PlayerGodMode.IsChecked == true;
    }

    private void CheckBox_AntiAFK_Click(object sender, RoutedEventArgs e)
    {
        Globals.anti_afk((CheckBox_AntiAFK.IsChecked == true));
        Settings.Player.AntiAFK = CheckBox_AntiAFK.IsChecked == true;
    }

    private void CheckBox_WaterProof_Click(object sender, RoutedEventArgs e)
    {
        Ped.set_waterproof(Hacks.get_local_ped(), (CheckBox_WaterProof.IsChecked == true));
        Settings.Player.WaterProof = CheckBox_WaterProof.IsChecked == true;
    }

    private void CheckBox_Invisibility_Click(object sender, RoutedEventArgs e)
    {
        Ped.set_invisible(Hacks.get_local_ped(), (CheckBox_Invisibility.IsChecked == true));
        Settings.Player.Invisible = CheckBox_Invisibility.IsChecked == true;
    }

    private void CheckBox_UndeadOffRadar_Click(object sender, RoutedEventArgs e)
    {
        Ped.set_max_health(Hacks.get_local_ped(), ((CheckBox_UndeadOffRadar.IsChecked == true) ? 0.0f : 328.0f));
        Settings.Player.UndeadOffRadar = CheckBox_UndeadOffRadar.IsChecked == true;
    }

    private void CheckBox_NoRagdoll_Click(object sender, RoutedEventArgs e)
    {
        Ped.set_no_ragdoll(Hacks.get_local_ped(), (CheckBox_NoRagdoll.IsChecked == true));
        Settings.Player.NoRagdoll = CheckBox_NoRagdoll.IsChecked == true;
    }

    private void CheckBox_NPCIgnore_Click(object sender, RoutedEventArgs e)
    {
        Ped.set_everyone_ignore(Hacks.get_local_ped(), (CheckBox_NPCIgnore.IsChecked == true));
        Ped.set_cops_ignore(Hacks.get_local_ped(), (CheckBox_PoliceIgnore.IsChecked == true));
        Settings.Player.EveryoneIgnore = CheckBox_NPCIgnore.IsChecked == true;
        Settings.Player.CopsIgnore = CheckBox_PoliceIgnore.IsChecked == true;
    }

    private void CheckBox_AutoClearWanted_Click(object sender, RoutedEventArgs e)
    {
        Ped.set_wanted_level(Hacks.get_local_ped(), 0);
        Settings.Common.AutoClearWanted = CheckBox_AutoClearWanted.IsChecked == true;
    }

    private void CheckBox_AutoKillNPC_Click(object sender, RoutedEventArgs e)
    {
        Hacks.kill_npcs();
        Settings.Common.AutoKillNPC = CheckBox_AutoKillNPC.IsChecked == true;
    }

    private void CheckBox_AutoKillHostilityNPC_Click(object sender, RoutedEventArgs e)
    {
        Hacks.kill_enemies();
        Settings.Common.AutoKillHostilityNPC = CheckBox_AutoKillHostilityNPC.IsChecked == true;
    }

    private void CheckBox_AutoKillPolice_Click(object sender, RoutedEventArgs e)
    {
        Hacks.kill_cops();
        Settings.Common.AutoKillPolice = CheckBox_AutoKillPolice.IsChecked == true;
    }

    private void CheckBox_FrameFlags_Click(object sender, RoutedEventArgs e)
    {
        FrameFlagsExplosiveAmmo  = ((CheckBox_FrameFlagsExplosiveAmmo.IsChecked == true) ? 1 : 0);
        FrameFlagsFlamingAmmo    = ((CheckBox_FrameFlagsFlamingAmmo.IsChecked == true) ? 1 : 0);
        FrameFlagsExplosiveFists = ((CheckBox_FrameFlagsExplosiveFists.IsChecked == true) ? 1 : 0);
        FrameFlagsSuperJump      = ((CheckBox_FrameFlagsSuperJump.IsChecked == true) ? 1 : 0);
    }

    private void CheckBox_NoCollision_Click(object sender, RoutedEventArgs e)
    {
        Ped.set_no_collision(Hacks.get_local_ped(), CheckBox_NoCollision.IsChecked == true);
        Settings.Player.NoCollision = CheckBox_NoCollision.IsChecked == true;
    }

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

        Ped.set_health(Hacks.get_local_ped(), 328.0f);
        Ped.set_max_health(Hacks.get_local_ped(), 328.0f);
        Ped.set_armour(Hacks.get_local_ped(), 50.0f);
    }

    private void Button_ClearWanted_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Ped.set_wanted_level(Hacks.get_local_ped(), 0);
    }

    private void Button_Suicide_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Ped.set_health(Hacks.get_local_ped(), 0.0f);
    }

    private void Slider_MovingFoward_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Settings.Forward = (float)Slider_MovingFoward.Value;
    }
}
