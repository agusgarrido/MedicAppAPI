namespace MedicAppAPI.Models
{
    public class Paciente
    {
        public int PacienteID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public string Contacto { get; set; }
        public string? Correo {  get; set; }
    }
}
