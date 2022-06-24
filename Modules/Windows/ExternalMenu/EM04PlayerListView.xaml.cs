using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Core;
using GTA5OnlineTools.Features.Data;

namespace GTA5OnlineTools.Modules.Windows.ExternalMenu;

/// <summary>
/// EM04PlayerListView.xaml 的交互逻辑
/// </summary>
public partial class EM04PlayerListView : UserControl
{
    // 显示玩家列表
    private List<PlayerData> playerData = new List<PlayerData>();

    public EM04PlayerListView()
    {
        InitializeComponent();

        ExternalMenuView.ClosingDisposeEvent += ExternalMenuView_ClosingDisposeEvent;
    }

    private void ExternalMenuView_ClosingDisposeEvent()
    {

    }

    private void ListBox_PlayerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        TextBox_PlayerInfo.Clear();

        int index = ListBox_PlayerList.SelectedIndex;

        if (index != -1)
        {
            TextBox_PlayerInfo.AppendText($"战局房主 : {playerData[index].PlayerInfo2.Host}\r\n\r\n");

            TextBox_PlayerInfo.AppendText($"玩家RID : {playerData[index].RID}\r\n");
            TextBox_PlayerInfo.AppendText($"玩家昵称 : {playerData[index].Name}\r\n\r\n");

            TextBox_PlayerInfo.AppendText($"当前生命值 : {playerData[index].PlayerInfo2.Health:0.0}\r\n");
            TextBox_PlayerInfo.AppendText($"最大生命值 : {playerData[index].PlayerInfo2.MaxHealth:0.0}\r\n\r\n");

            TextBox_PlayerInfo.AppendText($"无敌状态 : {playerData[index].PlayerInfo2.GodMode}\r\n");
            TextBox_PlayerInfo.AppendText($"无布娃娃 : {playerData[index].PlayerInfo2.NoRagdoll}\r\n\r\n");

            TextBox_PlayerInfo.AppendText($"通缉等级 : {playerData[index].PlayerInfo2.WantedLevel}\r\n");
            TextBox_PlayerInfo.AppendText($"奔跑速度 : {playerData[index].PlayerInfo2.RunSpeed:0.0}\r\n\r\n");

            TextBox_PlayerInfo.AppendText($"X : {playerData[index].PlayerInfo2.V3Pos.X:0.0000}\r\n");
            TextBox_PlayerInfo.AppendText($"Y : {playerData[index].PlayerInfo2.V3Pos.Y:0.0000}\r\n");
            TextBox_PlayerInfo.AppendText($"Z : {playerData[index].PlayerInfo2.V3Pos.Z:0.0000}\r\n");
        }
    }

    private void Button_RefreshPlayerList_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        playerData.Clear();
        ListBox_PlayerList.Items.Clear();

        for (int i = 0; i < 32; i++)
        {
            long pCNetGamePlayer = Memory.Read<long>(Globals.NetworkPlayerMgrPTR, new int[] { 0x180 + (i * 8) });
            if (!Memory.IsValid(pCNetGamePlayer))
                continue;

            long pCPlayerInfo = Memory.Read<long>(pCNetGamePlayer + 0xA0);
            if (!Memory.IsValid(pCPlayerInfo))
                continue;

            long pCPed = Memory.Read<long>(pCPlayerInfo + 0x01E8);
            if (!Memory.IsValid(pCPed))
                continue;

            long pCNavigation = Memory.Read<long>(pCPed + 0x30, null);
            if (!Memory.IsValid(pCNavigation))
                continue;

            ////////////////////////////////////////////

            playerData.Add(new PlayerData()
            {
                RID = Memory.Read<long>(pCPlayerInfo + 0x90),
                Name = Memory.ReadString(pCPlayerInfo + 0xA4, null, 20),

                PlayerInfo2 = new PlayerInfo2()
                {
                    Host = Hacks.ReadGA<int>(1893548 + 1 + (i * 600) + 10) == 1 ? true : false,
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
            if (item.PlayerInfo2.Host)
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

    private void Button_TeleportSelectedPlayer_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        if (ListBox_PlayerList.SelectedItem != null)
        {
            int index = ListBox_PlayerList.SelectedIndex;

            if (index != -1)
            {
                Hacks.TeleportToCoords(Hacks.GetLocalPed(), playerData[index].PlayerInfo2.V3Pos);
            }
        }
    }
}
