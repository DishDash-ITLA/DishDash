using DishDash.Application.DTOs.InventarioDTO;
using DishDash.Application.Interfaces;
using DishDash.Domain.Entities;
using DishDash.Domain.Exceptions;
using DishDash.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.Services
{
    public class InventarioService(IProductoRepository productoRepo) : IInventarioService
    {
        public async Task<IEnumerable<ProductoResponseDto>> ObtenerTodosAsync()
        {
            var productos = await productoRepo.ObtenerTodosAsync();
            return productos.Select(MapToDto);
        }

        public async Task<IEnumerable<ProductoResponseDto>> ObtenerConAlertasAsync()
        {
            var productos = await productoRepo.ObtenerConAlertasAsync();
            return productos.Select(MapToDto);
        }

        public async Task<ProductoResponseDto> ObtenerPorIdAsync(int id)
        {
            var producto = await productoRepo.ObtenerPorIdAsync(id)
                ?? throw new NotFoundException("Producto", id);
            return MapToDto(producto);
        }

        public async Task<ProductoResponseDto> CrearAsync(CrearProductoDto dto)
        {
            var producto = new Producto
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                CategoriaId = dto.CategoriaId,
                UnidadId = dto.UnidadId,
                StockActual = 0,
                StockMinimo = dto.StockMinimo,
                StockMaximo = dto.StockMaximo,
                PrecioUnitario = dto.PrecioUnitario
            };
            var creado = await productoRepo.CrearAsync(producto);
            return MapToDto(creado);
        }

        public async Task<ProductoResponseDto> ActualizarAsync(int id, ActualizarProductoDto dto)
        {
            var producto = await productoRepo.ObtenerPorIdAsync(id)
                ?? throw new NotFoundException("Producto", id);

            producto.Nombre = dto.Nombre;
            producto.Descripcion = dto.Descripcion;
            producto.CategoriaId = dto.CategoriaId;
            producto.UnidadId = dto.UnidadId;
            producto.StockMinimo = dto.StockMinimo;
            producto.StockMaximo = dto.StockMaximo;
            producto.PrecioUnitario = dto.PrecioUnitario;
            producto.Activo = dto.Activo;
            producto.ActualizadoEn = DateTime.UtcNow;

            var actualizado = await productoRepo.ActualizarAsync(producto);
            return MapToDto(actualizado);
        }

        public async Task<MovimientoResponseDto> RegistrarMovimientoAsync(
            int productoId, MovimientoRequestDto dto, int usuarioId)
        {
            var producto = await productoRepo.ObtenerPorIdAsync(productoId)
                ?? throw new NotFoundException("Producto", productoId);

            var stockAnterior = producto.StockActual;

            // EsEntrada se resuelve en el repo/contexto; aquí calculamos optimistamente
            var movimiento = new MovimientoInventario
            {
                ProductoId = productoId,
                TipoMovimientoId = dto.TipoMovimientoId,
                UsuarioId = usuarioId,
                Cantidad = dto.Cantidad,
                StockAnterior = stockAnterior,
                StockResultante = stockAnterior, // el repositorio recalcula con EsEntrada
                Motivo = dto.Motivo
            };

            await productoRepo.RegistrarMovimientoAsync(movimiento);

            return new MovimientoResponseDto(
                movimiento.MovimientoId,
                movimiento.TipoMovimiento?.Nombre ?? string.Empty,
                movimiento.Cantidad,
                movimiento.StockAnterior,
                movimiento.StockResultante,
                movimiento.Motivo,
                movimiento.FechaMovimiento
            );
        }

        public async Task<IEnumerable<CategoriaResponseDto>> ObtenerCategoriasAsync()
            => (await productoRepo.ObtenerTodosAsync())
                .Select(p => p.Categoria)
                .DistinctBy(c => c.CategoriaId)
                .Select(c => new CategoriaResponseDto(c.CategoriaId, c.Nombre, c.Descripcion));

        public async Task<IEnumerable<UnidadMedidaResponseDto>> ObtenerUnidadesAsync()
            => (await productoRepo.ObtenerTodosAsync())
                .Select(p => p.Unidad)
                .DistinctBy(u => u.UnidadId)
                .Select(u => new UnidadMedidaResponseDto(u.UnidadId, u.Nombre, u.Abreviatura));

        private static string CalcularNivelAlerta(Producto p)
        {
            if (p.StockActual == 0) return "StockAgotado";
            if (p.StockActual <= p.StockMinimo * 0.5m) return "StockCritico";
            if (p.StockActual <= p.StockMinimo) return "StockBajo";
            return "OK";
        }

        private static ProductoResponseDto MapToDto(Producto p) => new(
            p.ProductoId,
            p.Nombre,
            p.Descripcion,
            p.Categoria?.Nombre ?? string.Empty,
            p.Unidad?.Nombre ?? string.Empty,
            p.Unidad?.Abreviatura ?? string.Empty,
            p.StockActual,
            p.StockMinimo,
            p.StockMaximo,
            p.PrecioUnitario,
            CalcularNivelAlerta(p),
            p.Activo
        );
    }
}
