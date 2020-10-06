using SQLite;

namespace TaskTimer.Database.DbModels
{
    public class DbProjectModel
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Company { get; set; }

        public string ProjectName { get; set; }

        public string AddedDate { get; set; }

        public bool IsActive { get; set; }
    }
}
