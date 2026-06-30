using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.DTOs.ReservasDTO
{
    public record CrearClienteDto(

        [Required, MaxLength(100)] string Nombre,
        [Required, MaxLength(100)] string Apellido,
        [EmailAddress, MaxLength(150)] string? Email,
        [MaxLength(20)] string? Telefono
    );
}
