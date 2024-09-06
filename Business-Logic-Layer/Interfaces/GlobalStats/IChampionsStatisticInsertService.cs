using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Interfaces.GlobalStats
{
    public interface IChampionsStatisticInsertService
    {
        Task<(bool result, int championsInserted)> TryAddChampionDataAsync(List<MatchDto> matches);
    }
}
