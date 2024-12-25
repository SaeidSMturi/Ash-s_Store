using DrugStore.Data;
using DrugStore.Data.Entities.User;
using DrugStore.DTOs.User;
using DrugStore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace DrugStore.Services
{
    public class UserService : IUserService
    {
        private readonly WebContext _context;

        public UserService(WebContext context)
        {
            _context = context;
        }

        public async Task<bool> IsExistUserNameAsync(string userName)
        {
            return await _context.Users.AnyAsync(u => u.UserName == userName);
        }

        public async Task<User?> GetAllDetailsForAdminAsync(int userId)
        {
            return await _context.Users
                .AsNoTracking()
                .Include(u => u!.UserRoles)
                .ThenInclude(r => r.Role)
                .SingleOrDefaultAsync(u => u.UserId == userId);
        }

        public bool IsPhoneNumberCorrect(string phoneNumber)
        {
            return phoneNumber.Any(char.IsLetter);
        }

        public async Task<bool> IsExistEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<int> AddUserAsync(User? user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user.UserId;
        }

        public async Task<User?> LoginUserAsync(LoginViewModel login)
        {
            string hashPassword = UserDataGenerator.PasswordHasher(login.Password);

            return await _context.Users.SingleOrDefaultAsync(u =>
                u.Email == login.Email && u.Password == hashPassword);
        }

        public async Task<User?> FindUserWithSecurityCodeAsync(string securityCode)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.SecurityCode == securityCode);
        }

        public async Task<User?> FindUserWithActiveCodeAsync(string activeCode)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.ActiveCode == activeCode);
        }

        public async Task<User?> FindUserWithUsernameAsync(string userName)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.UserName.ToLower() == userName.ToLower());
        }

        public async Task<User?> FindUserWithUserIdAsync(int id)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<bool> UpdateUserAsync(User? user)
        {
            if (user == null || user.UserName == "admin") return false;

            try
            {
                _context.Update(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e);
                Console.ResetColor();
                return false;
            }
        }

        public async Task<bool> IsEmailAsync(string email)
        {
            return await Task.FromResult(IsEmail(email));
        }

        public async Task<bool> IsChangeInMinutesAsync(User user, int minutes)
        {
            return await Task.FromResult(IsChangeInMinutes(user, minutes));
        }

        private bool IsEmail(string email)
        {
            const string pattern = @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$";
            return Regex.IsMatch(email, pattern);
        }

        private bool IsChangeInMinutes(User user, int minutes)
        {
            double dateTime = (DateTime.Now - user.LastChange).TotalMinutes;
            return user != null && dateTime >= minutes;
        }

        public async Task<List<User?>> GetAdminUserIdsAsync(int? roleId)
        {
            var allAdmin = _context.Users.Where(u => u.UserRoles.Any());
            return roleId != 0 ? await allAdmin.Where(u => u.UserRoles.Any(r => r.RoleId == roleId)).ToListAsync() : await allAdmin.ToListAsync();
        }

        public async Task<int> AddUserFromAdminAsync(CreateUserViewModel createUser)
        {
            var user = new User
            {
                UserName = createUser.UserName,
                FirstName = createUser.FirstName,
                LastName = createUser.LastName,
                Email = createUser.Email,
                RegisterDate = DateTime.Now,
                ActiveCode = Guid.NewGuid().ToString().Replace("-", ""),
                IsEmailActive = false,
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.UserId;
        }

        public async Task<bool> EditUserFromAdminAsync(EditUserViewModel editUser)
        {
            var user = await FindUserWithUserIdAsync(editUser.UserId);
            if (user == null) return false;

            user.FirstName = editUser.FirstName;
            user.LastName = editUser.LastName;
            user.Email = editUser.Email;

            if (!string.IsNullOrEmpty(editUser.Password))
            {
                user.Password = UserDataGenerator.PasswordHasher(editUser.Password);
            }

            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await FindUserWithUserIdAsync(userId);
            if (user == null) return false;

            user.IsDelete = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<EditUserViewModel> GetEditUserAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .SingleOrDefaultAsync(u => u.UserId == userId);

            if (user == null) return null;

            return new EditUserViewModel
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                Password = user.Password,
                Address = user.Address,
                PostalCode = user.PostalCode,
                IsEmailActive = user.IsEmailActive,
                SelectedRoles = user.UserRoles.Select(r => r.RoleId).ToList()
            };
        }

    }
}
