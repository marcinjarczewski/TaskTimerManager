using Caliburn.Micro;
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

        public void ShowDialog(string title, string text)
        {
            //var dialog = new DialogViewModel(title, text);
            //this.manager.ShowDialogAsync(dialog);
        }
    }
}