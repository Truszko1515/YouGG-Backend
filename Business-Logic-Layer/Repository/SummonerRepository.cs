using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Services;
using System.Dynamic;

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
                Repository_id = Guid.NewGuid();
        }

        public Guid Repository_id;
        private List<MatchDto>? _matchesDetails { get; set; }

        public async Task<Object> GetSummonerKDA(string summonerName)
        {
            var summonerPUUID = await _summonerInfoService.GetSummonerPUUIDByNameAsync(summonerName);
            var matchIDs      = await _matchesService.GetMatchListByPUUIDAsync(summonerPUUID);
            var matches       = await _matchDetailsService.GetMatchDetailsListByMatchIdsAsync(matchIDs);

            var KDA = matches.SelectMany(match => match.info.participants).
                              Where(participant => participant.summonerName == summonerName).
                              Select(participant => participant.kills);
                                                                           
            return (KDA.Average() + "\nRepository id - " + this.Repository_id);
        }
    }
}
