using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.DTOs.PersonalDTO
{
    public record CrearEmpleadoDto(
    [Required, MaxLength(100)] string Nombre,
    [Required, MaxLength(100)] string Apellido,
    [MaxLength(20)] string? Cedula,
    [EmailAddress, MaxLength(150)] string? Email,
    [MaxLength(20)] string? Telefono,
    [Required] int PuestoId,
    [Required] DateOnly FechaIngreso,
    decimal? Salario
    );
}
