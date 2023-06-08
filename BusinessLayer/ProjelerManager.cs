using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataAccessLayer;
using EntityLayer.Entities;

namespace BusinessLayer
{
    public class ProjelerManager
    {

        GenericRepository<PROJELER> gr = new GenericRepository<PROJELER>();
        GenericRepository<PROJERAPOR> pr = new GenericRepository<PROJERAPOR>();
        SqlBaglanti bgl = new SqlBaglanti();
       

        public List<PROJELER> getAll()
        {
            return gr.List();
        }
        public IQueryable getProjeRapor(int id) 
        {
            return gr.projeRaporCustom(id);
        }

        public PROJELER getSelectedProje(int id) 
        {
            return gr.projeAnlasma(x=>x.projekodu == id);
        }

        public string addProje(PROJELER p)
        {
           
          
            try
            {
                if(gr.Insert(p) == 1)
                {
                    return "PROJE EKLEME İŞLEMİ BAŞARILI";
                }
                else
                {
                    return "PROJE EKLEME İŞLEMİ BAŞARISIZ";
                }

            }
            catch (Exception ex)
            {

                return ex.ToString();
            }
        }

        public string updateProje(PROJELER p)
        {
            PROJELER value = gr.Find(x => x.projeID == p.projeID);
            try
            {
                if (value != null)
                {
                    value.firma = p.firma;
                    value.proje = p.proje;
                    value.projeadi = p.projeadi;
                    value.anlasmatutari = p.anlasmatutari;
                    gr.Update(value);
                    return "PROJE BAŞARIYLA GÜNCELLENDİ";
                }
                else
                {
                    return "BÖYLE BİR PROJE BULUNAMADI";
                }

            }
            catch (Exception ex)
            {

                return ex.ToString();
            }
           



        }

        public List<PROJELER> getProje(int id)
        {
            return gr.List(x => x.projekodu == id);
        }
        public List<PROJERAPOR> projedeneme(int id)
        {
            return pr.projedeneme(id);
           
        }

      
    }
}
