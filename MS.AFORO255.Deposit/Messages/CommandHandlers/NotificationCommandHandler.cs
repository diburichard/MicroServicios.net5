using Aforo255.Cross.Event.Src.Bus;
using MediatR;
using MS.AFORO255.Deposit.Messages.Commands;
using MS.AFORO255.Deposit.Messages.Events;
using System.Threading;
using System.Threading.Tasks;

namespace MS.AFORO255.Deposit.Messages.CommandHandlers
{
    public class NotificationCommandHandler : IRequestHandler<NotificationCreateCommand, bool>
    {
        private readonly IEventBus _bus;

        public NotificationCommandHandler(IEventBus bus)
        {
            _bus = bus;
        }

        public Task<bool> Handle(NotificationCreateCommand request, CancellationToken cancellationToken)
        {
            _bus.Publish(new NotificationCreatedEvent(
                    request.IdTransaction,
                    request.Amount,
                    request.Type,
                    request.MessageBody,
                    request.Address,
                    request.AccountId
                ));
            return Task.FromResult(true);
        }
    }

}
