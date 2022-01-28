using System;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using System.Threading;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.DryIoc;
using GTA5OnlineTools.Views;
using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Modules.Windows.ExternalMenu;

namespace GTA5OnlineTools
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : PrismApplication
    {
        public static Mutex AppMainMutex;

        protected override void OnStartup(StartupEventArgs e)
        {
            AppMainMutex = new Mutex(true, ResourceAssembly.GetName().Name, out var createdNew);

            if (createdNew)
            {
                RegisterEvents();

                base.OnStartup(e);
            }
            else
            {
                MessageBox.Show("请不要重复打开，程序已经运行\n如果一直提示，请到\"任务管理器-详细信息（win7为进程）\"里结束本程序",
                    "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                Current.Shutdown();
            }
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainView>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<UC0IndexView>();
            containerRegistry.RegisterForNavigation<UC1HacksView>();
            containerRegistry.RegisterForNavigation<UC2ModulesView>();
            containerRegistry.RegisterForNavigation<UC3ToolsView>();
            containerRegistry.RegisterForNavigation<UC4UpdateView>();
            containerRegistry.RegisterForNavigation<UC5AboutView>();

            containerRegistry.RegisterForNavigation<EM0PlayerStateView>();
            containerRegistry.RegisterForNavigation<EM1SpawnVehicleView>();
            containerRegistry.RegisterForNavigation<EM2SpawnWeaponView>();
            containerRegistry.RegisterForNavigation<EM3WorldFunctionView>();
            containerRegistry.RegisterForNavigation<EM4OnlineOptionView>();
            containerRegistry.RegisterForNavigation<EM5PlayerListView>();
            containerRegistry.RegisterForNavigation<EM6CustomTPView>();
            containerRegistry.RegisterForNavigation<EM7ExternalOverlayView>();
            containerRegistry.RegisterForNavigation<EM8SessionChatView>();

        }

        private void RegisterEvents()
        {
            // UI线程未捕获异常处理事件（UI主线程）
            DispatcherUnhandledException += App_DispatcherUnhandledException;

            // 非UI线程未捕获异常处理事件（例如自己创建的一个子线程）
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            // Task线程内未捕获异常处理事件
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.Exception, e.ToString());
            FileUtil.SaveErrorLog(str);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.ExceptionObject as Exception, e.ToString());
            FileUtil.SaveErrorLog(str);
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.Exception, e.ToString());
            FileUtil.SaveErrorLog(str);
        }

        /// <summary>
        /// 生成自定义异常消息
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="backStr">备用异常消息：当ex为null时有效</param>
        /// <returns>异常字符串文本</returns>
        private static string GetExceptionMsg(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("【出现时间】：" + DateTime.Now.ToString());
            if (ex != null)
            {
                sb.AppendLine("【异常类型】：" + ex.GetType().Name);
                sb.AppendLine("【异常信息】：" + ex.Message);
                sb.AppendLine("【堆栈调用】：" + ex.StackTrace);
            }
            else
            {
                sb.AppendLine("【未处理异常】：" + backStr);
            }
            return sb.ToString();
        }
    }
}
