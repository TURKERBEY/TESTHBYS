using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Models
{
  public  class ModulHeaderVMData
    {

        public int? UST_Modulid { get; set; }
        public string KullaniciAdSoyad { get; set; }

        public string kullanici_id { get; set; }

        public string KullaniciYetkiGrupAdi { get; set; }
        public string Ust_ModulAdi { get; set; }
        public int Modulid { get; set; }
        public string ModulAdi { get; set; }
        public DateTime? YetkiSuresi { get; set; }

        public bool TemaDark { get; set; }

		//ToolBar için

		public List<Modul> Anamoduls;
        public List<Modul> Ustmoduls;
        public List<Modul> moduls;
        public List<kullanici_Yetki> Kullanici_Yetki;
        public List<Kullanicilar> Kullanicilar;
      

    }
}
