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
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace TaskTimer.Wpf.ViewModels
{
    public class HistoryViewModel : Screen, IScreenViewModel
    {
        private IDbAccess _database;
        private IMapper _mapper;
        private INavigator _navigator;

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
            set {
                _calendar = value;
            }
        }

        private DateTime _DateFrom;

        public DateTime DateFrom
        {
            get { return _DateFrom; }
            set { _DateFrom = value; }
        }

        private DateTime _DateTo;

        public DateTime DateTo
        {
            get { return _DateTo; }
            set { _DateTo = value; }
        }


        public HistoryViewModel(IDbAccess db, IMapper mapper, INavigator navigator)
        {
            _database = db;
            _mapper = mapper;
            _navigator = navigator;
        }

        public void Init()
        {
            var tasks = _database.GetHistoryTasks();
            var mapped = _mapper.Map<List<TaskModel>>(tasks);
            Tasks = new BindableCollection<TaskItemViewModel>(tasks.Select(t => new TaskItemViewModel(_mapper, _database, _navigator, _mapper.Map<TaskModel>(t), Tasks)).ToList());
            foreach (var mappedI in mapped)
            {
                mappedI.EndDate = mappedI.StartDate;
            }
            Calendar = new CalendarViewModel(DateTime.Now, mapped);

            DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
        }
    }
}
