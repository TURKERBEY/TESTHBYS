using com.sun.org.apache.xml.@internal.resolver.helpers;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace WEB.Models
{
    public class YetkiKullaniciModel
    {
        public int AnaModul { get; set; }
        public int UstModul { get; set; }
        public int kullanici_id { get; set; }
        
        
        public List<Modul> Anamoduls;
        public List<Modul> Ustmoduls;
        public List<Modul> moduls;
        public List<kullanici_Yetki> Kullanici_Yetki;
        public List<Kullanicilar> kullanicilars;



        public List<YetkiModulKullaniciListesiModel> YetkiEklenecekListe { get; set; }

        public List<YetkiModulOzelYetki> OzelYetkiEklenecekListe { get; set; }

        

        public class YetkiModulOzelYetki
        {

            public int id { get; set; }
            public int İslemTip { get; set; } //1 Güncelleme Yetkisi 2: Kaydet Yetkisi 3:Sil Yetkisi

        }

            public class YetkiModulKullaniciListesiModel
        {
           
            public int Kullanici_id { get; set; }
            public int modul_id { get; set; }
        }

        
    }
}
