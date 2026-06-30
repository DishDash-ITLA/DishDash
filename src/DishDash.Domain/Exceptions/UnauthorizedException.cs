using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Domain.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string mensaje = "No autorizado.") : base(mensaje) { }

    }
}
