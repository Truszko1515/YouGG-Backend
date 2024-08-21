using Business_Logic_Layer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Interfaces
{
    public interface ISummonerLeagueService
    {
        Task<LeagueEntryDto> GetLeagueEntry(string SummonerId);
    }
}
