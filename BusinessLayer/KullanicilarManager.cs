using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using DataAccessLayer;
using EntityLayer.Entities;

namespace BusinessLayer
{
    public class KullanicilarManager
    {
        GenericRepository<KULLANICILAR> gr = new GenericRepository<KULLANICILAR>();

        public static string MD5Olustur(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }



        public List<KULLANICILAR> getAll()
        {
            return gr.List();
           
        }

        public string addKullanici(KULLANICILAR k)
        {
            try
            {
                var kullanici = gr.Find(x => x.kullaniciadi == k.kullaniciadi);
                if (kullanici == null)
                {
                    var sifre = MD5Olustur(k.sifre);
                    k.sifre = sifre;
                    gr.Insert(k);
                    return "KULLANICI BAŞARIYLA OLUŞTURULDU";
                }
                else
                {
                    return "BÖYLE BİR KULLANICI ZATEN VAR";
                }
            }
            catch (Exception ex)
            {

                return ex.ToString();
            }
           
            
          
           
        }

        public string updateKullanici(KULLANICILAR k)
        {
            try
            {
                KULLANICILAR value = gr.Find(x => x.kullaniciID == k.kullaniciID);
                if(value != null)
                {
                    var sifre = MD5Olustur(k.sifre);
                    value.kullaniciadi = k.kullaniciadi;
                    value.sifre = sifre;
                    value.kullanicipaneli = k.kullanicipaneli;
                    value.projeler = k.projeler;
                    value.raporlar = k.raporlar;
                    value.rezerveacma = k.rezerveacma;
                    value.satinalma = k.satinalma;
                    value.siparisiptali = k.siparisiptali;
                    value.stokcikisduzenleme = k.stokcikisduzenleme;
                    value.stokcikisi = k.stokcikisi;
                    value.stokdurumu = k.stokdurumu;
                    value.stokgirisduzenleme = k.stokgirisduzenleme;
                    value.stokgirisi = k.stokgirisi;
                    value.stokgirisisilme = k.stokgirisisilme;
                    gr.Update(value);
                    return "KULLANICI BAŞARIYLA GÜNCELLENDİ";
                }
                else
                {
                    return "BÖYLE BİR KULLANICI BULUNAMADI";
                }
            }
            catch (Exception ex)
            {

                return ex.ToString();
            }
           
           
            

            

        }

        public KULLANICILAR getKullanici(string kullaniciadi,string sifre)
        {
            var md5Sifre = MD5Olustur(sifre);
            return gr.getKullanicilar(x=>x.kullaniciadi==kullaniciadi, y=>y.sifre==md5Sifre);
        }

    }
}
