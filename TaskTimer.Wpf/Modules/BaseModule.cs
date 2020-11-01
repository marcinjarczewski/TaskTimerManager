﻿using TaskTimer.Contracts;
using TaskTimer.Wpf.ViewModels;
using TaskTimer.Contracts.Bootstrappers;

namespace TaskTimer.Wpf.Modules
{
    public class BaseModule : IModule
    {
        private readonly IShell _shell;
        private readonly MainViewModel _mainViewModel;
        private readonly SettingsViewModel _settingsViewModel;
        private readonly HistoryViewModel _historyViewModel;
        private readonly AboutViewModel _aboutViewModel;

        public BaseModule(IShell shell, MainViewModel mainViewModel, SettingsViewModel settingsViewModel, HistoryViewModel historyViewModel, AboutViewModel aboutViewModel)
        {
            _shell = shell;
            _mainViewModel = mainViewModel;
            _settingsViewModel = settingsViewModel;
            _historyViewModel = historyViewModel;
            _aboutViewModel = aboutViewModel;
        }

        public void Init()
        {
            _shell.MenuItems.Add(new ShellMenuItem() { Caption = "Tasks", ScreenViewModel = _mainViewModel });
            _shell.MenuItems.Add(new ShellMenuItem() { Caption = "History", ScreenViewModel = _historyViewModel });
            _shell.MenuItems.Add(new ShellMenuItem() { Caption = "Settings", ScreenViewModel = _settingsViewModel });
            _shell.MenuItems.Add(new ShellMenuItem() { Caption = "About", ScreenViewModel = _aboutViewModel });
        }
    }
}