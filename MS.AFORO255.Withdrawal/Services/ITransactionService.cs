using MS.AFORO255.Withdrawal.Models;

namespace MS.AFORO255.Withdrawal.Services
{
    public interface ITransactionService
    {
        Transaction Withdrawal(Transaction transaction);
        Transaction WithdrawalReverse(Transaction transaction);
    }
}
