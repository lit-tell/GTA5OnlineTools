using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using GameOverlay.Drawing;
using GameOverlay.Windows;
using GTA5OnlineTools.Features.Core;
using GTA5OnlineTools.Features.Data;

namespace GTA5OnlineTools.Features.SDK
{
    public class Overlay : IDisposable
    {
        private readonly GraphicsWindow _window;

        private readonly Dictionary<string, SolidBrush> _brushes;
        private readonly Dictionary<string, Font> _fonts;
        private readonly Dictionary<string, Image> _images;

        private Memory.WindowData windowData;

        private Thread AimbotThread;

        // 视角宽和视角高
        private float gview_width;
        private float gview_height;

        public Overlay()
        {
            windowData = Memory.GetGameWindowData();
            Memory.SetForegroundWindow();

            Offsets.Mask.TempPTR = Memory.FindPattern(Offsets.Mask.ViewPortPTR);
            Globals.ViewPortPTR = Memory.Rip_37(Offsets.Mask.TempPTR);

            Offsets.Mask.TempPTR = Memory.FindPattern(Offsets.Mask.AimingPedPTR);
            Globals.AimingPedPTR = Memory.Rip_37(Offsets.Mask.TempPTR);

            Offsets.Mask.TempPTR = Memory.FindPattern(Offsets.Mask.CCameraPTR);
            Globals.CCameraPTR = Memory.Rip_37(Offsets.Mask.TempPTR);

            /////////////////////////////////////////////

            _brushes = new Dictionary<string, SolidBrush>();
            _fonts = new Dictionary<string, Font>();
            _images = new Dictionary<string, Image>();

            var gfx = new Graphics()
            {
                VSync = Settings.Overlay.VSync,
                MeasureFPS = true,
                PerPrimitiveAntiAliasing = true,
                TextAntiAliasing = true
            };

            _window = new GraphicsWindow(windowData.Left, windowData.Top, windowData.Width, windowData.Height, gfx)
            {
                FPS = Settings.Overlay.FPS,
                IsTopmost = true,
                IsVisible = true
            };

            _window.DestroyGraphics += _window_DestroyGraphics;
            _window.DrawGraphics += _window_DrawGraphics;
            _window.SetupGraphics += _window_SetupGraphics;

            AimbotThread = new Thread(new ParameterizedThreadStart(SpecialThread_Thread));
            AimbotThread.IsBackground = true;
            AimbotThread.Start();
        }

        private void _window_SetupGraphics(object sender, SetupGraphicsEventArgs e)
        {
            var gfx = e.Graphics;

            if (e.RecreateResources)
            {
                foreach (var pair in _brushes) pair.Value.Dispose();
                foreach (var pair in _images) pair.Value.Dispose();
            }

            _brushes["black"] = gfx.CreateSolidBrush(0, 0, 0);
            _brushes["white"] = gfx.CreateSolidBrush(255, 255, 255);
            _brushes["red"] = gfx.CreateSolidBrush(255, 0, 98);
            _brushes["green"] = gfx.CreateSolidBrush(0, 128, 0);
            _brushes["blue"] = gfx.CreateSolidBrush(30, 144, 255);
            _brushes["bgcolor"] = gfx.CreateSolidBrush(11, 11, 11, 150);
            _brushes["grid"] = gfx.CreateSolidBrush(255, 255, 255, 0.2f);
            _brushes["deepPink"] = gfx.CreateSolidBrush(247, 63, 147, 255);

            _brushes["transparency"] = gfx.CreateSolidBrush(0, 0, 0, 0);

            if (e.RecreateResources) return;

            _fonts["arial"] = gfx.CreateFont("Arial", 12);
            _fonts["Microsoft YaHei"] = gfx.CreateFont("Microsoft YaHei", 12);
            _fonts["consolas"] = gfx.CreateFont("Consolas", 14);
        }

        private void _window_DestroyGraphics(object sender, DestroyGraphicsEventArgs e)
        {
            foreach (var pair in _brushes) pair.Value.Dispose();
            foreach (var pair in _fonts) pair.Value.Dispose();
            foreach (var pair in _images) pair.Value.Dispose();
        }

