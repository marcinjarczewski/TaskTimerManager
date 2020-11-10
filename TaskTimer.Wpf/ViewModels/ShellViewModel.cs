using Caliburn.Micro;
using TaskTimer.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using TaskTimer.Wpf.Helpers;

namespace TaskTimer.Wpf.ViewModels
{
    public class ShellViewModel : PropertyChangedBase //, INotifyPropertyChanged
    {
        public ShellViewModel()
        {
            WindowWidth = 1200;
            MenuItems = new BindableCollection<ShellMenuItem>();
        }

        public BindableCollection<ShellMenuItem> MenuItems { get; private set; }

        private ShellMenuItem _selectedMenuItem;
        public ShellMenuItem SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set
            {
                if (_selectedMenuItem == value)
                    return;
                _selectedMenuItem = value;
                NotifyOfPropertyChange(() => SelectedMenuItem);
                NotifyOfPropertyChange(() => CurrentView);
            }
        }

        private int _windowWidth;
        public int WindowWidth
        {
            get { return _windowWidth; }
            set
            {
                _windowWidth = value;
                WindowHelper.WindowWidth = value;
            }
        }

        public object CurrentView
        {
            get
            {
                var screenView = _selectedMenuItem == null ? MenuItems.FirstOrDefault()?.ScreenViewModel : _selectedMenuItem.ScreenViewModel;
                screenView?.Init();
                return screenView;
            }
        }
    }
}
