using SQLite;

namespace TaskTimer.Database.DbModels
{
    public class DbTaskModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public string InvoiceSubject { get; set; }

        public string InvoiceDescription { get; set; }

        public int ClientId { get; set; }

        public string ClientName { get; set; }

        public string AddedDate { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public int TimeInSeconds { get; set; }

        public int ReportedTimeInSeconds { get; set; }

        public int InvoiceReportedTimeInSeconds { get; set; }

        public bool IsEnded { get; set; }

        public bool IsPaused { get; set; }

        public bool IsActive { get; set; }

        public string LastExportDate { get; set; }
    }
}
