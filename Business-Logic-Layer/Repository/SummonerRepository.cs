using Business_Logic_Layer.Dtos;
using Business_Logic_Layer.Helpers;
using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Services;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
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
        private readonly ISummonerLeagueService _summonerLeagueEntryService;
        private readonly ISummonerMasteryService _summonerMasteryService;



        public SummonerRepository(IMatchDetailsService matchDetailsService,
                                  IMatchesService matchesService,
                                  ISummonerInfoService summonerInfoService,
                                  ISummonerPUUIDService summonerPUUIDService,
                                  ISummonerLeagueService summonerLeagueEntryService,
                                  ISummonerMasteryService summonerMasteryService)                                  
        {
                _matchDetailsService = matchDetailsService;
                _matchesService = matchesService;   
                _summonerInfoService = summonerInfoService;
                _summonerPUUIDService = summonerPUUIDService;
                _summonerLeagueEntryService = summonerLeagueEntryService;
                _summonerMasteryService = summonerMasteryService;
        }
        
        public async Task<double> GetKDA(string summonerName)
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
        public async Task<IEnumerable<SummonerChampionsPlayRateDto>> GetChampionsPlayRate(string summonerName)
        {
            // We are sure that SummonerPUUID here is not null 
            // Otherwise error is handled inside Get method
            var summonerPUUID = await _summonerPUUIDService.GetSummonerPUUIDByNameAsync(summonerName);
            var matchesIDs = await _matchesService.GetMatchListByPUUIDAsync(summonerPUUID);
            if (!matchesIDs.Any()) return Enumerable.Empty<SummonerChampionsPlayRateDto>();

            var matches = await _matchDetailsService.GetMatchDetailsListByMatchIdsAsync(matchesIDs, summonerPUUID);

            var championCounts = ChampionsPlayRateQuery.GetChampionCounts(matches, summonerPUUID);
            var topChampions = ChampionsPlayRateQuery.GetTopChampions(championCounts);

            List<SummonerChampionsPlayRateDto> champs = new List<SummonerChampionsPlayRateDto>();

            foreach (var champName in topChampions)
            {
                champs.Add(new SummonerChampionsPlayRateDto()
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
        public async Task<IEnumerable<SummonerPositionsChartDto>> GetPosition(string summonerName)
        {
            var summonerPUUID = await _summonerPUUIDService.GetSummonerPUUIDByNameAsync(summonerName);
            var matchesIDs = await _matchesService.GetMatchListByPUUIDAsync(summonerPUUID);
            if (!matchesIDs.Any()) return new List<SummonerPositionsChartDto>();

            var matches = await _matchDetailsService.GetMatchDetailsListByMatchIdsAsync(matchesIDs, summonerPUUID);

            // Initializing List with roles
            List<SummonerPositionsChartDto> positions = new List<SummonerPositionsChartDto>
            {
                new SummonerPositionsChartDto { RoleInGame = "TOP", Percentage = 0 },
                new SummonerPositionsChartDto { RoleInGame = "JUNGLE", Percentage = 0 },
                new SummonerPositionsChartDto { RoleInGame = "MIDDLE", Percentage = 0 },
                new SummonerPositionsChartDto { RoleInGame = "BOTTOM", Percentage = 0 },
                new SummonerPositionsChartDto { RoleInGame = "UTILITY", Percentage = 0 }
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
        public async Task<SummonerLastGamesDto> GetLastGamesWinRate(string summonerName)
        {
            var summonerPUUID = await _summonerPUUIDService.GetSummonerPUUIDByNameAsync(summonerName);
            var matchesIDs = await _matchesService.GetMatchListByPUUIDAsync(summonerPUUID);
            if (!matchesIDs.Any()) return new SummonerLastGamesDto();

            var matches = await _matchDetailsService.GetMatchDetailsListByMatchIdsAsync(matchesIDs, summonerPUUID);

            var participants = matches.SelectMany(match => match.info.participants)
                                      .Where(participant => participant.puuid == summonerPUUID);

            SummonerLastGamesDto lastGames = new SummonerLastGamesDto
            {
                gamesPlayed = matches.Count(),
                wins = participants.Count(p => p.win),
                losses = participants.Count(p => !p.win)
            };

            return lastGames;
        }
        public async Task<SummonerLeagueEntryDto> GetLeagueEntries(string summonerName)
        {
            var summonerPUUID = await _summonerPUUIDService.GetSummonerPUUIDByNameAsync(summonerName);
            
            var summonerInfo = await _summonerInfoService.GetSummonerInfoByPuuidAsync(summonerPUUID);

            var result = await _summonerLeagueEntryService.GetLeagueEntry(summonerInfo.id);
            
            return result;
        }
        public async Task<List<SummonerMasteryDto>> GetChampionsMastery(string summonerName)
        {
            var summonerPUUID = await _summonerPUUIDService.GetSummonerPUUIDByNameAsync(summonerName);
            var result = await _summonerMasteryService.GetSummonerChampionsMastery(summonerPUUID);
            var finalResult = result.ToList();

            // Pobierz dane o bohaterach
            var champions = await ChampionMappingHelper.GetChampionsAsync();

            // Mapowanie championId na nazwy bohaterów
            var mappedResult = finalResult.Select(dto => (
                name: (champions.ContainsKey(dto.championId) ? champions[dto.championId].Replace("'", "") : "Unknown"),
                masteryLevel: dto.championLevel,
                points: dto.championPoints
            )).ToList();

            List<SummonerMasteryDto> summonerMasteryList = new  List<SummonerMasteryDto>();

            foreach ( var champion in mappedResult) 
            {
                summonerMasteryList.Add(new SummonerMasteryDto {
                    name = champion.name,
                    masteryLevel = champion.masteryLevel,
                    points = champion.points
                });
            }
            Console.Clear();
            // Debugging: Log or inspect the mapped result
            Console.WriteLine("Mapped Result Count: " + mappedResult.Count());
            foreach (var item in summonerMasteryList)
            {
                Console.WriteLine($"ChampionName: {item.name}, ChampionLevel: {item.masteryLevel}, ChampionPoints: {item.points}");
            }

            return summonerMasteryList;
        }
        public async Task<List<SummonerMatchDetailsDto>> GetMatchesDetails(string summonerName)
        {
            var summonerPUUID = await _summonerPUUIDService.GetSummonerPUUIDByNameAsync(summonerName);
            var matchesIDs = await _matchesService.GetMatchListByPUUIDAsync(summonerPUUID);
            if (!matchesIDs.Any()) return new List<SummonerMatchDetailsDto>();

            var matches = await _matchDetailsService.GetMatchDetailsListByMatchIdsAsync(matchesIDs, summonerPUUID); 

            List<SummonerMatchDetailsDto> matchDetailsList = new ();
            List<TeamMember> teamList = new ();
            List<TeamMember> opponentsList = new ();

            Stopwatch stopwatch = Stopwatch.StartNew();
            foreach (var match in matches)
            {
                var participant = match.info.participants.FirstOrDefault(p => p.puuid == summonerPUUID);

                if(participant != null)
                {
                    var team = match.info.participants.Take(5)
                                                      .Select(p => new TeamMember
                                                      {
                                                        name = !p.summonerName.Equals("") ? p.summonerName : "Unknown",
                                                        champion = p.championName,
                                                        tagLine = p.riotIdTagline,
                                                        fullName = $"{p.summonerName} #{p.riotIdTagline}"
                                                      })
                                                      .ToList();

                    // Ostatnie 5 uczestników jako opponents
                    var opponents = match.info.participants.Skip(5)
                                                           .Take(5)
                                                           .Select(p => new TeamMember
                                                           {
                                                               name = !p.summonerName.Equals("") ? p.summonerName : "Unknown",
                                                               champion = p.championName,
                                                               tagLine = p.riotIdTagline,
                                                               fullName = $"{p.summonerName} #{p.riotIdTagline}"
                                                           })
                                                           .ToList();

                    matchDetailsList.Add(new SummonerMatchDetailsDto
                    {
                        timeAgo = SummonerMatchDetailsHelper.GetTimeAgo(match.info.gameStartTimestamp),
                        champion = participant?.championName,
                        kills = participant.kills,
                        deaths = participant.deaths,
                        assists = participant.assists,
                        kdaRatio = participant.challenges.kda,
                        items = new[] {participant.item0, participant.item1, participant.item2, participant.item3, participant.item4, participant.item5, participant.item6 }
                                       .Select(id => id)
                                       .Where(value => value > 0)
                                       .ToArray(), 
                        spell1 =  SummonerSpellHelper.GetSummonerSpell(participant.summoner1Id),
                        spell2 =  SummonerSpellHelper.GetSummonerSpell(participant.summoner2Id),
                        runePrimary =  SummonerRunesHelper.GetRuneIconPath(participant.perks.styles[0].selections[0].perk),
                        runeSecondary =  SummonerRunesHelper.GetRuneIconPath(participant.perks.styles[1].style),
                        gameDuration = GameDurationHelper.GetGameDuration(match.info.gameDuration),

                        cs = SummonerMatchDetailsHelper.GetCS(participant.totalMinionsKilled, 
                                                              participant.neutralMinionsKilled, 
                                                              match.info.gameDuration),
                        result = participant.win ? "Zwycięstwo" : "Przegrana",
                        team = team,
                        opponents = opponents,
                    });
                }
                
            }
            stopwatch.Stop();
            Console.Clear();
            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);

            return matchDetailsList;

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
