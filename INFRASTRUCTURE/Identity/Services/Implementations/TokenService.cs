using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using INFRASTRUCTURE.Identity.Models;
using INFRASTRUCTURE.Identity.Options;
using INFRASTRUCTURE.Identity.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace INFRASTRUCTURE.Identity.Services.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<JwtConfig> _options;
        private readonly UserManager<ApplicationUser> _userManager;

        public TokenService(IOptions<JwtConfig> options, UserManager<ApplicationUser> userManager)
        {
            _options = options;
            _userManager = userManager;
        }

        public async Task<string> CreateTokeAsync(string mail)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_options.Value.Secret);
            var user = await _userManager.FindByEmailAsync(mail);
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, mail)
            };

            claims.AddRange(roles.Select(role =>
                new Claim(ClaimTypes.Role, role)));

            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = DateTime.UtcNow.AddDays(_options.Value.Expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}