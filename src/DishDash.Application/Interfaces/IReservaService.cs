using DishDash.Application.DTOs.ReservasDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.Interfaces
{
    public interface IReservaService
    {
        Task<IEnumerable<ReservaResponseDto>> ObtenerTodasAsync();
        Task<IEnumerable<ReservaResponseDto>> ObtenerPorFechaAsync(DateOnly fecha);
        Task<ReservaResponseDto> ObtenerPorIdAsync(int id);
        Task<ReservaResponseDto> CrearAsync(CrearReservaDto dto, int usuarioId);
        Task<ReservaResponseDto> ActualizarAsync(int id, ActualizarReservaDto dto);
        Task<ReservaResponseDto> CambiarEstadoAsync(int id, CambiarEstadoReservaDto dto, int usuarioId);
    }
}
