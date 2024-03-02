using BusinessLayer.Utilities.Security;
using BusinessLayer.Utilities.Security.Hash;
using DataAccessLayer.Repositories;
using DataAccessLayer.Utilities.YetkiKontrol;
using EntityLayer.Concrete;
using EntityLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using WEB.Models;

namespace WEB.Controllers
{
	public class TanimlamaİslemleriController : Controller
	{
		readonly IMemoryCache _memoryCache;
		readonly IHttpContextAccessor _httpContextAccessorSession;
		public TanimlamaİslemleriController(IHttpContextAccessor session, IMemoryCache memoryCache)
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

		public TanimlamaİslemleriModel kullanicilarigetir()
		{
			TanimlamaİslemleriModel Tanimlama_Model = new TanimlamaİslemleriModel();
			if (SessionKontrol() == true)
			{


				TanimGenericRepositories<Kullanicilar> tanimGenericRepositories = new TanimGenericRepositories<Kullanicilar>();
				TanimGenericRepositories<YetkiGrupTur> TanimlamayetkiGroupRepositories = new TanimGenericRepositories<YetkiGrupTur>();
				TanimGenericRepositories<Personel> TanimlamaSaglik_PersonelRepositories = new TanimGenericRepositories<Personel>();
				Tanimlama_Model.KullaniciListesi = tanimGenericRepositories.GetlistAll();
				Tanimlama_Model.YetkiGrupTurs = TanimlamayetkiGroupRepositories.GetlistAll();
				Tanimlama_Model.Personels = TanimlamaSaglik_PersonelRepositories.GetlistAll();
				ViewData["Tanimlamaİslemleri"] = Tanimlama_Model;
				return Tanimlama_Model;





			}
			else
			{
				return Tanimlama_Model;
			}

		}
		[HttpPost]
		public IActionResult Secilikullanicilarigetir(Kullanicilar kullanicilar)
		{
			TanimlamaİslemleriModel Tanimlama_Model = new TanimlamaİslemleriModel();
			if (SessionKontrol() == true)
			{
                ModulHeaderVMData _modulHeaderVM = new();
                _modulHeaderVM = _memoryCache.Get<ModulHeaderVMData>(_httpContextAccessorSession.HttpContext.Session.GetString("User"));

                    GenericRepositories<Kullanicilar> tanimGenericRepositories = new GenericRepositories<Kullanicilar>();
				TanimGenericRepositories<YetkiGrupTur> TanimlamayetkiGroupRepositories = new TanimGenericRepositories<YetkiGrupTur>();
				TanimGenericRepositories<Personel> TanimlamaSaglik_PersonelRepositories = new TanimGenericRepositories<Personel>();

				if (Tanimlama_Model.KullaniciListesi is null)
				{
					Tanimlama_Model.KullaniciListesi = tanimGenericRepositories.GetlistAll(kullanicilar ,int.Parse(_modulHeaderVM.kullanici_id)).FindAll(x => x.id == kullanicilar.id);
				}
				if (Tanimlama_Model.KullaniciListesi.Count == 0)
				{
					Tanimlama_Model.KullaniciListesi = tanimGenericRepositories.GetlistAll(kullanicilar, int.Parse(_modulHeaderVM.kullanici_id)).FindAll(x => x.Ad == kullanicilar.Ad).FindAll(y => y.soyad == kullanicilar.soyad);
				}

				if (Tanimlama_Model.KullaniciListesi.Count == 0)
				{
					Tanimlama_Model.KullaniciListesi = tanimGenericRepositories.GetlistAll(kullanicilar, int.Parse(_modulHeaderVM.kullanici_id)).FindAll(x => x.KullaniciAd == kullanicilar.KullaniciAd);
				}
				if (kullanicilar.Yetki_Grup_ıd is not null)
				{
					if (Tanimlama_Model.KullaniciListesi.Count == 0)
					{
						Tanimlama_Model.KullaniciListesi = tanimGenericRepositories.GetlistAll(kullanicilar, int.Parse(_modulHeaderVM.kullanici_id)).FindAll(x => x.Yetki_Grup_ıd == kullanicilar.Yetki_Grup_ıd);
					}
				}

				//if (kullanicilar.Aktif is not null)
				//{
				//    if (Tanimlama_Model.KullaniciListesi.Count == 0)
				//    {
				//        Tanimlama_Model.KullaniciListesi = tanimGenericRepositories.GetlistAll().FindAll(x => x.Yetki_Grup_ıd == kullanicilar.Yetki_Grup_ıd);
				//    }
				//}

				Tanimlama_Model.YetkiGrupTurs = TanimlamayetkiGroupRepositories.GetlistAll();
				Tanimlama_Model.Personels = TanimlamaSaglik_PersonelRepositories.GetlistAll();
				ViewData["Tanimlamaİslemleri"] = Tanimlama_Model;

				return View("KullaniciTanimlama");





			}
			else
			{
				return RedirectToAction("Index", "error");
			}

		}
		[HttpPost]
		public IActionResult KullaniciDuzenle(TanimlamaİslemleriModel _Tanimlamamodel)
		{
			if (SessionKontrol() == true)
			{

				TanimGenericRepositories<Kullanicilar> tanimGenericRepositories = new TanimGenericRepositories<Kullanicilar>();
				TanimGenericRepositories<YetkiGrupTur> TanimlamayetkiGroupRepositories = new TanimGenericRepositories<YetkiGrupTur>();
				TanimGenericRepositories<Personel> TanimlamaSaglik_PersonelRepositories = new TanimGenericRepositories<Personel>();
				_Tanimlamamodel.TanimlamaKullaniciGuncelleme = true;
				_Tanimlamamodel.KullaniciListesi = tanimGenericRepositories.GetlistAll().FindAll(x => x.id == _Tanimlamamodel.Tanimlama_kullanici_ID);
				_Tanimlamamodel.YetkiGrupTurs = TanimlamayetkiGroupRepositories.GetlistAll();
				_Tanimlamamodel.Personels = TanimlamaSaglik_PersonelRepositories.GetlistAll();
				ViewData["Tanimlamaİslemleri"] = _Tanimlamamodel;

				return View("KullaniciTanimlama");
			}
			else
			{
				return RedirectToAction("Index", "error");
			}
		}

