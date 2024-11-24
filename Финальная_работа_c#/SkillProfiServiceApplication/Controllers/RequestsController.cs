using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SkillProfiServiceApplication.Interfaces;

namespace SkillProfiServiceApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RequestsController : Controller
    {
        private readonly IRequestServiceData requestServiceData;

        public RequestsController(IRequestServiceData requestServiceData)
        {
            this.requestServiceData = requestServiceData;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpGet]
        [Route("getall")]
        public async Task<ActionResult<string>> GetAllRequests()
        {
            Console.WriteLine("GetAllRequests");
            return Ok(JsonSerializer.Serialize<IEnumerable<Request>>(requestServiceData.GetAllRequests()));
        }

        [HttpPost]
        [Route("get")]
        public async Task<ActionResult<string>> GetRequests([FromBody] RequestFilter requestFilter)
        {
            return Ok(JsonSerializer.Serialize<IEnumerable<Request>>(requestServiceData.GetRequests(requestFilter)));
        }

        [HttpPost]
        //[ValidateAntiForgeryToken] - с активным атрибутом запрос не обрабатывается
        [Route("add")]
        public async Task<ActionResult> AddRequest([FromBody] Request request)
        {
            requestServiceData.AddRequest(request);
            return Ok();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Route("changerequeststatus")]
        public async Task<ActionResult> ChangeRequestStatus([FromBody] Request request)
        {
            requestServiceData.ChangeRequestStatus(request);
            return Ok();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Route("find")]
        public async Task<string> FindPhoneBookEntry([FromBody] int id)
        {
            return JsonSerializer.Serialize<Request>(requestServiceData.FindRequest(id));
        }
    }
}