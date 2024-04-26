using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PhoneBookWebApplication.Controllers
{
    public class PhoneBookController : Controller
    {
        [NonAction]
        public List<PhoneBook> GetPhoneBookCollection()
        {
            List<PhoneBook> phoneBooks = new List<PhoneBook>();
            for (int i = 0; i < 10; i++)
            {
                phoneBooks.Add(new PhoneBook(i, $"Surname - {i}",
                    $"Name - {i}", $"Patronymic - {i}", $"NumberPhone - {i}",
                    $"Address - {i}", $"Description - {i}"));
            }

            return phoneBooks;
        }
        
        // GET: PhoneBook
        public ActionResult Index()
        {
            return View(GetPhoneBookCollection());
        }

        // GET: PhoneBook/Details/5
        public ActionResult Details(int id)
        {
            return View(GetPhoneBookCollection()[id]);
        }

        // GET: PhoneBook/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhoneBook/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PhoneBook/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PhoneBook/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PhoneBook/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PhoneBook/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}