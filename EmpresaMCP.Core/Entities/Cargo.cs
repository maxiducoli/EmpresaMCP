using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaMCP.Core.Entities
{
    public class Cargo
    {
        public int CargoID { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int NivelGerarquico { get; set; }
        public decimal SalarioMinimo { get; set; }
        public decimal SalarioMaximo { get; set; }
        public byte RequiereTitulo { get; set; }
        public byte Activo { get; set; }
    }
}
