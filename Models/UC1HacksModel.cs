using System.Windows;
using Prism.Mvvm;

namespace GTA5OnlineTools.Models
{
    public class UC1HacksModel : BindableBase
    {
        private bool _kiddionIsRun = false;
        /// <summary>
        /// Kiddion运行状态
        /// </summary>
        public bool KiddionIsRun
        {
            get { return _kiddionIsRun; }
            set { _kiddionIsRun = value; RaisePropertyChanged(); }
        }

        private bool _subVersionIsRun = false;
        /// <summary>
        /// SubVersion运行状态
        /// </summary>
        public bool SubVersionIsRun
        {
            get { return _subVersionIsRun; }
            set { _subVersionIsRun = value; RaisePropertyChanged(); }
        }

        private bool _gTAHaxIsRun = false;
        /// <summary>
        /// GTAHax运行状态
        /// </summary>
        public bool GTAHaxIsRun
        {
            get { return _gTAHaxIsRun; }
            set { _gTAHaxIsRun = value; RaisePropertyChanged(); }
        }

        private bool _bincoHaxIsRun = false;
        /// <summary>
        /// BincoHax运行状态
        /// </summary>
        public bool BincoHaxIsRun
        {
            get { return _bincoHaxIsRun; }
            set { _bincoHaxIsRun = value; RaisePropertyChanged(); }
        }

        private bool _lSCHaxIsRun = false;
        /// <summary>
        /// LSCHax运行状态
        /// </summary>
        public bool LSCHaxIsRun
        {
            get { return _lSCHaxIsRun; }
            set { _lSCHaxIsRun = value; RaisePropertyChanged(); }
        }

        private bool _pedDropperIsRun = false;
        /// <summary>
        /// PedDropper运行状态
        /// </summary>
        public bool PedDropperIsRun
        {
            get { return _pedDropperIsRun; }
            set { _pedDropperIsRun = value; RaisePropertyChanged(); }
        }

        private bool _jobMoneyIsRun = false;
        /// <summary>
        /// JobMoney运行状态
        /// </summary>
        public bool JobMoneyIsRun
        {
            get { return _jobMoneyIsRun; }
            set { _jobMoneyIsRun = value; RaisePropertyChanged(); }
        }

        private object _frameContent;

        public object FrameContent
        {
            get { return _frameContent; }
            set { _frameContent = value; RaisePropertyChanged(); }
        }


        private Visibility _frameVisibilityState = Visibility.Collapsed;
        /// <summary>
        /// Frame的显示状态
        /// </summary>
        public Visibility FrameVisibilityState
        {
            get { return _frameVisibilityState; }
            set { _frameVisibilityState = value; RaisePropertyChanged(); }
        }
    }
}
