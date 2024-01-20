using MedicAppAPI.Data;
using MedicAppAPI.DTOs;
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

        public HorarioController(MedicAppDb db, DataVerifier dataVerifier)
        {
            _db = db;
            _dataVerifier = dataVerifier;
        }

       // OBTENER TODOS LOS HORARIOS
        
        [HttpGet]
        public async Task<ActionResult<List<HorarioDTO>>> GetAllAsync()
        {
            var horarios = await _db.Horarios
                .Include(h => h.Doctor)
                .GroupBy(h => new { h.DoctorID, h.Doctor.Nombre, h.Doctor.Apellido })
                .Select(group => new HorarioDTO
                {
                    Doctor = $"{group.Key.Apellido}, {group.Key.Nombre}",
                    Detalle = group
                    .OrderBy(h => h.Dia)
                    .Select(h => new DetalleHorarioDTO
                    {
                        Dia = h.Dia.ToString(),
                        Inicio = h.Inicio.ToString(@"hh\:mm"),
                        Fin = h.Fin.ToString(@"hh\:mm")
                    }).ToList()
                })
                .ToListAsync();

            return Ok(horarios);
        }

        // OBTENER LO HORARIOS DE UN MÉDICO

        [HttpGet("filtrar-doctor/{doctorID}")]
        public async Task<ActionResult<List<HorarioDTO>>> GetByDoctorAsync(int doctorID)
        {
            var doctor = await _db.Doctores.FindAsync(doctorID);
            if (doctor is null)
            {
                return NotFound("El Doctor no existe.");
            }
            var detalle = await _db.Horarios
                .Where(h => h.DoctorID == doctorID)
                .Include(h => h.Doctor)
                .Select(h => new DetalleHorarioDTO
                {
                    Dia = h.Dia.ToString(),
                    Inicio = h.Inicio.ToString(@"hh\:mm"),
                    Fin = h.Fin.ToString(@"hh\:mm")
                })
                .ToListAsync();

            return Ok(new HorarioDTO {
                Doctor = $"{doctor.Apellido},{doctor.Nombre}",
                Detalle = detalle
            });
        }

        // AGREGAR HORARIOS A UN MÉDICO

        [HttpPost("agregar/{doctorID}")]
        public async Task<ActionResult<NuevoHorarioDTO>> PostNuevoHorario(int doctorID, [FromBody] NuevoHorarioDTO horario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doctor = await _db.Doctores.FindAsync(doctorID);
            if (doctor is null)
            {
                return NotFound("El Doctor no existe");
            }

            int dia = (int)Enum.Parse(typeof(Dias), _dataVerifier.Capitalizar(horario.Dia));
            bool esDiaValido = _dataVerifier.EsDiaValido(dia);
            if (!esDiaValido)
            {
                return BadRequest("El día no es correcto.");
            }

            bool diaYaAgendado = await _dataVerifier.DiaYaAgendado(doctorID, dia);
            if (diaYaAgendado)
            {
                return BadRequest("El día ya se encuentra agendado. Por favor, modifíquelo.");
            }

            var nuevoHorario = new Horario
            {
                Dia = (Dias)dia,
                Inicio = TimeSpan.ParseExact(horario.Inicio, @"hh\:mm", CultureInfo.InvariantCulture),
                Fin = TimeSpan.ParseExact(horario.Fin, @"hh\:mm", CultureInfo.InvariantCulture),
                Duración = TimeSpan.FromMinutes(30)
            };

            _db.Horarios.Add(nuevoHorario);
            _db.SaveChangesAsync();

            return Ok(nuevoHorario);
        }

        // EDITAR DIA, HORARIO DE LLEGADA Y/U HORARIO DE FINALIZACIÓN DE UN MÉDICO

        [HttpPatch("editar/{doctorID}/{dia}")]
        public async Task<ActionResult<HorarioDTO>> PatchEditarHorario(int doctorID, string dia, [FromBody] ModificarHorarioDTO horario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doctor = await _db.Doctores.FindAsync(doctorID);
            if (doctor is null)
            {
                return NotFound("El Doctor no existe");
            }

            int diaAModificar = (int)Enum.Parse(typeof(Dias), _dataVerifier.Capitalizar(dia));

            bool esDiaValido = _dataVerifier.EsDiaValido(diaAModificar);
            if (!esDiaValido)
            {
                return BadRequest("El día no es correcto.");
            }

            bool diaYaAgendado = await _dataVerifier.DiaYaAgendado(doctorID, diaAModificar);
            if (!diaYaAgendado)
            {
                return NotFound("El día no está agendado.");
            }

            var registro = await _dataVerifier.ObtenerHorario(doctorID, diaAModificar);

            if (!string.IsNullOrEmpty(horario.Inicio) && TimeSpan.TryParseExact(horario.Inicio, @"hh\:mm", CultureInfo.InvariantCulture, out TimeSpan nuevoInicio))
            {
                registro.Inicio = nuevoInicio;
            }

            if (!string.IsNullOrEmpty(horario.Fin) && TimeSpan.TryParseExact(horario.Fin, @"hh\:mm", CultureInfo.InvariantCulture, out TimeSpan nuevoFin))
            {
                registro.Fin = nuevoFin;
            }

            await _db.SaveChangesAsync();

            return Ok(new DetalleHorarioDTO
            {
                Dia = registro.Dia.ToString(),
                Inicio = registro.Inicio.ToString(),
                Fin = registro.Fin.ToString()
            });
        }

        // ELIMINAR DÍA DE ATENCIÓN

        [HttpDelete("eliminar/{doctorID}/{dia}")]
        public async Task<ActionResult> DeleteHorario(int doctorID, string dia)
        {
            var doctor = await _db.Doctores.FindAsync(doctorID);
            if (doctor is null)
            {
                return NotFound("El Doctor no existe");
            }

            int diaAEliminar = (int)Enum.Parse(typeof(Dias), _dataVerifier.Capitalizar(dia));
            bool diaYaAgendado = await _dataVerifier.DiaYaAgendado(doctorID, diaAEliminar);
            if (!diaYaAgendado)
            {
                return BadRequest("El día no se encuentra agendado.");
            }

            var horarioExistente = await _dataVerifier.ObtenerHorario(doctorID, diaAEliminar);
            
            _db.Horarios.Remove(horarioExistente);
           
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
