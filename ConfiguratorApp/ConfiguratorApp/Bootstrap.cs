using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using ConfiguratorApp.Services;
using ConfiguratorApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConfiguratorApp
{
    public class Bootstrap
    {
        public Bootstrap()
        {
        }

        public static void Initialize()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<BasePageViewModel>().AsSelf();
            builder.RegisterType<MainPageViewModel>().AsSelf();
            builder.RegisterType<SpreadsheetViewViewModel>().AsSelf();
            builder.RegisterType<ProductService>().As<IProductService>();
            Autofac.IContainer container = builder.Build();
            AutofacServiceLocator asl = new AutofacServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => asl);
        }
    }
}
