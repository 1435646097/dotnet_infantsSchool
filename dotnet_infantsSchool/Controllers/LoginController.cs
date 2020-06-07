using IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model.Dtos;
using Model.Entitys;
using Model.Helper;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_infantsSchool.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IConfiguration _configuration;

        public LoginController(IUserServices userServices, IConfiguration configuration)
        {
            _userServices = userServices;
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<ActionResult<MessageModel<string>>> Login(LoginDto loginDto)
        {
            MessageModel<string> res = new MessageModel<string>();
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
            User user = await _userServices.GetEntitys().Where(u => u.Account == loginDto.Account && u.Pwd == loginDto.Pwd).FirstOrDefaultAsync();
            if (user == null)
            {
                res.Code = 404;
                res.Success = false;
                res.Msg = "账户或密码错！";
                return Ok(res);
            }
            string userRoles = user.UserRole.Select(u => u.Role.Name).Join(",");
            string token = jwtHandler.WriteToken(new JwtSecurityToken
                (issuer: _configuration["Authentication:Issuer"],
                 audience: _configuration["Authentication:Audience"],
                 claims: new Claim[]
                 {
                     new Claim("id",user.Id.ToString()),
                     new Claim(ClaimTypes.Name,user.Name),
                     new Claim(ClaimTypes.Role,userRoles),
                 },
                 expires: DateTime.Now.AddDays(7),
                 signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SigningKey"])), SecurityAlgorithms.HmacSha256)
                 ));
            res.Data = "Bearer " + token;
            return Ok(res);
        }
    }
}
