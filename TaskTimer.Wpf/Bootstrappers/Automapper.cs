using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TaskTimer.Contracts;
using TaskTimer.Wpf.ViewModels;
using TaskTimer.Contracts.Db;
using TaskTimer.Database.DbModels;

namespace TaskTimer.Wpf.Bootstrappers
{
    public static class Automapper
    {
        public static IMapper Init()
        {
            var _configuraton = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DbConfigModel, DbConfigDto>();
                cfg.CreateMap<DbConfigDto, DbConfigModel>();

                cfg.CreateMap<DbLogModel, DbLogDto>();
                cfg.CreateMap<DbLogDto, DbLogModel>();
            });

            return _configuraton.CreateMapper();
        }
    }
}