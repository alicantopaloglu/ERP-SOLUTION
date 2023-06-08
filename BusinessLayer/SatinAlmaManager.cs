using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer;
using EntityLayer.Entities;

namespace BusinessLayer
{
    public class SatinAlmaManager
    {
        GenericRepository<SIPARIS> gr = new GenericRepository<SIPARIS>();
        public string updateSiparis(SIPARIS s)
        {
            
            try
            {
                SIPARIS value = gr.Find(x => x.siparisID == s.siparisID);
                if(value == null)
                {
                    return "BÖYLE BİR SİPARİŞ BULUNAMADI";
                }
                else
                {
                    if (s.alinanmiktar > value.miktar - value.alinanmiktar || s.alinanmiktar <= 0)
                    {
                        return "BELİRLENEN MİKTARDAN FAZLA KAYIT OLUŞTURULAMAZ";
                    }
                    else
                    {
                        value.saciklama = s.saciklama;

                        value.kalanmiktar = value.miktar - (s.alinanmiktar + value.alinanmiktar);
                        if (value.kalanmiktar != 0)
                        {
                            value.durumu = "EKSİK VAR";
                        }
                        else
                        {
                            value.durumu = "SİPARİŞ VERİLDİ";
                        }

                        value.alinanmiktar = value.alinanmiktar + s.alinanmiktar;
                        value.siparisFirmaID = s.siparisFirmaID;
                        value.termintarihi = s.termintarihi;


                        gr.Update(value);
                        return "SİPARİŞ ONAYLANDI";


                    }
                }
               
            }
            catch (Exception ex)
            {

                return ex.ToString();
            }

            

        }

        public string updateStokGirisSiparis(SIPARIS s)
        {
            try
            {
                SIPARIS value = gr.Find(x => x.siparisID == s.siparisID);
                if(value!= null)
                {
                    value.stokeklenenmiktar += s.stokeklenenmiktar;
                    if (value.miktar == value.stokeklenenmiktar)
                    {
                        value.durumu = "SİPARİŞ TAMAMLANDI";
                    }
                    gr.Update(value);
                    return "SİPARİŞ TAMAMLANDI";
                }
                else
                {
                    return "BÖYLE BİR SİPARİŞ BULUNAMADI";
                }
            }
            catch (Exception ex)
            {

                return ex.ToString();
            }
           
           

        }


    }
}
