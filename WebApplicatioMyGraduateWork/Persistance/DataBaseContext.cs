using Microsoft.EntityFrameworkCore;
using WebApplicatioMyGraduateWork.BankTables;

namespace WebApplicatioMyGraduateWork.Persistance
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Accounts>? account { get; set; }
        public DbSet<BankStaff>? bankStaff { get; set; }
        public DbSet<Customers>? customers { get; set; }
        public DbSet<Deposits>? deposits { get; set; }
        public DbSet<Loans>? loans { get; set; }
        public DbSet<Payments>? payments { get; set; }
        public DbSet<ReceiverAccountId>? receiverAccountIds { get; set; }
        public DbSet<SenderAccountId>? senderAccountIds { get; set; }
        public DbSet<Transactions>? transactions { get; set; }
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options) { }

    }
}
