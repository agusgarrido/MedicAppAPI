using MedicAppAPI.Models;
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
        public string Nombre { get; set; }
        public string Apellido { get; set; }
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

        public string Nombre{ get; set; }
        public string Apellido { get; set; }
        public string Especialidad { get; set; }

    }
}
