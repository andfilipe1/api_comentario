using System;
using System.Linq;
using System.Threading.Tasks;
using APIBlog.AcessoPostgre;
using APIBlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        public readonly APIBlogContext _context;

        public PhotosController(APIBlogContext context)
        {
            _context = context;
        }


        [HttpGet("listar-fotos")]
        public ActionResult Get()
        {
            try
            {
                return Ok(new { Fotos = _context.Fotos.ToList() });
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.ToString());
            }

            
        }

        [Authorize]
        [HttpPost("incluir")]
        public async Task<ActionResult> IncluirFotos(FotoViewModel foto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
            }

            try
            {
                var album = _context.Albuns.Where(a => a.Id == foto.Id_Album).FirstOrDefault();
                Postagem novaPostagem = new Postagem();
                novaPostagem.Fotos.Add(foto.Foto);
                album.Postagens.Add(novaPostagem);

                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.ToString());
            }

            return Ok();

            
        }
    }
}
