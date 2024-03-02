using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
namespace WEB.Models
{
    public class HastaAramaModel
    {

        public Decimal? Tckimlik { get; set; }
        public int? Dosyano { get; set; }
        public int? Basvuruno { get; set; }

        public int? İslemTur { get; set; } //islemTur=1 Hasta Bilgisi Bulunamadi  2:(Hasta Kaydedildi) 3:Hasta kaydedildi uyarısı Atıldı 
        //4:hasta Düzenleme Ekranına Atıldı
        public string Message { get; set; }
		public string Adi { get; set; }

        public string Soyadi { get; set; }
        public DateTime? BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }

        public List<Hasta> Hastalistesi { get; set; }
        public List<HastaKayitTur> HastaKayitTur { get; set; }

        public List<Ulke> UyrukTur { get; set; }
    }
}
