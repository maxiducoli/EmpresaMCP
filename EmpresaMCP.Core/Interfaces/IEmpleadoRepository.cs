using EmpresaMCP.Core.Entities;

namespace EmpresaMCP.Core.Interfaces
{
    public interface IEmpleadoRepository
    {
        Task<IEnumerable<Empleado>> GetAllEmpleadosAsync();
        Task<Empleado?> GetEmpleadoByIdAsync(int id);
        Task <IEnumerable<Empleado>> GetEmployeByNameAsync(string name);
    }
}
