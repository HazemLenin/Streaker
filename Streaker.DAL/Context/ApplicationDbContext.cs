using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Streaker.Core.Common;
using Streaker.Core.Domains;

namespace Streaker.DAL.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().HasQueryFilter(u => u.IsActive);
            builder.Entity<Streak>().HasQueryFilter(q => !q.IsDeleted);
            builder.Entity<Commit>().HasQueryFilter(q => !q.IsDeleted);
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted);
            
            foreach (var entry in entries)
            {
                if (entry.Entity is BaseDomain)
                {
                    entry.State = EntityState.Modified;
                    entry.CurrentValues["IsDeleted"] = true;
                    entry.CurrentValues["Deleted"] = DateTime.UtcNow;
                }
                else if (entry.Entity is ApplicationUser)
                {
                    entry.State = EntityState.Modified;
                    entry.CurrentValues["IsActive"] = false;
                    entry.CurrentValues["Deactivated"] = DateTime.UtcNow;
                }
            }
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var entries = ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted);

            foreach (var entry in entries)
            {
                if (entry.Entity is BaseDomain)
                {
                    entry.State = EntityState.Modified;
                    entry.CurrentValues["IsDeleted"] = true;
                    entry.CurrentValues["Deleted"] = DateTime.UtcNow;
                }
                else if (entry.Entity is ApplicationUser)
                {
                    entry.State = EntityState.Modified;
                    entry.CurrentValues["IsActive"] = false;
                    entry.CurrentValues["Deactivated"] = DateTime.UtcNow;
                }
            }
            return await base.SaveChangesAsync();
        }

        public DbSet<Streak> Streaks { get; set; }
        public DbSet<Commit> Commits { get; set; }
    }
}
