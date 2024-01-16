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

    public class FiltroEspecialidadDTO
    {
        public int DoctorID { get; set; }
        public string NombreCompleto { get; set; }
    }
}
