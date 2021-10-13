using HostelAPI.Core.Services.Abstraction;
using HostelAPI.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HostelAPI.Core.Services.Implementation
{
   public  class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _Configuration;
        private readonly UserManager<AppUser> _userManager;

        public TokenGenerator(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _Configuration = configuration;
            _userManager = userManager;
        }
        public async Task<string> GenerateToken(AppUser user)
        {
            var authuserclaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                authuserclaims.Add(new Claim(ClaimTypes.Role, role));
            }







            var SigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration["JWTSettings:SecretKey"]));

            var token = new JwtSecurityToken
                   (audience: _Configuration["JWTSettings:Audience"],
                       issuer: _Configuration["JWTSettings:Issuer"],
                       claims: authuserclaims,
                      expires: DateTime.Now.AddMinutes(5),
                      signingCredentials: new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256)
                 );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

