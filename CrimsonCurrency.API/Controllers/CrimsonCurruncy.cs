using CrimsonCurrency.Data.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrimsonCurrency.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CrimsonCurruncy : ControllerBase
    {
        // can be as singleton
        public Blockchain bc { get; set; }
        public CrimsonCurruncy()
        {
            bc = new Blockchain();
        }

        [HttpGet]
        public ActionResult Get() => Ok(bc);


        [HttpPost]
        [Route("mine")]
        public ActionResult MineBlock([FromBody] Dataholder data)
        {
            if (data == null) return BadRequest("Invalid Block");
            var blck = bc.AddBlock(data);
            return Ok(bc);

        }
    }
}
