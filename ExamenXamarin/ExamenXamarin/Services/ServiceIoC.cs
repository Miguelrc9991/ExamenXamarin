using Autofac;
using ExamenXamarin.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace ExamenXamarin.Services
{
    public class ServiceIoC
    {
        private IContainer container;

        public ServiceIoC()
        {
            this.RegisterDependencies();
        }
        private void RegisterDependencies()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<ServiceApiSeries>();
            builder.RegisterType<ModificarPersonajeViewModel>();

            builder.RegisterType<SeriesListViewModel>();
            builder.RegisterType<SerieViewModel>();


            string resourceName = "ExamenXamarin.appsettings.json";
            Stream stream =
            GetType().GetTypeInfo().Assembly
            .GetManifestResourceStream(resourceName);
            IConfiguration configuration =
            new ConfigurationBuilder().AddJsonStream(stream)
            .Build();
            builder.Register<IConfiguration>(z => configuration);
            this.container = builder.Build();
        }
        public ModificarPersonajeViewModel ModificarPersonajeViewModel
        {
            get
            {
                return this.container.Resolve<ModificarPersonajeViewModel>();
            }
        }
        public SeriesListViewModel SeriesListViewModel
        {
            get
            {
                return this.container.Resolve<SeriesListViewModel>();
            }
        }
        public SerieViewModel SerieViewModel
        {
            get
            {
                return this.container.Resolve<SerieViewModel>();
            }
        }
    }

}
