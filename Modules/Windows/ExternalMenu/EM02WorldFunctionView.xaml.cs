using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Data;

namespace GTA5OnlineTools.Modules.Windows.ExternalMenu;

/// <summary>
/// EM02WorldFunctionView.xaml 的交互逻辑
/// </summary>
public partial class EM02WorldFunctionView : UserControl
{
    public EM02WorldFunctionView()
    {
        InitializeComponent();

        ExternalMenuView.ClosingDisposeEvent += ExternalMenuView_ClosingDisposeEvent;
    }

    private void ExternalMenuView_ClosingDisposeEvent()
    {
        
    }

    private void Button_Sessions_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        var str = (e.OriginalSource as Button).Content.ToString();

        int index = MiscData.Sessions.FindIndex(t => t.Name == str);
        if (index != -1)
        {
            Online.LoadSession(MiscData.Sessions[index].ID);
        }
    }

    private void Button_Disconnect_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Online.Disconnect();
    }

    private void Button_EmptySession_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Online.EmptySession();
    }

    private void Button_LocalWeather_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        var str = (e.OriginalSource as Button).Content.ToString();

        int index = MiscData.LocalWeathers.FindIndex(t => t.Name == str);
        if (index != -1)
        {
            Hacks.SetLocalWeather(MiscData.LocalWeathers[index].ID);
        }
    }

    private void Button_KillNPC_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Hacks.kill_npcs();
    }
    private void Button_KillAllHostilityNPC_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Hacks.kill_enemies();
    }

    private void Button_KillAllPolice_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Hacks.kill_cops();
    }

    private void Button_DestroyAllVehicles_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Hacks.destroy_all_vehicles();
    }

    private void Button_DestroyAllNPCVehicles_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Hacks.destroy_vehs_of_npcs();
    }

    private void Button_DestroyAllHostilityNPCVehicles_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Hacks.destroy_vehs_of_enemies();
    }

    private void Button_TPAllNPCToMe_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Hacks.tp_npcs_to_me();
    }

    private void Button_TPHostilityNPCToMe_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Hacks.tp_enemies_to_me();
    }

    private void Button_RPxN_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        var str = (e.OriginalSource as Button).Content.ToString();

        int index = MiscData.RPxNs.FindIndex(t => t.Name == str);
        if (index != -1)
        {
            Online.RPMultiplier(MiscData.RPxNs[index].ID);
        }
    }

    private void Button_REPxN_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        var str = (e.OriginalSource as Button).Content.ToString();

        int index = MiscData.REPxNs.FindIndex(t => t.Name == str);
        if (index != -1)
        {
            Online.REPMultiplier(MiscData.REPxNs[index].ID);
        }
    }
}
