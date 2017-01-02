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

        // GET api/values/yulcat
        [HttpGet("{id}")]
        public string Get(string id)
        {
            return RedisManager.GetPercentage("stage1",id).ToString();
        }

        // GET api/values/stage1.yulcat
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
                var result = new JsonResult(RedisManager.GetPercentage(scoreData.stage, scoreData.id));
                result.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
                return result;
            }
            catch(System.ArgumentException)
            {
                var error = new JsonResult("ScoreData Argument not valid");
                error.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest;
                HttpContext.Response.StatusCode = error.StatusCode.Value;
                return new JsonResult(error);
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
