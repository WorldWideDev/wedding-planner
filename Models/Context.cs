using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }    
        public DbSet<Wedding> Weddings { get; set; }    
        public DbSet<Response> Responses { get; set; }    
        public DbSet<Address> Addresses { get; set; }    

    }
}