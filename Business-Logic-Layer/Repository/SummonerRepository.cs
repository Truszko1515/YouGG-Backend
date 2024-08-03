using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Services;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
using System.Dynamic;
using System.Net.Http;
using System.Security.Cryptography;

namespace Business_Logic_Layer.Repository
{
    public sealed class SummonerRepository : ISummonerRepository
    {
        private readonly IMatchDetailsService  _matchDetailsService;
        private readonly IMatchesService _matchesService;
        private readonly ISummonerInfoService _summonerInfoService;
        private readonly ISummonerPUUIDService _summonerPUUIDService;

        private readonly List<MatchDto> matchDetails;

        public SummonerRepository(IMatchDetailsService matchDetailsService,
                                  IMatchesService matchesService,
                                  ISummonerInfoService summonerInfoService,
                                  ISummonerPUUIDService summonerPUUIDService
            )                                  
        {
                _matchDetailsService = matchDetailsService;
                _matchesService = matchesService;   
                _summonerInfoService = summonerInfoService;
                _summonerPUUIDService = summonerPUUIDService;
        }

        

        public async Task<IEnumerable<float>> GetSummonerKDA(string summonerName)
        {

            var summonerPUUID = await _summonerPUUIDService.GetSummonerPUUIDByNameAsync(summonerName);
            var matchesIDs      = await _matchesService.GetMatchListByPUUIDAsync(summonerPUUID);
            var matches       = await _matchDetailsService.GetMatchDetailsListByMatchIdsAsync(matchesIDs, summonerPUUID);

            Stopwatch stopwatch = Stopwatch.StartNew();

            var KDA = matches.SelectMany(match => match.info.participants)
                             .Where(participant => participant.puuid == summonerPUUID)
                             .Select(p => p.challenges.kda);
                              

            stopwatch.Stop();
            var time = stopwatch.Elapsed;

            return KDA;
        }
        
        public async Task<IEnumerable<double>> GetSummonerKillsDeathsAssists(string summonerName)
        {

            var summonerPUUID = await _summonerPUUIDService.GetSummonerPUUIDByNameAsync(summonerName);
            var matchIDs = await _matchesService.GetMatchListByPUUIDAsync(summonerPUUID);
            var matches = await _matchDetailsService.GetMatchDetailsListByMatchIdsAsync(matchIDs, summonerPUUID);

            Stopwatch stopwatch = Stopwatch.StartNew();

            var KDA = matches.SelectMany(match => match.info.participants).
                              Where(participant => participant.puuid == summonerPUUID).
                              Select(participant => Math.Round((participant.kills + participant.assists) / (float)(participant.deaths != 0 ? participant.deaths : 1), 2 ));

            stopwatch.Stop();
            var time = stopwatch.Elapsed;

            return KDA;
        }

    }
}
