using EmpresaMCP.Core.Entities;
using EmpresaMCP.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmpresaMCP.Web.Controllers.API
{
        [Route("api/[controller]")]
        [ApiController]
    public class ContratosApiController : ControllerBase
    {
        private readonly IContratosRepository _repo;

        // Constructor con inyección de dependencias
        public ContratosApiController(IContratosRepository repo)
        {
            _repo = repo;
        }

        // GET: api/empleados/activos
        [HttpGet("activos")]
        public async Task<ActionResult<IEnumerable<Contratos>>> GetContratosActivos()
        {
            var contratos = await _repo.GetAllContratosAsync();
            //int contador = empleados.Count();

            return Ok(new
            {
                success = true,
                count = contratos.Count(),
                data = contratos
            });
        }

        // GET: api/empleados/buscar?termino=juan
        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Contratos>>> BuscarEmpleados(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                return BadRequest(new { success = false, message = "El término de búsqueda es requerido" });
            }

            var contratos = await _repo.GetContratoByNameAsync(nombre);

            return Ok(new
            {
                success = true,
                count = contratos.Count(),
                data = contratos
            });
        }

        // GET: api/empleados/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Contratos>> GetEmpleado(int id)
        {
            var contrato = await _repo.GetContratoByIdAsync(id);

            if (contrato == null)
            {
                return NotFound(new { success = false, message = "Contrato no encontrada." });
            }

            return Ok(new { success = true, data = contrato });
        }
    }
}
