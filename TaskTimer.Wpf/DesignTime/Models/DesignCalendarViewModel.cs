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
    public class DesignCalendarViewModel : PropertyChangedBase
    {
        private List<WeekViewModel> _weeks;

        public List<WeekViewModel> Weeks
        {
            get { return _weeks; }
            set { _weeks = value; }
        }

        private DateTime _date;

        public string MonthString
        {
            get { return char.ToUpper(_date.ToString("MMMM")[0]) + _date.ToString("MMMM").Substring(1) + " " + _date.Year.ToString(); }
        }

        public List<TaskModel> _tasks { get; set; }

        public DesignCalendarViewModel()
        {
            _date = DateTime.Now;
            var dateNow = DateTime.Now;
            int monthNumber = dateNow.Month;
            var curDate = new DateTime(dateNow.Year, monthNumber, 1);
            Weeks = new List<WeekViewModel>();
            var tasks = GetTasks(curDate);
            while (curDate.Month == monthNumber)
            {
                Weeks.Add(new WeekViewModel(curDate, tasks, monthNumber));
                curDate = curDate.AddDays(7);
                tasks.AddRange(GetTasks(curDate));
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


            _tasks = tasks;
        }

        private List<TaskModel> GetTasks(DateTime monday)
        {
            var tasks = new List<TaskModel>()
            {
                new TaskModel
                {
                    ClientName = "Abc",
                    Subject = "Test1",
                    StartDate = monday,
                    EndDate = monday
                },
                new TaskModel
                {
                    ClientName = "Abc",
                    Subject = "Test2",
                    StartDate = monday.AddDays(1),
                    EndDate = monday.AddDays(1),
                },
                new TaskModel
                {
                    ClientName = "Abc",
                    Subject = "Test3",
                    StartDate = monday.AddDays(4),
                    EndDate =  monday.AddDays(4),
                },
                new TaskModel
                {
                    ClientName = "Abc",
                    Subject = "Test4",
                    StartDate =  monday.AddDays(5),
                    EndDate =  monday.AddDays(5),
                },
                new TaskModel
                {
                    ClientName = "Testowy klient",
                    Subject = "Temat 1",
                    StartDate = monday,
                    EndDate = monday
                },
                new TaskModel
                {
                    ClientName = "Inny kl",
                    Subject = "Temat 1",
                    StartDate = monday,
                    EndDate = monday
                },
                new TaskModel
                {
                    ClientName = "Testowy klient",
                    Subject = "Wcale nie taka długa nazwa tematu",
                    StartDate = monday,
                    EndDate = monday
                },
                new TaskModel
                {
                    ClientName = "Testowy klient",
                    Subject = "Temat 2",
                    StartDate = monday.AddDays(2),
                    EndDate = monday.AddDays(2),
                },
            };
            return tasks;
        }
    }
}
