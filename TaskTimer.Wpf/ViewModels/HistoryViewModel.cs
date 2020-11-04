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
using System.ComponentModel;

namespace TaskTimer.Wpf.ViewModels
{
    public class HistoryViewModel : Screen, IScreenViewModel, INotifyPropertyChanged
    {
        private IDbAccess _database;
        private IMapper _mapper;
        private INavigator _navigator;


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


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

        private TaskItemViewModel _selectedTask;

        public TaskItemViewModel SelectedTask
        {
            get { return _selectedTask; }
            set { _selectedTask = value; }
        }


        private DateTime _DateFrom;

        public DateTime DateFrom
        {
            get { return _DateFrom; }
            set
            {
                _DateFrom = value;
                FilterItems();
                OnPropertyChanged("DateFrom");
            }
        }

        private DateTime _DateTo;

        public DateTime DateTo
        {
            get { return _DateTo; }
            set
            {
                _DateTo = value;
                FilterItems();
                OnPropertyChanged("DateTo");
            }
        }


        public HistoryViewModel(IDbAccess db, IMapper mapper, INavigator navigator)
        {
            _database = db;
            _mapper = mapper;
            _navigator = navigator;
        }

        public void SetDate(int year, int month)
        {
            DateFrom = new DateTime(year, month, 1);
            DateTo = new DateTime(year, month, 1).AddMonths(1).AddDays(-1);
        }

        public void FilterItems()
        {
            var tasks = _database.GetHistoryTasks();
            var mapped = _mapper.Map<List<TaskModel>>(tasks);
            if (DateFrom != default(DateTime))
            {
                tasks = tasks.Where(t => t.EndDate?.Date >= DateFrom).ToList();
            }
            if (DateTo != default(DateTime))
            {
                tasks = tasks.Where(t => t.EndDate?.Date <= DateTo).ToList();
            }
            Tasks.Clear();
            Tasks.AddRange(tasks.Select(t => new TaskItemViewModel(_mapper, _database, _navigator, _mapper.Map<TaskModel>(t), Tasks, true)));
            //Tasks = new BindableCollection<TaskItemViewModel>(tasks.Select(t => new TaskItemViewModel(_mapper, _database, _navigator, _mapper.Map<TaskModel>(t), Tasks)).ToList());
        }

        public void Init()
        {
            var tasks = _database.GetHistoryTasks();
            var mapped = _mapper.Map<List<TaskModel>>(tasks);
            System.Action<int, int> filterDelegate = SetDate;
            Calendar = new CalendarViewModel(DateTime.Now, mapped, filterDelegate);
            Tasks = new BindableCollection<TaskItemViewModel>(tasks.Select(t => new TaskItemViewModel(_mapper, _database, _navigator, _mapper.Map<TaskModel>(t), Tasks)).ToList());
            DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
            //foreach (var mappedI in mapped)
            //{
            //    mappedI.EndDate = mappedI.StartDate;
            //}
        }
    }
}
