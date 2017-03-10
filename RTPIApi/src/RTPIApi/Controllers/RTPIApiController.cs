//RTPIApiController.cs

using Microsoft.AspNetCore.Mvc;
using RTPIAPI.Services;
using System;
using System.Threading.Tasks;

namespace RTPIAPI.Controllers
{
    public class RTPIApiController : Controller
    {
        private IRTPIServiceFactory _RTPIServiceFactory;

        public RTPIApiController(IRTPIServiceFactory serviceFactory)
        {
            _RTPIServiceFactory = serviceFactory;
        }

        [HttpGet("RTPIApi/RTPIHead/{service}/{stop}")]
        public async Task<IActionResult> Get(string service, string stop)
        {

            var rtpiService = _RTPIServiceFactory.GetRTPIService(service);

            if (rtpiService != null)
            {
                try
                {
                    var rtpi = await rtpiService.GetRTPIForStop(stop);
                    return Ok(rtpi);
                }
                catch (Exception)
                {
                    // TODO: fix this!
                    return BadRequest("Could Not get Real Time Info");
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
