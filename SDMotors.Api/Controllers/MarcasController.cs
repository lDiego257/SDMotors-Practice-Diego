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
    public class MarcasController : ControllerBase
    {
        private readonly SDMotorsApiContext _context;

        public MarcasController(SDMotorsApiContext context)
        {
            _context = context;
        }

        // GET: api/Marcas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Marca>>> GetMarcas()
        {
          if (_context.Marcas == null)
          {
              return NotFound();
          }
            return await _context.Marcas.ToListAsync();
        }

        // GET: api/Marcas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Marca>> GetMarca(int id)
        {
          if (_context.Marcas == null)
          {
              return NotFound();
          }
            var marca = await _context.Marcas.FindAsync(id);

            if (marca == null)
            {
                return NotFound();
            }

            return marca;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMarca(int id, Marca marca)
        {
            if (id != marca.MarcaId)
            {
                return BadRequest("El id de la entidad no puede ser modificado");
            }
            _context.Entry(marca).State = EntityState.Modified;
            _context.Entry<Marca>(marca).Property(x => x.FechaRegistro).IsModified = false; //Sirve para que no se modifique
                                                                                            //la fecha de registro de la marca

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarcaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Marca>> PostMarca(MarcaDto m1)
        {
            Marca marca = new Marca() {Nombre = m1.Nombre};
            _context.Marcas.Add(marca);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMarca", new { id = marca.MarcaId }, marca);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMarca(int id)
        {
            if (_context.Marcas == null)
            {
                return NotFound();
            }
            var marca = await _context.Marcas.FindAsync(id);
            if (marca == null)
            {
                return NotFound();
            }

            _context.Marcas.Remove(marca);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MarcaExists(int id)
        {
            return (_context.Marcas?.Any(e => e.MarcaId == id)).GetValueOrDefault();
        }
    }
}
