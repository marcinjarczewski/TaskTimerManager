using SQLite;

namespace TaskTimer.Database.DbModels
{
    public class DbProjectEntryModel
    {
        [PrimaryKey]
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string AddedDate { get; set; }

        public bool IsActive { get; set; }
    }
}
