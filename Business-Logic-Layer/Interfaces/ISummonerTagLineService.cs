using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Interfaces
{
    public interface ISummonerTagLineService
    {
        Task<string> GetSummonerTagLine(string SummonerPUUID);
    }
}
