using SQLite;

namespace TaskTimer.Database.DbModels
{
    public class DbLogModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Message { get; set; }

        public string Exception { get; set; }

        public string AddedDate { get; set; }
    }
}
