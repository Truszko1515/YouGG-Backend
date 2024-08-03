using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Dtos
{
    public class UserDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
