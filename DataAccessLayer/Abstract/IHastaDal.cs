using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
   public interface IHastaDal
    {
        List<Hasta> ListAllHastaAdsoyad(Hasta hasta, string Ad,string soyadi, int kullanici);
        List<Hasta> ListAllHastaTC(Hasta hasta, Decimal TC, int kullanici);
        List<Hasta> ListAllHastaDosya(Hasta hasta, int TC,  int kullanici);
        void AddHasta(Hasta hasta, int kullanici);
        //void DeleteHasta(Hasta hasta, int kullanici);
        void UpdateHasta(Hasta hasta, int kullanici);

        //Hasta GetById(int id, int kullanici);

    }
}
