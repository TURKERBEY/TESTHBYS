using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Models
{
    public class versiyonnotlariModel
    {
        public List<VersiyonModel> VersiyonNotlarilistesi;


        public class VersiyonModel
        {
            public string versiyon { get; set; }
            public string not { get; set; }

        }

    }
}
