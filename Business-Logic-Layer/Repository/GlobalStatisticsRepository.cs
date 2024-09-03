using Azure;
using Business_Logic_Layer.Dtos;
using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Interfaces.GlobalStats;
using Business_Logic_Layer.Services;
using Data_Acces_Layer;
using Data_Acces_Layer.Interfaces;
using Data_Acces_Layer.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Repository
{
    public class GlobalStatisticsRepository : IGlobalStatisticsRepository
    {
        private readonly ISummonerPUUIDService _summonerPUUIDService;
        private readonly IMatchesService  _matchesService;
        private readonly IMatchDetailsService _matchDetailsService;
        private readonly IGlobalStatsRepository _globalStatsRepository;
        private readonly IChampionsStatisticService _championsStatisticService;
        private readonly ApplicationDbContext _dbContext;


        public GlobalStatisticsRepository(ISummonerPUUIDService summonerPUUIDService,
                                          IMatchesService matchesService,
                                          IMatchDetailsService matchDetailsService,
                                          IGlobalStatsRepository globalStatsRepository,
                                          IChampionsStatisticService championsStatisticService,
                                          ApplicationDbContext dbContext)
        {
            _summonerPUUIDService = summonerPUUIDService;
            _matchesService = matchesService;
            _matchDetailsService = matchDetailsService;
            _globalStatsRepository = globalStatsRepository;
            _championsStatisticService = championsStatisticService;
            _dbContext = dbContext;
        }

        public async Task<(bool result, int championsInserted)> UpdateChampionsData(string summonerName)
        {
            var summonerPUUID = await _summonerPUUIDService.GetSummonerPUUIDByNameAsync(summonerName);
            var matchesIDs = await _matchesService.GetMatchListByPUUIDAsync(summonerPUUID);
            var matches = await _matchDetailsService.GetMatchDetailsListByMatchIdsAsync(matchesIDs, summonerPUUID);

            var insertChampionDataGlobal = await _championsStatisticService.TryAddChampionDataAsync(matches);
            
            if(insertChampionDataGlobal.result)
            {
                return (true, insertChampionDataGlobal.championsInserted);
            }

            return (false, 0);
        }

        // private - internal methods
    }
}
