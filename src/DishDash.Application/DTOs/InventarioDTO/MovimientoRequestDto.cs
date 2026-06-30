using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.DTOs.InventarioDTO
{
    public record MovimientoRequestDto(

        [Required] int TipoMovimientoId,
        [Required, Range(0.001, double.MaxValue)] decimal Cantidad,
        [MaxLength(300)] string? Motivo
    );
}