        private void _window_DrawGraphics(object sender, DrawGraphicsEventArgs e)
        {
            var gfx = e.Graphics;
            gfx.ClearScene(_brushes["transparency"]);
            ResizeWindow(gfx);

            // 绘制帧数
            gfx.DrawText(_fonts["Microsoft YaHei"], 12, _brushes["green"], 10, _window.Height / 3, $"FPS：{gfx.FPS}");

            ///////////////////////////////////////////////////////
            //                 用户自定义绘制区域                   //
            ///////////////////////////////////////////////////////

            // 视角宽和视角高
            gview_width = windowData.Width / 2;
            gview_height = windowData.Height / 2;

            long pWorldPTR = Memory.Read<long>(Globals.WorldPTR);
            long pCPed = Memory.Read<long>(pWorldPTR + 0x8);

            // 玩家自己RID
            long pCPlayerInfo = Memory.Read<long>(pCPed + 0x10C8);
            long myRID = Memory.Read<long>(pCPlayerInfo + 0x90);

            // 自己坐标
            long pCNavigation = Memory.Read<long>(pCPed + 0x30);
            Vector3 myPosV3 = Memory.Read<Vector3>(pCNavigation + 0x50);

            // 玩家列表
            long pPlayerListPTR = Memory.Read<long>(Globals.PlayerListPTR);
            int playerCount = Memory.Read<int>(pPlayerListPTR + 0x178);

            // Ped数量
            long pReplayInterfacePTR = Memory.Read<long>(Globals.ReplayInterfacePTR);
            long my_offset_0x18 = Memory.Read<long>(pReplayInterfacePTR + 0x18);
            int pedCount = Memory.Read<int>(my_offset_0x18 + 0x110);

            gfx.DrawText(_fonts["Microsoft YaHei"], 12, _brushes["blue"], 10, _window.Height / 3 + 30,
                $"GTA5线上小助手\n\nX: {myPosV3.X:0.0000}\nY: {myPosV3.Y:0.0000}\nZ: {myPosV3.Z:0.0000}\n\n" +
                $"玩家数量: {playerCount}\nPed数量: {pedCount}");

            long pAimingPedPTR = Memory.Read<long>(Globals.AimingPedPTR);
            bool isAimPed = Memory.Read<long>(pAimingPedPTR + 0x280) > 0;

            if (Settings.Overlay.ESP_Crosshair)
            {
                // 当玩家按住右键准心对准敌人，准心变成粉红色，否则为绿色
                if (isAimPed && Convert.ToBoolean(WinAPI.GetKeyState((int)WinVK.RBUTTON) & WinAPI.KEY_PRESSED))
                {
                    DrawCrosshair(gfx, _brushes["deepPink"], 7.0f, 1.5f);
                }
                else
                {
                    DrawCrosshair(gfx, _brushes["green"], 7.0f, 1.5f);
                }
            }

            ///////////////////////////////////////////////////////

            for (int i = 0; i < 128; i++)
            {
                long ped_offset_0 = Memory.Read<long>(my_offset_0x18 + 0x100);
                ped_offset_0 = Memory.Read<long>(ped_offset_0 + i * 0x10);
                if (ped_offset_0 == 0)
                {
                    continue;
                }

                float pedHealth = Memory.Read<float>(ped_offset_0 + 0x280);
                if (pedHealth <= 0)
                {
                    continue;
                }

                float pedMaxHealth = Memory.Read<float>(ped_offset_0 + 0x2A0);
                float pedHPPercentage = pedHealth / pedMaxHealth;

                long ped_offset_1 = Memory.Read<long>(ped_offset_0 + 0x10C8);
                long pedRID = Memory.Read<long>(ped_offset_1 + 0x90);
                if (myRID == pedRID)
                {
                    continue;
                }

                string pedName = Memory.ReadString(ped_offset_1 + 0xA4, null, 20);

                // 绘制玩家
                if (!Settings.Overlay.ESP_Player)
                {
                    if (pedName != "")
                    {
                        continue;
                    }
                }

                // 绘制Ped
                if (!Settings.Overlay.ESP_NPC)
                {
                    if (pedName == "")
                    {
                        continue;
                    }
                }

                Vector3 pedPosV3 = Memory.Read<Vector3>(ped_offset_0 + 0x90);

                // Ped与自己的距离
                float myToPedDistance = (float)Math.Sqrt(
                    Math.Pow(myPosV3.X - pedPosV3.X, 2) +
                    Math.Pow(myPosV3.Y - pedPosV3.Y, 2) +
                    Math.Pow(myPosV3.Z - pedPosV3.Z, 2));

                long ped_offset_2 = Memory.Read<long>(ped_offset_0 + 0x30);

                Vector2 v2PedSinCos = new Vector2
                {
                    X = Memory.Read<float>(ped_offset_2 + 0x20),
                    Y = Memory.Read<float>(ped_offset_2 + 0x30)
                };

                if (Settings.Overlay.ESP_3DBox)
                {
                    if (pedName != "")
                    {
                        // 玩家 3DBox
                        DrawAABBBox(gfx, _brushes["red"], pedPosV3, v2PedSinCos);
                    }
                    else
                    {
                        // Ped 3DBox
                        DrawAABBBox(gfx, _brushes["white"], pedPosV3, v2PedSinCos);
                    }
                }

                Vector2 pedPosV2 = WorldToScreen(pedPosV3);
                Vector2 pedBoxV2 = GetBoxWH(pedPosV3);

                if (!IsNullVector2(pedPosV2))
                {
                    if (pedName != "")
                    {
                        if (Settings.Overlay.ESP_2DBox)
                        {
                            // 2D方框
                            Draw2DBox(gfx, _brushes["red"], pedPosV2, pedBoxV2, 0.7f);
                        }

                        if (Settings.Overlay.ESP_2DLine)
                        {
                            if (Settings.Overlay.ESP_2DBox)
                            {
                                // 2DBox射线
                                Draw2DLine(gfx, _brushes["red"], pedPosV2, pedBoxV2, 0.7f);
                            }
                            else
                            {
                                // 3DBox射线
                                DrawAABBLine(gfx, _brushes["red"], pedPosV3, 0.7f);
                            }
                        }

                        if (Settings.Overlay.ESP_2DHealthBar)
                        {
                            if (Settings.Overlay.ESP_2DBox)
                            {
                                // 2DBox血条
                                Draw2DHealthBar(gfx, _brushes["green"], pedPosV2, pedBoxV2, pedHPPercentage, 0.7f);
                            }
                            else
                            {
                                // 3DBox血条
                                Draw3DHealthBar(gfx, _brushes["green"], pedPosV2, pedBoxV2, pedHPPercentage, 0.7f);
                            }
                        }

                        if (Settings.Overlay.ESP_HealthText)
                        {
                            if (Settings.Overlay.ESP_2DBox)
                            {
                                // 2DBox血量数字
                                Draw2DHealthText(gfx, _brushes["red"], pedPosV2, pedBoxV2, pedHealth, pedMaxHealth, i);
                            }
                            else
                            {
                                // 3DBox血量数字
                                Draw3DHealthText(gfx, _brushes["red"], pedPosV2, pedBoxV2, pedHealth, pedMaxHealth, i);
                            }
                        }

                        if (Settings.Overlay.ESP_NameText)
                        {
                            if (Settings.Overlay.ESP_2DBox)
                            {
                                // 2DBox玩家名称
                                Draw2DNameText(gfx, _brushes["red"], pedPosV2, pedBoxV2, pedName, myToPedDistance);
                            }
                            else
                            {
                                // 3DBox玩家名称
                                Draw3DNameText(gfx, _brushes["red"], pedPosV2, pedBoxV2, pedName, myToPedDistance);
                            }
                        }
                    }
                    else
                    {
                        if (Settings.Overlay.ESP_2DBox)
                        {
                            // 2D方框
                            Draw2DBox(gfx, _brushes["white"], pedPosV2, pedBoxV2, 0.7f);
                        }

                        if (Settings.Overlay.ESP_2DLine)
                        {
                            if (Settings.Overlay.ESP_2DBox)
                            {
                                // 2DBox射线
                                Draw2DLine(gfx, _brushes["white"], pedPosV2, pedBoxV2, 0.7f);
                            }
                            else
                            {
                                // 3DBox射线
                                DrawAABBLine(gfx, _brushes["white"], pedPosV3, 0.7f);
                            }
                        }

                        if (Settings.Overlay.ESP_2DHealthBar)
                        {
                            if (Settings.Overlay.ESP_2DBox)
                            {
                                // 2DBox血条
                                Draw2DHealthBar(gfx, _brushes["green"], pedPosV2, pedBoxV2, pedHPPercentage, 0.7f);
                            }
                            else
                            {
                                // 3DBox血条
                                Draw3DHealthBar(gfx, _brushes["green"], pedPosV2, pedBoxV2, pedHPPercentage, 0.7f);
                            }
                        }

                        if (Settings.Overlay.ESP_HealthText)
                        {
                            if (Settings.Overlay.ESP_2DBox)
                            {
                                // 2DBox血量数字
                                Draw2DHealthText(gfx, _brushes["white"], pedPosV2, pedBoxV2, pedHealth, pedMaxHealth, i);
                            }
                            else
                            {
                                // 3DBox血量数字
                                Draw3DHealthText(gfx, _brushes["white"], pedPosV2, pedBoxV2, pedHealth, pedMaxHealth, i);
                            }
                        }

                        if (Settings.Overlay.ESP_NameText)
                        {
                            if (Settings.Overlay.ESP_2DBox)
                            {
                                // 2DBox玩家名称
                                Draw2DNameText(gfx, _brushes["white"], pedPosV2, pedBoxV2, pedName, myToPedDistance);
                            }
                            else
                            {
                                // 3DBox玩家名称
                                Draw3DNameText(gfx, _brushes["white"], pedPosV2, pedBoxV2, pedName, myToPedDistance);
                            }
                        }
                    }

                    //int pedEntityType = Memory.Read<int>(ped_offset_0 + 0x10B8);
                    //byte pedEntityType = Memory.Read<byte>(ped_offset_0 + 0x2B);
                    //byte oHostility = Memory.Read<byte>(ped_offset_0 + 0x18C);

                    //pedEntityType = pedEntityType >> 14 & 0x1F;

                    //gfx.DrawText(_fonts["Microsoft YaHei"], 10, _brushes["red"],
                    //    pedPosV2.X, pedPosV2.Y,
                    //    $"Type : {pedEntityType}");
                }

                if (Settings.Overlay.ESP_Bone)
                {
                    // 骨骼
                    DrawBone(gfx, ped_offset_0, 0, 7);
                    DrawBone(gfx, ped_offset_0, 7, 8);
                    DrawBone(gfx, ped_offset_0, 8, 3);
                    DrawBone(gfx, ped_offset_0, 8, 4);
                    DrawBone(gfx, ped_offset_0, 7, 5);
                    DrawBone(gfx, ped_offset_0, 7, 6);
                }
            }
        }

