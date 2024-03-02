using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
  public  class Kullanicilar
    {
       
        public int id { get; set; }
        public string Ad { get; set; }
        public string soyad { get; set; }
        public string KullaniciAd { get; set; }
        public int? Personel_İd { get; set; }

        public bool  Aktif { get; set; }
        public string sifre { get; set; }

        public string sifrelemeTur { get; set; }
        public string Email { get; set; }
        public DateTime? KayitZamani { get; set; }
        public int? EkleyenKullanici_id { get; set; }
        public DateTime? GuncellemeZamani { get; set; }

        public int? GuncelleyenKullanici_id { get; set; }
        public int? Yetki_Grup_ıd { get; set; }
    }
}
