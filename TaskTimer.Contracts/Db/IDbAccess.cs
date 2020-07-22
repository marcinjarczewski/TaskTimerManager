namespace TaskTimer.Contracts.Db
{
    public interface IDbAccess
    {
        void Init();

        void Drop();

        void AddLog(DbLogDto log);

        DbLogDto GetLog(int logId);

        DbConfigDto GetConfig();

        void SaveConfig(DbConfigDto config);
    }
}
