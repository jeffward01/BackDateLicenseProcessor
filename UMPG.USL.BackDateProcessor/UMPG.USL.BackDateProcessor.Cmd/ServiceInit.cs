using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using NLog.Internal;
using Topshelf;
using Topshelf.Autofac;
using UMPG.USL.BackDateProcessor.Business;
using UMPG.USL.BackDateProcessor.Business.Logging;
using UMPG.USL.BackDateProcessor.Business.Managers;
using UMPG.USL.BackDateProcessor.Business.Services;
using UMPG.USL.BackDateProcessor.Cmd.Application;

namespace UMPG.USL.BackDateProcessor.Cmd
{
    internal class ServiceInit
    {
        private static void Main(string[] args)
        {

            // Create your container
            var dataAccess = Assembly.GetExecutingAssembly();
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(dataAccess)
                       .Where(t => t.Name.EndsWith("Service"))
                       .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(dataAccess)
                   .Where(t => t.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(dataAccess)
                   .Where(t => t.Name.EndsWith("Provider"))
                   .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(dataAccess)
                .Where(t => t.Name.EndsWith("Manager"))
                .AsImplementedInterfaces();

            builder.RegisterType<TimeSpanUtil>().As<ITimeSpanUtil>();
            builder.RegisterType<ServiceManager>().As<IServiceManager>();
            builder.RegisterType<ConfigurationManager>().As<IConfigurationManager>();
            builder.RegisterType<DataHarmonizationLogManager>().As<IDataHarmonizationLogManager>();
            builder.RegisterType<ServiceManager>().As<IServiceManager>();
            builder.RegisterType<SnapshotLicenseManager>().As<ISnapshotLicenseManager>();
            builder.RegisterType<SnapshotManager>().As<ISnapshotManager>();
            builder.RegisterType<SnapshotLicenseProductManager>().As<ISnapshotLicenseProductManager>();
            builder.RegisterType<DataHarmonizationManager>().As<IDataHarmonizationManager>();
            var container = builder.Build();




            HostFactory.Run(dataHarmonizationService =>
            {
                dataHarmonizationService.SetServiceName("LicenseSnapshotBackDateProcessor");
                dataHarmonizationService.SetDisplayName("LicenseSnapshotBackDateProcessor");

                dataHarmonizationService.Service<LicenseSnapshotBackDateService>();
                dataHarmonizationService.UseAutofacContainer(container);
                dataHarmonizationService.StartAutomatically();
                dataHarmonizationService.RunAsLocalSystem();
                dataHarmonizationService.EnableShutdown();
                dataHarmonizationService.EnableServiceRecovery(recovery =>
                    recovery.RestartService(1));
                dataHarmonizationService.UseNLog();
                dataHarmonizationService.DependsOnEventLog();
            });
        }
    }
}
