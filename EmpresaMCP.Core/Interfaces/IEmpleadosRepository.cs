using EmpresaMCP.Core.Entities;

namespace EmpresaMCP.Core.Interfaces
{
    public interface IEmpleadosRepository
    {
        Task<IEnumerable<Empleados>> GetAllEmpleadosAsync();
        Task<Empleados?> GetEmpleadoByIdAsync(int id);
        Task <IEnumerable<Empleados>> GetEmployeByNameAsync(string name);
    }
}
