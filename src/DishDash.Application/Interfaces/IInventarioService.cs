using DishDash.Application.DTOs.InventarioDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.Interfaces
{
    public interface IInventarioService
    {
        Task<IEnumerable<ProductoResponseDto>> ObtenerTodosAsync();
        Task<IEnumerable<ProductoResponseDto>> ObtenerConAlertasAsync();
        Task<ProductoResponseDto> ObtenerPorIdAsync(int id);
        Task<ProductoResponseDto> CrearAsync(CrearProductoDto dto);
        Task<ProductoResponseDto> ActualizarAsync(int id, ActualizarProductoDto dto);
        Task<MovimientoResponseDto> RegistrarMovimientoAsync(int productoId, MovimientoRequestDto dto, int usuarioId);
        Task<IEnumerable<CategoriaResponseDto>> ObtenerCategoriasAsync();
        Task<IEnumerable<UnidadMedidaResponseDto>> ObtenerUnidadesAsync();
    }
}
