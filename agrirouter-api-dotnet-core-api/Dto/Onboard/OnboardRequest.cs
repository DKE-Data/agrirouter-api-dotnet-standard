using Newtonsoft.Json;

namespace Agrirouter.Api.Dto.Onboard
{
    /// <summary>
    /// Data transfer object for the communication.
    /// </summary>
    public class OnboardRequest
    {
        [JsonProperty(PropertyName = "id")] public string Id { get; set; }

        [JsonProperty(PropertyName = "applicationId")]
        public string ApplicationId { get; set; }

        [JsonProperty(PropertyName = "certificationVersionId")]
        public string CertificationVersionId { get; set; }

        [JsonProperty(PropertyName = "gatewayId")]
        public string GatewayId { get; set; }

        [JsonProperty(PropertyName = "UTCTimestamp")]
        public string UtcTimestamp { get; set; }

        [JsonProperty(PropertyName = "timeZone")]
        public string TimeZone { get; set; }

        [JsonProperty(PropertyName = "certificateType")]
        public string CertificateType { get; set; }
    }
}