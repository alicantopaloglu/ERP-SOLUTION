using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer;
using EntityLayer.Entities;

namespace BusinessLayer
{
    public class StokGirisiManager
    {
        GenericRepository<STOKGIRISI> gr = new GenericRepository<STOKGIRISI>();
        GenericRepository<STOKDURUMU> gr2 = new GenericRepository<STOKDURUMU>();
        GenericRepository<SIPARIS> gr3 = new GenericRepository<SIPARIS>();
        GenericRepository<STOKCIKISI> gr4 = new GenericRepository<STOKCIKISI>();
        StokDurumuManager sm = new StokDurumuManager();
        SatinAlmaManager satinAlmaManager = new SatinAlmaManager();

        public List<STOKGIRISI> getAll()
        {
            return gr.List();
        }

        public List<STOKGIRISI> getStokGirisi(int stokgirisID)
        {
            return gr.List(x => x.stokgirisID == stokgirisID);
        }
        #region addStokGirisi
        public string addStokGirisi(STOKGIRISI s)
        {
            try
            {
                STOKDURUMU sd = gr2.Find(x => x.stokkodu == s.stokkodu);
                SIPARIS sp = gr3.Find(x => x.siparisID == s.siparisID);
                if (sd != null && sp != null)
                {
                    var kdvDahilBirimFiyati = (s.kdv / 100 * s.birimfiyati) + s.birimfiyati;

                    if (s.miktar <= sp.miktar - sp.stokeklenenmiktar && s.miktar <= sp.alinanmiktar)
                    {
                        if (s.miktar <= 0 || s.kdv <= 0 || s.birimfiyati <= 0)
                        {
                            return "MİKTAR,KDV,BİRİM FİYATI 0'DAN KÜÇÜK OLAMAZ";

                        }
                        else
                        {
                            if (sd.miktar == 0)
                            {
                                sd.miktar = s.miktar;
                                sd.kullanilabilirmiktar = s.miktar;
                                sd.ortalamabirimfiyati = kdvDahilBirimFiyati;
                                sd.toplamtutar = kdvDahilBirimFiyati * s.miktar;

                            }

                            else
                            {
                                var totalMiktar = sd.miktar + s.miktar;
                                var totalKullanilabilirMiktar = sd.kullanilabilirmiktar + s.miktar;
                                var totalToplamTutar = sd.toplamtutar + (s.miktar * kdvDahilBirimFiyati);

                                sd.miktar = totalMiktar;
                                sd.kullanilabilirmiktar = totalKullanilabilirMiktar;
                                sd.toplamtutar = totalToplamTutar;
                                sd.ortalamabirimfiyati = totalToplamTutar / totalMiktar;
                            }
                            s.kdvdahilbirimfiyati = kdvDahilBirimFiyati;
                            s.toplamtutar = kdvDahilBirimFiyati * s.miktar;
                            s.kullanilabilirmiktar = s.miktar;
                            s.kdvtutari = (kdvDahilBirimFiyati - s.birimfiyati) * s.miktar;
                            s.tarih = DateTime.UtcNow.Date;
                            s.firmaID = sp.siparisFirmaID;
                            s.projekodu = sp.projekodu;

                            sp.stokeklenenmiktar = s.miktar;
                            satinAlmaManager.updateStokGirisSiparis(sp);

                            sm.updateStokDurumu(sd);
                            gr.Insert(s);
                            return "STOK EKLEME BAŞARILI";
                        }
                    }
                    else
                    {
                        if (s.miktar <= 0 || s.kdv <= 0 || s.birimfiyati <= 0 || s.miktar.Equals(null) || s.kdv.Equals(null) || s.birimfiyati.Equals(null))
                        {
                            return "MİKTAR,KDV,BİRİM FİYATI 0'DAN KÜÇÜK OLAMAZ";

                        }
                        else
                        {
                            return "SİPARİŞ MİKTARINDAN FAZLA MİKTARDA GİRİŞ YAPILAMAZ";
                        }

                    }
                }
                else
                {
                    return "BÖYLE BİR STOK BULUNAMADI";
                }
            }
            catch (Exception ex)
            {

                return ex.ToString();
            }

        }
        #endregion

        public string updateStokGirisi(STOKGIRISI s)
        {
            try
            {
                STOKDURUMU sd = gr2.Find(x => x.stokkodu == s.stokkodu);
                STOKGIRISI sg = gr.Find(x => x.stokgirisID == s.stokgirisID);

                STOKCIKISI sc = gr4.Find(x => x.stokgirisID == s.stokgirisID);
                if (sd == null)
                {
                    return "BÖYLE BİR STOK KARTI OLUŞTURULMAMIŞ";
                }
                else
                {
                    if (sg == null)
                    {
                        return "BÖYLE BİR STOK GİRİŞİ BULUNAMADI";
                    }
                    else
                    {
                        if (sc != null && sg.miktar != sg.kullanilabilirmiktar)
                        {
                            return "STOK ÇIKIŞI YAPILAN STOKLAR GÜNCELLENEMEZ";
                        }
                        else
                        {
                            if (s.miktar > sg.miktar)
                            {
                                return "İLK GİRİLEN MİKTARDAN DAHA FAZLA GİRİLEMEZ";
                            }
                            else
                            {
                                var kdvDahilBirimFiyati = (s.kdv / 100 * s.birimfiyati) + s.birimfiyati;

                                sd.miktar = sd.miktar - sg.miktar + s.miktar;
                                sd.kullanilabilirmiktar = sd.kullanilabilirmiktar - sg.miktar + s.miktar;
                                var toplamtutar = sd.toplamtutar - sg.toplamtutar;
                                sd.toplamtutar = toplamtutar + (s.miktar * kdvDahilBirimFiyati);
                                sd.ortalamabirimfiyati = sd.toplamtutar / sd.miktar;







                                sg.miktar = s.miktar;
                                sg.kdv = s.kdv;
                                sg.kdvtutari = (kdvDahilBirimFiyati - s.birimfiyati) * s.miktar;
                                sg.kdvdahilbirimfiyati = kdvDahilBirimFiyati;
                                sg.birimfiyati = s.birimfiyati;
                                sg.toplamtutar = kdvDahilBirimFiyati * s.miktar;
                                sg.kullanilabilirmiktar = s.miktar;
                                if (s.faturano != null)
                                {
                                    sg.faturano = s.faturano;
                                }
                                if (s.faturatarihi != null)
                                {
                                    sg.faturatarihi = s.faturatarihi;
                                }
                                if (s.irsaliyetarihi != null)
                                {
                                    sg.irsaliyetarihi = s.irsaliyetarihi;
                                }
                                if (s.irsaliyeno != null)
                                {
                                    sg.irsaliyeno = s.irsaliyeno;
                                }
                                if (s.firmaID == 0)
                                {
                                    sg.firmaID = s.firmaID;
                                }
                                if (s.projekodu == 0)
                                {
                                    sg.projekodu = s.projekodu;
                                }
                                if (gr2.Update(sd) == 1)
                                {
                                    if (gr.Update(sg) == 1)
                                    {
                                        return "STOK GİRİŞİ BAŞARIYLA GÜNCELLENDİ";
                                    }
                                    else
                                    {
                                        return "STOK GİRİŞİ GÜNCELLENEMEDİ";
                                    }
                                }
                                else
                                {
                                    return "STOK DURUMU GÜNCELLENEMEDİ";
                                }





                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                return ex.ToString();
            }
        }




    }
}
