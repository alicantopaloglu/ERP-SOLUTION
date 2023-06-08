using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using EntityLayer.Entities;
using System.Data.SqlClient;
using DataAccessLayer;
using System.Data;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace HedlabErpWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjelerController : ControllerBase
    {
        ProjelerManager pm = new ProjelerManager();
        SqlBaglanti bgl = new SqlBaglanti();


        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult(pm.getAll());
        }
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            return new JsonResult(pm.getProje(id));
        }
        [HttpPost]
        public JsonResult Post(PROJELER p)
        {
            return new JsonResult(pm.addProje(p));
        }
        [HttpPut]
        public JsonResult Put(PROJELER p)
        {
            return new JsonResult(pm.updateProje(p));
        }

        public SqlBaglanti GetBgl()
        {
            return bgl;
        }

        public static List<Dictionary<string, string>> GetDataTableDictionaryList(DataTable dt)
        {
            return dt.AsEnumerable().Select(
                row => dt.Columns.Cast<DataColumn>().ToDictionary(
                    column => column.ColumnName,
                    column => row[column].ToString()
                )).ToList();
        }


        [HttpGet("{action}/{id}")]
        public JsonResult getProjeRapor(int id)
        {
            return new JsonResult(pm.getProjeRapor(id));
        }
        [HttpGet("{action}/{id}")]
        public JsonResult getSelectedProje(int id)
        {
            return new JsonResult(pm.getSelectedProje(id));
        }
        [HttpGet("{action}/{id}")]
        public List<PROJERAPOR> getProjeler(int id, SqlBaglanti bgl)
        {


            //DataTable dt = new DataTable();
            //SqlDataAdapter da = new SqlDataAdapter("WITH stokcikis ( stokkodu,stokcikismiktari,cikistoplamtutar) AS (SELECT stokkodu,SUM(miktar)," +
            //    "SUM(toplamtutar) from STOKCIKISI where projekodu='" + id + "' group by stokkodu ), stokgiris ( stokkodu,stokgirismiktari,giristoplamtutar)" +
            //    "AS(select stokkodu, SUM(miktar) ,SUM(toplamtutar)from STOKGIRISI where projekodu = '" + id + "' group by stokkodu), stokkarti " +
            //    "(stokkodu,urunadi,uretici,yuzey,boy,en,kalinlik,tanim) AS (select stokkodu,urunadi,uretici,yuzey,boy,en,kalinlik,tanim from STOKKARTI" +
            //    ")select coalesce(a.stokkodu, b.stokkodu) AS Stokkodu, ISNULL(a.stokcikismiktari, 0 ) AS StokCikisMiktari,ISNULL(b.stokgirismiktari, 0 )AS StokGirisMiktari, ISNULL(a.cikistoplamtutar, 0 )AS CikisToplamTutar,ISNULL(b.giristoplamtutar, 0 ) AS GirisToplamTutar,c.urunadi," +
            //    "c.uretici,c.yuzey, c.boy,c.en,c.kalinlik,c.tanim from stokcikis AS a full join stokgiris as b  ON a.stokkodu = b.stokkodu full join stokkarti as c " +
            //    "on a.stokkodu = c.stokkodu where a.stokkodu=c.stokkodu", bgl.baglanti());
            //da.Fill(dt);

            //string JsonResult;
            //JsonResult = JsonConvert.SerializeObject(dt);







            return pm.projedeneme(id);
        }
    }
}
