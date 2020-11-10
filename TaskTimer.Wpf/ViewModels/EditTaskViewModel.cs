using Caliburn.Micro;
using Newtonsoft.Json;
using System;
using System.Windows;
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

        public bool ReportedTimeIsVisible { get; set; }

        public bool DateEndIsVisible { get; set; }

        public bool TimeIsVisible { get; set; }

        public bool InvoiceTimeIsVisible { get; set; }

        public bool InvoiceIsVisible { get; set; }

        public bool InvoiceDescriptionIsVisible { get; set; }

        public EditTaskViewModel(INavigator navigator, DbTaskDto task, bool showInvoices)
        {
            ReportedTimeIsVisible = task.IsEnded;
            TimeIsVisible = task.IsEnded;
            InvoiceTimeIsVisible = task.IsEnded && showInvoices;
            DateEndIsVisible = task.IsEnded;
            InvoiceIsVisible = showInvoices;
            InvoiceDescriptionIsVisible = showInvoices;
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
            if(!Task.IsActive)
            {
                var res = _navigator.ShowDialog(true, "Usuń zadanie", "Czy na pewno chcesz usunąć zadanie?");
                if(!res)
                {
                    Task.IsActive = true;
                }
                callback(res);
                return;
            }
            callback(_navigator.ShowDialog(true, "Anuluj edycji zadania", "Czy na pewno chcesz anulować edycję zadania?"));         
        }

        public void Init()
        {
        }


        public void Cancel()
        {
            TryClose(false);
        }


        public void Delete()
        {
            Task.IsActive = false;
            TryClose(true);
        }

        public void SaveTask()
        {
            _isSaved = true;
            string validateError = Validate();
            if (string.IsNullOrEmpty(validateError))
            {
                TryClose(true);
            }
            else
            {
                _navigator.ShowDialog("Walidacja zadania", validateError);
            }
        }

        public void Export()
        {
            Clipboard.SetText(JsonConvert.SerializeObject(Task));
            _navigator.ShowDialog("Eksport", "Dane zapisane do schowka");         
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
