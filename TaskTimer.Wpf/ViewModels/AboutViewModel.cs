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
    public class AboutViewModel : PropertyChangedBase, IScreenViewModel
    { 
        private readonly IDbAccess _database;
        private readonly IMapper _mapper;

       
        /// <summary>
        /// Calls just once.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="mapper"></param>
        public AboutViewModel(IMapper mapper, IDbAccess db)
        {
            _database = db;
            _mapper = mapper;
        }

        /// <summary>
        /// Calls every time when view is activated.
        /// </summary>
        public void Init()
        {
        }
    }
}
