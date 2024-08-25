using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Dtos
{
    public class SummonerChampionsPlayRateDto
    {
        public string? name { get; set; }
        public int winRate { get; set; }
        public int wins { get; set; }
        public int losses { get; set; }
        public float kda { get; set; }
    }
}
