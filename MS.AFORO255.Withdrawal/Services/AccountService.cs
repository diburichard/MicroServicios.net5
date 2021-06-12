using Aforo255.Cross.Http.Src;
using Microsoft.Extensions.Configuration;
using MS.AFORO255.Withdrawal.DTOs;
using MS.AFORO255.Withdrawal.Models;
using Polly;
using Polly.CircuitBreaker;
using System;
using System.Threading.Tasks;

namespace MS.AFORO255.Withdrawal.Services
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

        public async Task<bool> WithdrawalAccount(AccountRequest request)
        {
            string uri = _configuration["proxy:urlAccountWithdrawal"];
            var response = await _httpClient.PostAsync(uri, request);
            response.EnsureSuccessStatusCode();
            return true;
        }

        public bool WithdrawalReverse(Transaction request)
        {            
            _transactionService.WithdrawalReverse(request);
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
                        response = WithdrawalAccount(account).Result;
                    });
                }

                if (circuitBreakerPolicy.CircuitState != CircuitState.Closed)
                {
                    Transaction transaction = new Transaction()
                    {
                        AccountId = request.AccountId,
                        Amount = request.Amount,
                        CreationDate = DateTime.Now.ToShortDateString(),
                        Type = "Withdrawal Reverse"
                    };
                    WithdrawalReverse(transaction);
                    response = false;
                }
            });

            return response;

        }
    }
}
