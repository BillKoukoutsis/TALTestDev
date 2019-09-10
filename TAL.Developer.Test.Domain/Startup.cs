using System;
using System.Reflection;
using System.Web.Http;
using Microsoft.Owin;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;

[assembly: OwinStartup(typeof(TAL.Developer.Test.Domain.Startup))]
namespace TAL.Developer.Test.Domain
{
    public class Startup
    {
        private readonly Lazy<IKernel> _kernel = new Lazy<IKernel>(() =>
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            NinjectConfig.RegisterServices(kernel);

            return kernel;
        });

        public void Configuration(IAppBuilder app)
        {
            app.UseNinjectMiddleware(() => _kernel.Value);

            HttpConfiguration webApiConfig = new HttpConfiguration();

            WebApiConfig.Register(webApiConfig);
            app.UseWebApi(webApiConfig);

            //Ninject            
            app.UseNinjectWebApi(webApiConfig);

            //AutoMapper
            if (!AutoMapperConfig.IsInitialized)
                AutoMapperConfig.Initialize();

            //Database path
            string codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            System.UriBuilder uri = new System.UriBuilder(codeBase);
            string path = System.Uri.UnescapeDataString(uri.Path);
            var parent = System.IO.Directory.GetParent(path);
            while (parent.Name != "TALDevTest")
            {
                parent = System.IO.Directory.GetParent(parent.FullName);
            }
            System.AppDomain.CurrentDomain.SetData("DataDirectory", parent.FullName);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            var issuer = ServiceSettings.OAuthIssuer;
            var secret = ServiceSettings.OAuthSecret;
        }

    }
}