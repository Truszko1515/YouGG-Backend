using Data_Acces_Layer.Interfaces;
using Data_Acces_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Acces_Layer.Repository
{
    public sealed class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MemberRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;  

        public async Task<User?> GetByEmailAsync(string email) 
            =>  await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);

        public async Task<User?> GetByUsernameAsync(string username)
            => await _dbContext.Users.SingleOrDefaultAsync(u => u.Username == username);

        public async Task<bool> IsEmailUniqueAsync(string email)
            => !await _dbContext.Users.AnyAsync(user => user.Email == email);

        public async Task<bool> IsUsernameUniqueAsync(string username)
            => !await _dbContext.Users.AnyAsync(user => user.Username == username);

        public async Task<List<string>> GetUserRolesAsync(Guid userId)
            => await _dbContext.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.Role.RoleName)
                .ToListAsync();

        public void AddUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
    }
}
