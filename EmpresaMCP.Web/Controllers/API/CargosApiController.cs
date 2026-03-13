using EmpresaMCP.Core.Entities;
using EmpresaMCP.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmpresaMCP.Web.Controllers.API
{
        [Route("api/[controller]")]
    [ApiController]
    public class CargosApiController : Controller
    {
        private readonly ICargosRepository _repo;

        // Constructor con inyección de dependencias
        public CargosApiController(ICargosRepository repo)
        {
            _repo = repo;
        }

        // GET: api/empleados/activos
        [HttpGet("activos")]
        public async Task<ActionResult<IEnumerable<Cargos>>> GetCargosActivos()
        {
            var cargos = await _repo.GetAllCargosAsync();
            //int contador = empleados.Count();

            return Ok(new
            {
                success = true,
                count = cargos.Count(),
                data = cargos
            });
        }

        // GET: api/empleados/buscar?termino=juan
        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Cargos>>> BuscarCargos(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                return BadRequest(new { success = false, message = "El término de búsqueda es requerido" });
            }

            var cargos = await _repo.GetCargoByNameAsync(nombre);

            return Ok(new
            {
                success = true,
                count = cargos.Count(),
                data = cargos
            });
        }

        // GET: api/empleados/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Cargos>> GetCargoById(int id)
        {
            var cargo = await _repo.GetCargoByIdAsync(id);

            if (cargo == null)
            {
                return NotFound(new { success = false, message = "Cargo no encontrado." });
            }

            return Ok(new { success = true, data = cargo });
        }
    }
}
