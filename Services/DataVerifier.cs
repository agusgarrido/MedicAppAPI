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
    }
}
