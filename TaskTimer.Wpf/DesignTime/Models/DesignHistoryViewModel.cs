using Caliburn.Micro;
using TaskTimer.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TaskTimer.Wpf.Models;
using TaskTimer.Wpf.ViewModels;
using System.Linq;

namespace TaskTimer.Wpf.DesignTime.Models
{
    public class DesignHistoryViewModel : PropertyChangedBase
    {
        private BindableCollection<TaskItemViewModel> _tasks;

        public BindableCollection<TaskItemViewModel> Tasks
        {
            get { return _tasks; }
            set { _tasks = value; }
        }


        private CalendarViewModel _calendar;

        public CalendarViewModel Calendar
        {
            get { return _calendar; }
            set
            {
                _calendar = value;
            }
        }

        public DesignHistoryViewModel()
        {
            var designCalendar = new DesignCalendarViewModel();
            Tasks = new BindableCollection<TaskItemViewModel>();
            Tasks = new BindableCollection<TaskItemViewModel>(designCalendar._tasks.Select(t => new TaskItemViewModel(t)).ToList());
            Calendar = new CalendarViewModel(DateTime.Now,designCalendar._tasks);
        }
    }
}
