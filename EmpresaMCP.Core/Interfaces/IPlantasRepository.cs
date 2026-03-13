using EmpresaMCP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaMCP.Core.Interfaces
{
    public interface IPlantasRepository
    {
        Task<IEnumerable<Plantas>> GetAllPlantasAsync();
        Task<Plantas?> GetPlantaByIdAsync(int id);
        Task<IEnumerable<Plantas>> GetPlantaByNameAsync(string name);
    }
}
