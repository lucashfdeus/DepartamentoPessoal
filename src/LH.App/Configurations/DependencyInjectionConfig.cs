using LH.App.Data;
using LH.App.Extensions;
using LH.Business.Interfaces;
using LH.Business.Interfaces.Repository;
using LH.Business.Interfaces.Services;
using LH.Business.Notificacoes;
using LH.Business.Services;
using LH.Date.Repository;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;

namespace LH.App.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependecies(this IServiceCollection services)
        {
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<IRegistroPontoRepository, RegistroPontoRepository>();
            services.AddScoped<IDepartamentoRepository, DepartamentoRepository>();
            services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();

            services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();

            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IRegistroPontoService, RegistroPontoService>();
            services.AddScoped<IDepartamentoService, DepartamentoService>();
            services.AddScoped<IFuncionarioService, FuncionarioService>();

            return services;
        }
    }
}
