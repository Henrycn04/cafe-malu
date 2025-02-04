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
        private ObtenerCafesQuery _obtenerCafesQuery;
        private ObtenerClientesQuery _obtenerClientesQuery;
        private ObtenerVentasQuery _obtenerVentasQuery;

        public VentasController(RegistrarVentaCommand registrarVentaCommand, RegistrarClienteCommand registrarClienteCommand, RegistrarCafeCommand registrarCafeCommand, ObtenerClientesQuery obtenerClientesQuery, ObtenerCafesQuery obtenerCafesQuery, ObtenerVentasQuery obtenerVentasQuery)
        {
            this._registrarVentaCommand = registrarVentaCommand;
            this._registrarClienteCommand = registrarClienteCommand;
            this._registrarCafeCommand = registrarCafeCommand;
            this._obtenerCafesQuery = obtenerCafesQuery;
            this._obtenerClientesQuery = obtenerClientesQuery;
            this._obtenerVentasQuery = obtenerVentasQuery;
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

        [HttpGet]
        public async Task<ActionResult<ListaClientesModel>> ObtenerClientes()
        {
            ListaClientesModel clientes = new ListaClientesModel();
            clientes = this._obtenerClientesQuery.ObtenerClientes();
            return Ok(clientes);
        }

        [HttpGet]
        public async Task<ActionResult<ListaCafesModel>> ObtenerCafes()
        {
            ListaCafesModel cafes = new ListaCafesModel();
            cafes = this._obtenerCafesQuery.ObtenerCafes();
            return Ok(cafes);
        }

        [HttpGet]
        public async Task<ActionResult<ListaVentasModel>> ObtenerVentas()
        {
            ListaVentasModel ventas = new ListaVentasModel();
            ventas = this._obtenerVentasQuery.ObtenerVentas();
            return Ok(ventas);
        }
    }
}
