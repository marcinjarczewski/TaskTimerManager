using Caliburn.Micro;
using TaskTimer.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TaskTimer.Wpf.Models;

namespace TaskTimer.Wpf.DesignTime.Models
{
    public class DesignDayViewModel : PropertyChangedBase
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

        private string _Task1Text;

        public string Task1Text
        {
            get { return "112 12 2"; }
        }

        private string _Task2Text;

        public string Task2Text
        {
            get { return "312 12 2"; }
        }

        private string _Task3Text;

        public string Task3Text
        {
            get { return "f a 312 12 2"; }
        }

        private bool _task1IsVisible;

        public bool Task1IsVisible
        {
            get { return _task1IsVisible; }
            set { _task1IsVisible = value; }
        }

        private bool _task2IsVisible;

        public bool Task2IsVisible
        {
            get { return _task2IsVisible; }
            set { _task2IsVisible = value; }
        }

        private bool _task3IsVisible;

        public bool Task3IsVisible
        {
            get { return _task3IsVisible; }
            set { _task3IsVisible = value; }
        }

        private bool _moreTaskIsVisible;

        public bool MoreTaskIsVisible
        {
            get { return _moreTaskIsVisible; }
            set { _moreTaskIsVisible = value; }
        }

        public DesignDayViewModel()
        {
            Day = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek + 1).Day;
            Task1IsVisible = true;
            Task2IsVisible = true;
            Task3IsVisible = true;
            MoreTaskIsVisible = true;
        }
    }
}
