using Microsoft.EntityFrameworkCore;
using SrinivasaERP.Models;
using System.Collections.Generic;

namespace SrinivasaERP.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Register> Registers { get; set; } // keep as-is
        public DbSet<CurrentAddress> CurrentAddresses { get; set; }
        public DbSet<ApplyLeave> ApplyLeaves { get; set; }
        public object? Users { get; internal set; }
        
        
    }
}