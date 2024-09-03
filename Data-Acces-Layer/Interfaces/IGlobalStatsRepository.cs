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
    }
}
