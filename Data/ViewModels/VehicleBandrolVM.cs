using ProjeBandrol.Data.Enum;
using ProjeBandrol.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ProjeBandrol.Data.ViewModels
{
    public class VehicleBandrolVM
    {
      
        public int Id { get; set; }

        [Display(Name = "Seri No")]
        [Required(ErrorMessage = "Name is required")]
        public string SeriNo { get; set; }

        [Display(Name = "Renk")]
        [Required(ErrorMessage = "Name is required")]
        public string Renk { get; set; }

        [Display(Name = "Model")]
        [Required(ErrorMessage = "Name is required")]
        public string Model { get; set; }

        [Display(Name = "Plaka")]
        [Required(ErrorMessage = "Name is required")]
        public string Plaka { get; set; }


        [Display(Name = "BandrolDurumuTxt")]
        public string BandrolDurumuTxt { get; set; }

        [Display(Name = "BandrolDurumu")]
        [Required(ErrorMessage = "Name is required")]
        public bool BandrolDurumu { get; set; }

        [Display(Name = "KullaniciTipi")]
        [Required(ErrorMessage = "Name is required")]
        public UserType KullaniciTipi { get; set; }

    }
}
