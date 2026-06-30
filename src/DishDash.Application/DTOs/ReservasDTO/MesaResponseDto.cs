using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.DTOs.ReservasDTO
{
    public record MesaResponseDto(

        int MesaId,
        int Numero,
        int Capacidad,
        string? Ubicacion,
        string Estado,
        bool Activa
    );
}
