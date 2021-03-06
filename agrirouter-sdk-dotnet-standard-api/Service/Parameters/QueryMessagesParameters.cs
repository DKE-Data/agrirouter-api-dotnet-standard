using System.Collections.Generic;
using Agrirouter.Feed.Request;

namespace Agrirouter.Api.Service.Parameters
{
    /// <summary>
    ///     Parameter container definition.
    /// </summary>
    public class QueryMessagesParameters : MessageParameters
    {
        /// <summary>
        ///     Senders.
        /// </summary>
        public List<string> Senders { get; set; }

        /// <summary>
        ///     Message IDs.
        /// </summary>
        public List<string> MessageIds { get; set; }

        /// <summary>
        ///     Validity period.
        /// </summary>
        public ValidityPeriod ValidityPeriod { get; set; }
    }
}