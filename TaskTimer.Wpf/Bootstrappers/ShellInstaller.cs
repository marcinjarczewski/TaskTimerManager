﻿using AutoMapper;
using Caliburn.Micro;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TaskTimer.Contracts;
using TaskTimer.Contracts.Bootstrappers;
using TaskTimer.Contracts.Db;
using TaskTimer.Database.Services;
using TaskTimer.Wpf.Modules;
using TaskTimer.Wpf.ViewModels;

namespace TaskTimer.Wpf.Bootstrappers
{
    public class ShellInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var mapper = Automapper.Init();

            container
                .Register(Component.For<IWindsorContainer>().Instance(container))
                .Register(Component.For<ModuleLoader>())
                .Register(Component.For<MainViewModel>())
                .Register(Component.For<DialogViewModel>())
                .Register(Component.For<SettingsViewModel>())
                .Register(Component.For<TaskItemViewModel>())
                .Register(Component.For<IModule>().ImplementedBy<BaseModule>())
                .Register(Component.For<IShell>().ImplementedBy<BaseShell>())
                .Register(Component.For<ShellViewModel>() /*.LifeStyle.Singleton*/)
                .Register(Component.For<HistoryViewModel>())
                .Register(Component.For<AboutViewModel>())

                .Register(Component.For<IMapper>().Instance(mapper))
                .Register(Component.For<INavigator>().ImplementedBy<Navigator>())
                .Register(Component.For<IDbAccess>().ImplementedBy<DbAccess>());
        }
    }
}
