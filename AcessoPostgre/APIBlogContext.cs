
using APIBlog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APIBlog.AcessoPostgre
{
    public class APIBlogContext : IdentityDbContext
    {
        public APIBlogContext(DbContextOptions<APIBlogContext> options) : base(options) { }

        public DbSet<Foto> Fotos { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Album> Albuns { get; set; }
        public DbSet<Postagem> Postagens { get; set; }
    }
}
