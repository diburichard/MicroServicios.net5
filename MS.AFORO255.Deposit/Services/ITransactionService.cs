using MS.AFORO255.Deposit.Models;

namespace MS.AFORO255.Deposit.Services
{
    public interface ITransactionService
    {
        Transaction Deposit(Transaction transaction);
        Transaction DepositReverse(Transaction transaction);
    }
}
