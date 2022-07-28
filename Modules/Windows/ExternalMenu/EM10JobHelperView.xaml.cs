using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Data;

namespace GTA5OnlineTools.Modules.Windows.ExternalMenu;

/// <summary>
/// EM10JobHelperView.xaml 的交互逻辑
/// </summary>
public partial class EM10JobHelperView : UserControl
{
    public EM10JobHelperView()
    {
        InitializeComponent();

        ExternalMenuView.ClosingDisposeEvent += ExternalMenuView_ClosingDisposeEvent;
    }

    private void ExternalMenuView_ClosingDisposeEvent()
    {

    }

    private void CheckBox_RemoveBunkerSupplyDelay_Click(object sender, RoutedEventArgs e)
    {
        Online.RemoveBunkerSupplyDelay(CheckBox_RemoveBunkerSupplyDelay.IsChecked == true);
    }

    private void CheckBox_UnlockBunkerResearch_Click(object sender, RoutedEventArgs e)
    {
        Online.UnlockBunkerResearch(CheckBox_UnlockBunkerResearch.IsChecked == true);
    }

    private void Button_TriggerBunkerResearch_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        int supplycount = Hacks.ReadGA<int>(Hacks.GetBusinessIndex(5) + 2);
        int progress = Hacks.ReadGA<int>(Hacks.GetBusinessIndex(5) + 12);

