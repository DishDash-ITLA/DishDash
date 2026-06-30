using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.DTOs.InventarioDTO
{
    public record ProductoResponseDto(

        int ProductoId,
        string Nombre,
        string? Descripcion,
        string Categoria,
        string Unidad,
        string Abreviatura,
        decimal StockActual,
        decimal StockMinimo,
        decimal? StockMaximo,
        decimal? PrecioUnitario,
        string NivelAlerta,
        bool Activo
    );
}
