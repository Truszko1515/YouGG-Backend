using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.DTOs;

namespace webapi.Controllers
{
    [Route("api/[controller]/[action]/")]
    [EnableCors("LocalHostPolicy")]
    [ApiController]
    public class SummonerController : ControllerBase
    {
        private readonly ILogger<SummonerController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ISummonerRepository _summonerRepository;

        public SummonerController(ILogger<SummonerController> logger,
                                  IConfiguration configuration,
                                  ISummonerRepository summonerRepository)
        {
            _configuration = configuration;
            _logger = logger;
            _summonerRepository = summonerRepository;
        }

        [Authorize]
        [HttpGet("{summonerName}")]
        public async Task<ActionResult> KDA(string summonerName)
        {
            return Ok(await _summonerRepository.GetSummonerKDA(summonerName));
        }
    }
}
