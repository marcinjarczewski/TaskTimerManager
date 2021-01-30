using Caliburn.Micro;
using TaskTimer.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TaskTimer.Wpf.Models;
using TaskTimer.Contracts.Db;

namespace TaskTimer.Wpf.DesignTime.Models
{
    public class DesignClientViewModel : PropertyChangedBase
    {
        private DbClientDto _client;

        public DbClientDto Client
        {
            get { return _client; }
            set { _client = value; }
        }

        public bool IsEditClient { get; set; }

        public DesignClientViewModel()
        {
            IsEditClient = true;
            Client = new DbClientDto
            {
                AddedDate = DateTime.Now,
                IsActive = true,
                Priority = 1,
                Name = "Testowy klient",
                SearchName = "OTRS test k",
                SearchId = "Otrs test Id"
            };
        }
    }
}
