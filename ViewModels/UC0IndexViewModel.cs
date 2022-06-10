using Prism.Events;

using GTA5OnlineTools.Models;
using GTA5OnlineTools.Event;

namespace GTA5OnlineTools.ViewModels;

public class UC0IndexViewModel
{
    public UC0IndexModel UC0IndexModel { get; set; }

    public UC0IndexViewModel(IEventAggregator eventAggregator)
    {
        UC0IndexModel = new UC0IndexModel();
        UC0IndexModel.NoticeInfo = "正在获取最新公告内容...";

        eventAggregator.GetEvent<NoticeMsgEvent>().Subscribe(UpdateNotice);
    }

    private void UpdateNotice(string noticeStr)
    {
        UC0IndexModel.NoticeInfo = noticeStr;
    }
}
