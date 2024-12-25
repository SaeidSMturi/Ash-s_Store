using DrugStore.Data;
using DrugStore.Data.Entities.Permission;
using DrugStore.Data.Entities.User;
using DrugStore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Services
{
    public class RoleService : IRoleService
    {
        private readonly WebContext _context;
        private readonly ILogger<RoleService> _logger;

        public RoleService(WebContext context, ILogger<RoleService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IReadOnlyList<Role> GetAllRoles()
        {
            return _context.Roles.Where(r => r.IsVisible).Include(role => role.RolePermissions).Include(role => role.UserRoles).ToList();
        }

        public int AddRole(Role role)
        {
            try
            {
                if (role.RoleTitle != "Administrator")
                {
                    role.IsVisible = true;
                    _context.Roles.Add(role);
                    _context.SaveChanges();
                    _logger.LogInformation($"The role has been successfully created in the RoleService. Role title: {role.RoleTitle}");
                    return role.RoleId;
                }
                return 0;
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occurred while creating the role in the RoleService. Error message: {e.Message}");
                return 0;
            }
        }

        public void DeleteRoleById(int id)
        {
            try
            {
                var role = GetRoleById(id);
                if (role.RoleTitle != "Administrator")
                {
                    role.IsDelete = true;
                    _context.Update(role);
                    _context.SaveChanges();
                    _logger.LogInformation($"The role has been successfully removed in the RoleService. Role title: {role.RoleTitle}");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occurred while removing the role in the RoleService. Error message: {e.Message}");
                throw;
            }
        }

        public void EditRole(Role role)
        {
            try
            {
                if (role.RoleTitle != "Administrator")
                {
                    role.IsVisible = true;
                    _context.Update(role);
                    _context.SaveChanges();
                    _logger.LogInformation($"The role has been successfully edited in the RoleService. Role title: {role.RoleTitle}");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occurred while editing the role in the RoleService. Error message: {e.Message}");
                throw;
            }
        }

        public Role GetRoleById(int id)
        {
            return _context.Roles.Find(id);
        }

        public bool AddRolesToUser(IReadOnlyList<int>? roleIds, int userId)
        {
            try
            {
                if (userId != 1)
                {
                    if (roleIds != null)
                    {
                        foreach (var roleId in roleIds)
                        {
                            _context.UserRoles.Add(new UserRole
                            {
                                RoleId = roleId,
                                UserId = userId
                            });
                        }
                    }
                    _context.SaveChanges();
                    _logger.LogInformation($"The role has been successfully added to user in the RoleService. userId: {userId}");
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occurred while adding roles to user in the RoleService. Error message: {e.Message}");
                return false;
            }
        }

        public bool EditRolesUser(int userId, IReadOnlyList<int> roleIds)
        {
            try
            {
                if (userId != 1)
                {
                    var userRoles = _context.UserRoles.Where(r => r.UserId == userId).ToList();
                    _context.UserRoles.RemoveRange(userRoles);
                    AddRolesToUser(roleIds, userId);
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occurred while editing roles for user in the RoleService. Error message: {e.Message}");
                return false;
            }
        }

        public IReadOnlyList<Permission> GetAllPermissions()
        {
            return _context.Permissions.ToList();
        }

        public void AddPermissionToRole(int roleId, IReadOnlyList<int> permissions)
        {
            if (roleId != 1)
            {
                foreach (var permissionId in permissions)
                {
                    _context.RolePermissions.Add(new RolePermission
                    {
                        PermissionId = permissionId,
                        RoleId = roleId
                    });
                }
                _context.SaveChanges();
            }
        }

        public IReadOnlyList<int> GetPermissionsByRoleId(int roleId)
        {
            return _context.RolePermissions
                .Where(p => p.RoleId == roleId)
                .Select(r => r.PermissionId)
                .ToList();
        }

        public void UpdatePermissionRole(int roleId, IReadOnlyList<int> permissions)
        {
            if (roleId != 1)
            {
                var rolePermissions = _context.RolePermissions.Where(p => p.RoleId == roleId).ToList();
                _context.RolePermissions.RemoveRange(rolePermissions);
                AddPermissionToRole(roleId, permissions);
            }
        }

        public bool CheckPermission(int permissionId, string username)
        {
            var userId = _context.Users.Single(u => u.UserName == username).UserId;

            var userRoles = _context.UserRoles
                .Where(r => r.UserId == userId)
                .Select(r => r.RoleId)
                .ToList();

            if (!userRoles.Any())
                return false;

            var rolesPermissions = _context.RolePermissions
                .Where(p => p.PermissionId == permissionId)
                .Select(p => p.RoleId)
                .ToList();

            return rolesPermissions.Any(p => userRoles.Contains(p));
        }

        public IReadOnlyList<Permission> GetUserPermissions(string username)
        {
            var userId = _context.Users.Single(u => u.UserName == username).UserId;

            var userRoleIds = _context.UserRoles
                .Where(r => r.UserId == userId)
                .Select(r => r.RoleId)
                .ToList();

            if (!userRoleIds.Any())
                return new List<Permission>();

            var permissionIds = _context.RolePermissions
                .Where(rp => userRoleIds.Contains(rp.RoleId))
                .Select(rp => rp.PermissionId)
                .Distinct()
                .ToList();

            return _context.Permissions
                .Where(p => permissionIds.Contains(p.PermissionId))
                .ToList();
        }
    }
}
