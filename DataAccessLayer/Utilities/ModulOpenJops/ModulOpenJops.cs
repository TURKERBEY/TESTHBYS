using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using EntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Utilities.ModulOpenJops
{
   public class ModulOpenJops
    {
        readonly TanimGenericRepositories<Modul> _tanimGenericRepositories = new();
        public List<Modul> ModulBilgisiCek(ModulHeaderVMData modulHeaderVM)
        {
         

            return modulHeaderVM.moduls;
         }

    }
}
