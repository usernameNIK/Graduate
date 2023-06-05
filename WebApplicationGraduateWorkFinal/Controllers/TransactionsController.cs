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
    public class TransactionsController : ControllerBase
    {
        private ITransactionService _transactionService;
        IMapper _mapper;

        public TransactionsController(ITransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }

        //create new transaction
        [HttpPost("create_new_transaction")]
        public IActionResult CreateNewTransaction([FromBody] TransactionRequestDto transactionRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(transactionRequestDto);

            var transaction = _mapper.Map<Transactions>(transactionRequestDto);
            return Ok(_transactionService.CreateNewTransaction(transaction));
        }

        [HttpPost("make_deposit")]
        public IActionResult MakeDeposit(string AccountNumber, decimal Ammount, string TransactionPin)
        {
            if (!Regex.IsMatch(AccountNumber, @"^[0][1-9]\d{9}$|^[1-9]\d{9}$")) return BadRequest("AccountNumber most be 10 - digit");
            return Ok(_transactionService.MakeDeposit(AccountNumber, Ammount, TransactionPin));
        }

        [HttpPost("make_withdrawal")]
        public IActionResult MakeWithdrawal(string AccountNumber, decimal Ammount, string TransactionPin)
        {
            if (!Regex.IsMatch(AccountNumber, @"^[0][1-9]\d{9}$|^[1-9]\d{9}$")) return BadRequest("AccountNumber most be 10 - digit");
            return Ok(_transactionService.MakeWithdrawal(AccountNumber, Ammount, TransactionPin));
        }

        [HttpPost("make_funds_transaction")]
        public IActionResult MakeFundsTransfer(string FromAccount, string ToAccount, decimal Ammount, string TransactionPin)
        {
            if (!Regex.IsMatch(FromAccount, @"^[0][1-9]\d{9}$|^[1-9]\d{9}$") || !Regex.IsMatch(ToAccount, @"^[0][1-9]\d{9}$|^[1-9]\d{9}$")) return BadRequest("AccountNumber most be 10 - digit");

            return Ok(_transactionService.MakeFundsTransfer(FromAccount, ToAccount, Ammount, TransactionPin));
        }
    }
}
