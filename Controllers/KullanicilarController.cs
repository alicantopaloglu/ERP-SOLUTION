using BusinessLayer;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HedlabErpWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullanicilarController : ControllerBase
    {
        KullanicilarManager km = new KullanicilarManager();
        KULLANICILAR user = new KULLANICILAR();

        [HttpGet]
        public JsonResult Get()
        {
            var values = km.getAll();
            return new JsonResult(values);
        }


        [HttpGet("{action}/{details}")]
        public string GetKullanicilar(string kullaniciadi, string sifre)
        {
          
            KULLANICILAR values = km.getKullanici(kullaniciadi, sifre);

            try
            {
                if(values != null)
                {
                    var claims = new[]
           {
                 new Claim(ClaimTypes.Name,kullaniciadi),
                 new Claim(type:"kullaniciID",value:values.kullaniciID.ToString()),
                 new Claim(type:"stokcikisi",value:values.stokcikisi.ToString()),
                 new Claim(type:"stokgirisi",value:values.stokgirisi.ToString()),
                 new Claim(type:"stokdurumu",value:values.stokdurumu.ToString()),
                 new Claim(type:"stokgirisduzenleme",value:values.stokgirisduzenleme.ToString()),
                 new Claim(type:"stokgirisisilme",value:values.stokgirisisilme.ToString()),
                 new Claim(type:"stokcikisduzenleme",value:values.stokcikisduzenleme.ToString()),
                 new Claim(type:"satinalma",value:values.satinalma.ToString()),
                 new Claim(type:"siparisiptali",value:values.siparisiptali.ToString()),
                 new Claim(type:"kullanicipaneli",value:values.kullanicipaneli.ToString()),
                 new Claim(type:"projeler",value:values.projeler.ToString()),
                 new Claim(type:"firmagirisi",value:values.firmagirisi.ToString()),
                 new Claim(type:"siparisgirisi",value:values.siparisgirisi.ToString()),
                 new Claim(type:"siparisduzenleme",value:values.siparisduzenleme.ToString()),

             };


                    var bytes = Encoding.UTF8.GetBytes("erpbackendasdasdsasadsadadad");
                    SymmetricSecurityKey key = new SymmetricSecurityKey(bytes);
                    SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    JwtSecurityToken token = new JwtSecurityToken(issuer: "deneme", audience: "deneme",
                        notBefore: DateTime.Now, expires: DateTime.Now.AddMinutes(1), claims: claims, signingCredentials: credentials);




                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                    return handler.WriteToken(token);
                }
                else
                {
                    return "KULLANICI BULUNAMADI";
                }
            }
            catch (Exception ex)
            {

                return ex.ToString();
            }


            //try
            //{
            //    if (values.Count > 0)
            //    {
            //        return new JsonResult(values);
            //    }
            //    else
            //    {
            //        return new JsonResult(null);
            //    }
            //}
            //catch (Exception ex)
            //{

            //    return new JsonResult(ex);
            //}
        }
        [HttpPost]
        public JsonResult Post(KULLANICILAR k)
        {

            return new JsonResult(km.addKullanici(k));
        }
        [HttpPut]
        public JsonResult Put(KULLANICILAR k)
        {

            return new JsonResult(km.updateKullanici(k));

        }

    }
}
