using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReGoap.Unity;
using ReGoap.Core;

public class Perro_CanSeePlayer_Sensor : ReGoapSensor<string,object>
{
    [SerializeField]
    private GameObject player;


    public override void Init(IReGoapMemory<string, object> memory)
    {
        base.Init(memory);

        memory.GetWorldState().Set("CanSeePlayer", true);

        memory.GetWorldState().Set("PlayerObject", player);
    }

    public override void UpdateSensor()
    {
        base.UpdateSensor();


        if (NavMeshFunctions.get_navmesh_point(player.transform.position))
        {
            memory.GetWorldState().Set("CanSeePlayer", true);
        }
        else
        {
            memory.GetWorldState().Set("CanSeePlayer", false);
        }
    }
}