        private void ResizeWindow(Graphics gfx)
        {
            // 窗口移动跟随
            windowData = Memory.GetGameWindowData();
            _window.X = windowData.Left;
            _window.Y = windowData.Top;
            _window.Width = windowData.Width;
            _window.Height = windowData.Height;
            gfx.Resize(_window.Width, _window.Height);
        }

        private void SpecialThread_Thread(object sender)
        {
            while (true)
            {
                if (Settings.Overlay.AimBot_Enabled)
                {
                    float aimBot_Min_Distance = Settings.Overlay.AimBot_Fov;
                    Vector3 aimBot_ViewAngles = new Vector3 { X = 0, Y = 0, Z = 0 };
                    Vector3 teleWpedCoords = new Vector3 { X = 0, Y = 0, Z = 0 };

                    // 玩家自己RID
                    long myRID = Memory.Read<long>(Globals.WorldPTR, Offsets.RID);

                    // 相机坐标
                    long pCCameraPTR = Memory.Read<long>(Globals.CCameraPTR);
                    long pCCameraPTR_0 = Memory.Read<long>(pCCameraPTR + 0x00);
                    pCCameraPTR_0 = Memory.Read<long>(pCCameraPTR_0 + 0x3C0);
                    Vector3 cameraV3Pos = Memory.Read<Vector3>(pCCameraPTR_0 + 0x60);

                    // 是否是第一人称，当Fov=0为第一人称或者开镜状态，第三人称50
                    float isFPP = Memory.Read<float>(pCCameraPTR_0 + 0x10, new int[] { 0x30 });
                    // 玩家是否处于载具中，或者掩护状态（载具/掩体=0，正常=16）
                    byte isPlayerInCar = Memory.Read<byte>(Globals.WorldPTR, Offsets.InVehicle);

                    // Ped实体
                    long pReplayInterfacePTR = Memory.Read<long>(Globals.ReplayInterfacePTR);
                    long my_offset_0x18 = Memory.Read<long>(pReplayInterfacePTR + 0x18);

                    for (int i = 0; i < 128; i++)
                    {
                        long ped_offset_0 = Memory.Read<long>(my_offset_0x18 + 0x100);
                        ped_offset_0 = Memory.Read<long>(ped_offset_0 + i * 0x10);
                        if (ped_offset_0 == 0)
                        {
                            continue;
                        }

                        float pedHealth = Memory.Read<float>(ped_offset_0 + 0x280);
                        if (pedHealth <= 0)
                        {
                            continue;
                        }

                        long ped_offset_1 = Memory.Read<long>(ped_offset_0 + 0x10C8);
                        long pedRID = Memory.Read<long>(ped_offset_1 + 0x90);
                        if (myRID == pedRID)
                        {
                            continue;
                        }

                        string pedName = Memory.ReadString(ped_offset_1 + 0xA4, null, 20);

                        // 绘制玩家
                        if (!Settings.Overlay.ESP_Player)
                        {
                            if (pedName != "")
                            {
                                continue;
                            }
                        }

                        // 绘制Ped
                        if (!Settings.Overlay.ESP_NPC)
                        {
                            if (pedName == "")
                            {
                                continue;
                            }
                        }

                        Vector3 pedV3Pos = Memory.Read<Vector3>(ped_offset_0 + 0x90);
                        Vector2 pedV2Pos = WorldToScreen(pedV3Pos);

                        // 自瞄数据
                        float aimBot_Distance = (float)Math.Sqrt(Math.Pow(pedV2Pos.X - gview_width, 2) + Math.Pow(pedV2Pos.Y - gview_height, 2));
                        // 获取距离准心最近的方框
                        if (aimBot_Distance < aimBot_Min_Distance)
                        {
                            aimBot_Min_Distance = aimBot_Distance;
                            aimBot_ViewAngles = GetCCameraViewAngles(cameraV3Pos, GetBonePosition(ped_offset_0, Settings.Overlay.AimBot_BoneIndex));
                            teleWpedCoords = pedV3Pos;
                        }
                    }

                    // 玩家处于载具或者掩护状态中不启用自瞄，无目标取消自瞄
                    if (isPlayerInCar != 0x00 && aimBot_Min_Distance != Settings.Overlay.AimBot_Fov)
                    {
                        // 默认按住Ctrl键自瞄
                        if (Convert.ToBoolean(WinAPI.GetKeyState((int)Settings.Overlay.AimBot_Key) & WinAPI.KEY_PRESSED))
                        {
                            if (isFPP == 0)
                            {
                                // 第一人称及开镜自瞄
                                Memory.Write<float>(pCCameraPTR_0 + 0x40, aimBot_ViewAngles.X);
                                Memory.Write<float>(pCCameraPTR_0 + 0x44, aimBot_ViewAngles.Y);
                                Memory.Write<float>(pCCameraPTR_0 + 0x48, aimBot_ViewAngles.Z);
                            }
                            else
                            {
                                // 第三人称及自瞄
                                Memory.Write<float>(pCCameraPTR_0 + 0x3D0, aimBot_ViewAngles.X);
                                Memory.Write<float>(pCCameraPTR_0 + 0x3D4, aimBot_ViewAngles.Y);
                                Memory.Write<float>(pCCameraPTR_0 + 0x3D8, aimBot_ViewAngles.Z);
                            }

                            if (Convert.ToBoolean(WinAPI.GetKeyState((int)WinVK.F5) & WinAPI.KEY_PRESSED))
                            {
                                Teleport.SetTeleportV3Pos(teleWpedCoords);
                            }
                        }
                    }
                }

                Thread.Sleep(1);
            }
        }

