//-------------------------------------------------------------------------------
// <copyright file="HandlerRegistryExtensions.cs" company="MMS AG">
//   Copyright (c) MMS AG, 2008-2015
// </copyright>
//-------------------------------------------------------------------------------

namespace MMS.ServiceBus.Pipeline
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("ReSharper", "UnusedParameter.Global", Justification = "The registry parameter is only used to extend a specific type")]
    public static class HandlerRegistryExtensions
    {
        public static IReadOnlyCollection<object> ConsumeAll(this IHandlerRegistry registry)
        {
            return new ReadOnlyCollection<object>(new List<object>());
        }

        public static IReadOnlyCollection<object> RejectAll(this IHandlerRegistry registry)
        {
            return new ReadOnlyCollection<object>(new List<object> { new AlwaysRejectMessageHandler() });
        } 
    }
}