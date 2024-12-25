using DrugStore.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace DrugStore
{
    public class UserDataGenerator
    {
        private static IUserService _userService;

        public UserDataGenerator(IUserService userService)
        {
            _userService = userService;
        }
        public static string GeneratUniqCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        public static string GenerateUniqueUserName(string email)
        {

            int atIndex = email.IndexOf("@");
            string username = email.Substring(0, atIndex);
            Random random = new Random();

            string generatedUsername = username + random.Next(0, 100);

            return generatedUsername;
        }


        public static string PasswordHasher(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                string hashedPassword = Convert.ToBase64String(hashBytes);

                return hashedPassword;
            }
        }

    }
}
