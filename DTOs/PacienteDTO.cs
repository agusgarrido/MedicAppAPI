using System.ComponentModel.DataAnnotations;

namespace MedicAppAPI.DTOs
{
    public class PacienteDTO
    {
        public int PacienteID { get; set; }
        public string NombreCompleto { get; set; }
        //public string Apellido { get; set; }
        public string DNI { get; set; }
        public string Contacto { get; set; }
        public string? Correo { get; set; }
    }

    public class PacienteInputDTO
    {
        [Required(ErrorMessage = "El campo nombre es obligatorio.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo apellido es obligatorio.")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "El campo DNI es obligatorio.")]
        public string DNI { get; set; }
        [Required(ErrorMessage = "El campo contacto es obligatorio.")]
        public string Contacto { get; set; }
        public string? Correo { get; set; }
    }

        public class EditarPacienteDTO
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? DNI { get; set; }
        public string? Contacto { get; set; }
        public string? Correo { get; set; }
    }
}
