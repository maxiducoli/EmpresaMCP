using Microsoft.AspNetCore.Mvc;
using EmpresaMCP.Core.Entities;
using EmpresaMCP.Core.Interfaces;

namespace EmpresaMCP.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsistenciasApiController : ControllerBase
    {
        private readonly IAsistenciasRepository _repo;

        // Constructor con inyección de dependencias
        public AsistenciasApiController(IAsistenciasRepository repo)
        {
            _repo = repo;
        }

        // GET: api/empleados/activos
        [HttpGet("activos")]
        public async Task<ActionResult<IEnumerable<Asistencias>>> GetEmpleadosActivos()
        {
            var aistencias = await _repo.GetAllAsistemciasAsync();
            //int contador = empleados.Count();

            return Ok(new
            {
                success = true,
                count = aistencias.Count(),
                data = aistencias
            });
        }

        // GET: api/empleados/buscar?termino=juan
        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Asistencias>>> BuscarEmpleados(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                return BadRequest(new { success = false, message = "El término de búsqueda es requerido" });
            }

            var aistencias = await _repo.GetAsistemciaByObsAsync(nombre);

            return Ok(new
            {
                success = true,
                count = aistencias.Count(),
                data = aistencias
            });
        }

        // GET: api/empleados/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Asistencias>> GetEmpleado(int id)
        {
            var aistencia = await _repo.GetAsistemciaByIdAsync(id);

            if (aistencia == null)
            {
                return NotFound(new { success = false, message = "Asistencia no encontrada." });
            }

            return Ok(new { success = true, data = aistencia });
        }
    }
}