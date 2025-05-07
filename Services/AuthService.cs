using System;
using System.Linq;
using Pertiv_be_with_dotnet.Data;
using Pertiv_be_with_dotnet.Helper;
using Pertiv_be_with_dotnet.Models;
using System.Diagnostics;

namespace Pertiv_be_with_dotnet.Services
{
    public class AuthService
    {
        private static AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public UserModel LoginAccount(string email, string password)
        {
            var findEmailAccount = _context.Users.FirstOrDefault(item => item.Email == email);
            if (findEmailAccount == null)
            {
                throw new Exception("Email or password is not valid");
            }


            if (!AccountPassword.VerifyPassword(password, findEmailAccount.Password))
            {
                throw new Exception("Email or password is not valid 2");
            }
            return findEmailAccount;
        }



    }
}
