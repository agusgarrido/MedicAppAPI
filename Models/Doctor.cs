using System.ComponentModel.DataAnnotations.Schema;

namespace MedicAppAPI.Models
{
    public class Doctor
    {

        public int DoctorID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        [ForeignKey("Especialidad")]
        public int EspecialidadID { get; set; }
        public Especialidad Especialidad {  get; set; }

    }
}
