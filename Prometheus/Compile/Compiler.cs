﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using GOLD;
using Logging;
using Prometheus.Compile.Optomizer;
using Prometheus.Compile.Packaging;
using Prometheus.Exceptions;
using Prometheus.Exceptions.Compiler;
using Prometheus.Grammar;
using Prometheus.Nodes;
using Prometheus.Packages;
using Prometheus.Properties;

namespace Prometheus.Compile
{
    /// <summary>
    /// Handles the execution of the source code file.
    /// </summary>
    public class Compiler
    {
        /// <summary>
        /// Logging
        /// </summary>
        private static readonly Logger _logger = Logger.Create(typeof (Compiler));

        /// <summary>
        /// Used to create nodes.
        /// </summary>
        private readonly NodeFactory _factory;

        /// <summary>
        /// The GOLD parser.
        /// </summary>
        private readonly GOLD.Parser _parser;

        /// <summary>
        /// Calculates the command tree from the source code and returns the root node.
        /// </summary>
        /// <param name="pFileName">The name of the source file.</param>
        /// <param name="pSourceCode">The source code to compile.</param>
        public Node Compile(string pFileName, string pSourceCode)
        {
            //This procedure starts the GOLD Parser Engine and handles each of the
            //messages it returns. Each time a reduction is made, you can create new
            //custom object and reassign the .CurrentReduction property. Otherwise, 
            //the system will use the Reduction object that was returned.
            //
            //The resulting tree will be a pure representation of the language 
            //and will be ready to implement.

            string sourceCode = pSourceCode + "\n";
            string[] lines = sourceCode.Split(new[] { '\n' });

            _parser.Open(ref sourceCode);
            _parser.TrimReductions = true;

            for (ParseMessage response = _parser.Parse(); response != ParseMessage.Accept; response = _parser.Parse())
            {
                int x = _parser.CurrentPosition().Line + 1;
                int y = _parser.CurrentPosition().Column + 1;
                Location location = new Location(pFileName, lines[x - 1].Trim(), x, y);

                try
                {
                    if (!CreateNode(response, location))
                    {
                        continue;
                    }
                    Reduction reduction = _parser.CurrentReduction as Reduction;
                    _parser.CurrentReduction = (reduction != null)
                        ? _factory.Create(reduction, location)
                        : _parser.CurrentReduction;
                }
                catch (PrometheusException e)
                {
                    e.Where = e.Where ?? location;
                    throw;
                }
            }

            Node node = _parser.CurrentReduction as Node;
            if (node == null)
            {
                throw new InternalErrorException(Errors.ProgramMissing, Location.None);
            }

            // always make Program the root node
            if (node.Type == GrammarSymbol.Program)
            {
                return node;
            }

            Node root = new Node(GrammarSymbol.Program, node.Location);
            root.Children.Add(node);
            return root;
        }

        /// <summary>
        /// Checks if the current response from the parser is to
        /// create a new node.
        /// </summary>
        /// <param name="pResponse">The current response</param>
        /// <param name="pLocation">Location</param>
        /// <returns>True if a reduction</returns>
        private bool CreateNode(ParseMessage pResponse, Location pLocation)
        {
            switch (pResponse)
            {
                case ParseMessage.LexicalError:
                    throw new LexicalException(_parser, pLocation);

                case ParseMessage.SyntaxError:
                    throw new SyntaxException(_parser, pLocation);

                case ParseMessage.InternalError:
                    throw new InternalErrorException(pLocation);

                case ParseMessage.NotLoadedError:
                    throw new NotLoadedException(pLocation);

                case ParseMessage.GroupError:
                    throw new EofException(pLocation);
            }

            return pResponse == ParseMessage.Reduction;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Compiler()
        {
            _parser = new GOLD.Parser();

            _factory = new NodeFactory();

            const string fullResourceName = "Prometheus.Grammar.Grammar.egt";
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fullResourceName))
            {
                if (stream == null)
                {
                    throw new CompilerException(string.Format(Errors.MissingResource, fullResourceName), Location.None);
                }

                using (BinaryReader reader = new BinaryReader(stream))
                {
                    _parser.LoadTables(reader);
                }
            }
        }
    }
}