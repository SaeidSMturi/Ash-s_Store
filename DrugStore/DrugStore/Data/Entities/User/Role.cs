using DrugStore.Data.Entities.Permission;
using System.ComponentModel.DataAnnotations;

namespace DrugStore.Data.Entities.User
{
    public class Role
    {
        public Role()
        {

        }

        [Key]
        public int RoleId { get; set; }

        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string RoleTitle { get; set; }
        public bool IsDelete { get; set; }
        public bool IsVisible { get; set; }



        #region Relations

        public virtual IEnumerable<UserRole> UserRoles { get; set; }
        public virtual IEnumerable<RolePermission> RolePermissions { get; set; }

        #endregion
    }
}
