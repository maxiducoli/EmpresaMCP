namespace EmpresaMCP.Core.Entities
{
    public class Empleados
    {
        // Propiedad de clave primaria
        public int EmpleadoID { get; set; }

        // Datos básicos
        public string Legajo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;

        // Datos de contacto
        public string? Email { get; set; }
        public string? Telefono { get; set; }

        // Datos personales
        public DateTime? FechaNacimiento { get; set; }
        public string? DNI { get; set; }
        public string? Direccion { get; set; }
        public string? Ciudad { get; set; }
        public string? Provincia { get; set; }
        public string? CodigoPostal { get; set; }

        // Claves foráneas (relaciones)
        public int? CargoID { get; set; }
        public int? SectorID { get; set; }
        public int? DepartamentoID { get; set; }
        public int? PlantaID { get; set; }
        public int? JefeDirectoID { get; set; }

        // Propiedades de navegación (las configuraremos después en el DbContext)
        // public Departamento? Departamento { get; set; }
        // public Cargo? Cargo { get; set; }
        // public Sector? Sector { get; set; }
        // public Planta? Planta { get; set; }
        // public Empleado? JefeDirecto { get; set; }

        // Fechas laborales
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaEgreso { get; set; }

        // Estado
        public bool? Activo { get; set; }

        // Auditoría
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? Sexo { get; set; }
    }
}