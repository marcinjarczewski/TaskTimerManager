using Caliburn.Micro;
using System;
using System.Collections.Generic;
using TaskTimer.Contracts.Bootstrappers;
using TaskTimer.Wpf.Models;

namespace TaskTimer.Wpf.ViewModels
{
    public class DayViewModel : Screen, IScreenViewModel
    {
        private DateTime _date { get; set; }

        private int _Day;

        public int Day
        {
            get { return _Day; }
            set { _Day = value; }
        }

        private string _MonthName;

        public string MonthName
        {
            get { return _MonthName; }
            set { _MonthName = value; }
        }    

        private List<TaskModel> _tasks;

        public List<TaskModel> Tasks
        {
            get { return _tasks; }
            set { _tasks = value; }
        }


        public string Task1Text
        {
            get { return Tasks?.Count > 0 ? GetNameFromTask(Tasks[0]) : ""; }
        }

        public string Task2Text
        {
            get { return Tasks?.Count > 1 ? GetNameFromTask(Tasks[1]) : ""; }
        }


        public string Task3Text
        {
            get { return Tasks?.Count > 2 ? GetNameFromTask(Tasks[2]) : ""; }
        }

        private bool _task1IsVisible;

        public bool Task1IsVisible
        {
            get { return Tasks?.Count > 0; }
        }

        private bool _task2IsVisible;

        public bool Task2IsVisible
        {
            get { return Tasks?.Count > 1; }
        }

        private bool _task3IsVisible;

        public bool Task3IsVisible
        {
            get { return Tasks?.Count > 2; }
        }

        private bool _moreTaskIsVisible;

        public bool MoreTaskIsVisible
        {
            get { return Tasks?.Count > 3; }
        }

        private bool _IsCurrentMonth;

        public bool IsCurrentMonth
        {
            get { return _IsCurrentMonth; }
            set { _IsCurrentMonth = value; }
        }

        /// <summary>
        /// for designer
        /// </summary>
        public DayViewModel()
        {
            Tasks = new List<TaskModel>();
            _date = DateTime.Now;
        }


        public DayViewModel(DateTime day, List<TaskModel> tasks, int monthNumber)
        {
            _date = day;
            Day = _date.Day;
            Tasks = tasks;
            IsCurrentMonth = _date.Month == monthNumber;
            MonthName =  _date.Month.ToString();
        }

        private string GetNameFromTask(TaskModel task)
        {
            return string.Format("{1}h:{2}: {3}", task.StartDate.ToString("hh:MM"), Math.Round((double)task.ReportedTimeInSeconds / 3600.0, 1).ToString(), task.ClientName, task.Subject);
            //return string.Format("{0} ({1}h) {2} {3}", task.StartDate.TimeOfDay.ToString("hh:MM"), Math.Round((double)task.ReportedTimeInSeconds/60,1),task.ClientName, task.Subject);
        }

        public void Init()
        {
        }
    }
}
