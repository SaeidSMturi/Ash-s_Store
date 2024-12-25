using DrugStore.Data.Entities.User;
using System.ComponentModel.DataAnnotations;

namespace DrugStore.Data.Entities.Permission
{
    public class RolePermission
    {
        [Key]
        public int RP_Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        public virtual Role Role { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
