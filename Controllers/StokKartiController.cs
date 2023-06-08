
using BusinessLayer;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;


namespace HedlabErpWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StokKartiController : ControllerBase
    {
        StokKartiManager sm = new StokKartiManager();
     

        [HttpGet]
        public JsonResult Get()
        {
            var values = sm.getAll();
         
            return new JsonResult(values);
        }

        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            var values = sm.getStok(id);
            return new JsonResult(values);
        }
        [HttpGet("{action}")]
        public JsonResult GetBirim()
        {
            return new JsonResult(sm.getBirim());
        }

        [HttpPost]
        public JsonResult Post(STOKKARTI sk)
        {

            
            return new JsonResult(sm.addStokKarti(sk));
        }

        [HttpPut]
        public JsonResult Put(STOKKARTI sk)
        {
            
            return new JsonResult(sm.updateStokKarti(sk));
        }
       
    }
}
