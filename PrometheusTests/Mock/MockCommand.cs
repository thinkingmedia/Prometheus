﻿using System.Diagnostics;
using Prometheus.Nodes.Types;
using Prometheus.Nodes.Types.Bases;
using Prometheus.Parser.Executors;

namespace PrometheusTest.Mock
{
    public class MockCommand : ExecutorGrammar
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MockCommand(Executor pExecutor)
            : base(pExecutor)
        {
        }

        /// <summary>
        /// Prints a string to the output.
        /// </summary>
        /// <param name="pValue">The message to print.</param>
        //[ExecuteSymbol(GrammarSymbol.PrintProc)]
        public DataType Print(DataType pValue)
        {
            Debug.WriteLine(pValue.ToString());

            return UndefinedType.Undefined;
        }
    }
}