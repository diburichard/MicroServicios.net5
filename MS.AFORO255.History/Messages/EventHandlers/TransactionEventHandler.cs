using Aforo255.Cross.Event.Src.Bus;
using MS.AFORO255.History.Messages.Events;
using MS.AFORO255.History.Models;
using MS.AFORO255.History.Services;
using System.Threading.Tasks;

namespace MS.AFORO255.History.Messages.EventHandlers
{
    public class TransactionEventHandler : IEventHandler<TransactionCreatedEvent>
    {
        private readonly IHistoryService _historyService;

        public TransactionEventHandler(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        public Task Handle(TransactionCreatedEvent @event)
        {
            _historyService.Add(new HistoryTransaction()
            {
                IdTransaction = @event.IdTransaction,
                Amount = @event.Amount,
                Type = @event.Type,
                CreationDate = @event.CreationDate,
                AccountId = @event.AccountId

            });
            return Task.CompletedTask;

        }
    }
}
