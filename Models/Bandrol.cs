using BandrolSistemi.Data.Enum;
using BandrolSistemi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjeBandrol.Models
{
    public class Bandrol
    {
        [Key]
        public int Id { get; set; }

        public string BandrolNo { get; set; }
        public int VehicleId { get; set; }
        public string UserId { get; set; }



        [ForeignKey("VehicleId")]
        public virtual Vehicle Vehicle { get; set; } = null!;


        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }
    }
}
