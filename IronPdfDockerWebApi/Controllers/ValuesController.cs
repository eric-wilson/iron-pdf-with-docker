using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IronPdfDockerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {

            IronPdfDockerLib.Generator generator = new IronPdfDockerLib.Generator();

            var result = generator.BuildReport();


            Console.WriteLine(result);


            

            return new string[] { "Attepting to generate a pdf:",  result };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {

            IronPdfDockerLib.Generator generator = new IronPdfDockerLib.Generator();

            var result = generator.BuildReport();


            Console.WriteLine(result);


            return result;
        }

       
    }
}
