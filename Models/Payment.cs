using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjeBandrol.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public string DekontNo { get; set; }

        [Required]
        public string UserId { get; set; }
        [Required]
        public int VehicleId { get; set; }
        [Required]
    

        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }

        [ForeignKey("VehicleId")]
        public virtual Vehicle Vehicle { get; set; } = null!;



    }
}
