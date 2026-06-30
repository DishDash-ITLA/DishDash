using DishDash.Application.DTOs.ReservasDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.Interfaces
{
    public interface IMesaService
    {
        Task<IEnumerable<MesaResponseDto>> ObtenerTodasAsync();
        Task<IEnumerable<MesaResponseDto>> ObtenerDisponiblesAsync(DisponibilidadMesaRequestDto dto);
        Task<MesaResponseDto> ObtenerPorIdAsync(int id);
        Task<MesaResponseDto> CrearAsync(CrearMesaDto dto);
        Task<MesaResponseDto> ActualizarEstadoAsync(int id, string nuevoEstado);
    }
}
