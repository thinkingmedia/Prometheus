﻿using System;
using Prometheus.Compile;

namespace Prometheus.Exceptions.Compiler
{
    /// <summary>
    /// The base exception for all compiler time errors.
    /// </summary>
    public class CompilerException : PrometheusException
    {
        /// <summary>
        /// Formats the message.
        /// </summary>
        private static string Format(string pMessage, Cursor pCursor)
        {
            return string.Format("{0} {1}", pMessage, pCursor);
        }

        /// <summary>
        /// Wraps around another exception.
        /// </summary>
        protected CompilerException(string pMessage, Cursor pCursor, Exception pInnerException)
            : base(Format(pMessage, pCursor), pInnerException)
        {
        }

        /// <summary>
        /// Throw a message.
        /// </summary>
        public CompilerException(string pMessage, Cursor pCursor)
            : base(Format(pMessage, pCursor))
        {
        }

        /// <summary>
        /// Throw a message.
        /// </summary>
        public CompilerException(string pMessage)
            : base(pMessage)
        {
        }
    }
}