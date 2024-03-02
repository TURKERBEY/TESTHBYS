using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
   public interface ILoginDal
    {
        List<Kullanicilar> kullaniciAllList();
        void AddKullanici(Kullanicilar kullanici);
        void DeleteKullanici(Kullanicilar kullanici);
        void UpdateKullanici(Kullanicilar kullanici);

        Kullanicilar GetbyId(int id);
        Kullanicilar Getbykullanici(string kullaniciAd , string sifre);
    }
}
