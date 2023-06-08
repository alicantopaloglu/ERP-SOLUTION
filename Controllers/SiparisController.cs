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
    public class SiparisController : ControllerBase
    {

        SiparisManager sm = new SiparisManager();
        [HttpGet]
        public JsonResult Get()
        {
            var values = sm.getAll();
            return new JsonResult(values);
        }
        [HttpGet("{action}")]
        public JsonResult GetStockOrder()
        {
            var values = sm.getStockOrder();
            return new JsonResult(values);
        }
       
        [HttpGet("{action}/{details}")]
        public JsonResult GetUserOrder(int kullaniciID)
        {
            var values = sm.getUserOrder(kullaniciID);
            return new JsonResult(values);  
        }
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            var values = sm.getSiparis(id);
            return new JsonResult(values);
        }
        [HttpPost]
        public JsonResult Post(SIPARIS s)
        {
            
            return new JsonResult(sm.addSiparis(s));
        }
        [HttpPut]
        public JsonResult Put(SIPARIS s)
        {
            //var s = sm.getSiparis(id);
            
            return new JsonResult(sm.updateSiparis(s));
        }
    }
}
