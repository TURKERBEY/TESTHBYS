using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
  public  class Hasta
    {




        [Key]
        public int ıd { get; set; }

		public int? HastaKayitTip_id { get; set; }
		 
		public int Uyruk_id { get; set; }
		public string Kimliksiz_HastaBilgisi { get; set; }
		public Decimal TC { get; set; }
		 
		public string Ad { get; set; }
        public string Soyad { get; set; }


		public DateTime? Beyan_DogumTarih { get; set; }
		public DateTime? DogumTarih { get; set; }
        public string Cinsiyet { get; set; }
		public string DogumYeri { get; set; }

		public string AnneTc { get; set; }
		public string BabaTc { get; set; }
		public string AnneDosyano { get; set; }
		public string BabaDosyano { get; set; }
		public string DogumSirasi { get; set; }
		public string AnneAdi { get; set; }
		public string BabaAdi { get; set; }
		public string MedeniHal { get; set; }
		public string Meslek { get; set; }
		public string OgrenimDurumu { get; set; }

		public int? KanGrubu_id { get; set; }
		public int? MuayeneOncelikSirasi_id { get; set; }
		public string EngellilikDurumu { get; set; }
		public DateTime? OlumTarihi { get; set; }
		public string OlumYeri { get; set; }
		public string Telefon { get; set; }
		public string Adres { get; set; }
		public string PasaportNo { get; set; }
		public string Yupass { get; set; }

		public int? SonKurumKodu { get; set; }
		public DateTime? Kayit_Zamani { get; set; }

		public int? EkleyenKullanici_id { get; set; }
		public DateTime? GuncellemeZamani { get; set; }
		public int? GuncelleyenKulanici_id { get; set; }
		
        

    }
}
