using Azure;
using MedicAppAPI.Data;
using MedicAppAPI.DTOs;
using MedicAppAPI.Interfaces;
using MedicAppAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Numerics;

namespace MedicAppAPI.Services
{
    public class DoctorService : IDoctor
    {

        private readonly MedicAppDb _db;

        public DoctorService(MedicAppDb db)
        {
            _db = db;
        }
        public async Task<IEnumerable<DoctorDTO>> ObtenerTodosAsync()
        {
            var doctores = await _db.Doctores
                .Include(d => d.Especialidad)
                .Select(d => new DoctorDTO
                {
                    DoctorID = d.DoctorID,
                    NombreCompleto = $"{d.Apellido}, {d.Nombre}",
                    Especialidad = d.Especialidad.Nombre
                })
                .ToListAsync();
            return doctores;
        }

        public async Task<DoctorDTO> ObtenerDoctorAsync(int doctorID)
        {
            var doctor = await _db.Doctores
                .Include(d => d.Especialidad)
                .FirstOrDefaultAsync(d => d.DoctorID == doctorID);
            
            if (doctor is null) return null;

            return new DoctorDTO
            {
                DoctorID = doctor.DoctorID,
                NombreCompleto = $"{doctor.Apellido}, {doctor.Nombre}",
                Especialidad = doctor.Especialidad.Nombre
            };
        }

        public async Task<DoctorDTO> CrearDoctorAsync(DoctorInputDTO nuevoDoctor)
        {
            var especialidad = await DevolverEspecialidad(nuevoDoctor.Especialidad);
            if (especialidad is null) return null;

            bool doctorExiste = await DoctorExiste(nuevoDoctor);
            if (doctorExiste) return null;
            
            var nuevoRegistro = new Doctor
            {
                Nombre = nuevoDoctor.Nombre,
                Apellido = nuevoDoctor.Apellido,
                EspecialidadID = especialidad.EspecialidadID,
            };

            _db.Doctores.Add(nuevoRegistro);
            await _db.SaveChangesAsync();

            var doctorDTO = new DoctorDTO
            {
                DoctorID = nuevoRegistro.DoctorID,
                NombreCompleto = $"{nuevoRegistro.Apellido}, {nuevoRegistro.Nombre}",
                Especialidad = nuevoRegistro.Especialidad.Nombre
            };

            return doctorDTO;
        }

        public async Task<DoctorDTO> EditarDoctorAsync(int doctorID, EditarDoctorDTO actualizacion)
        {
            var doctor = await _db.Doctores
                .Include(d => d.Especialidad)
                .FirstOrDefaultAsync(d => d.DoctorID == doctorID);
           
            if (doctor is null) return null;

            doctor.Nombre = actualizacion.Nombre ?? doctor.Nombre;
            doctor.Apellido = actualizacion.Apellido ?? doctor.Apellido;

            var especialidad = await DevolverEspecialidad(actualizacion.Especialidad);
            if (especialidad is not null)
            {
                doctor.EspecialidadID = especialidad.EspecialidadID;
            }

            await _db.SaveChangesAsync();

            var doctorDTO = new DoctorDTO
            {
                DoctorID = doctor.DoctorID,
                NombreCompleto = $"{doctor.Apellido}, {doctor.Nombre}",
                Especialidad = doctor.Especialidad.Nombre
            };

            return doctorDTO;
        }

        public async Task<bool> EliminarDoctorAsync(int doctorID)
        {
            var doctor = await _db.Doctores.FindAsync(doctorID);
            if (doctor is null) return false;
            
            _db.Doctores.Remove(doctor);
            await _db.SaveChangesAsync();

            return true;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        private async Task<Especialidad> DevolverEspecialidad(string nombreEspecialidad)
        {
            var especialidad = await _db.Especialidades
                .FirstOrDefaultAsync(e => e.Nombre == nombreEspecialidad);
            if (especialidad is null) return null;

            return especialidad;
        }

        private async Task<bool> EspecialidadExiste(int especialidadID)
        {
            var especialidad = await _db.Especialidades
                .FindAsync(especialidadID);
            if (especialidad is null) return false;

            return true;
        }
        private async Task<bool> DoctorExiste(DoctorInputDTO doctor)
        {
            return await _db.Doctores
                .AnyAsync(d =>
                d.Nombre.ToLower().Replace(" ", "-") == doctor.Nombre.ToLower().Replace(" ", "-") &&
                d.Apellido.ToLower().Replace(" ", "-") == doctor.Apellido.ToLower().Replace(" ", "-") &&
                d.Especialidad.Nombre.ToLower().Replace(" ", "-") == doctor.Especialidad.ToLower().Replace(" ", "-")
                );
        }
        // Agregar el resto de los métodos
    }
}
