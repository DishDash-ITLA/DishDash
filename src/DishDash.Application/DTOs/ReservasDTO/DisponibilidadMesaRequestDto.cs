using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.DTOs.ReservasDTO
{
    public record DisponibilidadMesaRequestDto(

        [Required] DateOnly Fecha,
        [Required] TimeOnly Hora,
        [Required, Range(1, 50)] int Personas
    );
}
