using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Modul
    {
        public int id { get; set; }
        public int? UstModul_ID { get; set; }
        
        public string Adi { get; set; }
        public string Aciklama { get; set; }
        public bool? Aktif { get; set; }
        public int? Birim_ID { get; set; }
        
        public string Controller { get; set; }

        public string Action { get; set; }

    }
}
