using APIBlog.AcessoPostgre;
using APIBlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        public readonly APIBlogContext _context;

        public CommentController(APIBlogContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost("cadastrar")]
        public async Task<ActionResult> ComentarPost(ComentarioViewModel comentario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
            }

            try
            {
                var postagem = _context.Postagens.Where(p => p.Id == comentario.IdPostagem).FirstOrDefault();
                postagem.Comentarios.Add(comentario.Comentario);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.ToString());
            }

            return Ok();
        }

        [HttpGet("listar-comentarios")]
        public async Task<ActionResult> ListarComentarios()
        {
            try
            {
                return Ok(_context.Comentarios.ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.ToString());
            }
        }

    }
}
