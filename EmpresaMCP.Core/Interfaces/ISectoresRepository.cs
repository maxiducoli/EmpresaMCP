using EmpresaMCP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaMCP.Core.Interfaces
{
    public interface ISectoresRepository
    {
        Task<IEnumerable<Sectores>> GetAllSectorsAsync();
        Task<Sectores?> GetSectorByIdAsync(int id);
        Task<IEnumerable<Sectores>> GetSectorByNameAsync(string name);
    }
}
