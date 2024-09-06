using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Dtos
{
    public class ChampionStatisticsResult
    {
        public string ChampionName { get; set; } = string.Empty;
        public double? Kills { get; set; }
        public double? Deaths { get; set; }
        public double? Assists { get; set; }
        public double? WinRatio { get; set; }
        public double? MinionsFirst10Minutes { get; set; }
        public double? TotalCS { get; set; }
        public double? CSperMinute { get; set; }
        public double? KDA { get; set; }
        public double? DmgDealt { get; set; }
        public double? VisionScore { get; set; }
        public double? GameLength { get; set; }
        public int? MatchesOnGivenChamp { get; set; }
        public int? MatchesAfterFilters { get; set; }

    }

}
