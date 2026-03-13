using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaMCP.Core.Entities
{
    public class Contratos
    {
        public int ContratoID { get; set; }
        public int EmpleadoID { get; set; }
        public string? TipoContrato { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? HorasSemanales { get; set; }
        public string? ModalidadTrabajo { get; set; } = string.Empty;
        public int? PeriodoPruebaMeses { get; set; }
        public string? Observaciones { get; set; } = string.Empty;
        public bool? Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
