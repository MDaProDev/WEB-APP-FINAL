using Microsoft.EntityFrameworkCore;
using WEB_APP_FINAL.Models;

namespace WEB_APP_FINAL.Data
{
    public class AppDbContext: DbContext
    {
        public required DbSet<Producto> Productos { get; set; }
        public required DbSet<Tarea> Tareas { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
