﻿using Dapper.Application.Interfaces;
using Dapper.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Dapper.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            //DBContext
            services.AddDbContext<PayLotsDBContext>(options => options.UseSqlServer(configuration.GetConnectionString("PayLotsConnectionString")));
            services.AddTransient<IProductRepository, ProductRepository>();
            //services.AddTransient<IBeneficiarioRepository,BeneficiarioRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBeneficiarioRepository,BeneficiarioRepository>();
            services.AddScoped<IAsignacionesRepository,AsignacionesRepository>();
        }
    }
}