        private void DrawCrosshair(Graphics gfx, IBrush brush, float length, float stroke)
        {
            gfx.DrawLine(brush,
                gview_width - length,
                gview_height,
                gview_width + length,
                gview_height, stroke);
            gfx.DrawLine(brush,
                gview_width,
                gview_height - length,
                gview_width,
                gview_height + length, stroke);

            //gfx.DrawCircle(brush, gview_width, gview_height, gview_height / 4, stroke);
        }

        private void Draw2DBox(Graphics gfx, IBrush brush, Vector2 pedPosV2, Vector2 boxV2, float stroke)
        {
            // 2D方框
            gfx.DrawRectangle(brush, Rectangle.Create(
                pedPosV2.X - boxV2.X / 2,
                pedPosV2.Y - boxV2.Y / 2,
                boxV2.X,
                boxV2.Y), stroke);
        }

        private void Draw2DLine(Graphics gfx, IBrush brush, Vector2 pedPosV2, Vector2 pedBoxV2, float stroke)
        {
            // 2D射线
            gfx.DrawLine(brush,
                windowData.Width / 2,
                0,
                pedPosV2.X,
                pedPosV2.Y - pedBoxV2.Y / 2, stroke);
        }

        private void Draw2DHealthBar(Graphics gfx, IBrush brush, Vector2 pedPosV2, Vector2 pedBoxV2, float pedHPPercentage, float stroke)
        {
            // 2DBox血条
            gfx.DrawRectangle(_brushes["white"], Rectangle.Create(
                pedPosV2.X - pedBoxV2.X / 2 - pedBoxV2.X / 8,
                pedPosV2.Y + pedBoxV2.Y / 2,
                pedBoxV2.X / 10,
                pedBoxV2.Y * -1.0f), stroke);
            gfx.FillRectangle(brush, Rectangle.Create(
                pedPosV2.X - pedBoxV2.X / 2 - pedBoxV2.X / 8,
                pedPosV2.Y + pedBoxV2.Y / 2,
                pedBoxV2.X / 10,
                pedBoxV2.Y * pedHPPercentage * -1.0f));
        }

