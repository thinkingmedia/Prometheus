﻿using System.Diagnostics;
using Prometheus.Nodes.Types.Bases;

namespace Prometheus.Nodes.Types
{
    /// <summary>
    /// Holds the name of a reference, such as a variable name.
    /// </summary>
    [DebuggerDisplay("{Name}")]
    public class IdentifierType : DataType
    {
        /// <summary>
        /// the display name
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Constructor
        /// </summary>
        public IdentifierType(string pName)
        {
            Name = pName.ToLower();
        }

        /// <summary>
        /// The identifier name
        /// </summary>
        public override string ToString()
        {
            return Name;
        }
    }
}