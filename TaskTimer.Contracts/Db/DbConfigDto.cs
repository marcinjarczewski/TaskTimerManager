namespace TaskTimer.Contracts.Db
{
    public class DbConfigDto
    {
        public int Id { get; set; }

        public bool DisableInvoices { get; set; }

        public bool CopyDataToInvoice { get; set; }

        public bool? AutoSave { get; set; }
    }
}
