using BusinessLayer;
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
    public class StokDurumuController : ControllerBase
    {

        StokDurumuManager sm = new StokDurumuManager();
        
        [HttpGet]
        public JsonResult Get()
        {
            var values = sm.getAll();
            return new JsonResult(values);
        }

        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            var values = sm.getStokDurumu(id);
            return new JsonResult(values);
        }


    }
}
