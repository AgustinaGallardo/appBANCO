using BackBanco.Dominio;
using BancoBack.Dominio;
using BancoBack.Negocio.Implementacion;
using BancoBack.Negocio.Interfaz;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiBanco.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BancoController : ControllerBase
    {

        private IAplicacion app;
        public BancoController()
        {
            app = new Aplicacion();
        }

        [HttpGet("/tipocuenta")]
        public IActionResult ObtenerCuentas()
        {
            return Ok(app.ObtenerTipos());
        }

        [HttpGet("/clientesCuentas")]
        public IActionResult ObtenerClientesCuentas(int id)
        {
            return Ok(app.ObtenerClientesCuentas(id));
        }

        [HttpGet("/clientes")]
        public IActionResult ObtenerClientes()
        {
            return Ok(app.ObtenerCliente());
        }

        [HttpPost("/cliente")]
        public IActionResult PostCliente(Cliente oCliente)
        {
            try
            {
                if (oCliente == null)
                {
                    return BadRequest();
                }
                return Ok(app.Save(oCliente));
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Error interno, intente luego");
            }
            
        }

        [HttpPost("/login")]
        public IActionResult PostLogin(Login Log)
        {
            try
            {
                if (Log == null)
                {
                    return BadRequest();
                }
                return Ok(app.Login(Log));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno, intente luego");
            }

        }

        [HttpPost("/alta/tipoCuenta")]
        public IActionResult PostTipoCuenta(TipoCuenta oTipo)
        {
            try
            {
                if (oTipo == null)
                {
                    return BadRequest();
                }
                return Ok(app.SaveTipoCuenta(oTipo));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno, intente luego");
            }

        }

        [HttpPut("/tipocuenta")]
        public IActionResult PutTipoCuenta(TipoCuenta tipo)
        {
            try
            {
                if (tipo == null)
                {
                    return BadRequest();
                }
                return Ok(app.Modificar(tipo));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno, intente luego");
            }           
          }

        [HttpPut("{id}")]
        public IActionResult PutTipoCuentaBaja(int id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }
                return Ok(app.Baja(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno, intente luego");
            }

        }
        




    }
}
