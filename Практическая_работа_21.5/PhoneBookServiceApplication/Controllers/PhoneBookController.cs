using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneBookServiceApplication.Interfaces;
using System.Text.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PhoneBookServiceApplication
{
    [ApiController]
    [Route("[controller]")]
    public class PhoneBookController : Controller //наследование от ControllerBase почему-то вызывает ошибки в подключении  
    {
        private readonly IPhoneBookData phoneBookData;

        public PhoneBookController(IPhoneBookData PhoneBookData)
        {
            this.phoneBookData = PhoneBookData;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpGet]
        [Route("get")]
        public async Task<ActionResult<string>> GetPhoneBookEntry()
        {
            return Ok(JsonSerializer.Serialize<IEnumerable<PhoneBook>>(phoneBookData.GetPhoneBookEntry()));
        }

        [HttpPost]
        //[ValidateAntiForgeryToken] - с активным атрибутом запрос не обрабатывается
        [Route("add")]
        public async Task<ActionResult> AddPhoneBookEntry([FromBody] PhoneBook phoneBook)
        {
            phoneBookData.AddPhoneBookEntry(phoneBook);
            return Ok();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Route("delete")]
        public async Task<ActionResult> DeletePhoneBookEntry([FromBody] PhoneBook phoneBook)
        {
            phoneBookData.DeletePhoneBookEntry(phoneBook);
            return Ok();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Route("edit")]
        public async Task<ActionResult> EditPhoneBookEntry([FromBody] PhoneBook phoneBook)
        {
            phoneBookData.EditPhoneBookEntry(phoneBook);
            return Ok();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Route("find")]
        public async Task<string> FindPhoneBookEntry([FromBody] int id)
        {
            return JsonSerializer.Serialize<PhoneBook>(phoneBookData.FindPhoneBookEntry(id));
        }
    }
}
