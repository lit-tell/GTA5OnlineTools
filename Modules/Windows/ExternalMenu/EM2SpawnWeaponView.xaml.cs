using System.Windows.Controls;

namespace GTA5OnlineTools.Modules.Windows.ExternalMenu
{
    /// <summary>
    /// EM2SpawnWeaponView.xaml 的交互逻辑
    /// </summary>
    public partial class EM2SpawnWeaponView : UserControl
    {
        public EM2SpawnWeaponView()
        {
            InitializeComponent();

            ExternalMenuView.ClosingDisposeEvent += ExternalMenuView_ClosingDisposeEvent;
        }

        private void ExternalMenuView_ClosingDisposeEvent()
        {

        }
    }
}
