using Caliburn.Micro;
using System;
using TaskTimer.Contracts;
using TaskTimer.Contracts.Bootstrappers;
using TaskTimer.Contracts.Db;

namespace TaskTimer.Wpf.ViewModels
{
    public class EditTaskViewModel : Screen, IScreenViewModel
    {
        private DbTaskDto _task;

        public DbTaskDto Task
        {
            get { return _task; }
            set { _task = value; }
        }

        private bool _isSaved;

        private INavigator _navigator;

        public bool IsEditClient { get; set; }


        public EditTaskViewModel(INavigator navigator, DbTaskDto task)
        {
            IsEditClient = true;
            Task = task;
            _navigator = navigator;
        }

        public override void CanClose(Action<bool> callback)
        {
            if (_isSaved)
            {
                callback(true);
                return;
            }
            callback(_navigator.ShowDialog(true, "Anuluj tworzenie klienta", "Czy na pewno chcesz anulować tworzenie klienta?"));         
        }

        public void Init()
        {
        }

        public void SaveClient()
        {
            _isSaved = true;
            string validateError = Validate();
            if (string.IsNullOrEmpty(validateError))
            {
                TryClose(true);
            }
            else
            {
                _navigator.ShowDialog("Walidacja klienta", validateError);
            }
        }

        public string Validate()
        {
            if(string.IsNullOrEmpty(Task.Subject))
            {
                return "Nazwa zadania nie może być pusta";
            }

            return "";
        }
    }
}
