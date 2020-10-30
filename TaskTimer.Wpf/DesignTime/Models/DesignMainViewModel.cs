using Caliburn.Micro;
using TaskTimer.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TaskTimer.Wpf.Models;
using TaskTimer.Contracts.Bootstrappers;
using TaskTimer.Wpf.ViewModels;

namespace TaskTimer.Wpf.DesignTime.Models
{
    public class DesignMainViewModel : PropertyChangedBase, IScreenViewModel
    {
        public DesignMainViewModel()
        {
            Clients = new BindableCollection<ClientModel>();
            Clients.Add(new ClientModel() { Name = "C1", Priority = 1 });
            Clients.Add(new ClientModel() { Name = "Client Name 2", Priority = 2 });
            Clients.Add(new ClientModel() { Name = "Client 3", Priority = 3 });
            Tasks = new BindableCollection<TaskItemViewModel>();
            Tasks.Add(new TaskItemViewModel { PauseIsVisible = false, Time = 150, PlayIsVisible = true,  Task = new TaskModel { ClientName = "C1", Subject = "obsługa telefoniczna" } });
            Tasks.Add(new TaskItemViewModel { Time = 210, PauseIsVisible = true, Task = new TaskModel { ClientName = "C2", Subject = "obsługa ,mailowa" } });
            Tasks.Add(new TaskItemViewModel { Time = 11243, PauseIsVisible = true, Task = new TaskModel { ClientName = "Client Name 2", Subject = "obsługa telefoniczna" } });
            SelectedClient = Clients[2];
        }


        private BindableCollection<ClientModel> _Clients;

        public BindableCollection<ClientModel> Clients
        {
            get { return _Clients; }
            set { _Clients = value; }
        }

        private BindableCollection<TaskItemViewModel> _tasks;

        public BindableCollection<TaskItemViewModel> Tasks
        {
            get { return _tasks; }
            set { _tasks = value; }
        }

        private ClientModel _SelectedClient;

        public ClientModel SelectedClient
        {
            get { return _SelectedClient; }
            set { _SelectedClient = value; NotifyOfPropertyChange(() => SelectedClient); }
        }

        public void Init()
        {
        }
    }
}
