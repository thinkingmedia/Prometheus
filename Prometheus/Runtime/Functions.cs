﻿using System.Collections.Generic;
using Prometheus.Exceptions.Executor;
using Prometheus.Grammar;
using Prometheus.Nodes.Types;
using Prometheus.Objects;
using Prometheus.Parser.Executors;
using Prometheus.Parser.Executors.Attributes;

namespace Prometheus.Runtime
{
    /// <summary>
    /// </summary>
    public class Functions : ExecutorGrammar
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Functions(Executor pExecutor)
            : base(pExecutor)
        {
        }

        /// <summary>
        /// Handles passing arguments to the call method.
        /// </summary>
        [ExecuteSymbol(GrammarSymbol.ArgumentList)]
        public Data Arguments()
        {
            return Data.Undefined;
        }

        /// <summary>
        /// Handles passing arguments to the call method.
        /// </summary>
        [ExecuteSymbol(GrammarSymbol.ArgumentList)]
        public Data Arguments(Data pArg1)
        {
            return new Data(new ArgumentList {pArg1});
        }

        /// <summary>
        /// Handles passing arguments to the call method.
        /// </summary>
        [ExecuteSymbol(GrammarSymbol.ArgumentList)]
        public Data Arguments(Data pArg1, Data pArg2)
        {
            return new Data(new ArgumentList {pArg1, pArg2});
        }

        /// <summary>
        /// Handles passing arguments to the call method.
        /// </summary>
        [ExecuteSymbol(GrammarSymbol.ArgumentList)]
        public Data Arguments(Data pArg1, Data pArg2, Data pArg3)
        {
            return new Data(new ArgumentList {pArg1, pArg2, pArg3});
        }

        /// <summary>
        /// Handles passing arguments to the call method.
        /// </summary>
        [ExecuteSymbol(GrammarSymbol.ArgumentList)]
        public Data Arguments(Data pArg1, Data pArg2, Data pArg3, Data pArg4)
        {
            return new Data(new ArgumentList {pArg1, pArg2, pArg3, pArg4});
        }

        /// <summary>
        /// Handles passing arguments to the call method.
        /// </summary>
        [ExecuteSymbol(GrammarSymbol.ArgumentList)]
        public Data Arguments(Data pArg1, Data pArg2, Data pArg3, Data pArg4, Data pArg5)
        {
            return new Data(new ArgumentList {pArg1, pArg2, pArg3, pArg4, pArg5});
        }

        /// <summary>
        /// Handles passing arguments to the call method.
        /// </summary>
        [ExecuteSymbol(GrammarSymbol.ArgumentList)]
        public Data Arguments(Data pArg1, Data pArg2, Data pArg3, Data pArg4, Data pArg5, Data pArg6)
        {
            return new Data(new ArgumentList {pArg1, pArg2, pArg3, pArg4, pArg5, pArg6});
        }

        /// <summary>
        /// Executes an identify as a function.
        /// </summary>
        [ExecuteSymbol(GrammarSymbol.CallExpression)]
        public Data Call(Data pClosure)
        {
            return Call(pClosure, Data.Undefined);
        }

        /// <summary>
        /// Executes a closure a function.
        /// </summary>
        [ExecuteSymbol(GrammarSymbol.CallExpression)]
        public Data Call(Data pClosure, Data pArguments)
        {
            try
            {
                // calling base constructor
                if (pClosure.Type == typeof (Alias))
                {
                    Alias a = pClosure.getAlias();
                    Instance inst = Executor.Cursor.Heap.Get(a);
                    Dictionary<string, Data> variables = Runtime.Arguments.CollectArguments(inst.Constructor, pArguments);
                    return Executor.Execute(inst.Constructor, variables);
                }
                else
                {
                    Closure closure = pClosure.getClosure();
                    // empty function check
                    if (closure.Function.Children.Count == 0)
                    {
                        return Data.Undefined;
                    }
                    Dictionary<string, Data> variables = Runtime.Arguments.CollectArguments(closure.Function, pArguments);
                    variables.Add("this", closure.This);
                    return Executor.Execute(closure.Function.Children[0], variables);
                }
            }
            catch (ReturnException returnData)
            {
                return returnData.Value;
            }
        }

        /// <summary>
        /// Executes an internal function.
        /// </summary>
        [ExecuteSymbol(GrammarSymbol.CallInternal)]
        public Data CallInternal(Data pIdentifier, Data pArguments)
        {
            string name = pIdentifier.getIdentifier().Name;
            return Executor.Execute(name, pArguments.getArgumentList());
        }

        /// <summary>
        /// Performs a return exception to break out of the function.
        /// </summary>
        [ExecuteSymbol(GrammarSymbol.ReturnProc)]
        public Data Return()
        {
            throw new ReturnException(Data.Undefined);
        }

        /// <summary>
        /// Performs a return exception to break out of the function.
        /// </summary>
        [ExecuteSymbol(GrammarSymbol.ReturnProc)]
        public Data Return(Data pData)
        {
            throw new ReturnException(pData);
        }
    }
}