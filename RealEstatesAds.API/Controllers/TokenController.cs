using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RealEstateAds.Data.Contracts;
using RealEstatesAds.API.Models.UserModel;

namespace RealEstatesAds.API.Controllers
{
    [Route("token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IUserRepo _realEstateRepo;
        private readonly IConfiguration _configuration;

        public TokenController(IUserRepo realEstateRepo, IConfiguration configuration)
        {
            this._realEstateRepo = realEstateRepo;
            this._configuration = configuration;
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody] UserCred user)
        {
            var validUser = _realEstateRepo.ValidUser(user.username, user.password);
            if (!validUser) return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.username)
                }),
                IssuedAt = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new
            {
                access_token = new JwtSecurityTokenHandler().WriteToken(token),
                userName = user.username,
                issued = token.ValidFrom,
                expires = token.ValidTo
            });
        }
    }
}
