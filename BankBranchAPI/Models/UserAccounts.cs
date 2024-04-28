using Microsoft.AspNetCore.Mvc;

namespace BankBranchAPI.Models
{
    public class UserAccounts()
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool isAdmin { get; set; }

        public static UserAccounts Create(string username, string password, bool isAdmin = false)
        {
            return new UserAccounts
            {
                username = username,
                password = BCrypt.Net.BCrypt.EnhancedHashPassword(password),
                isAdmin = isAdmin
            };
        }

        public bool VerifyPassword(string pwd) => BCrypt.Net.BCrypt.EnhancedVerify(pwd, this.password);
    }


}

