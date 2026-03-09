using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaMCP.Core.Entities
{
    public class Salarios
    {
        public int EmpleadoId { get; set; }
        public decimal SalarioBase { get; set; }
        public decimal Bonificaciones { get; set; }
        public decimal Deducciones { get; set; }
        public Decimal SalarioNetro { get; set; }  
        public decimal Moneda { get; set; }
        public DateTime FechaVigencia { get; set; }
        public DateTime FechaFin { get; set; }
        public string Activo { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
    }
}
