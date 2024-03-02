using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IGenericDal<T> where T : class
    {
        void Insert(T t, int kullanici);
        void Delete(T t, int kullanici);

        void Update(T t, int kullanici);

        List<T> GetlistAll(T t, int kullanici);

        T GetByID(T t, int ID, int kullanici);
         
    }
}
