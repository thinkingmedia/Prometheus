﻿using System;
using System.Collections.Generic;
using Prometheus.Compile;
using Prometheus.Exceptions.Compiler;
using Prometheus.Exceptions.Executor;
using Prometheus.Grammar;
using Prometheus.Nodes;
using Prometheus.Nodes.Types;
using Prometheus.Nodes.Types.Bases;
using Prometheus.Parser.Executors;
using Prometheus.Parser.Executors.Attributes;

namespace Prometheus.Runtime
{
    /// <summary>
    /// Implements the operators for greater than and less than.
    /// </summary>
    // ReSharper disable UnusedParameter.Global
    // ReSharper disable UnusedMember.Global
    // ReSharper disable MemberCanBePrivate.Global
    // ReSharper disable ClassNeverInstantiated.Global
    public class Relational : ExecutorGrammar, iOptimizer
    {
        /// <summary>
        /// A list of relational operators
        /// </summary>
        private static readonly HashSet<GrammarSymbol> _compareSymbols = new HashSet<GrammarSymbol>
                                                                         {
                                                                             GrammarSymbol.GtOperator,
                                                                             GrammarSymbol.LtOperator,
                                                                             GrammarSymbol.GteOperator,
                                                                             GrammarSymbol.LteOperator,
                                                                             GrammarSymbol.EqualOperator,
                                                                             GrammarSymbol.NotEqualOperator,
                                                                             GrammarSymbol.AndOperator,
                                                                             GrammarSymbol.OrOperator
                                                                         };

