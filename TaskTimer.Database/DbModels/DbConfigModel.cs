using SQLite;

namespace TaskTimer.Database.DbModels
{
    public class DbConfigModel
    {
        [PrimaryKey]
        public int Id { get; set; }

        public bool? AutoSave { get; set; }

        public bool? CopyDataToInvoice { get; set; }

        public int? RoundReportedTime { get; set; }

        public bool? DisableInvoices { get; set; }
    }
}
