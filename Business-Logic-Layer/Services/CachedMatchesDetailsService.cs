using Business_Logic_Layer.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services
{
    public class CachedMatchesDetailsService : IMatchDetailsService
    {x

        private readonly MatchDetailsService _matchDetailsService;
        private readonly IMemoryCache _memoryCache;
        public CachedMatchesDetailsService(MatchDetailsService matchDetailsService, IMemoryCache memoryCache)
        {
            _matchDetailsService = matchDetailsService;
            _memoryCache = memoryCache;
        }

        public Task<MatchDto> GetMatchDetailsByMatchIdAsync(string matchId)
        {
            // Caching logic to be implemented
            
            return  _matchDetailsService.GetMatchDetailsByMatchIdAsync(matchId);

        }

        public Task<List<MatchDto>> GetMatchDetailsListByMatchIdsAsync(IEnumerable<string> matchIdsList, string SummonerPUUID)
        {
            // Caching logic to be implemented

            return _matchDetailsService.GetMatchDetailsListByMatchIdsAsync(matchIdsList, SummonerPUUID);
        }
    }
}
