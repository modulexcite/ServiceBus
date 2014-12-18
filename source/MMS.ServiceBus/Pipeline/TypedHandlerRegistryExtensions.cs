//-------------------------------------------------------------------------------
// <copyright file="TypedHandlerRegistryExtensions.cs" company="MMS AG">
//   Copyright (c) MMS AG, 2008-2015
// </copyright>
//-------------------------------------------------------------------------------

namespace MMS.ServiceBus.Pipeline
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    [SuppressMessage("ReSharper", "UnusedParameter.Global", Justification = "The registry parameter is only used to extend a specific type")]
    public static class TypedHandlerRegistryExtensions
    {
        public static IReadOnlyCollection<IHandleMessageAsync<TMessage>> ConsumeWith<TMessage>(this IHandlerRegistry registry, params IHandleMessageAsync<TMessage>[] handlers)
        {
            return new ReadOnlyCollection<IHandleMessageAsync<TMessage>>(handlers.ToList());
        }
    }
}