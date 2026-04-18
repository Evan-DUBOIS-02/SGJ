using UnityEngine;
using UnityEditor.AssetImporters;
using Unity.GraphToolkit.Editor;
using System;
using System.Collections.Generic;
using System.Linq;

[ScriptedImporter(1, DialogueGraph.AssetExtension)]
public class DialogueGraphImporter : ScriptedImporter
{
    public override void OnImportAsset(AssetImportContext ctx)
    {
        DialogueGraph editorGraph = GraphDatabase.LoadGraphForImporter<DialogueGraph>(ctx.assetPath);
        RuntimeDialogueGraph runtimeGraph = ScriptableObject.CreateInstance<RuntimeDialogueGraph>();
        Dictionary<INode, string> nodeIDMap = new Dictionary<INode, string>();

        foreach (INode node in editorGraph.GetNodes())
        {
            nodeIDMap[node] = Guid.NewGuid().ToString();
        }
        
        StartNode startNode = editorGraph.GetNodes().OfType<StartNode>().FirstOrDefault();
        if (startNode != null)
        {
            IPort entryPort = startNode.GetOutputPorts().FirstOrDefault()?.firstConnectedPort;
            if (entryPort != null)
            {
                runtimeGraph.EntryNodeId = nodeIDMap[entryPort.GetNode()];
            }
        }

        foreach (INode node in editorGraph.GetNodes())
        {
            if (node is StartNode || node is EndNode) continue;
            
            RuntimeDialogueNode runtimeNode = new RuntimeDialogueNode{ NodeId = nodeIDMap[node] };
            if (node is DialogueNode dialogueNode)
            {
                ProcessDialogueNode(dialogueNode, runtimeNode, nodeIDMap);
            }
            else if(node is ChoiceNode choiceNode)
            {
                ProcessChoiceNode(choiceNode, runtimeNode, nodeIDMap);
            }
            
            runtimeGraph.AllNodes.Add(runtimeNode);
        }
        
        ctx.AddObjectToAsset("RuntimeData", runtimeGraph);
        ctx.SetMainObject(runtimeGraph);
    }

    private void ProcessDialogueNode(DialogueNode node, RuntimeDialogueNode runtimeNode,
        Dictionary<INode, string> nodeIDMap)
    {
        runtimeNode.DialogueText = GetPortValue<string>(node.GetInputPortByName("Dialogue"));
        
        var nextNodePort = node.GetOutputPortByName("out")?.firstConnectedPort;
        if (nextNodePort != null)
            runtimeNode.NextNodeId = nodeIDMap[nextNodePort.GetNode()];
    }

    private void ProcessChoiceNode(ChoiceNode node, RuntimeDialogueNode runtimeNode,
        Dictionary<INode, string> nodeIDMap)
    {
        node.GetNodeOptionByName("requestData").TryGetValue(out RequestData data);
        if (data == null) return;

        runtimeNode.RequestData = data;
        runtimeNode.DialogueText = data.Description;
        
        IEnumerable<IPort> choiceOutputPorts = node.GetOutputPorts().Where(p => p.name.StartsWith("Choice "));
        if (choiceOutputPorts.Count() != data.Choices.Count) return;
        
        for(int i = 0; i <  data.Choices.Count; i++)
        {
            ChoiceNodeData choice = new ChoiceNodeData();
            choice.ChoiceData = data.Choices[i];
            
            var outputPort = choiceOutputPorts.ElementAt(i);
            choice.DestinationNodeId = outputPort.firstConnectedPort != null
                ? nodeIDMap[outputPort.firstConnectedPort.GetNode()]
                : null;
            
            runtimeNode.Choices.Add(choice);
        }
    }

    private T GetPortValue<T>(IPort port)
    {
        if(port == null) return default;
        
        if (port.isConnected)
        {
            if (port.firstConnectedPort.GetNode() is IVariableNode variableNode)
            {
                variableNode.variable.TryGetDefaultValue(out T value);
                return value;
            }
        }
        
        port.TryGetValue(out T fallbackValue);
        return fallbackValue;
    }
}
