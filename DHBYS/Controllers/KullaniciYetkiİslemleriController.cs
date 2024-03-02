using BusinessLayer.Utilities.Security;
using DataAccessLayer.Repositories;
using DataAccessLayer.Utilities;
using EntityLayer.Concrete;
using EntityLayer.Models;
using javax.naming;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NuGet.Packaging;
using System.Collections.Generic;
using System.Security.Principal;
using WEB.Models;

namespace WEB.Controllers
{
    public class KullaniciYetkiİslemleriController : Controller
    {
        readonly IMemoryCache _memoryCache;
        readonly IHttpContextAccessor _httpContextAccessorSession;
        public KullaniciYetkiİslemleriController(IHttpContextAccessor session, IMemoryCache memoryCache)
        {

            _memoryCache = memoryCache;

            _httpContextAccessorSession = session;
        }
        public bool SessionKontrol()
        {


            if (_httpContextAccessorSession.HttpContext.Session.GetString("User") is not null)
            {
                ModulHeaderVMData _modulHeaderVM = new();
                if (_memoryCache.Get<ModulHeaderVMData>(_httpContextAccessorSession.HttpContext.Session.GetString("User")) is not null)
                {
                    _modulHeaderVM = _memoryCache.Get<ModulHeaderVMData>(_httpContextAccessorSession.HttpContext.Session.GetString("User"));
                    if (_modulHeaderVM is not null)
                    {


                        kullanici_Aktive_Control aktive_Control = new();


                        if ((aktive_Control.kullanici_kontrol(_modulHeaderVM.kullanici_id, _httpContextAccessorSession.HttpContext.Session.GetString("User"))) == true)

                        {
                            aktive_Control.update(_modulHeaderVM.kullanici_id);
                            ViewData["SubHeader"] = _modulHeaderVM;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }

        }

        public void modulGetir()
        {
            Opening opening = new Opening();
            ModulHeaderVMData modulHeaderVM = new ModulHeaderVMData();



            modulHeaderVM = opening.YetkiEkraniModulGetirİslemi();

            YetkiKullaniciModel _yetkiKullaniciModel = new YetkiKullaniciModel();

            _yetkiKullaniciModel.Anamoduls = modulHeaderVM.Anamoduls;
            _yetkiKullaniciModel.Ustmoduls = modulHeaderVM.moduls;
            _yetkiKullaniciModel.moduls = modulHeaderVM.moduls;

            _yetkiKullaniciModel.Kullanici_Yetki = modulHeaderVM.Kullanici_Yetki;
            _yetkiKullaniciModel.kullanicilars = modulHeaderVM.Kullanicilar;


            ViewData["YetkiEkrani"] = _yetkiKullaniciModel;

        }


        public void YetkiVer(YetkiKullaniciModel yetkiKullaniciModel, bool kullanniciAt)
        {
            ModulHeaderVMData _modulHeaderVM = new ModulHeaderVMData();
            _modulHeaderVM = _memoryCache.Get<ModulHeaderVMData>(_httpContextAccessorSession.HttpContext.Session.GetString("User"));
            bool onkontrol()
            {
                if (yetkiKullaniciModel.YetkiEklenecekListe is not null)
                {
                    if (yetkiKullaniciModel.YetkiEklenecekListe.Count > 0)
                    {


                        var Modul = _modulHeaderVM.moduls.Find(X => X.Controller == "KullaniciYetkiİslemleri");
                        var kullaniciYetki = _modulHeaderVM.Kullanici_Yetki.Find(X => X.Modul_ID == Modul.id);
                        if (kullaniciYetki.KaydetYetkisi == true)
                        {
                            return true;
                        }
                        else
                        {
                            ViewBag.message = "Kaydetme Yetkiniz Yok!";
                            return false;
                        }

                    }
                    else
                    {

                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            if (onkontrol() == true)
            {


                TanimGenericRepositories<Modul> ModulGeneric = new TanimGenericRepositories<Modul>();
                TanimGenericRepositories<kullanici_Yetki> KullanciYetkiGeneriCrepositories = new TanimGenericRepositories<kullanici_Yetki>();

                GenericRepositories<kullanici_Yetki> YetkiKaydetmeislemi = new GenericRepositories<kullanici_Yetki>();


                if (kullanniciAt == true)
                {
                    GenericRepositories<kullanici_Aktive> GenerickullaniciAtmaRepsitoiries = new GenericRepositories<kullanici_Aktive>();
                    TanimGenericRepositories<kullanici_Aktive> TanimkullaniciAtmaRepsitoiries = new TanimGenericRepositories<kullanici_Aktive>();
                    foreach (var item in yetkiKullaniciModel.YetkiEklenecekListe)
                    {

                        var kullanici_Aktive = TanimkullaniciAtmaRepsitoiries.GetlistAll().FindAll(x => x.kullanici_id == item.Kullanici_id).Find(y => y.aktif == true);
                        if (kullanici_Aktive is not null)
                        {
                            if (kullanici_Aktive.aktif == true)
                            {
                                kullanici_Aktive.aktif = false;
                                GenerickullaniciAtmaRepsitoiries.Update(kullanici_Aktive, int.Parse(_modulHeaderVM.kullanici_id));
                            }
                        }

                    }
                }

                foreach (var item in yetkiKullaniciModel.YetkiEklenecekListe)
                {
                    var modul = ModulGeneric.GetlistAll().Find(x => x.id == item.modul_id);
                    var Ustmodul = ModulGeneric.GetlistAll().Find(x => x.id == modul.UstModul_ID);
                    var Anamodul = ModulGeneric.GetlistAll().Find(x => x.id == Ustmodul.UstModul_ID);
                    if (Ustmodul is not null)
                    {
                        if (KullanciYetkiGeneriCrepositories.GetlistAll().FindAll(X => X.Kullanici_ID == item.Kullanici_id).Find(Y => Y.Modul_ID == Ustmodul.id) is null)
                        {

                            kullanici_Yetki Yetki = new kullanici_Yetki();
                            Yetki.Kullanici_ID = item.Kullanici_id;
                            Yetki.Modul_ID = Ustmodul.id;
                            Yetki.SayfadaIslemYapabilsinmi = true;
                            Yetki.KaydetYetkisi = true;
                            Yetki.SilYetkisi = true;


                            YetkiKaydetmeislemi.Insert(Yetki, int.Parse(_modulHeaderVM.kullanici_id));
                        }
                    }

                    if (Anamodul is not null)
                    {
                        if (KullanciYetkiGeneriCrepositories.GetlistAll().FindAll(X => X.Kullanici_ID == item.Kullanici_id).Find(Y => Y.Modul_ID == Anamodul.id) is null)
                        {

                            kullanici_Yetki Yetki = new kullanici_Yetki();
                            Yetki.Kullanici_ID = item.Kullanici_id;
                            Yetki.Modul_ID = Anamodul.id;
                            Yetki.SayfadaIslemYapabilsinmi = true;
                            Yetki.KaydetYetkisi = true;
                            Yetki.SilYetkisi = true;


                            YetkiKaydetmeislemi.Insert(Yetki, int.Parse(_modulHeaderVM.kullanici_id));
                        }
                    }

                    if (modul is not null)
                    {
                        if (KullanciYetkiGeneriCrepositories.GetlistAll().FindAll(X => X.Kullanici_ID == item.Kullanici_id).Find(Y => Y.Modul_ID == modul.id) is null)
                        {

                            kullanici_Yetki Yetki = new kullanici_Yetki();
                            Yetki.Kullanici_ID = item.Kullanici_id;
                            Yetki.Modul_ID = modul.id;
                            Yetki.SayfadaIslemYapabilsinmi = true;
                            Yetki.KaydetYetkisi = true;
                            Yetki.SilYetkisi = true;


                            YetkiKaydetmeislemi.Insert(Yetki, int.Parse(_modulHeaderVM.kullanici_id));
                            ViewBag.message = "Başari İle Kaydedildi!";
                        }
                    }

                }




            }
        }


        public void YetkiAl(YetkiKullaniciModel yetkiKullaniciModel, bool kullanniciAt)
        {
            ModulHeaderVMData _modulHeaderVM = new ModulHeaderVMData();
            _modulHeaderVM = _memoryCache.Get<ModulHeaderVMData>(_httpContextAccessorSession.HttpContext.Session.GetString("User"));
            bool onkontrol()
            {
                if (yetkiKullaniciModel.YetkiEklenecekListe is not null)
                {
                    if (yetkiKullaniciModel.YetkiEklenecekListe.Count > 0)
                    {


                        var Modul = _modulHeaderVM.moduls.Find(X => X.Controller == "KullaniciYetkiİslemleri");
                        var kullaniciYetki = _modulHeaderVM.Kullanici_Yetki.Find(X => X.Modul_ID == Modul.id);
                        if (kullaniciYetki.SilYetkisi == true)
                        {
                            return true;
                        }
                        else
                        {
                            ViewBag.message = "Silme Yetkiniz Yok!";
                            return false;
                        }

                    }
                    else
                    {

                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            if (onkontrol() == true)
            {

                kullanici_Yetki Yetki = new kullanici_Yetki();
                TanimGenericRepositories<Modul> ModulGeneric = new TanimGenericRepositories<Modul>();
                TanimGenericRepositories<kullanici_Yetki> KullanciYetkiGeneriCrepositories = new TanimGenericRepositories<kullanici_Yetki>();

                GenericRepositories<kullanici_Yetki> Yetkisilmeislemi = new GenericRepositories<kullanici_Yetki>();



                if (kullanniciAt == true)
                {
                    GenericRepositories<kullanici_Aktive> GenerickullaniciAtmaRepsitoiries = new GenericRepositories<kullanici_Aktive>();
                    TanimGenericRepositories<kullanici_Aktive> TanimkullaniciAtmaRepsitoiries = new TanimGenericRepositories<kullanici_Aktive>();
                    foreach (var item in yetkiKullaniciModel.YetkiEklenecekListe)
                    {

                        var kullanici_Aktive = TanimkullaniciAtmaRepsitoiries.GetlistAll().FindAll(x => x.kullanici_id == item.Kullanici_id).Find(y => y.aktif == true);
                        if (kullanici_Aktive is not null)
                        {
                            if (kullanici_Aktive.aktif == true)
                            {
                                kullanici_Aktive.aktif = false;
                                GenerickullaniciAtmaRepsitoiries.Update(kullanici_Aktive, int.Parse(_modulHeaderVM.kullanici_id));
                            }
                        }
                    }
                }

                foreach (var item in yetkiKullaniciModel.YetkiEklenecekListe)
                {
                    int silmeDongusu = 0;
					int silmeDongusuartır = 0;
					Yetki = KullanciYetkiGeneriCrepositories.GetlistAll().FindAll(X => X.Kullanici_ID == item.Kullanici_id).Find(y => y.Modul_ID == item.modul_id);



                    Yetkisilmeislemi.Delete(Yetki, int.Parse(_modulHeaderVM.kullanici_id));


                    var modul = ModulGeneric.GetlistAll().Find(x => x.id == item.modul_id);
                    var Ustmodul = ModulGeneric.GetlistAll().FindAll(x => x.UstModul_ID == modul.UstModul_ID);


                    var Ustmodul2 = ModulGeneric.GetlistAll().Find(x => x.id == modul.UstModul_ID);
                    var Anamodul = ModulGeneric.GetlistAll().Find(x => x.id == Ustmodul2.UstModul_ID);



                    var UstModulDiger = ModulGeneric.GetlistAll().FindAll(X => X.UstModul_ID == Anamodul.id);


				
					foreach (var ustmodulegoresorgu in UstModulDiger) //Ana module baglı üst modüller içinde döner
					{

               
						silmeDongusu = 0;

						var UstModulDiger2 = ModulGeneric.GetlistAll().FindAll(X => X.UstModul_ID == ustmodulegoresorgu.id); 
                        foreach (var ustmodulegoresorgu2 in UstModulDiger2) // modul içerisinde döner 
                        {
							if (KullanciYetkiGeneriCrepositories.GetlistAll().FindAll(X => X.Kullanici_ID == item.Kullanici_id).Find(y => y.Modul_ID == ustmodulegoresorgu2.id) is  null)
							{
								silmeDongusu++;
							}
                            
						}

						if (silmeDongusu == UstModulDiger2.Count)
						{
							Yetki = KullanciYetkiGeneriCrepositories.GetlistAll().FindAll(X => X.Kullanici_ID == item.Kullanici_id).Find(y => y.Modul_ID == ustmodulegoresorgu.id);
							if (Yetki is not null)
							{
								Yetkisilmeislemi.Delete(Yetki, int.Parse(_modulHeaderVM.kullanici_id));
							}

						}


						if (KullanciYetkiGeneriCrepositories.GetlistAll().FindAll(X => X.Kullanici_ID == item.Kullanici_id).Find(y => y.Modul_ID == ustmodulegoresorgu.id) is null)
						{
							silmeDongusuartır++;

						}

						if (UstModulDiger.Count == silmeDongusuartır)
						{
							Yetki = KullanciYetkiGeneriCrepositories.GetlistAll().FindAll(X => X.Kullanici_ID == item.Kullanici_id).Find(y => y.Modul_ID == Anamodul.id);
							if (Yetki is not null)
							{
								Yetkisilmeislemi.Delete(Yetki, int.Parse(_modulHeaderVM.kullanici_id));
							}
						}


					}
                   
 



                }




            }
        }

        public void OzelYetkiVer(YetkiKullaniciModel _yetkiKullaniciModel, bool kullanniciAt)
        {
            if (SessionKontrol() == true)
            {
                ModulHeaderVMData _modulHeaderVM = new ModulHeaderVMData();
                _modulHeaderVM = _memoryCache.Get<ModulHeaderVMData>(_httpContextAccessorSession.HttpContext.Session.GetString("User"));
                bool onkontrol()
                {
                    if (_yetkiKullaniciModel.OzelYetkiEklenecekListe is not null)
                    {
                        if (_yetkiKullaniciModel.OzelYetkiEklenecekListe.Count > 0)
                        {


                            var Modul = _modulHeaderVM.moduls.Find(X => X.Controller == "KullaniciYetkiİslemleri");
                            var kullaniciYetki = _modulHeaderVM.Kullanici_Yetki.Find(X => X.Modul_ID == Modul.id);
                            if (kullaniciYetki.SayfadaIslemYapabilsinmi == true)
                            {
                                return true;
                            }
                            else
                            {
                                ViewBag.message = "Güncelleme Yetkiniz Yok!";
                                return false;
                            }

                        }
                        else
                        {

                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

                if (onkontrol() == true)
                {
                    TanimGenericRepositories<kullanici_Yetki> TanimkullaniciYetkiRepsitoiries = new TanimGenericRepositories<kullanici_Yetki>();
                    GenericRepositories<kullanici_Yetki> genericKullanici = new GenericRepositories<kullanici_Yetki>();
                    kullanici_Yetki yetki = new kullanici_Yetki();

                    if (kullanniciAt == true)
                    {
                        GenericRepositories<kullanici_Aktive> GenerickullaniciAtmaRepsitoiries = new GenericRepositories<kullanici_Aktive>();
                        TanimGenericRepositories<kullanici_Aktive> TanimkullaniciAtmaRepsitoiries = new TanimGenericRepositories<kullanici_Aktive>();
                        foreach (var item in _yetkiKullaniciModel.OzelYetkiEklenecekListe)
                        {
                            var _yetki = TanimkullaniciYetkiRepsitoiries.GetByID(item.id);
                            var kullanici_Aktive = TanimkullaniciAtmaRepsitoiries.GetlistAll().FindAll(x => x.kullanici_id == _yetki.Kullanici_ID).Find(y => y.aktif == true);
                            if (kullanici_Aktive is not null)
                            {
                                if (kullanici_Aktive.aktif == true)
                                {
                                    kullanici_Aktive.aktif = false;
                                    GenerickullaniciAtmaRepsitoiries.Update(kullanici_Aktive, int.Parse(_modulHeaderVM.kullanici_id));
                                }
                            }


                        }
                    }

                    foreach (var item in _yetkiKullaniciModel.OzelYetkiEklenecekListe)
                    {
                        var kullaniciYetki = TanimkullaniciYetkiRepsitoiries.GetlistAll().Find(x => x.id == item.id);
                        if (kullaniciYetki is not null)
                        {


                            if (item.İslemTip == 1)
                            {
                                yetki.id = item.id;
                                yetki.Modul_ID = kullaniciYetki.Modul_ID;
                                yetki.Kullanici_ID = kullaniciYetki.Kullanici_ID;
                                yetki.SayfadaIslemYapabilsinmi = true; // Asil işi
                                yetki.KaydetYetkisi = kullaniciYetki.KaydetYetkisi;
                                yetki.SilYetkisi = kullaniciYetki.SilYetkisi;
                                yetki.BitisTarihi = kullaniciYetki.BitisTarihi;



                                genericKullanici.Update(yetki, int.Parse(_modulHeaderVM.kullanici_id));

                            }

                            if (item.İslemTip == 2)
                            {
                                yetki.id = item.id;
                                yetki.Modul_ID = kullaniciYetki.Modul_ID;
                                yetki.Kullanici_ID = kullaniciYetki.Kullanici_ID;
                                yetki.SayfadaIslemYapabilsinmi = kullaniciYetki.SayfadaIslemYapabilsinmi;
                                yetki.KaydetYetkisi = true; // Asil işi
                                yetki.SilYetkisi = kullaniciYetki.SilYetkisi;
                                yetki.BitisTarihi = kullaniciYetki.BitisTarihi;

                                genericKullanici.Update(yetki, int.Parse(_modulHeaderVM.kullanici_id));

                            }

                            if (item.İslemTip == 3)
                            {
                                yetki.id = item.id;
                                yetki.Modul_ID = kullaniciYetki.Modul_ID;
                                yetki.Kullanici_ID = kullaniciYetki.Kullanici_ID;
                                yetki.SayfadaIslemYapabilsinmi = kullaniciYetki.SayfadaIslemYapabilsinmi;
                                yetki.KaydetYetkisi = kullaniciYetki.KaydetYetkisi;
                                yetki.SilYetkisi = true; // Asil işi
                                yetki.BitisTarihi = kullaniciYetki.BitisTarihi;


                                genericKullanici.Update(yetki, int.Parse(_modulHeaderVM.kullanici_id));

                            }

                        }
                    }
                    ViewBag.message = "Başari İle Kaydedildi";

                }




            }
        }
        public void OzelYetkiAl(YetkiKullaniciModel _yetkiKullaniciModel, bool kullanniciAt)
        {
            if (SessionKontrol() == true)
            {
                ModulHeaderVMData _modulHeaderVM = new ModulHeaderVMData();
                _modulHeaderVM = _memoryCache.Get<ModulHeaderVMData>(_httpContextAccessorSession.HttpContext.Session.GetString("User"));
                bool onkontrol()
                {
                    if (_yetkiKullaniciModel.OzelYetkiEklenecekListe is not null)
                    {


                        if (_yetkiKullaniciModel.OzelYetkiEklenecekListe.Count > 0)
                        {


                            var Modul = _modulHeaderVM.moduls.Find(X => X.Controller == "KullaniciYetkiİslemleri");
                            var kullaniciYetki = _modulHeaderVM.Kullanici_Yetki.Find(X => X.Modul_ID == Modul.id);
                            if (kullaniciYetki.SayfadaIslemYapabilsinmi == true)
                            {
                                return true;
                            }
                            else
                            {
                                ViewBag.message = "Güncelleme Yetkiniz Yok!";
                                return false;
                            }

                        }
                        else
                        {

                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

                if (onkontrol() == true)
                {
                    TanimGenericRepositories<kullanici_Yetki> TanimkullaniciYetkiRepsitoiries = new TanimGenericRepositories<kullanici_Yetki>();
                    GenericRepositories<kullanici_Yetki> genericKullanici = new GenericRepositories<kullanici_Yetki>();
                    kullanici_Yetki yetki = new kullanici_Yetki();



                    if (kullanniciAt == true)
                    {
                        GenericRepositories<kullanici_Aktive> GenerickullaniciAtmaRepsitoiries = new GenericRepositories<kullanici_Aktive>();
                        TanimGenericRepositories<kullanici_Aktive> TanimkullaniciAtmaRepsitoiries = new TanimGenericRepositories<kullanici_Aktive>();
                        foreach (var item in _yetkiKullaniciModel.OzelYetkiEklenecekListe)
                        {
                            var _yetki = TanimkullaniciYetkiRepsitoiries.GetByID(item.id);
                            var kullanici_Aktive = TanimkullaniciAtmaRepsitoiries.GetlistAll().FindAll(x => x.kullanici_id == _yetki.Kullanici_ID).Find(y => y.aktif == true);
                            if (kullanici_Aktive is not null)
                            {
                                if (kullanici_Aktive.aktif == true)
                                {
                                    kullanici_Aktive.aktif = false;
                                    GenerickullaniciAtmaRepsitoiries.Update(kullanici_Aktive, int.Parse(_modulHeaderVM.kullanici_id));
                                }
                            }
                        }
                    }


                    foreach (var item in _yetkiKullaniciModel.OzelYetkiEklenecekListe)

                    {
                        var kullaniciYetki = TanimkullaniciYetkiRepsitoiries.GetlistAll().Find(x => x.id == item.id);

                        if (kullaniciYetki is not null)
                        {

                            if (item.İslemTip == 1)
                            {
                                yetki.id = item.id;
                                yetki.Modul_ID = kullaniciYetki.Modul_ID;
                                yetki.Kullanici_ID = kullaniciYetki.Kullanici_ID;
                                yetki.SayfadaIslemYapabilsinmi = false; // Asil işi
                                yetki.KaydetYetkisi = kullaniciYetki.KaydetYetkisi;
                                yetki.SilYetkisi = kullaniciYetki.SilYetkisi;
                                yetki.BitisTarihi = kullaniciYetki.BitisTarihi;



                                genericKullanici.Update(yetki, int.Parse(_modulHeaderVM.kullanici_id));

                            }

                            if (item.İslemTip == 2)
                            {
                                yetki.id = item.id;
                                yetki.Modul_ID = kullaniciYetki.Modul_ID;
                                yetki.Kullanici_ID = kullaniciYetki.Kullanici_ID;
                                yetki.SayfadaIslemYapabilsinmi = kullaniciYetki.SayfadaIslemYapabilsinmi;
                                yetki.KaydetYetkisi = false; // Asil işi
                                yetki.SilYetkisi = kullaniciYetki.SilYetkisi;
                                yetki.BitisTarihi = kullaniciYetki.BitisTarihi;

                                genericKullanici.Update(yetki, int.Parse(_modulHeaderVM.kullanici_id));

                            }

                            if (item.İslemTip == 3)
                            {
                                yetki.id = item.id;
                                yetki.Modul_ID = kullaniciYetki.Modul_ID;
                                yetki.Kullanici_ID = kullaniciYetki.Kullanici_ID;
                                yetki.SayfadaIslemYapabilsinmi = kullaniciYetki.SayfadaIslemYapabilsinmi;
                                yetki.KaydetYetkisi = kullaniciYetki.KaydetYetkisi;
                                yetki.SilYetkisi = false; // Asil işi
                                yetki.BitisTarihi = kullaniciYetki.BitisTarihi;


                                genericKullanici.Update(yetki, int.Parse(_modulHeaderVM.kullanici_id));

                            }

                        }
                    }
                    ViewBag.message = "Başari İle Kaydedildi";

                }




            }
        }

        [HttpPost]
        public IActionResult YetkiVerBaslat()
        {

            if (SessionKontrol() == true)

            {

                bool kullaniciatmaislemi()
                {
                    var Eklenecesessionkey = _httpContextAccessorSession.HttpContext.Session.GetString("kullaniciAt");
                    if (Eklenecesessionkey != null)
                    {

                        var deger = _memoryCache.Get<bool>(_httpContextAccessorSession.HttpContext.Session.GetString("kullaniciAt"));
                        _memoryCache.Remove(_httpContextAccessorSession.HttpContext.Session.GetString("kullaniciAt"));

                        if (deger == true)
                        {

                            return deger;


                        }
                        else
                        {

                            return false;
                        }
                    }




                    return false;
                }




                var Eklenecesessionkey = _httpContextAccessorSession.HttpContext.Session.GetString("eklenecekModul");
                if (Eklenecesessionkey != null)
                {
                    if (_memoryCache.Get<YetkiKullaniciModel>(_httpContextAccessorSession.HttpContext.Session.GetString("eklenecekModul")) is not null)
                    {

                        YetkiVer(_memoryCache.Get<YetkiKullaniciModel>(_httpContextAccessorSession.HttpContext.Session.GetString("eklenecekModul")), kullaniciatmaislemi());

                    }

                    _memoryCache.Remove(_httpContextAccessorSession.HttpContext.Session.GetString("eklenecekModul"));
                }

                var Silsessionkey = _httpContextAccessorSession.HttpContext.Session.GetString("Yetkisil");
                if (Silsessionkey != null)
                {
                    if (_memoryCache.Get<YetkiKullaniciModel>(_httpContextAccessorSession.HttpContext.Session.GetString("Yetkisil")) is not null)
                    {

                        YetkiAl(_memoryCache.Get<YetkiKullaniciModel>(_httpContextAccessorSession.HttpContext.Session.GetString("Yetkisil")), kullaniciatmaislemi());

                    }
                    _memoryCache.Remove(_httpContextAccessorSession.HttpContext.Session.GetString("Yetkisil"));
                }


                var OzelYetkiVeriSessionKey = _httpContextAccessorSession.HttpContext.Session.GetString("OzelYetkiTrue");
                if (OzelYetkiVeriSessionKey != null)
                {
                    if (_memoryCache.Get<YetkiKullaniciModel>(_httpContextAccessorSession.HttpContext.Session.GetString("OzelYetkiTrue")) is not null)
                    {

                        OzelYetkiVer(_memoryCache.Get<YetkiKullaniciModel>(_httpContextAccessorSession.HttpContext.Session.GetString("OzelYetkiTrue")), kullaniciatmaislemi());
                    }
                    _memoryCache.Remove(_httpContextAccessorSession.HttpContext.Session.GetString("OzelYetkiTrue"));
                }




                var OzelYetkiAliSessionKey = _httpContextAccessorSession.HttpContext.Session.GetString("OzelYetkiFalse");
                if (OzelYetkiAliSessionKey != null)
                {
                    if (_memoryCache.Get<YetkiKullaniciModel>(_httpContextAccessorSession.HttpContext.Session.GetString("OzelYetkiFalse")) is not null)
                    {

                        OzelYetkiAl(_memoryCache.Get<YetkiKullaniciModel>(_httpContextAccessorSession.HttpContext.Session.GetString("OzelYetkiFalse")), kullaniciatmaislemi());
                    }
                    _memoryCache.Remove(_httpContextAccessorSession.HttpContext.Session.GetString("OzelYetkiFalse"));
                }





                modulGetir();
                return View("Index");
            }
            else
            {
                return RedirectToAction("Index", "error");
            }
        }
        [HttpPost]
        public void OzelYetkiDoldur(int id, int İslemTip, YetkiKullaniciModel yetkiKullaniciModel)
        {
            void ekle()
            {



                YetkiKullaniciModel.YetkiModulOzelYetki Yetki = new YetkiKullaniciModel.YetkiModulOzelYetki();

                // Diğer özelliklerin atanması
                Yetki.id = id;
                Yetki.İslemTip = İslemTip;

                // Yeni bir liste oluşturun



                // Liste içine yeni bir öğe ekleyin
                if (yetkiKullaniciModel.OzelYetkiEklenecekListe is not null)
                {
                    yetkiKullaniciModel.OzelYetkiEklenecekListe.Add(Yetki);
                }
                else
                {
                    yetkiKullaniciModel.OzelYetkiEklenecekListe = new List<YetkiKullaniciModel.YetkiModulOzelYetki>();
                    yetkiKullaniciModel.OzelYetkiEklenecekListe.Add(Yetki);
                }




                _memoryCache.Set<YetkiKullaniciModel>(_httpContextAccessorSession.HttpContext.Session.GetString("OzelYetkiTrue"), yetkiKullaniciModel);





            }


            var sessionKeykullanici = _httpContextAccessorSession.HttpContext.Session.GetString("OzelYetkiTrue");
            if (sessionKeykullanici == null)
            {
                _httpContextAccessorSession.HttpContext.Session.SetString("OzelYetkiTrue", "1234567");
            }



            if (_memoryCache.Get<YetkiKullaniciModel>(_httpContextAccessorSession.HttpContext.Session.GetString("OzelYetkiTrue")) is not null)
            {
                yetkiKullaniciModel = _memoryCache.Get<YetkiKullaniciModel>(_httpContextAccessorSession.HttpContext.Session.GetString("OzelYetkiTrue"));
            }

            if (yetkiKullaniciModel.OzelYetkiEklenecekListe is not null)
            {
                if (yetkiKullaniciModel.OzelYetkiEklenecekListe.FindAll(X => X.id == id) is null)
                {

                    ekle();


                }

            }
            else
            {
                ekle();
            }
        }

        [HttpPost]
        public void OzelYetkiSilDoldur(int id, int İslemTip, YetkiKullaniciModel yetkiKullaniciModel)
        {
            void ekle()
            {



                YetkiKullaniciModel.YetkiModulOzelYetki Yetki = new YetkiKullaniciModel.YetkiModulOzelYetki();

                // Diğer özelliklerin atanması
                Yetki.id = id;
                Yetki.İslemTip = İslemTip;

                // Yeni bir liste oluşturun



                // Liste içine yeni bir öğe ekleyin
                if (yetkiKullaniciModel.OzelYetkiEklenecekListe is not null)
                {
                    yetkiKullaniciModel.OzelYetkiEklenecekListe.Add(Yetki);
                }
                else
                {
                    yetkiKullaniciModel.OzelYetkiEklenecekListe = new List<YetkiKullaniciModel.YetkiModulOzelYetki>();
                    yetkiKullaniciModel.OzelYetkiEklenecekListe.Add(Yetki);
                }




                _memoryCache.Set<YetkiKullaniciModel>(_httpContextAccessorSession.HttpContext.Session.GetString("OzelYetkiFalse"), yetkiKullaniciModel);





            }


            var sessionKeykullanici = _httpContextAccessorSession.HttpContext.Session.GetString("OzelYetkiFalse");
            if (sessionKeykullanici == null)
            {
                _httpContextAccessorSession.HttpContext.Session.SetString("OzelYetkiFalse", "12345678");
            }



            if (_memoryCache.Get<YetkiKullaniciModel>(_httpContextAccessorSession.HttpContext.Session.GetString("OzelYetkiFalse")) is not null)
            {
                yetkiKullaniciModel = _memoryCache.Get<YetkiKullaniciModel>(_httpContextAccessorSession.HttpContext.Session.GetString("OzelYetkiFalse"));
            }

            if (yetkiKullaniciModel.OzelYetkiEklenecekListe is not null)
            {
                if (yetkiKullaniciModel.OzelYetkiEklenecekListe.FindAll(X => X.id == id) is null)
                {

                    ekle();


                }

            }
            else
            {
                ekle();
            }
        }

        [HttpPost]
        public void YetkiListesiDoldur(int eklenecekkullanici, int eklenecekmodul, YetkiKullaniciModel yetkiKullaniciModel)
        {


            //_YetkiKullaniciModel.EklenecekModulListesi.Add(0);

            //modulGetir(); 

            void ekle()
            {



                YetkiKullaniciModel.YetkiModulKullaniciListesiModel Yetki = new YetkiKullaniciModel.YetkiModulKullaniciListesiModel();

                // Diğer özelliklerin atanması
                Yetki.Kullanici_id = eklenecekkullanici;
                Yetki.modul_id = eklenecekmodul;

                // Yeni bir liste oluşturun



                // Liste içine yeni bir öğe ekleyin
                if (yetkiKullaniciModel.YetkiEklenecekListe is not null)
                {
                    yetkiKullaniciModel.YetkiEklenecekListe.Add(Yetki);
                }
                else
                {
                    yetkiKullaniciModel.YetkiEklenecekListe = new List<YetkiKullaniciModel.YetkiModulKullaniciListesiModel>();
                    yetkiKullaniciModel.YetkiEklenecekListe.Add(Yetki);
                }




                _memoryCache.Set<YetkiKullaniciModel>(_httpContextAccessorSession.HttpContext.Session.GetString("eklenecekModul"), yetkiKullaniciModel);





            }


            var sessionKeykullanici = _httpContextAccessorSession.HttpContext.Session.GetString("eklenecekModul");
            if (sessionKeykullanici == null)
            {
                _httpContextAccessorSession.HttpContext.Session.SetString("eklenecekModul", "1234");
            }



            if (_memoryCache.Get<YetkiKullaniciModel>(_httpContextAccessorSession.HttpContext.Session.GetString("eklenecekModul")) is not null)
            {
                yetkiKullaniciModel = _memoryCache.Get<YetkiKullaniciModel>(_httpContextAccessorSession.HttpContext.Session.GetString("eklenecekModul"));
            }

            if (yetkiKullaniciModel.YetkiEklenecekListe is not null)
            {
                if (yetkiKullaniciModel.YetkiEklenecekListe.FindAll(X => X.Kullanici_id == eklenecekkullanici).Find(Y => Y.modul_id == eklenecekmodul) is null)
                {

                    ekle();


                }

            }
            else
            {
                ekle();
            }

            //yetkiKullaniciModel.EklenecekModulListesi = eklenecekmodul;



        }



        [HttpPost]
        public void YetkSilDoldur(int silinecekkullanici, int silinecekmodul, YetkiKullaniciModel yetkiKullaniciModel)
        {


            //_YetkiKullaniciModel.EklenecekModulListesi.Add(0);

            void sil()
            {



                YetkiKullaniciModel.YetkiModulKullaniciListesiModel Yetki = new YetkiKullaniciModel.YetkiModulKullaniciListesiModel();

                // Diğer özelliklerin atanması
                Yetki.Kullanici_id = silinecekkullanici;
                Yetki.modul_id = silinecekmodul;

                // Yeni bir liste oluşturun



                // Liste içine yeni bir öğe ekleyin
                if (yetkiKullaniciModel.YetkiEklenecekListe is not null)
                {
                    yetkiKullaniciModel.YetkiEklenecekListe.Add(Yetki);
                }
                else
                {
                    yetkiKullaniciModel.YetkiEklenecekListe = new List<YetkiKullaniciModel.YetkiModulKullaniciListesiModel>();
                    yetkiKullaniciModel.YetkiEklenecekListe.Add(Yetki);
                }




                _memoryCache.Set<YetkiKullaniciModel>(_httpContextAccessorSession.HttpContext.Session.GetString("Yetkisil"), yetkiKullaniciModel);
            }


            var sessionKeykullanici = _httpContextAccessorSession.HttpContext.Session.GetString("Yetkisil");
            if (sessionKeykullanici == null)
            {
                _httpContextAccessorSession.HttpContext.Session.SetString("Yetkisil", "12345");
            }



            if (_memoryCache.Get<YetkiKullaniciModel>(_httpContextAccessorSession.HttpContext.Session.GetString("Yetkisil")) is not null)
            {
                yetkiKullaniciModel = _memoryCache.Get<YetkiKullaniciModel>(_httpContextAccessorSession.HttpContext.Session.GetString("Yetkisil"));
            }

            if (yetkiKullaniciModel.YetkiEklenecekListe is not null)
            {
                if (yetkiKullaniciModel.YetkiEklenecekListe.FindAll(X => X.Kullanici_id == silinecekkullanici).Find(Y => Y.modul_id == silinecekmodul) is null)
                {



                    sil();




                }
            }
            else
            {
                sil();
            }


            //yetkiKullaniciModel.EklenecekModulListesi = eklenecekmodul;


        }
        [HttpPost]
        public void kullaniciAt(bool kullaniciat)
        {

            var sessionKeykullanici = _httpContextAccessorSession.HttpContext.Session.GetString("kullaniciAt");
            if (sessionKeykullanici == null)
            {
                _httpContextAccessorSession.HttpContext.Session.SetString("kullaniciAt", "12345678910");
            }
            else
            {
                _memoryCache.Remove(_httpContextAccessorSession.HttpContext.Session.GetString("kullaniciAt"));
            }

            _memoryCache.Set<bool>(_httpContextAccessorSession.HttpContext.Session.GetString("kullaniciAt"), kullaniciat);


        }
        [HttpGet]
        public IActionResult Index()
        {
            if (SessionKontrol() == true)
            {
                modulGetir();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "error");
            }

        }
    }
}
