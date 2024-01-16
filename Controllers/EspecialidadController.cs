using MedicAppAPI.Data;
using MedicAppAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedicAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadController : ControllerBase
    {

        private readonly MedicAppDb _db;

        public EspecialidadController(MedicAppDb db)
        {
            _db = db;
        }

        // GET ALL

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
    }
}
