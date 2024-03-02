using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Utilities.Log;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class HastaRepositories : IHastaDal
    {
        readonly Logging<Hasta> logging = new ();
        
        
        public void AddHasta(Hasta hasta , int kullanici)
        {
            using var c = new Context();

            if (hasta is not null)
            {
                logging.insertlogkayit(hasta, kullanici);
            }
            c.Add(hasta);
            c.SaveChanges();
        }

		public void UpdateHasta(Hasta hasta, int kullanici)
		{

			using var c = new Context();
			if (hasta is not null)
			{
                logging.Updatelogkayit(hasta, kullanici);
            }
			c.Update(hasta);
			c.SaveChanges();
		}

		public List<Hasta> ListAllHastaAdsoyad(Hasta hasta, string adi,string soyadi, int kullanici)
        {
            var c = new Context();

            if (c.Set<Hasta>() is not null)
            {

                var gelen = c.Set<Hasta>().ToList().FindAll(x => x.Ad == adi).FindAll(x => x.Soyad ==soyadi);
                if (gelen.Distinct().Count() == 1)
                {
                    hasta= gelen[0];
                    logging.Goruntulemelogkayit(hasta, kullanici);
                }
                

            }

            return c.Set<Hasta>().ToList().FindAll(x => x.Ad == adi).FindAll(x => x.Soyad == soyadi);
        }

        public List<Hasta> ListAllHastaDosya(Hasta hasta, int Dosyano, int kullanici)
        {
            var c = new Context();

            if (c.Set<Hasta>() is not null)
            {

                var gelen = c.Set<Hasta>().ToList().FindAll(x => x.ıd == Dosyano);
                if (gelen.Distinct().Count() == 1)
                {
                    hasta = gelen[0];
                    logging.Goruntulemelogkayit(hasta, kullanici);
                }


            }

            return c.Set<Hasta>().ToList().FindAll(x => x.ıd == Dosyano);
        }

        public List<Hasta> ListAllHastaTC(Hasta hasta, Decimal TC , int kullanici)
        {
            var c = new Context();

            if (c.Set<Hasta>() is not null)
            {

                var gelen = c.Set<Hasta>().ToList().FindAll(x => x.TC == TC);
                if (gelen.Distinct().Count() == 1)
                {
                    hasta = gelen[0];
                    logging.Goruntulemelogkayit(hasta, kullanici);
                }


            }

            return c.Set<Hasta>().ToList().FindAll(x => x.TC == TC);
        }

        
    }
}
