using AutenticacaoJWT.Models;
using Microsoft.EntityFrameworkCore;

namespace AutenticacaoJWT.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {    
        }

        public DbSet<UsuarioModel> Usuarios { get; set; }

    }
}
