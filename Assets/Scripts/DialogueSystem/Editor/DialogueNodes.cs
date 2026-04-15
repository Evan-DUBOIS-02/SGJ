using Unity.GraphToolkit.Editor;
using System;

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
    private const string optionID = "portCount";
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        // Contains one input for the flow
        context.AddInputPort("in").Build();
        
        // Contains some input datas
        context.AddInputPort<string>("Speaker").Build();
        context.AddInputPort<string>("Dialogue").Build();

        // Contains variables numbers of choices with both input data and output for the flow
        var option = GetNodeOptionByName(optionID);
        option.TryGetValue(out int portCount);
        for (int i = 0; i < portCount; i++)
        {
            context.AddInputPort<string>($"Choice Text {i}").Build();
            context.AddOutputPort($"Choice {i}").Build();
        }
    }

    // Allow to define a node option
    protected override void OnDefineOptions(IOptionDefinitionContext context)
    {
        // Define number of choice available
        context.AddOption<int>(optionID).WithDefaultValue(2).Delayed();
    }
}