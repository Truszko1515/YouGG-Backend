using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using webapi.DTOs;
using System.Text;
using Microsoft.AspNetCore.Cors;

namespace webapi.Controllers
{
    [Route("api/summoner/")]
    [EnableCors("LocalHostPolicy")]
    [ApiController]
    public class ServicesTestController : ControllerBase
    {
        private readonly ILogger<ServicesTestController> _logger;
        private readonly IConfiguration _configuration;

        private readonly ISummonerInfoService _summonerInfoService;
        private readonly IMatchesService _matchesService;
        private readonly IMatchDetailsService _matchDetailsService;
        private readonly ISummonerPUUIDService _summonerPUUIDService;

        private readonly ISummonerRepository _summonerRepository;

        public ServicesTestController(ILogger<ServicesTestController> logger, 
                                  IConfiguration configuration,
                                  ISummonerInfoService summonerInfoService,
                                  IMatchesService matchesService,
                                  IMatchDetailsService matchDetailsService,
                                  ISummonerRepository summonerRepository,
                                  ISummonerPUUIDService summonerPUUIDService)
        {
            _logger = logger;
            _configuration = configuration; 
            _summonerInfoService = summonerInfoService;
            _matchesService = matchesService;
            _matchDetailsService = matchDetailsService;
            _summonerRepository = summonerRepository;
            _summonerPUUIDService = summonerPUUIDService;
        }

        [HttpGet("[action]/{summonerName}")]
        public async Task<ActionResult<string>> SummonerPUUID(string summonerName)
        {
            return Ok(await _summonerPUUIDService.GetSummonerPUUIDByNameAsync(summonerName));
        }

        [HttpGet("{summonerName}")]
        public async Task<ActionResult<SummonerDTO>> SummonerInfo(string summonerName)
        {
            string summonerPUUID = await _summonerPUUIDService.GetSummonerPUUIDByNameAsync(summonerName);

            return Ok(await _summonerInfoService.GetSummonerInfoByPuuidAsync(summonerPUUID));
        }


        [HttpGet("[action]/{summonerName}")]
        public async Task<ActionResult<IEnumerable<string>>> SummonerMaatchesList(string summonerName)
        {
            string summonerPUUID = await _summonerPUUIDService.GetSummonerPUUIDByNameAsync(summonerName);

            if (summonerPUUID != null)
                return Ok(await _matchesService.GetMatchListByPUUIDAsync(summonerPUUID));

            
            return NotFound();
        }

        [HttpGet("[action]/{matchID}")]
        public async Task<ActionResult<MatchDto>> MatchDetails(string matchID)
        {
            return Ok(await _matchDetailsService.GetMatchDetailsByMatchIdAsync(matchID));
        }
    }


}
