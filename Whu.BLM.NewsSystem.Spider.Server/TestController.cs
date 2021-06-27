using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Whu.BLM.NewsSystem.Server.Data.Context;

namespace Whu.BLM.NewsSystem.Spider.Server
{
    [ApiController]
    [Route("/test")]
    public class TestController : ControllerBase
    {
        private readonly NewsSystemContext _context;

        public TestController(NewsSystemContext context)
        {
            _context = context;
        }

        [HttpGet("count")]
        public int GetNewsCount()
        {
            return _context.News.Count();
        }
    }
}