using Agrirouter.Request;
using com.dke.data.agrirouter.impl.service.common;
using com.dke.data.agrirouter.impl.service.messaging.abstraction;

namespace com.dke.data.agrirouter.impl.service.messaging
{
    /// <summary>
    /// Service to publish messages.
    /// </summary>
    public class PublishMessageService : SendMessageBaseService
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="messagingService">-</param>
        /// <param name="encodeMessageService">-</param>
        public PublishMessageService(MessagingService messagingService, EncodeMessageService encodeMessageService) : base(messagingService, encodeMessageService)
        {
        }

        protected override RequestEnvelope.Types.Mode Mode => RequestEnvelope.Types.Mode.Publish;
    }
}