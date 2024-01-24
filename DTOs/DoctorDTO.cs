using MedicAppAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicAppAPI.DTOs
{
    public class DoctorDTO
    {

        public int DoctorID { get; set; }
        public string NombreCompleto { get; set; }
        public string Especialidad { get; set; }

    }

    public class EditarDoctorDTO
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Especialidad { get; set; }
    }

    public class EditarEspecialidadDTO
    {
        public string Especialidad { get; set; }
    }

    public class FiltroEspecialidadDTO
    {
        public int DoctorID { get; set; }
        public string NombreCompleto { get; set; }
    }

    public class DoctorInputDTO
    {
        [Required(ErrorMessage = "El campo nombre es obligatorio.")]
        public string Nombre{ get; set; }
        [Required(ErrorMessage = "El campo apellido es obligatorio.")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "El campo especialidad es obligatorio.")]
        public string Especialidad { get; set; }

    }
}
