using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer.Repositories
{
    public class LoginRepository : ILoginDal
    {
        readonly Context c = new Context();
        public void AddKullanici(Kullanicilar kullanici)
        {
            c.Add(kullanici);
            c.SaveChanges();
        }

        public void DeleteKullanici(Kullanicilar kullanici)
        {
           
            c.Remove(kullanici);
            c.SaveChanges();
        }

        public Kullanicilar GetbyId(int id)
        {
            throw new NotImplementedException();
        }

        //public Kullanici GetbyId(int id)
        //{
        //    return c.Kullanicilist.Find(id);
        //}

        public Kullanicilar Getbykullanici(string kullaniciAd, string sifre)
        {
            using var c = new Context();
            return c.Kullanicilar.Where(x => x.KullaniciAd == kullaniciAd).FirstOrDefault();
        
        }

        //public Kullanici Getbykullanici(Kullanici kullanici)
        //{
        //    using var c = new Context();
        //    return c.Find(, kullanici.KullaniciAd);
        //}

        public List<Kullanicilar> kullaniciAllList()
        {
            throw new NotImplementedException();
        }

        public void UpdateKullanici(Kullanicilar kullanici)
        {
            c.Update(kullanici);
            c.SaveChanges();
        }
    }
}
