using MedicAppAPI.Data;
using MedicAppAPI.DTOs;
using MedicAppAPI.Interfaces;
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
        private readonly IPaciente _pacienteService;

        public PacienteController(MedicAppDb db, DataVerifier dataVerifier, IPaciente pacienteService)
        {
            _db = db;
            _dataVerifier = dataVerifier;
            _pacienteService = pacienteService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            return Ok(await _pacienteService.ObtenerTodosAsync());
        }

        [HttpGet("{pacienteID}")]
        public async Task<IActionResult> ObtenerPaciente(int pacienteID)
        {
            var paciente = await _pacienteService.ObtenerPacienteAsync(pacienteID);
            if (paciente is null) return NotFound("El paciente no existe.");
            return Ok(paciente);
        }

        [HttpPost("agregar")]
        public async Task<IActionResult> CrearPaciente([FromBody] PacienteInputDTO nuevoPaciente)
        {
            var nuevoRegistro = await _pacienteService.CrearPacienteAsync(nuevoPaciente);
            if (nuevoRegistro is null) return BadRequest("El paciente ya existe.");

            return CreatedAtAction(nameof(ObtenerPaciente), new {pacienteID = nuevoRegistro.PacienteID }, nuevoRegistro);
        }

        [HttpPut("editar/{pacienteID}")]
        public async Task<IActionResult> EditarPaciente(int pacienteID, [FromBody] EditarPacienteDTO pacienteActualizado)
        {
            var pacienteEditado = await _pacienteService.EditarPacienteAsync(pacienteID, pacienteActualizado);
            if (pacienteEditado is null) return NotFound("El paciente no existe.");

            return Ok(pacienteEditado);
        }

        [HttpDelete("eliminar/{pacienteID}")]
        public async Task<IActionResult> EliminarPaciente (int pacienteID)
        {
            var pacienteEliminado = await _pacienteService.EliminarPacienteAsync(pacienteID);
            if (!pacienteEliminado) return NotFound();
            
            return NoContent();
        }
    }
}
