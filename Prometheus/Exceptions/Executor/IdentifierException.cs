﻿using Prometheus.Nodes;

namespace Prometheus.Exceptions.Executor
{
    /// <summary>
    /// A rethrowable exception related to an identifier.
    /// </summary>
    public class IdentifierException : RunTimeException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public IdentifierException(string pMessage, Node pNode)
            : base(pMessage, pNode)
        {
        }
    }
}