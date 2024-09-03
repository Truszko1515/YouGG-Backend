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

        [HttpGet("{summonerName}")]
        public async Task<ActionResult> KDA(string summonerName)
        {
            return Ok(await _summonerRepository.GetKDA(summonerName));
        }

        [HttpGet("{summonerName}")]
        public async Task<ActionResult> ChampionsPlayed(string summonerName)
        {
            return Ok(await _summonerRepository.GetChampionsPlayRate(summonerName));
        }

        [HttpGet("{summonerName}")]
        public async Task<ActionResult> KillParticipation(string summonerName)
        {
            return Ok(await _summonerRepository.GetKillParticipation(summonerName));
        }

        [HttpGet("{summonerName}")]
        public async Task<ActionResult> Positions(string summonerName)
        {
            return Ok(await _summonerRepository.GetPosition(summonerName));
        }

        [HttpGet("{summonerName}")]
        public async Task<ActionResult> LastGamesRatio(string summonerName)
        {
            return Ok(await _summonerRepository.GetLastGamesWinRate(summonerName));
        }

        [HttpGet("{summonerName}")]
        public async Task<ActionResult> LeagueEntries(string summonerName)
        {
            return Ok(await _summonerRepository.GetLeagueEntries(summonerName));
        }

        [HttpGet("{summonerName}")]
        public async Task<ActionResult> ChampionsMastery(string summonerName)
        {
            return Ok(await _summonerRepository.GetChampionsMastery(summonerName));
        }

        [HttpGet("{summonerName}")]
        public async Task<ActionResult> MatchesDetails(string summonerName)
        {
            return Ok(await _summonerRepository.GetMatchesDetails(summonerName));
        }

        [HttpGet("{summonerName}")]
        public async Task<ActionResult> TrySaveDataToDB(string summonerName)
        {
            return Ok(await _summonerRepository.GetMatchesDetails(summonerName));
        }
    }
}
