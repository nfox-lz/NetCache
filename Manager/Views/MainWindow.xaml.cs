using System.Windows;

namespace Compete.NetCache.Manager.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            (DataContext as ViewModels.MainViewModel).AddCommandBinding(CommandBindings);
        }
    }
}
