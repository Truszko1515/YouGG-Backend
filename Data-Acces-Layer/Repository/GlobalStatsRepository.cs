using Data_Acces_Layer.Interfaces;
using Data_Acces_Layer.Models;
using Business_Logic_Layer.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;

namespace Data_Acces_Layer.Repository
{
    public class GlobalStatsRepository : IGlobalStatsRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public GlobalStatsRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;
        public async Task<bool> TryAddChampionDataAsync(MatchStatisticGlobal championDataGlobal)
        {
            try
            {
                var count = await _dbContext.MatchesStatisticsGlobal
                                        .CountAsync(c => c.MatchId == championDataGlobal.MatchId);

                // jeżeli jest już w bazie 10 meczy o takim Match_id - oznacza to że dane z tego meczu 
                // zostały już uwzględnione w bazie.
                if (count >= 10) { return false; }

                 _dbContext.MatchesStatisticsGlobal.Add(championDataGlobal);

                return true;
            }
            catch
            {
                throw new HttpRequestException("Błąd przy insertowaniu meczu do globalnych statystyk"
                                                , null, HttpStatusCode.InternalServerError);
            }         
        }

        public IQueryable<MatchStatisticGlobal> GetChampionStatisticsQuery(ChampionStatisticsRequestDto request)
        {
            if (request.ChampionName == "Wukong")
                request.ChampionName = "MonkeyKing";
            // Tworzymy podstawowe zapytanie dla danego bohatera
            var query = _dbContext.MatchesStatisticsGlobal
                .Where(x => x.ChampionName == request.ChampionName);

            return query;
        }

        public async Task<ChampionStatisticsResult?> ExecuteChampionStatisticsQueryAsync(
            IQueryable<MatchStatisticGlobal> query,
            SelectedColumns selectedColumns)
        {
            // Grupa wyników po ChampionName
            var result = await query.GroupBy(x => x.ChampionName)
                .Select(g => new ChampionStatisticsResult
                {
                    ChampionName = g.Key == "FiddleSticks" ? "Fiddlesticks" : (g.Key == "MonkeyKing" ? "Wukong" : g.Key),
                    MatchesAfterFilters = g.Count(),
                    MatchesOnGivenChamp = _dbContext.MatchesStatisticsGlobal.Where(x => x.ChampionName == g.Key).Count(),

                    Kills = selectedColumns.Kills ? g.Average(x => x.Kills) : null,
                    Deaths = selectedColumns.Deaths ? g.Average(x => x.Deaths) : null,
                    Assists = selectedColumns.Assists ? g.Average(x => x.Assists) : null,
                    WinRatio = selectedColumns.WinRatio ? g.Average(x => x.Win ? 1 : 0) : null,
                    MinionsFirst10Minutes = selectedColumns.MinionsFirst10Minutes ? g.Average(x => x.MinionsFirst10Minutes) : null,
                    TotalCS = selectedColumns.TotalCS ? g.Average(x => x.TotalCS) : null,
                    CSperMinute = selectedColumns.CSperMinute ? g.Average(x => x.CSperMinute) : null,
                    KDA = selectedColumns.KDA ? g.Average(x => (x.Kills + x.Assists) / (x.Deaths == 0 ? 1 : x.Deaths)) : null,
                    DmgDealt = selectedColumns.TotalDamageDealtToChampions ? g.Average(x => x.TotalDamageDealtToChampions) : null,
                    VisionScore = selectedColumns.VisionScore ? g.Average(x => x.VisionScore) : null,
                    GameLength = selectedColumns.GameLength ? g.Average(x => x.GameLengthSeconds / 60.0) : null,
                    
                }).ToListAsync();

            if(result.Count != 0)
            return result[0];

            return null;
        }

    }
}
