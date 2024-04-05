using Microsoft.EntityFrameworkCore;
using System.Net;
using Website_API.Models;

namespace Website_API.Data
{
    public class ApplicationDBContext:DbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) :base(options)
        {

        }
        public DbSet<Customers> Customers { get; set; }

        public DbSet<User> User { get; set; }
        public DbSet<User_Address> Address { get; set; }
    }
}
