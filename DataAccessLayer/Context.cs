using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=localhost;database=HedlabErpV3;integrated security=true;");
            //optionsBuilder.UseSqlServer("workstation id=erpsolutiondatabase.mssql.somee.com;packet size=4096;user id=alicantopaloglu2_SQLLogin_1;pwd=kpy63toqj3;data source=erpsolutiondatabase.mssql.somee.com;persist security info=False;initial catalog=erpsolutiondatabase");
        }


        public DbSet<STOKKARTI> STOKKARTI { get; set; }
        public DbSet<STOKGIRISI> STOKGIRISI { get; set; }
        public DbSet<STOKCIKISI> STOKCIKISI { get; set; }
        public DbSet<STOKDURUMU> STOKDURUMU { get; set; }
        public DbSet<SIPARIS> SIPARIS { get; set; }
        public DbSet<PROJELER> PROJELER { get; set; }
        public DbSet<FIRMALAR> FIRMALAR { get; set; }
        public DbSet<KULLANICILAR> KULLANICILAR { get; set; }
        

    }
}
