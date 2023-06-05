using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebApplication123.Models;
using WebApplication123.Services.Interfaces;
using WebApplication123.Units;
using WebApplicationGraduateWorkFinal.Context;
using WebApplicationGraduateWorkFinal.Model;

namespace WebApplicationGraduateWorkFinal.Services
{
    public class TransactionService : ITransactionService
    {
        private DataBaseContext _dataBaseContext;
        ILogger<TransactionService> _logger;
        private AppSettings _settings;
        private static string _ourBankSettlementAccount;
        private readonly IAccountServices _accountServices;

        public TransactionService(DataBaseContext dataBaseContext,
           ILogger<TransactionService> logger,
           IOptions<AppSettings> settings,
           IAccountServices accountServices)
        {
            _dataBaseContext = dataBaseContext;
            _logger = logger;
            _settings = settings.Value;
            _ourBankSettlementAccount = _settings.OurBankSettlementAccount;
            _accountServices = accountServices;
        }
        public Response CreateNewTransaction(Transactions transaction)
        {
            Response response = new Response();
            _dataBaseContext.Transactions.Add(transaction);
            _dataBaseContext.SaveChanges();
            response.ResponseCode = "00";
            response.ResponseMessage = "Transaction create successfully";
            response.Date = null;

            return response;
        }

        public Response FindTransactionByDate(DateTime data)
        {
            var transaction = _dataBaseContext.Transactions.Where(x => x.TransactionDate == data).ToList();

            Response response = new Response();
            response.ResponseCode = "00";
            response.ResponseMessage = "Transaction create successfully";
            response.Date = transaction;

            return response;
        }

