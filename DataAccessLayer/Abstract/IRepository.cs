using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data.SqlClient;
using EntityLayer.Entities;

namespace DataAccessLayer.Abstract
{
    public interface IRepository<T>
    {
        int Insert(T p);
        int Insert2(T p);
        int Update(T p);
        int Delete(T p);
        T projeAnlasma(Expression<Func<T, bool>> where);
        List<T> getGirisler(Expression<Func<T, bool>> where, Expression<Func<T, bool>> where3, Expression<Func<T, DateTime>> where2);
        T getKullanicilar(Expression<Func<T, bool>> where, Expression<Func<T, bool>> where2);
       
        List<T> List();
        List<T> List(Expression<Func<T, bool>> where);
        List<T> List(Expression<Func<T, bool>> where, Expression<Func<T, bool>> where2);
        List<T> List(Expression<Func<T, bool>> where, Expression<Func<T, bool>> where2, Expression<Func<T, bool>> where3);
       

        IQueryable StokDurumuListCustom();
        IQueryable StokCikisiListCustom(int id);
        IQueryable BirimListCustom();
        IQueryable projeRaporCustom(int id);
        List<PROJERAPOR> projedeneme();

        T get(Expression<Func<T, bool>> where);
        T GetById(int id);
        T Find(Expression<Func<T, bool>> where);

    }
}
