using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.DTOs.InventarioDTO
{
    public record ActualizarProductoDto(

        [Required, MaxLength(150)] string Nombre,
        [MaxLength(300)] string? Descripcion,
        [Required] int CategoriaId,
        [Required] int UnidadId,
        [Required, Range(0, double.MaxValue)] decimal StockMinimo,
        decimal? StockMaximo,
        decimal? PrecioUnitario,
        bool Activo = true
    );
}
