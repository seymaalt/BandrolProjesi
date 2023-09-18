using ProjeBandrol.Data.Enum;

namespace ProjeBandrol.Data.ViewModels
{
    public class BandrolListeVM
    {
        public string Plaka { get; set; }

        public string BandrolNo { get; set; }
      
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }

        public bool Aktiflik { get; set; }

        public ApplicationStatusEnum BasvuruDurumu { get; set; }

    }
}
