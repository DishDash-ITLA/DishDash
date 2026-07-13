using DishDash.Application.DTOs.PersonalDTO;
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
    public class PersonalService(
    IEmpleadoRepository empleadoRepo,
    IPuestoRepository puestoRepo,
    ITurnoRepository turnoRepo,
    IAsistenciaRepository asistenciaRepo) : IPersonalService
    {
        // ── Empleados ────────────────────────────────────────────
        public async Task<IEnumerable<EmpleadoResponseDto>> ObtenerEmpleadosAsync()
        {
            var empleados = await empleadoRepo.ObtenerTodosAsync();
            return empleados.Select(MapEmpleadoToDto);
        }

        public async Task<EmpleadoResponseDto> ObtenerEmpleadoPorIdAsync(int id)
        {
            var empleado = await empleadoRepo.ObtenerPorIdAsync(id)
                ?? throw new NotFoundException("Empleado", id);
            return MapEmpleadoToDto(empleado);
        }

        public async Task<EmpleadoResponseDto> CrearEmpleadoAsync(CrearEmpleadoDto dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.Cedula) &&
                await empleadoRepo.ExisteCedulaAsync(dto.Cedula))
                throw new ConflictException($"Ya existe un empleado con la cédula '{dto.Cedula}'.");

            var puesto = await puestoRepo.ObtenerPorIdAsync(dto.PuestoId)
                ?? throw new NotFoundException("Puesto", dto.PuestoId);

            var empleado = new Empleado
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Cedula = dto.Cedula,
                Email = dto.Email,
                Telefono = dto.Telefono,
                PuestoId = dto.PuestoId,
                FechaIngreso = dto.FechaIngreso,
                Salario = dto.Salario,
                UsuarioId = dto.UsuarioId,
                Activo = true
            };

            var creado = await empleadoRepo.CrearAsync(empleado);
            creado.Puesto = puesto;
            return MapEmpleadoToDto(creado);
        }

        public async Task<EmpleadoResponseDto> ActualizarEmpleadoAsync(int id, ActualizarEmpleadoDto dto)
        {
            var empleado = await empleadoRepo.ObtenerPorIdAsync(id)
                ?? throw new NotFoundException("Empleado", id);

            if (!string.IsNullOrWhiteSpace(dto.Cedula) &&
                await empleadoRepo.ExisteCedulaAsync(dto.Cedula, excluirId: id))
                throw new ConflictException($"Ya existe otro empleado con la cédula '{dto.Cedula}'.");

            var puesto = await puestoRepo.ObtenerPorIdAsync(dto.PuestoId)
                ?? throw new NotFoundException("Puesto", dto.PuestoId);

            empleado.Nombre = dto.Nombre;
            empleado.Apellido = dto.Apellido;
            empleado.Cedula = dto.Cedula;
            empleado.Email = dto.Email;
            empleado.Telefono = dto.Telefono;
            empleado.PuestoId = dto.PuestoId;
            empleado.Salario = dto.Salario;
            empleado.Activo = dto.Activo;
            empleado.ActualizadoEn = DateTime.UtcNow;
            empleado.Puesto = puesto;

            var actualizado = await empleadoRepo.ActualizarAsync(empleado);
            return MapEmpleadoToDto(actualizado);
        }

        // ── Puestos ──────────────────────────────────────────────
        public async Task<IEnumerable<PuestoResponseDto>> ObtenerPuestosAsync()
        {
            var puestos = await puestoRepo.ObtenerTodosAsync();
            return puestos.Select(p => new PuestoResponseDto(p.PuestoId, p.Nombre, p.Descripcion));
        }

        // ── Turnos ───────────────────────────────────────────────
        public async Task<IEnumerable<TurnoResponseDto>> ObtenerTurnosAsync()
        {
            var turnos = await turnoRepo.ObtenerTodosAsync();
            return turnos.Select(t => new TurnoResponseDto(t.TurnoId, t.Nombre, t.HoraInicio, t.HoraFin));
        }

        public async Task<AsignacionResponseDto> AsignarTurnoAsync(AsignarTurnoDto dto, int usuarioId)
        {
            _ = await empleadoRepo.ObtenerPorIdAsync(dto.EmpleadoId)
                ?? throw new NotFoundException("Empleado", dto.EmpleadoId);

            _ = await turnoRepo.ObtenerPorIdAsync(dto.TurnoId)
                ?? throw new NotFoundException("Turno", dto.TurnoId);

            var asignacion = new AsignacionTurno
            {
                EmpleadoId = dto.EmpleadoId,
                TurnoId = dto.TurnoId,
                Fecha = dto.Fecha,
                Observacion = dto.Observacion,
                AsignadoPorId = usuarioId
            };

            var creada = await turnoRepo.AsignarAsync(asignacion);
            return MapAsignacionToDto(creada);
        }

        public async Task<IEnumerable<AsignacionResponseDto>> ObtenerAsignacionesPorFechaAsync(DateOnly fecha)
        {
            var asignaciones = await turnoRepo.ObtenerAsignacionesPorFechaAsync(fecha);
            return asignaciones.Select(MapAsignacionToDto);
        }

        public async Task<IEnumerable<AsignacionResponseDto>> ObtenerAsignacionesPorEmpleadoAsync(int empleadoId)
        {
            var asignaciones = await turnoRepo.ObtenerAsignacionesPorEmpleadoAsync(empleadoId);
            return asignaciones.Select(MapAsignacionToDto);
        }

        // ── Asistencia ───────────────────────────────────────────
        public async Task<AsistenciaResponseDto> RegistrarAsistenciaAsync(RegistrarAsistenciaDto dto, int usuarioId)
        {
            _ = await empleadoRepo.ObtenerPorIdAsync(dto.EmpleadoId)
                ?? throw new NotFoundException("Empleado", dto.EmpleadoId);

            var asistencia = new Asistencia
            {
                EmpleadoId = dto.EmpleadoId,
                AsignacionId = dto.AsignacionId,
                Fecha = dto.Fecha,
                HoraEntrada = dto.HoraEntrada ?? DateTime.UtcNow,
                Estado = "Presente",
                Observacion = dto.Observacion,
                RegistradoPorId = usuarioId
            };

            var creada = await asistenciaRepo.RegistrarAsync(asistencia);
            return MapAsistenciaToDto(creada);
        }

        public async Task<AsistenciaResponseDto> MarcarSalidaAsync(int asistenciaId, MarcarSalidaDto dto)
        {
            var asistencia = await asistenciaRepo.ObtenerPorIdAsync(asistenciaId)
                ?? throw new NotFoundException("Asistencia", asistenciaId);

            asistencia.HoraSalida = dto.HoraSalida;
            var actualizada = await asistenciaRepo.ActualizarAsync(asistencia);
            return MapAsistenciaToDto(actualizada);
        }

        public async Task<IEnumerable<AsistenciaResponseDto>> ObtenerAsistenciasPorFechaAsync(DateOnly fecha)
        {
            var asistencias = await asistenciaRepo.ObtenerPorFechaAsync(fecha);
            return asistencias.Select(MapAsistenciaToDto);
        }

        public async Task<IEnumerable<AsistenciaResponseDto>> ObtenerAsistenciasPorEmpleadoAsync(int empleadoId)
        {
            var asistencias = await asistenciaRepo.ObtenerPorEmpleadoAsync(empleadoId);
            return asistencias.Select(MapAsistenciaToDto);
        }

        // ── Mappers ──────────────────────────────────────────────
        private static EmpleadoResponseDto MapEmpleadoToDto(Empleado e) => new(
            e.EmpleadoId, e.Nombre, e.Apellido, e.Cedula, e.Email, e.Telefono,
            e.Puesto?.Nombre ?? string.Empty, e.PuestoId, e.FechaIngreso, e.Salario, e.Activo
        );

        private static AsignacionResponseDto MapAsignacionToDto(AsignacionTurno a) => new(
            a.AsignacionId, a.EmpleadoId,
            a.Empleado is not null ? $"{a.Empleado.Nombre} {a.Empleado.Apellido}" : string.Empty,
            a.TurnoId, a.Turno?.Nombre ?? string.Empty, a.Fecha, a.Observacion
        );

        private static AsistenciaResponseDto MapAsistenciaToDto(Asistencia a) => new(
            a.AsistenciaId, a.EmpleadoId,
            a.Empleado is not null ? $"{a.Empleado.Nombre} {a.Empleado.Apellido}" : string.Empty,
            a.Fecha, a.HoraEntrada, a.HoraSalida, a.Estado, a.Observacion
        );
    }
}
