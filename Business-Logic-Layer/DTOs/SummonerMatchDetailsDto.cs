using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Dtos
{
    public class SummonerMatchDetailsDto
    {
        public string type { get; set; } = "Ranked Solo";
        public string? timeAgo { get; set; } 
        public string? champion { get; set; } 
        public int kills { get; set; } 
        public int deaths { get; set; } 
        public int assists { get; set; } 
        public float kdaRatio { get; set; } 
        public int[]? items { get; set; }
        public string? spell1 { get; set; }
        public string? spell2 { get; set; }
        public string? runePrimary { get; set; }
        public string? runeSecondary { get; set; }
        public string? gameDuration { get; set; }
        public string? cs { get; set; } // CS 198 (8.4)
        public string? result { get; set; } // Win or lose
        public List<TeamMember>? team { get; set; } 
        public List<TeamMember>? opponents { get; set; } 
        
   
    }

    public class TeamMember
    {
        public string? name { get; set; }
        public string? champion { get; set; }
        public string? tagLine { get; set; }
        public string? fullName { get; set; }

    }
}


