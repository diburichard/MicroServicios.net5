using Microsoft.AspNetCore.Mvc;
using MS.AFORO255.Withdrawal.DTOs;
using MS.AFORO255.Withdrawal.Services;
using System;

namespace MS.AFORO255.Withdrawal.Controllers
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

        [HttpPost("Withdrawal")]
        public IActionResult Withdrawal([FromBody] TransactionRequest request)
        {
            Models.Transaction transaction = new Models.Transaction()
            {
                AccountId = request.AccountId,
                Amount = request.Amount,
                CreationDate = DateTime.Now.ToShortDateString(),
                Type = "Withdrawal"
            };
            transaction = _transactionService.Withdrawal(transaction);

            return Ok(transaction);
        }
    }
}
