using ReGoap.Core;
using ReGoap.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perro_MoveToPlayer_Action : ReGoapAction<string,object>
{
    

    [SerializeField]
    private Perro_Controller controller;
    [SerializeField]
    private float around_player_radius;

    protected override void Awake()
    {
        base.Awake();
        preconditions.Set("IsNearPlayer", false);
        preconditions.Set("CanSeePlayer", true);

        effects.Set("IsNearPlayer", true);
        effects.Set("IsNearBall", false);
    }

    public override List<ReGoapState<string, object>> GetSettings(GoapActionStackData<string, object> stackData)
    {
        return base.GetSettings(stackData);
    }

    public override void Run(IReGoapAction<string, object> previous, IReGoapAction<string, object> next, ReGoapState<string, object> settings, ReGoapState<string, object> goalState, Action<IReGoapAction<string, object>> done, Action<IReGoapAction<string, object>> fail)
    {
        base.Run(previous, next, settings, goalState, done, fail);

        Vector3 next_point;
        GameObject player = (GameObject)agent.GetMemory().GetWorldState().Get("PlayerObject");
        if(NavMeshFunctions.Find_RandomPoint_InRadius(player.transform.position, around_player_radius, out next_point))
        {
            Vector3 offset_with_player = next_point - player.transform.position;
            agent.GetMemory().GetWorldState().Set("offset_with_player", offset_with_player);
            controller.StartCoroutine(controller.move_towards_object_relative_pos(player, offset_with_player, () => doneCallback(this), () => failCallback(this)));
        }
        else
        {
            failCallback(this);
        }
        
       
    }

    public override void Exit(IReGoapAction<string, object> next)
    {
        base.Exit(next);

        agent.GetMemory().GetWorldState().Set("IsNearPlayer", true);
        //agent.GetMemory().GetWorldState().Set("IsNearBall", false);
    }

}
