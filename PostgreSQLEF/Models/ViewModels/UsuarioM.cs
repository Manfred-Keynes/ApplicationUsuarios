using System.ComponentModel.DataAnnotations;

namespace PostgreSQLEF.Models.ViewModels
{
    public class UsuarioM
    {
        public int Id { get; set; }
       
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "El Apellido es obligatorio")]
        public string? Apellido { get; set; }
        [Required(ErrorMessage = "El Telefono es obligatorio")]
        public string? Telefono { get; set; }
        [Required(ErrorMessage = "El Puesto es obligatorio")]
        public string? Puesto { get; set; }
        [Required(ErrorMessage = "El Email es obligatorio")]
        public string? Email { get; set; }
        [StringLength(10, MinimumLength = 4, ErrorMessage = "La contraseña debe tener entre 4 y 10 caracteres")]
        public string? Contrasenia { get; set; }
        [Required(ErrorMessage = "EL campo Fecha de nacimietnto es obligatorio")]
        public DateOnly? FechaNacimiento { get; set; }

    }
}
