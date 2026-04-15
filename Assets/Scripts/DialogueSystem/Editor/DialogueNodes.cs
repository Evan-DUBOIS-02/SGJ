using Unity.GraphToolkit.Editor;
using System;
using UnityEditor.Localization.Plugins.XLIFF.V12;

/*
 * THIS FILE CONTAINS ALL NODE TYPE DEFINITION NEEDED FOR OUT DIALOGUE SYSTEM
 */

/*
 * Define the start of the graph
 */
[Serializable]
public class StartNode : Node
{
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        // Contains only one output
        context.AddOutputPort("out").Build();
    }
}

/*
 * Define the end of the graph
 */
[Serializable]
public class EndNode : Node
{
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        // Contains only one input
        context.AddInputPort("in").Build();
    }
}

/*
 * Define a simple dialogue node
 */
[Serializable]
public class DialogueNode : Node
{
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        // Contains one input and one output for the flow
        context.AddInputPort("in").Build();
        context.AddOutputPort("out").Build();
        
        // Contains some input datas
        context.AddInputPort<string>("Speaker").Build();
        context.AddInputPort<string>("Dialogue").Build();
    }
}

/*
 * Define a choice dialogue node
 */
[Serializable]
public class ChoiceNode : Node
{
    private const string requestData = "requestData";
    
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        // Contains one input for the flow
        context.AddInputPort("in").Build();

        // Contains variables numbers of choices with output for the flow
        var option = GetNodeOptionByName(requestData);
        option.TryGetValue(out RequestData data);
        if (data == null) return;
        for (int i = 0; i < data.Choices.Count; i++)
        {
            context.AddOutputPort($"Choice {i}").WithDisplayName(data.Choices[i].ChoiceText).Build();
        }
    }

    // Allow to define a node option
    protected override void OnDefineOptions(IOptionDefinitionContext context)
    {
        // Define the request data
        context.AddOption<RequestData>(requestData).Delayed();
    }
}