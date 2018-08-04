using System;
using System.Collections.Generic;
using System.Text;
using DDDExample.Cqrs.Command.Customer;
using DDDExample.Data.Context;
using DDDExample.Data.EventSourcing;
using DDDExample.Data.Hanlders.Customer;
using DDDExample.Domain.Core;
using DDDExample.Domain.Core.DomainNotifications;
using DDDExample.Domain.Event;
using DDDExample.Identity.Authorization;
using DDDExample.Identity.Data;
using DDDExample.Identity.Models;
using DDDExample.Identity.Services;
using DDDExample.SharedKernel.AutoMapper;
using DDDExample.SharedKernel.Cqrs;
using DDDExample.SharedKernel.Cqrs.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DDDExample.IoC
{
    public class DddExampleBootstrapper
    {
        public static void Register(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // ASP.NET Authorization Polices
            services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();

            services.AddTransient<IEmailSender, AuthEmailMessageSender>();
            services.AddTransient<ISmsSender, AuthSMSMessageSender>();


            services.AddScoped<DddExampleContext>();
            services.AddScoped<EventSourcingContext>();

            services.AddCqrs<CustomerQueryHandler>();

            services.AddScoped<IProcessor, Processor>();

            AutoMapperConfig.Register<CreateCustomer>();

            //Cqrs
            services.AddCqrs<CustomerQueryHandler>();

            // Infra - Identity
            services.AddScoped<IUser, AspNetUser>();

            services.AddScoped<IDomainNotification, DomainNotificationHandler>();
            services.AddScoped<IEventRepository, EventRespoitory>();
        }
    }
}
