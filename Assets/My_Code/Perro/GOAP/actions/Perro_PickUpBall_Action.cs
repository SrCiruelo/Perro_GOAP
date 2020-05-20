using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReGoap.Unity;
using ReGoap.Core;
using System;

public class Perro_PickUpBall_Action : ReGoapAction<string,object>
{
    [SerializeField]
    private picker perro_picker;

    protected override void Awake()
    {
        base.Awake();
        preconditions.Set("IsNearBall", true);

        effects.Set("HasBall",true);
    }

    public override void Run(IReGoapAction<string, object> previous, IReGoapAction<string, object> next, ReGoapState<string, object> settings, ReGoapState<string, object> goalState, Action<IReGoapAction<string, object>> done, Action<IReGoapAction<string, object>> fail)
    {
        base.Run(previous, next, settings, goalState, done, fail);
        if (perro_picker.can_pick)
        {
            perro_picker.pick_obj();
            doneCallback(this);
        }
        else
        {
            failCallback(this);
        }
        
    }

    public override void Exit(IReGoapAction<string, object> next)
    {
        base.Exit(next);
        agent.GetMemory().GetWorldState().Set("HasBall", false);
    }
}
