using TaskTimer.Contracts;
using TaskTimer.WPF.ViewModels;
using TaskTimer.Contracts.Bootstrappers;
using System.Collections.Generic;
using System.Reflection;

namespace TaskTimer.WPF.Bootstrappers
{
    public class BaseShell : IShell
    {
        private readonly ModuleLoader _loader;
        private readonly ShellViewModel _shellViewModel;

        public BaseShell(ModuleLoader loader, ShellViewModel shellViewModel)
        {
            _loader = loader;
            _shellViewModel = shellViewModel;
        }

        public IList<ShellMenuItem> MenuItems { get { return _shellViewModel.MenuItems; } }

        public IModule LoadModule(Assembly assembly)
        {
            return _loader.LoadModule(assembly);
        }
    }
}