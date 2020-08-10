using System;
using Application;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Interfaces;
using Domain.Entities.Auth;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.Service.Merchant.Transactions;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

namespace IOC
{
    public class AutofacDependencyContainer
    {
        public static IServiceProvider RegisterServices(IServiceCollection services)
        {
            // Now register our services with Autofac container.
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ApplicationModule());
            builder.RegisterModule(new InfrastructureModule());
            builder.RegisterModule(new InfrastructureModule());
            builder.RegisterType<ITransactionRepositories>().AsSelf().As<MerchantContext>().InstancePerLifetimeScope();

            builder.Populate(services);
            var container = builder.Build();
            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(container);

        }
    }
}