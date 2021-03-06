//-------------------------------------------------------------------------------
// <copyright file="DeliveryOptions.cs" company="MMS AG">
//   Copyright (c) MMS AG, 2008-2015
// </copyright>
//-------------------------------------------------------------------------------

namespace MMS.ServiceBus
{
    using System.Collections.Generic;

    public abstract class DeliveryOptions
    {
        protected DeliveryOptions()
        {
            this.Headers = new Dictionary<string, string>();
        }

        public IDictionary<string, string> Headers { get; private set; }

        public Queue ReplyToAddress { get; set; }
    }
}