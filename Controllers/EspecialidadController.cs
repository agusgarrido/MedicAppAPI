using MedicAppAPI.Data;
using MedicAppAPI.DTOs;
using MedicAppAPI.Interfaces;
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
        private readonly IEspecialidad _especialidadService;

        public EspecialidadController(MedicAppDb db, DataVerifier dataVerifier, IEspecialidad especialidadService)
        {
            _db = db;
            _dataVerifier = dataVerifier;
            _especialidadService = especialidadService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            return Ok( await _especialidadService.ObtenerTodasAsync());
        }

        [HttpPost("agregar")]
        public async Task<IActionResult> CrearEspecialidad([FromBody] EspecialidadInputDTO nuevaEspecialidad)
        {
            var nuevoRegistro = await _especialidadService.CrearEspecialidadAsync(nuevaEspecialidad);
            if (nuevoRegistro is null) return BadRequest("La especialidad ya existe.");

            return Ok(nuevoRegistro);
        }

        [HttpPut("editar/{especialidadID}")]
        public async Task<IActionResult> EditarEspecialidad(int especialidadID, [FromBody] EspecialidadInputDTO especialidadActualizada)
        {
            var registroEditado = await _especialidadService.EditarEspecialidadAsync(especialidadID, especialidadActualizada);
            if (registroEditado is null) return BadRequest("La especialidad no existe o el valor proporcionado es nulo.");

            return Ok(registroEditado);
        }

        [HttpDelete("eliminar/{especialidadID}")]
        public async Task<IActionResult> EliminarEspecialidad(int especialidadID)
        {
            var eliminado = await _especialidadService.EliminarEspecialidadAsync(especialidadID);
            if (!eliminado) return NotFound();
            return NoContent();
        }
    }
}
