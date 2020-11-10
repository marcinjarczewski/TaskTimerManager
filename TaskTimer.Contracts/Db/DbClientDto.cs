using System;

namespace TaskTimer.Contracts.Db
{
    public class DbClientDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string SearchName { get; set; }

        public DateTime AddedDate { get; set; }

        public DateTime? ExportedDate { get; set; }

        public bool IsActive { get; set; }

        public int Priority { get; set; }

        public string Description { get; set; }
    }
}
