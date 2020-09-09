using System.Reflection.Emit;
using Dapper.Infraestructure.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dapper.Infraestructure.Identity.Context {
    public class IdentityContext : IdentityDbContext<ApplicationUser> {
       
        public IdentityContext(DbContextOptions<IdentityContext> options) : base (options) { }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=DBName;Integrated Security=True");
        }*/

        protected override void OnModelCreating(ModelBuilder builder)
        {
          base.OnModelCreating(builder);
        }

       
    }
}