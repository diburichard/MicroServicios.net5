using Aforo255.Cross.Http.Src;
using Microsoft.Extensions.Configuration;
using MS.AFORO255.Deposit.DTOs;
using MS.AFORO255.Deposit.Models;
using Polly;
using Polly.CircuitBreaker;
using System;
using System.Threading.Tasks;

namespace MS.AFORO255.Deposit.Services
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly ITransactionService _transactionService;
        private readonly IHttpClient _httpClient;

        public AccountService(IConfiguration configuration, ITransactionService transactionService, 
            IHttpClient httpClient)
        {
            _configuration = configuration;
            _transactionService = transactionService;
            _httpClient = httpClient;
        }

        public async Task<bool> DepositAccount(AccountRequest request)
        {
            string uri = _configuration["proxy:urlAccountDeposit"];
            var response = await _httpClient.PostAsync(uri, request);
            response.EnsureSuccessStatusCode();
            return true;
        }

        public bool DepositReverse(Transaction request)
        {            
            _transactionService.DepositReverse(request);
            return true;
        }

        public bool Execute(Transaction request)
        {
            bool response = false;

            var circuitBreakerPolicy = Policy.Handle<Exception>().
                CircuitBreaker(3, TimeSpan.FromSeconds(15));

            var retry = Policy.Handle<Exception>()
                    .WaitAndRetryForever(attemp => TimeSpan.FromSeconds(15))
                    .Wrap(circuitBreakerPolicy);

            retry.Execute(() =>
            {
                if (circuitBreakerPolicy.CircuitState == CircuitState.Closed)
                {
                    circuitBreakerPolicy.Execute(() =>
                    {
                        AccountRequest account = new AccountRequest()
                        {
                            Amount = request.Amount,
                            IdAccount = request.AccountId
                        };
                        response = DepositAccount(account).Result;
                    });
                }

                if (circuitBreakerPolicy.CircuitState != CircuitState.Closed)
                {
                    Transaction transaction = new Transaction()
                    {
                        AccountId = request.AccountId,
                        Amount = request.Amount,
                        CreationDate = DateTime.Now.ToShortDateString(),
                        Type = "Deposit Reverse"
                    };
                    DepositReverse(transaction);
                    response = false;
                }
            });

            return response;

        }
    }
}
