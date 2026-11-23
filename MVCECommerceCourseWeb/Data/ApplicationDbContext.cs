using Microsoft.EntityFrameworkCore;
using MVCECommerceCourseWeb.Models;

namespace MVCECommerceCourseWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
    }
}
