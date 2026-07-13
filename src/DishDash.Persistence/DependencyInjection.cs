using DishDash.Domain.Interfaces;
using DishDash.Persistence.Context;
using DishDash.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DishDash.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddDbContext<DishDashDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    sql => sql.MigrationsAssembly(typeof(DishDashDbContext).Assembly.FullName)
                ));

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IRolRepository, RolRepository>();
            services.AddScoped<ITokenRevocadoRepository, TokenRevocadoRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IMesaRepository, MesaRepository>();
            services.AddScoped<IReservaRepository, ReservaRepository>();
            services.AddScoped<IProductoRepository, ProductoRepository>();
            services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();
            services.AddScoped<IPuestoRepository, PuestoRepository>();
            services.AddScoped<ITurnoRepository, TurnoRepository>();
            services.AddScoped<IAsistenciaRepository, AsistenciaRepository>();

            return services;
        }
    }
}
