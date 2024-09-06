using Business_Logic_Layer.Dtos;
using Business_Logic_Layer.Interfaces.GlobalStats;
using Data_Acces_Layer;
using Data_Acces_Layer.Interfaces;
using Data_Acces_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.GlobalStatsServices
{
    public class ChampionsStatisticService : IChampionsStatisticService
    {
        private readonly IGlobalStatsRepository _globalStatsRepository;

        public ChampionsStatisticService(IGlobalStatsRepository globalStatsRepository)
        {
            _globalStatsRepository = globalStatsRepository;
        }

        public async Task<ChampionStatisticsResult?> GetChampionStatisticsAsync(ChampionStatisticsRequestDto request)
        {
            // Tworzymy dynamiczne zapytanie bazujące na wybranych kolumnach i filtrach
            var query = _globalStatsRepository.GetChampionStatisticsQuery(request);

            // Filtrujemy według wybranych filtrów
            if (request.Filters != null)
            {
                query = ApplyFilters(query, request.Filters);
            }

            // Pobieramy wyniki na podstawie wybranych kolumn
            var result = await _globalStatsRepository.ExecuteChampionStatisticsQueryAsync(query, request.SelectedColumns);

            if(result != null) 
                return result;

            return null;
        }

        private IQueryable<MatchStatisticGlobal> ApplyFilters(IQueryable<MatchStatisticGlobal> query, Filters filters)
        {
            // Przykładowe filtry (rozwiń zgodnie z potrzebami)
            if (filters.Kills != null)
            {
                query = filters.Kills.Comparison == "greater"
                    ? query.Where(x => x.Kills > filters.Kills.Value)
                    : query.Where(x => x.Kills < filters.Kills.Value);
            }

            if (filters.GameLength != null)
            {
                query = filters.GameLength.Comparison == "greater"
                    ? query.Where(x => x.GameLengthSeconds > filters.GameLength.Value * 60)
                    : query.Where(x => x.GameLengthSeconds < filters.GameLength.Value * 60);
            }

            if (filters.TotalCS != null)
            {
                query = filters.TotalCS.Comparison == "greater"
                    ? query.Where(x => x.TotalCS > filters.TotalCS.Value)
                    : query.Where(x => x.TotalCS < filters.TotalCS.Value);
            }

            if (filters.VisionScore != null)
            {
                query = filters.VisionScore.Comparison == "greater"
                    ? query.Where(x => x.VisionScore > filters.VisionScore.Value)
                    : query.Where(x => x.VisionScore < filters.VisionScore.Value);
            }

            if (filters.TotalDamageDealt != null)
            {
                query = filters.TotalDamageDealt.Comparison == "greater"
                    ? query.Where(x => x.TotalDamageDealtToChampions > filters.TotalDamageDealt.Value)
                    : query.Where(x => x.TotalDamageDealtToChampions < filters.TotalDamageDealt.Value);
            }
            
            if (filters.Lane != null)
            {
                query = query.Where(x => x.TeamPosition == filters.Lane.Value);
            }

            if (filters.Result != null)
            {
                query = filters.Result.Value == "Win"
                    ? query.Where(x => x.Win == true)
                    : query.Where(x => x.Win == false); 
            }


            // Dodaj inne filtry w podobny sposób
            return query;
        }

    }
}
