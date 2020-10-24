using Caliburn.Micro;
using System;
using TaskTimer.Contracts;
using TaskTimer.Contracts.Db;
using TaskTimer.Wpf.ViewModels;

namespace TaskTimer.Wpf.Modules
{
    public class Navigator : INavigator
    {
        IWindowManager manager = new WindowManager();
        public Navigator()
        {
        }

        public DbProjectDto EditProject(DbProjectDto project = null)
        {
            throw new System.NotImplementedException();
        }

        public DbClientDto NewClient(DbClientDto client = null)
        {
            var dialog = client != null ? new EditClientViewModel(this, client) : new EditClientViewModel(this);
            var result = this.manager.ShowDialog(dialog) ?? false;
            if(!result)
            {
                return null;
            }
            return dialog.Client;
        }

        public DbTaskDto NewTimer(DbTaskDto client = null)
        {
            return null;
            //var dialog = client != null ? new EditClientViewModel(this, client) : new EditClientViewModel(this);
            //var result = this.manager.ShowDialog(dialog) ?? false;
            //if (!result)
            //{
            //    return null;
            //}
            //return dialog.Client;
        }

        public void ShowDialog(string title, string text)
        {
            var dialog = new DialogViewModel(title, text);
            this.manager.ShowDialog(dialog);
        }

        public bool ShowDialog(bool isConfirm, string title, string text)
        {
            var dialog = new DialogViewModel(isConfirm, title, text);
            return this.manager.ShowDialog(dialog) ?? false;
        }
    }
}