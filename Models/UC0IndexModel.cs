using Prism.Mvvm;

namespace GTA5OnlineTools.Models
{
    public class UC0IndexModel : BindableBase
    {
        private string _noticeInfo;
        /// <summary>
        /// 通知公告
        /// </summary>
        public string NoticeInfo
        {
            get { return _noticeInfo; }
            set { _noticeInfo = value; RaisePropertyChanged(); }
        }

    }
}
