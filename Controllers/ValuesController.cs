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
        public JsonResult Get()
        {
            return Json(RedisManager.GetRange("stage1"));
        }

        [HttpGet("{id}")]
        public string Get(string id)
        {
            return RedisManager.GetPercentage("stage1",id).ToString();
        }

        // GET api/values/5
        [HttpGet("{stage}.{id}")]
        public string Get(string stage, string id)
        {
            return RedisManager.GetPercentage(stage,id).ToString();
        }

        // POST api/values
        [HttpPost]
        public JsonResult Post(Score scoreData)
        {
            try
            {
                RedisManager.AddScore(scoreData.stage, scoreData.id, scoreData.score);
                return Json(RedisManager.GetPercentage(scoreData.stage, scoreData.id));
            }
            catch(System.ArgumentException)
            {
                return Json(scoreData);
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
