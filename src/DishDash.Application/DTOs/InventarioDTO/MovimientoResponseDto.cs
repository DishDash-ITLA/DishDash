using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.DTOs.InventarioDTO
{
    public record MovimientoResponseDto(

        int MovimientoId,
        string TipoMovimiento,
        decimal Cantidad,
        decimal StockAnterior,
        decimal StockResultante,
        string? Motivo,
        DateTime FechaMovimiento
    );
}
