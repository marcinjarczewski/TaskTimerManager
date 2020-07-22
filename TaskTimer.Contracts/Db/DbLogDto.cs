namespace TaskTimer.Contracts.Db
{
    public class DbLogDto
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public string Exception { get; set; }

        public string AddedDate { get; set; }
    }
}
