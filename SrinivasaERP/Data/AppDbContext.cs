using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using SrinivasaERP.Models;

namespace SrinivasaERP.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<SrinivasaERP.Models.Register> Registers { get; set; }
        public DbSet<ApplyLeave> ApplyLeaves { get; set; }

    }
}
