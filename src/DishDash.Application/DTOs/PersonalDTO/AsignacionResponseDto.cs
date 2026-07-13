using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.DTOs.PersonalDTO
{
    public record AsignacionResponseDto(
    int AsignacionId,
    int EmpleadoId,
    string NombreEmpleado,
    int TurnoId,
    string NombreTurno,
    DateOnly Fecha,
    string? Observacion
    );
}
