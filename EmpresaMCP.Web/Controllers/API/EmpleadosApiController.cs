using Microsoft.AspNetCore.Mvc;
using EmpresaMCP.Core.Entities;
using EmpresaMCP.Core.Interfaces;

namespace EmpresaMCP.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosApiController : ControllerBase
    {
        private readonly IEmpleadoRepository _repo;

        // Constructor con inyección de dependencias
        public EmpleadosApiController(IEmpleadoRepository repo)
        {
            _repo = repo;
        }

        // GET: api/empleados/activos
        [HttpGet("activos")]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleadosActivos()
        {
            var empleados = await _repo.GetAllEmpleadosAsync();
            //int contador = empleados.Count();

            return Ok(new
            {
                success = true,
                count = empleados.Count(),
                data = empleados
            });
        }

        // GET: api/empleados/buscar?termino=juan
        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Empleado>>> BuscarEmpleados(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                return BadRequest(new { success = false, message = "El término de búsqueda es requerido" });
            }

          var empleados = await _repo.GetEmployeByNameAsync(nombre);

            return Ok(new
            {
                success = true,
                count = empleados.Count(),
                data = empleados
            });
        }

        // GET: api/empleados/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> GetEmpleado(int id)
        {
            var empleado = await _repo.GetEmpleadoByIdAsync(id);

            if (empleado == null)
            {
                return NotFound(new { success = false, message = "Empleado no encontrado" });
            }

            return Ok(new { success = true, data = empleado });
        }
    }
}