using EmpresaMCP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaMCP.Core.Interfaces
{
    public interface IAsistenciasRepository
    {
        Task<IEnumerable<Asistencias>> GetAllAsistemciasAsync();
        Task<Asistencias?> GetAsistemciaByIdAsync(int id);
        Task<IEnumerable<Asistencias>> GetAsistemciaByObsAsync(string name);
    }
}
