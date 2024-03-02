using DataAccessLayer.Utilities;
using Microsoft.AspNetCore.Mvc;
using EntityLayer.Concrete;
using EntityLayer.Models;
using System.Collections.Generic;
using System.Linq;
 
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace WEB.Controllers
{
    public class VersiyonController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public VersiyonController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
           
            versiyonnotlariModel versiyonnotlariModel;

            versiyonnotlariModel= versiyonnotlari();

            ViewData["versiyon"] = versiyonnotlariModel;
            return View();
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
                //string folderName = new DirectoryInfo(subdirectory).Name;

                // Alt klasördeki tüm .txt dosyalarını al
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

        [HttpGet]
        public ActionResult GetNotes(string version)
        {
            // Versiyon numarasına göre notları al
            // Bu kısmı veritabanınızdan veya başka bir veri kaynağından notları çekmek için değiştirmeniz gerekebilir

            versiyonnotlariModel versiyonnotlariModel;
          
            versiyonnotlariModel = versiyonnotlari();

            var ver = versiyonnotlariModel.VersiyonNotlarilistesi.FindAll(x => x.versiyon == version);
            List<string> notes = ver.Select(x => x.not).ToList();

            // Notları bir partial view'e aktar ve geri döndür
            return PartialView("_NotesPartialView", notes);
        }
    }
}
