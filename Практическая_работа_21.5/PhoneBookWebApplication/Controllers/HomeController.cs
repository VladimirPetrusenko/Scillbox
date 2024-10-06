using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneBookWebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace PhoneBookWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        RoleManager<IdentityRole> roleManager;
        PhoneBookApi phoneBookApi;
        PhoneBook currentPhoneBook;

        public HomeController(ILogger<HomeController> logger, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            this.roleManager = roleManager;
            phoneBookApi = new PhoneBookApi();
        }

        private async Task AddRole()
        {
            string roleName1 = "admin";
            string roleName2 = "user";

            var roleExists1 = await roleManager.RoleExistsAsync(roleName1);

            if (!roleExists1)
            {
                var result1 = await roleManager.CreateAsync(new IdentityRole(roleName1));
                if (result1.Succeeded)
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

            var roleExists2 = await roleManager.RoleExistsAsync(roleName2);

            if (!roleExists2)
            {
                var result2 = await roleManager.CreateAsync(new IdentityRole(roleName2));
                if (result2.Succeeded)
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
        }

        public async Task<IActionResult> Index()
        {
            await AddRole();

            return View(await phoneBookApi.GetPhoneBookEntry());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await phoneBookApi.FindPhoneBookEntry(id)); //если не ставить await, то будет на view будет приходить объект Task<PhoneBook>, а не сам PhoneBook
        }

        [Authorize(Roles = "user, admin")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetDataFromViewField(string surname, string name, string patronymic, string numberPhone, string address, string description)
        {
            var PhoneBookEntry = new PhoneBook()
            {
                Surname = surname,
                Name = name,
                Patronymic = patronymic,
                NumberPhone = numberPhone,
                Address = address,
                Description = description
            };

            string result = await phoneBookApi.AddPhoneBookEntry(PhoneBookEntry);

            Console.WriteLine(result);

            return Redirect("~/");
        }

        // GET: PhoneBook/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            currentPhoneBook = await phoneBookApi.FindPhoneBookEntry(id);

            if (currentPhoneBook == null)
            {
                return NotFound();
            }
            return View(currentPhoneBook);
        }

        // PUT: PhoneBook/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                currentPhoneBook = await phoneBookApi.FindPhoneBookEntry(id);

                currentPhoneBook.Surname = collection["Surname"];
                currentPhoneBook.Name = collection["Name"];
                currentPhoneBook.Patronymic = collection["Patronymic"];
                currentPhoneBook.NumberPhone = collection["NumberPhone"];
                currentPhoneBook.Address = collection["Address"];
                currentPhoneBook.Description = collection["Description"];

                await phoneBookApi.EditPhoneBookEntry(currentPhoneBook);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PhoneBook/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            currentPhoneBook = await phoneBookApi.FindPhoneBookEntry(id);

            if (currentPhoneBook == null)
            {
                return NotFound();
            }
            return View(currentPhoneBook);
        }

        // POST: PhoneBook/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                currentPhoneBook = await phoneBookApi.FindPhoneBookEntry(id);

                await phoneBookApi.DeletePhoneBookEntry(currentPhoneBook);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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
    }
}
