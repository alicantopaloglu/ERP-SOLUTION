using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer;
using EntityLayer.Entities;

namespace BusinessLayer
{
    public class FirmalarManager
    {
        GenericRepository<FIRMALAR> gr = new GenericRepository<FIRMALAR>();

        public List<FIRMALAR> getAll()
        {
            return gr.List();
        }

        public string addFirma(FIRMALAR f)
        {
            try
            {
                if(gr.Insert(f) == 1)
                {
                    return "FİRMA EKLEME İŞLEMİ BAŞARILI";
                }
                else
                {
                    return "FİRMA EKLEME İŞLEMİ BAŞARISIZ";
                }
            }
            catch (Exception ex)
            {

                return ex.ToString();
            }
            
        }

        public string updateFirma(FIRMALAR f)
        {
            FIRMALAR value = gr.Find(x => x.firmaID == f.firmaID);
            try
            {
                if(value != null)
                {
                    value.firmaadi = f.firmaadi;
                    value.adresbilgisi = f.adresbilgisi;
                    value.telefon = f.telefon;
                    value.vergidairesi = f.vergidairesi;
                    value.vergino = f.vergino;
                    gr.Update(value);
                    return "FİRMA GÜNCELLENDİ";
                }
                else
                {
                    return "FİRMA BİLGİSİ BULUNAMADI";
                }
            }
            catch (Exception ex )
            {

                return ex.ToString();
            }
            

        }

        public List<FIRMALAR> getFirma(int id)
        {
            return gr.List(x => x.firmaID == id);
        }

    }
}
