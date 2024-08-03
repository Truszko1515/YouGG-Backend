using Business_Logic_Layer.Dtos;
using Business_Logic_Layer.Interfaces;
using Data_Acces_Layer.Interfaces;
using Data_Acces_Layer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IConfiguration _configuration;
        private readonly IJwtProvider _jwtProvider;

        public AuthenticationService(IMemberRepository memberRepository,
                                     IConfiguration configuration,
                                     IJwtProvider jwtProvider)
        {
            _memberRepository = memberRepository;
            _configuration = configuration;
            _jwtProvider = jwtProvider;
        }
        public async Task<string> Authenticate(LoginDto userLogsIn)
        {
            var member = await _memberRepository.GetByEmailAsync(userLogsIn.Email);

            if (member == null || !VerifyPasswordHash(userLogsIn.Password, member.PasswordHash, member.PasswordSalt))
            {
                return null;
            }

            string token = await _jwtProvider.Generate(member);

            return token;
        }

        public async Task<bool> Register(RegisterDto userRegister)
        {
            if (!await _memberRepository.IsEmailUniqueAsync(userRegister.Email))
            {
                return false; // Username already exists
            }

            CreatePasswordHash(userRegister.Password, out string passwordHash, out string passwordSalt);

            var user = new User
            {
                UserId = Guid.NewGuid(),
                Username = userRegister.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Email = userRegister.Email
            };

            _memberRepository.AddUser(user);
            return true;
        }

        private void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = Convert.ToBase64String(hmac.Key);
                passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }

        private bool VerifyPasswordHash(string password, string storedHash, string storedSalt)
        {
            using (var hmac = new HMACSHA512(Convert.FromBase64String(storedSalt)))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return storedHash == Convert.ToBase64String(computedHash);
            }
        }
    }
}
