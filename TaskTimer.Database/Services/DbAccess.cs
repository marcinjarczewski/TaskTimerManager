using AutoMapper;
using TaskTimer.Contracts.Db;
using TaskTimer.Database.DbModels;
using SQLite;
using System.Collections.Generic;

namespace TaskTimer.Database.Services
{
    public class DbAccess : IDbAccess
    {
        private IMapper _mapper;

        public DbAccess(IMapper mapper)
        {
            _mapper = mapper;
            Init();
        }

        static object locker = new object();
        private SQLiteConnection GetConnection()
        {
            return new SQLiteConnection("taskTimerDB");
        }

        /// <summary>
        /// Create tables and init config
        /// </summary>
        public void Init()
        {
            var database = GetConnection();
            database.CreateTable<DbLogModel>();
            database.CreateTable<DbConfigModel>();
            database.CreateTable<DbClientModel>();
            database.CreateTable<DbTaskModel>();
            if (database.Table<DbConfigModel>().FirstOrDefault(u => u.Id == 1) == null)
            {
                database.Insert(new DbConfigModel
                {
                    AutoSave = false,
                    Id = 1
                });
            }        
            database.Commit();
        }

        public void Drop()
        {
            var database = GetConnection();
            database.DropTable<DbLogModel>();
            database.DropTable<DbConfigModel>();
            database.DropTable<DbClientModel>();
            database.Commit();
        }

        public void AddLog(DbLogDto log)
        {
            var logModel = _mapper.Map<DbLogModel>(log);
            var database = GetConnection();
            lock (locker)
            {
                database.Insert(logModel);
                database.Commit();
            }
        }

        public DbLogDto GetLog(int logId)
        {
            var database = GetConnection();
            var model = database.Table<DbLogModel>().FirstOrDefault(f => f.Id == logId);
            return _mapper.Map<DbLogDto>(model);
        }

        public List<DbClientDto> GetClients()
        {
            var database = GetConnection();
            var models = database.Table<DbClientModel>().ToList();
            return _mapper.Map<List<DbClientDto>>(models);
        }

        public List<DbTaskDto> GetActiveTasks()
        {
            var database = GetConnection();
            lock (locker)
            {
                var models = database.Table<DbTaskModel>().Where(t => t.IsActive && !t.IsEnded).ToList();
                return _mapper.Map<List<DbTaskDto>>(models);
            }
        }

        public List<DbTaskDto> GetHistoryTasks()
        {
            var database = GetConnection();
            lock (locker)
            {
                var models = database.Table<DbTaskModel>().Where(t => t.IsActive && t.IsEnded).ToList();
                return _mapper.Map<List<DbTaskDto>>(models);
            }
        }


        /// <summary>
        /// Get single record from db. Only 1 record is used by config.
        /// </summary>
        /// <returns>Settings</returns>
        public DbConfigDto GetConfig()
        {
            var database = GetConnection();
            var model = database.Table<DbConfigModel>().FirstOrDefault();
            var temp = database.Table<DbConfigModel>().ToList();
            return _mapper.Map<DbConfigDto>(model);
        }

        public void SaveConfig(DbConfigDto config)
        {
            var database = GetConnection();
            var model = database.Table<DbConfigModel>().FirstOrDefault() ?? new DbConfigModel();
            model.CopyDataToInvoice = config.CopyDataToInvoice;
            model.DisableInvoices = config.DisableInvoices;
            model.RoundReportedTime = config.RoundReportedTime;
            model.LanguageCode = config.LanguageCode;
            //set values here        
            database.RunInTransaction(() =>
            {
                database.Update(model);
            });
            database.Commit();
        }

        public void AddClient(DbClientDto client)
        {
            var clientModel = _mapper.Map<DbClientModel>(client);
            var database = GetConnection();
            lock (locker)
            {
                database.Insert(clientModel);
                database.Commit();
            }
        }

        public int AddTask(DbTaskDto task)
        {
            var taskModel = _mapper.Map<DbTaskModel>(task);
            var database = GetConnection();
            lock (locker)
            {
                database.Insert(taskModel);
                database.Commit();
                return taskModel.Id;
            }
        }

        public void EditTask(DbTaskDto task)
        {
            var database = GetConnection();
            lock (locker)
            {
                var mappedTask = _mapper.Map<DbTaskModel>(task);
                var dbTask = database.Table<DbTaskModel>().FirstOrDefault(u => u.Id == task.Id);
                dbTask.Subject = mappedTask.Subject;
                dbTask.IsEnded = mappedTask.IsEnded;
                dbTask.EndDate = mappedTask.EndDate;
                dbTask.IsPaused = mappedTask.IsPaused;
                dbTask.Description = mappedTask.Description;
                dbTask.InvoiceDescription = mappedTask.InvoiceDescription;
                dbTask.InvoiceSubject = mappedTask.InvoiceSubject;
                dbTask.TimeInSeconds = mappedTask.TimeInSeconds;
                dbTask.ReportedTimeInSeconds = mappedTask.ReportedTimeInSeconds;
                dbTask.InvoiceReportedTimeInSeconds = mappedTask.InvoiceReportedTimeInSeconds;
                dbTask.IsActive = mappedTask.IsActive;
                database.RunInTransaction(() =>
                {
                    database.Update(dbTask);
                });
                database.Commit();
            }
        }

        public void EditClient(DbClientDto client)
        {
            var database = GetConnection();
            lock (locker)
            {
                var dbClient = database.Table<DbClientModel>().FirstOrDefault(u => u.Id == client.Id);
                dbClient.Name = client.Name;
                dbClient.SearchName = client.SearchName;
                dbClient.Description = client.Description;
                dbClient.Priority = client.Priority;
                dbClient.IsActive = client.IsActive;
                database.Update(dbClient);
                database.Commit();
            }
        }
    }
}
