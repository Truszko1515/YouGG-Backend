using Business_Logic_Layer.Dtos;
using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Validation;
using Data_Acces_Layer.Interfaces;
using Data_Acces_Layer.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public async Task<string> Authenticate(LoginDto loginCreds)
        {
            var member = await _memberRepository.GetByEmailAsync(loginCreds.Email);

            if (member == null || !VerifyPasswordHash(loginCreds.Password, member.PasswordHash, member.PasswordSalt))
            {
                return null;
            }

            string token = await _jwtProvider.Generate(member);

            return token;
        }

        public async Task<RegisterResponseDto> Register(RegisterDto registerCreds)
        {
            // Checking if given email already exists

            if (!await _memberRepository.IsEmailUniqueAsync(registerCreds.Email))
            {
                return new RegisterResponseDto(false, "User registration failed. Username may already be taken."); // Username already exists
            }

            // Validating user registration creds

            var results = await new RegisterCredsValidator(_memberRepository).ValidateAsync(registerCreds);
            if (!results.IsValid)
            {
                StringBuilder stringBuilder = new StringBuilder(string.Empty);
                
                foreach (var failure in results.Errors)
                {
                    stringBuilder.Append($"{failure.ErrorMessage},");
                }

                return new RegisterResponseDto(false, stringBuilder.ToString());
            }

            CreatePasswordHash(registerCreds.Password, out string passwordHash, out string passwordSalt);

            var user = new User
            {
                UserId = Guid.NewGuid(),
                Username = registerCreds.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Email = registerCreds.Email
            };

            _memberRepository.AddUser(user);

            return new RegisterResponseDto(true, "User registered successfully.");
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
