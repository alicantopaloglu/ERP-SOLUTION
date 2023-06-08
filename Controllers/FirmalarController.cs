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
    public class FirmalarController : ControllerBase
    {
        FirmalarManager fm = new FirmalarManager();

        [HttpGet]
        public JsonResult Get()
        {
            var values = fm.getAll();
            return new JsonResult(values);
        }
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            var values = fm.getFirma(id);
            return new JsonResult(values);
        }
        [HttpPost]
        public JsonResult Post(FIRMALAR f)
        {
            return new JsonResult(fm.addFirma(f));
        }
        [HttpPut]
        public JsonResult Put(FIRMALAR f)
        {
            return new JsonResult(fm.updateFirma(f));
        }
    }
}
