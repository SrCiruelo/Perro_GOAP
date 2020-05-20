using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReGoap.Unity;
using ReGoap.Core;
using System;

public class Perro_CanSeeBall_Sensor : ReGoapSensor<string,object>
{
    [SerializeField]
    private GameObject ball;


    public override void Init(IReGoapMemory<string, object> memory)
    {
        base.Init(memory);

        //En este caso haremos que el perro siempre vea la pelota
        memory.GetWorldState().Set("CanSeeBall", true);

        memory.GetWorldState().Set("BallObject", ball);
    }

    public override void UpdateSensor()
    {
        base.UpdateSensor();
        if (NavMeshFunctions.get_navmesh_point(ball.transform.position))
        {
            memory.GetWorldState().Set("CanSeeBall", true);
        }
        else
        { 
            memory.GetWorldState().Set("CanSeeBall", false);
        }
    }
}