        private void Draw3DHealthBar(Graphics gfx, IBrush brush, Vector2 pedPosV2, Vector2 pedBoxV2, float pedHPPercentage, float stroke)
        {
            // 3DBox血条
            gfx.DrawRectangle(_brushes["white"], Rectangle.Create(
                pedPosV2.X - pedBoxV2.X / 2,
                pedPosV2.Y + pedBoxV2.Y / 2 + pedBoxV2.X / 10,
                pedBoxV2.X,
                pedBoxV2.X / 10 / 2), stroke);
            gfx.FillRectangle(brush, Rectangle.Create(
                pedPosV2.X - pedBoxV2.X / 2,
                pedPosV2.Y + pedBoxV2.Y / 2 + pedBoxV2.X / 10,
                pedBoxV2.X * pedHPPercentage,
                pedBoxV2.X / 10 / 2));
        }

        private void Draw2DHealthText(Graphics gfx, IBrush brush, Vector2 pedPosV2, Vector2 pedBoxV2, float pedHealth, float pedMaxHealth, int index)
        {
            // 2DBox血量数字
            gfx.DrawText(_fonts["Microsoft YaHei"], 10, brush,
                pedPosV2.X - pedBoxV2.X / 2,
                pedPosV2.Y + pedBoxV2.Y / 2 + pedBoxV2.X / 8 - pedBoxV2.X / 10,
                $"[{index}] HP : {pedHealth:0}/{pedMaxHealth:0}");
        }

