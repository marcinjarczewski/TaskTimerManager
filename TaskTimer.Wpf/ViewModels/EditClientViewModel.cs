using Caliburn.Micro;
using System;
using TaskTimer.Contracts;
using TaskTimer.Contracts.Bootstrappers;
using TaskTimer.Contracts.Db;

namespace TaskTimer.Wpf.ViewModels
{
    public class EditClientViewModel : Screen, IScreenViewModel
    {
        private DbClientDto _client;

        public DbClientDto Client
        {
            get { return _client; }
            set { _client = value; }
        }

        public EditClientViewModel(INavigator navigator)
        {
            _navigator = navigator;
            Client = new DbClientDto
            {
                AddedDate = DateTime.Now,
                IsActive = true,
                Priority = 1
            };
        }

        private bool _isSaved;

        private INavigator _navigator;

        public bool IsEditClient { get; set; }

        /// <summary>
        /// for designer
        /// </summary>
        public EditClientViewModel()
        {
            Client = new DbClientDto();
        }

        public EditClientViewModel(INavigator navigator, DbClientDto client)
        {
            IsEditClient = true;
            Client = client;
            _navigator = navigator;
        }

        public override void CanClose(Action<bool> callback)
        {
            if (_isSaved)
            {
                callback(true);
                return;
            }
            callback(_navigator.ShowDialog(true, "Anuluj tworzenie klienta", "Czy na pewno chcesz anulować tworzenie klienta?"));         
        }

        public void Init()
        {
        }

        public void SaveClient()
        {
            _isSaved = true;
            string validateError = Validate();
            if (string.IsNullOrEmpty(validateError))
            {
                TryClose(true);
            }
            else
            {
                _navigator.ShowDialog("Walidacja klienta", validateError);
            }
        }

        public void Cancel()
        {
                TryClose(false);
        }

        public string Validate()
        {
            if(string.IsNullOrEmpty(Client.Name))
            {
                return "Nazwa klienta nie może być pusta";
            }

            return "";
        }
    }
}
