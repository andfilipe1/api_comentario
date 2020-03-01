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
    public class AlbumController : ControllerBase
    {
        public readonly APIBlogContext _context;

        public AlbumController(APIBlogContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost("cadastrar")]
        public async Task<ActionResult> CriarAlbum(Album album)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
            }

            try
            {
                _context.Albuns.Add(album);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.ToString());
            }

            return Ok();
        }

        [HttpGet("listar-albuns")]
        public async Task<ActionResult> ListarAlbuns()
        {
            try
            {
                return Ok(_context.Albuns.ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.ToString());
            }
        }
    }
}
