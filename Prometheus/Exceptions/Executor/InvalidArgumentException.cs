﻿using System;
using Prometheus.Nodes;

namespace Prometheus.Exceptions.Executor
{
    public class InvalidArgumentException : RunTimeException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pMessage">Exception message</param>
        public InvalidArgumentException(string pMessage)
            : base(pMessage)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pMessage">Exception message</param>
        /// <param name="pNode">The node the error relates to</param>
        public InvalidArgumentException(string pMessage, Node pNode) : base(pMessage, pNode)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pMessage">Exception message</param>
        /// <param name="pInner">Inner exception</param>
        public InvalidArgumentException(string pMessage, Exception pInner) : base(pMessage, pInner)
        {
        }
    }
}