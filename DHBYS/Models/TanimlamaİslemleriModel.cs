 
using System.Collections.Generic;
using EntityLayer.Concrete;
namespace WEB.Models
{
	public class TanimlamaİslemleriModel
	{
        public int Tanimlama_kullanici_ID { get; set; }
		public bool TanimlamaKullaniciGuncelleme { get; set; }	
         
		public List<Kullanicilar> KullaniciListesi { get; set; }

		public List<YetkiGrupTur> YetkiGrupTurs { get; set; }

		public List<Personel> Personels { get; set; }
	}
}
