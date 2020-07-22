using Caliburn.Micro;
using TaskTimer.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TaskTimer.WPF.DesignTime.Models
{
    public class DesignShellViewModel : PropertyChangedBase
    {
        public DesignShellViewModel()
        {
            MenuItems = new ObservableCollection<ShellMenuItem>();
            MenuItems.Add(new ShellMenuItem() { Caption = "Convert" });
            MenuItems.Add(new ShellMenuItem() { Caption = "Settings"});      
        }

        public ObservableCollection<ShellMenuItem> MenuItems { get; private set; }
    }
}
