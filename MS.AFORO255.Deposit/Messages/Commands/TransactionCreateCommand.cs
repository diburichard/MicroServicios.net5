using Aforo255.Cross.Event.Src.Commands;

namespace MS.AFORO255.Deposit.Messages.Commands
{
    public class TransactionCreateCommand : Command
    {
        public TransactionCreateCommand(int idTransaction, decimal amount, string type,
            string creationDate, int accountId)
        {
            IdTransaction = idTransaction;
            Amount = amount;
            Type = type;
            CreationDate = creationDate;
            AccountId = accountId;
        }

        public int IdTransaction { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string CreationDate { get; set; }
        public int AccountId { get; set; }
    }
}
