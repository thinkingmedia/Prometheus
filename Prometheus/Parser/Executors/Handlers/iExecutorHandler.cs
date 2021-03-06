﻿using Prometheus.Nodes;
using Prometheus.Nodes.Types.Bases;

namespace Prometheus.Parser.Executors.Handlers
{
    /// <summary>
    /// Objects that handle the execution of a node.
    /// </summary>
    public interface iExecutorHandler
    {
        /// <summary>
        /// The unique ID of the handler.
        /// </summary>
        int GetHandlerCode();

        /// <summary>
        /// Handle execution of a node.
        /// </summary>
        DataType Handle(Node pNode);
    }
}