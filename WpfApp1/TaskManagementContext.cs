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

            //modelBuilder.Entity<Task>().ToTable("Tasks");

                //.HasIndex(a => a.TaskID);

            //int i = 0;

            //modelBuilder.Entity<User>().ToTable("Users");
            //modelBuilder.Entity<Task>().Property(b => b.TaskID).HasColumnType("int");
            int i = 0;
        }

        
    }
}
