﻿using System;
using Prometheus.Compile;

namespace Prometheus.Exceptions.Compiler
{
    public class EofException : CompilerException
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public EofException(Cursor pCursor)
            : base("Unexpected end of file.", pCursor)
        {
        }

        /// <summary>
        /// Wraps around another exception.
        /// </summary>
        public EofException(string pMessage, Cursor pCursor, Exception pInnerException)
            : base(pMessage, pCursor, pInnerException)
        {
        }

        /// <summary>
        /// Throw a message.
        /// </summary>
        public EofException(string pMessage, Cursor pCursor)
            : base(pMessage, pCursor)
        {
        }
    }
}