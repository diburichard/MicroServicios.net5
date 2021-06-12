using MS.AFORO255.Deposit.DTOs;
using MS.AFORO255.Deposit.Models;
using System.Threading.Tasks;

namespace MS.AFORO255.Deposit.Services
{
    public interface IAccountService
    {
        Task<bool> DepositAccount(AccountRequest request);
        bool DepositReverse(Transaction request);
        bool Execute(Transaction request);

    }
}
