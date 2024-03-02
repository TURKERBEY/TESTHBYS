using BusinessLayer.Utilities.Security;
using com.sun.tools.javac.util;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using EntityLayer.Models;
using java.awt;
using java.util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using NuGet.Packaging;
using System;
using System.Collections.Generic;
using System.Linq;
using WEB.Models;
using WebServis.KpsMernis;




namespace WEB.Controllers
{

	public class HastaKayitKabulController : Controller
	{

		readonly IMemoryCache _memoryCache;
		readonly IHttpContextAccessor _httpContextAccessorSession;
		public HastaKayitKabulController(IHttpContextAccessor session, IMemoryCache memoryCache)
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

		public List<Ulke> UyrukTur()
		{
			TanimGenericRepositories<Ulke> UyrukTurGeneric = new();



			return UyrukTurGeneric.GetlistAll();
		}
		public List<HastaKayitTur> hastakayitTur()
		{
			TanimGenericRepositories<HastaKayitTur> hastakayitTurGeneric = new();



			return hastakayitTurGeneric.GetlistAll();
		}
		public List<Hasta> hastaGetirİslemiBaslat(HastaAramaModel hastaArama, int kullanici)
		{

			if (SessionKontrol() == true)
			{


				Hasta hasta = new();
				if (hastaArama.Hastalistesi is not null)
				{
					hastaArama.Hastalistesi.Clear();
					hastaArama.Adi = null;
					hastaArama.Soyadi = null;
					hastaArama.BaslangicTarihi = null;
					hastaArama.BitisTarihi = null;
				}


				HastaRepositories HastaGeneric = new();



				if (hastaArama.Dosyano is not null)
				{
					hastaArama.Hastalistesi = HastaGeneric.ListAllHastaDosya(hasta, hastaArama.Dosyano.GetValueOrDefault(), kullanici);
				}


				if (hastaArama.Tckimlik is not null && hastaArama.Hastalistesi is null)
				{
					hastaArama.Hastalistesi = HastaGeneric.ListAllHastaTC(hasta, hastaArama.Tckimlik.GetValueOrDefault(), kullanici);

				}

				if (hastaArama.Adi is not null && hastaArama.Soyadi is not null && hastaArama.Hastalistesi is null)
				{
					hastaArama.Hastalistesi = HastaGeneric.ListAllHastaAdsoyad(hasta, hastaArama.Adi.ToUpper(), hastaArama.Soyadi.ToUpper(), kullanici);

				}



				//if (hastaArama.Basvuruno is not null)
				//{
				//    //Tamamlanacak
				//}
				//if (hastaArama.BaslangicTarihi is not null && hastaArama.BaslangicTarihi is not null)
				//{
				//    hastaBilgisi = HastaGeneric.GetlistAll(hasta, int.Parse(hastaArama.Dosyano.ToString()), kullanici);
				//}


				if (hastaArama is not null)
				{
					if (hastaArama.Hastalistesi.Count == 0)
					{
						hastaArama.İslemTur = 1;
						ViewBag.Message = "Hasta Bilgisi Bulunamadığından Kayıt Ekranına Yönlendirildiniz...";
					}

				}



				if (hastaArama.Hastalistesi is not null)
				{

					return hastaArama.Hastalistesi;
				}
				else
				{

					return hastaArama.Hastalistesi;
				}
			}
			else
			{
				hastaArama = null;
				return hastaArama.Hastalistesi;

			}

		}

