using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class TaskManagementContext : DbContext
    {

        public virtual DbSet<Task> Tasks { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Comments> Comments { get; set; }

        public TaskManagementContext() : base(nameOrConnectionString: "Default") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }

        
    }
}
