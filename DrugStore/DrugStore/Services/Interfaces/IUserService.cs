using DrugStore.Data.Entities.User;
using DrugStore.DTOs.User;

namespace DrugStore.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> IsExistUserNameAsync(string userName);
        Task<User?> GetAllDetailsForAdminAsync(int userId);
        Task<bool> IsExistEmailAsync(string email);
        Task<int> AddUserAsync(User? user);
        Task<User?> LoginUserAsync(LoginViewModel login);
        Task<User?> FindUserWithSecurityCodeAsync(string activeCode);
        Task<User?> FindUserWithActiveCodeAsync(string activeCode);
        Task<User?> FindUserWithUsernameAsync(string userName);
        Task<User?> FindUserWithUserIdAsync(int Id);
        Task<bool> UpdateUserAsync(User? user);
        Task<bool> IsEmailAsync(string email);
        Task<bool> IsChangeInMinutesAsync(User user, int minutes);
        Task<List<User?>> GetAdminUserIdsAsync(int? roleId = 0);

        #region AdminPanel
        Task<int> AddUserFromAdminAsync(CreateUserViewModel createUser);

        Task<bool> EditUserFromAdminAsync(EditUserViewModel editUser);
        Task<EditUserViewModel> GetEditUserAsync(int userId);
        Task<bool> DeleteUserAsync(int userId);

        #endregion
    }
}
