using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;
namespace DataAccessLayer.Abstract
{
   public interface IModulDal
    {
        List<Modul> ListAllModul();

        void AddModul(Modul modul);
        void DeleteModul(Modul modul);
        void Update(Modul modul);
        Modul GetById(int id);
    }
}
