using MedicAppAPI.Data;
using MedicAppAPI.DTOs;
using MedicAppAPI.Models;
using MedicAppAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace MedicAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadController : ControllerBase
    {

        private readonly MedicAppDb _db;
        private readonly DataVerifier _dataVerifier;

        public EspecialidadController(MedicAppDb db, DataVerifier dataVerifier)
        {
            _db = db;
            _dataVerifier = dataVerifier;
        }

        // OBTENER TODOS

        [HttpGet]
        public async Task<ActionResult<List<Especialidad>>> GetAllAsync()
        {
            var especialidades = await _db.Especialidades
                .Select(e => new Especialidad
                {
                    EspecialidadID = e.EspecialidadID,
                    Nombre = e.Nombre
                })
                .ToListAsync();
            return Ok(especialidades);
        }

        [HttpPost("agregar")]
        public async Task<ActionResult<EspecialidadDTO>> PostEspecialidad([FromBody] EspecialidadDTO especialidad)
        {
            bool especialidadExiste = await _dataVerifier.EspecialidadExiste(especialidad.Nombre);
            if (especialidadExiste)
            {
                return BadRequest($"La especialidad '{especialidad}' ya existe.");
            }

            var nuevaEspecialidad = new Especialidad
            {
                Nombre = especialidad.Nombre
            };

            _db.Especialidades.Add(nuevaEspecialidad);
            await _db.SaveChangesAsync();

            return Ok($"Nueva especialidad agregada: {nuevaEspecialidad}");
        }

        // EDITAR ESPECIALIDAD
        [HttpPut("editar/{especialidadId}")]
        public async Task<ActionResult> PutEspecialidad(int especialidadId, [FromBody] EspecialidadDTO especialidadEditada)
        {
            var especialidadExistente = await _db.Especialidades.FindAsync(especialidadId);
            if (especialidadExistente is null)
            {
                return NotFound("La especialidad no existe.");
            }
            var registroEditado = new EspecialidadDTO
            {
                Nombre = especialidadEditada.Nombre
            };
            await _db.SaveChangesAsync();

            return Ok($"Especialidad editada: {registroEditado.Nombre}");
        }
        
        // ELIMINAR ESPECIALIDAD

        [HttpDelete("eliminar/{especialidadId}")]
        public async Task<ActionResult> DeleteEspecialidad(int especialidadId)
        {
            var especialidadExiste = await _db.Especialidades.FindAsync(especialidadId);
            if (especialidadExiste is null)
            {
                return NotFound("La especialidad no existe.");
            }
            _db.Especialidades.Remove(especialidadExiste);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
