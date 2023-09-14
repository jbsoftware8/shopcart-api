using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using CommanApi.Interface;
using CommanApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;
using CommanApi.Repository;
using System.ComponentModel.Design;

namespace CommanApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILoginService _LoginService;
        private readonly IConfiguration _configuration;
        public AccountController(ILoginService LoginService, IConfiguration configuration)
        {
            _LoginService = LoginService;
            _configuration = configuration;
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginViewModel model)
        {
            string token = "";
            var std = await _LoginService.SignInAsync(HttpContext, model);
            if (std != null)
            {
                token = await CreateToken(std);
            }
            return Ok(new { data = std, token = token });
        }
        private async Task<string> CreateToken(RegistrationModel userDetail)
        {
            List<Claim> claims = new List<Claim>
            {

                new Claim("UserId", userDetail.ID.ToString()),
                new Claim("UserName", userDetail.UserName.ToString()),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["AppSettings:Token"]));
            var cruds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cruds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> Registration(RegistrationModel Master)
        {
            var data= await _LoginService.CreateUpdateAsync(Master, Master.ID > 0 ? 1 : 0);
            return Ok(new { data = data });
        }
        [HttpGet("GetAllUser/{id}")]
        public async Task<IActionResult> GetAllUser(int id)
        {
            var data = await _LoginService.GetAllUser(id);
            return Ok(data);
        }
    }
}
