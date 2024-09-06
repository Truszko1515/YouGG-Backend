using Business_Logic_Layer.Dtos;
using Data_Acces_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Acces_Layer.Interfaces
{
    public interface IGlobalStatsRepository
    {
        Task<bool> TryAddChampionDataAsync(MatchStatisticGlobal championDataGlobal);
        IQueryable<MatchStatisticGlobal> GetChampionStatisticsQuery(ChampionStatisticsRequestDto request);

        Task<ChampionStatisticsResult> ExecuteChampionStatisticsQueryAsync(
            IQueryable<MatchStatisticGlobal> query,
            SelectedColumns selectedColumns);
    }
}
