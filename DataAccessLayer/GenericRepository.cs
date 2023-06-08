using DataAccessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Data;


namespace DataAccessLayer
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        SqlBaglanti bgl = new SqlBaglanti();
        Context c = new Context();
        DbSet<T> _object;
        public GenericRepository()
        {
            _object = c.Set<T>();
        }
        public List<T> getGirisler(Expression<Func<T, bool>> where, Expression<Func<T, bool>> where2, Expression<Func<T, DateTime>> where3)
        {
            var value = _object.Where(where).Where(where2).OrderBy(where3).ToList();
            c.SaveChanges();

            return value;

        }
        public T getKullanicilar(Expression<Func<T, bool>> where, Expression<Func<T, bool>> where2)
        {
            return _object.Where(where).Where(where2).FirstOrDefault();
        }


        public decimal count(Expression<Func<T, bool>> where, Expression<Func<T, decimal>> where2)
        {
            return _object.Where(where).Sum(where2);
        }
        public T projeAnlasma(Expression<Func<T, bool>> where)
        {
            return _object.Where(where).FirstOrDefault();
        }

        public int Delete(T p)
        {
            throw new NotImplementedException();
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            return _object.FirstOrDefault(where);
        }

        public T get(Expression<Func<T, bool>> where)
        {
            return _object.FirstOrDefault(where);
        }

        public T GetById(int id)
        {
            return _object.Find(id);
        }

        public object getGirisler(Func<STOKGIRISI, bool> p1, bool v, Func<object, object> p2)
        {
            throw new NotImplementedException();
        }

        public int Insert(T p)
        {
            _object.Add(p);
            return c.SaveChanges();

        }
        public int Insert2(T p)
        {
            _object.Add(p);
            return 1;
        }

        public List<T> List()
        {
            return _object.ToList();
        }

        public List<T> List(Expression<Func<T, bool>> where)
        {
            return _object.Where(where).ToList();
        }
        public List<T> List(Expression<Func<T, bool>> where, Expression<Func<T, bool>> where2, Expression<Func<T, bool>> where3)
        {
            return _object.Where(where).Where(where2).Where(where3).ToList();
        }
        public List<T> List(Expression<Func<T, bool>> where, Expression<Func<T, bool>> where2)
        {
            return _object.Where(where).Where(where2).ToList();
        }



        public IQueryable StokDurumuListCustom()
        {
            var query = from item in c.STOKDURUMU
                        join item2 in c.STOKKARTI
                        on item.stokkodu equals item2.stokkodu
                        orderby item.stokkodu ascending
                        select new
                        {
                            item.toplamtutar,
                            item.ortalamabirimfiyati,
                            item.kullanilabilirmiktar,
                            item.stokkodu,
                            item.rezervealinmismiktar,
                            item2.urunadi,
                            item2.tanim,
                            item2.kalinlik,
                            item2.birim,
                            item2.boy,
                            item2.en,
                            item.miktar,
                            item2.yuzey,
                            item2.uretici



                        };
            return query;
        }

        public IQueryable StokCikisiListCustom(int id)
        {
            var query = from item in c.STOKCIKISI
                        where item.kullaniciID == id
                        join item2 in c.STOKKARTI
                        on item.stokkodu equals item2.stokkodu

                        orderby item.stokkodu ascending
                        select new
                        {

                            item.birimfiyati,
                            item.stokcikisID,
                            item.stokgirisID,
                            tarih = item.tarih.ToShortDateString(),
                            item.miktar,
                            item.stokkodu,
                            item.toplamtutar,
                            item.projekodu,
                            item2.tanim,
                            item2.urunadi,
                            item2.kalinlik,
                            item2.boy,
                            item2.en,
                            item2.birim,
                            item2.yuzey
                        };


            return query;
        }
        public IQueryable projeRaporCustom(int id)
        {
            var query = from item in c.STOKCIKISI
                        where item.projekodu == id
                       
                        group item by new { item.stokkodu, item.birimfiyati } into g
                        select new
                        {
                            stokkodu = g.Key.stokkodu,
                            stokcikismiktari = g.Sum(x => x.miktar),
                            birimfiyati = g.Key.birimfiyati,
                            stokcikistoplam = g.Sum(x => x.toplamtutar)


                        };
            return query;
        }


        public IQueryable BirimListCustom()
        {
            var query = c.STOKKARTI.Select(x => x.birim).Distinct();






            return query;

        }
        public List<PROJERAPOR> projedeneme(int id)
        {
            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("WITH stokcikis ( stokkodu,stokcikismiktari,cikistoplamtutar) AS (SELECT stokkodu,SUM(miktar)," +
                "SUM(toplamtutar) from STOKCIKISI where projekodu='" + id + "' group by stokkodu ), stokgiris ( stokkodu,stokgirismiktari,giristoplamtutar)" +
                "AS(select stokkodu, SUM(miktar) ,SUM(toplamtutar)from STOKGIRISI where projekodu = '" + id + "' group by stokkodu), stokkarti " +
                "(stokkodu,urunadi,uretici,yuzey,boy,en,kalinlik,tanim) AS (select stokkodu,urunadi,uretici,yuzey,boy,en,kalinlik,tanim from STOKKARTI" +
                ")select coalesce(a.stokkodu, b.stokkodu) AS Stokkodu, ISNULL(a.stokcikismiktari, 0 ) AS StokCikisMiktari,ISNULL(b.stokgirismiktari, 0 )AS StokGirisMiktari, ISNULL(a.cikistoplamtutar, 0 )AS CikisToplamTutar,ISNULL(b.giristoplamtutar, 0 ) AS GirisToplamTutar,c.urunadi," +
                "c.uretici,c.yuzey, c.boy,c.en,c.kalinlik,c.tanim from stokcikis AS a full join stokgiris as b  ON a.stokkodu = b.stokkodu full join stokkarti as c " +
                "on a.stokkodu = c.stokkodu where a.stokkodu=c.stokkodu", bgl.baglanti());
            da.Fill(dt);

            var projeList = new List<PROJERAPOR>(dt.Rows.Count);
            foreach (DataRow dr in dt.Rows)
            {
                var values = dr.ItemArray;
                PROJERAPOR rapor = new PROJERAPOR()
                {
                    stokkodu = Convert.ToInt32(dr[0]),
                    stokcikismiktari = Convert.ToDecimal(dr[1]),
                    stokgirismiktari = Convert.ToDecimal(dr[2]),
                    stokcikistoplam = Convert.ToDecimal(dr[3]),
                    stokgiristoplam = Convert.ToDecimal(dr[4]),
                    urunadi = Convert.ToString(dr[5]),
                    uretici = Convert.ToString(dr[6]),
                    yuzey = Convert.ToString(dr[7]),
                    boy = Convert.ToDecimal(dr[8]),
                    en = Convert.ToDecimal(dr[9]),
                    kalinlik = Convert.ToDecimal(dr[10]),
                    tanim = Convert.ToString(dr[11])



                };
                projeList.Add(rapor);
            }






            return projeList;


        }



        public int Update(T p)
        {
            _object.Update(p);
            return c.SaveChanges();
        }



        int IRepository<T>.Delete(T p)
        {
            throw new NotImplementedException();
        }

        T IRepository<T>.Find(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        T IRepository<T>.get(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        T IRepository<T>.GetById(int id)
        {
            throw new NotImplementedException();
        }

        int IRepository<T>.Insert(T p)
        {
            throw new NotImplementedException();
        }

        List<T> IRepository<T>.List()
        {
            throw new NotImplementedException();
        }

        List<T> IRepository<T>.List(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        int IRepository<T>.Update(T p)
        {
            throw new NotImplementedException();
        }

        public List<PROJERAPOR> projedeneme()
        {
            throw new NotImplementedException();
        }

      
    }
}
