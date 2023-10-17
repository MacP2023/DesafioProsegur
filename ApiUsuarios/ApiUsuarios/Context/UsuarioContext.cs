using ApiUsuarios.Models;
using Microsoft.EntityFrameworkCore;


namespace ApiUsuarios.Context
{
    public class UsuarioContext:DbContext
    
    {
        public UsuarioContext(DbContextOptions<UsuarioContext> options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }
    }

    
}
