using EmpresaMCP.Core.Entities;

namespace EmpresaMCP.Core.Interfaces
{
    public interface ICargosRepository
    {
        Task<IEnumerable<Cargos>> GetAllCargosAsync();
        Task<Cargos?> GetCargoByIdAsync(int id);
        Task<IEnumerable<Cargos>> GetCargoByNameAsync(string name);
    }
}