        /// <summary>
        /// Checks if a node performs math operations on two constant values.
        /// </summary>
        /// <param name="pNode">The node to check</param>
        /// <returns>True if it can be reduced.</returns>
        private static bool CanReduce(Node pNode)
        {
            return (_compareSymbols.Contains(pNode.Symbol)
                    && pNode.Children[0].Symbol == GrammarSymbol.Value
                    && pNode.Children[1].Symbol == GrammarSymbol.Value);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Relational(Executor pExecutor)
            : base(pExecutor)
        {
        }

        /// <summary>
        /// Filter what nodes this optimizer is executed for.
        /// </summary>
        public bool Optimizable(GrammarSymbol pType)
        {
            return _compareSymbols.Contains(pType);
        }

        /// <summary>
        /// Called for each node in the tree. Implement this to
        /// modify just the node.
        /// </summary>
        /// <returns>True if tree was modified.</returns>
        public bool OptimizeNode(Node pNode)
        {
            if (pNode.Children.Count != 2 
                || pNode.Data.Count != 0
                || !CanReduce(pNode))
            {
                return false;
            }

            if (pNode.Children[0].Data.Count != 1
                || pNode.Children[1].Data.Count != 1)
            {
                return false;
            }

            DataType valueA = pNode.Children[0].Data[0];
            DataType valueB = pNode.Children[1].Data[0];

            pNode.Children.Clear();

            switch (pNode.Symbol)
            {
                case GrammarSymbol.GtOperator:
                    pNode.Data.Add(GreaterThan(pNode, valueA, valueB));
                    break;
                case GrammarSymbol.LtOperator:
                    pNode.Data.Add(LessThan(pNode, valueA, valueB));
                    break;
                case GrammarSymbol.GteOperator:
                    pNode.Data.Add(GreaterThanEqual(pNode, valueA, valueB));
                    break;
                case GrammarSymbol.LteOperator:
                    pNode.Data.Add(LessThanEqual(pNode, valueA, valueB));
                    break;
                case GrammarSymbol.EqualOperator:
                    pNode.Data.Add(Equal(pNode, valueA, valueB));
                    break;
                case GrammarSymbol.NotEqualOperator:
                    pNode.Data.Add(NotEqual(pNode, valueA, valueB));
                    break;
                case GrammarSymbol.AndOperator:
                    pNode.Data.Add(AndOp(pNode, valueA, valueB));
                    break;
                case GrammarSymbol.OrOperator:
                    pNode.Data.Add(OrOp(pNode, valueA, valueB));
                    break;
                default:
                    throw new UnsupportedDataTypeException(
                        string.Format("Cannot optimize <{0}> value type", pNode.Symbol), pNode.Location);
            }

            pNode.Symbol = GrammarSymbol.Value;

            return true;
        }

        /// <summary>
        /// Inspect a node
        /// </summary>
        /// <param name="pParent"></param>
        /// <param name="pChild"></param>
        /// <returns>Same node, a new node or null to remove it.</returns>
        public bool OptimizeChild(Node pParent, Node pChild)
        {
            return false;
        }

        /// <summary>
        /// Called when a parent node matches the handled type. Implement this to
        /// modify the parent to child relationship.
        /// </summary>
        /// <returns>True if tree was modified.</returns>
        public bool OptimizeParent(Node pParent, Node pChild)
        {
            return false;
        }

        /// <summary>
        /// Inspect a node after optimization has finished. This method
        /// is called only once per node.
        /// </summary>
        public void OptimizePost(Node pNode)
        {
        }

        /// <summary>
        /// Checks if an array contains a data type using relational equals operator.
        /// </summary>
        public static bool Contains(ArrayType pArr, DataType pValue)
        {
            for (int i = 0, c = pArr.Values.Count; i < c; i++)
            {
                if (DataEqual(pArr.Values[i], pValue))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Used to recursively compare arrays.
        /// </summary>
        public static bool DataEqual(DataType pValue1, DataType pValue2)
        {
            // same object reference
            if (pValue1 == pValue2)
            {
                return true;
            }

            // compare strings
            StringType str1 = pValue1 as StringType;
            StringType str2 = pValue2 as StringType;
            if (str1 != null && str2 != null)
            {
                return str1.IgnoreCase || str2.IgnoreCase
                    ? String.Compare(str1.Value, str2.Value, StringComparison.CurrentCultureIgnoreCase) == 0
                    : String.CompareOrdinal(str1.Value, str2.Value) == 0;
            }

            // compare numbers
            NumericType num1 = pValue1 as NumericType;
            NumericType num2 = pValue2 as NumericType;
            if (num1 != null && num2 != null)
            {
                if (num1.Type == num2.Type && num1.isLong)
                {
                    return num1.Long == num2.Long;
                }
                return System.Math.Abs(num1.Double - num2.Double) < 0.0000000000001;
            }

            // compare boolean
            BooleanType bool1 = pValue1 as BooleanType;
            BooleanType bool2 = pValue2 as BooleanType;
            if (bool1 != null && bool2 != null)
            {
                return bool1.Value == bool2.Value;
            }

            // compare pointers
            InstanceType inst1 = pValue1 as InstanceType;
            InstanceType inst2 = pValue2 as InstanceType;
            if (inst1 != null && inst2 != null)
            {
                return inst1 == inst2;
            }

            // compare with undefined
            UndefinedType undefined1 = pValue1 as UndefinedType;
            UndefinedType undefined2 = pValue2 as UndefinedType;
            if (undefined1 != null && undefined2 != null)
            {
                return true;
            }
            if (undefined1 != null || undefined2 != null)
            {
                return false;
            }

            // TODO: This might get stuck in a recursion loop if the array contains a reference to the same array twice
            // compare array
            ArrayType array1 = pValue1 as ArrayType;
            ArrayType array2 = pValue2 as ArrayType;
            if (array1 != null && array2 != null)
            {
                if (array1.Count != array2.Count)
                {
                    return false;
                }
                bool result = true;
                for (int i = 0, c = array1.Count; i < c; i++)
                {
                    result &= DataEqual(array1[i], array2[i]);
                }
                return result;
            }

            return false;
        }

        /// <summary>
        /// AND operator
        /// </summary>
        [ExecuteSymbol(GrammarSymbol.AndOperator)]
        public DataType AndOp(Node pNode, DataType pValue1, DataType pValue2)
        {
            pValue1 = Resolve(pValue1);
            pValue2 = Resolve(pValue2);

            BooleanType bool1 = pValue1 as BooleanType;
            BooleanType bool2 = pValue2 as BooleanType;
            if (bool1 != null && bool2 != null)
            {
                return new BooleanType(bool1.Value && bool2.Value);
            }

            throw DataTypeException.InvalidTypes("AND", pValue1, pValue2);
        }

        /// <summary>
        /// ~ operator
        /// </summary>
        [ExecuteSymbol(GrammarSymbol.BitInvertOperator)]
        public DataType Bitwise(Node pNode, DataType pValue)
        {
            pValue = Resolve(pValue);

            NumericType num = pValue as NumericType;
            if (num != null && num.isLong)
            {
                return new NumericType(~num.Long);
            }

            throw DataTypeException.InvalidTypes("~", pValue);
        }

        /// <summary>
        /// Equal
        /// </summary>
        [ExecuteSymbol(GrammarSymbol.EqualOperator)]
        public DataType Equal(Node pNode, DataType pValue1, DataType pValue2)
        {
            DataType data1 = Resolve(pValue1);
            DataType data2 = Resolve(pValue2);
            return new BooleanType(DataEqual(data1, data2));
        }

        /// <summary>
        /// Not operator
        /// </summary>
        [ExecuteSymbol(GrammarSymbol.NotOperator)]
        public DataType Equal(Node pNode, DataType pValue1)
        {
            pValue1 = Resolve(pValue1);

            BooleanType bool1 = pValue1 as BooleanType;
            if (bool1 != null)
            {
                return new BooleanType(!bool1.Value);
            }

            throw DataTypeException.InvalidTypes("NOT", pValue1);
        }

        /// <summary>
        /// Greater Than
        /// </summary>
        [ExecuteSymbol(GrammarSymbol.GtOperator)]
        public DataType GreaterThan(Node pNode, DataType pValue1, DataType pValue2)
        {
            pValue1 = Resolve(pValue1);
            pValue2 = Resolve(pValue2);

            StringType str1 = pValue1 as StringType;
            StringType str2 = pValue2 as StringType;
            if (str1 != null && str2 != null)
            {
                return new BooleanType(String.CompareOrdinal(str1.Value, str2.Value) > 0);
            }

            NumericType num1 = pValue1 as NumericType;
            NumericType num2 = pValue2 as NumericType;
            if (num1 != null && num2 != null)
            {
                if (num1.Type == num2.Type && num1.Type == typeof (long))
                {
                    return new BooleanType(num1.Long > num2.Long);
                }
                return new BooleanType(num1.Double > num2.Double);
            }

            throw DataTypeException.InvalidTypes(">", pValue1, pValue2);
        }

        /// <summary>
        /// Greater Than
        /// </summary>
        [ExecuteSymbol(GrammarSymbol.GteOperator)]
        public DataType GreaterThanEqual(Node pNode, DataType pValue1, DataType pValue2)
        {
            pValue1 = Resolve(pValue1);
            pValue2 = Resolve(pValue2);

            StringType str1 = pValue1 as StringType;
            StringType str2 = pValue2 as StringType;
            if (str1 != null && str2 != null)
            {
                return new BooleanType(String.CompareOrdinal(str1.Value, str2.Value) >= 0);
            }

            NumericType num1 = pValue1 as NumericType;
            NumericType num2 = pValue2 as NumericType;
            if (num1 != null && num2 != null)
            {
                if (num1.Type == num2.Type && num1.Type == typeof (long))
                {
                    return new BooleanType(num1.Long >= num2.Long);
                }
                return new BooleanType(num1.Double >= num2.Double);
            }

            throw DataTypeException.InvalidTypes(">=", pValue1, pValue2);
        }

        /// <summary>
        /// Greater Than
        /// </summary>
        [ExecuteSymbol(GrammarSymbol.LtOperator)]
        public DataType LessThan(Node pNode, DataType pValue1, DataType pValue2)
        {
            pValue1 = Resolve(pValue1);
            pValue2 = Resolve(pValue2);

            StringType str1 = pValue1 as StringType;
            StringType str2 = pValue2 as StringType;
            if (str1 != null && str2 != null)
            {
                return new BooleanType(String.CompareOrdinal(str1.Value, str2.Value) < 0);
            }

            NumericType num1 = pValue1 as NumericType;
            NumericType num2 = pValue2 as NumericType;
            if (num1 != null && num2 != null)
            {
                if (num1.Type == num2.Type && num1.Type == typeof (long))
                {
                    return new BooleanType(num1.Long < num2.Long);
                }
                return new BooleanType(num1.Double < num2.Double);
            }

            throw DataTypeException.InvalidTypes("<", pValue1, pValue2);
        }

        /// <summary>
        /// Greater Than
        /// </summary>
        [ExecuteSymbol(GrammarSymbol.LteOperator)]
        public DataType LessThanEqual(Node pNode, DataType pValue1, DataType pValue2)
        {
            pValue1 = Resolve(pValue1);
            pValue2 = Resolve(pValue2);

            StringType str1 = pValue1 as StringType;
            StringType str2 = pValue2 as StringType;
            if (str1 != null && str2 != null)
            {
                return new BooleanType(String.CompareOrdinal(str1.Value, str2.Value) <= 0);
            }

            NumericType num1 = pValue1 as NumericType;
            NumericType num2 = pValue2 as NumericType;
            if (num1 != null && num2 != null)
            {
                if (num1.Type == num2.Type && num1.Type == typeof (long))
                {
                    return new BooleanType(num1.Long <= num2.Long);
                }
                return new BooleanType(num1.Double <= num2.Double);
            }

            throw DataTypeException.InvalidTypes("<=", pValue1, pValue2);
        }

        /// <summary>
        /// - operator
        /// </summary>
        [ExecuteSymbol(GrammarSymbol.NegOperator)]
        public DataType Negative(Node pNode, DataType pValue)
        {
            pValue = Resolve(pValue);

            NumericType num = pValue as NumericType;
            if (num != null)
            {
                return num.isLong
                    ? new NumericType(-num.Long)
                    : new NumericType(-num.Double);
            }
            throw DataTypeException.InvalidTypes("-", pValue);
        }

        /// <summary>
        /// Not equal
        /// </summary>
        [ExecuteSymbol(GrammarSymbol.NotEqualOperator)]
        public DataType NotEqual(Node pNode, DataType pValue1, DataType pValue2)
        {
            pValue1 = Resolve(pValue1);
            pValue2 = Resolve(pValue2);

            return new BooleanType(!DataEqual(pValue1, pValue2));
        }

        /// <summary>
        /// OR operator
        /// </summary>
        [ExecuteSymbol(GrammarSymbol.OrOperator)]
        public DataType OrOp(Node pNode, DataType pValue1, DataType pValue2)
        {
            pValue1 = Resolve(pValue1);
            pValue2 = Resolve(pValue2);

            BooleanType bool1 = pValue1 as BooleanType;
            BooleanType bool2 = pValue2 as BooleanType;
            if (bool1 != null && bool2 != null)
            {
                return new BooleanType(bool1.Value || bool2.Value);
            }

            throw DataTypeException.InvalidTypes("OR", pValue1, pValue2);
        }

        /// <summary>
        /// + operator
        /// Doesn't change the value, but can only work on numeric types.
        /// </summary>
        [ExecuteSymbol(GrammarSymbol.PlusOperator)]
        public DataType Plus(Node pNode, DataType pValue)
        {
            pValue = Resolve(pValue);

            NumericType num = pValue as NumericType;
            if (num != null)
            {
                return num;
            }
            throw DataTypeException.InvalidTypes("+", pValue);
        }

        /// <summary>
        /// x-- operator
        /// </summary>
        [ExecuteSymbol(GrammarSymbol.PostDecOperator)]
        public DataType PostDec(Node pNode, QualifiedType pValue)
        {
            iVariablePointer pointer = Cursor.Resolve(pValue);
            NumericType num = pointer.Read() as NumericType;
            if (num == null)
            {
                throw DataTypeException.InvalidTypes("--", pValue);
            }

            num = num.isLong
                ? new NumericType(num.Long - 1)
                : new NumericType(num.Double - 1.0);
            pointer.Write(num);
            return num;
        }

        /// <summary>
        /// x++ operator
        /// </summary>
        [ExecuteSymbol(GrammarSymbol.PostIncOperator)]
        public DataType PostInc(Node pNode, QualifiedType pValue)
        {
            iVariablePointer pointer = Cursor.Resolve(pValue);
            NumericType num = pointer.Read() as NumericType;
            if (num == null)
            {
                throw DataTypeException.InvalidTypes("++", pValue);
            }

            num = num.isLong
                ? new NumericType(num.Long + 1)
                : new NumericType(num.Double + 1.0);
            pointer.Write(num);

            return num;
        }

        /// <summary>
        /// --x operator
        /// </summary>
        [ExecuteSymbol(GrammarSymbol.PreDecOperator)]
        public DataType PreDec(Node pNode, QualifiedType pValue)
        {
            iVariablePointer pointer = Cursor.Resolve(pValue);
            NumericType num = pointer.Read() as NumericType;
            if (num == null)
            {
                throw DataTypeException.InvalidTypes("--", pValue);
            }

            pointer.Write(num.isLong
                ? new NumericType(num.Long - 1)
                : new NumericType(num.Double - 1.0));
            return num;
        }

        /// <summary>
        /// ++x operator
        /// </summary>
        [ExecuteSymbol(GrammarSymbol.PreIncOperator)]
        public DataType PreInc(Node pNode, QualifiedType pValue)
        {
            iVariablePointer pointer = Cursor.Resolve(pValue);
            NumericType num = pointer.Read() as NumericType;
            if (num == null)
            {
                throw DataTypeException.InvalidTypes("++", pValue);
            }

            pointer.Write(num.isLong
                ? new NumericType(num.Long + 1)
                : new NumericType(num.Double + 1.0));
            return num;
        }
    }
}