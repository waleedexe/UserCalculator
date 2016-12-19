using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlRepository
{
    public class UserDb: DbContext
    {
        public UserDb() : base("UserContext")
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserOperation> UserOperations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserOperation>()
                .HasRequired(u => u.User);
                
        }
    }
}
