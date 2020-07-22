using SQLite;

namespace TaskTimer.Database.DbModels
{
    public class DbConfigModel
    {
        [PrimaryKey]
        public int Id { get; set; }

        public bool? AutoSave { get; set; }
    }
}
