using MedicAppAPI.Data;
using MedicAppAPI.DTOs;
using MedicAppAPI.Models;
using MedicAppAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedicAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {

        private readonly MedicAppDb _db;
        private readonly DataVerifier _dataVerifier;

        public PacienteController(MedicAppDb db, DataVerifier dataVerifier)
        {
            _db = db;
            _dataVerifier = dataVerifier;
        }

        // OBTENER TODOS

        [HttpGet]
        public async Task<ActionResult<List<PacienteDTO>>> GetAllAsync()
        {
            var pacientes = await _db.Pacientes
                .Select(p => new PacienteDTO
                {
                    PacienteID = p.PacienteID,
                    NombreCompleto = $"{p.Apellido}, {p.Nombre}",
                    DNI = p.DNI,
                    Contacto = p.Contacto,
                    Correo = p.Correo

                }).
                ToListAsync();
            return Ok(pacientes);
        }

        // OBTENER POR DNI

        [HttpGet("buscar/{pacienteDNI}")]
        public async Task<ActionResult<PacienteDTO>> GetByDNIAsync(string dni)
        {
            var paciente = await _db.Pacientes
                .FirstOrDefaultAsync(p => p.DNI == dni);

            if (paciente is null)
            {
                return NotFound("Paciente no encontrado.");
            }

            var pacienteEncontrado = new PacienteDTO
            {
                PacienteID = paciente.PacienteID,
                NombreCompleto = $"{paciente.Apellido}, {paciente.Nombre}",
                DNI = paciente.DNI,
                Contacto = paciente.Contacto,
                Correo = paciente.Correo
            };

            return Ok(pacienteEncontrado);
        }

        // CREAR PACIENTE
        [HttpPost("agregar")]
        public async Task<ActionResult<PacienteInputDTO>> PostPaciente([FromBody] PacienteInputDTO paciente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool pacienteExiste = await _dataVerifier.PacienteExiste(paciente.DNI);
            if (pacienteExiste)
            {
                return BadRequest("El paciente ya existe.");
            }
            var nuevoPaciente = new Paciente
            {
                Nombre = paciente.Nombre,
                Apellido = paciente.Apellido,
                DNI = paciente.DNI,
                Contacto = paciente.Contacto,
                Correo = paciente.Correo
            };

            _db.Pacientes.Add(nuevoPaciente);
            await _db.SaveChangesAsync();

            return Ok($"Se ha agregado el registro '{paciente.Apellido}, {paciente.Nombre} (DNI: {paciente.DNI})' a la base de datos.");
        }

        // EDITAR PACIENTE

        [HttpPut("editar/{pacienteDNI}")]
        public async Task<ActionResult<EditarPacienteDTO>> PutPaciente(string pacienteDNI, [FromBody] EditarPacienteDTO pacienteEditado)
        {
            bool pacienteExiste = await _dataVerifier.PacienteExiste(pacienteDNI);
            if (!pacienteExiste)
            {
                return BadRequest("El paciente no existe.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var registro = await _dataVerifier.ObtenerPaciente(pacienteDNI);

            registro.Nombre = pacienteEditado.Nombre ?? registro.Nombre;
            registro.Apellido = pacienteEditado.Apellido ?? registro.Apellido;
            registro.DNI = pacienteEditado.DNI ?? registro.DNI;
            registro.Contacto = pacienteEditado.Contacto ?? registro.Contacto;
            registro.Correo = pacienteEditado.Correo ?? registro.Correo;

            await _db.SaveChangesAsync();
            return Ok($"Paciente DNI {registro.DNI} actualizado.");
        }

        // ELIMINAR UN PACIENTE

        [HttpDelete("eliminar/{pacienteDNI}")]
        public async Task<ActionResult> DeletePaciente(string pacienteDNI)
        {
            var pacienteExistente = await _dataVerifier.ObtenerPaciente(pacienteDNI);
            if(pacienteExistente is null)
            {
                return NotFound("El paciente no existe.");
            }
            _db.Pacientes.Remove(pacienteExistente);
            
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
