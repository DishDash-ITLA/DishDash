using DishDash.Application.DTOs.ReservasDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteResponseDto>> ObtenerTodosAsync();
        Task<ClienteResponseDto> ObtenerPorIdAsync(int id);
        Task<ClienteResponseDto> CrearAsync(CrearClienteDto dto);
        Task<ClienteResponseDto> ActualizarAsync(int id, CrearClienteDto dto);
    }
}
