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
    public class TestSummonerController : ControllerBase
    {
        private readonly ILogger<TestSummonerController> _logger;
        private readonly IConfiguration _configuration;

        private readonly ISummonerInfoService _summonerInfoService;
        private readonly IMatchesService _matchesService;
        private readonly IMatchDetailsService _matchDetailsService;

        public TestSummonerController(ILogger<TestSummonerController> logger, 
                                  IConfiguration configuration,
                                  ISummonerInfoService summonerInfoService,
                                  IMatchesService matchesService,
                                  IMatchDetailsService matchDetailsService)
        {
            _logger = logger;
            _configuration = configuration; 
            _summonerInfoService = summonerInfoService;
            _matchesService = matchesService;
            _matchDetailsService = matchDetailsService;
        }

        [HttpGet("Info")]
        public async Task<ActionResult<SummonerDTO>> GetSummonerByNameAsync(string summonerName)
        {
            return Ok(await _summonerInfoService.GetSummonerInfoByNameAsync(summonerName));
        }


        [HttpGet("MatchesList")]
        public async Task<ActionResult<IEnumerable<string>>> GetListOfMatchesByNameAsync(string summonerName)
        {
            return Ok(await _matchesService.GetMatchListByNameAsync(summonerName));
        }

        [HttpGet("MatchDetailsByID")]
        public async Task<ActionResult<MatchDto>> GetMatchDetailsByMatchIdAsync(string matchID)
        {
            return Ok(await _matchDetailsService.GetMatchDetailsByMatchIdAsync(matchID));
        }

        [HttpGet("LastMatchDetails")]
        public async Task<ActionResult<MatchDto>> GetLastMatchDetailsAsync(string summonerName)
        {
            return Ok(await _matchDetailsService.GetLastMatchDetailsByNameAsync(summonerName));
        }

        [HttpGet("MatchParticipants")]
        public async Task<IActionResult> GetLastMatchPatricipants(string summonerName)
        {
            return Ok(await _matchDetailsService.GetParticipantsOfLastMatchAsync(summonerName));
        }

        [HttpGet("SummonerKDA")]
        public async Task<IActionResult> GetSummonerKdaAsync(string summonerName)
        {
            return Ok(await _matchDetailsService.GetSummonerKDAFromLastTwentyGames(summonerName));
        }


    }


}
