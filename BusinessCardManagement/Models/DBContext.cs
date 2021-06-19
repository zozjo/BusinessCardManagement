using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace BusinessCardManagement.Models
{
    public class DBContext :DbContext
    {
        public DBContext() : base("DBConnectionString") { 
        
        }
        public DbSet<BusinessCard> businessCards { get; set; }
        public DbSet<User> users { get; set; }
    }
}