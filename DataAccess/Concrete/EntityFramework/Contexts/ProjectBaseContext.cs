using Microsoft.EntityFrameworkCore;
using Entities.Concrete;
using Core.Entities.Concrete;
using Core.Utilities.Helper;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public class ProjectBaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(b => b.Ignore(RelationalEventId.AmbientTransactionWarning));
            optionsBuilder.UseSqlServer(ConfigHelper.GetConfig("ConnectionString"));
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<Description> Description { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<ResetPasswordRequests> ResetPasswordRequests { get; set; }
        public DbSet<RolePermissions> RolePermissions { get; set; }
        public DbSet<RolePermissionsMatch> RolePermissionsMatch { get; set; }
        public DbSet<RolePermissionsType> RolePermissionsType { get; set; }
        public DbSet<UserType> UserType { get; set; }
    }
}
