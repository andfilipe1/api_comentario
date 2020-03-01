using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using APIBlog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace APIBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public UserController(SignInManager<IdentityUser> singInManager, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _signInManager = singInManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("novo")]
        public async Task<ActionResult> Cadastrar(RegistroUsuarioModel usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
            }

            var novoUsuario = new IdentityUser
            {
                UserName = usuario.Nome,
                Email = usuario.Email,
                EmailConfirmed = usuario.Confirmado
            };

            var result = await _userManager.CreateAsync(novoUsuario, usuario.Senha);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _signInManager.SignInAsync(novoUsuario, false);

            return Ok(await GerarToken(usuario.Email));
        }

        [HttpPost("login")]
        public async Task<ActionResult> Entrar(LoginModel login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
            }

            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Senha, false, false);

            if (result.Succeeded)
            {
                return Ok(await GerarToken(login.Email));
            }
            else
            {
                return BadRequest("Falha no Login. Usuário ou Senha inválidos");
            }
        }

        private async Task<String> GerarToken(string email)
        {
            var usuario = await _userManager.FindByEmailAsync(email);

            var claims = new[] { new Claim(ClaimTypes.Name, usuario.UserName), new Claim(ClaimTypes.Email, usuario.Email) };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["sKey"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                 issuer: "APIBlog",
                 audience: "https://localhost",
                 claims: claims,
                 expires: DateTime.Now.AddMinutes(600),
                 signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}