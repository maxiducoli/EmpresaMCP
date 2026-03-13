using EmpresaMCP.Core.Entities;
using EmpresaMCP.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmpresaMCP.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalariosApiController : ControllerBase
    {
        private readonly ISalariosRepository _repo;

        // Constructor con inyección de dependencias
        public SalariosApiController(ISalariosRepository repo)
        {
            _repo = repo;
        }

        // GET: api/empleados/activos
        [HttpGet("activos")]
        public async Task<ActionResult<IEnumerable<Salarios>>> GetPlantasActivos()
        {
            var salarios = await _repo.GetAllSalariosAsync();
            //int contador = empleados.Count();

            return Ok(new
            {
                success = true,
                count = salarios.Count(),
                data = salarios
            });
        }

        // GET: api/empleados/buscar?termino=juan
        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Salarios>>> BuscarSalarios(decimal amount)
        {
            //if (amount <= 0)
            //{
            //    return BadRequest(new { success = false, message = "El término de búsqueda es requerido" });
            //}

            var salarios = await _repo.GetSalarioByAmountAsync(amount);

            return Ok(new
            {
                success = true,
                count = salarios.Count(),
                data = salarios
            });
        }

        // GET: api/empleados/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Salarios>> GetDepartamento(int id)
        {
            var salario = await _repo.GetSalarioByIdAsync(id);

            if (salario == null)
            {
                return NotFound(new { success = false, message = "Salario no encontrado." });
            }

            return Ok(new { success = true, data = salario });
        }
    }
}