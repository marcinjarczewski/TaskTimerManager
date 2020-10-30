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
    public class DesignWeekViewModel : PropertyChangedBase
    {
        private List<DayViewModel> _days;

        public List<DayViewModel> Days
        {
            get { return _days; }
            set { _days = value; }
        }

        private List<TaskModel> _tasks { get; set; }



        public DesignWeekViewModel()
        {
            int monthNumber = DateTime.Now.Month;
            var mondayDiff = (int)DateTime.Now.DayOfWeek - 1;
            var monday = DateTime.Now.AddDays(-mondayDiff).Date;
            Days = new List<DayViewModel>();
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

            _tasks = tasks;
            for (int i = 0; i < 7; i++)
            {
                var day = monday.Date.AddDays(i);
                Days.Add(new DayViewModel(day, tasks.Where(t => t.EndDate?.Day == day.Day).ToList(), monthNumber));
            }
        }
    }
}