		public HastaAramaModel HastaUpdateBaslat(Hasta hasta)
		{
			
	
			HastaAramaModel hastaAramaModel = new();

			

			if (SessionKontrol() == true)
			{



				ModulHeaderVMData _modulHeaderVM = new();

				_modulHeaderVM = _memoryCache.Get<ModulHeaderVMData>(_httpContextAccessorSession.HttpContext.Session.GetString("User"));
				var Modul = _modulHeaderVM.moduls.Find(X => X.Controller == "HastaKayitKabul");
				var kullaniciYetki = _modulHeaderVM.Kullanici_Yetki.Find(X => X.Modul_ID == Modul.id);
				HastaRepositories hastaRepositories = new();
				var gelen = hasta.TC;
				bool TC()
				{
					if (hasta.TC == 0M)
					{
						return false;
					}
					if (hasta.TC.ToString().Length < 12 || hasta.TC.ToString().Length == 0)
					{
						return true;
					}
					else
					{
						return false;
					}
				}

				bool BabaTC()
				{

					if (hasta.BabaTc is not null)
					{
						if (hasta.BabaTc.Length < 12 || hasta.BabaTc.Length == 0)
						{
							return true;
						}
						else
						{
							return false;
						}
					}
					else
					{
						return true;
					}


				}

				bool AnneTC()
				{

					if (hasta.AnneTc is not null)
					{
						if (hasta.AnneTc.Length < 12 || hasta.AnneTc.Length == 0)
						{
							return true;
						}
						else
						{
							return false;
						}
					}
					else
					{
						return true;
					}


				}


				if (TC()==true)
				{


					if (BabaTC() == true)
					{

						if (AnneTC() == true)
						{

							if (kullaniciYetki.SayfadaIslemYapabilsinmi == true)
							{
								if (hasta.HastaKayitTip_id is not null)
								{


									if (hasta.HastaKayitTip_id == 1)
									{
										MernisKimlikDogrulama mernisKimlikDogrulama = new();
										if (mernisKimlikDogrulama.KpsKimlikDogrula(hasta.TC, hasta.Ad, hasta.Soyad, hasta.DogumTarih.GetValueOrDefault()) == true)
										{
											hasta.GuncellemeZamani = System.DateTime.Now;
											hasta.GuncelleyenKulanici_id = int.Parse(_modulHeaderVM.kullanici_id);
											hastaRepositories.UpdateHasta(hasta, int.Parse(_modulHeaderVM.kullanici_id));
											ViewBag.message = "Guncelleme işlemi Başarılı";
											hastaAramaModel.Tckimlik = hasta.TC;
											hastaAramaModel.Hastalistesi = hastaGetirİslemiBaslat(hastaAramaModel, int.Parse(_modulHeaderVM.kullanici_id));

											return hastaAramaModel;

										}
										else
										{
											ViewBag.message = "Girilen Bilgiler Gerçek kişiye Ait Degil Lütfen Gerçek Kişi Bilgisi Girin!";
											hastaAramaModel.İslemTur = 4;
											return hastaAramaModel;
										}
									}
									else
									{



										hasta.GuncellemeZamani = System.DateTime.Now;
										hasta.GuncelleyenKulanici_id = int.Parse(_modulHeaderVM.kullanici_id);
										hastaRepositories.UpdateHasta(hasta, int.Parse(_modulHeaderVM.kullanici_id));
										ViewBag.message = "Guncelleme işlemi Başarılı";
										hastaAramaModel.Tckimlik = hasta.TC;
										hastaAramaModel.Hastalistesi = hastaGetirİslemiBaslat(hastaAramaModel, int.Parse(_modulHeaderVM.kullanici_id));







										return hastaAramaModel;


									}
								}
								else
								{
									ViewBag.message = "Lütfen Hasta Kayit Tipini Seçin!";
									hastaAramaModel.İslemTur = 4;
									return hastaAramaModel;
								}

							}
							else
							{
								ViewBag.message = "Güncelleme Yetkiniz Yok!";
								hastaAramaModel.İslemTur = 4;
								return hastaAramaModel;
							}
						}
						else
						{
							ViewBag.message = "Anne TC kimlik 11 Haneden Fazla Olamaz!";
							hastaAramaModel.İslemTur = 4;
							return hastaAramaModel;
						}
					}
					else
					{
						ViewBag.message = "Baba TC kimlik 11 Haneden Fazla Olamaz!";
						hastaAramaModel.İslemTur = 4;
						return hastaAramaModel;
					}

				}



				else
				{
					ViewBag.message = "TC kimlik 11 Haneden Fazla Olamaz!";
					hastaAramaModel.İslemTur = 4;
					return hastaAramaModel;
				}
			}
			else
			{
				return hastaAramaModel;
			}

		}

