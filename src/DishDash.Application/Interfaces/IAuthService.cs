using DishDash.Application.DTOs.AuthDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto dto);
        Task LogoutAsync(string jti, int usuarioId, DateTime expiraEn);
        Task<bool> TokenEstaRevocadoAsync(string jti);
        Task CambiarPasswordAsync(int usuarioId, CambiarPasswordDto dto);
    }
}
