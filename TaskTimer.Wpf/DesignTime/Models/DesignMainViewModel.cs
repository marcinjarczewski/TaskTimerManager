using Caliburn.Micro;
using TaskTimer.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TaskTimer.Wpf.Models;
using TaskTimer.Contracts.Bootstrappers;

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
            Tasks = new BindableCollection<TaskModel>();
            Tasks.Add(new TaskModel() { ClientName = "C1", Subject = "wdrożenie RP" });
            Tasks.Add(new TaskModel() { ClientName = "C1", Subject = "obsługa telefoniczna" });
            Tasks.Add(new TaskModel() { ClientName = "Client Name 2", Subject = "obsługa telefoniczna" });
            SelectedClient = Clients[2];
        }


        private BindableCollection<ClientModel> _Clients;

        public BindableCollection<ClientModel> Clients
        {
            get { return _Clients; }
            set { _Clients = value; }
        }

        private BindableCollection<TaskModel> _tasks;

        public BindableCollection<TaskModel> Tasks
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
            throw new NotImplementedException();
        }
    }
}
