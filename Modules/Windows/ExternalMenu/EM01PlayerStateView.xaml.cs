using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Core;
using GTA5OnlineTools.Features.Data;

namespace GTA5OnlineTools.Modules.Windows.ExternalMenu
{
    /// <summary>
    /// EM01PlayerStateView.xaml 的交互逻辑
    /// </summary>
    public partial class EM01PlayerStateView : UserControl
    {
        // 快捷键
        private HotKeys MainHotKeys;
        // 特殊功能
        private int FrameFlag = 0;

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
            MainHotKeys.AddKey(WinVK.INSERT);
            MainHotKeys.AddKey(WinVK.F3);
            MainHotKeys.AddKey(WinVK.F4);
            MainHotKeys.AddKey(WinVK.F5);
            MainHotKeys.AddKey(WinVK.F6);
            MainHotKeys.AddKey(WinVK.F7);
            MainHotKeys.AddKey(WinVK.F8);
            MainHotKeys.AddKey(WinVK.BACK);
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
                    case (int)WinVK.INSERT:
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
                    case (int)WinVK.BACK:
                        if (CheckBox_NoCollision.IsChecked == true)
                        {
                            Settings.Player.NoCollision = !Settings.Player.NoCollision;
                        }
                        break;
                }
            }));
        }

        private void MainThread()
        {
            while (true)
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
                switch (FrameFlag)
                {
                    case 1:
                        Memory.Write<int>(Globals.WorldPTR, Offsets.SpecialAmmo, (int)EnumData.FrameFlags.SuperJump);
                        break;
                    case 2:
                        Memory.Write<int>(Globals.WorldPTR, Offsets.SpecialAmmo, (int)EnumData.FrameFlags.FireAmmo);
                        break;
                    case 3:
                        Memory.Write<int>(Globals.WorldPTR, Offsets.SpecialAmmo, (int)EnumData.FrameFlags.ExplosiveAmmo);
                        break;
                }

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

                if (Settings.Common.AutoKillPolice)
                    World.KillPolice();

                Thread.Sleep(200);
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

        private void CheckBox_AutoKillPolice_Click(object sender, RoutedEventArgs e)
        {
            World.KillPolice();
            Settings.Common.AutoKillPolice = CheckBox_AutoKillPolice.IsChecked == true;
        }

        private void RadioButton_FrameFlags_Click(object sender, RoutedEventArgs e)
        {
            if (RadioButton_FrameFlags_Default.IsChecked == true)
            {
                FrameFlag = 0;
            }
            else if (RadioButton_FrameFlags_SuperJump.IsChecked == true)
            {
                FrameFlag = 1;
            }
            else if (RadioButton_FrameFlags_FireAmmo.IsChecked == true)
            {
                FrameFlag = 2;
            }
            else if (RadioButton_FrameFlags_ExplosiveAmmo.IsChecked == true)
            {
                FrameFlag = 3;
            }
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
}