        if (progress == 60)
        {
            TextBox_Result.Text = $"地堡研究进度已完成，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
        else if (supplycount <= 0)
        {
            TextBox_Result.Text = $"地堡原材料已空，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
        else
        {
            Hacks.WriteGA<int>(Hacks.GetBusinessIndex(5) + 13, 0);
            TextBox_Result.Text = $"正在触发地堡研究，当前研究进度为 {progress}，原材料数量 {supplycount}";
        }
    }

    private void Button_TriggerBunkerProduction_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        int supplycount = Hacks.ReadGA<int>(Hacks.GetBusinessIndex(5) + 2);
        int progress = Hacks.ReadGA<int>(Hacks.GetBusinessIndex(5) + 1);

        if (progress == 100)
        {
            TextBox_Result.Text = $"地堡库存已满，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
        else if (supplycount <= 0)
        {
            TextBox_Result.Text = $"地堡原材料已空，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
        else
        {
            Hacks.WriteGA<int>(Hacks.GetBusinessIndex(5) + 9, 0);
            TextBox_Result.Text = $"正在触发地堡生产，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
    }

    private void CheckBox_RemoveBuyingCratesCooldown_Click(object sender, RoutedEventArgs e)
    {
        Online.CEOBuyingCratesCooldown(CheckBox_CooldownForBuyingCrates.IsChecked == true);
    }

    private void CheckBox_RemoveSellingCratesCooldown_Click(object sender, RoutedEventArgs e)
    {
        Online.CEOSellingCratesCooldown(CheckBox_CooldownForSellingCrates.IsChecked == true);
    }

    private void CheckBox_PricePerCrateAtCrates_Click(object sender, RoutedEventArgs e)
    {
        Online.CEOPricePerCrateAtCrates(CheckBox_PricePerCrateAtCrates.IsChecked == true);
    }

    private int CheckBusinessID(int index)
    {
        //int[] Meth = new int[4] { 1, 6, 11, 16 };
        //int[] Weed = new int[4] { 2, 7, 12, 17 };
        //int[] Cocaine = new int[4] { 3, 8, 13, 18 };
        //int[] Cash = new int[4] { 4, 9, 14, 19 };
        //int[] Documents = new int[4] { 5, 10, 15, 20 };

        int[] business = new int[5];

        for (int i = 0; i < 5; i++)
        {
            int temp = Hacks.ReadGA<int>(Hacks.GetBusinessIndex(i));

            if (temp <= 5)
            {
                switch (temp)
                {
                    case 0:
                        business[4] = i;
                        break;
                    case 1:
                        business[0] = i;
                        break;
                    case 2:
                        business[1] = i;
                        break;
                    case 3:
                        business[2] = i;
                        break;
                    case 4:
                        business[3] = i;
                        break;
                }
            }
            else
            {
                temp %= 5;

                switch (temp)
                {
                    case 0:
                        business[4] = i;
                        break;
                    case 1:
                        business[0] = i;
                        break;
                    case 2:
                        business[1] = i;
                        break;
                    case 3:
                        business[2] = i;
                        break;
                    case 4:
                        business[3] = i;
                        break;
                }
            }

        }

        return business[index];
    }

    private void CheckBox_TriggerMethProduction_Click(object sender, RoutedEventArgs e)
    {
        int id = CheckBusinessID(0);

        int supplycount = Hacks.ReadGA<int>(Hacks.GetBusinessIndex(id) + 2);
        int progress = Hacks.ReadGA<int>(Hacks.GetBusinessIndex(id) + 1);

        if (progress == 20)
        {
            TextBox_Result.Text = $"冰毒实验室库存已满，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
        else if (supplycount <= 0)
        {
            TextBox_Result.Text = $"冰毒实验室原材料已空，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
        else
        {
            Hacks.WriteGA<int>(Hacks.GetBusinessIndex(id) + 9, 0);
            TextBox_Result.Text = $"正在触发冰毒实验室生产，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
    }

    private void CheckBox_TriggerWeedProduction_Click(object sender, RoutedEventArgs e)
    {
        int id = CheckBusinessID(1);

        int supplycount = Hacks.ReadGA<int>(Hacks.GetBusinessIndex(id) + 2);
        int progress = Hacks.ReadGA<int>(Hacks.GetBusinessIndex(id) + 1);

        if (progress == 80)
        {
            TextBox_Result.Text = $"大麻种植场库存已满，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
        else if (supplycount <= 0)
        {
            TextBox_Result.Text = $"大麻种植场原材料已空，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
        else
        {
            Hacks.WriteGA<int>(Hacks.GetBusinessIndex(id) + 9, 0);
            TextBox_Result.Text = $"正在触发大麻种植场生产，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
    }

    private void CheckBox_TriggerCocaineProduction_Click(object sender, RoutedEventArgs e)
    {
        int id = CheckBusinessID(2);

        int supplycount = Hacks.ReadGA<int>(Hacks.GetBusinessIndex(id) + 2);
        int progress = Hacks.ReadGA<int>(Hacks.GetBusinessIndex(id) + 1);

        if (progress == 10)
        {
            TextBox_Result.Text = $"可卡因工厂库存已满，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
        else if (supplycount <= 0)
        {
            TextBox_Result.Text = $"可卡因工厂原材料已空，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
        else
        {
            Hacks.WriteGA<int>(Hacks.GetBusinessIndex(id) + 9, 0);
            TextBox_Result.Text = $"正在触发可卡因工厂生产，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
    }

    private void CheckBox_TriggerCashProduction_Click(object sender, RoutedEventArgs e)
    {
        int id = CheckBusinessID(3);

        int supplycount = Hacks.ReadGA<int>(Hacks.GetBusinessIndex(id) + 2);
        int progress = Hacks.ReadGA<int>(Hacks.GetBusinessIndex(id) + 1);

        if (progress == 40)
        {
            TextBox_Result.Text = $"假钞工厂库存已满，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
        else if (supplycount <= 0)
        {
            TextBox_Result.Text = $"假钞工厂原材料已空，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
        else
        {
            Hacks.WriteGA<int>(Hacks.GetBusinessIndex(id) + 9, 0);
            TextBox_Result.Text = $"正在触发假钞工厂生产，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
    }

    private void CheckBox_TriggerDocumentsProduction_Click(object sender, RoutedEventArgs e)
    {
        int id = CheckBusinessID(4);

        int supplycount = Hacks.ReadGA<int>(Hacks.GetBusinessIndex(id) + 2);
        int progress = Hacks.ReadGA<int>(Hacks.GetBusinessIndex(id) + 1);

        if (progress == 60)
        {
            TextBox_Result.Text = $"伪证件办公室库存已满，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
        else if (supplycount <= 0)
        {
            TextBox_Result.Text = $"伪证件办公室原材料已空，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
        else
        {
            Hacks.WriteGA<int>(Hacks.GetBusinessIndex(id) + 9, 0);
            TextBox_Result.Text = $"正在触发伪证件办公室生产，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
    }

    private void CheckBox_RemoveMCSupplyDelay_Click(object sender, RoutedEventArgs e)
    {
        Online.RemoveMCSupplyDelay(CheckBox_RemoveMCSupplyDelay.IsChecked == true);
    }

    private void Button_CEOCargos_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        var str = (e.OriginalSource as Button).Content.ToString();

        int index = MiscData.CEOCargos.FindIndex(t => t.Name == str);
        if (index != -1)
        {
            // They are in gb_contraband_buy at func_915, for future updates.
            Online.CEOSpecialCargo(false);
            Thread.Sleep(100);
            Online.CEOSpecialCargo(true);
            Thread.Sleep(100);
            Online.CEOCargoType(MiscData.CEOCargos[index].ID);
        }
    }

    private void CheckBox_RemoveExportVehicleDelay_Click(object sender, RoutedEventArgs e)
    {
        Online.RemoveExportVehicleDelay(CheckBox_RemoveExportVehicleDelay.IsChecked == true);
    }

    private void CheckBox_RemoveSmugglerRunInDelay_Click(object sender, RoutedEventArgs e)
    {
        Online.RemoveSmugglerRunInDelay(CheckBox_RemoveSmugglerRunInDelay.IsChecked == true);
    }

    private void CheckBox_RemoveSmugglerRunOutDelay_Click(object sender, RoutedEventArgs e)
    {
        Online.RemoveSmugglerRunOutDelay(CheckBox_RemoveSmugglerRunOutDelay.IsChecked == true);
    }

    private void CheckBox_SetBunkerResupplyCosts_Click(object sender, RoutedEventArgs e)
    {
        Online.SetBunkerResupplyCosts(CheckBox_SetBunkerResupplyCosts.IsChecked == true);
    }

    private void CheckBox_SetMCResupplyCosts_Click(object sender, RoutedEventArgs e)
    {
        Online.SetMCResupplyCosts(CheckBox_SetMCResupplyCosts.IsChecked == true);
    }

    private void CheckBox_SetMCSaleMultipliers_Click(object sender, RoutedEventArgs e)
    {
        Online.SetMCSaleMultipliers(CheckBox_SetMCSaleMultipliers.IsChecked == true);
    }

    private void CheckBox_SetBunkerSuppliesPerUnitProduced_Click(object sender, RoutedEventArgs e)
    {
        Online.SetBunkerSuppliesPerUnitProduced(CheckBox_SetBunkerSuppliesPerUnitProduced.IsChecked == true);
    }

    private void CheckBox_SetMCSuppliesPerUnitProduced_Click(object sender, RoutedEventArgs e)
    {
        Online.SetMCSuppliesPerUnitProduced(CheckBox_SetMCSuppliesPerUnitProduced.IsChecked == true);
    }

    private void CheckBox_RemoveNightclubOutDelay_Click(object sender, RoutedEventArgs e)
    {
        Online.RemoveNightclubOutDelay(CheckBox_RemoveNightclubOutDelay.IsChecked == true);
    }

    private void CheckBox_NightclubNoTonyLaunderingMoney_Click(object sender, RoutedEventArgs e)
    {
        Online.NightclubNoTonyLaunderingMoney(CheckBox_NightclubNoTonyLaunderingMoney.IsChecked == true);
    }
}
