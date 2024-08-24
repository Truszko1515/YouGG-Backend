using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Dtos
{
    public class SummonerMasteryDto
    {
        public long championId { get; set; }
        public string name { get; set; } = string.Empty;
        public int championLevel { get; set; }
        public int championPoints { get; set; }
        public int masteryLevel { get; set; }
        public int points { get; set; }
    }
}
