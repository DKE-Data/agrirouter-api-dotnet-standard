using Agrirouter.Request;
using com.dke.data.agrirouter.impl.service.common;
using com.dke.data.agrirouter.impl.service.messaging.abstraction;

namespace com.dke.data.agrirouter.impl.service.messaging
{
    public class PublishAndSendMultipleDirectMessagesService : SendMultipleMessagesBaseService
    {
        public PublishAndSendMultipleDirectMessagesService(MessagingService messagingService, EncodeMessageService encodeMessageService) : base(messagingService, encodeMessageService)
        {
        }

        protected override RequestEnvelope.Types.Mode Mode => RequestEnvelope.Types.Mode.PublishWithDirect;
    }
}