        private void Draw3DHealthText(Graphics gfx, IBrush brush, Vector2 pedPosV2, Vector2 pedBoxV2, float pedHealth, float pedMaxHealth, int index)
        {
            // 3DBox血量数字
            gfx.DrawText(_fonts["Microsoft YaHei"], 10, brush,
                pedPosV2.X - pedBoxV2.X / 2,
                pedPosV2.Y + pedBoxV2.Y / 2 + pedBoxV2.X / 10 + pedBoxV2.X / 10 / 2 + pedBoxV2.X / 8 - pedBoxV2.X / 10,
                $"[{index}] HP : {pedHealth:0}/{pedMaxHealth:0}");
        }

        private void Draw2DNameText(Graphics gfx, IBrush brush, Vector2 pedPosV2, Vector2 pedBoxV2, string name, float distance)
        {
            // 2DBox玩家名称
            gfx.DrawText(_fonts["Microsoft YaHei"], 10, brush,
                pedPosV2.X + pedBoxV2.X / 2 + pedBoxV2.X / 8 - pedBoxV2.X / 10,
                pedPosV2.Y - pedBoxV2.Y / 2,
                $"[{distance:0m}] ID : {name}");
        }

        private void Draw3DNameText(Graphics gfx, IBrush brush, Vector2 pedPosV2, Vector2 pedBoxV2, string name, float distance)
        {
            // 3DBox玩家名称
            gfx.DrawText(_fonts["Microsoft YaHei"], 10, brush,
                pedPosV2.X + pedBoxV2.X / 2 + pedBoxV2.X / 10 + pedBoxV2.X / 10 / 2 + pedBoxV2.X / 8 - pedBoxV2.X / 10,
                pedPosV2.Y - pedBoxV2.Y / 2,
                $"[{distance:0m}] ID : {name}");
        }

        private struct AxisAlignedBox
        {
            public Vector3 Min;
            public Vector3 Max;
        }

        private void DrawAABBLine(Graphics gfx, IBrush brush, Vector3 m_Position, float stroke)
        {
            Vector3 aabb_0 = new Vector3(0.0f, 0.0f, 1.0f) + m_Position; // 0
            Vector2 aabb_0V2Pos = WorldToScreen(aabb_0);

            if (!IsNullVector2(aabb_0V2Pos))
            {
                gfx.DrawLine(brush,
                    windowData.Width / 2, 0,
                    aabb_0V2Pos.X, aabb_0V2Pos.Y, stroke);
            }
        }

