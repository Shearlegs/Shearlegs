﻿using Shearlegs.Web.API.Models.NodeDaemons;
using Shearlegs.Web.API.Models.NodeDaemons.Params;
using Shearlegs.Web.API.Models.Nodes;
using Shearlegs.Web.API.Models.Nodes.Params;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Orchestrations.Nodes
{
    public interface INodeOrchestrationService
    {
        ValueTask<Node> AddNodeAsync(AddNodeParams @params);
        ValueTask<ExecutePluginResult> ExecutePluginAsync(int nodeId, ExecutePluginParams @params);
        ValueTask<ProcessedPluginInfo> ProcessPluginAsync(int nodeId, ProcessPluginParams @params);
        ValueTask<IEnumerable<Node>> RetrieveAllNodesAsync();
        ValueTask<Node> RetrieveNodeByIdAsync(int nodeId);
        ValueTask<Node> RetrieveNodeByNameAsync(string nodeName);
        ValueTask<NodeDaemonStatistics> RetrieveNodeDaemonByIdAsync(int nodeId);
        ValueTask<NodeDaemonInfo> RetrieveNodeDaemonInfoByIdAsync(int nodeId);
        ValueTask<Node> UpdateNodeAsync(UpdateNodeParams @params);
    }
}