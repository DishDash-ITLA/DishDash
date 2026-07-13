using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.DTOs.PersonalDTO
{
    public record EmpleadoResponseDto(
    int EmpleadoId,
    string Nombre,
    string Apellido,
    string? Cedula,
    string? Email,
    string? Telefono,
    string Puesto,
    int PuestoId,
    DateOnly FechaIngreso,
    decimal? Salario,
    bool Activo
    );
}
