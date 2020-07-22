namespace TaskTimer.Contracts.Db
{
    public class DbConfigDto
    {
        public int Id { get; set; }

        public string SourceFolderPath { get; set; }

        public string TargetFolderPath { get; set; }

        public bool? AutoSave { get; set; }
    }
}
