using Business_Logic_Layer.Dtos;
using Business_Logic_Layer.Helpers.CollectionQueryHelpers;
using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Services;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
using System.Dynamic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace Business_Logic_Layer.Repository
{
    public sealed class SummonerRepository : ISummonerRepository
    {
        private readonly IMatchDetailsService  _matchDetailsService;
        private readonly IMatchesService _matchesService;
        private readonly ISummonerInfoService _summonerInfoService;
        private readonly ISummonerPUUIDService _summonerPUUIDService;


        public SummonerRepository(IMatchDetailsService matchDetailsService,
                                  IMatchesService matchesService,
                                  ISummonerInfoService summonerInfoService,
                                  ISummonerPUUIDService summonerPUUIDService)                                  
        {
                _matchDetailsService = matchDetailsService;
                _matchesService = matchesService;   
                _summonerInfoService = summonerInfoService;
                _summonerPUUIDService = summonerPUUIDService;
        }
        
        public async Task<double> GetSummonerKDA(string summonerName)
        {
            // We are sure that SummonerPUUID here is not null 
            // Otherwise error is handled inside Get method
            var summonerPUUID = await _summonerPUUIDService.GetSummonerPUUIDByNameAsync(summonerName);
            var matchesIDs      = await _matchesService.GetMatchListByPUUIDAsync(summonerPUUID);
            if (!matchesIDs.Any()) return 0;

            var matches       = await _matchDetailsService.GetMatchDetailsListByMatchIdsAsync(matchesIDs, summonerPUUID);


            var KDA = matches.SelectMany(match => match.info.participants)
                             .Where(participant => participant.puuid == summonerPUUID)
                             .Select(p => p.challenges.kda)
                             .ToList();
                              

            return Math.Round(KDA.Average(),2);
        }
        public async Task<IEnumerable<ChampionsPlayRateDto>> GetSummonerChampionsPlayRate(string summonerName)
        {
            // We are sure that SummonerPUUID here is not null 
            // Otherwise error is handled inside Get method
            var summonerPUUID = await _summonerPUUIDService.GetSummonerPUUIDByNameAsync(summonerName);
            var matchesIDs = await _matchesService.GetMatchListByPUUIDAsync(summonerPUUID);
            if (!matchesIDs.Any()) return Enumerable.Empty<ChampionsPlayRateDto>();

            var matches = await _matchDetailsService.GetMatchDetailsListByMatchIdsAsync(matchesIDs, summonerPUUID);

            var championCounts = ChampionsPlayRateQuery.GetChampionCounts(matches, summonerPUUID);
            var topChampions = ChampionsPlayRateQuery.GetTopChampions(championCounts);

            List<ChampionsPlayRateDto> champs = new List<ChampionsPlayRateDto>();

            foreach (var champName in topChampions)
            {
                champs.Add(new ChampionsPlayRateDto()
                {
                    name = champName,
                    kda = championCounts[champName].Count > 0 ? ((float)championCounts[champName].TotalKda) / championCounts[champName].Count : 0,
                    losses = (championCounts[champName].Count - championCounts[champName].wins),
                    winRate = (int)Math.Round(((double)championCounts[champName].wins / championCounts[champName].Count) * 100),
                    wins = championCounts[champName].wins 
                });
            }

            return champs;
        }
        public async Task<double> GetKillParticipation(string summonerName)
        {
            // We are sure that SummonerPUUID here is not null 
            // Otherwise error is handled inside Get method
            var summonerPUUID = await _summonerPUUIDService.GetSummonerPUUIDByNameAsync(summonerName);
            var matchesIDs = await _matchesService.GetMatchListByPUUIDAsync(summonerPUUID);
            if (!matchesIDs.Any()) return 0; 

            var matches = await _matchDetailsService.GetMatchDetailsListByMatchIdsAsync(matchesIDs, summonerPUUID);

            var killParticipations = matches.SelectMany(match => match.info.participants)
                                            .Where(participant => participant.puuid == summonerPUUID)
                                            .Select(p => p.challenges.killParticipation * 100)
                                            .ToList();

            if (!killParticipations.Any()) return 0; 

            return Math.Round(killParticipations.Average()); 
        }
        public async Task<IEnumerable<PositionsChartDto>> GetPosition(string summonerName)
        {
            var summonerPUUID = await _summonerPUUIDService.GetSummonerPUUIDByNameAsync(summonerName);
            var matchesIDs = await _matchesService.GetMatchListByPUUIDAsync(summonerPUUID);
            if (!matchesIDs.Any()) return new List<PositionsChartDto>();

            var matches = await _matchDetailsService.GetMatchDetailsListByMatchIdsAsync(matchesIDs, summonerPUUID);

            // Initializing List with roles
            List<PositionsChartDto> positions = new List<PositionsChartDto>
            {
                new PositionsChartDto { RoleInGame = "TOP", Percentage = 0 },
                new PositionsChartDto { RoleInGame = "JUNGLE", Percentage = 0 },
                new PositionsChartDto { RoleInGame = "MIDDLE", Percentage = 0 },
                new PositionsChartDto { RoleInGame = "BOTTOM", Percentage = 0 },
                new PositionsChartDto { RoleInGame = "UTILITY", Percentage = 0 }
            };

            // fetching roles and counting them
            var roleCounts = matches.SelectMany(match => match.info.participants)
                                    .Where(participant => participant.puuid == summonerPUUID)
                                    .GroupBy(p => p.teamPosition)
                                    .ToDictionary(group => group.Key.ToUpper(), group => group.Count());

            
            int totalRoles = roleCounts.Values.Sum();

            if (totalRoles == 0) return positions;

            // Updating percentage indicatior for every role
            foreach (var roleCount in roleCounts)
            {
                var position = positions.FirstOrDefault(p => p.RoleInGame == roleCount.Key);
                if (position != null)
                {
                    position.Percentage = (int)((double)roleCount.Value / totalRoles * 100);
                }
            }

            return positions;
        }
        public async Task<LastGamesWinRateDto> GetLastGamesWinRate(string summonerName)
        {
            var summonerPUUID = await _summonerPUUIDService.GetSummonerPUUIDByNameAsync(summonerName);
            var matchesIDs = await _matchesService.GetMatchListByPUUIDAsync(summonerPUUID);
            if (!matchesIDs.Any()) return new LastGamesWinRateDto();

            var matches = await _matchDetailsService.GetMatchDetailsListByMatchIdsAsync(matchesIDs, summonerPUUID);

            var participants = matches.SelectMany(match => match.info.participants)
                                      .Where(participant => participant.puuid == summonerPUUID);

            LastGamesWinRateDto lastGames = new LastGamesWinRateDto
            {
                gamesPlayed = matches.Count(),
                wins = participants.Count(p => p.win),
                losses = participants.Count(p => !p.win)
            };

            return lastGames;
        }


        // test
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
