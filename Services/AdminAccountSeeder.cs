using Pertiv_be_with_dotnet.Data;
using Pertiv_be_with_dotnet.Models;
using Pertiv_be_with_dotnet.Enums;

using System.Linq;
using System;
using System.Diagnostics;
using Pertiv_be_with_dotnet.Helper;
namespace Pertiv_be_with_dotnet.Services
{
    public class AdminAccountSeeder
    {
        private static AppDbContext _context;

        public AdminAccountSeeder(AppDbContext context)
        {
             _context = context;
        }
        public void CreateAccount()
        {
            var findExitingAdmin = _context.Users.FirstOrDefault(acc => acc.Email == "admin@admin.com");
            if (findExitingAdmin == null)
            {
                Debug.WriteLine("Create account is running");
            var dataAdmin = new UserModel
            {
                Email = "admin@admin.com",
                Name = "admin",
                Password = AccountPassword.HashPassword("admin123"),
                Role = UserRole.admin,
            };
            _context.Users.Add(dataAdmin);
            _context.SaveChanges();
            } 
        }
    }
}
