using Microsoft.EntityFrameworkCore;
using SrinivasaERP.Models;

namespace SrinivasaERP.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Register> Registers { get; set; }
        public DbSet<ApplyLeave> ApplyLeaves { get; set; }
        public DbSet<CurrentAddress> CurrentAddresses { get; set; } 
    }
}
