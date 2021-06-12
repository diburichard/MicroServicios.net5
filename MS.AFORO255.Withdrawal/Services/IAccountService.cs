using MS.AFORO255.Withdrawal.DTOs;
using MS.AFORO255.Withdrawal.Models;
using System.Threading.Tasks;

namespace MS.AFORO255.Withdrawal.Services
{
    public interface IAccountService
    {
        Task<bool> WithdrawalAccount(AccountRequest request);
        bool WithdrawalReverse(Transaction request);
        bool Execute(Transaction request);

    }
}
