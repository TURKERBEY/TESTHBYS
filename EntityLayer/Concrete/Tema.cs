using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
	public class Tema
	{
        public int ID { get; set; }
		public int Kullanici_id { get; set; }

		public bool Dark { get; set; }

	}
}
