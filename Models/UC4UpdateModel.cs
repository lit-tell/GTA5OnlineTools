using Prism.Mvvm;

namespace GTA5OnlineTools.Models
{
    public class UC4UpdateModel : BindableBase
    {
        private string _changeInfo;
        /// <summary>
        /// 更新日志
        /// </summary>
        public string ChangeInfo
        {
            get { return _changeInfo; }
            set { _changeInfo = value; RaisePropertyChanged(); }
        }
    }
}
