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
            kernel.Bind<IOccupationRatingsService>().To<OccupationRatingsService>();
            kernel.Bind<IOccupationsService>().To<OccupationsService>();
            kernel.Bind<IMembersService>().To<MembersService>();

            // Repositories
            kernel.Bind<IOccupationRatingsRepository>().To<OccupationRatingsRepository>();
            kernel.Bind<IOccupationsRepository>().To<OccupationsRepository>();
            kernel.Bind<IMembersRepository>().To<MembersRepository>();

            // Libraries
            kernel.Bind<ILogger>().To<NLogger>();
        }
    }
}