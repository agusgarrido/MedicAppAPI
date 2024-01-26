using MedicAppAPI.DTOs;

namespace MedicAppAPI.Interfaces
{
    public interface IPaciente
    {
        Task<IEnumerable<PacienteDTO>> ObtenerTodosAsync();
        Task<PacienteDTO> ObtenerPacienteAsync(int pacienteID);
        Task<PacienteDTO> CrearPacienteAsync(PacienteInputDTO nuevoPaciente);
        Task<PacienteDTO> EditarPacienteAsync(int pacienteID, EditarPacienteDTO pacienteActualizado);
        Task<bool> EliminarPacienteAsync(int pacienteID);
    }
}
