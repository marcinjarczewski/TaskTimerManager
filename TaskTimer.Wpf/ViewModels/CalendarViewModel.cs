using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TaskTimer.Contracts.Bootstrappers;
using TaskTimer.Wpf.Models;

namespace TaskTimer.Wpf.ViewModels
{
    public class CalendarViewModel : Screen, IScreenViewModel, INotifyPropertyChanged
    {
        private DateTime _date;

        public string MonthString
        {
            get { return char.ToUpper(_date.ToString("MMMM")[0]) + _date.ToString("MMMM").Substring(1) + " " + _date.Year.ToString(); }
        }

        private BindableCollection<WeekViewModel> _weeks;

        public BindableCollection<WeekViewModel> Weeks
        {
            get { return _weeks; }
            set { _weeks = value; }
        }

        private List<TaskModel> _tasks { get; set; }
        private System.Action<int,int> _filterDelegation { get; set; }

        /// <summary>
        /// for designer
        /// </summary>
        public CalendarViewModel()
        {
            Weeks = new BindableCollection<WeekViewModel>();
            _tasks = new List<TaskModel>();
        }

        public CalendarViewModel(DateTime date, List<TaskModel> tasks, System.Action<int, int> filterTasks)
        {
            _date = date;
            _tasks = tasks;
            _filterDelegation = filterTasks;
            Weeks = new BindableCollection<WeekViewModel>();
            GenerateWeeksForMonth();
        }

        private void GenerateWeeksForMonth()
        {
            Weeks.Clear();
            var monthNumber = _date.Month;
            var curDate = new DateTime(_date.Year, monthNumber, 1);
            while (curDate.Month == monthNumber)
            {
                Weeks.Add(new WeekViewModel(curDate, _tasks, monthNumber));
                curDate = curDate.AddDays(7);
            }
            var mondayDiff = (int)curDate.DayOfWeek - 1;
            if (mondayDiff < 0)
            {
                mondayDiff += 7;
            }
            var monday = curDate.AddDays(-mondayDiff).Date;
            if (monday.Month == monthNumber)
            {
                Weeks.Add(new WeekViewModel(curDate, _tasks, monthNumber));
            }
        }

        public void GetPreviousMonth()
        {            
            _date = _date.AddMonths(-1);
            GenerateWeeksForMonth();
            OnPropertyChanged("MonthString");
            OnPropertyChanged("Weeks");
            _filterDelegation(_date.Year, _date.Month);
        }

        public void GetNextMonth()
        {
            _date = _date.AddMonths(1);
            GenerateWeeksForMonth();
            OnPropertyChanged("MonthString");
            OnPropertyChanged("Weeks");
            _filterDelegation(_date.Year, _date.Month);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void Init()
        {
        }
    }
}
