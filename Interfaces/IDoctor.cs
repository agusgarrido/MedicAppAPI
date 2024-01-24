using MedicAppAPI.DTOs;
using MedicAppAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedicAppAPI.Interfaces
{
    public interface IDoctor
    {
        Task<IEnumerable<DoctorDTO>> ObtenerTodosAsync();
        Task<DoctorDTO> ObtenerDoctorAsync(int doctorID);
        Task<DoctorDTO> CrearDoctorAsync([FromBody] DoctorInputDTO nuevoDoctor);
        Task<DoctorDTO> EditarDoctorAsync(int doctorID, [FromBody] EditarDoctorDTO doctorActualizado);
        Task<bool> EliminarDoctorAsync(int doctorID);
    }
}
