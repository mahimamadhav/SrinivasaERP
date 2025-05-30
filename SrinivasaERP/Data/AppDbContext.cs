using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace SrinivasaERP.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<SrinivasaERP.Models.Register> Registers { get; set; }
        
    }
}
