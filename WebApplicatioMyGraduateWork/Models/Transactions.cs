namespace WebApplicatioMyGraduateWork.BankTables
{
    public class Transactions
    {
        public int AccountId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime TimeStamp { get; set; }
        public Accounts? Accounts { get; set; }
    }
}
