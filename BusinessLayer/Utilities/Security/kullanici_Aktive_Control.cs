using System;
using BusinessLayer.Utilities.Security.Hash;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;



namespace BusinessLayer.Utilities.Security
{
    public class kullanici_Aktive_Control
    {


        readonly Hash256 Hash256 = new Hash256();
        readonly TanimGenericRepositories<kullanici_Aktive> genericKullanici_aktive = new();

      



        public void insert(int kullanici_id , string session)

        {

            kullanici_Aktive kullanici_Aktive = new();
            kullanici_Aktive.kullanici_id = kullanici_id;
            kullanici_Aktive.aktif = true;
            kullanici_Aktive.Sure = DateTime.Now;
            kullanici_Aktive.AktifSure = DateTime.Now;
            kullanici_Aktive.key = Hash256.HashCreate(kullanici_Aktive.kullanici_id.ToString() + session,session);

            genericKullanici_aktive.Insert(kullanici_Aktive);
        }


        public void update(string kullanici_ID)
        {



            var gelen = genericKullanici_aktive.GetlistAll().FindAll(x => x.kullanici_id == int.Parse(kullanici_ID)).Find(X => X.aktif == true);
            gelen.aktif = true;
            gelen.Sure = DateTime.Now;



            genericKullanici_aktive.Update(gelen);
        }
        public void updateFalse(string kullanici_ID)
        {



            var gelen = genericKullanici_aktive.GetlistAll().FindAll(x => x.kullanici_id == int.Parse(kullanici_ID)).Find(X => X.aktif == true);
            gelen.aktif = false;




            genericKullanici_aktive.Update(gelen);
        }
        public bool kullanici_kontrol(string kullanici_ID , string session)
        {



            var gelen = genericKullanici_aktive.GetlistAll().FindAll(x => x.kullanici_id == int.Parse(kullanici_ID)).Find(X => X.aktif == true);



            if (gelen is not null)
            {


                if (Hash256.ValidateHash(gelen.kullanici_id.ToString() + session, session,gelen.key)==true && DateTime.Now < gelen.Sure.Value.AddMinutes(5) && gelen.aktif == true)
                {
                    return true;
                }
                else
                {
                    gelen.aktif = false;
                    genericKullanici_aktive.Update(gelen);
                    return false;
                }


            }
            else
            {
                return false;
            }
        }



    }
}
