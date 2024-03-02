using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
	using System;

	public class Personel
	{

		public int ID { get; set; }
		public string AD { get; set; }
		public string SOYADI { get; set; }
		public string ONCEKI_SOYADI { get; set; }
		public DateTime? DOGUM_TARIHI { get; set; }
		public string DOGUM_YERI { get; set; }
		public string CINSIYET { get; set; }
		public string ANNE_ADI { get; set; }
		public string BABA_ADI { get; set; }
		public string EV_TELEFONU { get; set; }
		public string CEP_TELEFONU { get; set; }
		public string EPOSTA_ADRESI { get; set; }
		public string OGRENIM_DURUMU { get; set; }
		public string TC_KIMLIK_NUMARASI { get; set; }
		public string KAN_GRUBU { get; set; }
		public string ULKE_KODU { get; set; }
		public string ADRES_TIPI { get; set; }
		public string ADRES_KODU_SEVIYESI { get; set; }
		public string ACIK_ADRES { get; set; }
		public string IL_KODU { get; set; }
		public string ILCE_KODU { get; set; }
		public string PERSONEL_SICIL_NUMARASI { get; set; }
		public string EMEKLI_SICIL_NUMARASI { get; set; }
		public string ASKERLIK_DURUMU { get; set; }
		public string KLINIK_KODU { get; set; }
		public string TESCIL_NUMARASI { get; set; }
		public string DIPLOMA_NUMARASI { get; set; }
		public string MEDULA_BRANS_KODU { get; set; }
		public string CALISMA_DURUMU { get; set; }
		public string UNVAN_KODU { get; set; }
		public string PERSONEL_GOREV_KODU { get; set; }
		public string IS_DURUMU { get; set; }
		public string MEMURIYET_TIPI { get; set; }
		public DateTime? SAGLIK_TESISINE_BASLAMA_TARIHI { get; set; }
		public DateTime? ASALET_ALMA_TARIHI { get; set; }
		public DateTime? TERFI_TARIHI { get; set; }
		public DateTime? EMEKLI_TERFI_TARIHI { get; set; }
		public DateTime? ILK_ISE_BASLAMA_TARIHI { get; set; }
		public DateTime? ISTEN_AYRILMA_TARIHI { get; set; }
		public string ISTEN_AYRILMA_NEDENI { get; set; }
		public string ISTEN_AYRILMA_ACIKLAMASI { get; set; }
		public DateTime? MEMURIYETE_BASLAMA_TARIHI { get; set; }
		public string KADRO_UNVAN_KODU { get; set; }
		public string IMZA_UNVAN_KODU { get; set; }
		public string AKADEMIK_UNVAN { get; set; }
		public string CALISTIGI_BIRIM_KODU { get; set; }
		public string KADROLU_GOREV_YERI { get; set; }
		public string ENGELLILIK_DURUMU { get; set; }
		public string DEVLET_HIZMET_YUKUMLULUK_KODU { get; set; }
		public bool? AKTIFLIK_BILGISI { get; set; }
		public byte[] FOTOGRAF_BILGISI { get; set; }
		public string FOTOGRAF_DOSYA_YOLU { get; set; }
		public string HEKIM_MEDULA_SIFRESI { get; set; }
		public DateTime? KAYIT_ZAMANI { get; set; }
		public string EKLEYEN_KULLANICI_KODU { get; set; }
		public DateTime? GUNCELLEME_ZAMANI { get; set; }
		public string GUNCELLEYEN_KULLANICI_KODU { get; set; }
	}

}
