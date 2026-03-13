using EmpresaMCP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaMCP.Core.Interfaces
{
    public interface IContratosRepository
    {
            Task<IEnumerable<Contratos>> GetAllContratosAsync();
            Task<Contratos?> GetContratoByIdAsync(int id);
            Task<IEnumerable<Contratos>> GetContratoByNameAsync(string name);
    }
}
