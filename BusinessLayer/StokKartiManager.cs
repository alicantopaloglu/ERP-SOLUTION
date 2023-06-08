using DataAccessLayer;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer
{
    public class StokKartiManager
    {

        GenericRepository<STOKKARTI> gr = new GenericRepository<STOKKARTI>();
        StokDurumuManager sm = new StokDurumuManager();
        STOKDURUMU sd = new STOKDURUMU();





        public List<STOKKARTI> getAll()
        {
            return gr.List();
        }

        public IQueryable getBirim()
        {
            return gr.BirimListCustom();
        }

        public string addStokKarti(STOKKARTI s)
        {
            
            s.tarih = DateTime.Now.Date;
            try
            {

                
              

                if (s.minmiktar < 0 || s.maxmiktar < 0)
                {
                    return "Minumum ve Maksimum miktar 0'dan küçük olamaz";
                }
                else
                {
                    
                    if (gr.Insert(s) == 1)
                    {
                        sd.stokkodu = s.stokkodu;
                        if (sm.addStokDurumu(sd) == 1)
                        {
                            return "STOK EKLEME İŞLEMİ BAŞARILI";
                        }
                        else
                        {
                            return "GENEL STOKLARA EKLENEMEDİ";
                        }
                       
                    }
                    else
                    {
                        return "STOK EKLENME İŞLEME BAŞARISIZ";
                    }
                }

            }
            catch (Exception ex)
            {

                return ex.ToString();
            }

        }

        public string updateStokKarti(STOKKARTI s)
        {
            STOKKARTI value = gr.Find(x => x.stokkodu == s.stokkodu);

            try
            {
                if (value != null)
                {
                    value.maxmiktar = s.maxmiktar;
                    value.minmiktar = s.minmiktar;
                    value.tanim = s.tanim;
                    value.urunadi = s.urunadi;
                    value.uretici = s.uretici;
                    value.yuzey = s.yuzey;
                    value.birim = s.birim;
                    value.boy = s.boy;
                    value.en = s.en;
                    value.kalinlik = s.kalinlik;
                    gr.Update(value);
                    return "Güncellendi";
                }
                else
                {
                    string mesaj = "Böyle bir stok kodu bulunamadı";
                    return mesaj;
                }
            }
            catch (Exception ex)
            {

                return ex.ToString();
            }

        }

        public List<STOKKARTI> getStok(int id)
        {
            return gr.List(x => x.stokkodu == id);
        }
    }
}
