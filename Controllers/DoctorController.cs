using MedicAppAPI.Data;
using MedicAppAPI.DTOs;
using MedicAppAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicAppAPI.Controllers;
using MedicAppAPI.Services;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MedicAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {

        private readonly MedicAppDb _db;
        private readonly DataVerifier _dataVerifier;

        public DoctorController(MedicAppDb db, DataVerifier dataVerifier)
        {
            _db = db;
            _dataVerifier = dataVerifier;
        }

        // OBTENER TODOS

        [HttpGet]
        public async Task<ActionResult<List<DoctorDTO>>> GetAllAsync()
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
            return Ok(doctores);
        }

        // OBTENER POR ESPECIALIDAD

        [HttpGet("filtrar-especialidad/{especialidad}")]
        public async Task<ActionResult<List<DoctorDTO>>> GetByEspecialidadAsync(string especialidad)
        {
            var formattedSpecialty = especialidad.ToLower().Replace(" ", "-");
            var doctores = await _db.Doctores
                .Include(d => d.Especialidad)
                .Where(d => d.Especialidad.Nombre.ToLower().Replace(" ", "-") == formattedSpecialty)
                .Select(d => new FiltroEspecialidadDTO
                {
                    DoctorID = d.DoctorID,
                    NombreCompleto = $"{d.Apellido}, {d.Nombre}",
                })
                .ToListAsync();
            return Ok(new { Especialidad = especialidad, Doctores = doctores });
        }

        // AGREGAR DOCTOR

        [HttpPost("agregar")]
        public async Task<ActionResult<DoctorInputDTO>> PostDoctor([FromBody] DoctorInputDTO doctor)
        {
            var especialidad = await _dataVerifier.ObtenerEspecialidad(doctor.Especialidad);
            if (especialidad is null)
            {
                return BadRequest($"La especialidad '{doctor.Especialidad}' no existe.");
            }
            bool existeDoctor = await _dataVerifier.DoctorExiste(doctor);
            if (existeDoctor)
            {
                return BadRequest($"El doctor ya existe.");
            }
            var nuevoDoctor = new Doctor
            {
                Nombre = doctor.Nombre,
                Apellido = doctor.Apellido,
                EspecialidadID = especialidad.EspecialidadID,
            };

            _db.Doctores.Add(nuevoDoctor);
            await _db.SaveChangesAsync();

            return Ok($"Se ha agregado el registro '{doctor.Apellido}, {doctor.Nombre}' a la base de datos.");
        }

        // EDITAR MÉDICO
        [HttpPut("editar/{doctorId}")]
        public async Task<ActionResult<DoctorDTO>> PutDoctor(int doctorId, [FromBody] EditarDoctorDTO doctorEditado)
        {
            var doctorExistente = await _db.Doctores.FindAsync(doctorId);
            if (doctorExistente is null)
            {
                return NotFound("El doctor no existe.");
            }

            var registroEditado = new EditarDoctorDTO
            {
                Nombre = doctorEditado.Nombre,
                Apellido = doctorEditado.Apellido
            };

            await _db.SaveChangesAsync();

            return Ok($"Doctor actualizado: '{registroEditado.Apellido}, {registroEditado.Nombre}'");
        }

        // EDITAR ESPECIALIDAD MÉDICO
        [HttpPut("editar-especialidad/{doctorId}")]
        public async Task<ActionResult<DoctorDTO>> PutEspecialidadDoctor(int doctorId, [FromBody] EditarEspecialidadDTO doctorEditado)
        {
            var doctorExistente = await _db.Doctores.FindAsync(doctorId);
            if (doctorExistente is null)
            {
                return NotFound("El doctor no existe.");
            }

            bool especialidadExiste = await _dataVerifier.EspecialidadExiste(doctorEditado.Especialidad);
            if (!especialidadExiste)
            {
                return BadRequest($"La especialidad '{doctorEditado.Especialidad}' no existe.");
            }

            var registroEditado = new EditarEspecialidadDTO
            {
                Especialidad = doctorEditado.Especialidad,
            };

            await _db.SaveChangesAsync();

            return Ok($"Especialidad actualizada: '{registroEditado.Especialidad}'");
        }

        // ELIMINAR MÉDICO

        [HttpDelete("eliminar/{doctorId}")]
        public async Task<ActionResult> DeleteDoctor(int doctorId)
        {
            var doctorExistente = await _db.Doctores.FindAsync(doctorId);
            if (doctorExistente is null)
            {
                return NotFound("El doctor no existe.");
            }
            _db.Doctores.Remove(doctorExistente);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
