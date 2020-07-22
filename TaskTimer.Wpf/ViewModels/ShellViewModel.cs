using Caliburn.Micro;
using TaskTimer.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTimer.WPF.ViewModels
{
    public class ShellViewModel : PropertyChangedBase
    {
        public ShellViewModel()
        {
            MenuItems = new ObservableCollection<ShellMenuItem>();
        }

        public ObservableCollection<ShellMenuItem> MenuItems { get; private set; }

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

        public object CurrentView
        {
            get {
                var screenView = _selectedMenuItem == null ? MenuItems.FirstOrDefault()?.ScreenViewModel : _selectedMenuItem.ScreenViewModel;
                screenView?.Init();
                return screenView;
            }
        }
    }
}
