using Microsoft.AspNetCore.Mvc;
using EmpresaMCP.Core.Entities;
using EmpresaMCP.Core.Interfaces;

namespace EmpresaMCP.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectoresApiController : ControllerBase
    {
        private readonly ISectoresRepository _repo;

        // Constructor con inyección de dependencias
        public SectoresApiController(ISectoresRepository repo)
        {
            _repo = repo;
        }

        // GET: api/empleados/activos
        [HttpGet("activos")]
        public async Task<ActionResult<IEnumerable<Sectores>>> GetSectoresActivos()
        {
            var sectores = await _repo.GetAllSectorsAsync();
            //int contador = empleados.Count();

            return Ok(new
            {
                success = true,
                count = sectores.Count(),
                data = sectores
            });
        }

        // GET: api/empleados/buscar?termino=juan
        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Sectores>>> BuscarSectores(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                return BadRequest(new { success = false, message = "El término de búsqueda es requerido" });
            }

            var sectores = await _repo.GetSectorByNameAsync(nombre);

            return Ok(new
            {
                success = true,
                count = sectores.Count(),
                data = sectores
            });
        }

        // GET: api/empleados/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Sectores>> GetSector(int id)
        {
            var sector = await _repo.GetSectorByIdAsync(id);

            if (sector == null)
            {
                return NotFound(new { success = false, message = "Sector no encontrado" });
            }

            return Ok(new { success = true, data = sector });
        }
    }
}