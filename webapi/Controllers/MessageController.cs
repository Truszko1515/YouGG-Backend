using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly List<string> _userlist;
        private readonly IConfiguration _configuration;

        public MessageController(IConfiguration configuration)
        {
            _userlist = new List<string>();
            _configuration = configuration;
        }

        [HttpPost, Route("")]
        public IActionResult Post([FromBody] string message)
        {
            _userlist.Add(message);

            return Ok(_userlist); 
        }

        [HttpGet(Name = "ApiKey")]
        public string ApiKey()
        {
            if (_configuration.GetValue<string>("Developement_Api_Keys") is not null)
                return _configuration.GetValue<string>("Developement_Api_Keys");

            return "nie znaleziono";
        }

        
    }
}
