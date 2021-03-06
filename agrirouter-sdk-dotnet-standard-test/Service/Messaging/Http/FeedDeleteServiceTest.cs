using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using Agrirouter.Feed.Request;
using Agrirouter.Response;
using Agrirouter.Api.Definitions;
using Agrirouter.Api.Dto.Onboard;
using Agrirouter.Api.Service.Parameters;
using Agrirouter.Impl.Service.Common;
using Agrirouter.Impl.Service.Messaging;
using Agrirouter.Test.Data;
using Agrirouter.Test.Helper;
using Xunit;

namespace Agrirouter.Test.Service.Messaging.Http
{
    /// <summary>
    ///     Functional tests.
    /// </summary>
    [Collection("Integrationtest")]
    public class FeedDeleteServiceTest
    {
        private static readonly HttpClient HttpClient = HttpClientFactory.AuthenticatedHttpClient(OnboardResponse);

        private static OnboardResponse OnboardResponse =>
            OnboardResponseIntegrationService.Read(Identifier.Http.CommunicationUnit.SingleEndpointWithoutRoute);

        [Fact]
        public void
            GivenExistingEndpointsWhenFeedDeleteWithoutParametersWhenPerformingQueryThenTheMessageShouldNotBeOkBecauseTheMessageIdsAreMissing()
        {
            var feedDeleteService = new FeedDeleteService(new HttpMessagingService(HttpClient));
            var feedDeleteParameters = new FeedDeleteParameters
            {
                OnboardResponse = OnboardResponse
            };
            feedDeleteService.Send(feedDeleteParameters);

            Thread.Sleep(TimeSpan.FromSeconds(5));

            var fetchMessageService = new FetchMessageService(HttpClient);
            var fetch = fetchMessageService.Fetch(OnboardResponse);
            Assert.Single(fetch);

            var decodedMessage = DecodeMessageService.Decode(fetch[0].Command.Message);
            Assert.Equal(400, decodedMessage.ResponseEnvelope.ResponseCode);

            var messages = DecodeMessageService.Decode(decodedMessage.ResponsePayloadWrapper.Details);
            Assert.NotNull(messages);
            Assert.NotEmpty(messages.Messages_);
            Assert.Single(messages.Messages_);
            Assert.Equal("VAL_000017", messages.Messages_[0].MessageCode);
            Assert.Equal(
                "messageIds information required to process message is missing or malformed.",
                messages.Messages_[0].Message_);
        }

        [Fact]
        public void GivenExistingEndpointsWhenFeedDeleteWithUnknownMessageIdsMessageIdsThenTheResultShouldBeOk()
        {
            var feedDeleteService = new FeedDeleteService(new HttpMessagingService(HttpClient));
            var feedDeleteParameters = new FeedDeleteParameters
            {
                OnboardResponse = OnboardResponse,
                MessageIds = new List<string> {Guid.NewGuid().ToString()}
            };
            feedDeleteService.Send(feedDeleteParameters);

            Thread.Sleep(TimeSpan.FromSeconds(5));

            var fetchMessageService = new FetchMessageService(HttpClient);
            var fetch = fetchMessageService.Fetch(OnboardResponse);
            Assert.Single(fetch);

            var decodedMessage = DecodeMessageService.Decode(fetch[0].Command.Message);
            Assert.Equal(200, decodedMessage.ResponseEnvelope.ResponseCode);
            Assert.Equal(ResponseEnvelope.Types.ResponseBodyType.AckWithMessages,
                decodedMessage.ResponseEnvelope.Type);

            var messages = DecodeMessageService.Decode(decodedMessage.ResponsePayloadWrapper.Details);
            Assert.NotNull(messages);
            Assert.NotEmpty(messages.Messages_);
            Assert.Single(messages.Messages_);
            Assert.Equal("VAL_000205", messages.Messages_[0].MessageCode);
            Assert.Equal(
                "Feed message cannot be found.",
                messages.Messages_[0].Message_);
        }

        [Fact]
        public void
            GivenExistingEndpointsWhenFeedDeleteWithUnknownMessageIdsSenderIdsThenTheResultShouldNotBeOkBecauseTheMessageIdsAreMissing()
        {
            var feedDeleteService = new FeedDeleteService(new HttpMessagingService(HttpClient));
            var feedDeleteParameters = new FeedDeleteParameters
            {
                OnboardResponse = OnboardResponse,
                Senders = new List<string> {Guid.NewGuid().ToString()}
            };
            feedDeleteService.Send(feedDeleteParameters);

            Thread.Sleep(TimeSpan.FromSeconds(5));

            var fetchMessageService = new FetchMessageService(HttpClient);
            var fetch = fetchMessageService.Fetch(OnboardResponse);
            Assert.Single(fetch);

            var decodedMessage = DecodeMessageService.Decode(fetch[0].Command.Message);
            Assert.Equal(400, decodedMessage.ResponseEnvelope.ResponseCode);
            Assert.Equal(ResponseEnvelope.Types.ResponseBodyType.AckWithFailure,
                decodedMessage.ResponseEnvelope.Type);

            var messages = DecodeMessageService.Decode(decodedMessage.ResponsePayloadWrapper.Details);
            Assert.NotNull(messages);
            Assert.NotEmpty(messages.Messages_);
            Assert.Single(messages.Messages_);
            Assert.Equal("VAL_000017", messages.Messages_[0].MessageCode);
            Assert.Equal(
                "messageIds information required to process message is missing or malformed.",
                messages.Messages_[0].Message_);
        }

        [Fact]
        public void
            GivenExistingEndpointsWhenFeedDeleteWithValidityPeriodThenTheResultShouldNotBeOkBecauseTheMessageIdsAreMissing()
        {
            var feedDeleteService = new FeedDeleteService(new HttpMessagingService(HttpClient));
            var feedDeleteParameters = new FeedDeleteParameters
            {
                OnboardResponse = OnboardResponse,
                ValidityPeriod = new ValidityPeriod()
            };
            feedDeleteParameters.ValidityPeriod.SentTo = UtcDataService.Timestamp(TimestampOffset.None);
            feedDeleteParameters.ValidityPeriod.SentTo = UtcDataService.Timestamp(TimestampOffset.FourWeeks);
            feedDeleteService.Send(feedDeleteParameters);

            Thread.Sleep(TimeSpan.FromSeconds(5));

            var fetchMessageService = new FetchMessageService(HttpClient);
            var fetch = fetchMessageService.Fetch(OnboardResponse);
            Assert.Single(fetch);

            var decodedMessage = DecodeMessageService.Decode(fetch[0].Command.Message);
            Assert.Equal(400, decodedMessage.ResponseEnvelope.ResponseCode);
            Assert.Equal(ResponseEnvelope.Types.ResponseBodyType.AckWithFailure,
                decodedMessage.ResponseEnvelope.Type);

            var messages = DecodeMessageService.Decode(decodedMessage.ResponsePayloadWrapper.Details);
            Assert.NotNull(messages);
            Assert.NotEmpty(messages.Messages_);
            Assert.Single(messages.Messages_);
            Assert.Equal("VAL_000017", messages.Messages_[0].MessageCode);
            Assert.Equal(
                "messageIds information required to process message is missing or malformed.",
                messages.Messages_[0].Message_);
        }
    }
}