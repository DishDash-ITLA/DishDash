using DishDash.Application.DTOs.UsuarioDTO;
using DishDash.Application.Interfaces;
using DishDash.Domain.Entities;
using DishDash.Domain.Exceptions;
using DishDash.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.Services
{
    public class UsuarioService(
    IUsuarioRepository usuarioRepo,
    IRolRepository rolRepo) : IUsuarioService
    {
        public async Task<IEnumerable<UsuarioResponseDto>> ObtenerTodosAsync()
        {
            var usuarios = await usuarioRepo.ObtenerTodosAsync();
            return usuarios.Select(MapToDto);
        }

        public async Task<UsuarioResponseDto> ObtenerPorIdAsync(int id)
        {
            var usuario = await usuarioRepo.ObtenerPorIdAsync(id)
                ?? throw new NotFoundException("Usuario", id);
            return MapToDto(usuario);
        }

        public async Task<UsuarioResponseDto> CrearAsync(CrearUsuarioDto dto)
        {
            if (await usuarioRepo.ExisteEmailAsync(dto.Email))
                throw new ConflictException($"Ya existe un usuario con el email '{dto.Email}'.");

            var rol = await rolRepo.ObtenerPorIdAsync(dto.RolId)
                ?? throw new NotFoundException("Rol", dto.RolId);

            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Email = dto.Email.ToLower().Trim(),
                RolId = dto.RolId,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Activo = true
            };

            var creado = await usuarioRepo.CrearAsync(usuario);
            creado.Rol = rol;
            return MapToDto(creado);
        }

        public async Task<UsuarioResponseDto> ActualizarAsync(int id, ActualizarUsuarioDto dto)
        {
            var usuario = await usuarioRepo.ObtenerPorIdAsync(id)
                ?? throw new NotFoundException("Usuario", id);

            if (await usuarioRepo.ExisteEmailAsync(dto.Email, excluirId: id))
                throw new ConflictException($"Ya existe otro usuario con el email '{dto.Email}'.");

            var rol = await rolRepo.ObtenerPorIdAsync(dto.RolId)
                ?? throw new NotFoundException("Rol", dto.RolId);

            usuario.Nombre = dto.Nombre;
            usuario.Apellido = dto.Apellido;
            usuario.Email = dto.Email.ToLower().Trim();
            usuario.RolId = dto.RolId;
            usuario.Activo = dto.Activo;
            usuario.Rol = rol;

            var actualizado = await usuarioRepo.ActualizarAsync(usuario);
            return MapToDto(actualizado);
        }

        public async Task EliminarAsync(int id)
        {
            var usuario = await usuarioRepo.ObtenerPorIdAsync(id)
                ?? throw new NotFoundException("Usuario", id);

            // Soft delete
            usuario.Activo = false;
            await usuarioRepo.ActualizarAsync(usuario);
        }

        private static UsuarioResponseDto MapToDto(Usuario u) => new(
            u.UsuarioId,
            u.Nombre,
            u.Apellido,
            u.Email,
            u.Rol?.Nombre ?? string.Empty,
            u.RolId,
            u.Activo,
            u.CreadoEn,
            u.UltimoAcceso
        );
    }
}
