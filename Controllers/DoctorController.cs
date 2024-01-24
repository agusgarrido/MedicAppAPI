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
using MedicAppAPI.Interfaces;

namespace MedicAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {

        private readonly MedicAppDb _db;
        private readonly DataVerifier _dataVerifier;
        private readonly IDoctor _doctorService;

        public DoctorController(MedicAppDb db, DataVerifier dataVerifier, IDoctor doctorService)
        {
            _db = db;
            _dataVerifier = dataVerifier;
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            return Ok(await _doctorService.ObtenerTodosAsync());
        }

        [HttpGet("{doctorID}")]
        public async Task<IActionResult> ObtenerDoctor(int doctorID)
        {

            var doctor = await _doctorService.ObtenerDoctorAsync(doctorID);
            
            if (doctor is null) return NotFound("El doctor no existe.");
            
            return Ok(doctor);
        }

        [HttpPost("agregar")]
        public async Task<IActionResult> CrearDoctor([FromBody] DoctorInputDTO nuevoDoctor)
        {
            
            var nuevoRegistro = await _doctorService.CrearDoctorAsync(nuevoDoctor);
            
            if (nuevoRegistro is null) return BadRequest("La especialidad no es válida o el doctor ya se encuentra registrado.");

            return CreatedAtAction(nameof(ObtenerDoctor), new { doctorID = nuevoRegistro.DoctorID }, nuevoRegistro);
        }

        [HttpPut("editar/{doctorID}")]
        public async Task<IActionResult> EditarDoctor(int doctorID, [FromBody] EditarDoctorDTO actualizacion)
        {
            var regitroEditado = await _doctorService.EditarDoctorAsync(doctorID, actualizacion);
            if (regitroEditado is null) return BadRequest("Error al actualizar: La especialidad no es válida o el doctor no existe.");

            return Ok(regitroEditado);
        }

        [HttpDelete("{doctorID}")]
        public async Task<IActionResult> EliminarDoctor(int doctorID)
        {
            var eliminado = await _doctorService.EliminarDoctorAsync(doctorID);
            if (!eliminado) return NotFound();

            return NoContent();
        }
    }
}
