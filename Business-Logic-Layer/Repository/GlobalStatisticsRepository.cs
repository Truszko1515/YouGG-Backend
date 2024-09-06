using Azure;
using Business_Logic_Layer.Dtos;
using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Interfaces.GlobalStats;
using Business_Logic_Layer.Services;
using Business_Logic_Layer.Services.GlobalStatsServices;
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
        // Summoner services
        private readonly ISummonerPUUIDService _summonerPUUIDService;
        private readonly IMatchesService  _matchesService;
        private readonly IMatchDetailsService _matchDetailsService;

        // Service inserting or getting champions stats to/from Database
        private readonly IChampionsStatisticInsertService _championsStatisticInsertService;
        private readonly IChampionsStatisticService _championsStatisticService;


        public GlobalStatisticsRepository(ISummonerPUUIDService summonerPUUIDService,
                                          IMatchesService matchesService,
                                          IMatchDetailsService matchDetailsService,
                                          IChampionsStatisticInsertService championsStatisticInsertService,
                                          IChampionsStatisticService championsStatisticService)
        {
            _summonerPUUIDService = summonerPUUIDService;
            _matchesService = matchesService;
            _matchDetailsService = matchDetailsService;
            _championsStatisticInsertService = championsStatisticInsertService;
            _championsStatisticService = championsStatisticService;

        }

        public async Task<(bool result, int championsInserted)> UpdateChampionsData(string summonerName)
        {
            var summonerPUUID = await _summonerPUUIDService.GetSummonerPUUIDByNameAsync(summonerName);
            var matchesIDs = await _matchesService.GetMatchListByPUUIDAsync(summonerPUUID);
            var matches = await _matchDetailsService.GetMatchDetailsListByMatchIdsAsync(matchesIDs, summonerPUUID);

            var insertChampionDataGlobal = await _championsStatisticInsertService.TryAddChampionDataAsync(matches);
            
            if(insertChampionDataGlobal.result)
            {
                return (true, insertChampionDataGlobal.championsInserted);
            }

            return (false, 0);
        }

        // Method that will use IChampionsStatisticService to retreive champion statistics.

        // private - internal methods
    }
}
