using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTimer.Wpf.Models
{
    public class ClientModel : INotifyPropertyChanged
    {

        private int _id;

        public int Id
        {
            get { return _id; }
            set {
                _id = value;
                OnPropertyChanged("Id");
            }
        }



        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; OnPropertyChanged("Name"); }
        }

        
        public string SearchName { get; set; }

        public DateTime AddedDate { get; set; }

        public DateTime? ExportedDate { get; set; }

        public bool IsActive { get; set; }

        public int Priority { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
