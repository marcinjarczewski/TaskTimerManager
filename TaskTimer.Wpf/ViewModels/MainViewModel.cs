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

namespace TaskTimer.Wpf.ViewModels
{
    public class MainViewModel : PropertyChangedBase, IScreenViewModel
    {
        private IDbAccess _database;
        private IMapper _mapper;
        private INavigator _navigator;


        private BindableCollection<ClientModel> _Clients;

        public BindableCollection<ClientModel> Clients
        {
            get { return _Clients; }
            set { _Clients = value; }
        }

        private ClientModel _SelectedClient;

        public ClientModel SelectedClient
        {
            get { return _SelectedClient; }
            set {
                _SelectedClient = value;
                NotifyOfPropertyChange(() => SelectedClient);
                NotifyOfPropertyChange(() => CanEditSelectedClient);
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
            _Clients = new BindableCollection<ClientModel>();
            Clients = new BindableCollection<ClientModel>();
            Clients.Add(new ClientModel { Name = "Klient 1", Priority = 1 });
            Clients.Add(new ClientModel { Name = "Klient 2", Priority = 2 });
            Clients.Add(new ClientModel { Name = "Klient 3", Priority = 3 });
        }
        /// <summary>
        /// Calls every time when view is activated.
        /// </summary>
        public void Init()
        {
            var config = _database.GetConfig();
            //SourcePath = config.SourceFolderPath;
            //TargetPath = config.TargetFolderPath;
        }

        public void AddNewClient()
        {
            var client = _navigator.NewClient();
        }

        public bool CanEditSelectedClient { get { return SelectedClient != null; } }


        public void EditSelectedClient(ClientModel selectedClient)
        {

        }
    }
}
