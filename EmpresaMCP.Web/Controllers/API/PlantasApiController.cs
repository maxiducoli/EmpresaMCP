using EmpresaMCP.Core.Entities;
using EmpresaMCP.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmpresaMCP.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantasApiController : ControllerBase
    {
        private readonly IPlantasRepository _repo;

        // Constructor con inyección de dependencias
        public PlantasApiController(IPlantasRepository repo)
        {
            _repo = repo;
        }

        // GET: api/empleados/activos
        [HttpGet("activos")]
        public async Task<ActionResult<IEnumerable<Plantas>>> GetPlantasActivos()
        {
            var plantas = await _repo.GetAllPlantasAsync();
            //int contador = empleados.Count();

            return Ok(new
            {
                success = true,
                count = plantas.Count(),
                data = plantas
            });
        }

        // GET: api/empleados/buscar?termino=juan
        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Plantas>>> BuscarDepartamentos(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                return BadRequest(new { success = false, message = "El término de búsqueda es requerido" });
            }

            var plantas = await _repo.GetPlantaByNameAsync(nombre);

            return Ok(new
            {
                success = true,
                count = plantas.Count(),
                data = plantas
            });
        }

        // GET: api/empleados/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Plantas>> GetDepartamento(int id)
        {
            var planta = await _repo.GetPlantaByIdAsync(id);

            if (planta == null)
            {
                return NotFound(new { success = false, message = "Planta no encontrada." });
            }

            return Ok(new { success = true, data = planta });
        }
    }
}