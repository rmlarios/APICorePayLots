using Dapper.Application.Interfaces;
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
            
            //services.AddTransient<IBeneficiarioRepository,BeneficiarioRepository>();
            
            services.AddScoped(typeof(IGenericDapperRepository<>),typeof(GenericDapperRepository<>)); 
            services.AddScoped<IBeneficiarioRepository,BeneficiarioRepository>();
            services.AddScoped<IAsignacionesRepository,AsignacionesRepository>();
            services.AddTransient<IUbicacionRepository,UbicacionRepository>();
            services.AddTransient<IBloqueRepository,BloqueRepository>();
            services.AddTransient<ILoteRepository,LoteRepository>();
            services.AddTransient<IMoraRepository,MoraRepository>();
            services.AddTransient<IProformaRepository,ProformaRepository>();
            services.AddTransient<IPagoRepository,PagoRepository>();
        }
    }
}
