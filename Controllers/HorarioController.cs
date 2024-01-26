using MedicAppAPI.Data;
using MedicAppAPI.DTOs;
using MedicAppAPI.Interfaces;
using MedicAppAPI.Models;
using MedicAppAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Globalization;
using System.Security.Cryptography;

namespace MedicAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorarioController : ControllerBase
    {

        private readonly MedicAppDb _db;
        private readonly DataVerifier _dataVerifier;
        private readonly IHorario _horarioService;

        public HorarioController(MedicAppDb db, DataVerifier dataVerifier, IHorario horarioService)
        {
            _db = db;
            _dataVerifier = dataVerifier;
            _horarioService = horarioService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            return Ok(await _horarioService.ObtenerTodosAsync());
        }

        [HttpGet("{doctorID}")]
        public async Task<IActionResult> ObtenerHorarios(int doctorID)
        {
            var horarios = await _horarioService.ObtenerHorariosAsync(doctorID);
            if (horarios is null) return NotFound("El doctor no tiene horarios asignados");

            return Ok(horarios);
        }

        [HttpPost("agregar")]
        public async Task<IActionResult> CrearHorario(int doctorID, [FromBody] NuevoHorarioDTO nuevoHorario)
        {
            var nuevoRegistro = await _horarioService.CrearHorarioAsync(doctorID, nuevoHorario);
            if (nuevoRegistro is null) return NotFound("El doctor no existe.");

            return CreatedAtAction(nameof(ObtenerHorarios), new {doctorID = doctorID}, nuevoHorario);
        }
        
        [HttpPut("editar/{doctorID}/{dia}")]
        public async Task<IActionResult> EditarHorario(int doctorID, string dia, [FromBody] ModificarHorarioDTO nuevoHorario)
        {
            var registroActualizado = await _horarioService.EditarHorarioAsync(doctorID, dia, nuevoHorario);
            if (registroActualizado is null) return BadRequest("Error al actualizar horario. Corrobore que el doctor u el horario sean válidos.");
            return Ok(registroActualizado);
        }

        [HttpDelete("eliminar/{doctorID}/{dia}")]
        public async Task<IActionResult> EliminarHorario(int doctorID, string dia)
        {
            var eliminado = await _horarioService.EliminarHorarioAsync(doctorID, dia);
            if (!eliminado) return BadRequest("Error al eliminar. Corrobore que el doctor exista y el día se encuentre agendado.");

            return NoContent();
        }
    }
}
