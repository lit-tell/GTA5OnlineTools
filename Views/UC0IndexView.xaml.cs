using GTA5OnlineTools.Models;

namespace GTA5OnlineTools.Views;

/// <summary>
/// UC0IndexView.xaml 的交互逻辑
/// </summary>
public partial class UC0IndexView : UserControl
{
    public UC0IndexModel UC0IndexModel { get; set; }

    public UC0IndexView()
    {
        InitializeComponent();

        this.DataContext = this;

        UC0IndexModel = new UC0IndexModel();
        UC0IndexModel.NoticeInfo = "正在获取最新公告内容...";
    }

    private void UpdateNotice(string noticeStr)
    {
        UC0IndexModel.NoticeInfo = noticeStr;
    }
}
