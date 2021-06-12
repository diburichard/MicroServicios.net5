using Aforo255.Cross.Event.Src.Events;

namespace MS.AFORO255.Deposit.Messages.Events
{
    public class NotificationCreatedEvent : Event
    {
        public NotificationCreatedEvent(int idTransaction, decimal amount, string type,
            string messageBody, string address, int accountId)
        {
            IdTransaction = idTransaction;
            Amount = amount;
            Type = type;
            MessageBody = messageBody;
            Address = address;
            AccountId = accountId;
        }

        public int IdTransaction { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string MessageBody { get; protected set; }
        public string Address { get; protected set; }
        public int AccountId { get; set; }

    }
}
