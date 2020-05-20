using ReGoap.Core;
using ReGoap.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perro_Descansar_Action : ReGoapAction<string, object>
{
    [SerializeField]
    private Perro_Controller controller;

    protected override void Awake()
    {
        base.Awake();
        preconditions.Set("Descansando", false);
        effects.Set("Descansando", true);
    }

    public override void Run(IReGoapAction<string, object> previous, IReGoapAction<string, object> next, ReGoapState<string, object> settings, ReGoapState<string, object> goalState, Action<IReGoapAction<string, object>> done, Action<IReGoapAction<string, object>> fail)
    {
        base.Run(previous, next, settings, goalState, done, fail);
        doneCallback(this);
        Debug.Log("Runned Descansando");
        controller.stay_in_pos();

    }

    public override void Exit(IReGoapAction<string, object> next)
    {
        base.Exit(next);
        agent.GetMemory().GetWorldState().Set("Descansando", true);
    }
}


