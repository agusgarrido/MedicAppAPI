using System.ComponentModel.DataAnnotations.Schema;

namespace MedicAppAPI.Models
{

    public enum Dias
    {
        Lunes = 1,
        Martes = 2,
        Miércoles = 3,
        Jueves = 4,
        Viernes = 5,
        Sábado = 6,
        Domingo = 0
    }
    
    public class Horario
    {
        public int HorarioID { get; set; }
        [ForeignKey("Doctor")]
        public int DoctorID { get; set; }
        public Dias Dia { get; set; }
        public TimeSpan Inicio {  get; set; }
        public TimeSpan Fin { get; set; }
        public TimeSpan Duración { get; set; }
        public Doctor Doctor { get; set; }
    }
}
