using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using EntityLayer.Models;
 
 
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
 


namespace DataAccessLayer.Utilities
{

    public class Opening
    {
       

       

        public ModulHeaderVMData YetkiEkraniModulGetirİslemi()
        {
            
        

            TanimGenericRepositories<Modul> ModulGenericRepositories = new();
            TanimGenericRepositories<kullanici_Yetki> kullanici_YetkiGenericRepositories = new();
            TanimGenericRepositories<Kullanicilar> kullanici_GenericRepositories = new();



            ModulHeaderVMData modulHeaderVM = new ModulHeaderVMData();

            modulHeaderVM.Kullanici_Yetki = kullanici_YetkiGenericRepositories.GetlistAll();

            modulHeaderVM.Kullanicilar = kullanici_GenericRepositories.GetlistAll().FindAll(x => x.Aktif == true);


            var modul = ModulGenericRepositories.GetlistAll().FindAll(x => x.Aktif == true);

           
            var Anamodul = modul.FindAll(x => x.UstModul_ID is null); // Ana Modulleri Ayırır
 

            modulHeaderVM.Anamoduls = Anamodul;  // Ana Modulleri alır

            var moduls = modul.FindAll(x => x.UstModul_ID is not null);  //  Modulleri ayırır

     
            modulHeaderVM.Ustmoduls = moduls.ToList();
            modulHeaderVM.moduls = moduls.ToList();

            foreach (var item in modulHeaderVM.moduls)
            {
                foreach (var item2 in modulHeaderVM.Anamoduls.ToList())
                {
                    if (moduls.Find(X => X.UstModul_ID == item2.id) is null)
                    {
                        modulHeaderVM.Ustmoduls.RemoveAll(X => X.UstModul_ID == item.id);

                    }
                }
            }

            foreach (var item in modulHeaderVM.moduls.ToList())
            {
                foreach (var item2 in modulHeaderVM.Ustmoduls.ToList())
                {
                    if (moduls.Find(X => X.UstModul_ID == item2.id) is not null)
                    {
                        //modulHeaderVM.moduls.RemoveAll(X => X.UstModul_ID == item2.id);
                        modulHeaderVM.moduls.RemoveAll(X => X.id == item2.id);

                    }

                }
            }

            return modulHeaderVM;
        }
        public ModulHeaderVMData Modulİslemleri(Kullanicilar kullanicilar)
        {

            TanimGenericRepositories<kullanici_Yetki> kullanici_YetkiGenericRepositories = new();
            TanimGenericRepositories<YetkiGrupTur> YetkiTur_GenericRepositories = new();
			TanimGenericRepositories<Tema> Tema = new();
			YetkiGrupTur YetkiGrupAdi = new();
            YetkiGrupAdi = YetkiTur_GenericRepositories.GetByID(kullanicilar.Yetki_Grup_ıd.GetValueOrDefault());
            var kullanici_yetki = kullanici_YetkiGenericRepositories.GetlistAll().FindAll(x => x.Kullanici_ID == int.Parse(kullanicilar.id.ToString())).FindAll(x => x.BitisTarihi.GetValueOrDefault(DateTime.Now) <= DateTime.Now);

            var TemaDark = Tema.GetlistAll().Find(x => x.Kullanici_id == kullanicilar.id);



			TanimGenericRepositories<Modul> ModulGenericRepositories = new();

            ModulHeaderVMData modulHeaderVM = new ModulHeaderVMData();

            var modul = ModulGenericRepositories.GetlistAll().FindAll(x => x.Aktif == true);
            
            modulHeaderVM.KullaniciAdSoyad = kullanicilar.Ad + ' ' + kullanicilar.soyad;
            modulHeaderVM.kullanici_id = kullanicilar.id.ToString();
            modulHeaderVM.KullaniciYetkiGrupAdi = YetkiGrupAdi.Adi;
            modulHeaderVM.Kullanici_Yetki = kullanici_yetki;
             if (TemaDark is not null)
            {
                modulHeaderVM.TemaDark = TemaDark.Dark;

			}
            var Anamodul = modul.FindAll(x => x.UstModul_ID is null); // Ana Modulleri Ayırır

            foreach (var item in Anamodul.ToList())   // Ana Modulleri alır
            {
                if (kullanici_yetki.Find(X => X.Modul_ID == item.id) is null)
                {

                    Anamodul.RemoveAll(x => x.id == item.id);

                }
            }
            modulHeaderVM.Anamoduls = Anamodul;  // Ana Modulleri alır

            var moduls = modul.FindAll(x => x.UstModul_ID is not null);  //  Modulleri ayırır

            foreach (var item in moduls.ToList())
            {
                if (kullanici_yetki.Find(X => X.Modul_ID == item.id) is null)
                {

                    moduls.RemoveAll(x => x.id == item.id);

                }
            }
            modulHeaderVM.Ustmoduls = moduls.ToList();
            modulHeaderVM.moduls = moduls.ToList();

            foreach (var item in modulHeaderVM.moduls)
            {
                foreach (var item2 in modulHeaderVM.Anamoduls.ToList())
                {
                    if (moduls.Find(X => X.UstModul_ID == item2.id) is null)
                    {
                        modulHeaderVM.Ustmoduls.RemoveAll(X => X.UstModul_ID == item.id);

                    }
                }
            }

            foreach (var item in modulHeaderVM.moduls.ToList())
            {
                foreach (var item2 in modulHeaderVM.Ustmoduls.ToList())
                {
                    if (moduls.Find(X => X.UstModul_ID == item2.id) is not null)
                    {
                        //modulHeaderVM.moduls.RemoveAll(X => X.UstModul_ID == item2.id);
                        modulHeaderVM.moduls.RemoveAll(X => X.id == item2.id);
                        
                    }

                }
            }

            return modulHeaderVM;

        }



    }
}
