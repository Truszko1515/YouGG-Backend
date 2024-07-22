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
    {

        private readonly IMatchDetailsService _decorated;
        private readonly IMemoryCache _memoryCache;
        public CachedMatchesDetailsService(IMatchDetailsService decorated, IMemoryCache memoryCache)
        {
            _decorated = decorated;
            _memoryCache = memoryCache;
        }

        public Task<MatchDto> GetMatchDetailsByMatchIdAsync(string matchId)
        {          
            return _decorated.GetMatchDetailsByMatchIdAsync(matchId);
        }

        public Task<List<MatchDto>?> GetMatchDetailsListByMatchIdsAsync(IEnumerable<string> matchIdsList, string SummonerPUUID)
        {

                return _memoryCache.GetOrCreateAsync(SummonerPUUID, options =>
                {
                    options.SetAbsoluteExpiration(TimeSpan.FromSeconds(180));

                    return _decorated.GetMatchDetailsListByMatchIdsAsync(matchIdsList, SummonerPUUID);
                });
        }
    }
}
