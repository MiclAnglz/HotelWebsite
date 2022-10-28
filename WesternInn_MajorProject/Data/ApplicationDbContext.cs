using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WesternInn_MajorProject.Models;

namespace WesternInn_MajorProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
          
        }

        public DbSet<WesternInn_MajorProject.Models.Booking> Booking { get; set; }

        public DbSet<WesternInn_MajorProject.Models.Room> Room { get; set; }

        public DbSet<WesternInn_MajorProject.Models.Guest> Guest { get; set; }

        public DbSet<WesternInn_MajorProject.Models.ViewStats> ViewStats { get; set; }

        public DbSet<WesternInn_MajorProject.Models.lastID> lastID { get; set; }
    }
}