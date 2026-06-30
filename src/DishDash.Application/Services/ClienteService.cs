using DishDash.Application.DTOs.ReservasDTO;
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
    public class ClienteService(IClienteRepository clienteRepo) : IClienteService
    {
        public async Task<IEnumerable<ClienteResponseDto>> ObtenerTodosAsync()
        {
            var clientes = await clienteRepo.ObtenerTodosAsync();
            return clientes.Select(MapToDto);
        }

        public async Task<ClienteResponseDto> ObtenerPorIdAsync(int id)
        {
            var cliente = await clienteRepo.ObtenerPorIdAsync(id)
                ?? throw new NotFoundException("Cliente", id);
            return MapToDto(cliente);
        }

        public async Task<ClienteResponseDto> CrearAsync(CrearClienteDto dto)
        {
            var cliente = new Cliente
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Email = dto.Email?.ToLower().Trim(),
                Telefono = dto.Telefono
            };
            var creado = await clienteRepo.CrearAsync(cliente);
            return MapToDto(creado);
        }

        public async Task<ClienteResponseDto> ActualizarAsync(int id, CrearClienteDto dto)
        {
            var cliente = await clienteRepo.ObtenerPorIdAsync(id)
                ?? throw new NotFoundException("Cliente", id);

            cliente.Nombre = dto.Nombre;
            cliente.Apellido = dto.Apellido;
            cliente.Email = dto.Email?.ToLower().Trim();
            cliente.Telefono = dto.Telefono;

            var actualizado = await clienteRepo.ActualizarAsync(cliente);
            return MapToDto(actualizado);
        }

        private static ClienteResponseDto MapToDto(Cliente c) => new(
            c.ClienteId, c.Nombre, c.Apellido, c.Email, c.Telefono, c.CreadoEn);
    }
}
