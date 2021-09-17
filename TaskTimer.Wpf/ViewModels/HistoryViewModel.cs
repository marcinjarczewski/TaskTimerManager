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
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using TaskTimer.Wpf.Helpers;

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

        private TaskItemViewModel _selectedTask;

        public TaskItemViewModel SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
                OnPropertyChanged("CanEditSelectedTask");
            }
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

        private DateTime _DateFrom;

        public DateTime DateFrom
        {
            get { return _DateFrom; }
            set
            {
                _DateFrom = value;
                FilterItems();
                OnPropertyChanged("DateFrom");
                OnPropertyChanged("Tasks");
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
                OnPropertyChanged("Tasks");
            }
        }

        public HistoryViewModel(IDbAccess db, IMapper mapper, INavigator navigator)
        {
            _database = db;
            _mapper = mapper;
            _navigator = navigator;
            Tasks = new BindableCollection<TaskItemViewModel>();
        }

        public void SetDate(int year, int month)
        {
            DateFrom = new DateTime(year, month, 1);
            DateTo = new DateTime(year, month, 1).AddMonths(1).AddDays(-1);
        }

        public void FilterItems()
        {
            var tasks = _database.GetHistoryTasks().OrderBy(t => t.ClientName).ThenByDescending(t =>t.EndDate).ToList();
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
            //Tasks.Clear();
            //Tasks.AddRange(new BindableCollection<TaskItemViewModel>(tasks.Select(t => new TaskItemViewModel(_mapper, _database, _navigator, _mapper.Map<TaskModel>(t), Tasks)).ToList()));
            DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
            OnPropertyChanged("Calendar");
            //foreach (var mappedI in mapped)
            //{
            //    mappedI.EndDate = mappedI.StartDate;
            //}
        }


        public bool CanEditSelectedTask { get { return SelectedTask != null && SelectedTask.Task != null && SelectedTask.Task.Id != 0; } }

        public void ExportRange()
        {
            var workbook = new XSSFWorkbook();
            IFont font = workbook.CreateFont();
            font.IsBold = true;
            //font.FontHeightInPoints = 11;
            ICellStyle boldStyle = workbook.CreateCellStyle();
            boldStyle.SetFont(font);

            var sheet = workbook.CreateSheet("Raport");
            int rowCounter = 0;
            int cellCounter = 0;
            var row = CreateRow(sheet,ref rowCounter,ref cellCounter);
            WriteToCell(row, ref cellCounter, "Raport " + DateFrom.ToString("dd-MM-yyyy") + ":" + DateTo.ToString("dd-MM-yyyy"), boldStyle);
            row = CreateRow(sheet, ref rowCounter, ref cellCounter);
            var groupedTasks = Tasks.GroupBy(t => t.Task.ClientName);
            var timeConverter = new TimerConverter();
            foreach (var groupedTask in groupedTasks)
            {
                row = CreateRow(sheet, ref rowCounter, ref cellCounter);
                WriteToCell(row, ref cellCounter, groupedTask.Key, boldStyle);
                WriteToCell(row, ref cellCounter, "łącznie: " + groupedTask.Sum(g => ((double)g.Task.ReportedTimeInSeconds)/60.0/60.0).ToString("0.##") + "h", boldStyle);
                row = CreateRow(sheet, ref rowCounter, ref cellCounter);
                WriteToCell(row, ref cellCounter, "Temat");
                WriteToCell(row, ref cellCounter, "Opis");
                WriteToCell(row, ref cellCounter, "Od kiedy");
                WriteToCell(row, ref cellCounter, "Do kiedy");
                WriteToCell(row, ref cellCounter, "Zaraportowany czas pracy");
                foreach (var task in groupedTask)
                {
                    row = CreateRow(sheet, ref rowCounter, ref cellCounter);
                    WriteToCell(row, ref cellCounter, task.Task.Subject);
                    WriteToCell(row, ref cellCounter, task.Task.Description);
                    WriteToCell(row, ref cellCounter, task.Task.StartDate.ToString("dd-MM-yyyy HH:mm"));
                    WriteToCell(row, ref cellCounter, task.Task.EndDate.Value.ToString("dd-MM-yyyy HH:mm"));
                    WriteToCell(row, ref cellCounter, (((double)task.Task.ReportedTimeInSeconds) / 60.0 / 60.0).ToString("0.##"));
                }
                //timeConverter.Convert(groupedTask.Sum(g => g.Task.ReportedTimeInSeconds), null, null, null).ToString()
                row = CreateRow(sheet, ref rowCounter, ref cellCounter);
            }
            using (FileStream stream = new FileStream("raport.xlsx", FileMode.Create, FileAccess.Write))
            {
                workbook.Write(stream);
            }
        }

        private IRow CreateRow(ISheet sheet, ref int rowNumber, ref int cellCounter)
        {
            var row = sheet.CreateRow(rowNumber++);
            cellCounter = 0;
            return row;
        }

        private void WriteToCell(IRow row, ref int cellNumber, string value, ICellStyle cellStyle = null)
        {
            var cell = row.CreateCell(cellNumber);
            if(cellStyle != null)
            {
                cell.CellStyle = cellStyle;
            }
            cell.SetCellValue(value);
            cellNumber++;
        }

        public void EditSelectedTask()
        {
            var taskDto = _navigator.NewTimer(_mapper.Map<DbTaskDto>(SelectedTask.Task), !_database.GetConfig().DisableInvoices);
            if (taskDto != null)
            {
                _database.EditTask(taskDto);
                SelectedTask.Task.Subject = taskDto.Subject;
                SelectedTask.Task.Description = taskDto.Description;
                SelectedTask.Task.InvoiceDescription = taskDto.InvoiceDescription;
                SelectedTask.Task.ReportedTimeInSeconds = taskDto.ReportedTimeInSeconds;
                SelectedTask.Task.InvoiceReportedTimeInSeconds = taskDto.InvoiceReportedTimeInSeconds;
                SelectedTask.Task.InvoiceSubject = taskDto.InvoiceSubject;
                SelectedTask.Task.StartDate = taskDto.StartDate;
                SelectedTask.Task.IsActive = taskDto.IsActive;
                NotifyOfPropertyChange(() => SelectedTask.Task.Subject);
                if (!taskDto.IsActive)
                {
                    Tasks.Remove(SelectedTask);
                }
                NotifyOfPropertyChange(() => Tasks);
                Calendar.GenerateWeeksForMonth();
                NotifyOfPropertyChange(() => Calendar);
            }
            //_navigator.ShowDialog(Task.ClientName, Task.Subject);
        }
    }
}
