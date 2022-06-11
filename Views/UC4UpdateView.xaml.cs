using GTA5OnlineTools.Models;

namespace GTA5OnlineTools.Views;

/// <summary>
/// UC4UpdateView.xaml 的交互逻辑
/// </summary>
public partial class UC4UpdateView : UserControl
{
    public UC4UpdateModel UC4UpdateModel { get; set; }

    public UC4UpdateView()
    {
        InitializeComponent();

        this.DataContext = this;

        UC4UpdateModel = new();

        UC4UpdateModel.ChangeInfo = "正在获取更新日志...";
    }

    private void UpdateChange(string changeStr)
    {
        UC4UpdateModel.ChangeInfo = changeStr;
    }
}
