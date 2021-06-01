using Microsoft.AspNetCore.Mvc;
using MS.AFORO255.Account.DTOs;
using MS.AFORO255.Account.Service;
using System.Linq;

namespace MS.AFORO255.Account.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_accountService.GetAll());
        }

        [HttpPost("Deposit")]
        public IActionResult Deposit([FromBody] AccountRequest request)
        {
            var result = _accountService.GetAll().Where(x => x.IdAccount == request.IdAccount).FirstOrDefault();
            Models.Account account = new Models.Account()
            {
                IdAccount = request.IdAccount,
                IdCustomer = result.IdCustomer,
                TotalAmount = result.TotalAmount + request.Amount,
                Customer = result.Customer
            };
            _accountService.Deposit(account);
            return Ok();
        }

        [HttpPost("Withdrawal")]
        public IActionResult Withdrawal([FromBody] AccountRequest request)
        {
            var result = _accountService.GetAll().Where(x => x.IdAccount == request.IdAccount).FirstOrDefault();
            if (result.TotalAmount < request.Amount)
            {
                return BadRequest(new { message = "The indicated amount cannot be withdrawal" });
            }
            Models.Account account = new Models.Account()
            {
                IdAccount = request.IdAccount,
                IdCustomer = result.IdCustomer,
                TotalAmount = result.TotalAmount - request.Amount,
                Customer = result.Customer
            };
            _accountService.Withdrawal(account);
            return Ok();
        }
    }
}
