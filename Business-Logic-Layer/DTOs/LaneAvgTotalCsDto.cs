using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Dtos
{
    public class LaneAvgTotalCsDto
    {
        public string LaneName { get; set; } = string.Empty;
        public double AvgTotalCS { get; set; }
    }
}
