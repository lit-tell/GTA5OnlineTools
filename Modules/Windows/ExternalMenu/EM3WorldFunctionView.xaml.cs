using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Data;

namespace GTA5OnlineTools.Modules.Windows.ExternalMenu
{
    /// <summary>
    /// EM3WorldFunctionView.xaml 的交互逻辑
    /// </summary>
    public partial class EM3WorldFunctionView : UserControl
    {
        public EM3WorldFunctionView()
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
                World.SetLocalWeather(MiscData.LocalWeathers[index].ID);
            }
        }

        private void Button_KillAllPeds_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            World.KillNPC(false);
        }
        private void Button_KillAllHostilityPeds_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            World.KillNPC(true);
        }

        private void Button_DestroyAllVehicles_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            World.DestroyAllVehicles();
        }

        private void Button_DestroyAllNPCVehicles_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            World.DestroyNPCVehicles(false);
        }

        private void Button_DestroyAllHostilityNPCVehicles_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            World.DestroyNPCVehicles(true);
        }

        private void Button_TPAllNPCToMe_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            World.TeleportNPCToMe(false);
        }

        private void Button_TPFriendNPCToMe_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            World.TeleportNPCToMe(true);
        }
    }
}
