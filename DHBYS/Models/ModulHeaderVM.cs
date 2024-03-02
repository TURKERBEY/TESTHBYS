﻿using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB.Models
{
    public class ModulHeaderVM
    {


        public int? UST_Modulid { get; set; }
        public string KullaniciAdSoyad { get; set; }

        public int kullanici_id { get; set; }

        public string KullaniciYetkiGrupAdi { get; set; }
        public string Ust_ModulAdi { get; set; }
        public int  Modulid { get; set; } 
        public string ModulAdi { get; set; } 
        public DateTime? YetkiSuresi { get; set; }


        public List<Modul>  Anamoduls;
        public List<Modul> Ustmoduls;
        public List<Modul> moduls;
        
    }

    

}
