using EntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Utilities.YetkiKontrol
{
    public class YetkiKontrol
    {

        //ModulHeaderVMData _modulHeaderVM = new ModulHeaderVMData();
        //_modulHeaderVM = _memoryCache.Get<ModulHeaderVMData>(_httpContextAccessorSession.HttpContext.Session.GetString("User"));
        public bool KaydetYetkisi(ModulHeaderVMData _modulHeaderVM,string controller)
        {
            var Modul = _modulHeaderVM.moduls.Find(X => X.Controller == controller);
            var kullaniciYetki = _modulHeaderVM.Kullanici_Yetki.Find(X => X.Modul_ID == Modul.id);

            if (kullaniciYetki.KaydetYetkisi == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }




        public bool SilYetkisi(ModulHeaderVMData _modulHeaderVM, string controller)
        {
            var Modul = _modulHeaderVM.moduls.Find(X => X.Controller == controller);
            var kullaniciYetki = _modulHeaderVM.Kullanici_Yetki.Find(X => X.Modul_ID == Modul.id);

            if (kullaniciYetki.KaydetYetkisi == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool GuncellemeYetkisi(ModulHeaderVMData _modulHeaderVM, string controller)
        {
            var Modul = _modulHeaderVM.moduls.Find(X => X.Controller == controller);
            var kullaniciYetki = _modulHeaderVM.Kullanici_Yetki.Find(X => X.Modul_ID == Modul.id);

            if (kullaniciYetki.KaydetYetkisi == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}
