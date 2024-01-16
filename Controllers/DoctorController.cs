using MedicAppAPI.Data;
using MedicAppAPI.DTOs;
using MedicAppAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedicAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {

        private readonly MedicAppDb _db;

        public DoctorController(MedicAppDb db)
        {
            _db = db;
        }

        // GET ALL

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

        // GET BY SPECIALTY

        [HttpGet("filtrar-especialidad/{especialidad}")]
        public async Task<ActionResult<List<DoctorDTO>>> GetActionAsync(string especialidad)
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
            return Ok(new { Especialidad = especialidad, Doctores = doctores});
        }
    }
}
