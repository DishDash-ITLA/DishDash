using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.DTOs.PersonalDTO
{
    public record AsignarTurnoDto(
    [Required] int EmpleadoId,
    [Required] int TurnoId,
    [Required] DateOnly Fecha,
    [MaxLength(200)] string? Observacion
    );

}
