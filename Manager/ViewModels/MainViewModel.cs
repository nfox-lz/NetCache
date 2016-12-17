using System.Windows;
using System.Windows.Input;

namespace Compete.NetCache.Manager.ViewModels
{
    internal sealed class MainViewModel
    {
        public ICommand Exit { get; private set; }

        public MainViewModel()
        {
            Exit = new RoutedUICommand() { Text = "退出" };
        }

        public void AddCommandBinding(CommandBindingCollection commandBindings)
        {
            commandBindings.Add(new CommandBinding(Exit, ExitCommandHandler));
        }

        /// <summary>
        /// Exit 命令的Executed事件的处理程序。 
        /// </summary>
        /// <param name="sender">事件处理程序所附加到的对象。</param>
        /// <param name="node">事件数据。</param>
        private void ExitCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            (sender as Window).Close();
        }
    }
}
