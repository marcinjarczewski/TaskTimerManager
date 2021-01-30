using Caliburn.Micro;
using Newtonsoft.Json;
using System;
using System.Linq;
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

        private IDbAccess _database;

        public bool ReportedTimeIsVisible { get; set; }

        public bool DateEndIsVisible { get; set; }

        public bool TimeIsVisible { get; set; }

        public bool InvoiceTimeIsVisible { get; set; }

        public bool InvoiceIsVisible { get; set; }

        public bool InvoiceDescriptionIsVisible { get; set; }

        public EditTaskViewModel(INavigator navigator, DbTaskDto task, IDbAccess database, bool showInvoices)
        {
            ReportedTimeIsVisible = task.IsEnded;
            TimeIsVisible = task.IsEnded;
            InvoiceTimeIsVisible = task.IsEnded && showInvoices;
            DateEndIsVisible = task.IsEnded;
            InvoiceIsVisible = showInvoices;
            InvoiceDescriptionIsVisible = showInvoices;
            Task = task;
            _navigator = navigator;
            _database = database;
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
            var config = _database.GetConfig();
            var client = _database.GetClients().FirstOrDefault(d => d.Id == Task.ClientId);
            var exportData = new
            {
                ClientName = client.SearchName,
                ClientId = client.SearchId,
                Description = Task.Description,
                Subject = Task.Subject,
                InvoiceSubject = Task.InvoiceSubject,
                InvoiceDescription = Task.InvoiceDescription,
                ReportedTime = Math.Round(Task.ReportedTimeInSeconds / 3600.0M, 2,MidpointRounding.AwayFromZero),
                InvoiceReportedTime = Math.Round(Task.InvoiceReportedTimeInSeconds / 3600.0M, 2, MidpointRounding.AwayFromZero),
                StartDate = Task.StartDate,
                EndDate = Task.EndDate,
                Owner = config.OTRSName,
                Queue = config.OTRSQueue
            };
            Clipboard.SetText(JsonConvert.SerializeObject(exportData));
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
