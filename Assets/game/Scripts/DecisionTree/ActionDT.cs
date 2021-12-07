using System;
using UnityEngine;

public class ActionDT : INodeDT
{
    private Action action;

    public ActionDT(Action action)
    {
        this.action = action;
    }
    
    public void Execute()
    {
        action();
    }
}
