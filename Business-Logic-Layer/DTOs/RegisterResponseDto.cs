using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Dtos
{
    public class RegisterResponseDto
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public RegisterResponseDto(bool Success)
        {
            this.Success = Success;
        }
        public RegisterResponseDto(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message; 
        }
    }
}
