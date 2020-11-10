using SQLite;

namespace TaskTimer.Database.DbModels
{
    public class DbClientModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string SearchName { get; set; }

        public string AddedDate { get; set; }

        public string ExportedDate { get; set; }

        public bool IsActive { get; set; }

        public int Priority { get; set; }

        public string Description { get; set; }
    }
}
