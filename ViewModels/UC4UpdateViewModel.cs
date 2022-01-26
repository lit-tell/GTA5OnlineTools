using Prism.Events;
using GTA5OnlineTools.Models;
using GTA5OnlineTools.Event;

namespace GTA5OnlineTools.ViewModels
{
    public class UC4UpdateViewModel
    {
        public UC4UpdateModel UC4UpdateModel { get; set; }

        public UC4UpdateViewModel(IEventAggregator eventAggregator)
        {
            UC4UpdateModel = new UC4UpdateModel();

            UC4UpdateModel.ChangeInfo = "正在获取更新日志...";

            eventAggregator.GetEvent<ChangeMsgEvent>().Subscribe(UpdateChange);
        }

        private void UpdateChange(string changeStr)
        {
            UC4UpdateModel.ChangeInfo = changeStr;
        }
    }
}
