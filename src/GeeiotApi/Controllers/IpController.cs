using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeeiotApi.Model.Ip;
using IP2Region;
using IP2Region.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WithSalt.Common.Date;
using WithSalt.Common.Extensions;

namespace GeeiotApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IpController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        private IHttpContextAccessor _accessor;

        public IpController(IHostingEnvironment hostingEnvironment, IHttpContextAccessor accessor)
        {
            _hostingEnvironment = hostingEnvironment;
            _accessor = accessor;
        }

        [Route("")]
        public IActionResult Get()
        {
            string ipv4 = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            if(ipv4 == "::1")
            {
                ipv4 = "127.0.0.1";
            }
            IpSearchResult result = new IpSearchResult();
            string dbPath = (_hostingEnvironment.ContentRootPath + @"\DB\ip2region.db").AutoPathConvert();
            using (var _search = new DbSearcher(dbPath))
            {
                DataBlock db = _search.MemorySearch(ipv4);
                result.Ip = ipv4;
                result.Local = db.Region;
                result.Time = TimeUtil.Timestamp();
            }
            return new JsonResult(result);
        }
    }
}