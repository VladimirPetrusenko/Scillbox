using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBookWebApplication.Data;

namespace PhoneBookWebApplication.Controllers
{
    public class PhoneBookController : Controller
    {
        public static DbContextOptions<PhoneBookWebApplicationContext> options = new DbContextOptions<PhoneBookWebApplicationContext>();

        PhoneBookWebApplicationContext db = new PhoneBookWebApplicationContext(options);

        [NonAction]
        public List<PhoneBook> GetPhoneBookCollection()
        {
            List<PhoneBook> phoneBooks = db.PhoneBook.ToList();

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
            foreach(var phoneBook in GetPhoneBookCollection())
            {
                if (phoneBook.ID == id)
                {
                    return View(GetPhoneBookCollection()[GetPhoneBookCollection().IndexOf(phoneBook)]);
                }
            }
            return null;
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

                PhoneBook phoneBook = new PhoneBook(collection["Surname"], collection["Name"],
                    collection["Patronymic"], collection["NumberPhone"], collection["Address"], collection["Description"]);

                Console.WriteLine(collection["Surname"]);

                db.PhoneBook.Add(phoneBook);

                db.SaveChanges();

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
            if (id == null)
            {
                return NotFound();
            }
            
            var phoneBook = db.PhoneBook.Find(id);
            
            if (phoneBook == null)
            {
                return NotFound();
            }
            return View(phoneBook);
        }

        // PUT: PhoneBook/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            Console.WriteLine("iii");
            Console.WriteLine(id.ToString());
            
            try
            {
                Console.WriteLine("iii");
                // TODO: Add update logic here

                PhoneBook phoneBook = new PhoneBook(collection["Surname"], collection["Name"],
                    collection["Patronymic"], collection["NumberPhone"], collection["Address"], collection["Description"]);

                PhoneBook phoneBook1 = db.PhoneBook.Find(id);    

                phoneBook1.Surname = phoneBook.Surname;
                phoneBook1.Name = phoneBook.Name;
                phoneBook1.Patronymic = phoneBook.Patronymic;
                phoneBook1.NumberPhone = phoneBook.NumberPhone;
                phoneBook1.Address = phoneBook.Address;
                phoneBook1.Description = phoneBook.Description;

                db.PhoneBook.Update(phoneBook1);
                db.SaveChanges();

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
            if (id == null)
            {
                return NotFound();
            }
            
            var phoneBook = db.PhoneBook.Find(id);

            if (phoneBook == null)
            {
                return NotFound();
            }
            return View(phoneBook);
        }

        // POST: PhoneBook/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {  
            try
            {
                // TODO: Add update logic here

                PhoneBook phoneBook1 = db.PhoneBook.Find(id);

                db.PhoneBook.Remove(phoneBook1);
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}