using Prism.Commands;

using GTA5OnlineTools.Models;
using GTA5OnlineTools.Common.Utils;

namespace GTA5OnlineTools.ViewModels;

public class UC5AboutViewModel
{
    public UC5AboutModel UC5AboutModel { get; set; }

    public DelegateCommand<string> HyperlinkClickCommand { get; private set; }

    public UC5AboutViewModel()
    {
        UC5AboutModel = new UC5AboutModel();
        HyperlinkClickCommand = new DelegateCommand<string>(HyperlinkClick);
    }

    private void HyperlinkClick(string url)
    {
        ProcessUtil.OpenLink(url);
    }
}
