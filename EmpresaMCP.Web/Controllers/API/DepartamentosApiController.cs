using EmpresaMCP.Core.Entities;
using EmpresaMCP.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmpresaMCP.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentosApiController : ControllerBase
    {
        private readonly IDepartamentosRepository _repo;

        // Constructor con inyección de dependencias
        public DepartamentosApiController(IDepartamentosRepository repo)
        {
            _repo = repo;
        }

        // GET: api/empleados/activos
        [HttpGet("activos")]
        public async Task<ActionResult<IEnumerable<Departamentos>>> GetContratosActivos()
        {
            var departamentos  = await _repo.GetAllDepartamentosAsync();
            //int contador = empleados.Count();

            return Ok(new
            {
                success = true,
                count = departamentos.Count(),
                data = departamentos
            });
        }

        // GET: api/empleados/buscar?termino=juan
        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Departamentos>>> BuscarDepartamentos(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                return BadRequest(new { success = false, message = "El término de búsqueda es requerido" });
            }

            var departamentos = await _repo.GetDepartamentoByNameAsync(nombre);

            return Ok(new
            {
                success = true,
                count = departamentos.Count(),
                data = departamentos
            });
        }

        // GET: api/empleados/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Departamentos>> GetDepartamento(int id)
        {
            var departamento = await _repo.GetDepartamentoByIdAsync(id);

            if (departamento == null)
            {
                return NotFound(new { success = false, message = "Departamento no encontrado." });
            }

            return Ok(new { success = true, data = departamento });
        }
    }
}
