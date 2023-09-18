using ProjeBandrol.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace ProjeBandrol.Data.ViewModels
{
    public class OdemeVM
    {
        public int Id { get; set; }


        public string DekontNo { get; set; }

        public int aracId { get; set; }
        public UserType KullaniciTipi { get; set; }
    }
}
