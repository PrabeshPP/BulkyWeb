using BulkyWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Data{
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        //place to create a table
        public DbSet<Category> Categories{get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Category>().HasData(
                new Category{Id=1,Name="Action",DisplayOrder=3},
                new Category{Id=2,Name="Sci-Fi",DisplayOrder=1},
                new Category{Id=3,Name="History",DisplayOrder=4}
            );
        }
    }
}