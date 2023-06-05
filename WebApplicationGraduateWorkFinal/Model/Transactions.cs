using System.ComponentModel.DataAnnotations;

namespace WebApplicationGraduateWorkFinal.Model
{
    public class Transactions
    {
        [Key]
        public int Id { get; set; }
        public string TransactionUniqueReference { get; set; } // this we will generate in every instance off this class
        public decimal TransactionAmount { get; set; }
        public TranStatus TransactionStatus { get; set; } // this is an enum too let's create it
        public bool IsSuccessful => TransactionStatus.Equals(TranStatus.Success);
        public string TransactionSourceAccount { get; set; }
        public string TransactionDestinationAccount { get; set; }
        public string TransactionParticulars { get; set; }
        public TranType TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }

        public Transactions()
        {
            TransactionUniqueReference = $"{Guid.NewGuid().ToString().Replace("-", "").Substring(1, 27)}";
        }
    }


    public enum TranStatus
    {
        Failed,
        Success,
        Error
    }

    public enum TranType
    {
        Deposit,
        Withdrawal,
        Transfer
    }
}

