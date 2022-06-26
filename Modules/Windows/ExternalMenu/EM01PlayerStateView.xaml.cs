﻿using GTA5OnlineTools.Common.Utils;
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
    private int FrameFlag = 0;
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
                        Weapon.FillCurrentAmmo();
                    }
                    break;
                case (int)WinVK.F4:
                    if (CheckBox_MovingFoward.IsChecked == true)
                    {
                        Teleport.MovingFoward();
                    }
                    break;
                case (int)WinVK.F5:
                    if (CheckBox_ToWaypoint.IsChecked == true)
                    {
                        Teleport.ToWaypoint();
                    }
                    break;
                case (int)WinVK.F6:
                    if (CheckBox_ToObjective.IsChecked == true)
                    {
                        Teleport.ToObjective();
                    }
                    break;
                case (int)WinVK.F7:
                    if (CheckBox_FillHealthArmor.IsChecked == true)
                    {
                        Player.FillHealthArmor();
                    }
                    break;
                case (int)WinVK.F8:
                    if (CheckBox_ClearWanted.IsChecked == true)
                    {
                        Player.WantedLevel(0x00);
                    }
                    break;
            }
        }));
    }

    private void MainThread()
    {
        while (true)
        {
            float oHealth = Ped.get_health(Hacks.GetLocalPed());
            float oMaxHealth = Ped.get_max_health(Hacks.GetLocalPed());
            float oArmor = Ped.get_armor(Hacks.GetLocalPed());

            int oWanted = Hacks.Ped_get_wanted_level(Hacks.GetLocalPed());
            float oRunSpeed = Hacks.Ped_get_run_speed(Hacks.GetLocalPed());
            float oSwimSpeed = Hacks.Ped_get_swim_speed(Hacks.GetLocalPed());
            float oStealthSpeed = Hacks.Ped_get_stealth_speed(Hacks.GetLocalPed());

            bool oInVehicle = Ped.is_in_vehicle(Hacks.GetLocalPed());
            byte oCurPassenger = Vehicle.get_cur_num_of_passenger(Ped.get_current_vehicle(Hacks.GetLocalPed()));

            ////////////////////////////////

            if (Settings.Player.GodMode)
                Player.GodMode(true);

            if (Settings.Player.AntiAFK)
                Player.AntiAFK(true);

            if (Settings.Player.NoRagdoll)
                Player.NoRagdoll(true);

            if (Settings.Player.NoCollision)
                Memory.Write(Globals.WorldPTR, Offsets.Player.NoCollision, -1.0f);

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
            if (FrameFlagsExplosiveAmmo == 1) Hacks.Ped_set_frame_flags_explosiveammo(Hacks.GetLocalPed(), true);
            if (FrameFlagsFlamingAmmo == 1) Hacks.Ped_set_frame_flags_flamingammo(Hacks.GetLocalPed(), true);
            if (FrameFlagsExplosiveFists == 1) Hacks.Ped_set_frame_flags_explosivefists(Hacks.GetLocalPed(), true);
            if (FrameFlagsSuperJump == 1) Hacks.Ped_set_frame_flags_superjump(Hacks.GetLocalPed(), true);

            Thread.Sleep(1);
        }
    }

    private void CommonThread()
    {
        while (true)
        {
            if (Settings.Common.AutoClearWanted)
                Player.WantedLevel(0x00);

            if (Settings.Common.AutoKillNPC)
                World.KillNPC(false);

            if (Settings.Common.AutoKillHostilityNPC)
                World.KillNPC(true);

            if (Settings.Common.AutoKillPolice)
                World.KillPolice();

            Thread.Sleep(200);
        }
    }

    private void Slider_Health_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) { Ped.set_health(Hacks.GetLocalPed(), (float)Slider_Health.Value); }

    private void Slider_MaxHealth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) { Ped.set_max_health(Hacks.GetLocalPed(), (float)Slider_MaxHealth.Value); }

    private void Slider_Armor_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) { Ped.set_armour(Hacks.GetLocalPed(), (float)Slider_Armor.Value); }

    private void Slider_Wanted_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) { Hacks.Ped_set_wanted_level(Hacks.GetLocalPed(), (int)Slider_Wanted.Value); }

    private void Slider_RunSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) { Hacks.Ped_set_run_speed(Hacks.GetLocalPed(), (float)Slider_RunSpeed.Value); }

    private void Slider_SwimSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) { Hacks.Ped_set_swim_speed(Hacks.GetLocalPed(), (float)Slider_SwimSpeed.Value); }

    private void Slider_StealthSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) { Hacks.Ped_set_stealth_speed(Hacks.GetLocalPed(), (float)Slider_StealthSpeed.Value); }

    private void CheckBox_PlayerGodMode_Click(object sender, RoutedEventArgs e)
    {
        Player.GodMode(CheckBox_PlayerGodMode.IsChecked == true);
        Settings.Player.GodMode = CheckBox_PlayerGodMode.IsChecked == true;
    }

    private void CheckBox_AntiAFK_Click(object sender, RoutedEventArgs e)
    {
        Player.AntiAFK(CheckBox_AntiAFK.IsChecked == true);
        Settings.Player.AntiAFK = CheckBox_AntiAFK.IsChecked == true;
    }

    private void CheckBox_WaterProof_Click(object sender, RoutedEventArgs e)
    {
        Player.WaterProof(CheckBox_WaterProof.IsChecked == true);
    }

    private void CheckBox_Invisibility_Click(object sender, RoutedEventArgs e)
    {
        Player.Invisibility(CheckBox_Invisibility.IsChecked == true);
    }

    private void CheckBox_UndeadOffRadar_Click(object sender, RoutedEventArgs e)
    {
        Player.UndeadOffRadar(CheckBox_UndeadOffRadar.IsChecked == true);
    }

    private void CheckBox_NoRagdoll_Click(object sender, RoutedEventArgs e)
    {
        Player.NoRagdoll(CheckBox_NoRagdoll.IsChecked == true);
        Settings.Player.NoRagdoll = CheckBox_NoRagdoll.IsChecked == true;
    }

    private void CheckBox_NPCIgnore_Click(object sender, RoutedEventArgs e)
    {
        if (CheckBox_NPCIgnore.IsChecked == true && CheckBox_PoliceIgnore.IsChecked == false)
        {
            Memory.Write<byte>(Globals.WorldPTR, Offsets.NPCIgnore, 0x04);
        }
        else if (CheckBox_NPCIgnore.IsChecked == false && CheckBox_PoliceIgnore.IsChecked == true)
        {
            Memory.Write<byte>(Globals.WorldPTR, Offsets.NPCIgnore, 0xC3);
        }
        else if (CheckBox_NPCIgnore.IsChecked == true && CheckBox_PoliceIgnore.IsChecked == true)
        {
            Memory.Write<byte>(Globals.WorldPTR, Offsets.NPCIgnore, 0xC7);
        }
        else
        {
            Memory.Write<byte>(Globals.WorldPTR, Offsets.NPCIgnore, 0x00);
        }
    }

    private void CheckBox_AutoClearWanted_Click(object sender, RoutedEventArgs e)
    {
        Player.WantedLevel(0x00);
        Settings.Common.AutoClearWanted = CheckBox_AutoClearWanted.IsChecked == true;
    }

    private void CheckBox_AutoKillNPC_Click(object sender, RoutedEventArgs e)
    {
        World.KillNPC(false);
        Settings.Common.AutoKillNPC = CheckBox_AutoKillNPC.IsChecked == true;
    }

    private void CheckBox_AutoKillHostilityNPC_Click(object sender, RoutedEventArgs e)
    {
        World.KillNPC(true);
        Settings.Common.AutoKillHostilityNPC = CheckBox_AutoKillHostilityNPC.IsChecked == true;
    }

    private void CheckBox_AutoKillPolice_Click(object sender, RoutedEventArgs e)
    {
        World.KillPolice();
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
        Player.NoCollision(CheckBox_NoCollision.IsChecked == true);
        Settings.Player.NoCollision = CheckBox_NoCollision.IsChecked == true;
    }

    private void Button_ToWaypoint_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Teleport.ToWaypoint();
    }

    private void Button_ToObjective_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Teleport.ToObjective();
    }

    private void Button_FillHealthArmor_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Player.FillHealthArmor();
    }

    private void Button_ClearWanted_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Player.WantedLevel(0x00);
    }

    private void Button_Suicide_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Player.Suicide();
    }

    private void Slider_MovingFoward_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Settings.Forward = (float)Slider_MovingFoward.Value;
    }
}
