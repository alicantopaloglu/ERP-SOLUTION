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
    public class StokCikisiController : ControllerBase
    {
        StokCikisiManager sm = new StokCikisiManager();
        [HttpPost]
        public JsonResult Post(STOKCIKISI s)
        {
            return new JsonResult(sm.addStokCikisi(s));
        }
        [HttpGet]
        public JsonResult Get() 
        {
            return new JsonResult(sm.getAll());
        }
        [HttpGet("{action}/{id}")]
        public JsonResult GetUserOutputs(int id) 
        {
            return new JsonResult(sm.GetUserOutputs(id));
        }
        [HttpPut]
        public JsonResult Put(STOKCIKISI s) 
        {
            return new JsonResult(sm.removeStokCikisi(s));
        }
    }
}
