using MedicAppAPI.Data;
using MedicAppAPI.DTOs;
using MedicAppAPI.Interfaces;
using MedicAppAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace MedicAppAPI.Services
{
    public class HorarioService: IHorario
    {
        private readonly MedicAppDb _db;

        public HorarioService(MedicAppDb db)
        {
            _db = db;
        }

        public async Task<IEnumerable<HorarioDTO>> ObtenerTodosAsync()
        {
            var horarios = await _db.Horarios
                .Include(h => h.Doctor)
                .GroupBy(h => new { h.DoctorID, h.Doctor.Nombre, h.Doctor.Apellido })
                .Select(group => new HorarioDTO
                {
                    DoctorID = group.Key.DoctorID,
                    Doctor = $"{group.Key.Apellido}, {group.Key.Nombre}",
                    Detalle = group
                    .OrderBy(h => h.Dia)
                    .Select(h => new DetalleHorarioDTO
                    {
                        Dia = h.Dia.ToString(),
                        Inicio = h.Inicio.ToString(@"hh\:mm"),
                        Fin = h.Fin.ToString(@"hh\:mm")
                    }).ToList()
                })
                .ToListAsync();
           
            return horarios;
        }

        public async Task<IEnumerable<HorarioDTO>> ObtenerHorariosAsync(int doctorID)
        {
            var doctor = await _db.Horarios.AnyAsync(h => h.DoctorID == doctorID);
            if (!doctor) return null;

            var horarios = await ObtenerTodosAsync();

            var horariosFiltrados = horarios
                .Where(h => h.DoctorID == doctorID)
                .ToList();

            return horariosFiltrados;
        }

        public async Task<DetalleHorarioDTO> CrearHorarioAsync(int doctorID, NuevoHorarioDTO nuevoHorario)
        {
            var doctor = await _db.Doctores.FindAsync(doctorID);
            if (doctor is null) return null;

            int dia = (int)Enum.Parse(typeof(Dias), Capitalizar(nuevoHorario.Dia));
            bool esDiaValido = EsDiaValido(dia);
            bool diaYaAgendado = await DiaYaAgendado(doctorID, dia);
            if (!esDiaValido || diaYaAgendado) return null;

            var nuevoRegistro = new Horario
            {
                DoctorID = doctor.DoctorID,
                Doctor = doctor,
                Dia = (Dias)dia,
                Inicio = TimeSpan.ParseExact(nuevoHorario.Inicio, @"hh\:mm", CultureInfo.InvariantCulture),
                Fin = TimeSpan.ParseExact(nuevoHorario.Fin, @"hh\:mm", CultureInfo.InvariantCulture),
                Duración = TimeSpan.FromMinutes(30)
            };

            _db.Horarios.Add(nuevoRegistro);
            await _db.SaveChangesAsync();

            var horarioDTO = new DetalleHorarioDTO
            {
                    Dia = nuevoRegistro.Dia.ToString(),
                    Inicio = nuevoRegistro.Inicio.ToString(@"hh\:mm"),
                    Fin = nuevoRegistro.Fin.ToString(@"hh\:mm")
            };
            return horarioDTO;
        }
        
        public async Task<DetalleHorarioDTO> EditarHorarioAsync(int doctorID, string dia, ModificarHorarioDTO horarioActualizado)
        {
            var doctor = await _db.Doctores.FindAsync(doctorID);
            if (doctor is null) return null;

            int diaAModificar = (int)Enum.Parse(typeof(Dias), Capitalizar(dia));

            bool esDiaValido = EsDiaValido(diaAModificar);
            bool diaYaAgendado = await DiaYaAgendado(doctorID, diaAModificar);
            if (!diaYaAgendado || !esDiaValido) return null;

            var registro = await ObtenerHorario(doctorID, diaAModificar);

            if (!string.IsNullOrEmpty(horarioActualizado.Inicio)
                && TimeSpan.TryParseExact(horarioActualizado.Inicio, @"hh\:mm", CultureInfo.InvariantCulture, out TimeSpan nuevoInicio))
            {
                registro.Inicio = nuevoInicio;
            }

            if (!string.IsNullOrEmpty(horarioActualizado.Fin)
                && TimeSpan.TryParseExact(horarioActualizado.Fin, @"hh\:mm", CultureInfo.InvariantCulture, out TimeSpan nuevoFin))
            {
                registro.Fin = nuevoFin;
            }

            await _db.SaveChangesAsync();

            var detalleHorarioDTO = new DetalleHorarioDTO
            {
                Dia = registro.Dia.ToString(),
                Inicio = registro.Inicio.ToString(),
                Fin = registro.Fin.ToString()
            };

            return detalleHorarioDTO;
        }

        public async Task<bool> EliminarHorarioAsync(int doctorID, string dia)
        {
            var doctor = await _db.Doctores.FindAsync(doctorID);
            if (doctor is null) return false;

            int diaAEliminar = (int)Enum.Parse(typeof(Dias), Capitalizar(dia));
            bool diaYaAgendado = await DiaYaAgendado(doctorID, diaAEliminar);
            if (!diaYaAgendado) return false;

            var horarioExistente = await ObtenerHorario(doctorID, diaAEliminar);

            _db.Horarios.Remove(horarioExistente);

            await _db.SaveChangesAsync();

            return true;
        }

        //////////////////////////////////////////////////////////////////////////////////////////

        public async Task<bool> DiaYaAgendado(int doctorID, int dia)
        {
            return await _db.Horarios.AnyAsync(h =>
                h.DoctorID == doctorID && (int)h.Dia == dia);
        }

        public bool EsDiaValido(int dia)
        {
            return Enum.IsDefined(typeof(Dias), dia);
        }

        public string Capitalizar(string dia)
        {
            return char.ToUpper(dia[0]) + dia[1..].ToLower();
        }

        public async Task<Horario> ObtenerHorario(int doctorID, int dia)
        {
            return await _db.Horarios
                .SingleOrDefaultAsync(h => h.DoctorID == doctorID && (int)h.Dia == dia);
        }

    }
}
