using System;
using System.Collections.Generic;
using Project.Entities;
using System.Data.Entity;
using System.Linq;
using System.Web;
namespace projectAPI.DAL
{
    public class PContext : DbContext
    {
        public PContext() : base("PContext")
        {
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;
        }
        public DbSet<Project.Entities.Project> Project { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
    
}