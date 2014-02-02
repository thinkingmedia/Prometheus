﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Prometheus.Exceptions.Parser;
using Prometheus.Nodes;

namespace Prometheus.Parser
{
    /// <summary>
    /// Executing blocks place their variables into this class
    /// to limit their scope.
    /// </summary>
    public class VariableScope : IDisposable
    {
        /// <summary>
        /// The parser's cursor
        /// </summary>
        private Cursor _cursor;

        /// <summary>
        /// The parent scope
        /// </summary>
        private VariableScope _parent;

        /// <summary>
        /// Storage of variable values.
        /// </summary>
        private readonly Dictionary<string, Data> _variables;

        /// <summary>
        /// Constructor
        /// </summary>
        public VariableScope(Cursor pCursor)
        {
            _cursor = pCursor;

            _parent = _cursor.Scope;

            _variables = new Dictionary<string, Data>();
        }

        /// <summary>
        /// Looks for the identifier in the current scope, and
        /// all parent scopes.
        /// </summary>
        /// <param name="pIdentifier">The identifier to find</param>
        /// <returns>The data object or Null if not found.</returns>
        public Data Get(string pIdentifier)
        {
            if (_variables.ContainsKey(pIdentifier))
            {
                return _variables[pIdentifier];
            }
            if (_parent != null)
            {
                return _parent.Get(pIdentifier);
            }
            throw new IdentifierException(Properties.Errors.IdentifierNotDefined, pIdentifier);
        }

        /// <summary>
        /// Looks for the identifier in the current scope, and
        /// all parent scopes.
        /// </summary>
        /// <param name="pIdentifier">The identifier to find</param>
        /// <param name="pData">The data to assign to the identifier</param>
        public void Set(string pIdentifier, Data pData)
        {
            if (_variables.ContainsKey(pIdentifier))
            {
                _variables[pIdentifier] = pData;
                return;
            }

            if (_parent == null)
            {
                throw new IdentifierException(Properties.Errors.IdentifierNotDefined, pIdentifier);
            }

            _parent.Set(pIdentifier, pData);
        }

        /// <summary>
        /// Creates a new variable in the current scope.
        /// </summary>
        /// <param name="pIdentifier">The identifier to create</param>
        /// <param name="pData">The data to assign</param>
        public void Create(string pIdentifier, Data pData)
        {
            // only check the current scope
            if (_variables.ContainsKey(pIdentifier))
            {
                throw new IdentifierException(Properties.Errors.IdentifierAlreadyDefined, pIdentifier);
            }
            _variables.Add(pIdentifier, pData);
        }

        /// <summary>
        /// Prints a list of all variables.
        /// </summary>
        public void Print(int pIndent = 0)
        {
            if (_parent != null)
            {
                _parent.Print(pIndent+1);
            }
            string indent = string.Format("{0}> ", " ".PadLeft(pIndent));
            foreach (KeyValuePair<string, Data> var in _variables)
            {
                if (var.Value.Type == typeof(string))
                {
                    Debug.WriteLine("{0}{1} = \"{2}\"", indent, var.Key, var.Value.Get<string>());
                    continue;
                }
                Debug.WriteLine("{0}{1} = {2}", indent, var.Key, var.Value.Get<string>() ?? "undefined");
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _cursor.Scope = _parent;

            _cursor = null;
            _parent = null;

            _variables.Clear();
        }
    }
}