using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using IdeaManMVC.Models.Ideas;

namespace IdeaManMVC.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            manager.AddClaim(userIdentity.GetUserId(), new Claim("iDea:FullName", FullName));
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<ApplicationUser> AppUsers { get; set; }
        public DbSet<IdeaEntry> Ideas { get; set; }
        public DbSet<Vote> Votes { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected void SettingsSavingShared()
        {
            var entities =
                ChangeTracker.Entries()
                    .Where(
                        x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var user = HttpContext.Current != null && HttpContext.Current.User != null
                ? HttpContext.Current.User.Identity.Name
                : "Anonymous";

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).DateCreated = DateTime.Now;
                    ((BaseEntity)entity.Entity).UserCreated = user;
                }
                ((BaseEntity)entity.Entity).DateModified = DateTime.Now;
                ((BaseEntity)entity.Entity).UserModified = user;
            }
        }
        public override int SaveChanges()
        {
            SettingsSavingShared();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            SettingsSavingShared();
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}