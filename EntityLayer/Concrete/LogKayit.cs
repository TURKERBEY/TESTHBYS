using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
  public  class LogKayit
    {
        [Key]
        public int? id { get; set; }
        public int Tablo_id { get; set; }
        public int Tablo__Identity { get; set; }
        public int kullanici_id { get; set; }
        public string islemtur { get; set; }

        public string Aciklama { get; set; }

        public DateTime Tarih { get; set; }
    }
}
