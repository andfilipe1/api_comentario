using System;

namespace APIBlog.Models
{
    public class Foto
    {
        public int Id { get; set; }
        /// <summary>
        /// Conteúdo em String base64
        /// </summary>
        public String Conteudo { get; set; }
        public String Descricao { get; set; }
    }
}
