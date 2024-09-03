using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Acces_Layer.Models
{
    public class SummonerDataExpiration
    {
        [Key]
        [Required]
        [MaxLength(35)]
        [Column(TypeName = "NVARCHAR(35)")]
        public string SummonerName { get; set; }

        [Required]
        public DateTime NextTimeAllowed { get; set; }
        
    }
}
