using System.Transactions;
using WebApplication123.Models;
using WebApplicationGraduateWorkFinal.Model;

namespace WebApplication123.Services.Interfaces
{
    public interface ITransactionService
    {
        Response CreateNewTransaction(Transactions transaction);
        Response FindTransactionByDate(DateTime data);
        Response MakeDeposit(string AccountNumber, decimal Ammount, string TransactionPin);
        Response MakeWithdrawal(string AccountNumber, decimal Ammount, string TransactionPin);
        Response MakeFundsTransfer(string FromAccount ,string AccountNumber, decimal Ammount, string TransactionPin);
    }
}
