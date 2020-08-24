using AutoMapper;
using Caliburn.Micro;
using Microsoft.Win32;
//using TaskTimer.Domain;
using TaskTimer.Contracts;
using TaskTimer.Contracts.Db;
using TaskTimer.Contracts.Bootstrappers;
using System;
using System.IO;

namespace TaskTimer.Wpf.ViewModels
{
    public class MainViewModel : PropertyChangedBase, IScreenViewModel
    {
        private IDbAccess _database;
        private IMapper _mapper;
        private INavigator _navigator;

        private string _SingleFile;

        public string SingleFile
        {
            get { return _SingleFile; }
            set
            {
                _SingleFile = value;
                NotifyOfPropertyChange(() => SingleFile);
            }
        }

        private string _TargetPath;

        public string TargetPath
        {
            get { return _TargetPath; }
            set { _TargetPath = value; }
        }

        private string _SourcePath;

        public string SourcePath
        {
            get { return _SourcePath; }
            set { _SourcePath = value; }
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
        }
        /// <summary>
        /// Calls every time when view is activated.
        /// </summary>
        public void Init()
        {
            var config = _database.GetConfig();
            SourcePath = config.SourceFolderPath;
            TargetPath = config.TargetFolderPath;
        }
    }
}
