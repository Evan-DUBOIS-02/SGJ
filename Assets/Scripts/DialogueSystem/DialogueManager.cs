using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    // the graph to process
    public RuntimeDialogueGraph RuntimeGraph;
    // list of the nodes in the graph
    private Dictionary<string, RuntimeDialogueNode> _nodeLookup = new Dictionary<string, RuntimeDialogueNode>();
    // the current node processing
    private RuntimeDialogueNode _currentNode = null;
    public RuntimeDialogueNode CurrentNode {get {return _currentNode;}}

    private void Awake()
    {
        // Get all runtime nodes and add it into the dictionnary by id
        foreach (var node in RuntimeGraph.AllNodes)
        {
            _nodeLookup[node.NodeId] = node;
        }
        Init();
    }

    public void Init()
    {
        // If the graph have entry node, start the flow
        if (!string.IsNullOrEmpty(RuntimeGraph.EntryNodeId))
        {
            if (_nodeLookup.ContainsKey(RuntimeGraph.EntryNodeId))
            {
                _currentNode = _nodeLookup[RuntimeGraph.EntryNodeId];
            }
        }
    }

    public void SetNextNode(string nodeID)
    {
        if (!_nodeLookup.ContainsKey(nodeID))
        {
            _currentNode = null;
            return;
        }
        
        _currentNode = _nodeLookup[nodeID];
    }
}