		public HastaAramaModel HastaKaydetBaslat(Hasta hasta)
		{
			HastaAramaModel hastaAramaModel = new();
			if (SessionKontrol() == true)
			{



				ModulHeaderVMData _modulHeaderVM = new();

				_modulHeaderVM = _memoryCache.Get<ModulHeaderVMData>(_httpContextAccessorSession.HttpContext.Session.GetString("User"));
				var Modul = _modulHeaderVM.moduls.Find(X => X.Controller == "HastaKayitKabul");
				var kullaniciYetki = _modulHeaderVM.Kullanici_Yetki.Find(X => X.Modul_ID == Modul.id);
				HastaRepositories hastaRepositories = new();
				bool TC()
				{
					if (hasta.TC == 0M)
					{
						return false;
					}
					if (hasta.TC.ToString().Length < 12 || hasta.TC.ToString().Length == 0)
					{
						return true;
					}
					else
					{
						return false;
					}
				}

				bool BabaTC()
				{

					if (hasta.BabaTc is not null)
					{
						if (hasta.BabaTc.Length < 12 || hasta.BabaTc.Length == 0)
						{
							return true;
						}
						else
						{
							return false;
						}
					}
					else
					{
						return true;
					}


				}

				bool AnneTC()
				{

					if (hasta.AnneTc is not null)
					{
						if (hasta.AnneTc.Length < 12 || hasta.AnneTc.Length == 0)
						{
							return true;
						}
						else
						{
							return false;
						}
					}
					else
					{
						return true;
					}


				}


				if (TC()==true)
				{


					if (BabaTC() == true)
					{

						if (AnneTC() == true)
						{

							if (kullaniciYetki.KaydetYetkisi == true)
							{
								if (hasta.HastaKayitTip_id is not null)
								{


									if (hasta.HastaKayitTip_id == 1)
									{
										MernisKimlikDogrulama mernisKimlikDogrulama = new();
										if (mernisKimlikDogrulama.KpsKimlikDogrula(hasta.TC, hasta.Ad, hasta.Soyad, hasta.DogumTarih.GetValueOrDefault()) == true)
										{
											hasta.Kayit_Zamani = System.DateTime.Now;
											hasta.EkleyenKullanici_id = int.Parse(_modulHeaderVM.kullanici_id);
											hastaRepositories.AddHasta(hasta, int.Parse(_modulHeaderVM.kullanici_id));
											ViewBag.message = "Kaydetme işlemi Başarılı";
											hastaAramaModel.Tckimlik = hasta.TC;
											hastaAramaModel.Hastalistesi = hastaGetirİslemiBaslat(hastaAramaModel, int.Parse(_modulHeaderVM.kullanici_id));

											return hastaAramaModel;

										}
										else
										{
											ViewBag.message = "Girilen Bilgiler Gerçek kişiye Ait Degil Lütfen Gerçek Kişi Bilgisi Girin!";
											hastaAramaModel.İslemTur = 1;
											return hastaAramaModel;
										}
									}
									else
									{



										hasta.Kayit_Zamani = System.DateTime.Now;
										hasta.EkleyenKullanici_id = int.Parse(_modulHeaderVM.kullanici_id);
										hastaRepositories.AddHasta(hasta, int.Parse(_modulHeaderVM.kullanici_id));
										ViewBag.message = "Kaydetme işlemi Başarılı";
										hastaAramaModel.Tckimlik = hasta.TC;
										hastaAramaModel.Hastalistesi = hastaGetirİslemiBaslat(hastaAramaModel, int.Parse(_modulHeaderVM.kullanici_id));







										return hastaAramaModel;


									}
								}
								else
								{
									ViewBag.message = "Lütfen Hasta Kayit Tipini Seçin!";
									hastaAramaModel.İslemTur = 1;
									return hastaAramaModel;
								}

							}
							else
							{
								ViewBag.message = "Kaydetme Yetkiniz Yok!";
								hastaAramaModel.İslemTur = 1;
								return hastaAramaModel;
							}
						}
						else
						{
							ViewBag.message = "Anne TC kimlik 11 Haneden Fazla Olamaz!";
							hastaAramaModel.İslemTur = 1;
							return hastaAramaModel;
						}
					}
					else
					{
						ViewBag.message = "Baba TC kimlik 11 Haneden Fazla Olamaz!";
						hastaAramaModel.İslemTur = 1;
						return hastaAramaModel;
					}

				}



				else
				{
					ViewBag.message = "TC kimlik 11 Haneden Fazla Olamaz!";
					hastaAramaModel.İslemTur = 1;
					return hastaAramaModel;
				}
			}
			else
			{
				return hastaAramaModel;
			}

		}
		public Hasta HastaKayitOnKontrol(Hasta hasta)
		{
			//oncelik tur vs gibi alnlar için

			hasta.Ad = hasta.Ad.ToUpper();
			hasta.Soyad = hasta.Soyad.ToUpper();


			return hasta;
		}

