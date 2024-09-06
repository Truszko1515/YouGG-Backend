using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Dtos
{
    public class ChampionStatisticsRequestDto
    {
        public string ChampionName { get; set; }
        public SelectedColumns SelectedColumns { get; set; }
        public Filters? Filters { get; set; }
    }
    public class SelectedColumns
    {
        public bool WinRatio { get; set; }
        public bool MinionsFirst10Minutes { get; set; }
        public bool TotalCS { get; set; }
        public bool CSperMinute { get; set; }
        public bool KDA { get; set; }
        public bool VisionScore { get; set; }
        public bool Kills { get; set; }
        public bool Deaths { get; set; }
        public bool Assists { get; set; }
        public bool TotalDamageDealtToChampions { get; set; }
        public bool GameLength { get; set; }
    }

    public class Filters
    {
        public FilterValue? Kills { get; set; }
        public FilterValue? GameLength { get; set; }
        public FilterValue? TotalCS { get; set; }
        public FilterValue? VisionScore { get; set; }
        public FilterValue? TotalDamageDealt { get; set; }
        public FilterStringValue? Lane { get; set; }
        public FilterStringValue? Result { get; set; }
    }

    public class FilterValue
    {
        public int Value { get; set; }
        public string? Comparison { get; set; }
    }

    public class FilterStringValue
    {
        public string? Value { get; set; }
    }

}
