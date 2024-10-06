﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PhoneBookDesktopApplication
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;

    public partial class DBDesktopPhoneBookEntities : DbContext
    {
        public DBDesktopPhoneBookEntities()
            : base("name=DBDesktopPhoneBookEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Users> Users { get; set; }

        List<Users> users; 

        public string FindUser(string userName, string password)
        {
            Users user = Users.FirstOrDefault(u => u.Username == userName);

            if (user != null)
            {
                if (user.Password.Trim() == password.ToString())
                {
                    Console.WriteLine($"Ок_{user.Role.ToString()}");
                    return ($"Ок_{user.Role.ToString()}");
                }
                else
                {
                    return ("Неверный пароль");
                }
            }
            else
            {
                return ("Пользователь не найден");
            }
        }
        public string RegisterUser(string userName, string password)
        {
            string role;
            if (userName.Contains("Admin") || userName.Contains("admin"))
            {
                role = "admin";
                
            }
            else
            {
                role = "user";
            }
            
            Users.Add(new Users(Users.OrderByDescending(p => p.Id).FirstOrDefault().Id + 1, userName, password, role));
            SaveChanges();
            return ("Ok");
        }

        public List<Users> GetUsers()
        {
            this.Users.Load();
            return this.Users.Local.ToList<Users>();
        }

        public void DeleteUser(Users users)
        {
            this.Users.Remove(users);
            SaveChanges();
        }
    }
}
