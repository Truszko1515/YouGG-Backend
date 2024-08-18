using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using webapi.DTOs;
using System.Text;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Data_Acces_Layer.Interfaces;

namespace webapi.Controllers
{
    [Route("api/summoner/[action]/")]
    [EnableCors("LocalHostPolicy")]
    [ApiController]
    public class SummonerInfoController : ControllerBase
    {
        private readonly ISummonerInfoService _summonerInfoService;
        private readonly IMatchesService _matchesService;
        private readonly IMatchDetailsService _matchDetailsService;
        private readonly ISummonerPUUIDService _summonerPUUIDService;

        private readonly ISummonerRepository _summonerRepository;
        private readonly IMemberRepository _memberRepository;
        public SummonerInfoController(
                                  ISummonerInfoService summonerInfoService,
                                  IMatchesService matchesService,
                                  IMatchDetailsService matchDetailsService,
                                  ISummonerRepository summonerRepository,
                                  ISummonerPUUIDService summonerPUUIDService,
                                  IMemberRepository memberRepository)
        {
            _summonerInfoService = summonerInfoService;
            _matchesService = matchesService;
            _matchDetailsService = matchDetailsService;
            _summonerRepository = summonerRepository;
            _summonerPUUIDService = summonerPUUIDService;
            _memberRepository = memberRepository;
        }

        [HttpGet("{summonerName}")]
        public async Task<ActionResult<string>> SummonerPUUID(string summonerName)
        {
            return Ok(await _summonerPUUIDService.GetSummonerPUUIDByNameAsync(summonerName));
        }

        [HttpGet("{summonerName}")]
        public async Task<ActionResult<IEnumerable<string>>> SummonerMaatchesList(string summonerName)
        {
            string summonerPUUID = await _summonerPUUIDService.GetSummonerPUUIDByNameAsync(summonerName);

            if (summonerPUUID != null)
                return Ok(await _matchesService.GetMatchListByPUUIDAsync(summonerPUUID));

            
            return NotFound();
        }

        [HttpGet("{matchID}")]
        public async Task<ActionResult<MatchDto>> MatchDetails(string matchID)
        {
            return Ok(await _matchDetailsService.GetMatchDetailsByMatchIdAsync(matchID));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<string> AuthorizationTest()
        {
            return Ok("Udało sięę :)))");
        }

        [HttpGet("{summonerName}")]
        public async Task<ActionResult<SummonerDTO>> Info(string summonerName)
        {
            string summonerPUUID = await _summonerPUUIDService.GetSummonerPUUIDByNameAsync(summonerName);

            return Ok(await _summonerInfoService.GetSummonerInfoByPuuidAsync(summonerPUUID));
        }
    }


}
