using MedicAppAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MedicAppAPI.Interfaces
{
    public interface IHorario
    {
        Task<IEnumerable<HorarioDTO>> ObtenerTodosAsync();
        Task<IEnumerable<HorarioDTO>> ObtenerHorariosAsync(int doctorID);
        Task<DetalleHorarioDTO> CrearHorarioAsync(int doctorID, NuevoHorarioDTO nuevoHorario);
        Task<DetalleHorarioDTO> EditarHorarioAsync(int doctorID, string dia, ModificarHorarioDTO horarioActualizado);
        Task<bool> EliminarHorarioAsync(int doctorID, string dia);
    }
}
