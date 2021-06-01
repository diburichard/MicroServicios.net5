using System.Collections.Generic;

namespace MS.AFORO255.Account.Service
{
    public interface IAccountService
    {
        IEnumerable<Models.Account> GetAll();
        bool Deposit(Models.Account account);
        bool Withdrawal(Models.Account account);
    }
}
