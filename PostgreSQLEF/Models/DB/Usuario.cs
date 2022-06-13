using System;
using System.Collections.Generic;

namespace PostgreSQLEF.Models.DB
{
    public partial class Usuario
    {
        public long Id { get; set; }
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public string? Puesto { get; set; }
        public string? Email { get; set; }
        public string? Contrasenia { get; set; }
        public string? Usuario1 { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateOnly? FechaNacimiento { get; set; }
        public string? Apellido { get; set; }
    }
}
