using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

namespace ListMVVM.Models
{
    public static class DataBaseLogic
    {
        public static DbContextOptions<ApplicationContext> options;

        public static void StartSettings()
        {
            /*var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            //my configure file
            builder.AddJsonFile("Configures/DbConfig.json");

            //building
            var config = builder.Build();

            //getting connection string
            string connectionString = config.GetConnectionString("DefaultConnection");*/

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            //setting options for ApplicationContext
            options = optionsBuilder.UseSqlite("Data Source=phonedb.db").Options;
        }

        //CRUD 
        public static void AddNewItem(Phone? item)
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                if (item != null)
                {
                    db.Phones.Add(item);
                    db.SaveChanges();
                    Console.WriteLine("Added successfully!");
                }
            }
        }
        public static void RemoveItem(Phone? item)
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                if (item != null)
                {
                    db.Phones.Remove(item);
                    db.SaveChanges();
                    Console.WriteLine("Removed successfully!");
                }
            }
        }
        public static void UpdateItem(Phone? item)
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                if (item != null)
                {
                    db.Phones.Update(item);
                    db.SaveChanges();
                    Console.WriteLine("Updated successfully!");
                }
            }
        }

        public static Phone[] GetAllData()
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var phones = db.Phones.ToList();
                return phones.ToArray();
            }
        }

    }
}
