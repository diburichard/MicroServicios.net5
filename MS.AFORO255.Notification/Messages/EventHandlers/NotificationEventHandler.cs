using Aforo255.Cross.Event.Src.Bus;
using MS.AFORO255.Deposit.Messages.Events;
using MS.AFORO255.Notification.Models;
using MS.AFORO255.Notification.Repositories;
using System;
using System.Threading.Tasks;

namespace MS.AFORO255.Notification.Messages.EventHandlers
{
    public class NotificationEventHandler : IEventHandler<NotificationCreatedEvent>
    {
        private readonly ContextDatabase _contextDatabase;

        public NotificationEventHandler(ContextDatabase contextDatabase)
        {
            _contextDatabase = contextDatabase;
        }

        public Task Handle(NotificationCreatedEvent @event)
        {
            /*Register DB*/
            SendMail sendMail = new SendMail()
            {
                SendDate = DateTime.Now.ToShortDateString(),
                Type = @event.Type,
                Message = @event.MessageBody,
                Address = @event.Address,
                AccountId = @event.AccountId
            };
            _contextDatabase.SendMail.Add(sendMail);
            _contextDatabase.SaveChanges();

            /*Send Mail*/

            return Task.CompletedTask;
        }
    }

}
