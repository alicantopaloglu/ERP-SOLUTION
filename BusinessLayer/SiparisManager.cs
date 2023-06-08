using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer;
using EntityLayer.Entities;

namespace BusinessLayer
{
    public class SiparisManager
    {

        GenericRepository<SIPARIS> gr = new GenericRepository<SIPARIS>();

        public List<SIPARIS> getAll()
        {
            return gr.List(x => x.siparisiptal == false, y => y.durumu != "SİPARİŞ TAMAMLANDI");
        }
        public List<SIPARIS> getStockOrder()
        {
            return gr.List(x => x.siparisiptal == false, y => y.durumu != "SİPARİŞ TAMAMLANDI", z => z.durumu != "SİPARİŞ KAYDI AÇILDI");
        }
        public List<SIPARIS> getUserOrder(int kullaniciID)
        {
            return gr.List(x => x.kullaniciID == kullaniciID,y=>y.siparisiptal==false,z=>z.durumu!="SİPARİŞ TAMAMLANDI");
        }

        

        public string addSiparis(SIPARIS s)
        {

            
            try
            {
                s.stokeklenenmiktar = 0;
                s.alinanmiktar = 0;
                s.kalanmiktar = s.miktar;
                s.siparisiptal = false;
                s.durumu = "SİPARİŞ KAYDI AÇILDI";
                s.siparisFirmaID = 0;
                s.tarih = DateTime.Now.Date;
                s.saciklama = "";
                if(s.taleptarihi == null)
                {
                    s.taleptarihi = DateTime.Now.Date;
                }
                if(s.termintarihi == null)
                {
                    s.termintarihi= DateTime.Now.Date;  
                }
                if (s.miktar <= 0)
                {
                    return "SİPARİŞ MİKTARI 0 VEYA 0'DAN KÜÇÜK OLAMAZ";
                }
                else
                {
                    if (gr.Insert(s) == 1)
                    {
                        return "SİPARİŞ KAYDI AÇILDI";
                    }
                    else
                    {
                        return "SİPARİŞ KAYDI AÇILAMADI";
                    }
                }
                
            }
            catch (Exception ex)
            {

                return ex.ToString();
            }
            

          
        }
       

        public string updateSiparis(SIPARIS s)
        {
            
            try
            {
                SIPARIS value = gr.Find(x => x.siparisID == s.siparisID);
                if(value != null)
                {
                    if (value.alinanmiktar > 0 || value.stokeklenenmiktar > 0)
                    {
                        return "STOK GİRİŞİ YAPILMIŞ SİPARİŞLER DÜZENLENEMEZ";
                    }
                    else
                    {
                        if (s.taleptarihi != null)
                        {
                            value.taleptarihi = s.taleptarihi;
                        }
                        if(s.termintarihi != null)
                        {
                            value.termintarihi = s.termintarihi;
                        }
                        if(s.firmaID != 0)
                        {
                            value.firmaID = s.firmaID;
                        }
                        if(s.projekodu != 0)
                        {
                            value.projekodu= s.projekodu;
                        }
                        value.aciklama = s.aciklama;
                        value.miktar = s.miktar;
                        value.kalanmiktar = s.miktar - value.alinanmiktar;
                      
                        value.projekodu = s.projekodu;
                        
                        

                        gr.Update(value);
                        return "SİPARİŞ KAYDI BAŞARIYLA GÜNCELLENDİ";
                    }
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


        public string iptalSiparis(SIPARIS s)
        {
            try
            {
                SIPARIS value = gr.Find(x => x.siparisID == s.siparisID);
                if (value != null)
                {
                    value.siparisiptal = true;
                    gr.Update(value);
                    return "SİPARİŞ İPTAL EDİLDİ";
                }
                else
                {
                    return "BÖYLE BİR SİPARİŞ KAYDI BULUNAMADI";
                }

            }
            catch (Exception ex)
            {

                return ex.ToString();
            }
            
          
        }

        public List<SIPARIS> getSiparis(int id)
        {
            return gr.List(x => x.siparisID == id);
        }
    }
}
