using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DotNetIdentityDemo.Models
{
    public class MyUser : IdentityUser<Guid,MyLogin,MyUserRole,MyClaim>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<MyUser,Guid> manager)
        {
            // 注意 authenticationType 必須符合 CookieAuthenticationOptions.AuthenticationType 中定義的項目
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在這裡新增自訂使用者宣告
            return userIdentity;
        }
    }

    public class MyUserRole : IdentityUserRole<Guid> { }

    public class MyRole : IdentityRole<Guid, MyUserRole>
    {
        public MyRole()
        {
            Id = Guid.NewGuid();
        }

        public MyRole(string roleName) : this()
        {
            Name = roleName;
        }
    }

    public class MyClaim : IdentityUserClaim<Guid> { }

    public class MyLogin : IdentityUserLogin<Guid> { }


    public class ApplicationDbContext : IdentityDbContext<MyUser,MyRole,Guid,MyLogin,MyUserRole,MyClaim>
    {
        public ApplicationDbContext() : base("MyIdentityConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //映射
            modelBuilder.Entity<MyUser>().ToTable("User");
            modelBuilder.Entity<MyUserRole>().ToTable("UserRole");
            modelBuilder.Entity<MyRole>().ToTable("Role");
            modelBuilder.Entity<MyClaim>().ToTable("UserClaim");
            modelBuilder.Entity<MyLogin>().ToTable("UserLogin");
            //設定自動增加屬性
            modelBuilder.Entity<MyClaim>().Property(r => r.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}