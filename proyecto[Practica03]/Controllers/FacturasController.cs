using DataDLL.Domain;
using DataDLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using proyecto_Practica03_.Models;
using proyecto_Practica03_.Services;
using static System.Net.Mime.MediaTypeNames;

namespace proyecto_Practica03_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : Controller
    {
        private readonly IAplication aplication;
        public FacturasController()
        {
            aplication = new FacturaService();
        }

        [HttpGet]
        public IActionResult GetFacturas([FromQuery] DateTime? fecha, [FromQuery] string? nombre)
        {
            try
            {
                var facturas = aplication.GetFacturaFechaPago(fecha, nombre);

                if (facturas == null || !facturas.Any())
                {
                    return NotFound("No se encontraron facturas con los criterios proporcionados.");
                }

                return Ok(facturas);
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Error en los datos de entrada: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"Operación no válida: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }




        [HttpPost]
        public IActionResult Post(Facturas factura)
        {
            if (factura.Nro_factura != 0)
            {
                return BadRequest("El número de factura debe quedar en 0");
            }
            else if (factura.forma_pago.Id_pago == 0)
            {
                return BadRequest("Debe seleccionar una forma de pago (no debe quedar en 0)");
            }
            else if (!aplication.ExistFormaPago(factura.forma_pago.Id_pago))
            {
                return BadRequest("La forma de pago seleccionada no existe.");
            }


            try
            {
                if (aplication.AgregarFactura(factura))
                {
                    return Ok($"Factura ID: [{factura.Nro_factura}] - Fecha: [{factura.Fecha}] almacenada con exito, el tipo de forma de pago se extraerá en función del código ingresado");
                }
                else return StatusCode(500, "Error al almacenar factura");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error Interno: {ex}");
            }
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Facturas facturaActualizada)
        {
            if (facturaActualizada == null)
            {
                return BadRequest("Datos no válidos");
            }

            try
            {
                facturaActualizada.Nro_factura = id;

                if (aplication.ActualizarFactura(id, facturaActualizada))
                {
                    return Ok($"Factura con id [{id}] actualizada con éxito");
                }
                else
                {
                    return NotFound($"No existe factura con el id: [{id}]");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error Interno: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool result = aplication.BorrarFactura(id);
                if (result)
                {
                    return Ok($"Factura con id [{id}] borrada con éxito.");
                }
                else
                {
                    return NotFound($"No se encontró un  con el id: [{id}] para borrar.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error Interno: {ex.Message}");
            }
        }


    }
}
