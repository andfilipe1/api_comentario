using System.Collections.Generic;

namespace APIBlog.Models
{
    public class Postagem
    {
        public int Id { get; set; }
        public List<Comentario> Comentarios { get; set; }
        public List<Foto> Fotos { get; set; }
    }
}
