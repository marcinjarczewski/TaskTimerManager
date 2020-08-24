using TaskTimer.Contracts;
using TaskTimer.Wpf.ViewModels;
using TaskTimer.Contracts.Bootstrappers;

namespace TaskTimer.Wpf.Modules
{
    public class BaseModule : IModule
    {
        private readonly IShell _shell;
        private readonly MainViewModel _mainViewModel;

        public BaseModule(IShell shell, MainViewModel mainViewModel)
        {
            _shell = shell;
            _mainViewModel = mainViewModel;
        }

        public void Init()
        {
            _shell.MenuItems.Add(new ShellMenuItem() { Caption = "Convert", ScreenViewModel = _mainViewModel });
            _shell.MenuItems.Add(new ShellMenuItem() { Caption = "Convert2", ScreenViewModel = _mainViewModel });
        }
    }
}