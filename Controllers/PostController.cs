using APIBlog.AcessoPostgre;
using APIBlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace APIBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        public readonly APIBlogContext _context;

        public PostController(APIBlogContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost("cadastrar")]
        public async Task<ActionResult> CadastrarPostagem(Postagem post)
        {
            if (!ModelState.IsValid)
            {
                return  BadRequest(ModelState.Values.SelectMany(v => v.Errors));
            }

            try
            {
                _context.Postagens.Add(post);
                _context.SaveChanges();
            }
            catch(Exception e)
            {
                return BadRequest(e.InnerException.ToString());
            }

            return Ok();
        }

        [HttpGet("listar-postagens")]
        public async Task<ActionResult> ListarPostagens()
        {
            try
            {
                return Ok(_context.Postagens.ToList());
            }catch(Exception e)
            {
                return BadRequest(e.InnerException.ToString());
            }
        }
    }
}
