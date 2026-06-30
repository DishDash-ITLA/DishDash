using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.DTOs.ReservasDTO
{
    public record ReservaResponseDto(

        int ReservaId,
        int ClienteId,
        string NombreCliente,
        int MesaId,
        int NumeroMesa,
        string UbicacionMesa,
        DateOnly FechaReserva,
        TimeOnly HoraReserva,
        int NumeroPersonas,
        string Estado,
        string? Notas,
        DateTime CreadoEn
    );
}
