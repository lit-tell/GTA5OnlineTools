using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using GTA5OnlineTools.ViewModels;
using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.Core;
using GTA5OnlineTools.Features.Data;
using GTA5OnlineTools.Features.SDK;

namespace GTA5OnlineTools.Modules.Windows
{
    /// <summary>
    /// ExternalMenuWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ExternalMenuWindow : Window
    {
        // 程序自身的窗口句柄
        private IntPtr MyHandle;

        // 多线程定时器
        private System.Timers.Timer MainTimersTimer;
        private Thread SpecialThread;

        private KeysManager MainKeysManager;

        // 显示玩家列表
        private List<PlayerData> playerData = new List<PlayerData>();

        // 特殊功能
        private NopHandle FrameFlagsZeroWriteCallNoper;
        private int FrameFlag;

        public ExternalMenuWindow()
        {
            InitializeComponent();

            //Dispatcher.BeginInvoke(new Action(delegate
            //{

            //}));
        }

        private void Window_ExternalMenu_Loaded(object sender, RoutedEventArgs e)
        {
            // 获取自身窗口句柄
            MyHandle = new WindowInteropHelper(this).Handle;

            // 载具列表
            for (int i = 0; i < VehicleData.VehicleDataClass.Count; i++)
            {
                ListBox_VehicleList.Items.Add(VehicleData.VehicleDataClass[i].VName);
            }
            ListBox_VehicleList.SelectedIndex = 0;

            // 武器列表
            for (int i = 0; i < WeaponData.WeaponDataClass.Count; i++)
            {
                ListBox_WeaponList.Items.Add(WeaponData.WeaponDataClass[i].WType);
            }
            ListBox_WeaponList.SelectedIndex = 0;

            // 子弹类型
            for (int i = 0; i < MiscData.ImpactExplosions.Count; i++)
            {
                ComboBox_ImpactExplosion.Items.Add(MiscData.ImpactExplosions[i].Name);
            }
            ComboBox_ImpactExplosion.SelectedIndex = 0;

            /////////////////////////////////////////////

            Task t = new Task(() =>
            {
                Memory.Initialize(CoreUtil.TargetAppName);

                Offsets.Mask.TempPTR = Memory.FindPattern(Offsets.Mask.WorldPTR);
                Globals.WorldPTR = Memory.Rip_37(Offsets.Mask.TempPTR);

                Offsets.Mask.TempPTR = Memory.FindPattern(Offsets.Mask.BlipPTR);
                Globals.BlipPTR = Memory.Rip_37(Offsets.Mask.TempPTR);

                Offsets.Mask.TempPTR = Memory.FindPattern(Offsets.Mask.GlobalPTR);
                Globals.GlobalPTR = Memory.Rip_37(Offsets.Mask.TempPTR);

                Offsets.Mask.TempPTR = Memory.FindPattern(Offsets.Mask.PlayerNameChatPTR);
                Globals.PlayerNameChatterPTR = Memory.Rip_37(Offsets.Mask.TempPTR);

                Offsets.Mask.TempPTR = Memory.FindPattern(Offsets.Mask.PlayerListPTR);
                Globals.PlayerListPTR = Memory.Rip_37(Offsets.Mask.TempPTR);

                Offsets.Mask.TempPTR = Memory.FindPattern(Offsets.Mask.ReplayInterfacePTR);
                Globals.ReplayInterfacePTR = Memory.Rip_37(Offsets.Mask.TempPTR);

                Offsets.Mask.TempPTR = Memory.FindPattern(Offsets.Mask.WeatherPTR);
                Globals.WeatherPTR = Memory.Rip_6A(Offsets.Mask.TempPTR);

                Offsets.Mask.TempPTR = Memory.FindPattern(Offsets.Mask.UnkModel);
                Globals.UnkModelPTR = Memory.Rip_37(Offsets.Mask.TempPTR);

                Offsets.Mask.TempPTR = Memory.FindPattern(Offsets.Mask.PickupData);
                Globals.PickupDataPTR = Memory.Rip_37(Offsets.Mask.TempPTR);

                Globals.FrameFlagsZeroWriteCallOffset = Memory.FindPattern(Offsets.Mask.FrameFlagsZeroWriteCall) + 0x07;
                FrameFlagsZeroWriteCallNoper = new NopHandle(Globals.FrameFlagsZeroWriteCallOffset, 5);
            });
            t.Start();

            /////////////////////////////////////////////

            MainTimersTimer = new System.Timers.Timer();
            MainTimersTimer.Elapsed += new System.Timers.ElapsedEventHandler(MainTimersTimer_Tick);
            MainTimersTimer.Interval = 500;
            MainTimersTimer.AutoReset = true;
            MainTimersTimer.Start();

            SpecialThread = new Thread(new ParameterizedThreadStart(SpecialThread_Thread));
            SpecialThread.IsBackground = true;
            SpecialThread.Start();

            MainKeysManager = new KeysManager();
            MainKeysManager.AddKey(WinVK.F9);
            MainKeysManager.AddKey(WinVK.F3);
            MainKeysManager.AddKey(WinVK.F4);
            MainKeysManager.AddKey(WinVK.F5);
            MainKeysManager.AddKey(WinVK.F6);
            MainKeysManager.AddKey(WinVK.F7);
            MainKeysManager.AddKey(WinVK.F8);
            MainKeysManager.AddKey(WinVK.BACK);
            MainKeysManager.KeyDownEvent += new KeysManager.KeyHandler(MyKeyDownEvent);
        }

        private void Window_ExternalMenu_Closing(object sender, CancelEventArgs e)
        {
            MainTimersTimer?.Stop();

            FrameFlagsZeroWriteCallNoper.ReStore();
        }

        #region 自定义方法区域
        private void ShowWindow()
        {
            Settings.ShowWindow = !Settings.ShowWindow;
            if (Settings.ShowWindow)
            {
                Show();
                Topmost = true;
                Topmost = false;
                WindowState = WindowState.Normal;
                WinAPI.SetForegroundWindow(MyHandle);
            }
            else
            {
                Hide();
                Topmost = false;
                Memory.SetForegroundWindow();
            }
        }
        #endregion

        private void MyKeyDownEvent(int keyId, string keyName)
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                switch (keyId)
                {
                    case (int)WinVK.F9:
                        ShowWindow();
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
                    case (int)WinVK.BACK:
                        if (CheckBox_NoCollision.IsChecked == true)
                        {
                            Settings.Player.NoCollision = !Settings.Player.NoCollision;
                        }
                        break;
                }
            }));
        }

        private void MainTimersTimer_Tick(object sender, EventArgs e)
        {
            float oHealth = Memory.Read<float>(Globals.WorldPTR, Offsets.Player.Health);
            float oMaxHealth = Memory.Read<float>(Globals.WorldPTR, Offsets.Player.MaxHealth);
            float oArmor = Memory.Read<float>(Globals.WorldPTR, Offsets.Player.Armor);

            byte oWanted = Memory.Read<byte>(Globals.WorldPTR, Offsets.Player.Wanted);
            float oRunSpeed = Memory.Read<float>(Globals.WorldPTR, Offsets.Player.RunSpeed);
            float oSwimSpeed = Memory.Read<float>(Globals.WorldPTR, Offsets.Player.SwimSpeed);
            float oStealthSpeed = Memory.Read<float>(Globals.WorldPTR, Offsets.Player.StealthSpeed);

            byte oInVehicle = Memory.Read<byte>(Globals.WorldPTR, Offsets.InVehicle);
            byte oCurPassenger = Memory.Read<byte>(Globals.WorldPTR, Offsets.Vehicle.CurPassenger);

            ////////////////////////////////

            if (Settings.Player.GodMode)
            {
                Player.GodMode(true);
            }

            if (Settings.Player.AntiAFK)
            {
                Player.AntiAFK(true);
            }

            if (Settings.Player.NoRagdoll)
            {
                Player.NoRagdoll(true);
            }

            if (Settings.Player.NoCollision)
            {
                Memory.Write(Globals.WorldPTR, Offsets.Player.NoCollision, -1.0f);
            }

            if (Settings.Vehicle.VehicleGodMode)
            {
                Memory.Write<byte>(Globals.WorldPTR, Offsets.Vehicle.GodMode, 0x01);
            }

            if (Settings.Vehicle.VehicleSeatbelt)
            {
                Memory.Write<byte>(Globals.WorldPTR, Offsets.Player.Seatbelt, 0xC9);
            }

            ////////////////////////////////

            Dispatcher.BeginInvoke(new Action(delegate
            {
                if (Slider_Health.Value != oHealth)
                {
                    Slider_Health.Value = oHealth;
                }

                if (Slider_MaxHealth.Value != oMaxHealth)
                {
                    Slider_MaxHealth.Value = oMaxHealth;
                }

                if (Slider_Armor.Value != oArmor)
                {
                    Slider_Armor.Value = oArmor;
                }

                if (Slider_Wanted.Value != oWanted)
                {
                    Slider_Wanted.Value = oWanted;
                }

                if (Slider_RunSpeed.Value != oRunSpeed)
                {
                    Slider_RunSpeed.Value = oRunSpeed;
                }

                if (Slider_SwimSpeed.Value != oSwimSpeed)
                {
                    Slider_SwimSpeed.Value = oSwimSpeed;
                }

                if (Slider_StealthSpeed.Value != oStealthSpeed)
                {
                    Slider_StealthSpeed.Value = oStealthSpeed;
                }

                ////////////////////////////////

                TextBlock_InVehicle.Text = $" {oInVehicle}";
                TextBlock_CurrentPassenger.Text = $" {oCurPassenger:0}";
            }));
        }

        private void SpecialThread_Thread(object sender)
        {
            while (true)
            {
                if (Settings.Special.AutoClearWanted)
                {
                    Player.WantedLevel(0x00);
                }

                Thread.Sleep(1);
            }
        }

        private void SetFrameFlag(int value)
        {
            FrameFlag += value;

            if (FrameFlag == 0 && FrameFlagsZeroWriteCallNoper.IsNoped)
            {
                FrameFlagsZeroWriteCallNoper.ReStore();
            }
            else
            {
                if (!FrameFlagsZeroWriteCallNoper.IsNoped)
                {
                    FrameFlagsZeroWriteCallNoper.Nop();
                }

                Memory.Write<int>(Globals.WorldPTR, Offsets.SpecialAmmo, FrameFlag);
            }
        }

        private void Slider_Health_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Memory.Write<float>(Globals.WorldPTR, Offsets.Player.Health, (float)Slider_Health.Value);
        }

        private void Slider_MaxHealth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Memory.Write<float>(Globals.WorldPTR, Offsets.Player.MaxHealth, (float)Slider_MaxHealth.Value);
        }

        private void Slider_Armor_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Memory.Write<float>(Globals.WorldPTR, Offsets.Player.Armor, (float)Slider_Armor.Value);
        }

        private void Slider_Wanted_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Memory.Write<byte>(Globals.WorldPTR, Offsets.Player.Wanted, (byte)Slider_Wanted.Value);
        }

        private void Slider_RunSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Memory.Write<float>(Globals.WorldPTR, Offsets.Player.RunSpeed, (float)Slider_RunSpeed.Value);
        }

        private void Slider_SwimSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Memory.Write<float>(Globals.WorldPTR, Offsets.Player.SwimSpeed, (float)Slider_SwimSpeed.Value);
        }

        private void Slider_StealthSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Memory.Write<float>(Globals.WorldPTR, Offsets.Player.StealthSpeed, (float)Slider_StealthSpeed.Value);
        }

        private void Button_ExitApp_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Sessions_Click(object sender, RoutedEventArgs e)
        {
            var str = (e.OriginalSource as Button).Content.ToString();

            int index = MiscData.Sessions.FindIndex(t => t.Name == str);
            if (index != -1)
            {
                Online.LoadSession(MiscData.Sessions[index].ID);
            }
        }

        private void Button_Disconnect_Click(object sender, RoutedEventArgs e)
        {
            Online.Disconnect();
        }

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

        private void CheckBox_UndeadOffRadar_Click(object sender, RoutedEventArgs e)
        {
            Player.UndeadOffRadar(CheckBox_UndeadOffRadar.IsChecked == true);
        }

        private void CheckBox_NoRagdoll_Click(object sender, RoutedEventArgs e)
        {
            Player.NoRagdoll(CheckBox_NoRagdoll.IsChecked == true);
            Settings.Player.NoRagdoll = CheckBox_NoRagdoll.IsChecked == true;
        }

        private void ListBox_VehicleList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListBox_VehicleList.SelectedIndex;

            if (index != -1)
            {
                ListBox_VehicleInfo.Items.Clear();

                for (int i = 0; i < VehicleData.VehicleDataClass[index].VCode.Count; i++)
                {
                    ListBox_VehicleInfo.Items.Add(VehicleData.VehicleDataClass[index].VCode[i].VName);
                }

                ListBox_VehicleInfo.SelectedIndex = 0;
            }
        }

        private void ListBox_VehicleInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index1 = ListBox_VehicleList.SelectedIndex;
            int index2 = ListBox_VehicleInfo.SelectedIndex;

            if (index1 != -1 && index2 != -1)
            {
                TempData.VCode = VehicleData.VehicleDataClass[index1].VCode[index2].VCode;
                TempData.VMod = VehicleData.VehicleDataClass[index1].VCode[index2].VMod;
            }
        }

        private void Button_SpawnVehicle_Click(object sender, RoutedEventArgs e)
        {


            string str = (e.OriginalSource as Button).Content.ToString();

            if (TempData.VCode != null && TempData.VMod != null)
            {
                if (str == "刷出线上载具A")
                {
                    Vehicle.SpawnVehicle(TempData.VCode, 5, -255.0f, TempData.VMod);
                }
                else
                {
                    Vehicle.SpawnVehicle(TempData.VCode, 5, 0.0f, TempData.VMod);
                }
            }
        }

        private void CheckBox_FireAmmo_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBox_FireAmmo.IsChecked == true)
            {
                SetFrameFlag((int)EnumData.FrameFlags.FireAmmo);
            }
            else
            {
                SetFrameFlag(-(int)EnumData.FrameFlags.FireAmmo);
            }
        }

        private void CheckBox_ExplosiveAmmo_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBox_ExplosiveAmmo.IsChecked == true)
            {
                SetFrameFlag((int)EnumData.FrameFlags.ExplosiveAmmo);
            }
            else
            {
                SetFrameFlag(-(int)EnumData.FrameFlags.ExplosiveAmmo);
            }
        }

        private void CheckBox_SuperJump_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBox_SuperJump.IsChecked == true)
            {
                SetFrameFlag((int)EnumData.FrameFlags.SuperJump);
            }
            else
            {
                SetFrameFlag(-(int)EnumData.FrameFlags.SuperJump);
            }
        }

        //private void CheckBox_ExpandedRadar_Click(object sender, RoutedEventArgs e)
        //{
        //    Player.ExpandedRadar(CheckBox_ExpandedRadar.IsChecked == true);
        //}

        private void CheckBox_AutoClearWanted_Click(object sender, RoutedEventArgs e)
        {
            Player.WantedLevel(0x00);
            Settings.Special.AutoClearWanted = CheckBox_AutoClearWanted.IsChecked == true;
        }

        private void CheckBox_NoCollision_Click(object sender, RoutedEventArgs e)
        {
            Player.NoCollision(CheckBox_NoCollision.IsChecked == true);
            Settings.Player.NoCollision = CheckBox_NoCollision.IsChecked == true;
        }

        private void CheckBox_OnlineSnow_Click(object sender, RoutedEventArgs e)
        {
            Online.SessionSnow(CheckBox_OnlineSnow.IsChecked == true);
        }

        private void CheckBox_AllowSellOnNonPublic_Click(object sender, RoutedEventArgs e)
        {
            Online.AllowSellOnNonPublic(CheckBox_AllowSellOnNonPublic.IsChecked == true);
        }

        private void CheckBox_RemovePassiveModeCooldown_Click(object sender, RoutedEventArgs e)
        {
            Online.RemovePassiveModeCooldown(CheckBox_RemovePassiveModeCooldown.IsChecked == true);
        }

        private void CheckBox_RemoveSuicideCooldown_Click(object sender, RoutedEventArgs e)
        {
            Online.RemoveSuicideCooldown(CheckBox_RemoveSuicideCooldown.IsChecked == true);
        }

        private void CheckBox_DisableOrbitalCooldown_Click(object sender, RoutedEventArgs e)
        {
            Online.DisableOrbitalCooldown(CheckBox_DisableOrbitalCooldown.IsChecked == true);
        }

        private void Button_GetInOnlinePV_Click(object sender, RoutedEventArgs e)
        {


            Online.GetInOnlinePV();
        }

        private void Button_FillVehicleHealth_Click(object sender, RoutedEventArgs e)
        {


            Vehicle.FillHealth();
        }

        private void CheckBox_OffRadar_Click(object sender, RoutedEventArgs e)
        {
            Online.OffRadar(CheckBox_OffRadar.IsChecked == true);
        }

        private void CheckBox_GhostOrganization_Click(object sender, RoutedEventArgs e)
        {
            Online.GhostOrganization(CheckBox_GhostOrganization.IsChecked == true);
        }

        private void CheckBox_BribeOrBlindCops_Click(object sender, RoutedEventArgs e)
        {
            Online.BribeOrBlindCops(CheckBox_BribeOrBlindCops.IsChecked == true);
        }

        private void CheckBox_BribeAuthorities_Click(object sender, RoutedEventArgs e)
        {
            Online.BribeAuthorities(CheckBox_BribeAuthorities.IsChecked == true);
        }

        private void CheckBox_RevealPlayers_Click(object sender, RoutedEventArgs e)
        {
            Online.RevealPlayers(CheckBox_RevealPlayers.IsChecked == true);
        }

        private void Button_Blips_Click(object sender, RoutedEventArgs e)
        {
            var str = (e.OriginalSource as Button).Content.ToString();

            int index = MiscData.Blips.FindIndex(t => t.Name == str);
            if (index != -1)
            {
                Teleport.ToBlips(MiscData.Blips[index].ID);
            }
        }

        private void CheckBox_InstantBullShark_Click(object sender, RoutedEventArgs e)
        {
            Online.InstantBullShark(CheckBox_InstantBullShark.IsChecked == true);
        }

        private void CheckBox_BackupHeli_Click(object sender, RoutedEventArgs e)
        {
            Online.BackupHeli(CheckBox_BackupHeli.IsChecked == true);
        }

        private void CheckBox_Airstrike_Click(object sender, RoutedEventArgs e)
        {
            Online.Airstrike(CheckBox_Airstrike.IsChecked == true);
        }

        private void Button_ToWaypoint_Click(object sender, RoutedEventArgs e)
        {


            Teleport.ToWaypoint();
        }

        private void Button_ToObjective_Click(object sender, RoutedEventArgs e)
        {


            Teleport.ToObjective();
        }

        private void Button_FillHealthArmor_Click(object sender, RoutedEventArgs e)
        {


            Player.FillHealthArmor();
        }

        private void Button_ClearWanted_Click(object sender, RoutedEventArgs e)
        {


            Player.WantedLevel(0x00);
        }

        private void CheckBox_VehicleGodMode_Click(object sender, RoutedEventArgs e)
        {
            Vehicle.GodMode(Settings.Vehicle.VehicleGodMode = true);
            Settings.Vehicle.VehicleGodMode = CheckBox_VehicleGodMode.IsChecked == true;
        }

        private void CheckBox_VehicleSeatbelt_Click(object sender, RoutedEventArgs e)
        {
            Vehicle.Seatbelt(CheckBox_VehicleSeatbelt.IsChecked == true);
            Settings.Vehicle.VehicleSeatbelt = CheckBox_VehicleSeatbelt.IsChecked == true;
        }

        private void Button_Suicide_Click(object sender, RoutedEventArgs e)
        {


            Player.Suicide();
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

        private void Button_KillAllPeds_Click(object sender, RoutedEventArgs e)
        {


            World.KillNPC(false);
        }
        private void Button_KillAllHostilityPeds_Click(object sender, RoutedEventArgs e)
        {


            World.KillNPC(true);
        }

        private void Button_DestroyAllVehicles_Click(object sender, RoutedEventArgs e)
        {


            World.DestroyAllVehicles();
        }

        private void Button_DestroyAllNPCVehicles_Click(object sender, RoutedEventArgs e)
        {


            World.DestroyNPCVehicles(false);
        }

        private void Button_DestroyAllHostilityNPCVehicles_Click(object sender, RoutedEventArgs e)
        {


            World.DestroyNPCVehicles(true);
        }

        private void Button_RefreshPlayerList_Click(object sender, RoutedEventArgs e)
        {


            playerData.Clear();
            ListBox_PlayerList.Items.Clear();

            for (int i = 0; i < 32; i++)
            {
                long pCNetGamePlayer = Memory.Read<long>(Globals.PlayerListPTR, new int[] { 0x180 + (i * 8) });
                if (pCNetGamePlayer == 0)
                {
                    continue;
                }

                long pCPlayerInfo = Memory.Read<long>(pCNetGamePlayer + 0xA0);
                if (pCPlayerInfo == 0)
                {
                    continue;
                }

                long pCPed = Memory.Read<long>(pCPlayerInfo + 0x01E8);
                if (pCPed == 0)
                {
                    continue;
                }

                long pCNavigation = Memory.Read<long>(pCPed + 0x30, null);
                if (pCNavigation == 0)
                {
                    continue;
                }

                ////////////////////////////////////////////

                playerData.Add(new PlayerData()
                {
                    RID = Memory.Read<long>(pCPlayerInfo + 0x90),
                    Name = Memory.ReadString(pCPlayerInfo + 0xA4, null, 20),

                    PlayerInfo = new PlayerInfo()
                    {
                        Host = Hacks.ReadGA<int>(1630317 + 1 + (i * 595) + 10) == 1 ? true : false,
                        Health = Memory.Read<float>(pCPed + 0x280),
                        MaxHealth = Memory.Read<float>(pCPed + 0x2A0),
                        GodMode = Memory.Read<byte>(pCPed + 0x189) == 0x01 ? true : false,
                        NoRagdoll = Memory.Read<byte>(pCPed + 0x10B8) == 0x01 ? true : false,
                        WantedLevel = Memory.Read<byte>(pCPlayerInfo + 0x888),
                        RunSpeed = Memory.Read<float>(pCPlayerInfo + 0xCF0),
                        V3Pos = Memory.Read<Vector3>(pCNavigation + 0x50)
                    },
                });
            }

            int index = 0;

            foreach (var item in playerData)
            {
                if (item.PlayerInfo.Host)
                {
                    index++;
                    ListBox_PlayerList.Items.Add($"{index}  {item.Name} [房主]");
                }
                else
                {
                    index++;
                    ListBox_PlayerList.Items.Add($"{index}  {item.Name}");
                }
            }
        }

        private void ListBox_PlayerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextBox_PlayerInfo.Clear();

            if (ListBox_PlayerList.SelectedItem != null)
            {
                int index = ListBox_PlayerList.SelectedIndex;

                if (index != -1)
                {
                    TextBox_PlayerInfo.AppendText($"战局房主 : {playerData[index].PlayerInfo.Host}\r\n\r\n");

                    TextBox_PlayerInfo.AppendText($"玩家RID : {playerData[index].RID}\r\n");
                    TextBox_PlayerInfo.AppendText($"玩家昵称 : {playerData[index].Name}\r\n\r\n");

                    TextBox_PlayerInfo.AppendText($"当前生命值 : {playerData[index].PlayerInfo.Health:0.0}\r\n");
                    TextBox_PlayerInfo.AppendText($"最大生命值 : {playerData[index].PlayerInfo.MaxHealth:0.0}\r\n\r\n");

                    TextBox_PlayerInfo.AppendText($"无敌状态 : {playerData[index].PlayerInfo.GodMode}\r\n");
                    TextBox_PlayerInfo.AppendText($"无布娃娃 : {playerData[index].PlayerInfo.NoRagdoll}\r\n\r\n");

                    TextBox_PlayerInfo.AppendText($"通缉等级 : {playerData[index].PlayerInfo.WantedLevel}\r\n");
                    TextBox_PlayerInfo.AppendText($"奔跑速度 : {playerData[index].PlayerInfo.RunSpeed:0.0}\r\n\r\n");

                    TextBox_PlayerInfo.AppendText($"X : {playerData[index].PlayerInfo.V3Pos.X:0.0000}\r\n");
                    TextBox_PlayerInfo.AppendText($"Y : {playerData[index].PlayerInfo.V3Pos.Y:0.0000}\r\n");
                    TextBox_PlayerInfo.AppendText($"Z : {playerData[index].PlayerInfo.V3Pos.Z:0.0000}\r\n");
                }
            }
        }

        private void Button_TeleportSelectedPlayer_Click(object sender, RoutedEventArgs e)
        {


            if (ListBox_PlayerList.SelectedItem != null)
            {
                int index = ListBox_PlayerList.SelectedIndex;

                if (index != -1)
                {
                    Teleport.SetTeleportV3Pos(playerData[index].PlayerInfo.V3Pos);
                }
            }
        }

        private void CheckBox_Invisibility_Click(object sender, RoutedEventArgs e)
        {
            Player.Invisibility(CheckBox_Invisibility.IsChecked == true);
        }

        private void CheckBox_VehicleInvisibility_Click(object sender, RoutedEventArgs e)
        {
            Vehicle.Invisibility(CheckBox_VehicleInvisibility.IsChecked == true);
        }

        private void Button_FillCurrentAmmo_Click(object sender, RoutedEventArgs e)
        {


            Weapon.FillCurrentAmmo();
        }

        private void Button_TPAllNPCToMe_Click(object sender, RoutedEventArgs e)
        {


            World.TeleportNPCToMe(false);
        }

        private void Button_TPFriendNPCToMe_Click(object sender, RoutedEventArgs e)
        {


            World.TeleportNPCToMe(true);
        }

        private void CheckBox_WaterProof_Click(object sender, RoutedEventArgs e)
        {
            Player.WaterProof(CheckBox_WaterProof.IsChecked == true);
        }

        private void ListBox_WeaponList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListBox_WeaponList.SelectedIndex;

            if (index != -1)
            {
                ListBox_WeaponInfo.Items.Clear();

                for (int i = 0; i < WeaponData.WeaponDataClass[index].WPreview.Count; i++)
                {
                    ListBox_WeaponInfo.Items.Add(WeaponData.WeaponDataClass[index].WPreview[i].Name);
                }

                ListBox_WeaponInfo.SelectedIndex = 0;
            }
        }

        private void ListBox_WeaponInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index1 = ListBox_WeaponList.SelectedIndex;
            int index2 = ListBox_WeaponInfo.SelectedIndex;

            if (index1 != -1 && index2 != -1)
            {
                TempData.WPickup = WeaponData.WeaponDataClass[index1].WPreview[index2].Pickup;
            }
        }

        private void Button_SpawnWeapon_Click(object sender, RoutedEventArgs e)
        {
            Hacks.CreateAmbientPickup(TempData.WPickup);
        }

        private void Button_RepairVehicle_Click(object sender, RoutedEventArgs e)
        {
            Vehicle.Fix1stfoundBST();
        }

        private void Button_TurnOffBST_Click(object sender, RoutedEventArgs e)
        {
            Online.InstantBullShark(false);
        }

        private void RadioButton_VehicleExtras_None_Click(object sender, RoutedEventArgs e)
        {
            if (RadioButton_VehicleExtras_None.IsChecked == true)
            {
                Vehicle.Extras(0);
            }
            else if (RadioButton_VehicleExtras_Jump.IsChecked == true)
            {
                Vehicle.Extras(40);
            }
            else if (RadioButton_VehicleExtras_Boost.IsChecked == true)
            {
                Vehicle.Extras(66);
            }
            else if (RadioButton_VehicleExtras_Both.IsChecked == true)
            {
                Vehicle.Extras(96);
            }
        }

        private void CheckBox_VehicleParachute_Click(object sender, RoutedEventArgs e)
        {
            Vehicle.Parachute(CheckBox_VehicleParachute.IsChecked == true);
        }

        private void RadioButton_AmmoModifier_None_Click(object sender, RoutedEventArgs e)
        {
            if (RadioButton_AmmoModifier_None.IsChecked == true)
            {
                Weapon.AmmoModifier(0);
            }
            else if (RadioButton_AmmoModifier_AMMO.IsChecked == true)
            {
                Weapon.AmmoModifier(1);
            }
            else if (RadioButton_AmmoModifier_CLIP.IsChecked == true)
            {
                Weapon.AmmoModifier(2);
            }
            else if (RadioButton_AmmoModifier_Both.IsChecked == true)
            {
                Weapon.AmmoModifier(3);
            }
        }

        private void CheckBox_InfiniteAmmo_Click(object sender, RoutedEventArgs e)
        {
            Weapon.InfiniteAmmo(CheckBox_InfiniteAmmo.IsChecked == true);
        }

        private void CheckBox_NoReload_Click(object sender, RoutedEventArgs e)
        {
            Weapon.NoReload(CheckBox_NoReload.IsChecked == true);
        }

        private void Button_NoRecoil_Click(object sender, RoutedEventArgs e)
        {
            Weapon.NoRecoil();
        }

        private void CheckBox_NoSpread_Click(object sender, RoutedEventArgs e)
        {
            Weapon.NoSpread();
        }

        private void Button_MerryweatherServices_Click(object sender, RoutedEventArgs e)
        {
            var str = (e.OriginalSource as Button).Content.ToString();

            int index = MiscData.MerryweatherServices.FindIndex(t => t.Name == str);
            if (index != -1)
            {
                Online.MerryweatherServices(MiscData.MerryweatherServices[index].ID);
            }
        }

        private void Button_LocalWeather_Click(object sender, RoutedEventArgs e)
        {
            var str = (e.OriginalSource as Button).Content.ToString();

            int index = MiscData.LocalWeathers.FindIndex(t => t.Name == str);
            if (index != -1)
            {
                World.SetLocalWeather(MiscData.LocalWeathers[index].ID);
            }
        }

        private void Button_EmptySession_Click(object sender, RoutedEventArgs e)
        {
            ProcessMgr.SuspendProcess(Memory.GetProcessID());
            Thread.Sleep(10000);
            ProcessMgr.ResumeProcess(Memory.GetProcessID());
        }

        private void CheckBox_ImpactType_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBox_ImpactType.IsChecked == true)
            {
                Weapon.ImpactType(5);
            }
            else
            {
                Weapon.ImpactType(3);
            }
        }

        private void ComboBox_ImpactExplosion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ComboBox_ImpactExplosion.SelectedIndex;

            if (index != -1)
            {
                Weapon.ImpactExplosion(MiscData.ImpactExplosions[index].ID);
            }
        }

        private void CheckBox_ReloadMult_Click(object sender, RoutedEventArgs e)
        {
            Weapon.ReloadMult(CheckBox_ReloadMult.IsChecked == true);
        }

        private void CheckBox_Range_Click(object sender, RoutedEventArgs e)
        {
            Weapon.Range();
        }
    }
}
