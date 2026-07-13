using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.DTOs.PersonalDTO
{
    public record RegistrarAsistenciaDto(
    [Required] int EmpleadoId,
    int? AsignacionId,
    [Required] DateOnly Fecha,
    DateTime? HoraEntrada,
    [MaxLength(300)] string? Observacion
    );
}
