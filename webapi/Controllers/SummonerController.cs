using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using webapi.DTOs;
using System.Text;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SummonerController : ControllerBase
    {
        private readonly ILogger<SummonerController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ISummonerInfoService _summonerInfoService;
        private readonly IMatchesService _matchesService;

        public SummonerController(ILogger<SummonerController> logger, 
                                  IConfiguration configuration,
                                  ISummonerInfoService summonerInfoService,
                                  IMatchesService matchesService)
        {
            _logger = logger;
            _configuration = configuration; 
            _summonerInfoService = summonerInfoService;
            _matchesService = matchesService;
        }

        [HttpGet("Info")]
        public async Task<ActionResult<SummonerDTO>> GetSummonerByNameAsync(string SummonerName)
        {
            return Ok(await _summonerInfoService.GetSummonerInfoByNameAsync(SummonerName));
        }


        [HttpGet("MatchesList")]
        public async Task<ActionResult<IEnumerable<string>>> GetListOfMatchesForGivenSummoner(string SummonerName)
        {
            return Ok(await _matchesService.GetMatchListByNameAsync(SummonerName));
        }

    }


}
