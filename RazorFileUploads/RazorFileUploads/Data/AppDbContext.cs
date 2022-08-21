
using Microsoft.EntityFrameworkCore;
using RazorFileUploads.Models;

namespace Asp.NetCoreRazorFileUpload.Data
{
    public class AppDbContext:DbContext
    {
        public DbSet<FileModel>FileModels { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
