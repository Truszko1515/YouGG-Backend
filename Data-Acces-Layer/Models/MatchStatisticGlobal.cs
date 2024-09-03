using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Acces_Layer.Models
{
    public class MatchStatisticGlobal
    {
        public int id { get; set; }
        public string MatchId { get; set; }
        public string ChampionName { get; set; }
        public int MinionsFirst10Minutes { get; set; }
        public int ControlWardsPlaced { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public int Assists { get; set; }
        public string GameLengthString { get; set; }
        public int GameLengthSeconds { get; set; }
        public string TeamPosition { get; set; }
        public int TotalCS { get; set; }
        public float CSperMinute { get; set; }
        public bool Win { get; set; }
        public int VisionScore { get; set; }
        public int TotalDamageDealtToChampions { get; set; }
        public int WardTakedowns { get; set; }
    }
}





/*[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
[Key]
public int id { get; set; }

[Required]
[Column(TypeName = "NVARCHAR(25)")]
public string MatchId { get; set; }

[Required]
[Column(TypeName = "NVARCHAR(35)")]
public string ChampionName { get; set; }

[Required]
public int MinionsFirst10Minutes { get; set; }

[Required]
public int ControlWardsPlaced { get; set; }

[Required]
public int Kills { get; set; }

[Required]
public int Deaths { get; set; }

[Required]
public int Assists { get; set; }

[Required]
public string GameLength { get; set; }

[Required]
public string TeamPosition { get; set; }*/