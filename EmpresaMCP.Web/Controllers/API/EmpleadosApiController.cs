using Microsoft.AspNetCore.Mvc;
using EmpresaMCP.Core.Data;
using EmpresaMCP.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmpresaMCP.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosApiController : ControllerBase
    {
        private readonly EmpresaDbContext _context;

        // Constructor con inyección de dependencias
        public EmpleadosApiController(EmpresaDbContext context)
        {
            _context = context;
        }

        // GET: api/empleados/activos
        [HttpGet("activos")]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleadosActivos()
        {
            var empleados = await _context.Empleados
                .Where(e => e.Activo == true)
                .ToListAsync();

            return Ok(new
            {
                success = true,
                count = empleados.Count,
                data = empleados
            });
        }

        // GET: api/empleados/buscar?termino=juan
        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Empleado>>> BuscarEmpleados(string termino)
        {
            if (string.IsNullOrEmpty(termino))
            {
                return BadRequest(new { success = false, message = "El término de búsqueda es requerido" });
            }

            var empleados = await _context.Empleados
                .Where(e => e.Activo == true &&
                      (e.Nombre.Contains(termino) || e.Apellido.Contains(termino)))
                .ToListAsync();

            return Ok(new
            {
                success = true,
                count = empleados.Count,
                data = empleados
            });
        }

        // GET: api/empleados/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> GetEmpleado(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);

            if (empleado == null)
            {
                return NotFound(new { success = false, message = "Empleado no encontrado" });
            }

            return Ok(new { success = true, data = empleado });
        }
    }
}