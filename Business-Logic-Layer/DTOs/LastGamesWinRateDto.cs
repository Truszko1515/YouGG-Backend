using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Dtos
{
    public class LastGamesWinRateDto
    {
        public int gamesPlayed { get; set; } = 20;
        public int wins { get; set; } = 0;
        public int losses { get; set; } = 0;
    }
}
