using System.Collections.Generic;
using WebApplication123.Models;
using WebApplicationGraduateWorkFinal.Model;

namespace WebApplication123.Services.Interfaces
{
    public interface IAccountServices
    {
        Account Authenticate(string AccountNumber, string Pin);
        IEnumerable<Account> GetAccount();
        Account Create(Account account, string Pin, string ConfirmPin);
        void Update(Account account, string Pin = null);
        void Delete(int Id);
        Account GetById(int Id);
        Account GetByAccountNumber(string AccountNumber);
    }
}
