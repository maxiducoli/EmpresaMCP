using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaMCP.Core.Entities
{
    public  class Asistencia
    {
        public int AsistenciaID { get; private set; }
        public int EmpleadoID { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan? HoraEntrada { get; set; }
        public TimeSpan? HoraSalida { get; set; }
        public string? Estado { get; set; }
        public string? Observaciones { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
