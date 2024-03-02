 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 


namespace EntityLayer.Concrete
{
   public class kullanici_Yetki
    {
         [Key]
        public int? id { get; set; }
        public int Kullanici_ID { get; set; }
        
        public int? Modul_ID { get; set; }
        
        
       
        public bool? SayfadaIslemYapabilsinmi { get; set; }
        public bool? KaydetYetkisi { get; set; }
        public bool? SilYetkisi { get; set; }
     
        public DateTime? BitisTarihi { get; set; }
    }
}
