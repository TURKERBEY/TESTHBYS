using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Utilities.Log;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace DataAccessLayer.Repositories
{
    public class GenericRepositories<T> : IGenericDal<T> where T : class
    {
        readonly Logging<T> logging = new Logging<T>();
        public void Delete(T t ,int kullanici)
        {
           
            using var c = new Context();

            if (t is not null)
            {
                logging.Deletelogkayit(t, kullanici);
            }
                c.Remove(t);
            c.SaveChanges();

        }

        public T GetByID(T t,  int ID, int kullanici )
        {
        
            using var c = new Context();
             t= c.Set<T>().Find(ID);
            if (t is not null)
            {
                logging.Goruntulemelogkayit(t, kullanici);
            }
   
            return c.Set<T>().Find(ID);
        }

        
        public List<T> GetlistAll(T t, int kullanici)
        {

             var c = new Context();
            
            if (c.Set<T>() is not null)
            {

                var gelen = c.Set<T>().ToList();
                if (gelen.Distinct().Count() == 1)
                {
                    t = gelen[0];
                    logging.Goruntulemelogkayit(t, kullanici);
                }
                

            }

            return c.Set<T>().ToList();



        }

        public void Insert(T t, int kullanici)
        {

      
            using var c = new Context();
            c.Add(t);
            
       
          
            c.SaveChanges();

             
            if (t is not null)
            {
                logging.insertlogkayit(t, kullanici);
            }


        }

        public void Update(T t, int kullanici)
        {
           
            using var c = new Context();
            if (t is not null)
            {
                logging.Updatelogkayit(t, kullanici);
            }
                c.Update(t);
            c.SaveChanges();
        }
    }
}
