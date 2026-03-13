using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaMCP.Core.Entities
{
    public class Sectores
    {
        public int SectorID { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; } = string.Empty;
        public int? DepartamentoID { get; set; }
        public int? JefeSectorID { get; set; }
        public bool? Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
