using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Acces_Layer.Models;

namespace Data_Acces_Layer.Interfaces
{
    public interface IMemberRepository
    {
        //Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByUsernameAsync(string username);

        Task<bool> IsEmailUniqueAsync(string email);
        Task<bool> IsUsernameUniqueAsync(string username);

        Task<List<string>> GetUserRolesAsync(Guid userId);

        void AddUser(User user);

        //void Update(User member);


    }
}
