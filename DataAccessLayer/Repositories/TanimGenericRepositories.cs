using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
  public  class TanimGenericRepositories<T> : IGenericTanimDal<T> where T : class
    {
         

   

        public void Delete(T t)
        {
            using var c = new Context();
            c.Remove(t);
            c.SaveChanges();

        }

        public T GetByID(int ID)
        {
            using var c = new Context();
            return c.Set<T>().Find(ID);
        }

        public T GetByString(string ID)
        {
            using var c = new Context();
            return c.Set<T>().Find(ID);
        }
        public List<T> GetlistAll()
        {
            using var c = new Context();
            return c.Set<T>().ToList();
        }

        public void Insert(T t)
        {
            using var c = new Context();
            c.Add(t);
            c.SaveChanges();
        }

        public void Update(T t)
        {
            using var c = new Context();
            c.Update(t);
            c.SaveChanges();
        }
    }
}
