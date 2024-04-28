using Microsoft.EntityFrameworkCore;
using static BankBranchAPI.Controllers.BankController;

namespace BankBranchAPI.Models
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options) 
            : base(options)
        {
        }
        public DbSet<BankBranch> BankBranches { get; set; }
        public DbSet<Employee> employees { get; set; }
        public DbSet<UserAccounts> Users { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    options.UseSqlite("Data Source=Bank.db");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.CivilId)
                .IsUnique();
        }
    }
}

