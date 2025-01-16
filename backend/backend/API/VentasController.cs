using backend.Domain;
using backend.Application;
using Microsoft.AspNetCore.Mvc;

namespace backend.API
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class VentasController : ControllerBase
    {

        private RegistrarVentaCommand _registrarVentaCommand;
        private RegistrarClienteCommand _registrarClienteCommand;
        private RegistrarCafeCommand _registrarCafeCommand;

        public VentasController(RegistrarVentaCommand registrarVentaCommand, RegistrarClienteCommand registrarClienteCommand, RegistrarCafeCommand registrarCafeCommand)
        {
            this._registrarVentaCommand = registrarVentaCommand;
            this._registrarClienteCommand = registrarClienteCommand;
            this._registrarCafeCommand = registrarCafeCommand;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> RegistrarVenta([FromBody] VentaModel venta)
        {
            if (venta != null)
            {
                int filas = 0;
                filas = this._registrarVentaCommand.RegistrarVenta(venta);
                if (filas > 0)
                {
                    return Ok();

                } else
                {
                    return BadRequest();
                }
            } else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<bool>> RegistrarCliente([FromBody] ClienteModel cliente)
        {
            if (cliente != null)
            {
                int filas = 0;
                filas = this._registrarClienteCommand.RegistrarCliente(cliente);
                if (filas > 0)
                {
                    return Ok();

                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<bool>> RegistrarCafe([FromBody] CafeModel cafe)
        {
            if (cafe != null)
            {
                int filas = 0;
                filas = this._registrarCafeCommand.RegistrarCafe(cafe);
                if (filas > 0)
                {
                    return Ok();

                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
