using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugStore.Data.Entities.Permission
{
    public class Permission
    {
        public int PermissionId { get; set; }

        [Display(Name = "")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کارکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!")]
        public string PermissionTitle { get; set; }
        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual List<Permission> Permissions { get; set; }
        public virtual List<RolePermission> RolePermissions { get; set; }
    }
}
