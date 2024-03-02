using Microsoft.AspNetCore.Mvc;
using WEB.Models;
using BusinessLayer.Utilities.Security;
using DataAccessLayer.Utilities;
using Microsoft.Extensions.Caching.Memory;
using EntityLayer.Models;
using WEB.Utilities.OpenJobs;
using Microsoft.AspNetCore.Http;
using BusinessLayer.Utilities.Security.Hash;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Hosting.Internal;
using static EntityLayer.Models.versiyonnotlariModel;

namespace WEB.Controllers
{

    public class Home : Controller
    {



        readonly Opening _opening = new();

		readonly IMemoryCache _memoryCache;

		readonly IHttpContextAccessor _httpContextAccessorSession;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public Home(IHttpContextAccessor session, IMemoryCache memoryCache, IWebHostEnvironment hostingEnvironment)
        {

            _memoryCache = memoryCache;

            _httpContextAccessorSession = session;
            _hostingEnvironment = hostingEnvironment;
        }
        public void ModulSetle(ModulHeaderVMData modulHeaderVM)
        {
            _memoryCache.Remove(_httpContextAccessorSession.HttpContext.Session.GetString("User"));

			if (_memoryCache.Get<ModulHeaderVMData>(_httpContextAccessorSession.HttpContext.Session.GetString("User")) is null)
            {
                _memoryCache.Set<ModulHeaderVMData>(_httpContextAccessorSession.HttpContext.Session.GetString("User"), modulHeaderVM);
            }

        }
        public versiyonnotlariModel versiyonnotlari()
        {


            string folderName = "app-assets/VersiyonNotlari";

          
            string rootFolder = Path.Combine(_hostingEnvironment.WebRootPath, folderName);

            versiyonnotlariModel versiyonnotlariModel = new versiyonnotlariModel();
            versiyonnotlariModel.VersiyonNotlarilistesi = new List<versiyonnotlariModel.VersiyonModel>();

       
            string[] subdirectories = Directory.GetDirectories(rootFolder);

           
            

            foreach (string subdirectory in subdirectories)
            {
                versiyonnotlariModel.VersiyonModel versiyonModel = new versiyonnotlariModel.VersiyonModel();
      
                string[] txtFiles = Directory.GetFiles(subdirectory, "*.txt");
                versiyonModel.versiyon = Path.GetFileName(subdirectory);
                foreach (string txtFile in txtFiles)
                {
                    // Yeni bir versiyon modeli oluştur
                   

                    // Versiyon bilgisini ayarla
               

                    // Not defterinin içeriğini oku
                    string content = System.IO.File.ReadAllText(txtFile);
                  
                    // Not bilgisini ayarla
                    versiyonModel.not = content;

                    // Versiyon modelini listeye ekle
                    versiyonnotlariModel.VersiyonNotlarilistesi.Add(versiyonModel);

                }
            }
            return versiyonnotlariModel;
        }
        public void Logout()
        {


            ModulHeaderVMData modulHeaderVM;
            kullanici_Aktive_Control aktive_Control = new ();
            if (_httpContextAccessorSession.HttpContext.Session.GetString("User") is not null)
            {


                if (_memoryCache.Get<ModulHeaderVMData>(_httpContextAccessorSession.HttpContext.Session.GetString("User")) is not null)
                {


                    modulHeaderVM = _memoryCache.Get<ModulHeaderVMData>(_httpContextAccessorSession.HttpContext.Session.GetString("User"));
                    if (modulHeaderVM is not null)
                    {
                        if (aktive_Control.kullanici_kontrol(modulHeaderVM.kullanici_id.ToString(), _httpContextAccessorSession.HttpContext.Session.GetString("User")) == true)
                        {

                            aktive_Control.updateFalse(modulHeaderVM.kullanici_id);
                        }
                    }
                }
            }

        }

        public Kullanicilar loginKontrol(string User, string Pass)
        {



            LoginRepository login = new();
            Kullanicilar kullanici = login.Getbykullanici(User, Pass);
            Hash256 hash256 = new Hash256();

            if (kullanici is not null)
            {



                if (kullanici.Aktif == true && kullanici.KullaniciAd == User && hash256.HashCozumle(Pass,kullanici.sifre)==true)
                {


                    return kullanici;


                }
                else
                {

                    kullanici = null;
                    return kullanici;


                }
            }
            else
            {
                return kullanici;
            }




        }
      
        public IActionResult Login(Users_Model _Modeluser)
        {

            Logout();
            versiyonnotlariModel versiyonnotlariModel =versiyonnotlari();

            ViewData["versiyon"] = versiyonnotlariModel;

            //bool v=  openjops.islem();

            if (_Modeluser.Username != null && _Modeluser.Password != null)
            {

                var kullanicilar = loginKontrol(User: _Modeluser.Username, Pass: _Modeluser.Password);
                if (kullanicilar is not null)
                {


                    if (kullanicilar.id != 0 && kullanicilar.KullaniciAd != null && kullanicilar.sifre != null)
                    {
                        _httpContextAccessorSession.HttpContext.Session.SetString("User", kullanicilar.id.ToString());



                        kullanici_Aktive_Control aktive_Control = new ();
                        ModulHeaderVMData modulHeaderVM = new ();
                        //if (_memoryCache.Get<ModulHeaderVMData>(_httpContextAccessorSession.HttpContext.Session.GetString("User")) is null)   modulleri yenilesin diye 
                        //{
                        modulHeaderVM = _opening.Modulİslemleri(kullanicilar);
                        //}


                        if (aktive_Control.kullanici_kontrol(kullanicilar.id.ToString(), _httpContextAccessorSession.HttpContext.Session.GetString("User")) == true)
                        {

                            aktive_Control.updateFalse(modulHeaderVM.kullanici_id);
                            aktive_Control.insert(int.Parse(modulHeaderVM.kullanici_id), _httpContextAccessorSession.HttpContext.Session.GetString("User"));


                        }
                        else
                        {
                            aktive_Control.insert(int.Parse(modulHeaderVM.kullanici_id), _httpContextAccessorSession.HttpContext.Session.GetString("User"));
                        }
                        ModulSetle(modulHeaderVM);

                        return RedirectToAction("Index", "AnaEkran");
                    }
                    else
                    {
                        ViewBag.Message = "Kullanici Girişi Başarısız";

                    }

                }
                else
                {
                    ViewBag.Message = "Kullanici Girişi Hatalı";
                }
            }
            return View();


        }
    }
}
