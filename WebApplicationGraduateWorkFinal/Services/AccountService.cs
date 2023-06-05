using System.Text;
using WebApplication123.Services.Interfaces;
using WebApplicationGraduateWorkFinal.Context;
using WebApplicationGraduateWorkFinal.Model;

namespace WebApplicationGraduateWorkFinal.Services
{
    public class AccountService : IAccountServices
    {
        private DataBaseContext _dataBaseContext;

        public AccountService(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }
        public Account Authenticate(string AccountNumber, string Pin)
        {
            var account = _dataBaseContext.Account.Where(x => x.AccountNumberGenerated == AccountNumber).SingleOrDefault();
            if (account == null)
                return null;

            if (!VerifyPinHash(Pin, account.PinHash, account.PinSalt))
                return null;

            return account;
        }
        private static bool VerifyPinHash(string Pin, byte[] pinHash, byte[] pinSalt)
        {
            if (string.IsNullOrWhiteSpace(Pin)) throw new ArgumentNullException("Pin");

            using(var hmac = new System.Security.Cryptography.HMACSHA512(pinSalt))
            {
                var computedPinHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Pin));
                for(int i = 0; i < computedPinHash.Length; i++)
                {
                    if (computedPinHash[i] != pinHash[i]) return false;
                }
            }

            return true;
        }

        public Account Create(Account account, string Pin, string ConfirmPin)
        {
            if (_dataBaseContext.Account.Any(x => x.Email == account.Email)) throw new ApplicationException("An account alredy exists with this email");

            if (!Pin.Equals(ConfirmPin)) throw new ArgumentException("Pins do not match", "Pin");

            byte[] pinHash, pinSalt;
            CreatePinHash(Pin, out pinHash, out pinSalt);

            account.PinHash = pinHash;
            account.PinSalt = pinSalt;

            _dataBaseContext.Account.Add(account);
            _dataBaseContext.SaveChanges();

            return account;
        }
        private static void CreatePinHash(string pin, out byte[] pinHash, out byte[] pinSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                pinSalt = hmac.Key;
                pinHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(pin));

            }
        }

        public void Delete(int Id)
        {
            var account = _dataBaseContext.Account.Find(Id);
            if (account != null)
            {
                _dataBaseContext.Account.Remove(account);
                _dataBaseContext.SaveChanges();
            }
        }

        public IEnumerable<Account> GetAccount()
        {
            return _dataBaseContext.Account.ToList();
        }

        public Account GetByAccountNumber(string AccountNumber)
        {
            var account = _dataBaseContext.Account.Where(x => x.AccountNumberGenerated == AccountNumber).FirstOrDefault();

            return account;
        }

        public Account GetById(int Id)
        {
            var account = _dataBaseContext.Account.Where(x => x.Id == Id).FirstOrDefault();

            return account;
        }

        public void Update(Account account, string Pin = null)
        {
            var accountToBeUpdated = _dataBaseContext.Account.Where(x => x.Email == account.Email).FirstOrDefault();
            if (accountToBeUpdated == null) throw new ApplicationException("Account does not exist");

            if (!string.IsNullOrWhiteSpace(account.Email))
            {
                if (_dataBaseContext.Account.Any(x => x.Email == account.Email)) throw new ApplicationException("This Email" + account.Email + " already exists ");

                accountToBeUpdated.Email = account.Email;
            }

            if (!string.IsNullOrWhiteSpace(account.PhoneNumber))
            {
                if (_dataBaseContext.Account.Any(x => x.PhoneNumber == account.PhoneNumber)) throw new ApplicationException("This PhoneNumber" + account.PhoneNumber + " already exists ");

                accountToBeUpdated.PhoneNumber = account.PhoneNumber;
            }

            if (!string.IsNullOrWhiteSpace(Pin))
            {
                byte[] pinHash, pinSalt;
                CreatePinHash(Pin, out pinHash, out pinSalt);

                accountToBeUpdated.PinHash = pinHash;
                accountToBeUpdated.PinSalt = pinSalt;
                accountToBeUpdated.DateLastUpdated = DateTime.Now;
            }

            _dataBaseContext.Account.Update(accountToBeUpdated);
            _dataBaseContext.SaveChanges();

        }
    }
}
