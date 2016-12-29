using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webAPI.Containers;
using webAPI.Redis;

namespace webAPI.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public string Get()
        {
            return RedisManager.GetCount("stage1").ToString();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            return RedisManager.GetPercentage("stage1",id).ToString();
        }

        // POST api/values
        [HttpPost]
        public string Post(Score scoreData)
        {
            try
            {
                RedisManager.AddScore(scoreData.stage, scoreData.id, scoreData.score);
                return RedisManager.GetPercentage(scoreData.stage, scoreData.id).ToString();
            }
            catch(System.ArgumentException)
            {
                return Json(scoreData).ToString();
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
