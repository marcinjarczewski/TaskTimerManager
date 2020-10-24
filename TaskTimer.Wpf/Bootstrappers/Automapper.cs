using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TaskTimer.Contracts;
using TaskTimer.Wpf.ViewModels;
using TaskTimer.Contracts.Db;
using TaskTimer.Database.DbModels;
using System;
using System.Globalization;
using TaskTimer.Wpf.Models;

namespace TaskTimer.Wpf.Bootstrappers
{
    public static class Automapper
    {
        public static IMapper Init()
        {
            var _configuraton = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<string, DateTime>().ConvertUsing(s => DateTime.ParseExact(s,"dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture));
                cfg.CreateMap<string, DateTime?>().ConvertUsing(s =>string.IsNullOrEmpty(s) ? default(DateTime?) : DateTime.ParseExact(s, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture));

                cfg.CreateMap<DateTime, string>().ConstructUsing(s => s.ToString("dd-MM-yyyy HH:mm"));
                cfg.CreateMap<DateTime?, string>().ConstructUsing(s =>s.HasValue ? s.Value.ToString("dd-MM-yyyy HH:mm") : "");

                cfg.CreateMap<DbConfigModel, DbConfigDto>();
                cfg.CreateMap<DbConfigDto, DbConfigModel>();

                cfg.CreateMap<DbLogModel, DbLogDto>();
                cfg.CreateMap<DbLogDto, DbLogModel>();

                cfg.CreateMap<DbClientModel, DbClientDto>();
                cfg.CreateMap<DbClientDto, DbClientModel>();
                cfg.CreateMap<DbClientDto, ClientModel>();
                cfg.CreateMap<ClientModel, DbClientDto>();

                cfg.CreateMap<DbTaskModel, DbTaskDto>();
                cfg.CreateMap<DbTaskDto, DbTaskModel>();
                cfg.CreateMap<DbTaskDto, TaskModel>();
                cfg.CreateMap<TaskModel, DbTaskDto>();
            });

            return _configuraton.CreateMapper();
        }
    }
}