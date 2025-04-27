using Microsoft.EntityFrameworkCore;
using YardenProject.Models;

namespace YardenProject.Data
{
    public class GaiaDbContext : DbContext
    {
        public GaiaDbContext(DbContextOptions<GaiaDbContext> options) : base(options) { }

        public DbSet<OperationHistory> OperationHistories { get; set; }
    }
}
