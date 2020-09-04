using Autofac;
using Autofac.Integration.Mvc;
using ECommerce.Web.Managers;
using ECommerce.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Web.App_Start
{
    public static class AutofacConfig
    {
        public static void RegisterComponents()
        {

            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterAssemblyTypes(typeof(CustomerManager).Assembly)
            .Where(t => t.Name.EndsWith("Manager"))
            .AsImplementedInterfaces().InstancePerHttpRequest();

            builder.RegisterType(typeof(EComEntities)).As(typeof(DbContext)).InstancePerLifetimeScope();
            
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}