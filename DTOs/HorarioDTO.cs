using MedicAppAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicAppAPI.DTOs
{
    public class HorarioDTO
    {   
        public int DoctorID { get; set; }
        public string Doctor { get; set; }
        public List<DetalleHorarioDTO> Detalle { get; set; }

    }

    public class DetalleHorarioDTO
    {
        public string? Dia { get; set; }
        public string Inicio { get; set; }
        public string Fin { get; set; }
    }

    public class NuevoHorarioDTO
    {
        public string Dia { get; set; }
        public string Inicio { get; set; }
        public string Fin { get; set; }
    }

    public class ModificarHorarioDTO
    {
        public string? Inicio { get; set; }
        public string? Fin { get; set; }
    }
}
