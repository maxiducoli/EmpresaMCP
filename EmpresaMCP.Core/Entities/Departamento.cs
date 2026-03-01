namespace EmpresaMCP.Core.Entities
{
    public class Departamento
    {
        // Propiedad de clave primaria
        public int DepartamentoID { get; set; }

        // Propiedades básicas
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }

        // Clave foránea a Plantas
        public int? PlantaID { get; set; }

        // Propiedad de navegación (la veremos después)
        // public Planta? Planta { get; set; }

        // Propiedades económicas
        public decimal? PresupuestoAnual { get; set; }

        // Estado
        public bool? Activo { get; set; }

        // Auditoría
        public DateTime? FechaCreacion { get; set; }
    }
}