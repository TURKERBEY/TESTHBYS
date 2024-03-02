using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
   public class LogTabloTanim
    {
        public int id { get; set; }
        public int Tablo_id { get; set; }

        public bool GoruntulemeLogu { get; set; }
        public bool SilmeLogu { get; set; }
        public bool KayitLogu { get; set; }
        public bool GuncellemeLogu { get; set; }
    }
}
