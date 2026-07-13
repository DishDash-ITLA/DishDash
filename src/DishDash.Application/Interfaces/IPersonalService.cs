using DishDash.Application.DTOs.PersonalDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.Interfaces
{
    public interface IPersonalService
    {
        Task<IEnumerable<EmpleadoResponseDto>> ObtenerEmpleadosAsync();
        Task<EmpleadoResponseDto> ObtenerEmpleadoPorIdAsync(int id);
        Task<EmpleadoResponseDto> CrearEmpleadoAsync(CrearEmpleadoDto dto);
        Task<EmpleadoResponseDto> ActualizarEmpleadoAsync(int id, ActualizarEmpleadoDto dto);
        Task<IEnumerable<PuestoResponseDto>> ObtenerPuestosAsync();
        Task<IEnumerable<TurnoResponseDto>> ObtenerTurnosAsync();
        Task<AsignacionResponseDto> AsignarTurnoAsync(AsignarTurnoDto dto, int usuarioId);
        Task<IEnumerable<AsignacionResponseDto>> ObtenerAsignacionesPorFechaAsync(DateOnly fecha);
        Task<IEnumerable<AsignacionResponseDto>> ObtenerAsignacionesPorEmpleadoAsync(int empleadoId);
        Task<AsistenciaResponseDto> RegistrarAsistenciaAsync(RegistrarAsistenciaDto dto, int usuarioId);
        Task<AsistenciaResponseDto> MarcarSalidaAsync(int asistenciaId, MarcarSalidaDto dto);
        Task<IEnumerable<AsistenciaResponseDto>> ObtenerAsistenciasPorFechaAsync(DateOnly fecha);
        Task<IEnumerable<AsistenciaResponseDto>> ObtenerAsistenciasPorEmpleadoAsync(int empleadoId);
    }
}
