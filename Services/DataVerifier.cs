using MedicAppAPI.Data;
using MedicAppAPI.DTOs;
using MedicAppAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicAppAPI.Services
{
    public class DataVerifier
    {

        private readonly MedicAppDb _db;

        public DataVerifier(MedicAppDb db)
        {
            _db = db;
        }

        public async Task<bool> DoctorExiste(DoctorInputDTO doctor)
        {
            return await _db.Doctores
                .AnyAsync(d =>
                d.Nombre.ToLower().Replace(" ", "-") == doctor.Nombre.ToLower().Replace(" ", "-") &&
                d.Apellido.ToLower().Replace(" ", "-") == doctor.Apellido.ToLower().Replace(" ", "-") &&
                d.Especialidad.Nombre.ToLower().Replace(" ", "-") == doctor.Especialidad.ToLower().Replace(" ", "-")
                );
        }
        public async Task<Especialidad> ObtenerEspecialidad(string nombreEspecialidad)
        {
            return await _db.Especialidades
                .SingleOrDefaultAsync(e =>
                e.Nombre.ToLower().Replace(" ", "-") == nombreEspecialidad.ToLower().Replace(" ", "-"));
        }

        public async Task<bool> EspecialidadExiste(string nombreEspecialidad)
        {
            return await _db.Especialidades
                .AnyAsync(e =>
                e.Nombre.ToLower().Replace(" ", "-") == nombreEspecialidad.ToLower().Replace(" ", "-")
                );
        }

        public async Task<bool> PacienteExiste(string dni)
        {
            return await _db.Pacientes
                .AnyAsync(p => p.DNI == dni);
        }

        public async Task<Paciente> ObtenerPaciente(string dni)
        {
            return await _db.Pacientes
                .SingleOrDefaultAsync(p => p.DNI == dni);
        }

        public async Task<bool> DiaYaAgendado(int doctorID, int dia)
        {
            //Dias diaEnum = (Dias)Enum.Parse(typeof(Dias), dia, true);
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
