using DishDash.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DishDash.Persistence.Context
{
    public class DishDashDbContext(DbContextOptions<DishDashDbContext> options) : DbContext(options)
    {
        // Seguridad
        public DbSet<Rol> Roles => Set<Rol>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<TokenRevocado> TokensRevocados => Set<TokenRevocado>();

        // Reservas
        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<Mesa> Mesas => Set<Mesa>();
        public DbSet<Reserva> Reservas => Set<Reserva>();
        public DbSet<HistorialReserva> HistorialReservas => Set<HistorialReserva>();

        // Inventario
        public DbSet<Categoria> Categorias => Set<Categoria>();
        public DbSet<UnidadMedida> UnidadesMedida => Set<UnidadMedida>();
        public DbSet<Producto> Productos => Set<Producto>();
        public DbSet<TipoMovimiento> TiposMovimiento => Set<TipoMovimiento>();
        public DbSet<MovimientoInventario> MovimientosInventario => Set<MovimientoInventario>();
        public DbSet<AlertaStock> AlertasStock => Set<AlertaStock>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplica todas las configuraciones del assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DishDashDbContext).Assembly);

            // Schemas
            modelBuilder.Entity<Rol>().ToTable("Roles", "Seguridad");
            modelBuilder.Entity<Usuario>().ToTable("Usuarios", "Seguridad");
            modelBuilder.Entity<TokenRevocado>().ToTable("TokensRevocados", "Seguridad");

            modelBuilder.Entity<Cliente>().ToTable("Clientes", "Reservas");
            modelBuilder.Entity<Mesa>().ToTable("Mesas", "Reservas");
            modelBuilder.Entity<Reserva>().ToTable("Reservas", "Reservas");
            modelBuilder.Entity<HistorialReserva>().ToTable("HistorialReservas", "Reservas");

            modelBuilder.Entity<Categoria>().ToTable("Categorias", "Inventario");
            modelBuilder.Entity<UnidadMedida>().ToTable("UnidadesMedida", "Inventario");
            modelBuilder.Entity<Producto>().ToTable("Productos", "Inventario");
            modelBuilder.Entity<TipoMovimiento>().ToTable("TiposMovimiento", "Inventario");
            modelBuilder.Entity<MovimientoInventario>().ToTable("Movimientos", "Inventario");
            modelBuilder.Entity<AlertaStock>().ToTable("AlertasStock", "Inventario");

            ConfigurarSeguridad(modelBuilder);
            ConfigurarReservas(modelBuilder);
            ConfigurarInventario(modelBuilder);
        }

        private static void ConfigurarSeguridad(ModelBuilder mb)
        {
            mb.Entity<Rol>(e =>
            {
                e.HasKey(r => r.RolId);
                e.Property(r => r.Nombre).HasMaxLength(50).IsRequired();
                e.HasIndex(r => r.Nombre).IsUnique();
            });

            mb.Entity<Usuario>(e =>
            {
                e.HasKey(u => u.UsuarioId);
                e.Property(u => u.Nombre).HasMaxLength(100).IsRequired();
                e.Property(u => u.Apellido).HasMaxLength(100).IsRequired();
                e.Property(u => u.Email).HasMaxLength(150).IsRequired();
                e.Property(u => u.PasswordHash).HasMaxLength(500).IsRequired();
                e.HasIndex(u => u.Email).IsUnique();
                e.HasOne(u => u.Rol).WithMany(r => r.Usuarios).HasForeignKey(u => u.RolId);
            });

            mb.Entity<TokenRevocado>(e =>
            {
                e.HasKey(t => t.TokenId);
                e.Property(t => t.Jti).HasMaxLength(200).IsRequired();
                e.HasIndex(t => t.Jti).IsUnique();
                e.HasOne(t => t.Usuario).WithMany(u => u.TokensRevocados).HasForeignKey(t => t.UsuarioId);
            });
        }

        private static void ConfigurarReservas(ModelBuilder mb)
        {
            mb.Entity<Cliente>(e =>
            {
                e.HasKey(c => c.ClienteId);
                e.Property(c => c.Nombre).HasMaxLength(100).IsRequired();
                e.Property(c => c.Apellido).HasMaxLength(100).IsRequired();
                e.Property(c => c.Email).HasMaxLength(150);
                e.Property(c => c.Telefono).HasMaxLength(20);
            });

            mb.Entity<Mesa>(e =>
            {
                e.HasKey(m => m.MesaId);
                e.HasIndex(m => m.Numero).IsUnique();
                e.Property(m => m.Estado).HasMaxLength(20).HasDefaultValue("Disponible");
                e.Property(m => m.Ubicacion).HasMaxLength(100);
            });

            mb.Entity<Reserva>(e =>
            {
                e.HasKey(r => r.ReservaId);
                e.Property(r => r.Estado).HasMaxLength(20).HasDefaultValue("Pendiente");
                e.Property(r => r.Notas).HasMaxLength(500);
                e.HasOne(r => r.Cliente).WithMany(c => c.Reservas).HasForeignKey(r => r.ClienteId);
                e.HasOne(r => r.Mesa).WithMany(m => m.Reservas).HasForeignKey(r => r.MesaId);
                e.HasOne(r => r.UsuarioCreadoPor).WithMany().HasForeignKey(r => r.UsuarioCreadoPorId);
                e.HasIndex(r => r.FechaReserva);
                e.HasIndex(r => r.Estado);
            });

            mb.Entity<HistorialReserva>(e =>
            {
                e.HasKey(h => h.HistorialId);
                e.Property(h => h.EstadoAnterior).HasMaxLength(20).IsRequired();
                e.Property(h => h.EstadoNuevo).HasMaxLength(20).IsRequired();
                e.Property(h => h.Observacion).HasMaxLength(300);
                e.HasOne(h => h.Reserva).WithMany(r => r.Historial).HasForeignKey(h => h.ReservaId);
                e.HasOne(h => h.Usuario).WithMany().HasForeignKey(h => h.UsuarioId);
            });
        }

        private static void ConfigurarInventario(ModelBuilder mb)
        {
            mb.Entity<Categoria>(e =>
            {
                e.HasKey(c => c.CategoriaId);
                e.Property(c => c.Nombre).HasMaxLength(100).IsRequired();
                e.HasIndex(c => c.Nombre).IsUnique();
            });

            mb.Entity<UnidadMedida>(e =>
            {
                e.HasKey(u => u.UnidadId);
                e.Property(u => u.Nombre).HasMaxLength(50).IsRequired();
                e.Property(u => u.Abreviatura).HasMaxLength(10).IsRequired();
            });

            mb.Entity<Producto>(e =>
            {
                e.HasKey(p => p.ProductoId);
                e.Property(p => p.Nombre).HasMaxLength(150).IsRequired();
                e.Property(p => p.StockActual).HasPrecision(10, 3);
                e.Property(p => p.StockMinimo).HasPrecision(10, 3);
                e.Property(p => p.StockMaximo).HasPrecision(10, 3);
                e.Property(p => p.PrecioUnitario).HasPrecision(10, 2);
                e.HasOne(p => p.Categoria).WithMany(c => c.Productos).HasForeignKey(p => p.CategoriaId);
                e.HasOne(p => p.Unidad).WithMany(u => u.Productos).HasForeignKey(p => p.UnidadId);
            });

            mb.Entity<TipoMovimiento>(e =>
            {
                e.HasKey(t => t.TipoMovimientoId);
                e.Property(t => t.Nombre).HasMaxLength(50).IsRequired();
            });

            mb.Entity<MovimientoInventario>(e =>
            {
                e.HasKey(m => m.MovimientoId);
                e.Property(m => m.Cantidad).HasPrecision(10, 3);
                e.Property(m => m.StockAnterior).HasPrecision(10, 3);
                e.Property(m => m.StockResultante).HasPrecision(10, 3);
                e.Property(m => m.Motivo).HasMaxLength(300);
                e.HasOne(m => m.Producto).WithMany(p => p.Movimientos).HasForeignKey(m => m.ProductoId);
                e.HasOne(m => m.TipoMovimiento).WithMany(t => t.Movimientos).HasForeignKey(m => m.TipoMovimientoId);
                e.HasOne(m => m.Usuario).WithMany().HasForeignKey(m => m.UsuarioId);
            });

            mb.Entity<AlertaStock>(e =>
            {
                e.HasKey(a => a.AlertaId);
                e.Property(a => a.TipoAlerta).HasMaxLength(50).HasDefaultValue("StockBajo");
                e.Property(a => a.StockAlMomento).HasPrecision(10, 3);
                e.HasOne(a => a.Producto).WithMany(p => p.Alertas).HasForeignKey(a => a.ProductoId);
            });
        }
    }
}
