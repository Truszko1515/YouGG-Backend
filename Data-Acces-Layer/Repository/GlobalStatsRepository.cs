using Data_Acces_Layer.Interfaces;
using Data_Acces_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
            catch(Exception ex)
            {
                throw new HttpRequestException(ex.InnerException.Message, null, HttpStatusCode.InternalServerError);
            }         
        }
    }
}
