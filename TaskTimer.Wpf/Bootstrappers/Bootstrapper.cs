using Caliburn.Micro;
using Castle.Windsor;
using TaskTimer.Wpf.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace TaskTimer.Wpf.Bootstrappers
{
    public class Bootstrapper : BootstrapperBase
    {
        private readonly IWindsorContainer _container = new WindsorContainer();

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            Automapper.Init();
            var loader = _container.Resolve<ModuleLoader>();

            var exeDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var pattern = "*.dll";

            Directory
                .GetFiles(exeDir, pattern)
                .Select(Assembly.LoadFrom)
                .Select(loader.LoadModule)
                .Distinct()
                .Where(module => module != null).ToList()
                .ForEach(module => module.Init());

            DisplayRootViewFor<ShellViewModel>();
        }

        protected override void Configure()
        {
            _container.Install(new ShellInstaller());
        }

        protected override object GetInstance(Type service, string key)
        {
            return string.IsNullOrWhiteSpace(key)

                ? _container.Kernel.HasComponent(service)
                    ? _container.Resolve(service)
                    : base.GetInstance(service, key)

                : _container.Kernel.HasComponent(key)
                    ? _container.Resolve(key, service)
                    : base.GetInstance(service, key);
        }
    }
}
