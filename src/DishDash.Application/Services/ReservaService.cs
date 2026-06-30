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
    public class ReservaService(
     IReservaRepository reservaRepo,
     IClienteRepository clienteRepo,
     IMesaRepository mesaRepo) : IReservaService
    {
        public async Task<IEnumerable<ReservaResponseDto>> ObtenerTodasAsync()
        {
            var reservas = await reservaRepo.ObtenerTodasAsync();
            return reservas.Select(MapToDto);
        }

        public async Task<IEnumerable<ReservaResponseDto>> ObtenerPorFechaAsync(DateOnly fecha)
        {
            var reservas = await reservaRepo.ObtenerPorFechaAsync(fecha);
            return reservas.Select(MapToDto);
        }

        public async Task<ReservaResponseDto> ObtenerPorIdAsync(int id)
        {
            var reserva = await reservaRepo.ObtenerPorIdAsync(id)
                ?? throw new NotFoundException("Reserva", id);
            return MapToDto(reserva);
        }

        public async Task<ReservaResponseDto> CrearAsync(CrearReservaDto dto, int usuarioId)
        {
            _ = await clienteRepo.ObtenerPorIdAsync(dto.ClienteId)
                ?? throw new NotFoundException("Cliente", dto.ClienteId);

            var mesa = await mesaRepo.ObtenerPorIdAsync(dto.MesaId)
                ?? throw new NotFoundException("Mesa", dto.MesaId);

            if (!mesa.Activa)
                throw new BusinessException("La mesa no está disponible.");

            if (mesa.Capacidad < dto.NumeroPersonas)
                throw new BusinessException($"La mesa tiene capacidad para {mesa.Capacidad} personas.");

            if (await reservaRepo.HayConflictoMesaAsync(dto.MesaId, dto.FechaReserva, dto.HoraReserva))
                throw new ConflictException("La mesa ya tiene una reserva en ese horario.");

            var reserva = new Reserva
            {
                ClienteId = dto.ClienteId,
                MesaId = dto.MesaId,
                UsuarioCreadoPorId = usuarioId,
                FechaReserva = dto.FechaReserva,
                HoraReserva = dto.HoraReserva,
                NumeroPersonas = dto.NumeroPersonas,
                Notas = dto.Notas,
                Estado = "Pendiente"
            };

            var creada = await reservaRepo.CrearAsync(reserva);
            return MapToDto(creada);
        }

        public async Task<ReservaResponseDto> ActualizarAsync(int id, ActualizarReservaDto dto)
        {
            var reserva = await reservaRepo.ObtenerPorIdAsync(id)
                ?? throw new NotFoundException("Reserva", id);

            if (reserva.Estado == "Cancelada" || reserva.Estado == "Completada")
                throw new BusinessException("No se puede modificar una reserva cancelada o completada.");

            if (await reservaRepo.HayConflictoMesaAsync(reserva.MesaId, dto.FechaReserva, dto.HoraReserva, id))
                throw new ConflictException("La mesa ya tiene una reserva en ese horario.");

            reserva.FechaReserva = dto.FechaReserva;
            reserva.HoraReserva = dto.HoraReserva;
            reserva.NumeroPersonas = dto.NumeroPersonas;
            reserva.Notas = dto.Notas;
            reserva.ActualizadoEn = DateTime.UtcNow;

            var actualizada = await reservaRepo.ActualizarAsync(reserva);
            return MapToDto(actualizada);
        }

        public async Task<ReservaResponseDto> CambiarEstadoAsync(int id, CambiarEstadoReservaDto dto, int usuarioId)
        {
            var estadosValidos = new[] { "Pendiente", "Confirmada", "Cancelada", "Completada", "NoShow" };

            if (!estadosValidos.Contains(dto.NuevoEstado))
                throw new BusinessException($"Estado inválido: '{dto.NuevoEstado}'.");

            var reserva = await reservaRepo.ObtenerPorIdAsync(id)
                ?? throw new NotFoundException("Reserva", id);

            var historial = new HistorialReserva
            {
                ReservaId = id,
                UsuarioId = usuarioId,
                EstadoAnterior = reserva.Estado,
                EstadoNuevo = dto.NuevoEstado,
                Observacion = dto.Observacion
            };
            reserva.Historial.Add(historial);

            reserva.Estado = dto.NuevoEstado;
            reserva.ActualizadoEn = DateTime.UtcNow;

            var actualizada = await reservaRepo.ActualizarAsync(reserva);
            return MapToDto(actualizada);
        }

        private static ReservaResponseDto MapToDto(Reserva r) => new(
            r.ReservaId,
            r.ClienteId,
            r.Cliente is not null ? $"{r.Cliente.Nombre} {r.Cliente.Apellido}" : string.Empty,
            r.MesaId,
            r.Mesa?.Numero ?? 0,
            r.Mesa?.Ubicacion ?? string.Empty,
            r.FechaReserva,
            r.HoraReserva,
            r.NumeroPersonas,
            r.Estado,
            r.Notas,
            r.CreadoEn
        );
    }

}