        private void DrawAABBBox(Graphics gfx, IBrush brush, Vector3 m_Position, Vector2 m_SinCos)
        {
            AxisAlignedBox aabb = new AxisAlignedBox
            {
                Min = new Vector3(-0.5f, -0.5f, -1.0f),
                Max = new Vector3(0.5f, 0.5f, 1.0f)
            };

            Vector3 aabb_0 = new Vector3(
                aabb.Min.X * m_SinCos.Y - aabb.Min.Y * m_SinCos.X,
                aabb.Min.X * m_SinCos.X + aabb.Min.Y * m_SinCos.Y,
                aabb.Min.Z) + m_Position; // 0
            Vector3 aabb_1 = new Vector3(
                aabb.Min.X * m_SinCos.Y - aabb.Max.Y * m_SinCos.X,
                aabb.Min.X * m_SinCos.X + aabb.Max.Y * m_SinCos.Y,
                aabb.Min.Z) + m_Position; // 1
            Vector3 aabb_2 = new Vector3(
                aabb.Min.X * m_SinCos.Y - aabb.Min.Y * m_SinCos.X,
                aabb.Min.X * m_SinCos.X + aabb.Min.Y * m_SinCos.Y,
                aabb.Max.Z) + m_Position; // 2
            Vector3 aabb_3 = new Vector3(
                aabb.Min.X * m_SinCos.Y - aabb.Max.Y * m_SinCos.X,
                aabb.Min.X * m_SinCos.X + aabb.Max.Y * m_SinCos.Y,
                aabb.Max.Z) + m_Position; // 3

            Vector3 aabb_4 = new Vector3(
                aabb.Max.X * m_SinCos.Y - aabb.Min.Y * m_SinCos.X,
                aabb.Max.X * m_SinCos.X + aabb.Min.Y * m_SinCos.Y,
                aabb.Min.Z) + m_Position; // 4
            Vector3 aabb_5 = new Vector3(
                aabb.Max.X * m_SinCos.Y - aabb.Max.Y * m_SinCos.X,
                aabb.Max.X * m_SinCos.X + aabb.Max.Y * m_SinCos.Y,
                aabb.Min.Z) + m_Position; // 5
            Vector3 aabb_6 = new Vector3(
                aabb.Max.X * m_SinCos.Y - aabb.Min.Y * m_SinCos.X,
                aabb.Max.X * m_SinCos.X + aabb.Min.Y * m_SinCos.Y,
                aabb.Max.Z) + m_Position; // 6
            Vector3 aabb_7 = new Vector3(
                aabb.Max.X * m_SinCos.Y - aabb.Max.Y * m_SinCos.X,
                aabb.Max.X * m_SinCos.X + aabb.Max.Y * m_SinCos.Y,
                aabb.Max.Z) + m_Position; // 7

            /// <summary>
            ///                    max
            ///         6 --------- 7
            ///       / |         / |
            ///     2 --------- 3   |
            ///     |   |       |   |
            ///     |   |       |   |
            ///     |   4 --------- 5
            ///     | /         | /
            ///     0 --------- 1
            ///    min
            /// </summary>

            Vector2 aabb_0V2Pos = WorldToScreen(aabb_0);
            Vector2 aabb_1V2Pos = WorldToScreen(aabb_1);
            Vector2 aabb_2V2Pos = WorldToScreen(aabb_2);
            Vector2 aabb_3V2Pos = WorldToScreen(aabb_3);
            Vector2 aabb_4V2Pos = WorldToScreen(aabb_4);
            Vector2 aabb_5V2Pos = WorldToScreen(aabb_5);
            Vector2 aabb_6V2Pos = WorldToScreen(aabb_6);
            Vector2 aabb_7V2Pos = WorldToScreen(aabb_7);

            if (!IsNullVector2(aabb_0V2Pos) &&
                !IsNullVector2(aabb_1V2Pos) &&
                !IsNullVector2(aabb_2V2Pos) &&
                !IsNullVector2(aabb_3V2Pos) &&
                !IsNullVector2(aabb_4V2Pos) &&
                !IsNullVector2(aabb_5V2Pos) &&
                !IsNullVector2(aabb_6V2Pos) &&
                !IsNullVector2(aabb_7V2Pos))
            {
                gfx.DrawLine(brush, aabb_0V2Pos.X, aabb_0V2Pos.Y, aabb_1V2Pos.X, aabb_1V2Pos.Y, 0.7f);
                gfx.DrawLine(brush, aabb_0V2Pos.X, aabb_0V2Pos.Y, aabb_2V2Pos.X, aabb_2V2Pos.Y, 0.7f);
                gfx.DrawLine(brush, aabb_3V2Pos.X, aabb_3V2Pos.Y, aabb_1V2Pos.X, aabb_1V2Pos.Y, 0.7f);
                gfx.DrawLine(brush, aabb_3V2Pos.X, aabb_3V2Pos.Y, aabb_2V2Pos.X, aabb_2V2Pos.Y, 0.7f);

                gfx.DrawLine(brush, aabb_4V2Pos.X, aabb_4V2Pos.Y, aabb_5V2Pos.X, aabb_5V2Pos.Y, 0.7f);
                gfx.DrawLine(brush, aabb_4V2Pos.X, aabb_4V2Pos.Y, aabb_6V2Pos.X, aabb_6V2Pos.Y, 0.7f);
                gfx.DrawLine(brush, aabb_7V2Pos.X, aabb_7V2Pos.Y, aabb_5V2Pos.X, aabb_5V2Pos.Y, 0.7f);
                gfx.DrawLine(brush, aabb_7V2Pos.X, aabb_7V2Pos.Y, aabb_6V2Pos.X, aabb_6V2Pos.Y, 0.7f);

                gfx.DrawLine(brush, aabb_0V2Pos.X, aabb_0V2Pos.Y, aabb_4V2Pos.X, aabb_4V2Pos.Y, 0.7f);
                gfx.DrawLine(brush, aabb_1V2Pos.X, aabb_1V2Pos.Y, aabb_5V2Pos.X, aabb_5V2Pos.Y, 0.7f);
                gfx.DrawLine(brush, aabb_2V2Pos.X, aabb_2V2Pos.Y, aabb_6V2Pos.X, aabb_6V2Pos.Y, 0.7f);
                gfx.DrawLine(brush, aabb_3V2Pos.X, aabb_3V2Pos.Y, aabb_7V2Pos.X, aabb_7V2Pos.Y, 0.7f);
            }
        }

        private void DrawBone(Graphics gfx, long offset, int bone0, int bone1)
        {
            Vector2 v2Bone0 = WorldToScreen(GetBonePosition(offset, bone0));
            Vector2 v2Bone1 = WorldToScreen(GetBonePosition(offset, bone1));

            if (!IsNullVector2(v2Bone0) && !IsNullVector2(v2Bone1))
            {
                gfx.DrawLine(_brushes["white"], v2Bone0.X, v2Bone0.Y, v2Bone1.X, v2Bone1.Y, 1);
            }
        }

