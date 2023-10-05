using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.DTOs;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SummonerController : ControllerBase
    {
        private readonly ILogger<TestSummonerController> _logger;
        private readonly IConfiguration _configuration;
        // 
        private readonly ISummonerRepository _summonerRepository;

        public SummonerController(ILogger<TestSummonerController> logger,
                                  IConfiguration configuration,
                                  ISummonerRepository summonerRepository)
        {
            _configuration = configuration;
            _logger = logger;
            _summonerRepository = summonerRepository;
        }

        [HttpGet("KDA")]
        public async Task<IActionResult> GetSummonerKDA(string summonerName)
        {
            return Ok(await _summonerRepository.GetSummonerKDA(summonerName));
        }
    }
}
