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
    public class MesaService(IMesaRepository mesaRepo) : IMesaService
    {
        private static readonly string[] EstadosValidos = ["Disponible", "Ocupada", "Reservada", "Mantenimiento"];

        public async Task<IEnumerable<MesaResponseDto>> ObtenerTodasAsync()
        {
            var mesas = await mesaRepo.ObtenerTodasAsync();
            return mesas.Select(MapToDto);
        }

        public async Task<IEnumerable<MesaResponseDto>> ObtenerDisponiblesAsync(DisponibilidadMesaRequestDto dto)
        {
            var mesas = await mesaRepo.ObtenerDisponiblesAsync(dto.Fecha, dto.Hora, dto.Personas);
            return mesas.Select(MapToDto);
        }

        public async Task<MesaResponseDto> ObtenerPorIdAsync(int id)
        {
            var mesa = await mesaRepo.ObtenerPorIdAsync(id)
                ?? throw new NotFoundException("Mesa", id);
            return MapToDto(mesa);
        }

        public async Task<MesaResponseDto> CrearAsync(CrearMesaDto dto)
        {
            var mesa = new Mesa
            {
                Numero = dto.Numero,
                Capacidad = dto.Capacidad,
                Ubicacion = dto.Ubicacion,
                Estado = "Disponible"
            };
            var creada = await mesaRepo.CrearAsync(mesa);
            return MapToDto(creada);
        }

        public async Task<MesaResponseDto> ActualizarEstadoAsync(int id, string nuevoEstado)
        {
            if (!EstadosValidos.Contains(nuevoEstado))
                throw new BusinessException($"Estado inválido. Valores permitidos: {string.Join(", ", EstadosValidos)}");

            var mesa = await mesaRepo.ObtenerPorIdAsync(id)
                ?? throw new NotFoundException("Mesa", id);

            mesa.Estado = nuevoEstado;
            var actualizada = await mesaRepo.ActualizarAsync(mesa);
            return MapToDto(actualizada);
        }

        private static MesaResponseDto MapToDto(Mesa m) => new(
            m.MesaId, m.Numero, m.Capacidad, m.Ubicacion, m.Estado, m.Activa);
    }
}
