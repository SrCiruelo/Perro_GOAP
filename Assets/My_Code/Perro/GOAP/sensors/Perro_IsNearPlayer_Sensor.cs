using ReGoap.Core;
using ReGoap.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perro_IsNearPlayer_Sensor : ReGoapSensor<string,object>
{
    [SerializeField]
    private float max_distance_to_obj;
    [SerializeField]
    private GameObject player;
    Perro_Agent perro_agent;

    private void Awake()
    {
        
    }
    public override void Init(IReGoapMemory<string, object> memory)
    {
        base.Init(memory);

        memory.GetWorldState().Set("IsNearPlayer", false);
        perro_agent = GetComponent<Perro_Agent>();
    }
    public override void UpdateSensor()
    {
        
        ReGoapState<string,object> state = memory.GetWorldState();

        if((bool)memory.GetWorldState().Get("IsNearPlayer")== true)
        {
            Vector3 offset_with_player = (Vector3)memory.GetWorldState().Get("offset_with_player");
            Vector3 Nav_real_point;
            NavMeshFunctions.get_navmesh_point(player.transform.position + offset_with_player, out Nav_real_point);
            Vector3 Nav_self_point ;
            NavMeshFunctions.get_navmesh_point(transform.position, out Nav_self_point);

            if ((Nav_real_point - Nav_self_point).sqrMagnitude > max_distance_to_obj * max_distance_to_obj)
            {
                Debug.Log("Not near Player anymore: " + player.transform.position);
                memory.GetWorldState().Set("IsNearPlayer", false);
                perro_agent.state_change();
            }
        }
        //state.Set("startPosition", transform.position);
    }
}
