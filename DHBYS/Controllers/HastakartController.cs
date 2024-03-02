using BusinessLayer.Utilities.Security;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using EntityLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WEB.Models;

namespace WEB.Controllers
{
    public class HastakartController : Controller
    {

        readonly IMemoryCache _memoryCache;
        readonly IHttpContextAccessor _httpContextAccessorSession;
        public HastakartController(IHttpContextAccessor session, IMemoryCache memoryCache)
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
        public IActionResult Index()
        {
            if (SessionKontrol() == true)
            {
                TanimGenericRepositories<TedaviTur> tanimGenericRepositories = new TanimGenericRepositories<TedaviTur>();


                HastakartModel hastakartModel = new HastakartModel();

				hastakartModel.TedaviTur = tanimGenericRepositories.GetlistAll();

                ViewData["HastakartModel"] = hastakartModel;


                return View();
            }
            else
            {
                return RedirectToAction("Index", "error");
            }
           
        }
    }
}
