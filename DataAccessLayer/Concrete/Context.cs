using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete
{
    public class Context : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Server=tcp:okyssunucu.database.windows.net,1433;Initial Catalog=OKYS;Persist Security Info=False;User ID=OkysData;Password=Saka9155.test123.4;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;", option =>
            {
                option.EnableRetryOnFailure();
               // option.EnableRetryOnFailure(
               // maxRetryCount: 5,
               // maxRetryDelay: TimeSpan.FromSeconds(60),
               //errorNumbersToAdd: null);
            });


            //if (System.Environment.GetEnvironmentVariable("COMPUTERNAME") == "SAMETTURKER")
            //{
            //    optionsBuilder.UseSqlServer("Data Source=SAMETTURKER\\SQLEXPRESS;Initial Catalog=HBYS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", option =>
            //    {
            //        option.EnableRetryOnFailure();
            //        option.EnableRetryOnFailure(
            //        maxRetryCount: 5,
            //        maxRetryDelay: TimeSpan.FromSeconds(60),
            //       errorNumbersToAdd: null);
            //    });
            //}
            //else
            //{
            //    optionsBuilder.UseSqlServer("Data Source=BATUHANBOZKURT\\SQLEXPRESS;Initial Catalog=HBYS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", option =>
            //    {
            //        option.EnableRetryOnFailure();
            //        option.EnableRetryOnFailure(
            //        maxRetryCount: 5,
            //        maxRetryDelay: TimeSpan.FromSeconds(60),
            //       errorNumbersToAdd: null);
            //    });
            //}


        }

        public DbSet<Kullanicilar> Kullanicilar { get; set; }
        public DbSet<Modul> Modul { get; set; }

        public DbSet<kullanici_Yetki> kullanici_Yetki { get; set; }
        public DbSet<Hasta> Hasta { get; set; }

        public DbSet<kullanici_Aktive> kullanici_Aktive { get; set; }

        public DbSet<YetkiGrupTur> YetkiGrupTur { get; set; }

        public DbSet<LogKayit> LogKayit { get; set; }

        public DbSet<LogTablo> LogTablo { get; set; }
        public DbSet<LogTabloTanim> LogTabloTanim { get; set; }
        public DbSet<HastaKayitTur> HastaKayitTur { get; set; }

        public DbSet<Ulke> Ulke { get; set; }

        public DbSet<TedaviTur> TedaviTur { get; set; }

		public DbSet<Personel> Personel { get; set; }
		public DbSet<Tema> Tema { get; set; }
		

	}


}
