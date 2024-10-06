using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneBookWebApplication.AuthIdentityPhoneBookWebApplication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace PhoneBookWebApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private string roleName;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        [Authorize(Roles = "admin")] // Ограничиваем доступ для администраторов
        public async Task<IActionResult> GetUsers()
        {
            var users = userManager.Users.ToList<User>();
            return View(users);
        }

        [HttpGet]
        public IActionResult Login(/*string returnUrl = null*/)
        {
            return View(new UserLogin());
            /*{
                ReturnUrl = returnUrl
            });*/
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
                    /*if (Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }*/
                    //if()
                    Console.WriteLine("gggggg");
                    return RedirectToAction("Index", "Home");
                    //return RedirectToAction("Add", "Home");
                }
            }

            ModelState.AddModelError("", "Пользователь не найден");
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new UserRegistration());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegistration model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.LoginProp };
                var createResult = await userManager.CreateAsync(user, model.Password);

                if (user.UserName.Contains("Admin") || user.UserName.Contains("admin"))
                {
                    roleName = "admin";
                }
                else
                {
                    roleName = "user";
                }

                if (createResult.Succeeded)
                {
                    //await signInManager.SignInAsync(user, false); // если не закомментировать,
                    //то сразу заходит в эту учетную запись
                    var addRoleResult = await userManager.AddToRoleAsync(user, roleName);

                    if (addRoleResult.Succeeded)
                    {
                        //return Ok("Роль успешно назначена пользователю.");
                        Console.WriteLine("Роль успешно назначена пользователю.");
                    }
                    else
                    {
                        //return BadRequest(addRoleResult.Errors);
                        Console.WriteLine(addRoleResult.Errors);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var identityError in createResult.Errors)
                    {
                        ModelState.AddModelError("", identityError.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            return View(user);
            /*{
                ReturnUrl = returnUrl
            });*/
        }

        [HttpPost]
        [Authorize(Roles = "admin")] // Ограничение доступа
        public async Task<ActionResult> DeleteUser(IFormCollection collection)
        {
            /*Console.WriteLine(id);*/
            var user = await userManager.FindByIdAsync(collection["Id"]);

            if (user == null)
            {
                return NotFound("User not found.");
            }
            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("GetUsers", "Account"); // Перенаправление на список пользователей или другую страницу
            }
            return View("Error"); // Или отобразите сообщение об ошибке
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}