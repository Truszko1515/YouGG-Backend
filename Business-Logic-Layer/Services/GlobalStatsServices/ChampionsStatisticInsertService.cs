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

namespace Business_Logic_Layer.Services.GlobalStatsServices
{
    public class ChampionsStatisticInsertService : IChampionsStatisticInsertService
    {
        private readonly IGlobalStatsRepository _globalStatsRepository;
        private readonly ApplicationDbContext _dbContext;
        public ChampionsStatisticInsertService(IGlobalStatsRepository globalStatsRepository,
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

                    foreach (var p in participants.Where(p => !p.gameEndedInEarlySurrender) )
                    {
                        var championToInsert = new MatchStatisticGlobal();

                        championToInsert.MatchId = match.metadata.matchId;
                        championToInsert.ChampionName = p.championName;
                        championToInsert.MinionsFirst10Minutes = p.challenges.laneMinionsFirst10Minutes;
                        championToInsert.ControlWardsPlaced = p.challenges.controlWardsPlaced;
                        championToInsert.Kills = p.kills;
                        championToInsert.Assists = p.assists;
                        championToInsert.Deaths = p.deaths;
                        championToInsert.GameLengthString = GameDurationHelper.GetGameDuration(match.info.gameDuration);
                        championToInsert.GameLengthSeconds = match.info.gameDuration;
                        championToInsert.TeamPosition = p.teamPosition;
                        championToInsert.TotalCS = p.totalMinionsKilled + p.neutralMinionsKilled;
                        championToInsert.CSperMinute = SummonerMatchDetailsHelper.GetCSperMinute(p.totalMinionsKilled, p.neutralMinionsKilled, match.info.gameDuration);
                        championToInsert.Win = p.win;
                        championToInsert.VisionScore = p.visionScore;
                        championToInsert.TotalDamageDealtToChampions = p.totalDamageDealtToChampions;
                        championToInsert.WardTakedowns = p.challenges.wardTakedowns;
    

                        if (p.teamPosition == "JUNGLE")
                        {
                            championToInsert.MinionsFirst10Minutes = (int)p.challenges.jungleCsBefore10Minutes;
                            championToInsert.TotalCS = p.neutralMinionsKilled + p.totalMinionsKilled;
                        }
                            
                        bool success = await _globalStatsRepository.TryAddChampionDataAsync(championToInsert);

                        if (success)
                        {
                            championsInserted++;
                        }
                    }
                }

                await _dbContext.SaveChangesAsync();

                if(championsInserted == 0)
                    return (false, championsInserted);
            }
            catch
            {
                throw new HttpRequestException("błąd przy insertowaniu statystyk bohatera", null, HttpStatusCode.InternalServerError);
            }

            return (true, championsInserted);    
        }
    }
}
