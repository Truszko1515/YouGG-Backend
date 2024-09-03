using Business_Logic_Layer.Helpers;
using Business_Logic_Layer.Interfaces.GlobalStats;
using Data_Acces_Layer;
using Data_Acces_Layer.Interfaces;
using Data_Acces_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.InsertingGlobalStatsServices
{
    public class ChampionsStatisticService : IChampionsStatisticService
    {
        private readonly IGlobalStatsRepository _globalStatsRepository;
        private readonly ApplicationDbContext _dbContext;
        public ChampionsStatisticService(IGlobalStatsRepository globalStatsRepository,
                                         ApplicationDbContext dbContext)
        {
            _globalStatsRepository = globalStatsRepository;
            _dbContext = dbContext;
        }

        public async Task<(bool result, int championsInserted)> TryAddChampionDataAsync(List<MatchDto> matches)
        {
            int championsInserted = 0;

            try
            {
                foreach (var match in matches)
                {
                    var participants = match.info.participants;

                    foreach (var p in participants)
                    {
                        var championToInsert = new MatchStatisticGlobal();

                        championToInsert.MatchId = match.metadata.matchId;
                        championToInsert.ChampionName = p.championName;
                        championToInsert.MinionsFirst10Minutes = p.challenges.laneMinionsFirst10Minutes;
                        championToInsert.ControlWardsPlaced = p.challenges.controlWardsPlaced;
                        championToInsert.Kills = p.kills;
                        championToInsert.Assists = p.assists;
                        championToInsert.Deaths = p.deaths;
                        championToInsert.GameLength = GameDurationHelper.GetGameDuration(match.info.gameDuration);
                        championToInsert.TeamPosition = p.teamPosition;

                        if (p.teamPosition == "JUNGLE")
                            championToInsert.MinionsFirst10Minutes = p.totalAllyJungleMinionsKilled + p.totalEnemyJungleMinionsKilled;

                        bool success = await _globalStatsRepository.TryAddChampionDataAsync(championToInsert);

                        if (!success)
                        {
                            return (false, championsInserted); 
                        }

                        championsInserted++;
                    }
                }
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new HttpRequestException("błąd przy insertowaniu statystyk bohatera", null, HttpStatusCode.InternalServerError);
            }

            return (true, championsInserted);    
        }
    }
}
