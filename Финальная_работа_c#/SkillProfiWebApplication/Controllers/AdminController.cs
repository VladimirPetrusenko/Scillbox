using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkillProfiWebApplication.Data;
using SkillProfiWebApplication.DataContext;
using SkillProfiWebApplication.IdentitySkillProfi;
using SkillProfiWebApplication.ViewModels;

namespace SkillProfiWebApplication.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly SkillProfiContext _context;
        private readonly RequestServiceApi requestServiceApi;
        
        private IndexPageModel indexPage;
        private ContactsPageModel contactsPage;
        private RequestsPageModel requestsPage;

        private RequestFilter requestFilter;
        private DateTime zeroDate = new DateTime(0001, 1, 1, 0, 0, 0);

        private List<SocialNetwork> SocialNetworks = new List<SocialNetwork>();

        public AdminController(UserManager<User> userManager, SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager, SkillProfiContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this._context = context;
            requestServiceApi = new RequestServiceApi();
            requestFilter = new RequestFilter();

            InitPagesModel();
        }

        public async Task<IActionResult> Login()
        {
            //await CheckAdmin();
            return View(new UserLogin());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLogin model)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await signInManager.PasswordSignInAsync
                    (model.LoginProp, model.Password, false, lockoutOnFailure: false);

                if (loginResult.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь не найден");
                    return View(model);
                }
            }

            ModelState.AddModelError("", "Пользователь не найден");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View(indexPage);
        }

        public ActionResult EditIndex()
        {
            return View(indexPage);
        }

        [HttpPost]
        public ActionResult EditIndex(IndexPageModel indexPage)
        {
            var mainPageCaption = _context.AppElements.FirstOrDefault(e => e.Id == 1);
            mainPageCaption.ElementValue = indexPage.MainPageCaption;

            var requestLabel = _context.AppElements.FirstOrDefault(e => e.Id == 2);
            requestLabel.ElementValue = indexPage.RequestLabel;

            var requestFormsLabel = _context.AppElements.FirstOrDefault(e => e.Id == 4);
            requestFormsLabel.ElementValue = indexPage.RequestFormsLabel;

            _context.AppElements.Update(mainPageCaption);
            _context.AppElements.Update(requestLabel);
            _context.AppElements.Update(requestFormsLabel);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Projects()
        {
            return View(_context.Projects.ToList());
        }

        // GET: Admin/AddProject
        public ActionResult AddProject()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProject(Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/AddProject
        public ActionResult EditProject(int id)
        {
            return View(_context.Projects.FirstOrDefault(e => e.Id == id));
        }

        [HttpPost]
        public ActionResult EditProject(IFormCollection collection)
        {
            var project = _context.Projects.FirstOrDefault(e => e.Id == Convert.ToInt32(collection["Id"]));
            project.ProjectTitle = collection["ProjectTitle"];
            project.ProjectDetails = collection["ProjectDetails"];
            if (collection["ProjectImage"] != "")
            {
                project.ProjectImage = collection["ProjectImage"];
            }
            _context.Projects.Update(project);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/AddProject
        public ActionResult DeleteProject(int id)
        {
            return View(_context.Projects.FirstOrDefault(e => e.Id == id));
        }

        [HttpPost]
        public ActionResult DeleteProject(Project project)
        {

            _context.Projects.Remove(project);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Services()
        {
            return View(_context.Services.ToList());
        }

        // GET: Admin/AddProject
        public ActionResult AddService()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddService(Service service)
        {
            _context.Services.Add(service);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult EditService(int id)
        {
            return View(_context.Services.FirstOrDefault(e => e.Id == id));
        }

        [HttpPost]
        public ActionResult EditService(Service service)
        {
            _context.Services.Update(service);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult DeleteService(int id)
        {
            return View(_context.Services.FirstOrDefault(e => e.Id == id));
        }

        [HttpPost]
        public ActionResult DeleteService(Service service)
        {
            _context.Services.Remove(service);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Posts()
        {
            return View(_context.Posts.ToList());
        }

        // GET: Admin/AddProject
        public ActionResult AddPost()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPost(Post post)
        {
            post.DateOfPublication = DateTime.Now.Date.ToShortDateString();
            _context.Posts.Add(post);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult EditPost(int id)
        {
            return View(_context.Posts.FirstOrDefault(e => e.Id == id));
        }

        [HttpPost]
        public ActionResult EditPost(IFormCollection collection)
        {
            var post = _context.Posts.FirstOrDefault(e => e.Id == Convert.ToInt32(collection["Id"]));
            post.PostTitle = collection["PostTitle"];
            post.PostFragment = collection["PostFragment"];
            post.PostFullContent = collection["PostFullContent"];
            if (collection["PostImage"] != "")
            {
                post.PostImage = collection["PostImage"];
            }
            _context.Posts.Update(post);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult DeletePost(int id)
        {
            return View(_context.Posts.FirstOrDefault(e => e.Id == id));
        }

        [HttpPost]
        public ActionResult DeletePost(Post post)
        {
            _context.Posts.Remove(post);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Contacts()
        {
            return View(contactsPage);
        }

        public ActionResult EditContacts()
        {
            return View(contactsPage);
        }

        [HttpPost]
        public ActionResult EditContacts(ContactsPageModel contactsPage)
        {
            var address = _context.AppElements.FirstOrDefault(e => e.Id == 5);
            address.ElementValue = contactsPage.Address;

            var phoneNumber = _context.AppElements.FirstOrDefault(e => e.Id == 6);
            phoneNumber.ElementValue = contactsPage.PhoneNumber;

            var fax = _context.AppElements.FirstOrDefault(e => e.Id == 7);
            fax.ElementValue = contactsPage.Fax;

            var email = _context.AppElements.FirstOrDefault(e => e.Id == 8);
            email.ElementValue = contactsPage.Email;

            _context.AppElements.Update(address);
            _context.AppElements.Update(phoneNumber);
            _context.AppElements.Update(fax);
            _context.AppElements.Update(email);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult EditSocialNetworks()
        {
            return View(SocialNetworks);
        }

        [HttpPost]
        public IActionResult EditSocialNetworks(IFormCollection collection)
        {
            List<SocialNetwork> socialNetworks = new List<SocialNetwork>();

            for (int i = 0; i < collection.Count(); i++)
            {
                if (collection[$"Name - {i}"].ToString() != "")
                {
                    socialNetworks.Add(new SocialNetwork(collection[$"Name - {i}"].ToString(), collection[$"Link - {i}"].ToString()));
                }
            }

            foreach (var socialNetwork in socialNetworks)
            {
                if (socialNetwork.Link == "")
                {
                    socialNetwork.Link = "#";
                }
            }

            string socialNetworksString = "";

            for (int i = 0; i < socialNetworks.Count(); i++)
            {
                if (i < socialNetworks.Count() - 1)
                {
                    socialNetworksString = socialNetworksString + $" {socialNetworks[i].Name} | {socialNetworks[i].Link} |";
                }
                else
                {
                    socialNetworksString = socialNetworksString + $" {socialNetworks[i].Name} | {socialNetworks[i].Link}";
                }
            }

            var socialNetwork1 = _context.AppElements.FirstOrDefault(e => e.Id == 10);
            socialNetwork1.ElementValue = socialNetworksString;

            _context.AppElements.Update(socialNetwork1);
            _context.SaveChanges();

            return RedirectToAction(nameof(Contacts));
        }

        public async Task<ActionResult> Requests()
        {
            requestsPage.StartDate = DateTime.Now;
            requestsPage.EndDate = DateTime.Now;
            requestsPage.Requests = await requestServiceApi.GetAllRequests();
            return View(requestsPage);
        }

        [HttpPost]
        public async Task<ActionResult> SortRequests(RequestsPageModel requestsPage)
        {
            if (!string.IsNullOrEmpty(requestsPage.DateRange))
            {
                switch (requestsPage.DateRange)
                {
                    case "today":
                        requestFilter.StartDate = DateTime.Today;
                        requestFilter.EndDate = DateTime.Today.AddDays(1).AddTicks(-1); // до конца дня
                        break;
                    case "yesterday":
                        requestFilter.StartDate = DateTime.Today.AddDays(-1);
                        requestFilter.EndDate = DateTime.Today.AddTicks(-1);
                        break;
                    case "week":
                        requestFilter.StartDate = DateTime.Today.AddDays(-7);
                        requestFilter.EndDate = DateTime.Today;
                        break;
                    case "month":
                        requestFilter.StartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1); // Начало месяца
                        requestFilter.EndDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month,
                             DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)).AddTicks(-1); // Конец месяца
                        break;
                }
                this.requestsPage.Requests = await requestServiceApi.GetRequests(requestFilter);
                return View(this.requestsPage);
            }

            if (requestsPage.RequestStatus == "Все" && requestsPage.EndDate == zeroDate && requestsPage.StartDate == zeroDate)
            {
                this.requestsPage.Requests = await requestServiceApi.GetAllRequests();
                return View(this.requestsPage);
            }

            if (requestsPage.RequestStatus == "Все")
            {
                requestFilter.StartDate = requestsPage.StartDate;
                requestFilter.EndDate = requestsPage.EndDate;
                this.requestsPage.Requests = await requestServiceApi.GetRequests(requestFilter);
                return View(this.requestsPage);
            }

            if (requestsPage.RequestStatus != "Все")
            {
                requestFilter.StartDate = requestsPage.StartDate;
                requestFilter.EndDate = requestsPage.EndDate;
                requestFilter.RequestStatus = requestsPage.RequestStatus;
                this.requestsPage.Requests = await requestServiceApi.GetRequests(requestFilter);
                return View(this.requestsPage);
            }

            if (requestsPage.RequestStatus != "Все" && requestsPage.EndDate == zeroDate && requestsPage.StartDate == zeroDate)
            {
                if (requestsPage.RequestStatus != "Все")
                {
                    requestFilter.RequestStatus = requestsPage.RequestStatus;
                }
                this.requestsPage.Requests = await requestServiceApi.GetRequests(requestFilter);
                return View(this.requestsPage);
            }

            return View(this.requestsPage);
        }

        public async Task<ActionResult> RequestDetails(int id)
        {
            return View(await requestServiceApi.FindRequest(id));
        }

        [HttpPost]
        public async Task<ActionResult> ChangeRequestStatus(Request request)
        {
            await requestServiceApi.ChangeRequestStatus(request);
            return RedirectToAction(nameof(Requests));
        }

        public void InitPagesModel()
        {
            indexPage = new IndexPageModel();
            contactsPage = new ContactsPageModel();
            requestsPage = new RequestsPageModel();
            InitIndexPageModel(indexPage);
            InitContactsPageModel(contactsPage);
            InitRequestPageModel(requestsPage);
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

        public void InitRequestPageModel(RequestsPageModel requestsPage)
        {
            requestsPage.Statuses = new List<string> { "Received", "InProgress", "Completed", "Declined", "Canceled" };
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

        private async Task CheckAdmin()
        {
            var roleExists = await roleManager.RoleExistsAsync("admin");

            if (!roleExists)
            {
                var result = await roleManager.CreateAsync(new IdentityRole("admin"));
                if (result.Succeeded)
                {
                    Console.WriteLine("Role added.");
                }
                else
                {
                    Console.WriteLine("Role not added.");
                }
            }
            else
            {
                Console.WriteLine("Role added before.");
            }

            var loginResult = await signInManager.PasswordSignInAsync
                    ("admin", "admin", false, lockoutOnFailure: false);

            if (!loginResult.Succeeded)
            {
                var user = new User { UserName = "admin" };
                var createResult = await userManager.CreateAsync(user, "admin");

                if (createResult.Succeeded)
                {
                    var addRoleResult = await userManager.AddToRoleAsync(user, "admin");

                    if (addRoleResult.Succeeded)
                    {
                        Console.WriteLine("Роль успешно назначена пользователю.");
                    }
                    else
                    {
                        Console.WriteLine(addRoleResult.Errors);
                    }
                }
                else
                {
                    foreach (var identityError in createResult.Errors)
                    {
                        ModelState.AddModelError("", identityError.Description);
                    }
                }
            }
        }
    }
}