        private Vector3 GetBonePosition(long offset, int BoneID)
        {
            float[] bomematrix = Memory.ReadMatrix<float>(offset + 0x60, 16);

            Vector3 bone_offset_pos;
            bone_offset_pos.X = Memory.Read<float>(offset + 0x430 + BoneID * 0x10);
            bone_offset_pos.Y = Memory.Read<float>(offset + 0x430 + BoneID * 0x10 + 0x4);
            bone_offset_pos.Z = Memory.Read<float>(offset + 0x430 + BoneID * 0x10 + 0x4 + 0x4);

            Vector3 bone_pos;
            bone_pos.X = bomematrix[0] * bone_offset_pos.X + bomematrix[4] * bone_offset_pos.Y + bomematrix[8] * bone_offset_pos.Z + bomematrix[12];
            bone_pos.Y = bomematrix[1] * bone_offset_pos.X + bomematrix[5] * bone_offset_pos.Y + bomematrix[9] * bone_offset_pos.Z + bomematrix[13];
            bone_pos.Z = bomematrix[2] * bone_offset_pos.X + bomematrix[6] * bone_offset_pos.Y + bomematrix[10] * bone_offset_pos.Z + bomematrix[14];

            return bone_pos;
        }

        private Vector3 GetCCameraViewAngles(Vector3 cameraV3Pos, Vector3 enemyV3Pos)
        {
            // 鼠标角度，自瞄需要用到
            float distance = (float)Math.Sqrt(Math.Pow(cameraV3Pos.X - enemyV3Pos.X, 2) + Math.Pow(cameraV3Pos.Y - enemyV3Pos.Y, 2) + Math.Pow(cameraV3Pos.Z - enemyV3Pos.Z, 2));

            return new Vector3
            {
                X = (enemyV3Pos.X - cameraV3Pos.X) / distance,
                Y = (enemyV3Pos.Y - cameraV3Pos.Y) / distance,
                Z = (enemyV3Pos.Z - cameraV3Pos.Z) / distance
            };
        }

        private bool IsNullVector2(Vector2 vector)
        {
            return vector == new Vector2(0, 0);
        }

        private Vector2 WorldToScreen(Vector3 target)
        {
            Vector2 _worldToScreenPos;
            Vector3 _camera;

            float[] viewmatrix = Memory.ReadMatrix<float>(Globals.ViewPortPTR + 0xC0, 16);

            _camera.Z = viewmatrix[2] * target.X + viewmatrix[6] * target.Y + viewmatrix[10] * target.Z + viewmatrix[14];
            if (_camera.Z < 0.001f)
                return new Vector2(0, 0);

            _camera.X = windowData.Width / 2;
            _camera.Y = windowData.Height / 2;
            _camera.Z = 1 / _camera.Z;

            _worldToScreenPos.X = viewmatrix[0] * target.X + viewmatrix[4] * target.Y + viewmatrix[8] * target.Z + viewmatrix[12];
            _worldToScreenPos.Y = viewmatrix[1] * target.X + viewmatrix[5] * target.Y + viewmatrix[9] * target.Z + viewmatrix[13];

            _worldToScreenPos.X = _camera.X + _camera.X * _worldToScreenPos.X * _camera.Z;
            _worldToScreenPos.Y = _camera.Y - _camera.Y * _worldToScreenPos.Y * _camera.Z;

            return _worldToScreenPos;
        }

        private Vector2 GetBoxWH(Vector3 target)
        {
            Vector2 _box;
            Vector3 _camera;

            float[] viewmatrix = Memory.ReadMatrix<float>(Globals.ViewPortPTR + 0xC0, 16);

            _camera.Z = viewmatrix[2] * target.X + viewmatrix[6] * target.Y + viewmatrix[10] * target.Z + viewmatrix[14];
            if (_camera.Z < 0.001f)
                return new Vector2(0, 0);

            _camera.Y = windowData.Height / 2;
            _camera.Z = 1 / _camera.Z;

            _box.X = viewmatrix[1] * target.X + viewmatrix[5] * target.Y + viewmatrix[9] * (target.Z + 1.0f) + viewmatrix[13];
            _box.Y = viewmatrix[1] * target.X + viewmatrix[5] * target.Y + viewmatrix[9] * (target.Z - 1.0f) + viewmatrix[13];

            _box.X = _camera.Y - _camera.Y * _box.X * _camera.Z;
            _box.Y = _camera.Y - _camera.Y * _box.Y * _camera.Z;

            _box.Y = Math.Abs(_box.X - _box.Y);
            _box.X = _box.Y / 2;

            return _box;
        }

        /////////////////////////////////////////////

        public void Run()
        {
            _window.Create();
            _window.Join();
        }

        ~Overlay()
        {
            Dispose(false);
        }

        #region IDisposable Support
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                _window.Dispose();

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
