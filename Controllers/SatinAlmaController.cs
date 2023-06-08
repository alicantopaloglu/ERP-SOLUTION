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
    public class SatinAlmaController : ControllerBase
    {
        SatinAlmaManager sm = new SatinAlmaManager();

        [HttpPut]
        public JsonResult Put(SIPARIS s)
        {
            //var s = sm.getSiparis(id);
            //sm.updateSiparis(s);
            return new JsonResult(sm.updateSiparis(s));
        }


    }
}
