using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.DTOs.ReservasDTO
{
    public record CrearMesaDto(

        [Required, Range(1, 999)] int Numero,
        [Required, Range(1, 50)] int Capacidad,
        [MaxLength(100)] string? Ubicacion
    );
}
