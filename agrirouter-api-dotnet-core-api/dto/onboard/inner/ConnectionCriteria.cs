using System;

namespace com.dke.data.agrirouter.api.dto.onboard.inner
{
    public class ConnectionCriteria
    {
        public String GatewayId { get; set; }

        public String Measures { get; set; }

        public String Commands { get; set; }

        public String Host { get; set; }

        public String Port { get; set; }

        public String ClientId { get; set; }
    }
}