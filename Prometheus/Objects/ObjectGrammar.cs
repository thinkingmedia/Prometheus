﻿using System.Collections.Generic;
using Prometheus.Exceptions.Executor;
using Prometheus.Grammar;
using Prometheus.Nodes;
using Prometheus.Nodes.Types;
using Prometheus.Nodes.Types.Bases;
using Prometheus.Parser.Executors;
using Prometheus.Parser.Executors.Attributes;
using Prometheus.Storage;

namespace Prometheus.Objects
{
    /// <summary>
    /// Handles grammar related to declaring objects.
    /// </summary>
    public class ObjectGrammar : ExecutorGrammar
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ObjectGrammar(Executor pExecutor)
            : base(pExecutor)
        {
        }

        /// <summary>
        /// Lists object declarations.
        /// </summary>
        [ExecuteSymbol(GrammarSymbol.ListObjects)]
        public DataType ListObjects()
        {
            Executor.Cursor.Packages.Print();
            return DataType.Undefined;
        }

        /// <summary>
        /// </summary>
        [ExecuteSymbol(GrammarSymbol.NewExpression)]
        public DataType New(IdentifierType pIdentifier)
        {
            return New(pIdentifier, DataType.Undefined);
        }

        /// <summary>
        /// Instantiates an object instance and returns a reference to that object.
        /// </summary>
        [ExecuteSymbol(GrammarSymbol.NewExpression)]
        public DataType New(IdentifierType pIdentifier, DataType pArguments)
        {
            Declaration decl = Executor.Cursor.Packages.Get(pIdentifier);

            CreateInherited created = new CreateInherited(Executor.Cursor.Heap, decl);

            MemorySpace prevMemory = Executor.Cursor.Stack;
            Executor.Cursor.Stack = created.Inst.Members;

            try
            {
                Executor.Execute(created.Inst.Constructor, new Dictionary<string, DataType>());
            }
            catch (ReturnException)
            {
                // ignore return value from constructors
            }
            finally
            {
                Executor.Cursor.Stack = prevMemory;
                created.Inst.Members.Unset("this");
            }

            return created.Alias;
        }

        /// <summary>
        /// Declares a new object type
        /// </summary>
        [ExecuteSymbol(GrammarSymbol.ObjectDecl)]
        public DataType ObjectDeclare(DataType pBaseType, IdentifierType pIdentifier)
        {
            Node obj = Executor.Cursor.Node;
            Declaration baseDecl = null;
            if (pBaseType.GetType() == typeof (StaticType))
            {
                StaticType type = (StaticType)pBaseType;
                baseDecl = null; // TODO: Add an instance of a default object base.
            }
            else if (pBaseType.GetType() == typeof (QualifiedType))
            {
                QualifiedType baseType = (QualifiedType)pBaseType;
                baseDecl = Executor.Cursor.Packages.Get(baseType);
            }
            else
            {
                throw new UnexpectedErrorException(
                    string.Format("Can not declare object of base type <{0}>", pBaseType.GetType().FullName), obj);
            }

            Declaration decl = new Declaration(baseDecl, pIdentifier, obj);
            Executor.Cursor.Packages.Add(decl);

            return DataType.Undefined;
        }
    }
}