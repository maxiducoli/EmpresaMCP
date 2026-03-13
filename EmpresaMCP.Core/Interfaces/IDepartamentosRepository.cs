using EmpresaMCP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaMCP.Core.Interfaces
{
   public  interface IDepartamentosRepository
    {
        Task<IEnumerable<Departamentos>> GetAllDepartamentosAsync();
        Task<Departamentos?> GetDepartamentoByIdAsync(int id);
        Task<IEnumerable<Departamentos>> GetDepartamentoByNameAsync(string name);
    }
}
