using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.VisualBasic;
using Pertiv_be_with_dotnet.Data;
using Pertiv_be_with_dotnet.Enums;
using Pertiv_be_with_dotnet.Helper;
using Pertiv_be_with_dotnet.Models;

namespace Pertiv_be_with_dotnet.Services
{
    public class UserService
    {
        private static AppDbContext _context;

        public UserService(AppDbContext context) {
            _context = context;
        }

        public void CreateStaffAccount(UserModel user)
        {
            var exitingStaffAccount = _context.Users.FirstOrDefault(u => u.Email == user.Email);

            if (exitingStaffAccount != null)
            {
                throw new Exception("Email already exists");
            }
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public List<UserModel> GetStaffAccount ()
        {
            var data = _context.Users.Where(item => item.Role == UserRole.staff).ToList();

            return data;
        }

        public void DeleteStaffAccount(Guid userId)
        {
            var findStaffAccount = _context.Users.Where(item => item.Id == userId).FirstOrDefault();
            if (findStaffAccount == null)
            {
                throw new Exception("Staff account is not found");
            }

            _context.Users.Remove(findStaffAccount);
            _context.SaveChanges();
        }

        public void UpdateStaffAccount(Guid id,UserModel user)
        {
            var exitingAccount = _context.Users.Where(item => item.Id == id).FirstOrDefault();
            if (exitingAccount == null)
            {
                throw new Exception("Staff account is not found");
            }

            exitingAccount.Name = user.Name;

            bool checkSamePassword = AccountPassword.VerifyPassword(user.Password, exitingAccount.Password);

            if (!checkSamePassword)
            {
                exitingAccount.Password = AccountPassword.HashPassword(user.Password);
            }

            _context.Users.Update(exitingAccount);
            _context.SaveChanges();
        }
    }
}
