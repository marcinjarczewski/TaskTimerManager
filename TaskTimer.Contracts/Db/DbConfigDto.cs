namespace TaskTimer.Contracts.Db
{
    public class DbConfigDto
    {
        public int Id { get; set; }

        public bool DisableInvoices { get; set; }

        public bool CopyDataToInvoice { get; set; }

        public int? RoundReportedTime { get; set; }

        public string Language { get; set; }

        public string LanguageCode { get; set; }

        public string OTRSQueue { get; set; }

        public string OTRSName { get; set; }
    }
}
