using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskTimer.Contracts.Bootstrappers;
using TaskTimer.Wpf.Models;

namespace TaskTimer.Wpf.ViewModels
{
    public class WeekViewModel : Screen, IScreenViewModel
    {

        private List<DayViewModel> _days;

        public List<DayViewModel> Days
        {
            get { return _days; }
            set { _days = value; }
        }

        private List<TaskModel> _tasks { get; set; }

        /// <summary>
        /// for designer
        /// </summary>
        public WeekViewModel()
        {
            Days = new List<DayViewModel>();
            _tasks = new List<TaskModel>();
        }

        public WeekViewModel(DateTime date, List<TaskModel> tasks, int monthNumber)
        {
            _tasks = tasks;
            var mondayDiff = (int)date.DayOfWeek - 1;
            if(mondayDiff < 0)
            {
                mondayDiff += 7;
            }
            var monday = date.AddDays(-mondayDiff).Date;
            Days = new List<DayViewModel>();
            for (int i = 0; i < 7; i++)
            {
                var day = monday.Date.AddDays(i);
                Days.Add(new DayViewModel(day, tasks.Where(t => t.EndDate?.Date == day.Date).ToList(), monthNumber));
            }
        }


        public void Init()
        {
        }
    }
}
