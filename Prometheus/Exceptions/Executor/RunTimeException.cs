﻿using System;
using Prometheus.Compile;
using Prometheus.Nodes;

namespace Prometheus.Exceptions.Executor
{
    /// <summary>
    /// Base exception for all run time errors.
    /// </summary>
    public class RunTimeException : PrometheusException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        protected RunTimeException()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pMessage">Exception message</param>
        public RunTimeException(string pMessage)
            : base(pMessage)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pMessage">Exception message</param>
        /// <param name="pNode">The node the error relates to</param>
        protected RunTimeException(string pMessage, Node pNode)
            : base(pMessage, pNode.Location)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pMessage">Exception message</param>
        /// <param name="pInner">Inner exception</param>
        protected RunTimeException(string pMessage, Exception pInner)
            : base(pMessage, pInner)
        {
        }

        /// <summary>
        /// Wraps around another exception.
        /// </summary>
        public RunTimeException(string pMessage, Location pLocation, Exception pInnerException)
            : base(pMessage, pLocation, pInnerException)
        {
        }
    }
}