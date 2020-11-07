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

        private bool _DisableInvoices;

        public bool DisableInvoices
        {
            get { return _DisableInvoices; }
            set { _DisableInvoices = value; }
        }

        private bool _CopyDataToInvoice;

        public bool CopyDataToInvoice
        {
            get { return _CopyDataToInvoice; }
            set { _CopyDataToInvoice = value; }
        }

        private int _RoundReportedTime;

        public int RoundReportedTime
        {
            get { return _RoundReportedTime; }
            set { _RoundReportedTime = value; }
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
            CopyDataToInvoice = config.CopyDataToInvoice;
            RoundReportedTime = config.RoundReportedTime ?? 0;
            DisableInvoices = config.DisableInvoices;           
        }

        public void Save()
        {
            var config = new DbConfigDto
            {
                CopyDataToInvoice = CopyDataToInvoice,
                DisableInvoices = DisableInvoices,
                RoundReportedTime = RoundReportedTime
            };
            _database.SaveConfig(config);
        }
    }
}
