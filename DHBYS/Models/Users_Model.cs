using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Models
{
    public class Users_Model
    {
   
        public string Kullanici_İd { get; set; }
        public string KullaniciAdSoyad { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EMail { get; set; }

        public string Yetki_GrupTur { get; set; }
    }
}
