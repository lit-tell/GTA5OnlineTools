using System;
using Prism.Mvvm;

namespace GTA5OnlineTools.Models
{
    public class MainModel : BindableBase
    {
        /// <summary>
        /// 窗口标题
        /// </summary>
        public string WindowTitle { get; set; }

        //////////////////////////////////////////////////////////////

        private string _gTA5IsRun;
        /// <summary>
        /// GTA5是否运行
        /// </summary>
        public string GTA5IsRun
        {
            get { return _gTA5IsRun; }
            set { _gTA5IsRun = value; RaisePropertyChanged(); }
        }

        private string _appRunTime;
        /// <summary>
        /// 程序运行时间
        /// </summary>
        public string AppRunTime
        {
            get { return _appRunTime; }
            set { _appRunTime = value; RaisePropertyChanged(); }
        }
    }
}
