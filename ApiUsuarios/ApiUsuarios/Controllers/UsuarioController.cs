using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiUsuarios.Context;
using ApiUsuarios.Models;
using UtilidadesGenerales.Utilidades;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiUsuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioContext _context;

        
        public UsuarioController(UsuarioContext context)
        {
            _context = context;
        }


        // GET: api/<UsuarioController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuario()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound();

            return usuario;
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            if (!ExisteUsuario(usuario.UsuarioID))
            { 
                usuario.Clave=Util.EncriptarClave(usuario.Clave);
                _context.Add(usuario);
                await _context.SaveChangesAsync();
            }
            return CreatedAtAction("GetUsuario", new { id = usuario.UsuarioID }, usuario);
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            string Clave=_context.Usuarios.Find(usuario.UsuarioID).Clave;
            //string ClaveNueva = usuario.Clave;

            if (id != usuario.UsuarioID)
            {
                return BadRequest();
            }

            if (Clave!= usuario.Clave)
            {
                usuario.Clave = Util.EncriptarClave(usuario.Clave);
            }
                

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                if (!ExisteUsuario(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        // GET api/<UsuarioController>/5
        [HttpGet("Iniciar")]
        public ActionResult<List<Usuario>> GetIniciarSession(string UserName,string Clave)
        {

            var ClaveEncript = Util.EncriptarClave(Clave);
            var usuario = _context.Usuarios.Where(u => u.UserName.Equals(UserName) && u.Clave.ToUpper().Equals(ClaveEncript.ToUpper())).ToList();
            if (usuario.Count() == 0)
                return NotFound();

            return usuario;
        }
        private bool ExisteUsuario(int id)
        {
            return _context.Usuarios.Any(x=>x.UsuarioID==id);
        }
    }
}
