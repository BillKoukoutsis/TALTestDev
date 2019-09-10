using TAL.Developer.Test.Domain.Logging;
using TAL.Developer.Test.Domain.Repositories;
using TAL.Developer.Test.Domain.Services;
using Ninject;

namespace TAL.Developer.Test.Domain
{
    public class NinjectConfig
    {
        public static void RegisterServices(KernelBase kernel)
        {
            // Services
            kernel.Bind<IGroupsService>().To<GroupsService>();
            kernel.Bind<ITimezonesService>().To<TimezonesService>();
            kernel.Bind<IEmployeesService>().To<EmployeesService>();
            kernel.Bind<ITimesheetsService>().To<TimesheetsService>();

            // Repositories
            kernel.Bind<IGroupsRepository>().To<GroupsRepository>();
            kernel.Bind<ITimezonesRepository>().To<TimezonesRepository>();
            kernel.Bind<IEmployeesRepository>().To<EmployeesRepository>();
            kernel.Bind<ITimesheetsRepository>().To<TimesheetsRepository>();

            // Libraries
            kernel.Bind<ILogger>().To<NLogger>();
        }
    }
}