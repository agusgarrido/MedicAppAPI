using MedicAppAPI.Data;
using MedicAppAPI.DTOs;
using MedicAppAPI.Interfaces;
using MedicAppAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicAppAPI.Services
{
    public class PacienteService: IPaciente
    {
        private readonly MedicAppDb _db;

        public PacienteService(MedicAppDb db)
        {
            _db = db;
        }

        public async Task<IEnumerable<PacienteDTO>> ObtenerTodosAsync()
        {
            var pacientes = await _db.Pacientes
                .Select(p => new PacienteDTO
                {
                    PacienteID = p.PacienteID,
                    NombreCompleto = $"{p.Apellido}, {p.Nombre}",
                    DNI = p.DNI,
                    Contacto = p.Contacto,
                    Correo = p.Correo

                })
                .ToListAsync();
            return pacientes;
        }

        public async Task<PacienteDTO> ObtenerPacienteAsync(int pacienteID)
        {
            var pacienteExistente = await ObtenerPaciente(pacienteID);
            if (pacienteExistente is null) return null;

            var pacienteDTO = new PacienteDTO
            {
                PacienteID = pacienteExistente.PacienteID,
                NombreCompleto = $"{pacienteExistente.Apellido}, {pacienteExistente.Nombre}",
                DNI = pacienteExistente.DNI,
                Contacto = pacienteExistente.Contacto,
                Correo = pacienteExistente.Correo

            };
            return pacienteDTO;
        }

        public async Task<PacienteDTO> CrearPacienteAsync(PacienteInputDTO nuevoPaciente)
        {
            bool pacienteExiste = await PacienteExiste(nuevoPaciente);
            if (pacienteExiste) return null;

            var nuevoRegistro = new Paciente
            {
                Nombre = nuevoPaciente.Nombre,
                Apellido = nuevoPaciente.Apellido,
                DNI = nuevoPaciente.DNI,
                Contacto = nuevoPaciente.Contacto,
                Correo = nuevoPaciente.Correo ?? "Sin especificar.",
            };

            _db.Pacientes.Add(nuevoRegistro);
            await _db.SaveChangesAsync();

            var pacienteDTO = new PacienteDTO
            {
                PacienteID = nuevoRegistro.PacienteID,
                NombreCompleto = $"{nuevoRegistro.Apellido}, {nuevoRegistro.Nombre}",
                DNI = nuevoRegistro.DNI,
                Contacto = nuevoRegistro.Contacto,
                Correo = nuevoRegistro.Correo

            };
            return pacienteDTO;
        }

        public async Task<PacienteDTO> EditarPacienteAsync(int pacienteID, EditarPacienteDTO pacienteActualizado)
        {
            var pacienteExistente = await ObtenerPaciente(pacienteID);
            if (pacienteExistente is null) return null;

            pacienteExistente.Nombre = pacienteActualizado.Nombre ?? pacienteExistente.Nombre;
            pacienteExistente.Apellido = pacienteActualizado.Apellido ?? pacienteExistente.Nombre;
            pacienteExistente.DNI = pacienteActualizado.DNI ?? pacienteExistente.DNI;
            pacienteExistente.Contacto = pacienteActualizado.Contacto ?? pacienteExistente.Contacto;
            pacienteExistente.Correo = pacienteActualizado.Correo ?? pacienteExistente.Correo;

            await _db.SaveChangesAsync();

            var pacienteDTO = new PacienteDTO
            {
                PacienteID = pacienteExistente.PacienteID,
                NombreCompleto = $"{pacienteExistente.Apellido}, {pacienteExistente.Nombre}",
                DNI = pacienteExistente.DNI,
                Contacto = pacienteExistente.Contacto,
                Correo = pacienteExistente.Correo
            };
            return pacienteDTO;
        }

        public async Task<bool> EliminarPacienteAsync(int pacienteID)
        {
            var paciente = await _db.Pacientes.FindAsync(pacienteID);
            if (paciente is null) return false;

            _db.Pacientes.Remove(paciente);
            await _db.SaveChangesAsync();
            
            return true;
        }

        //////////////////////////////////////////////////////////////////////////////////

        private async Task<bool> PacienteExiste(PacienteInputDTO paciente)
        {
            return await _db.Pacientes
                .AnyAsync(p =>
                p.DNI == paciente.DNI);
        }

        private async Task<Paciente> ObtenerPaciente(int pacienteID)
        {
            return await _db.Pacientes
                .SingleOrDefaultAsync(p => p.PacienteID == pacienteID);
        }
    }
}
