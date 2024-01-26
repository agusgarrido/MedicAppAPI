using MedicAppAPI.DTOs;
using MedicAppAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedicAppAPI.Interfaces
{
    public interface IEspecialidad
    {
        Task<IEnumerable<EspecialidadDTO>> ObtenerTodasAsync();
        Task<EspecialidadDTO> CrearEspecialidadAsync(EspecialidadInputDTO nuevaEspecialidad);
        Task<EspecialidadDTO> EditarEspecialidadAsync(int especialidadID,EspecialidadInputDTO especialidadActualizada);
        Task<bool> EliminarEspecialidadAsync(int especialidadID);
    }
}
