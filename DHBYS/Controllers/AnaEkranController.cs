
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
    public class AnaEkranController : Controller
    {
        
        IMemoryCache _memoryCache;
        IHttpContextAccessor _httpContextAccessorSession;
        public AnaEkranController(IHttpContextAccessor session, IMemoryCache memoryCache)
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
		[HttpPost]
		public void Tema(int Tema)
		{
			if (SessionKontrol() == true)
			{
				ModulHeaderVMData _modulHeaderVM = new();
				_modulHeaderVM = _memoryCache.Get<ModulHeaderVMData>(_httpContextAccessorSession.HttpContext.Session.GetString("User"));
				TanimGenericRepositories<Tema> tanimGenericRepositories = new TanimGenericRepositories<Tema>();
				var GelenTema = tanimGenericRepositories.GetlistAll().Find(x => x.Kullanici_id == int.Parse(_modulHeaderVM.kullanici_id));
				if (GelenTema is null)
				{
					Tema tema = new Tema();
					tema.Kullanici_id = int.Parse(_modulHeaderVM.kullanici_id);
					tema.Dark = true;
					tanimGenericRepositories.Insert(tema);

					_modulHeaderVM.TemaDark = true;
					_memoryCache.Remove(_httpContextAccessorSession.HttpContext.Session.GetString("User"));

					if (_memoryCache.Get<ModulHeaderVMData>(_httpContextAccessorSession.HttpContext.Session.GetString("User")) is null)
					{
						_memoryCache.Set<ModulHeaderVMData>(_httpContextAccessorSession.HttpContext.Session.GetString("User"), _modulHeaderVM);
					}
				}
				else
				{
					if (GelenTema.Dark == true)
					{
						GelenTema.Dark = false;
						tanimGenericRepositories.Update(GelenTema);
						
						_modulHeaderVM.TemaDark = false;
						_memoryCache.Remove(_httpContextAccessorSession.HttpContext.Session.GetString("User"));

						if (_memoryCache.Get<ModulHeaderVMData>(_httpContextAccessorSession.HttpContext.Session.GetString("User")) is null)
						{
							_memoryCache.Set<ModulHeaderVMData>(_httpContextAccessorSession.HttpContext.Session.GetString("User"), _modulHeaderVM);
						}
					}
					else
					{
						GelenTema.Dark = true;
						tanimGenericRepositories.Update(GelenTema);
						_modulHeaderVM.TemaDark = true;
						_memoryCache.Remove(_httpContextAccessorSession.HttpContext.Session.GetString("User"));

						if (_memoryCache.Get<ModulHeaderVMData>(_httpContextAccessorSession.HttpContext.Session.GetString("User")) is null)
						{
							_memoryCache.Set<ModulHeaderVMData>(_httpContextAccessorSession.HttpContext.Session.GetString("User"), _modulHeaderVM);
						}
					}
				}
			}
		 
		}
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
