using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Dtos
{
    public class SummonerLeagueEntryDto
    {
        public string? summonerId { get; set; }
        public string? leagueId { get; set; }
        public string? tier { get; set; }
        public string? rank { get; set; }
        public int wins { get; set; }
        public int losses { get; set; }
        public int leaguePoints { get; set; }
        public string? queueType { get; set; }

    }
}
