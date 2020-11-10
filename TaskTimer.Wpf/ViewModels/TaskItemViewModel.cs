using AutoMapper;
using Caliburn.Micro;
using Microsoft.Win32;
//using TaskTimer.Domain;
using TaskTimer.Contracts;
using TaskTimer.Contracts.Db;
using TaskTimer.Contracts.Bootstrappers;
using System;
using System.IO;
using TaskTimer.Wpf.Models;

namespace TaskTimer.Wpf.ViewModels
{
    public class TaskItemViewModel : PropertyChangedBase, IScreenViewModel
    {
        private readonly IDbAccess _database;
        private readonly IMapper _mapper;
        private readonly INavigator _navigator;

        private TaskModel _task;

        public TaskModel Task
        {
            get { return _task; }
            set { _task = value; }
        }

        private BindableCollection<TaskItemViewModel> _parent;

        public BindableCollection<TaskItemViewModel> Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        private int _Time;

        public int Time
        {
            get { return _Time; }
            set {
                _Time = value;
                Task.TimeInSeconds = value;
                NotifyOfPropertyChange(() => TimeString);
                }
        }

        private string _TimeString;

        public string TimeString
        {
            get {
                string hours = ((int)Time/3600).ToString("00");
                int time = Time % 3600;
                string minutes = ((int)time / 60).ToString("00");
                string seconds = (time % 60).ToString("00");
                return string.Format("{0}:{1}:{2}", hours, minutes, seconds);
            }
        }

        public bool PauseIsVisible { get; set; }
        public bool PlayIsVisible { get; set; }
        private System.Timers.Timer _timer;
        private System.Timers.Timer _saveTimer;

        /// <summary>
        /// designer
        /// </summary>
        public TaskItemViewModel()
        {
            Task = new TaskModel();
        }



        /// <summary>
        /// designer
        /// </summary>
        public TaskItemViewModel(TaskModel model)
        {
            Task = model;
        }

        /// <summary>
        /// Calls just once.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="mapper"></param>
        public TaskItemViewModel(IMapper mapper, IDbAccess db, INavigator navigator, TaskModel model, BindableCollection<TaskItemViewModel> parent, bool isHistory = false)
        {
            _database = db;
            _mapper = mapper;
            _navigator = navigator;
            Task = model;
            Parent = parent;
            Time = Task.TimeInSeconds;

            if (!isHistory)
            {
                _timer = new System.Timers.Timer();
                _timer.Interval = 1000;
                _timer.Elapsed += OnTimedEvent;
                _timer.AutoReset = true;
                _timer.Enabled = true;

                _saveTimer = new System.Timers.Timer();
                _saveTimer.Interval = 30001;
                _saveTimer.Elapsed += SaveTimedEvent;
                _saveTimer.AutoReset = true;
                _saveTimer.Enabled = true;
            }
            PauseIsVisible = true;
        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            Time++;
        }


        private void SaveTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            _database.EditTask(_mapper.Map<DbTaskDto>(Task));
        }

        /// <summary>
        /// Calls every time when view is activated.
        /// </summary>
        public void Init()
        {
            var config = _database.GetConfig();
        }

        public void Edit()
        {
            var taskDto = _navigator.NewTimer(_mapper.Map<DbTaskDto>(Task),!_database.GetConfig().DisableInvoices && !_database.GetConfig().CopyDataToInvoice);
            if (taskDto != null)
            {
                _database.EditTask(taskDto);
                Task.Subject = taskDto.Subject;
                Task.Description = taskDto.Description;
                Task.InvoiceDescription = taskDto.InvoiceDescription;
                Task.ReportedTimeInSeconds = taskDto.ReportedTimeInSeconds;
                Task.InvoiceReportedTimeInSeconds = taskDto.ReportedTimeInSeconds;
                Task.InvoiceSubject = taskDto.InvoiceSubject;
                Task.StartDate = taskDto.StartDate;
                NotifyOfPropertyChange(() => Task.Subject);
            }
            //_navigator.ShowDialog(Task.ClientName, Task.Subject);
        }

        public void Pause()
        {
            Task.TimeInSeconds = Time;
            Task.IsPaused = true;
            _database.EditTask(_mapper.Map<DbTaskDto>(Task));
            _timer.Stop();
            Task.IsPaused =true;
            PauseIsVisible = false;
            PlayIsVisible = true;
            NotifyOfPropertyChange(() => PauseIsVisible);
            NotifyOfPropertyChange(() => PlayIsVisible);
        }

        public void Play()
        {
            Task.TimeInSeconds = Time;
            Task.IsPaused = false;
            _database.EditTask(_mapper.Map<DbTaskDto>(Task));
            _timer.Start();
            Task.IsPaused = true;
            PauseIsVisible = true;
            PlayIsVisible = false;
            NotifyOfPropertyChange(() => PauseIsVisible);
            NotifyOfPropertyChange(() => PlayIsVisible);
        }

        public void Stop()
        {
            Task.TimeInSeconds = Time;
            Task.IsEnded = true;
            Task.EndDate = DateTime.Now;
           var config = _database.GetConfig();
            if (config.RoundReportedTime.HasValue && config.RoundReportedTime.Value != 0)
            {
                var configValueInMinutes = config.RoundReportedTime.Value * 60;
                if (Task.TimeInSeconds % configValueInMinutes > 0)
                {
                    Task.ReportedTimeInSeconds = (Task.TimeInSeconds / configValueInMinutes + 1) * configValueInMinutes;
                }
                else
                {
                    Task.ReportedTimeInSeconds = configValueInMinutes * 60;
                }
            }
            if (config.CopyDataToInvoice)
            {
                Task.InvoiceDescription = Task.Description;
                Task.InvoiceReportedTimeInSeconds = Task.ReportedTimeInSeconds;
                Task.InvoiceSubject = Task.Subject;
            }
            var taskDto = _navigator.NewTimer(_mapper.Map<DbTaskDto>(Task), !config.DisableInvoices || config.CopyDataToInvoice);
            if (taskDto != null)
            {
                _saveTimer.Stop();
                _timer.Stop();
                _database.EditTask(taskDto);
                Parent.Remove(this);
            }
        }
    }
}
