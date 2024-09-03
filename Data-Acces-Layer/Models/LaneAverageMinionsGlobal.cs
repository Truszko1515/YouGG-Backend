using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Acces_Layer.Models
{
    public class LaneAverageMinionsGlobal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(15)]
        [Column(TypeName = "VARCHAR(15)")]
        public string LaneName { get; set; }

        [Required]
        public float AverageMonstersKilled { get; set; }
    }
}
