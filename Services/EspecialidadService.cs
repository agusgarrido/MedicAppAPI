using MedicAppAPI.Data;
using MedicAppAPI.DTOs;
using MedicAppAPI.Interfaces;
using MedicAppAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedicAppAPI.Services
{
    public class EspecialidadService : IEspecialidad
    {
        private readonly MedicAppDb _db;

        public EspecialidadService(MedicAppDb db)
        {
            _db = db;
        }

        public async Task<IEnumerable<EspecialidadDTO>> ObtenerTodasAsync()
        {
            var especialidad = await _db.Especialidades
                .Select(e => new EspecialidadDTO
                {
                    EspecialidadID = e.EspecialidadID,
                    Nombre = e.Nombre
                })
                .ToListAsync();
            return especialidad;
        }

        public async Task<EspecialidadDTO> CrearEspecialidadAsync(EspecialidadInputDTO nuevaEspecialidad)
        {
            bool especialidadExiste = await EspecialidadExiste(nuevaEspecialidad.Nombre);
            if (especialidadExiste) return null;

            var nuevoRegistro = new Especialidad { Nombre = nuevaEspecialidad.Nombre };

            _db.Especialidades.Add(nuevoRegistro);
            await _db.SaveChangesAsync();

            var especialidadDTO = new EspecialidadDTO
            {
                EspecialidadID = nuevoRegistro.EspecialidadID,
                Nombre = nuevoRegistro.Nombre
            };

            return especialidadDTO;
        }

        public async Task<EspecialidadDTO> EditarEspecialidadAsync(int especialidadID, EspecialidadInputDTO especialidadActualizada)
        {
            var especialidad = _db.Especialidades
                .FirstOrDefault(e => e.EspecialidadID == especialidadID);
            if (especialidad is null) return null;

            if (string.IsNullOrEmpty(especialidadActualizada.Nombre)) return null;

            especialidad.Nombre = especialidadActualizada.Nombre;

            await _db.SaveChangesAsync();

            var especialidadDTO = new EspecialidadDTO
            {
                EspecialidadID = especialidad.EspecialidadID,
                Nombre = especialidad.Nombre
            };
            return especialidadDTO;
        }

        public async Task<bool> EliminarEspecialidadAsync(int especialidadID)
        {
            var especialidad = await _db.Especialidades.FindAsync(especialidadID);
            if (especialidad is null) return false;

            _db.Especialidades.Remove(especialidad);
            await _db.SaveChangesAsync();
            
            return true;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        private async Task<bool> EspecialidadExiste(string nombreEspecialidad)
        {
            return await _db.Especialidades
                .AnyAsync(e => e.Nombre.ToLower().Replace(" ", "-") == nombreEspecialidad.ToLower().Replace(" ", "-"));
        }
    }
}
