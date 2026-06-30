using DishDash.Domain.Entities;
using DishDash.Domain.Interfaces;
using DishDash.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Persistence.Repositories
{
    public class ProductoRepository(DishDashDbContext ctx) : IProductoRepository
    {
        private IQueryable<Producto> Query() =>
            ctx.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Unidad);

        public async Task<Producto?> ObtenerPorIdAsync(int id)
            => await Query().FirstOrDefaultAsync(p => p.ProductoId == id);

        public async Task<IEnumerable<Producto>> ObtenerTodosAsync()
            => await Query().Where(p => p.Activo).OrderBy(p => p.Nombre).ToListAsync();

        public async Task<IEnumerable<Producto>> ObtenerConAlertasAsync()
            => await Query().Where(p => p.Activo && p.StockActual <= p.StockMinimo).ToListAsync();

        public async Task<Producto> CrearAsync(Producto producto)
        {
            ctx.Productos.Add(producto);
            await ctx.SaveChangesAsync();
            return await ObtenerPorIdAsync(producto.ProductoId) ?? producto;
        }

        public async Task<Producto> ActualizarAsync(Producto producto)
        {
            ctx.Productos.Update(producto);
            await ctx.SaveChangesAsync();
            return await ObtenerPorIdAsync(producto.ProductoId) ?? producto;
        }

        public async Task RegistrarMovimientoAsync(MovimientoInventario movimiento)
        {
            var tipoMov = await ctx.TiposMovimiento.FindAsync(movimiento.TipoMovimientoId)
                ?? throw new InvalidOperationException("Tipo de movimiento no encontrado.");

            var producto = await ctx.Productos.FindAsync(movimiento.ProductoId)
                ?? throw new InvalidOperationException("Producto no encontrado.");

            movimiento.StockAnterior = producto.StockActual;
            producto.StockActual = tipoMov.EsEntrada
                ? producto.StockActual + movimiento.Cantidad
                : producto.StockActual - movimiento.Cantidad;

            if (producto.StockActual < 0)
                throw new InvalidOperationException("Stock insuficiente para registrar la salida.");

            movimiento.StockResultante = producto.StockActual;
            producto.ActualizadoEn = DateTime.UtcNow;

            ctx.MovimientosInventario.Add(movimiento);

            // Generar alerta si corresponde
            if (producto.StockActual <= producto.StockMinimo)
            {
                var tipoAlerta = producto.StockActual == 0 ? "StockAgotado"
                    : producto.StockActual <= producto.StockMinimo * 0.5m ? "StockCritico"
                    : "StockBajo";

                ctx.AlertasStock.Add(new AlertaStock
                {
                    ProductoId = producto.ProductoId,
                    TipoAlerta = tipoAlerta,
                    StockAlMomento = producto.StockActual
                });
            }

            await ctx.SaveChangesAsync();
            movimiento.TipoMovimiento = tipoMov;
        }
    }
}
