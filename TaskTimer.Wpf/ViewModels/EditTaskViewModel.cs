using Caliburn.Micro;
using Newtonsoft.Json;
using System;
using System.Windows;
using TaskTimer.Contracts;
using TaskTimer.Contracts.Bootstrappers;
using TaskTimer.Contracts.Db;
using TaskTimer.Wpf.Properties;

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
                var res = _navigator.ShowDialog(true, Resources.DeleteTask, Resources.DeleteTaskDescription);
                if(!res)
                {
                    Task.IsActive = true;
                }
                callback(res);
                return;
            }
            callback(_navigator.ShowDialog(true, Resources.CancelEditTask, Resources.CancelEditTaskDescription));         
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
                _navigator.ShowDialog(Resources.TaskValidation, validateError);
            }
        }

        public bool CanExport()
        {
            return Task.IsEnded;
        }

        public void Export()
        {
            Clipboard.SetText(JsonConvert.SerializeObject(Task));
            _navigator.ShowDialog(Resources.Export, Resources.ExportDescription);         
        }

        public string Validate()
        {
            if(string.IsNullOrEmpty(Task.Subject))
            {
                return Resources.TaskNameValidation;
            }

            return "";
        }
    }
}
