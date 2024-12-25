using DrugStore.Data.Entities.User;
using Permission = DrugStore.Data.Entities.Permission.Permission;

namespace DrugStore.Services.Interfaces
{
    public interface IRoleService
    {
        #region Roles
        IReadOnlyList<Role> GetAllRoles();
        int AddRole(Role role);
        void DeleteRoleById(int id);
        void EditRole(Role role);
        Role GetRoleById(int id);
        bool AddRolesToUser(IReadOnlyList<int> roleIds, int userId);
        bool EditRolesUser(int userId, IReadOnlyList<int> roleIds);

        #endregion
        #region Permission

        IReadOnlyList<Permission> GetAllPermissions();
        void AddPermissionToRole(int roleId, IReadOnlyList<int> permissions);
        IReadOnlyList<int> GetPermissionsByRoleId(int roleId);
        void UpdatePermissionRole(int roleId, IReadOnlyList<int> permissions);

        bool CheckPermission(int permissionId, string username);
        IReadOnlyList<Permission> GetUserPermissions(string username);

        #endregion
    }
}
