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
        private IDbAccess _db;

        public Navigator(IDbAccess database)
        {
            _db = database;
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

        public DbTaskDto NewTimer(DbTaskDto timer, bool showInvoices)
        {
            var dialog = new EditTaskViewModel(this, timer, _db, showInvoices);
            var result = this.manager.ShowDialog(dialog) ?? false;
            if (!result)
            {
                return null;
            }
            return dialog.Task;
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