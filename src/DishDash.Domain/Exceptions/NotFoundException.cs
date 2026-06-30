using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entidad, object id)
        : base($"{entidad} con id '{id}' no fue encontrado.") { }
    }
}
