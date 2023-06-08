using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;
using EntityLayer.Entities;

namespace BusinessLayer
{
    public class StokCikisiManager
    {
        GenericRepository<STOKCIKISI> grStokCikisi = new GenericRepository<STOKCIKISI>();
        GenericRepository<STOKGIRISI> grStokGirisi = new GenericRepository<STOKGIRISI>();
        GenericRepository<STOKDURUMU> grStokDurumu = new GenericRepository<STOKDURUMU>();


        public List<STOKCIKISI> getAll()
        {
            return grStokCikisi.List();
        }

        public List<STOKCIKISI> getStokCikisi(int id)
        {
            return grStokCikisi.List(x => x.stokcikisID == id);

        }

        public IQueryable GetUserOutputs(int id)
        {
            return grStokCikisi.StokCikisiListCustom(id);
        }



        public string addStokCikisi(STOKCIKISI s)
        {

            
            try
            {
                var cikilacakMiktar = s.miktar;
                STOKDURUMU stokDurumu = grStokDurumu.Find(x => x.stokkodu == s.stokkodu);
                if(cikilacakMiktar.Equals(null) || cikilacakMiktar <= 0)
                {
                    return "MİKTAR 0 YA DA 0'DAN KÜÇÜK OLAMAZ";
                }
                else
                {
                    if(s.projekodu.Equals(null) || s.projekodu<=0)
                    {
                        return "PROJE KODU BOŞ GEÇİLEMEZ";
                    }
                    else
                    {
                        if (stokDurumu != null)
                        {
                            if (s.miktar > stokDurumu.kullanilabilirmiktar || s.miktar > stokDurumu.miktar)
                            {
                                return "ÇIKILMAK İSTENEN MİKTAR STOKLARDA BULUNMUYOR";
                            }
                            else
                            {
                                var girisler = grStokGirisi.getGirisler(x => x.stokkodu == s.stokkodu, u => u.kullanilabilirmiktar != 0, y => y.tarih);

                                foreach (var x in girisler)
                                {
                                    if (cikilacakMiktar != 0)
                                    {
                                        if (x.kullanilabilirmiktar <= cikilacakMiktar && x.kullanilabilirmiktar != 0)
                                        {
                                            STOKCIKISI stokCikisi = new STOKCIKISI();
                                            stokCikisi.projekodu = s.projekodu;
                                            stokCikisi.stokkodu = s.stokkodu;
                                            stokCikisi.kullaniciID = s.kullaniciID;
                                            cikilacakMiktar -= x.kullanilabilirmiktar;

                                            stokCikisi.tarih = DateTime.Today.Date;
                                            stokCikisi.miktar = x.kullanilabilirmiktar;
                                            stokCikisi.stokgirisID = x.stokgirisID;
                                            stokCikisi.birimfiyati = x.kdvdahilbirimfiyati;
                                            stokCikisi.toplamtutar = x.kdvdahilbirimfiyati * x.kullanilabilirmiktar;
                                            stokDurumu.miktar -= x.kullanilabilirmiktar;
                                            stokDurumu.kullanilabilirmiktar -= x.kullanilabilirmiktar;

                                            if (stokDurumu.miktar == 0)
                                            {
                                                stokDurumu.ortalamabirimfiyati = 0;
                                                stokDurumu.toplamtutar = 0;
                                            }
                                            else
                                            {
                                                stokDurumu.toplamtutar -= (x.kdvdahilbirimfiyati * x.kullanilabilirmiktar);
                                                stokDurumu.ortalamabirimfiyati = stokDurumu.toplamtutar / stokDurumu.miktar;

                                            }


                                            x.kullanilabilirmiktar = 0;
                                            grStokDurumu.Update(stokDurumu);
                                            grStokGirisi.Update(x);
                                            grStokCikisi.Insert(stokCikisi);




                                        }
                                        else if (x.kullanilabilirmiktar >= cikilacakMiktar)
                                        {
                                            STOKCIKISI stokCikisi = new STOKCIKISI();
                                            stokCikisi.projekodu = s.projekodu;
                                            stokCikisi.stokkodu = s.stokkodu;
                                            stokCikisi.kullaniciID = s.kullaniciID;
                                            x.kullanilabilirmiktar -= cikilacakMiktar;
                                            stokCikisi.tarih = DateTime.Today.Date;



                                            stokCikisi.miktar = cikilacakMiktar;
                                            stokCikisi.stokgirisID = x.stokgirisID;
                                            stokCikisi.birimfiyati = x.kdvdahilbirimfiyati;
                                            stokCikisi.toplamtutar = x.kdvdahilbirimfiyati * cikilacakMiktar;

                                            stokDurumu.miktar -= cikilacakMiktar;
                                            stokDurumu.kullanilabilirmiktar -= cikilacakMiktar;

                                            if (stokDurumu.miktar == 0)
                                            {
                                                stokDurumu.ortalamabirimfiyati = 0;
                                                stokDurumu.toplamtutar = 0;
                                            }
                                            else
                                            {
                                                stokDurumu.toplamtutar -= stokCikisi.toplamtutar;
                                                stokDurumu.ortalamabirimfiyati = stokDurumu.toplamtutar / stokDurumu.miktar;
                                            }

                                            cikilacakMiktar = 0;
                                            grStokDurumu.Update(stokDurumu);
                                            grStokGirisi.Update(x);
                                            grStokCikisi.Insert(stokCikisi);


                                        }
                                    }


                                }


                                return "STOK ÇIKIŞI BAŞARILI";


                            }
                        }
                        else
                        {
                            return "BÖYLE BİR STOK BULUNAMADI";
                        }
                    }
                   
                }
                
            }
            catch (Exception ex)
            {

                return ex.ToString();
            }

            

        }
        public string removeStokCikisi(STOKCIKISI s)
        {
            STOKCIKISI stokCikis = grStokCikisi.Find(x => x.stokcikisID == s.stokcikisID);
            STOKDURUMU stokDurumu = grStokDurumu.Find(x => x.stokkodu == s.stokkodu);
            STOKGIRISI stokGiris = grStokGirisi.Find(x=> x.stokgirisID == stokCikis.stokgirisID);
            try
            {
                if(stokCikis != null && stokDurumu != null )
                {
                    if (s.miktar >= stokCikis.miktar)
                    {
                        return "MİKTAR DAHA ÖNCE ÇIKIŞ YAPILAN MİKTARDAN FAZLA YA DA AYNI OLAMAZ";
                    }
                    else
                    {
                        if(s.miktar.Equals(null) || s.miktar<=0 || s.projekodu.Equals(null) || s.projekodu <= 0)
                        {
                            return "MİKTAR VE PROJE KODU BOŞ GEÇİLEMEZ";
                        }
                        else
                        {
                            stokDurumu.miktar = (stokDurumu.miktar + stokCikis.miktar) - s.miktar;
                            stokDurumu.toplamtutar = (stokCikis.miktar* stokCikis.birimfiyati) + stokDurumu.toplamtutar;
                            stokDurumu.toplamtutar -= stokCikis.birimfiyati * s.miktar;
                            stokDurumu.ortalamabirimfiyati = (stokDurumu.toplamtutar / stokDurumu.miktar);
                            stokDurumu.kullanilabilirmiktar = stokDurumu.kullanilabilirmiktar + stokCikis.miktar - s.miktar;

                            stokGiris.kullanilabilirmiktar = stokGiris.kullanilabilirmiktar + stokCikis.miktar - s.miktar;
                            


                            stokCikis.projekodu = s.projekodu;
                            stokCikis.miktar= s.miktar;
                            stokCikis.toplamtutar = stokCikis.birimfiyati * s.miktar;

                            grStokDurumu.Update(stokDurumu);
                            grStokGirisi.Update(stokGiris);
                            grStokCikisi.Update(stokCikis);

                            return "STOK ÇIKIŞI BAŞARIYLA GÜNCELLENDİ";
                            
                            
                           
                        }
                    }
                }
                else
                {
                    return "BÖYLE BİR KAYIT BULUNAMADI";
                }
                
            }
            catch (Exception ex)
            {

                return ex.ToString();
            }
           
        }
    }
}