		[HttpPost]
		public IActionResult YeniKayit(HastaAramaModel _hastaaramaModel)
		{
			if (SessionKontrol() == true)
			{
				_hastaaramaModel.İslemTur = 1;

				_hastaaramaModel.HastaKayitTur = hastakayitTur();
				_hastaaramaModel.UyrukTur = UyrukTur();
				ViewData["Hasta"] = _hastaaramaModel;
				return View("Index");
			}
			else
			{
				return View();
			}
		}

		[HttpPost]
		public IActionResult HastaUpdate(Hasta HastaBilgisi )
		{
			 
			
			if (SessionKontrol() == true)
			{
				HastaAramaModel _hastaaramaModel = new();
				_hastaaramaModel.HastaKayitTur = hastakayitTur();
				_hastaaramaModel.UyrukTur = UyrukTur();
				_hastaaramaModel.İslemTur = 1;


				HastaBilgisi = HastaKayitOnKontrol(HastaBilgisi);

				var gelen = HastaUpdateBaslat(HastaBilgisi);





				if (gelen.İslemTur != 1)
				{
					_hastaaramaModel.Hastalistesi = gelen.Hastalistesi;
					_hastaaramaModel.İslemTur = 4;
					_hastaaramaModel.Message = "Hasta Başari İle Kaydedildi";

					ViewData["Hasta"] = _hastaaramaModel;
					return View("Index");
				}
				else
				{
					ViewData["Hasta"] = _hastaaramaModel;
					return View("Index");
				}
			}
			else
			{
				return View("Index");
			}

		}

