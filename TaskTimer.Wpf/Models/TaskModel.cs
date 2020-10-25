using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTimer.Wpf.Models
{
    public class TaskModel : INotifyPropertyChanged
    {
        public int Id { get; set; }


        private string _Subject;
        public string Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }

        public string Description { get; set; }

        public string InvoiceSubject { get; set; }

        public string InvoiceDescription { get; set; }

        public int ClientId { get; set; }

        public string ClientName { get; set; }

        public DateTime AddedDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int TimeInSeconds { get; set; }

        public int ReportedTimeInSeconds { get; set; }

        public int InvoiceReportedTimeInSeconds { get; set; }

        public bool IsEnded { get; set; }

        public bool IsPaused { get; set; }

        public bool IsActive { get; set; }

        public DateTime? LastExportDate { get; set; }

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
