using Business_Logic_Layer.Dtos;
using Data_Acces_Layer.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Validation
{
    public class RegisterCredsValidator : AbstractValidator<RegisterDto>
    {
        private readonly IMemberRepository _memberRepository;

        public RegisterCredsValidator(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required")
                .Length(3, 50).WithMessage("Username must be at least 3 characters long")
                .MustAsync(BeUniqueUsername).WithMessage("Username already exists");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long")
        ;

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid Email format")
                .MustAsync(BeUniqueEmail).WithMessage("Email already exists");
        }

        private async Task<bool> BeUniqueUsername(string username, CancellationToken cancellationToken)
        {
            return await _memberRepository.IsUsernameUniqueAsync(username);
        }

        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            return await _memberRepository.IsEmailUniqueAsync(email); 
        }
    }
}
