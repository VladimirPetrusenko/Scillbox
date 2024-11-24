using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkillProfiWebApplication.DataContext;
using SkillProfiWebApplication.Models;
using SkillProfiWebApplication.ViewModels;

namespace SkillProfiWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SkillProfiContext _context;
        private IndexPageModel indexPage;
        private ContactsPageModel contactsPage;
        RequestServiceApi requestServiceApi;
        private List<SocialNetwork> SocialNetworks = new List<SocialNetwork>();

        public HomeController(ILogger<HomeController> logger, SkillProfiContext context)
        {
            _logger = logger;
            _context = context;
            InitPagesModel();
            
        }

        public IActionResult Index()
        {
            return View(indexPage);
        }

        public IActionResult Projects()
        {
            return View(_context.Projects.ToList());
        }

        public IActionResult ProjectDetails(int id)
        {
            return View(_context.Projects.FirstOrDefault(e => e.Id == id));
        }

        public IActionResult Services()
        {
            return View(_context.Services.ToList());
        }

        public IActionResult Posts()
        {
            return View(_context.Posts.ToList());
        }

        public IActionResult PostDetails(int id)
        {
            return View(_context.Posts.FirstOrDefault(e => e.Id == id));
        }

        public IActionResult Contacts()
        {
            return View(contactsPage);
        }

        [HttpPost]
        public async Task<IActionResult> AddRequest(IndexPageModel indexPageModel)
        {
            var request = new Request()
            {
                GuestName = indexPageModel.RequestFormName,
                Email = indexPageModel.RequestFormEmail,
                RequestText = indexPageModel.RequestFormMessage,
                Status = "Received",
                CreatedAt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 
                DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
            };

            await requestServiceApi.AddRequest(request);
            return Redirect("~/");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void InitPagesModel()
        {
            indexPage = new IndexPageModel();
            contactsPage = new ContactsPageModel();
            InitIndexPageModel(indexPage);
            InitContactsPageModel(contactsPage);

        }

        public void InitIndexPageModel(IndexPageModel indexPage)
        {
            indexPage.MainPageCaption = _context.AppElements.FirstOrDefault(e => e.Id == 1).ElementValue;
            indexPage.RequestLabel = _context.AppElements.FirstOrDefault(e => e.Id == 2).ElementValue;
            indexPage.RequestFormsLabel = _context.AppElements.FirstOrDefault(e => e.Id == 4).ElementValue;
        }

        public void InitContactsPageModel(ContactsPageModel contactsPage)
        {
            contactsPage.Address = _context.AppElements.FirstOrDefault(e => e.Id == 5).ElementValue;
            contactsPage.PhoneNumber = _context.AppElements.FirstOrDefault(e => e.Id == 6).ElementValue;
            contactsPage.Fax = _context.AppElements.FirstOrDefault(e => e.Id == 7).ElementValue;
            contactsPage.Email = _context.AppElements.FirstOrDefault(e => e.Id == 8).ElementValue;
            contactsPage.ContactsImage = _context.AppElements.FirstOrDefault(e => e.Id == 9).ElementValue;
            GetSocialNetworks(_context.AppElements.FirstOrDefault(e => e.Id == 10).ElementValue);
            contactsPage.SocialNetworks = SocialNetworks;
        }

        public void GetSocialNetworks(string SocialNetworksString)
        {
            SocialNetworks.Clear();

            string[] SocialNetworkItems = SocialNetworksString.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < SocialNetworkItems.Length; i++)
            {
                SocialNetworks.Add(new SocialNetwork(SocialNetworkItems[i].Trim().Replace("_", " "), SocialNetworkItems[i + 1].Trim().Replace("_", " ")));
                i++;
            }
        }

    }
}
