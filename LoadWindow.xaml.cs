namespace GTA5OnlineTools;

/// <summary>
/// LoadWindow.xaml 的交互逻辑
/// </summary>
public partial class LoadWindow : Window
{
    public LoadWindow()
    {
        InitializeComponent();
    }

    private void Window_Load_Loaded(object sender, RoutedEventArgs e)
    {
        Task.Run(() =>
        {
            Task.Delay(1500).Wait();

            this.Dispatcher.Invoke(() =>
            {
                var mainWindow = new MainWindow();
                mainWindow.Show();
                // 转移主程序控制权
                Application.Current.MainWindow = mainWindow;
                // 关闭初始化窗口
                this.Close();
            });
        });
    }
}
