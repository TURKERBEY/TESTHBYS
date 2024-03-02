using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
  public  class kullanici_Aktive
    {
        [Key]
        public int? id { get; set; }
        public int? kullanici_id { get; set; }
        public  string key { get; set; }

        public DateTime? Sure { get; set; }
        public DateTime? AktifSure { get; set; }

        public bool? aktif { get; set; }

    }
}
