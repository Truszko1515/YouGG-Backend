using Business_Logic_Layer.Interfaces;
using Data_Acces_Layer.Interfaces;
using Data_Acces_Layer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Authentication
{
    public sealed class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;
        private readonly IMemberRepository _memberRepository;
        public JwtProvider(IOptions<JwtOptions> options,
                           IMemberRepository memberRepository)
        {
            _options = options.Value;
            _memberRepository = memberRepository;
        }

        public async Task<string> Generate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("name", user.Username),
                new Claim("email", user.Email),
            };

            var roles = await _memberRepository.GetUserRolesAsync(user.UserId);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _options.Issuer,
                _options.Audience,
                claims,
                null,
                DateTime.UtcNow.AddMinutes(30),
                creds);

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
