using Business_Logic_Layer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using webapi.DTOs;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SummonerController : ControllerBase
    {
        private readonly ILogger<SummonerController> _logger;
        private readonly IConfiguration _configuration;

        public SummonerController(ILogger<SummonerController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet("Info")]
        public async Task<ActionResult<SummonerDTO>> GetSummonerByNameAsync(
            string SummonerName, 
            SummonerInfoService summonerInfoService)
        {
            var content = await summonerInfoService.GetSummonerInfoByNameAsync(SummonerName,
                _configuration.GetValue<string>("Api_Key"));

            return Ok(content);

        }

        [HttpGet("Matches")]
        public async Task<ActionResult<IEnumerable<List<string>>>> GetMatchesListForGivenSummoner()
        {
            List<string> matches = new List<string>
            {
                "test1",
                "test2",
                "test3"
            };

            return Ok(matches);
        }
    }
}