        public Response MakeDeposit(string AccountNumber, decimal Ammount, string TransactionPin)
        {
            Response response = new Response();
            Account sourseAccount;
            Account destinationAccount;
            Transactions transaction = new Transactions();

            var authUser = _accountServices.Authenticate(AccountNumber, TransactionPin);
            if (authUser == null) throw new ApplicationException("Invalid credentials");

            try
            {
                sourseAccount = _accountServices.GetByAccountNumber(_ourBankSettlementAccount);
                destinationAccount = _accountServices.GetByAccountNumber(AccountNumber);

                sourseAccount.CurrentAccountBalance -= Ammount;
                destinationAccount.CurrentAccountBalance += Ammount;

                if ((_dataBaseContext.Entry(sourseAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified) &&
                    (_dataBaseContext.Entry(destinationAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified))
                {
                    transaction.TransactionStatus = TranStatus.Success;
                    response.ResponseCode = "00";
                    response.ResponseMessage = "transaction successful";
                    response.Date = null;
                }
                else
                {
                    transaction.TransactionStatus = TranStatus.Success;
                    response.ResponseCode = "00";
                    response.ResponseMessage = "transaction failed";
                    response.Date = null;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"AN ERROR OCCURRED ... => {ex.Message}");
            }

            transaction.TransactionType = TranType.Deposit;
            transaction.TransactionSourceAccount = _ourBankSettlementAccount;
            transaction.TransactionDestinationAccount = AccountNumber;
            transaction.TransactionAmount = Ammount;
            transaction.TransactionDate = DateTime.Now;
            transaction.TransactionParticulars = $"NEW TRANSACTION FROM SOURCE => {JsonConvert.SerializeObject
                (transaction.TransactionSourceAccount)} TO DESTINATION ACCOUNT => {JsonConvert.SerializeObject
                (transaction.TransactionDestinationAccount)} ON DATE => {transaction.TransactionDate} FOR AMOUNT => {JsonConvert.SerializeObject
                (transaction.TransactionAmount)} TRANSACTION TYPE => {JsonConvert.SerializeObject(transaction.TransactionType)} TRANSACTION STATUS => {JsonConvert.SerializeObject(transaction.TransactionStatus)}";

            _dataBaseContext.Transactions.Add(transaction);
            _dataBaseContext.SaveChanges();

            return response;
        }

        public Response MakeFundsTransfer(string FromAccount, string ToAccount, decimal Ammount, string TransactionPin)
        {
            Response response = new Response();
            Account sourseAccount;
            Account destinationAccount;
            Transactions transaction = new Transactions();

            var authUser = _accountServices.Authenticate(FromAccount, TransactionPin);
            if (authUser == null) throw new ApplicationException("Invalid credentials");

            try
            {
                sourseAccount = _accountServices.GetByAccountNumber(FromAccount);
                destinationAccount = _accountServices.GetByAccountNumber(ToAccount);

                sourseAccount.CurrentAccountBalance -= Ammount;
                destinationAccount.CurrentAccountBalance += Ammount;

                if ((_dataBaseContext.Entry(sourseAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified) &&
                    (_dataBaseContext.Entry(destinationAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified))
                {
                    transaction.TransactionStatus = TranStatus.Success;
                    response.ResponseCode = "00";
                    response.ResponseMessage = "transaction successful";
                    response.Date = null;
                }
                else
                {
                    transaction.TransactionStatus = TranStatus.Success;
                    response.ResponseCode = "00";
                    response.ResponseMessage = "transaction failed";
                    response.Date = null;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"AN ERROR OCCURRED ... => {ex.Message}");
            }

            transaction.TransactionType = TranType.Transfer;
            transaction.TransactionSourceAccount = FromAccount;
            transaction.TransactionDestinationAccount = ToAccount;
            transaction.TransactionAmount = Ammount;
            transaction.TransactionDate = DateTime.Now;
            transaction.TransactionParticulars = $"NEW TRANSACTION FROM SOURCE => {JsonConvert.SerializeObject
                (transaction.TransactionSourceAccount)} TO DESTINATION ACCOUNT => {JsonConvert.SerializeObject
                (transaction.TransactionDestinationAccount)} ON DATE => {transaction.TransactionDate} FOR AMOUNT => {JsonConvert.SerializeObject
                (transaction.TransactionAmount)} TRANSACTION TYPE => {JsonConvert.SerializeObject(transaction.TransactionType)} TRANSACTION STATUS => {JsonConvert.SerializeObject(transaction.TransactionStatus)}";

            _dataBaseContext.Transactions.Add(transaction);
            _dataBaseContext.SaveChanges();

            return response;
        }

        public Response MakeWithdrawal(string AccountNumber, decimal Ammount, string TransactionPin)
        {
            Response response = new Response();
            Account sourseAccount;
            Account destinationAccount;
            Transactions transaction = new Transactions();

            var authUser = _accountServices.Authenticate(AccountNumber, TransactionPin);
            if (authUser == null) throw new ApplicationException("Invalid credentials");

            try
            {
                sourseAccount = _accountServices.GetByAccountNumber(AccountNumber);
                destinationAccount = _accountServices.GetByAccountNumber(_ourBankSettlementAccount);

                sourseAccount.CurrentAccountBalance -= Ammount;
                destinationAccount.CurrentAccountBalance += Ammount;

                if ((_dataBaseContext.Entry(sourseAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified) &&
                    (_dataBaseContext.Entry(destinationAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified))
                {
                    transaction.TransactionStatus = TranStatus.Success;
                    response.ResponseCode = "00";
                    response.ResponseMessage = "transaction successful";
                    response.Date = null;
                }
                else
                {
                    transaction.TransactionStatus = TranStatus.Success;
                    response.ResponseCode = "00";
                    response.ResponseMessage = "transaction failed";
                    response.Date = null;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"AN ERROR OCCURRED ... => {ex.Message}");
            }

            transaction.TransactionType = TranType.Withdrawal;
            transaction.TransactionSourceAccount = AccountNumber;
            transaction.TransactionDestinationAccount = _ourBankSettlementAccount;
            transaction.TransactionAmount = Ammount;
            transaction.TransactionDate = DateTime.Now;
            transaction.TransactionParticulars = $"NEW TRANSACTION FROM SOURCE => {JsonConvert.SerializeObject
                (transaction.TransactionSourceAccount)} TO DESTINATION ACCOUNT => {JsonConvert.SerializeObject
                (transaction.TransactionDestinationAccount)} ON DATE => {transaction.TransactionDate} FOR AMOUNT => {JsonConvert.SerializeObject
                (transaction.TransactionAmount)} TRANSACTION TYPE => {JsonConvert.SerializeObject(transaction.TransactionType)} TRANSACTION STATUS => {JsonConvert.SerializeObject(transaction.TransactionStatus)}";

            _dataBaseContext.Transactions.Add(transaction);
            _dataBaseContext.SaveChanges();

            return response;
        }
    }
}
