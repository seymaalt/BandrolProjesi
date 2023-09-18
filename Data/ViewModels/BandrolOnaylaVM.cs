using ProjeBandrol.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace ProjeBandrol.Data.ViewModels
{
    public class BandrolOnaylaVM
    {
        public int BandrolId { get; set; }

        public ApplicationStatusEnum BasvuruDurumu { get; set; }

        public string Ad { get; set; }


        public string Soyad { get; set; }


        public string TcKimlikNo { get; set; }

        public string DekontNo { get; set; }


        public string SeriNo { get; set; }

     
        public string Renk { get; set; }

     
        public string Model { get; set; }

       
        public string Plaka { get; set; }

        public UserType KullaniciTipi { get; set; }
    }
}
