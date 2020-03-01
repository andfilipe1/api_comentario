using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIBlog.Models
{
    public class RegistroUsuarioModel
    {
        public String Email { get; set; }
        public String Senha { get; set; }

        [Compare("Senha", ErrorMessage = "As senhas não conferem.")]
        public String ConfirmaSenha { get; set; }
        public String Nome { get; set; }
        public Boolean Confirmado { get; set; }

    }
}
