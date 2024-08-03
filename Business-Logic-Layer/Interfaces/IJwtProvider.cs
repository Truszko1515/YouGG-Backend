using Data_Acces_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Interfaces
{
    public interface IJwtProvider
    {
        Task<string> Generate(User user);
    }
}