		[HttpPost]
		public IActionResult KullaniciUpdate(Kullanicilar kullanicilar)
		{
			if (SessionKontrol() == true)
			{
				YetkiKontrol yetkiKontrol = new YetkiKontrol();
				Hash256 hash256 = new Hash256();
				ModulHeaderVMData
			 _modulHeaderVM = _memoryCache.Get<ModulHeaderVMData>(_httpContextAccessorSession.HttpContext.Session.GetString("User"));
				TanimGenericRepositories<Kullanicilar> KullanicitanimGenericRepositories = new TanimGenericRepositories<Kullanicilar>();

				var kullanicibilgisi = KullanicitanimGenericRepositories.GetlistAll().Find(x => x.id == kullanicilar.id);


 

				bool kullaniciKontrol()
				{
					if (kullanicibilgisi.KullaniciAd == kullanicilar.KullaniciAd.Replace(" ", ""))
					{
						return true;
					}
					else
					{

						if (KullanicitanimGenericRepositories.GetlistAll().Find(x => x.KullaniciAd == kullanicilar.KullaniciAd.Replace(" ", "")) is null)
						{
							return true;
						}
						else
						{
							ViewBag.message = "Kullanıcı Adı Zaten Mevcut!";
							return false;

						}
					}
				}


				if (kullaniciKontrol()==true)
				{
					if (yetkiKontrol.GuncellemeYetkisi(_modulHeaderVM, "Tanimlamaİslemleri") == true)
					{
						GenericRepositories<Kullanicilar> genericRepositories = new GenericRepositories<Kullanicilar>();
						kullanicilar.sifre = hash256.HashOlustur(kullanicilar.sifre);
						kullanicilar.KullaniciAd = kullanicilar.KullaniciAd.Replace(" ", "");
						genericRepositories.Update(kullanicilar, int.Parse(_modulHeaderVM.kullanici_id));
						ViewBag.message = "Başar ile Güncellendi";
					}
					else
					{
						ViewBag.message = "Güncelleme Yetkiniz yok!";
					}
				}

				TanimlamaİslemleriModel _Tanimlamamodel = new TanimlamaİslemleriModel();
                TanimGenericRepositories<Kullanicilar> tanimGenericRepositories = new TanimGenericRepositories<Kullanicilar>();
                TanimGenericRepositories<YetkiGrupTur> TanimlamayetkiGroupRepositories = new TanimGenericRepositories<YetkiGrupTur>();
                TanimGenericRepositories<Personel> TanimlamaSaglik_PersonelRepositories = new TanimGenericRepositories<Personel>();
                _Tanimlamamodel.TanimlamaKullaniciGuncelleme = true;
                _Tanimlamamodel.KullaniciListesi = tanimGenericRepositories.GetlistAll().FindAll(x => x.id == kullanicilar.id);
                _Tanimlamamodel.YetkiGrupTurs = TanimlamayetkiGroupRepositories.GetlistAll();
                _Tanimlamamodel.Personels = TanimlamaSaglik_PersonelRepositories.GetlistAll();
                ViewData["Tanimlamaİslemleri"] = _Tanimlamamodel;

               
                return View("KullaniciTanimlama");
			}
			else
			{
				return RedirectToAction("Index", "error");
			}
		}
		[HttpPost]
		public IActionResult KullaniciKaydetmeislemi(Kullanicilar kullanicilar)
		{

			if (SessionKontrol() == true)
			{

				YetkiKontrol yetkiKontrol = new YetkiKontrol();

				ModulHeaderVMData
				_modulHeaderVM = _memoryCache.Get<ModulHeaderVMData>(_httpContextAccessorSession.HttpContext.Session.GetString("User"));
				TanimGenericRepositories<Kullanicilar> KullanicitanimGenericRepositories = new TanimGenericRepositories<Kullanicilar>();


				if (KullanicitanimGenericRepositories.GetlistAll().Find(x => x.KullaniciAd == kullanicilar.KullaniciAd.Replace(" ", "")) is null)
				{
					if (yetkiKontrol.KaydetYetkisi(_modulHeaderVM, "Tanimlamaİslemleri") == true)
					{
						Hash256 hash256 = new Hash256();
						GenericRepositories<Kullanicilar> KullanicigenericRepositories = new GenericRepositories<Kullanicilar>();

						if (kullanicilar.id != 0)
						{
							kullanicilar.id = 0;
						}

						kullanicilar.KullaniciAd = kullanicilar.KullaniciAd.Replace(" ", "");
						kullanicilar.KayitZamani = DateTime.Now;
						kullanicilar.EkleyenKullanici_id = int.Parse(_modulHeaderVM.kullanici_id);
						kullanicilar.sifre = hash256.HashOlustur(kullanicilar.sifre);
						KullanicigenericRepositories.Insert(kullanicilar, int.Parse(_modulHeaderVM.kullanici_id));
						ViewBag.message = "Başari İle Kaydedildi";
					}
					else
					{
						ViewBag.message = "Kaydetme Yetkiniz Yok!";
					}
				}
				else
				{
					ViewBag.message = "Kullanıcı Zaten Mevcut!";
				}

				kullanicilarigetir();
				return View("KullaniciTanimlama");
			}
			else
			{
				return RedirectToAction("Index", "error");
			}

		}

		[HttpGet]
		[HttpPost]
		public IActionResult KullaniciTanimlama(Kullanicilar kullanicilar)
		{
			if (SessionKontrol() == true)
			{


				kullanicilarigetir();



				return View();
			}
			else
			{
				return RedirectToAction("Index", "error");
			}
		}


		[HttpGet]
		public IActionResult Index()
		{
			if (SessionKontrol() == true)
			{

				return View();
			}
			else
			{
				return RedirectToAction("Index", "error");
			}

		}
	}
}
