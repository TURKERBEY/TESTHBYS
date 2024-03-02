using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Utilities.Log
{
    public class Logging<T> where T : class
    {
        //.net core audit logging  use for select

       readonly TanimGenericRepositories<LogKayit> LogkayitGeneric = new();  
        readonly TanimGenericRepositories<LogTablo> LogtabloBilgisiGeneric = new();
        readonly TanimGenericRepositories<LogTabloTanim> LogTabloTanimiGeneric = new();

         


       

        public void Goruntulemelogkayit(T t, int kullanici)
        {
            LogKayit Log = new LogKayit();
            var tablo_Id = LogtabloBilgisiGeneric.GetlistAll().Find(x => x.Adi == t.GetType().Name);
            var TabloTanim = LogTabloTanimiGeneric.GetByID(tablo_Id.id);

            if (TabloTanim.GoruntulemeLogu == true)
            {

                var properties = t.GetType().GetProperties();
                List<string> Görüntüleme = new List<string>();
                foreach (var property in properties)
                {
                    var PropertyName = property.Name;
                    //You get "Property1" as a result

                    var PropetyValue = t.GetType().GetProperty(property.Name).GetValue(t, null);

                    if (PropetyValue is not null && PropetyValue.ToString() != "0")
                    {
                        Görüntüleme.Add(t.GetType().GetProperty(property.Name).GetValue(t, null).ToString());
                    }
                    else
                    {
                        Görüntüleme.Add(property.Name.ToString() + ": " + "null");
                    }
                }
                Log.id = null;
                Log.Tarih = DateTime.Now;
                Log.islemtur = "Görüntüleme";
                Log.Tablo__Identity = int.Parse(Görüntüleme[0]);
                Log.Aciklama = tablo_Id.Adi + " Bilgisi Görüntülendi";
                Log.Tablo_id = tablo_Id.id;
                Log.kullanici_id = kullanici;
                LogkayitGeneric.Insert(Log);

            }
        }
        public void Deletelogkayit(T t, int kullanici)
        {
            LogKayit Log = new LogKayit();

            var tablo_Id = LogtabloBilgisiGeneric.GetlistAll().Find(x => x.Adi == t.GetType().Name);
            var TabloTanim = LogTabloTanimiGeneric.GetByID(tablo_Id.id);
            if (TabloTanim.SilmeLogu == true)
            {
                var properties = t.GetType().GetProperties();
                List<string> DeleteBilgisi = new List<string>();
                List<string> DeleteBilgisi_id = new List<string>();
                foreach (var property in properties)
                {
                    var PropertyName = property.Name;
                    //You get "Property1" as a result

                    var PropetyValue = t.GetType().GetProperty(property.Name).GetValue(t, null);

                    if (PropetyValue is not null && PropetyValue.ToString() != "0")
                    {
                        DeleteBilgisi_id.Add(t.GetType().GetProperty(property.Name).GetValue(t, null).ToString());
                        DeleteBilgisi.Add(property.Name.ToString() + ": " + t.GetType().GetProperty(property.Name).GetValue(t, null).ToString());
                    }
                    else
                    {
                        DeleteBilgisi.Add(property.Name.ToString() + ": " + "null");
                    }



                }


                foreach (var item in DeleteBilgisi)
                {
                    Log.Aciklama += item.ToString();
                }
                Log.Tarih = DateTime.Now;
                Log.islemtur = "Delete";
                Log.Tablo__Identity = int.Parse(DeleteBilgisi_id[0]);
                Log.Tablo_id = tablo_Id.id;
                Log.kullanici_id = kullanici;
                LogkayitGeneric.Insert(Log);
            }

        }

        public void insertlogkayit(T t, int kullanici)
        {
            LogKayit Log = new LogKayit();

            var tablo_Id = LogtabloBilgisiGeneric.GetlistAll().Find(x => x.Adi == t.GetType().Name);
            var TabloTanim = LogTabloTanimiGeneric.GetByID(tablo_Id.id);
            if (TabloTanim.KayitLogu == true)
            {
                var properties = t.GetType().GetProperties();
                List<string> insertBilgisi = new List<string>();
                List<string> insertBilgisi_id = new List<string>();
                foreach (var property in properties)
                {
                    var PropertyName = property.Name;
                    //You get "Property1" as a result

                    var PropetyValue = t.GetType().GetProperty(property.Name).GetValue(t, null);

                    if (PropetyValue is not null && PropetyValue.ToString() != "0")
                    {
                        insertBilgisi_id.Add(t.GetType().GetProperty(property.Name).GetValue(t, null).ToString());
                        insertBilgisi.Add(property.Name.ToString() + ": " + t.GetType().GetProperty(property.Name).GetValue(t, null).ToString());
                    }
                    else
                    {
                        insertBilgisi.Add(property.Name.ToString() + ": " + "null");
                    }


                }


                foreach (var item in insertBilgisi)
                {
                    Log.Aciklama += item.ToString();
                }
                Log.Tarih = DateTime.Now;
                Log.islemtur = "İnsert";
                Log.Tablo__Identity = int.Parse(insertBilgisi_id[0]);
                Log.Tablo_id = tablo_Id.id;
                Log.kullanici_id = kullanici;
                LogkayitGeneric.Insert(Log);
            }

        }

        public void Updatelogkayit(T t, int kullanici)

        {
            LogKayit Log = new LogKayit();
            TanimGenericRepositories<T> GelenClasTanimiGeneric = new TanimGenericRepositories<T>();
            var tablo_Id = LogtabloBilgisiGeneric.GetlistAll().Find(x => x.Adi == t.GetType().Name);
            var TabloTanim = LogTabloTanimiGeneric.GetByID(tablo_Id.id);
            if (TabloTanim.GuncellemeLogu == true)
            {


                var properties = t.GetType().GetProperties();
                List<string> UpdateBilgisi = new ();
                List<string> Updaoncesi = new ();
                foreach (var property in properties)
                {
                    var PropertyName = property.Name;
                    //You get "Property1" as a result

                    var PropetyValue = t.GetType().GetProperty(property.Name).GetValue(t, null);

                    if (PropetyValue is not null && PropetyValue.ToString() != "0")
                    {
                        Updaoncesi.Add(PropetyValue.ToString());
                        UpdateBilgisi.Add(PropertyName.ToString() + ": " + PropetyValue.ToString());
                    }
                    else
                    {
                        UpdateBilgisi.Add(PropertyName.ToString() + ": " + "null");
                    }


                }

                var OncekiVeri = GelenClasTanimiGeneric.GetByID(int.Parse(Updaoncesi[0]));
                Log.Tablo__Identity = int.Parse(Updaoncesi[0]);
                Updaoncesi.Clear();
                foreach (var property in OncekiVeri.GetType().GetProperties())
                {
                    var PropertyName = property.Name;
                    //You get "Property1" as a result

                    var PropetyValue = OncekiVeri.GetType().GetProperty(property.Name).GetValue(OncekiVeri, null);

                    if (PropetyValue is not null && PropetyValue.ToString() != "0")
                    {

                        Updaoncesi.Add(PropertyName.ToString() + ": " + PropetyValue.ToString());
                    }
                    else
                    {
                        Updaoncesi.Add(PropertyName.ToString() + ": " + "null");
                    }


                }


                int sayac = 0;
                foreach (var item in Updaoncesi)
                {
                    if (item != UpdateBilgisi[sayac])
                    {
						Log.Aciklama += item.ToString();
					}
                    sayac++;
                  
                }
                int sayac2 = 0;
                Log.Aciklama += "Sonrasi :";
                foreach (var item in UpdateBilgisi)
                {
                    if (item != Updaoncesi[sayac2])
                    {
						Log.Aciklama += item.ToString();
					}
                    sayac2++;

                }


                Log.Tarih = DateTime.Now;
                Log.islemtur = "Güncelleme";

                Log.Tablo_id = tablo_Id.id;
                Log.kullanici_id = kullanici;
                LogkayitGeneric.Insert(Log);
            }

        }





    }


}


