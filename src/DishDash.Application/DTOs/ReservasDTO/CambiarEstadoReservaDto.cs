using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.DTOs.ReservasDTO
{
    public record CambiarEstadoReservaDto(

        [Required] string NuevoEstado,
        [MaxLength(300)] string? Observacion
    );
}
