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

namespace TaskTimer.Wpf.ViewModels
{
    public class MainViewModel : Screen, IScreenViewModel
    {
        private IDbAccess _database;
        private IMapper _mapper;
        private INavigator _navigator;

        private BindableCollection<ClientModel> _Clients;

        public BindableCollection<ClientModel> Clients
        {
            get { return _Clients; }
            set { _Clients = value;  }
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
            set {
                _SelectedClient = value;
                NotifyOfPropertyChange(() => SelectedClient);
                NotifyOfPropertyChange(() => CanEditSelectedClient);
                NotifyOfPropertyChange(() => CanAddTask);
            }
        }

        /// <summary>
        /// Calls just once.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="mapper"></param>
        public MainViewModel(IDbAccess db, IMapper mapper, INavigator navigator)
        {
            _database = db;
            _mapper = mapper;
            _navigator = navigator;

            Clients = new BindableCollection<ClientModel>();
            var clients = _database.GetClients();
            _mapper.Map(clients.OrderByDescending(c => c.Priority).ToList(), Clients);
            var tasks = _database.GetActiveTasks();
            Tasks = new BindableCollection<TaskItemViewModel>();
            var taskViewModels = tasks.Select(t => new TaskItemViewModel(_mapper, _database, _navigator, _mapper.Map<TaskModel>(t), Tasks)).ToList();
            Tasks.AddRange(taskViewModels);
        }
        /// <summary>
        /// Calls every time when view is activated.
        /// </summary>
        public void Init()
        {

        }

        public void AddNewClient()
        {
            var client = _navigator.NewClient();
            if(client != null)
            {
                _database.AddClient(client);
                _navigator.ShowDialog("Nowy klient", "Nowy klient został dodany");
            }
        }

        public void AddTask()
        {
            var task = new DbTaskDto
            {
                AddedDate = DateTime.Now,
                StartDate = DateTime.Now,
                IsActive = true,
                ClientName = SelectedClient.Name,             
            };
            task.Id = _database.AddTask(task);
            Tasks.Add(new TaskItemViewModel(_mapper, _database, _navigator, _mapper.Map<TaskModel>(task), Tasks));
            NotifyOfPropertyChange(() => Tasks);
        }

        public bool CanAddTask { get { return SelectedClient != null; } }

        public bool CanEditSelectedClient { get { return SelectedClient != null; } }

        public void EditSelectedClient()
        {
            var client = _navigator.NewClient(_mapper.Map<DbClientDto>(SelectedClient));
            if (client != null)
            {
                _database.EditClient(client);
                SelectedClient.Name = client.Name;
                SelectedClient.SearchName = client.SearchName;
                SelectedClient.Priority = client.Priority;
                NotifyOfPropertyChange(() => SelectedClient);
                _navigator.ShowDialog("Nowy klient", "Edycja klienta zapisana");
            }
        }

        public void SaveTasks()
        {
            Tasks.Select(t =>
            {
                _database.EditTask(_mapper.Map<DbTaskDto>(t));
                return 1;
            });
        }
    }
}
