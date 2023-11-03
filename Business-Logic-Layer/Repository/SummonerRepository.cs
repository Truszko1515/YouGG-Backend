using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Services;
using System.Diagnostics;
using System.Dynamic;
using System.Net.Http;

namespace Business_Logic_Layer.Repository
{
    public sealed class SummonerRepository : ISummonerRepository
    {
        private readonly IMatchDetailsService  _matchDetailsService;
        private readonly IMatchesService _matchesService;
        private readonly ISummonerInfoService _summonerInfoService;

        public SummonerRepository(IMatchDetailsService matchDetailsService,
                                  IMatchesService matchesService,
                                  ISummonerInfoService summonerInfoService)
                                  
        {
                _matchDetailsService = matchDetailsService;
                _matchesService = matchesService;   
                _summonerInfoService = summonerInfoService;
        }

        private List<MatchDto>? _matchesDetails { get; set; }

        public async Task<object> GetSummonerKDA(string summonerName)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            var summonerPUUID = await _summonerInfoService.GetSummonerPUUIDByNameAsync(summonerName);
            var matchIDs      = await _matchesService.GetMatchListByPUUIDAsync(summonerPUUID);
            var matches       = await _matchDetailsService.GetMatchDetailsListByMatchIdsAsync(matchIDs);

            var KDA = matches.SelectMany(match => match.info.participants).
                              Where(participant => participant.summonerName.ToLower() == summonerName.ToLower()).
                              Select(participant => participant.kills);

            stopwatch.Stop();
            var time = stopwatch.Elapsed;

            return (KDA.Average() + "\n" + time.Seconds + ":" + time.Milliseconds);
        }

        public async Task<object> GetQueueTypes(string summonerName)
        {
            var summonerPUUID = await _summonerInfoService.GetSummonerPUUIDByNameAsync(summonerName);
            var matchIDs = await _matchesService.GetMatchListByPUUIDAsync(summonerPUUID);
            var matches = await _matchDetailsService.GetMatchDetailsListByMatchIdsAsync(matchIDs);

            var queueTypes = matches.Select(match => match.info.queueId);
            
            return queueTypes;
        }
    }
}
