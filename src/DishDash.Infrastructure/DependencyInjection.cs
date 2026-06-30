using DishDash.Application.Interfaces;
using DishDash.Application.Services;
using DishDash.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

using System.Collections.Generic;
using System.Text;

namespace DishDash.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IReservaService, ReservaService>();
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IMesaService, MesaService>();
            services.AddScoped<IInventarioService, InventarioService>();

            return services;
        }
    }
}
