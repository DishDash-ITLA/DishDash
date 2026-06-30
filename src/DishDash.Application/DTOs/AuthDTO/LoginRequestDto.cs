using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.DTOs.AuthDTO
{
    public record LoginRequestDto(

         [Required, EmailAddress] string Email,
         [Required, MinLength(6)] string Password
    );

}
