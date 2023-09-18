
using ProjeBandrol.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjeBandrol.Models
{
    public class Vehicle 
    {
        [Key]
        public int Id { get; set; }

        public string SeriNo { get; set; }
        public string Renk { get; set;}
        public string Model { get; set; }
        public string Plaka { get; set; }
        public string BandrolDurumuTxt { get; set; }
        public bool BandrolDurumu { get; set; }
        public UserType KullaniciTipi { get; set; }

       
        [Required]
        public string UserId { get; set; }

        public virtual Bandrol? Bandrol { get; set; }
        public virtual Payment? Payment { get; set; }
        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }


    }
}
