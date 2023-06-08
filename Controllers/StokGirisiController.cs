using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using EntityLayer.Entities;


namespace HedlabErpWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StokGirisiController : ControllerBase
    {
        StokGirisiManager sm = new StokGirisiManager();
       
        [HttpGet]
        public JsonResult Get()
        {
            var values = sm.getAll();
            return new JsonResult(values);
        }

        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            var values = sm.getStokGirisi(id);
            return new JsonResult(values);
        }
        [HttpPost]
        public JsonResult Post(STOKGIRISI sg)
        {
            return new JsonResult(sm.addStokGirisi(sg));
        }
        [HttpPut]
        public JsonResult Put(STOKGIRISI s)
        {
            return new JsonResult(sm.updateStokGirisi(s));
        }



    }
}
