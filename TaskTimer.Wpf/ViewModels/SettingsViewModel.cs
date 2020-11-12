using AutoMapper;
using Caliburn.Micro;
using Microsoft.Win32;
//using TaskTimer.Domain;
using TaskTimer.Contracts;
using TaskTimer.Contracts.Db;
using TaskTimer.Contracts.Bootstrappers;
using System;
using System.IO;
using System.Threading;
using System.Globalization;
using TaskTimer.Wpf.Models;
using TaskTimer.Wpf.Helpers;
using TaskTimer.Wpf.Properties;

namespace TaskTimer.Wpf.ViewModels
{
    public class SettingsViewModel : PropertyChangedBase, IScreenViewModel
    { 
        private readonly IDbAccess _database;
        private readonly IMapper _mapper;
        private readonly INavigator _navigator;

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

        private LanguageModel _selectedLanguage;

        public LanguageModel SelectedLanguage
        {
            get { return _selectedLanguage; }
            set { _selectedLanguage = value; }
        }

        private BindableCollection<LanguageModel> _languages;

        public BindableCollection<LanguageModel> Languages
        {
            get { return _languages; }
            set { _languages = value; }
        }



        /// <summary>
        /// Calls just once.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="mapper"></param>
        public SettingsViewModel(INavigator navigator, IMapper mapper, IDbAccess db)
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
            CopyDataToInvoice = config.CopyDataToInvoice;
            RoundReportedTime = config.RoundReportedTime ?? 0;            
            DisableInvoices = config.DisableInvoices;
            Languages = new BindableCollection<LanguageModel>
            {
                new LanguageModel
                {
                    Code = LanguagesEnum.Polish.DescriptionAttr(),
                    Name = LanguagesEnum.Polish.TranslatedString(),
                },
                new LanguageModel
                {
                    Code = LanguagesEnum.English.DescriptionAttr(),
                    Name = LanguagesEnum.English.TranslatedString(),
                }
            };
            var languageEnum = EnumHelper.GetValueFromDescription<LanguagesEnum>(config.LanguageCode ?? "pl");
            switch (languageEnum)
            {
                case LanguagesEnum.Polish:
                    SelectedLanguage = Languages[0];
                    break;
                case LanguagesEnum.English:
                    SelectedLanguage = Languages[1];
                    break;
                default:
                    break;
            }
        }

        public void Save()
        {
            var oldConfig = _database.GetConfig();
            var config = new DbConfigDto
            {
                CopyDataToInvoice = CopyDataToInvoice,
                DisableInvoices = DisableInvoices,
                LanguageCode = SelectedLanguage.Code,
                RoundReportedTime = RoundReportedTime
            };
            _database.SaveConfig(config);
            if (SelectedLanguage.Code != (oldConfig.LanguageCode ?? "pl"))
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(SelectedLanguage.Code);
                Thread.CurrentThread.CurrentCulture = new CultureInfo(SelectedLanguage.Code);
                _navigator.ShowDialog(Resources.ChangeLanguage, Resources.ChangeLanguageDescription);
            }
        }
    }
}
