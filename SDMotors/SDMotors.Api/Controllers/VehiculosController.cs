using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SDMotors.Api.Data;
using SDMotors.Api.Models;



namespace SDMotors.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculosController : ControllerBase
    {
        private readonly SDMotorsApiContext _context;

        public VehiculosController(SDMotorsApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehiculo>>> GetVehiculos()
        {
          if (_context.Vehiculos == null)
          {
              return NotFound();
          }
          var vehiculos = await _context.Vehiculos.ToListAsync();
            foreach (var item in vehiculos)
            {
                item.StatusNombre = item.Status.ToString();
            }
          return Ok(vehiculos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vehiculo>> GetVehiculo(Guid id)
        {
          if (_context.Vehiculos == null)
          {
              return NotFound();
          }
            var vehiculo = await _context.Vehiculos.FindAsync(id);

            if (vehiculo == null)
            {
                return NotFound();
            }

            return vehiculo;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehiculo(Guid id, VehiculoDto v1)
        {
            Vehiculo vehiculo = new Vehiculo() { VehiculoId = id, Año = v1.Año, ImagenUrl = v1.ImagenUrl, MarcaId = v1.MarcaId, Modelo = v1.Modelo, Precio = v1.Precio, Status = v1.Status };
            _context.Entry(vehiculo).State = EntityState.Modified;
            _context.Entry<Vehiculo>(vehiculo).Property(x => x.FechaRegistro).IsModified = false; //Sirve para que no se modifique la fecha de registro
                                                                                           
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehiculoExists(id))
                {
                    return NotFound("Vehiculo no encontrado");
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }


        [HttpPost]
        public async Task<ActionResult<Vehiculo>> PostVehiculo(VehiculoDto v1)
        {
            var vehiculo = new Vehiculo() { Modelo = v1.Modelo, Año = v1.Año, ImagenUrl = v1.ImagenUrl, Precio = v1.Precio, Status = v1.Status.Value, MarcaId=v1.MarcaId};
            _context.Vehiculos.Add(vehiculo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehiculo", new { id = vehiculo.VehiculoId }, vehiculo);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehiculo(Guid id)
        {
            if (_context.Vehiculos == null)
            {
                return NotFound();
            }
            var vehiculo = await _context.Vehiculos.FindAsync(id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            _context.Vehiculos.Remove(vehiculo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehiculoExists(Guid id)
        {
            return (_context.Vehiculos?.Any(e => e.VehiculoId == id)).GetValueOrDefault();
        }
    }
}
