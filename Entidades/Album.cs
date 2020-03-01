using System;
using System.Collections.Generic;

namespace APIBlog.Models
{
    public class Album
    {
        public int Id { get; set; }
        public int Id_Dono { get; set; }
        public String Descricao { get; set; }
        public String Titulo { get; set; }
        public List<Postagem> Postagens { get; set; }
    }
}
