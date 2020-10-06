using System;

namespace TaskTimer.Contracts.Db
{
    public class DbProjectDto
    {
        public int Id { get; set; }

        public string Company { get; set; }

        public string ProjectName { get; set; }

        public DateTime AddedDate { get; set; }

        public bool IsActive { get; set; }
    }
}
