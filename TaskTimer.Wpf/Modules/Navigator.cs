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
            var dialog = new DialogViewModel("a", "bc");
            this.manager.ShowDialog(dialog);
            return new DbClientDto
            {
                AddedDate = DateTime.Now
            };
        }

        public void ShowDialog(string title, string text)
        {
            var dialog = new DialogViewModel(title, text);
            this.manager.ShowDialog(dialog);
        }
    }
}