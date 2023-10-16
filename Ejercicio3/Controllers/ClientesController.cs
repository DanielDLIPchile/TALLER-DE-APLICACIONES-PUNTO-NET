using Ejercicio3.Modelos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ejercicio3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        public List<Clientes> _clientes = new List<Clientes>
        {
            new Clientes {id=1,nombre="hasmter",edad=6},
            new Clientes {id=2,nombre="rata",edad=16},
            new Clientes {id=3,nombre="almeja",edad=26}
        };


        // GET: api/<ClientesController>
        [HttpGet]
        public ActionResult<IEnumerable<Clientes>> Get()
        {
            return _clientes;
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            var cliente = _clientes.FirstOrDefault(c => c.id == id);
            if (cliente == null)
            {
                return NotFound(); // Devolver un resultado 404 Not Found si no se encuentra el cliente.
            }

            return cliente.nombre; // Devolver el nombre del cliente con el ID especificado.
        }

        [HttpPost]
        public ActionResult<string> Post([FromBody] Clientes nuevoCliente)
        {
            // Asigna un nuevo ID para el nuevo cliente (esto puede variar según tu lógica).
            int nuevoId = _clientes.Max(c => c.id) + 1;
            nuevoCliente.id = nuevoId;

            _clientes.Add(nuevoCliente);

            return CreatedAtAction("Get", new { id = nuevoId }, nuevoCliente);
        }

        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            var cliente = _clientes.FirstOrDefault(c => c.id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            _clientes.Remove(cliente);
            return NoContent(); // Devuelve una respuesta sin contenido (204) después de la eliminación.
        }

        [HttpPatch("{id}")]
        public ActionResult<string> Patch(int id, [FromBody] Clientes clienteActualizado)
        {
            var cliente = _clientes.FirstOrDefault(c => c.id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            // Actualiza los datos del cliente con los valores proporcionados en clienteActualizado
            cliente.nombre = clienteActualizado.nombre;
            cliente.edad = clienteActualizado.edad;

            return Ok(cliente); // Devuelve una respuesta exitosa con el cliente actualizado.
        }
    }
}
