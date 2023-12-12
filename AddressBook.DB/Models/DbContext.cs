using AddressBook.DB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Address_Book.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
        { }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<AddressBooks> AddressBookEntries { get; set; }
        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Login> Login { get; set; }
        public DbSet<Register> Register { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddressBooks>()
                .HasOne(a => a.Job)
                .WithMany()
                .HasForeignKey(a => a.JobId);

            modelBuilder.Entity<AddressBooks>()
                .HasOne(a => a.Department)
                .WithMany()
                .HasForeignKey(a => a.DepartmentId);
        }
    }
}