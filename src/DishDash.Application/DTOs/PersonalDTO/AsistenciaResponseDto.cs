using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.DTOs.PersonalDTO
{
    public record AsistenciaResponseDto(
    int AsistenciaId,
    int EmpleadoId,
    string NombreEmpleado,
    DateOnly Fecha,
    DateTime? HoraEntrada,
    DateTime? HoraSalida,
    string Estado,
    string? Observacion
    );
}
