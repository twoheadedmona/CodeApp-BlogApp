using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Project.Core;
using Project.Core.Entities;

namespace Project.Infrastructure.Data
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext() : base("name=DatabaseContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, Project.Infrastructure.Migrations.Configuration>("DatabaseContext"));
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
