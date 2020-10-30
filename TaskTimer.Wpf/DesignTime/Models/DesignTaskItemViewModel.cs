using Caliburn.Micro;
using TaskTimer.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TaskTimer.Wpf.Models;

namespace TaskTimer.Wpf.DesignTime.Models
{
    public class DesignTaskItemViewModel : PropertyChangedBase
    {
        private TaskModel _task;

        public TaskModel Task
        {
            get { return _task; }
            set { _task = value; }
        }

        private BindableCollection<DesignTaskItemViewModel> _parent;

        public BindableCollection<DesignTaskItemViewModel> Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        private int _Time;

        public int Time
        {
            get { return _Time; }
            set
            {
                _Time = value;
                Task.TimeInSeconds = value;
                NotifyOfPropertyChange(() => TimeString);
            }
        }

        private string _TimeString;

        public string TimeString
        {
            get
            {
                string hours = ((int)Time / 3600).ToString("00");
                int time = Time % 3600;
                string minutes = ((int)time / 60).ToString("00");
                string seconds = (time % 60).ToString("00");
                return string.Format("{0}:{1}:{2}", hours, minutes, seconds);
            }
        }

        public bool PauseIsVisible { get; set; }
        public bool PlayIsVisible { get; set; }

        public DesignTaskItemViewModel()
        {
            Task = new TaskModel { ClientName = "Test client" , Subject= "Zadanie testowe" };
            Time = 1000;
            PlayIsVisible = true;
            PauseIsVisible = false;
        }
    }
}
