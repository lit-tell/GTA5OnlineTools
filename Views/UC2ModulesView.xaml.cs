﻿using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Modules.Windows;
using GTA5OnlineTools.Modules.Windows.ExternalMenu;

using Microsoft.Toolkit.Mvvm.Input;

namespace GTA5OnlineTools.Views;

/// <summary>
/// UC2ModulesView.xaml 的交互逻辑
/// </summary>
public partial class UC2ModulesView : UserControl
{
    private ExternalMenuView ExternalMenuView = null;

    private GTAHaxWindow GTAHaxWindow = null;
    private OutfitsWindow OutfitsWindow = null;
    private HeistCutWindow HeistCutWindow = null;
    private StatAutoScriptsWindow StatAutoScriptsWindow = null;
    private HeistPrepsWindow HeistPrepsWindow = null;
    private BigBaseV2Window BigBaseV2Window = null;

    public RelayCommand<string> ModelsClickCommand { get; private set; }

    private const string HintMsg = "未发现GTA5进程，请先运行GTA5游戏";

    public UC2ModulesView()
    {
        InitializeComponent();
        this.DataContext = this;

        ModelsClickCommand = new(ModelsClick);
    }

    private void ModelsClick(string obj)
    {
        AudioUtil.ClickSound();

        if (ProcessUtil.IsAppRun(CoreUtil.TargetAppName))
        {
            switch (obj)
            {
                case "ExternalMenu":
                    ExternalMenuClick();
                    break;
                case "GTAHax":
                    GTAHaxClick();
                    break;
                case "Outfits":
                    OutfitsClick();
                    break;
                case "HeistCut":
                    HeistCutClick();
                    break;
                case "StatAutoScripts":
                    StatAutoScriptsClick();
                    break;
                case "HeistPreps":
                    HeistPrepsClick();
                    break;
                case "BigBaseV2":
                    BigBaseV2Click();
                    break;
            }
        }
        else
        {
            MsgBoxUtil.ErrorMsgBox(HintMsg);
        }
    }

    private void ExternalMenuClick()
    {
        if (ExternalMenuView == null)
        {
            ExternalMenuView = new ExternalMenuView();
            ExternalMenuView.Show();
        }
        else
        {
            if (ExternalMenuView.IsVisible)
            {
                if (!ExternalMenuView.Topmost)
                {
                    ExternalMenuView.Topmost = true;
                    ExternalMenuView.Topmost = false;
                }

                ExternalMenuView.WindowState = WindowState.Normal;
            }
            else
            {
                ExternalMenuView = null;
                ExternalMenuView = new ExternalMenuView();
                ExternalMenuView.Show();
            }
        }
    }

    private void HeistPrepsClick()
    {
        if (HeistPrepsWindow == null)
        {
            HeistPrepsWindow = new HeistPrepsWindow();
            HeistPrepsWindow.Show();
        }
        else
        {
            if (HeistPrepsWindow.IsVisible)
            {
                HeistPrepsWindow.Topmost = true;
                HeistPrepsWindow.Topmost = false;
                HeistPrepsWindow.WindowState = WindowState.Normal;
            }
            else
            {
                HeistPrepsWindow = null;
                HeistPrepsWindow = new HeistPrepsWindow();
                HeistPrepsWindow.Show();
            }
        }
    }

    private void StatAutoScriptsClick()
    {
        if (StatAutoScriptsWindow == null)
        {
            StatAutoScriptsWindow = new StatAutoScriptsWindow();
            StatAutoScriptsWindow.Show();
        }
        else
        {
            if (StatAutoScriptsWindow.IsVisible)
            {
                StatAutoScriptsWindow.Topmost = true;
                StatAutoScriptsWindow.Topmost = false;
                StatAutoScriptsWindow.WindowState = WindowState.Normal;
            }
            else
            {
                StatAutoScriptsWindow = null;
                StatAutoScriptsWindow = new StatAutoScriptsWindow();
                StatAutoScriptsWindow.Show();
            }
        }
    }

    private void HeistCutClick()
    {
        if (HeistCutWindow == null)
        {
            HeistCutWindow = new HeistCutWindow();
            HeistCutWindow.Show();
        }
        else
        {
            if (HeistCutWindow.IsVisible)
            {
                HeistCutWindow.Topmost = true;
                HeistCutWindow.Topmost = false;
                HeistCutWindow.WindowState = WindowState.Normal;
            }
            else
            {
                HeistCutWindow = null;
                HeistCutWindow = new HeistCutWindow();
                HeistCutWindow.Show();
            }
        }
    }

    private void OutfitsClick()
    {
        if (OutfitsWindow == null)
        {
            OutfitsWindow = new OutfitsWindow();
            OutfitsWindow.Show();
        }
        else
        {
            if (OutfitsWindow.IsVisible)
            {
                OutfitsWindow.Topmost = true;
                OutfitsWindow.Topmost = false;
                OutfitsWindow.WindowState = WindowState.Normal;
            }
            else
            {
                OutfitsWindow = null;
                OutfitsWindow = new OutfitsWindow();
                OutfitsWindow.Show();
            }
        }
    }

    private void GTAHaxClick()
    {
        if (GTAHaxWindow == null)
        {
            GTAHaxWindow = new GTAHaxWindow();
            GTAHaxWindow.Show();
        }
        else
        {
            if (GTAHaxWindow.IsVisible)
            {
                GTAHaxWindow.Topmost = true;
                GTAHaxWindow.Topmost = false;
                GTAHaxWindow.WindowState = WindowState.Normal;
            }
            else
            {
                GTAHaxWindow = null;
                GTAHaxWindow = new GTAHaxWindow();
                GTAHaxWindow.Show();
            }
        }
    }

    private void BigBaseV2Click()
    {
        if (BigBaseV2Window == null)
        {
            BigBaseV2Window = new BigBaseV2Window();
            BigBaseV2Window.Show();
        }
        else
        {
            if (BigBaseV2Window.IsVisible)
            {
                BigBaseV2Window.Topmost = true;
                BigBaseV2Window.Topmost = false;
                BigBaseV2Window.WindowState = WindowState.Normal;
            }
            else
            {
                BigBaseV2Window = null;
                BigBaseV2Window = new BigBaseV2Window();
                BigBaseV2Window.Show();
            }
        }
    }
}
