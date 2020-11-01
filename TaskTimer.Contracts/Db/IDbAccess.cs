using System.Collections.Generic;

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

        List<DbClientDto> GetClients();

        void AddClient(DbClientDto client);

        void EditClient(DbClientDto client);

        List<DbTaskDto> GetActiveTasks();

        void EditTask(DbTaskDto task);

        int AddTask(DbTaskDto task);

        List<DbTaskDto> GetHistoryTasks();
    }
}
