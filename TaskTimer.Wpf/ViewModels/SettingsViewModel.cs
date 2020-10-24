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
    public class SettingsViewModel : PropertyChangedBase, IScreenViewModel
    { 
        private readonly IDbAccess _database;
        private readonly IMapper _mapper;

        private bool _IsInvoiceHidden;

        public bool IsInvoiceHidden
        {
            get { return _IsInvoiceHidden; }
            set { _IsInvoiceHidden = value; }
        }

        private bool _IsInvoiceDefault;

        public bool IsInvoiceDefault
        {
            get { return _IsInvoiceDefault; }
            set { _IsInvoiceDefault = value; }
        }


        private DbProjectDto Projects;

        public DbProjectDto MyProperty
        {
            get { return Projects; }
            set { Projects = value; }
        }


        /// <summary>
        /// Calls just once.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="mapper"></param>
        public SettingsViewModel(IMapper mapper, IDbAccess db)
        {
            _database = db;
            _mapper = mapper;
        }

        /// <summary>
        /// Calls every time when view is activated.
        /// </summary>
        public void Init()
        {
            var config = _database.GetConfig();
        }

        public void Save()
        {
            var config = new DbConfigDto
            {
            };
            _database.SaveConfig(config);
        }
    }
}
