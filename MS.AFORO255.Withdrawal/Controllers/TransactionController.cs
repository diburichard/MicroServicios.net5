using Aforo255.Cross.Event.Src.Bus;
using Microsoft.AspNetCore.Mvc;
using MS.AFORO255.Withdrawal.DTOs;
using MS.AFORO255.Withdrawal.Messages.Commands;
using MS.AFORO255.Withdrawal.Services;
using System;

namespace MS.AFORO255.Withdrawal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IEventBus _bus;
        private readonly ITransactionService _transactionService;
        private readonly IAccountService _accountService;

        public TransactionController(IEventBus bus, ITransactionService transactionService,
            IAccountService accountService)
        {
            _bus = bus;
            _transactionService = transactionService;
            _accountService = accountService;
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
            bool isProccess = _accountService.Execute(transaction);
            if (isProccess)
            {

                var transactionCreateCommand = new TransactionCreateCommand(
               idTransaction: transaction.Id,
               amount: transaction.Amount,
               type: transaction.Type,
               creationDate: transaction.CreationDate,
               accountId: transaction.AccountId
            );
                _bus.SendCommand(transactionCreateCommand);


                var notificationCreateCommand = new NotificationCreateCommand(
                    idTransaction: transaction.Id,
                    amount: transaction.Amount,
                    type: transaction.Type,
                    messageBody: $"Se proceso el {transaction.Type} con el monto de {transaction.Amount} de su cuenta {transaction.AccountId}",
                    address: "icuadros@aforo255.com",
                    accountId: transaction.AccountId
                 );
                _bus.SendCommand(notificationCreateCommand);
            }
            return Ok(transaction);
        }
    }
}
