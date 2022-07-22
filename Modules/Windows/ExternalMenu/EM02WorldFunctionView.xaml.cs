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
            Globals.Load_Session(MiscData.Sessions[index].ID);
        }
    }

    private void Button_Disconnect_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Globals.Disconnect();
    }

    private void Button_EmptySession_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Hacks.Empty_Session();
    }

    private void Button_LocalWeather_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        var str = (e.OriginalSource as Button).Content.ToString();

        int index = MiscData.LocalWeathers.FindIndex(t => t.Name == str);
        if (index != -1)
        {
            Hacks.Set_Local_Weather(MiscData.LocalWeathers[index].ID);
        }
    }

    private void Button_KillNPC_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Hacks.Kill_Npcs();
    }
    private void Button_KillAllHostilityNPC_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Hacks.Kill_Enemies();
    }

    private void Button_KillAllPolice_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Hacks.Kill_Cops();
    }

    private void Button_DestroyAllVehicles_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Hacks.Destroy_All_Vehicles();
    }

    private void Button_DestroyAllNPCVehicles_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Hacks.Destroy_Vehs_Of_Npcs();
    }

    private void Button_DestroyAllHostilityNPCVehicles_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Hacks.Destroy_Vehs_Of_Enemies();
    }

    private void Button_TPAllNPCToMe_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Hacks.Tp_Npcs_To_Me();
    }

    private void Button_TPHostilityNPCToMe_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Hacks.Tp_Enemies_To_Me();
    }

    private void Button_RPxN_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        var str = (e.OriginalSource as Button).Content.ToString();

        int index = MiscData.RPxNs.FindIndex(t => t.Name == str);
        if (index != -1)
        {
            Globals.RP_Multiplier(MiscData.RPxNs[index].ID);
        }
    }

    private void Button_REPxN_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        var str = (e.OriginalSource as Button).Content.ToString();

        int index = MiscData.REPxNs.FindIndex(t => t.Name == str);
        if (index != -1)
        {
            Globals.REP_Multiplier(MiscData.REPxNs[index].ID);
        }
    }
}
