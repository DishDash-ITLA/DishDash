using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.DTOs.ReservasDTO
{
    public record ActualizarReservaDto(

        [Required] DateOnly FechaReserva,
        [Required] TimeOnly HoraReserva,
        [Required, Range(1, 50)] int NumeroPersonas,
        [MaxLength(500)] string? Notas
    );
}
