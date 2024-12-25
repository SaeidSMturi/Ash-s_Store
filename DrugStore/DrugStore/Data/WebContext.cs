using DrugStore.Data.Entities.Permission;
using DrugStore.Data.Entities.Product;
using DrugStore.Data.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Data
{
    public class WebContext : DbContext
    {
        public WebContext(DbContextOptions<WebContext> options) : base(options)
        {
        }

        // Define your DbSets here
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(u => u.IsDelete == false);
            modelBuilder.Entity<Role>().HasQueryFilter(u => u.IsDelete == false);

            // Seed data
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    UserId = 1,
                    FirstName = "Admin",
                    LastName = "",
                    ActiveCode = "811b78fab121434b8e20b2b263e6bd7e",
                    Email = "Admin@example.com",
                    IsDelete = false,
                    IsEmailActive = true,
                    LastChange = DateTime.Now,
                    RegisterDate = DateTime.Now,
                    SecurityCode = "40c87d0a7d83413ab8e8c59229e8949e",
                    Password = "pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=",
                    UserName = "admin"
                });
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole()
                {
                    UR_Id = 1,
                    UserId = 1,
                    RoleId = 1
                });
            modelBuilder.Entity<Role>().HasData(
                new Role()
                {
                    RoleId = 1,
                    IsDelete = false,
                    RoleTitle = "Administrator",
                    IsVisible = false
                });

            modelBuilder.Entity<Permission>().HasData(
                new Permission()
                {
                    PermissionId = 1,
                    PermissionTitle = "Admin"
                }
            );

            modelBuilder.Entity<RolePermission>().HasData(
                new RolePermission()
                {
                    RP_Id = 1,
                    RoleId = 1,
                    PermissionId = 1
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
