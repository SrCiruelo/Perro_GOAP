using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReGoap.Unity;
using ReGoap.Core;
using System;
using System.Runtime.Remoting.Messaging;

public class Perro_MoveToBall_Action : ReGoapAction<string,object>
{
    [SerializeField]
    private float get_to_ball_radius;
    [SerializeField]
    private Perro_Controller controller;
    [SerializeField]
    private picker perro_picker;

    protected override void Awake()
    {
        base.Awake();

        preconditions.Set("IsNearBall", false);
        preconditions.Set("CanSeeBall", true);
        effects.Set("IsNearBall", true);
        effects.Set("IsNearPlayer", false);
    }

    public override void Run(IReGoapAction<string, object> previous, IReGoapAction<string, object> next, ReGoapState<string, object> settings, ReGoapState<string, object> goalState, Action<IReGoapAction<string, object>> done, Action<IReGoapAction<string, object>> fail)
    {
        base.Run(previous, next, settings, goalState, done, fail);

        GameObject ball_obj = (GameObject)agent.GetMemory().GetWorldState().Get("BallObject");

        Vector3 offset_with_ball = (transform.position - ball_obj.transform.position).normalized * get_to_ball_radius;
        offset_with_ball = new Vector3(offset_with_ball.x, 0, offset_with_ball.z);
        Debug.Log("Moving to Ball,  Next:" + next.ToString());
        try
        {
            controller.StartCoroutine(controller.move_towards_object_relative_pos(ball_obj, offset_with_ball, () => doneCallback(this), () => failCallback(this), () => { return perro_picker.can_pick; }));
        }
        catch (System.Exception err)
        {
            Debug.Log(err);
            failCallback(this);
        }
    }

    public override void Exit(IReGoapAction<string, object> next)
    {
        base.Exit(next);
        agent.GetMemory().GetWorldState().Set("IsNearBall", true);
        agent.GetMemory().GetWorldState().Set("IsNearPlayer", false);
    }
}
