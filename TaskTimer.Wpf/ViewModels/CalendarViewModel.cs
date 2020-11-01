using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskTimer.Contracts.Bootstrappers;
using TaskTimer.Wpf.Models;

namespace TaskTimer.Wpf.ViewModels
{
    public class CalendarViewModel : Screen, IScreenViewModel
    {
        private DateTime _date;

        public string MonthString
        {
            get { return char.ToUpper(_date.ToString("MMMM")[0]) + _date.ToString("MMMM").Substring(1) + " " + _date.Year.ToString(); }
        }

        private List<WeekViewModel> _weeks;

        public List<WeekViewModel> Weeks
        {
            get { return _weeks; }
            set { _weeks = value; }
        }

        private List<TaskModel> _tasks { get; set; }

        /// <summary>
        /// for designer
        /// </summary>
        public CalendarViewModel()
        {
            Weeks = new List<WeekViewModel>();
            _tasks = new List<TaskModel>();
        }

        public CalendarViewModel(DateTime date, List<TaskModel> tasks)
        {
            _date = date;
            _tasks = tasks;
            var monthNumber = date.Month;
            Weeks = new List<WeekViewModel>();
            var curDate = new DateTime(date.Year, monthNumber, 1);
            while (curDate.Month == monthNumber)
            {
                Weeks.Add(new WeekViewModel(curDate, tasks, monthNumber));
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
                Weeks.Add(new WeekViewModel(curDate, tasks, monthNumber));
            }

        }


        public void Init()
        {
        }
    }
}
