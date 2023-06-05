using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApplicationGraduateWorkFinal.Model;

namespace WebApplicationGraduateWorkFinal.Context
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

        public DbSet<Account> Account { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
    }
}
