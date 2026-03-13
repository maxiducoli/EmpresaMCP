using EmpresaMCP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaMCP.Core.Interfaces
{
    public interface ISalariosRepository
    {
        Task<IEnumerable<Salarios>> GetAllSalariosAsync();
        Task<Salarios?> GetSalarioByIdAsync(int id);
        Task<IEnumerable<Salarios>> GetSalarioByAmountAsync(decimal amount);
    }
}
