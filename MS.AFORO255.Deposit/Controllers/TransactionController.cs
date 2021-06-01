using Microsoft.AspNetCore.Mvc;
using MS.AFORO255.Deposit.DTOs;
using MS.AFORO255.Deposit.Services;
using System;

namespace MS.AFORO255.Deposit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionController( ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("Deposit")]
        public IActionResult Deposit([FromBody] TransactionRequest request)
        {
            Models.Transaction transaction = new Models.Transaction()
            {
                AccountId = request.AccountId,
                Amount = request.Amount,
                CreationDate = DateTime.Now.ToShortDateString(),
                Type = "Deposit"
            };
            transaction = _transactionService.Deposit(transaction);

            return Ok(transaction);
        }
    }
}