		[HttpPost]
		public IActionResult HastaKaydet(Hasta HastaBilgisi)
		{
			if (SessionKontrol() == true)
			{
				HastaAramaModel _hastaaramaModel = new();
				_hastaaramaModel.HastaKayitTur = hastakayitTur();
				_hastaaramaModel.UyrukTur = UyrukTur();
				_hastaaramaModel.İslemTur = 1;


				HastaBilgisi = HastaKayitOnKontrol(HastaBilgisi);

				var gelen = HastaKaydetBaslat(HastaBilgisi);





				if (gelen.İslemTur != 1)
				{
					_hastaaramaModel.Hastalistesi = gelen.Hastalistesi;
					_hastaaramaModel.İslemTur = 2;
					_hastaaramaModel.Message = "Hasta Başari İle Kaydedildi";

					ViewData["Hasta"] = _hastaaramaModel;
					return View("Index");
				}
				else
				{
					ViewData["Hasta"] = _hastaaramaModel;
					return View("Index");
				}
			}
			else
			{
				return View("Index");
			}

		}
		[HttpPost]
		public IActionResult HastaGetir(HastaAramaModel hastaArama)
		{
			if (SessionKontrol() == true)
			{
				bool TC()
				{
					if (hastaArama.Tckimlik == 0M)
					{
						return false;
					}
					if (hastaArama.Tckimlik.ToString().Length < 12 || hastaArama.Tckimlik.ToString().Length == 0)
					{
						return true;
					}
					else
					{
					  return false;
					}
				}

				ModulHeaderVMData _modulHeaderVM = new();
				_modulHeaderVM = _memoryCache.Get<ModulHeaderVMData>(_httpContextAccessorSession.HttpContext.Session.GetString("User"));
				if (hastaArama is not null)
				{
					if (hastaArama.Tckimlik is not null || hastaArama.Dosyano is not null || hastaArama.Adi is not null || hastaArama.Soyadi is not null)
					{
						if (TC()==true)
						{

							hastaArama.Hastalistesi = hastaGetirİslemiBaslat(hastaArama, int.Parse(_modulHeaderVM.kullanici_id));

							if (hastaArama.İslemTur == 1) // Uyruk Hasta Tür Gibi Alanlari Dolduruyor
							{
								if (hastaArama.HastaKayitTur is null || hastaArama.UyrukTur is null)
								{
									hastaArama.HastaKayitTur = hastakayitTur();
									hastaArama.UyrukTur = UyrukTur();
								}
							}
							 
							ViewData["Hasta"] = hastaArama;
							return View("Index", hastaArama);
						}
						else
						{
							ViewData["Hasta"] = hastaArama;
							hastaArama.İslemTur = 3;
							ViewBag.message = "Tc Kimlik 11 Haneden Fazla Olamaz!";
							return View("Index", hastaArama);
						}

					}
					ViewData["Hasta"] = hastaArama;
					hastaArama.İslemTur = 3;
					return View("Index", hastaArama);

				}
				ViewData["Hasta"] = hastaArama;
				hastaArama.İslemTur = 3;
				return View("Index", hastaArama);
			}
			else
			{
				return RedirectToAction("Index", "error");
			}

		}

	 
		[HttpPost]
		public IActionResult HastaDuzenle( int Dosyano ,HastaAramaModel _hastaaramaModel)
		{
		
		 

			if (SessionKontrol() == true)
			{
				//	var Hasta = _hastaaramaModel.Hastalistesi.Find(x => x.TC == _hastaaramaModel.Tckimlik);
				//	_hastaaramaModel.İslemTur = 4;

				//	_hastaaramaModel.HastaKayitTur = hastakayitTur();
				//	_hastaaramaModel.UyrukTur = UyrukTur();
				//	ViewData["Hasta"] = _hastaaramaModel;

				ModulHeaderVMData _modulHeaderVM = new();
				_modulHeaderVM = _memoryCache.Get<ModulHeaderVMData>(_httpContextAccessorSession.HttpContext.Session.GetString("User"));


				_hastaaramaModel.Hastalistesi = hastaGetirİslemiBaslat(_hastaaramaModel, int.Parse(_modulHeaderVM.kullanici_id));


				if (_hastaaramaModel.HastaKayitTur is null || _hastaaramaModel.UyrukTur is null)
				{
					_hastaaramaModel.HastaKayitTur = hastakayitTur();
					_hastaaramaModel.UyrukTur = UyrukTur();
				}
				_hastaaramaModel.İslemTur = 4;

				ViewData["Hasta"] = _hastaaramaModel;
				
				return View("Index");
			}
			else
			{
				return View();
			}
		 

		}


		[HttpGet]
		public IActionResult Index(HastaAramaModel hastaArama)
		{




			if (SessionKontrol() == true)
			{


				if (hastaArama.İslemTur == 2)
				{
					ViewBag.message = hastaArama.Message;
					hastaArama.Message = null;

				}





				ViewData["Hasta"] = hastaArama;

				return View(hastaArama);

			}
			else
			{
				return RedirectToAction("Index", "error");
			}

		}
	}
}
