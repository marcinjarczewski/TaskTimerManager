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

        /// <summary>
        /// Get single record from db. Only 1 record is used by config.
        /// </summary>
        /// <returns>Settings</returns>
        public DbConfigDto GetConfig()
        {
            var database = GetConnection();
            var model = database.Table<DbConfigModel>().FirstOrDefault();
            return _mapper.Map<DbConfigDto>(model);
        }

        public void SaveConfig(DbConfigDto config)
        {
            var database = GetConnection();
            var model = database.Table<DbConfigModel>().FirstOrDefault() ?? new DbConfigModel();
            //set values here        
            database.Update(model);
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

        public void EditClient(DbClientDto client)
        {
            var database = GetConnection();
            lock (locker)
            {
                var dbClient = database.Table<DbClientModel>().FirstOrDefault(u => u.Id == client.Id);
                dbClient.Name = client.Name;
                dbClient.SearchName = client.SearchName;
                dbClient.Priority = client.Priority;
                database.Update(dbClient);
                database.Commit();
            }
        }
    }
}
