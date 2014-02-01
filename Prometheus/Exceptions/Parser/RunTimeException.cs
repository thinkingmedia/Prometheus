﻿using System;
using Prometheus.Nodes;

namespace Prometheus.Exceptions.Parser
{
    /// <summary>
    /// Base exception for all run time errors.
    /// </summary>
    public class RunTimeException : PrometheusException
    {
        /// <summary>
        /// Formatting
        /// </summary>
        private static string Message(string pMessage, Node pNode)
        {
            return string.Format("{0} {1}", pMessage, pNode.Location);
        }

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
        /// <param name="pNode">The node the error relates to</param>
        protected RunTimeException(string pMessage, Node pNode)
            : base(Message(pMessage, pNode))
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
    }
}