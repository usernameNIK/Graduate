using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using WebApplication123.Models;
using WebApplication123.Services.Interfaces;
using WebApplicationGraduateWorkFinal.Model;

namespace WebApplicationGraduateWorkFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountServices _accountService;
        IMapper _mapper;

        public AccountController(IAccountServices accountServices, IMapper mapper)
        {
            _accountService = accountServices;
            _mapper = mapper;
        }

        [HttpPost("register_new_account")]
        public IActionResult RegisterNewAccount([FromBody] RegisterNewAccountModel newAccount)
        {
            if (!ModelState.IsValid) return BadRequest(newAccount);

            var account = _mapper.Map<Account>(newAccount);
            return Ok(_accountService.Create(account, newAccount.Pin, newAccount.ConfirmPin));

        }

        [HttpGet("GetAllAccounts")]
        public IActionResult GetAllAccounts()
        {
            var accounts = _accountService.GetAccount();
            var cleanedAccounts = _mapper.Map<IList<GetAccountModel>>(accounts);
            return Ok(cleanedAccounts);
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            if (!ModelState.IsValid) return BadRequest(model);

            return Ok(_accountService.Authenticate(model.AccountNumber, model.Pin));
        }

        [HttpGet("GetByAccountNumber")]
        public IActionResult GetByAccountNumber(string AccountNumber)
        {
            if (!Regex.IsMatch(AccountNumber, @"^[0][1-9]\d{9}$|^[1-9]\d{9}$")) return BadRequest("AccountNumber most be 10 - digit");

            var account = _accountService.GetByAccountNumber(AccountNumber);
            var cleanedAccount = _mapper.Map<GetAccountModel>(account);
            return Ok(cleanedAccount);
        }

        [HttpGet("GetAccountById")]
        public IActionResult GetAccountById(int Id)
        {
            var account = _accountService.GetById(Id);
            var cleanedAccount = _mapper.Map<GetAccountModel>(account);
            return Ok(cleanedAccount);
        }

        [HttpPut("UpdateAccount")]
        public IActionResult UpdateAccount([FromBody] UpdateAccountModel model)
        {
            if (!ModelState.IsValid) return BadRequest(model);

            var account = _mapper.Map<Account>(model);
            _accountService.Update(account, model.Pin);
            return Ok();
        }
    }
}
