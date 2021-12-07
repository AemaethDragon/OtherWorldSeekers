using System;
using UnityEngine;

public class ConditionDT : INodeDT
{
    private INodeDT trueChild, falseChild;
    private Func<bool> condition;

    public ConditionDT(Func<bool> condition, INodeDT tChild, INodeDT fChild)
    {
        this.condition = condition;
        trueChild = tChild;
        falseChild = fChild;
    }
    
    public void Execute()
    {
        if (condition()) trueChild.Execute();
        else falseChild.Execute();
    }
}
